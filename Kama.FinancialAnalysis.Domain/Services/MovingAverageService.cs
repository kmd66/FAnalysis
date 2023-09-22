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
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using YamlDotNet.Core;

namespace Kama.FinancialAnalysis.Domain
{
    public class MovingAverageService
    {
        MovingAverageDataSource dataSource = new MovingAverageDataSource();
        public async Task<Result> AddNullReng(List<PriceMinutely> addList)
        {
            List<MovingAverage> insert = new List<MovingAverage>();
            List<PriceMinutely> temporaryList = new List<PriceMinutely>();
            int i = 0;
            foreach (var item in addList)
            {
                i++;
                if (i > 1000)
                {
                    var result = await dataSource.GetEmptys(temporaryList.Select(x=>x.ID).ToList());
                    insert.AddRange(result.Data);
                    i = 0;
                    temporaryList = new List<PriceMinutely>();
                }
                temporaryList.Add(item);
            }
            if (temporaryList.Count > 0)
            {
                var result = await dataSource.GetEmptys(temporaryList.Select(x => x.ID).ToList());
                insert.AddRange(result.Data);
            }
            var query = (
                from p1 in insert
                join p2 in addList on p1.ID equals p2.ID
                select p2).ToList();

            return await AddReng(query);
        }
        public async Task<Result> AddReng(List<PriceMinutely> addList)
        {
            List<MovingAverage> temporaryList = new List<MovingAverage>();
            int i = 0;
            int i2= 0;
            foreach (var item in addList)
            {
                i++;
                if (i > 1000)
                {
                    //i2++;
                    //if (i2 > 10)
                    //    break;
                    await dataSource.AddListAsync(temporaryList);
                    i = 0;
                    temporaryList = new List<MovingAverage>();
                }
                var dyx = new MovingAverage
                {
                    ID = item.ID,
                    Date = item.Date,
                    Type = item.Type,
                    M5 = 0,
                    M30 = 0,
                    H1 = 0,
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
                var closeTime = DbIndex.GetSession((byte)item.Type).GetTimeColse();
                //var dt = item.Date.AddDays(-1);
                int y = item.Date.Year;
                int m = item.Date.Month;
                int d = item.Date.Day;
                var dateTime = new DateTime(y, m, d, closeTime[0], closeTime[1], 0);
                dateTime = dateTime.AddMinutes(-1);
                list = listSymbol.Where(x => x.Date > dateTime && x.Date <= item.Date).ToList();
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
