using DeviceDetectorNET;
using DeviceDetectorNET.Class.Device;
using Kama.AppCore;
using Kama.FinancialAnalysis.DAL;
using Kama.FinancialAnalysis.Model;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Information;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Web.UI;

namespace Kama.FinancialAnalysis.Domain
{
    public class DistanceMeasurementHelper
    {
        DistanceMeasurementDataSource _dataSource = new DistanceMeasurementDataSource();

        static List<DistanceMeasurement> otherSymbol = new List<DistanceMeasurement>();
        public DistanceMeasurementHelper()
        {
        }
        public async Task Start()
        {
            Start(SymbolType.xauusd, SessionType.london);
        }
        private async Task Start(SymbolType symbolType, SessionType sessionType)
        {
            var dbIndexPrice= DbIndexPrice.GetByType(symbolType);
            var allPrice= DbIndexPrice.All(symbolType);
            otherSymbol = new List<DistanceMeasurement>();

            var list = (
                from b in DbIndexBiggerThanSD.Index
                join p in dbIndexPrice on b.PriceID equals p.ID
                join Ma in DbIndexMovingAverage.Index on p.ID equals Ma.ID
                join pMax in dbIndexPrice on b.MaxPriceID equals pMax.ID
                join pMin in dbIndexPrice on b.MinPriceID equals pMin.ID
                where b.Type == symbolType && b.Session == sessionType
                select new DistanceMeasurement
                {
                    ID = Guid.NewGuid(),
                    BiggerThanSdID = b.ID,
                    Rate = Math.Round(Math.Abs(p.Close - p.Open)/ b.R1000, 10),
                    Macd = Math.Round(Math.Abs(p.Close - Ma.D)/ b.R1000, 10),
                    UpGs = Math.Round((pMax.Close - p.Close) / b.R1000, 10),
                    DownGs = Math.Round((p.Close - pMin.Close) / b.R1000, 10),
                    Type = symbolType,
                    Session = sessionType
                }).ToList();

            var listSdPrice = (
                from b in DbIndexBiggerThanSD.Index
                join p in dbIndexPrice on b.PriceID equals p.ID
                where b.Type == symbolType && b.Session == sessionType
                select new { id = b.ID, obj = p }
                ).ToList();

            var listOtherSymbol = (
                from b in listSdPrice
                join p in allPrice on b.obj.Date equals p.Date
                select new { id = b.id, obj = p }
                ).ToList();


            var sdList4 = DbIndexStandardDeviation.Index.Where(x => x.Type != symbolType)
                .GroupBy(x => new { x.Type, x.Date.Hour, x.Date.Minute }).Select(x => x.First()).ToList();


            var listOther = (
                from p in listOtherSymbol
                join ma in DbIndexMovingAverage.Index on p.obj.ID equals ma.ID
                join s in sdList4 on new { p.obj.Date.Hour, p.obj.Date.Minute, p.obj.Type } equals new { s.Date.Hour, s.Date.Minute, s.Type }
                select new DistanceMeasurementJoin
                {
                    BiggerThanSdID = p.id,
                    Price = p.obj,
                    D = ma.D,
                    R1000 = s.R1000
                }).ToList();


            foreach (var item in listOther)
            {
                var minMAax = BiggerThanSDHelper.MinMAax(allPrice, item.Price.ID, item.Price.Type);
                var max = allPrice.FirstOrDefault(x => x.ID == minMAax[0]);
                var min = allPrice.FirstOrDefault(x => x.ID == minMAax[1]);

                otherSymbol.Add(new DistanceMeasurement
                {
                    ID = Guid.NewGuid(),
                    BiggerThanSdID = item.BiggerThanSdID,
                    Rate = Math.Round(Math.Abs(item.Price.Close - item.Price.Open) / item.R1000, 10),
                    Macd = Math.Round(Math.Abs(item.Price.Close - item.D) / item.R1000, 10),
                    UpGs = Math.Round((max.Close - item.Price.Close) / item.R1000, 10),
                    DownGs = Math.Round((item.Price.Close - min.Close) / item.R1000, 10),
                    Type = item.Price.Type,
                    Session = sessionType
                });
            }

            //await insertDb(list);
            await insertDb(otherSymbol);
            otherSymbol = new List<DistanceMeasurement>();
        }
        private async Task insertDb(List<DistanceMeasurement> model)
        {
            if (model.Count == 0)
                return;
            int i = 0;
            var insertList = new List<DistanceMeasurement>();
            foreach (var item in model)
            {
                i++;
                if (i > 1000)
                {
                    i = 0;
                    await _dataSource.AddAsync(insertList);
                    insertList = new List<DistanceMeasurement>();
                }
                insertList.Add(item);

            }
            if(insertList.Count> 0)
                await _dataSource.AddAsync(insertList);
        }
    }
}
