using Kama.AppCore;
using Kama.FinancialAnalysis.DAL;
using Kama.FinancialAnalysis.Model;
using Kama.Library.Helper.DynamicReport;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Database;
using OfficeOpenXml.FormulaParsing.Excel.Functions.DateTime;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Math;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kama.FinancialAnalysis.Domain
{
    public class BollingerBandsService
    {
        BollingerBandsDataSource dataSource = new BollingerBandsDataSource();
        double stdDeviation = 2;

        public async Task<Result> AddAll(SymbolType symbolType)
        {
            var data = DbIndexPrice.GetByType(symbolType);

            List<BollingerBands> temporaryList = new List<BollingerBands>();

            foreach (var item in data)
            {
                var average20 = calculateMA(data, item, 20);
                var average30 = calculateMA(data, item, 30);
                var average40 = calculateMA(data, item, 40);
                var bollingerBands = BollingerBands.InstanceFromPriceMinutely(item, symbolType);
               
                if (average40 != 0)
                {
                    var stdDev20 = calculateSdv(data, item, 20, average20);
                    var stdDev30 = calculateSdv(data, item, 30, average30);
                    var stdDev40 = calculateSdv(data, item, 40, average40);

                    bollingerBands.A20 = average20;
                    bollingerBands.U20 = average20 + stdDeviation * stdDev20;
                    bollingerBands.L20 = average20 - stdDeviation * stdDev20;
                    bollingerBands.A30 = average30;
                    bollingerBands.U30 = average30 + stdDeviation * stdDev30;
                    bollingerBands.L30 = average30 - stdDeviation * stdDev30;
                    bollingerBands.A40 = average40;
                    bollingerBands.U40 = average40 + stdDeviation * stdDev40;
                    bollingerBands.L40 = average40 - stdDeviation * stdDev40;
                }
                
                temporaryList.Add(bollingerBands);

                if (temporaryList.Count > 100)
                {
                    await dataSource.AddListAsync(temporaryList);
                    temporaryList = new List<BollingerBands>();
                }
            }

            if (temporaryList.Count > 0)
                await dataSource.AddListAsync(temporaryList);

            return Result.Successful();
        }

        private double calculateMA(List<PriceMinutely> data, PriceMinutely item, int period)
        {
            var l = data.Where(x => x.ID < item.ID).Take(period).ToList();
            if (l.Count < period)
                return 0;

            return l.Sum(x => x.Close) / period;
        }

        private double calculateSdv(List<PriceMinutely> data, PriceMinutely item, int period, double average)
        {
            var l = data.Where(x => x.ID < item.ID).Take(period).ToList();
            if (l.Count < period)
                return item.Close;

            double sumSquaredDiff = 0;
            foreach (var k in l)
            {
                var diff = k.Close - average;
                sumSquaredDiff += diff * diff;

            }
            return Math.Sqrt(sumSquaredDiff / period);
        }
    }

}
