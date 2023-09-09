using DeviceDetectorNET.Results;
using Kama.AppCore;
using Kama.AppCore.IOC;
using Kama.FinancialAnalysis.DAL;
using Kama.FinancialAnalysis.Model;
using OfficeOpenXml.FormulaParsing.Excel.Functions.DateTime;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Information;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Logical;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kama.FinancialAnalysis.Domain
{
    public class StandardDeviationService
    {
        public async Task<Result> AddReng(List<PriceMinutely> addList)
        {
            var dataSource = new StandardDeviationDataSource();
            List<List<StandardDeviation>> insertList = new List<List<StandardDeviation>>();
            List<StandardDeviation> temporaryList = new List<StandardDeviation>();
            int i = 0;
            int i2 = 0;

            foreach (var item in addList)
            {
                i++;
                if (i > 1000)
                {
                    i2++;
                    if (i2 > 10)
                        break;
                    insertList.Add(temporaryList);
                    i = 0;
                    temporaryList = new List<StandardDeviation>();
                }
                var dyx = new StandardDeviation
                {
                    ID = item.ID,
                    Date = item.Date,
                    Type = item.Type,
                    R100 = Computing(item, 100),
                    R500 = Computing(item, 500),
                    R1000 = Computing(item, 1000),

                };
                temporaryList.Add(dyx);
            }

            if (temporaryList.Count > 0)
                insertList.Add(temporaryList);

            foreach (var insert in insertList)
                await dataSource.AddListAsync(insert);

            return Result.Successful();
        }
        public double Computing(PriceMinutely item, int reng)
        {

            List<PriceMinutely> listSymbol = new List<PriceMinutely>();
            List<PriceMinutely> list = new List<PriceMinutely>();
            listSymbol = DbIndex.GetByType(item.Type); 

            foreach (var l in listSymbol)
            {
                if (l.Date.Hour == item.Date.Hour && l.Date.Minute == item.Date.Minute)
                    list.Add(new PriceMinutely() {Close = l.Close, Open = l.Open });
                if (list.Count >= reng)
                    break;
            }

            if (list.Count == 0)
                return 0;

            foreach (var l in list)
                l.Close = l.Close - l.Open ;

            var average = list.Sum(x => x.Close) / list.Count;

            //open useing for Variance
            foreach (var l in list)
                l.Open = Math.Pow(l.Close - average, 2);

            var averageVariance = list.Sum(x => x.Open) / list.Count;
            return Math.Sqrt(averageVariance);

        }
    }
}
