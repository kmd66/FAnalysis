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
        BiggerThanSdDataSource _biggerThanSdDataSource = new BiggerThanSdDataSource();

        public DistanceMeasurementHelper()
        {
        }
        public async Task Start()
        {
            var biggerThanSDs = await _biggerThanSdDataSource.ListAsync(new BiggerThanSDVM());
            if (!biggerThanSDs.Success) System.Environment.Exit(500);
            DbIndexBiggerThanSD.Index = biggerThanSDs.Data.ToList();

            Start(SymbolType.xauusd, SessionType.london);
            await Start(SymbolType.xauusd, SessionType.newYork);
        }
        private async Task Start(SymbolType symbolType, SessionType sessionType)
        {
            var dbIndexPrice= DbIndexPrice.GetByType(symbolType);
            var allPrice= DbIndexPrice.All(symbolType);

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
                    Rate = Math.Round((p.Close - p.Open)/ b.R1000, 10),
                    Macd = Math.Round((p.Close - Ma.D)/ b.R1000, 10),
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


            var listOtherTemp = (
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

            var biggerThanSdOtherSymbols = new List<BiggerThanSD>();
            foreach (var item in listOtherTemp)
            {
                var minMAax = BiggerThanSDHelper.MinMAax(allPrice, item.Price.ID, item.Price.Type);
                item.pMAxID = minMAax[0];
                item.pMinID = minMAax[1];
                biggerThanSdOtherSymbols.Add(new BiggerThanSD { 
                    ID= item.BiggerThanSdID,
                    PriceID = item.Price.ID,
                    Type = item.Price.Type,
                    MaxPriceID = minMAax[0],
                    MinPriceID = minMAax[1],
                    R1000 = item.R1000,
                    Session = sessionType
                });
            }

            var listOther = (
                from p in listOtherTemp
                join pMax in allPrice on p.pMAxID equals pMax.ID
                join pMin in allPrice on p.pMinID equals pMin.ID
                select new DistanceMeasurement
                {
                    ID = Guid.NewGuid(),
                    BiggerThanSdID = p.BiggerThanSdID,
                    Rate = Math.Round((p.Price.Close - p.Price.Open) / p.R1000, 10),
                    Macd = Math.Round((p.Price.Close - p.D) / p.R1000, 10),
                    UpGs = Math.Round((pMax.Close - p.Price.Close) / p.R1000, 10),
                    DownGs = Math.Round((p.Price.Close - pMin.Close) / p.R1000, 10),
                    Type = p.Price.Type,
                    Session = sessionType
                }).ToList();

            var jk = dbIndexPrice.FirstOrDefault(x => x.ID == 1694790600010);

            var listOtsher = (
                from p in listOtherTemp
                select new DistanceMeasurement
                {
                    ID = Guid.NewGuid(),
                    BiggerThanSdID = p.BiggerThanSdID,
                    Rate = Math.Round((p.Price.Close - p.Price.Open) / p.R1000, 10),
                    Macd = Math.Round((p.Price.Close - p.D) / p.R1000, 10),
                    Type = p.Price.Type,
                    Session = sessionType
                }).ToList();


            await insertBiggerThanSdOtherSymbols(biggerThanSdOtherSymbols);
            await insertDb(list);
            await insertDb(listOther);
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
            if (insertList.Count > 0)
                await _dataSource.AddAsync(insertList);
        }

        private async Task insertBiggerThanSdOtherSymbols(List<BiggerThanSD> model)
        {
            if (model.Count == 0)
                return;
            int i = 0;
            var insertList = new List<BiggerThanSD>();
            foreach (var item in model)
            {
                i++;
                if (i > 1000)
                {
                    i = 0;
                    await _biggerThanSdDataSource.AddBiggerThanSdOtherSymbolsAsync(insertList);
                    insertList = new List<BiggerThanSD>();
                }
                insertList.Add(item);

            }
            if (insertList.Count > 0)
                await _biggerThanSdDataSource.AddBiggerThanSdOtherSymbolsAsync(insertList);
        }
    }
}
