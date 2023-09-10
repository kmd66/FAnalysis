using DeviceDetectorNET.Results;
using Kama.AppCore;
using Kama.AppCore.IOC;
using Kama.FinancialAnalysis.DAL;
using Kama.FinancialAnalysis.Model;
using OfficeOpenXml.FormulaParsing.Excel.Functions;
using OfficeOpenXml.FormulaParsing.Excel.Functions.DateTime;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Information;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Logical;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kama.FinancialAnalysis.Domain
{
    public class MovingAverageService
    {
        public async Task<Result> AddReng(List<PriceMinutely> addList)
        {
            var dataSource = new MovingAverageDataSource();
            List<MovingAverage> temporaryList = new List<MovingAverage>();
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
                    await dataSource.AddListAsync(temporaryList);
                    i = 0;
                    temporaryList = new List<MovingAverage>();
                }
                var dyx = new MovingAverage
                {
                    ID = item.ID,
                    Date = item.Date,
                    Type = item.Type,
                    M5 = Computing(item, 5),
                    M30 = Computing(item, 30),
                    H1 = Computing(item, 60),
                    D = Computing(item, 0),
                };
                temporaryList.Add(dyx);
            }

            if (temporaryList.Count > 0)
                await dataSource.AddListAsync(temporaryList);

            return Result.Successful();
        }
        public double Computing(PriceMinutely item, int reng)
        {

            List<PriceMinutely> listSymbol = new List<PriceMinutely>();
            List<PriceMinutely> list = new List<PriceMinutely>();

            listSymbol = DbIndex.GetByType(item.Type);

            if (reng == 0)
            {
                var dt = item.Date.AddDays(-1);
                int y = dt.Date.Year;
                int m = dt.Date.Month;
                int d = dt.Date.Day;
                var dateTime = new DateTime(y, m, d,23,59,0);
                list = listSymbol.Where(x => x.Date > dateTime).ToList();
            }
            else
            {
                list = listSymbol.Where(x => x.ID <= item.ID).Take(reng).ToList();
            }

            if (list.Count == 0)
                return 0;

            return list.Sum(x => x.Close) / list.Count;

        }
    }
}
