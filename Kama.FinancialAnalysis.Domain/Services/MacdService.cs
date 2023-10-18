using Kama.AppCore;
using Kama.FinancialAnalysis.DAL;
using Kama.FinancialAnalysis.Model;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Database;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Math;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Kama.FinancialAnalysis.Domain
{
    public class MacdService
    {
        MacdDataSource dataSource = new MacdDataSource();
        
        public async Task<Result> AddAll(SymbolType symbolType)
        {
            var data = DbIndexPrice.GetByType(symbolType);
            var list = data.OrderBy(x => x.ID).ToList();

            List<Macd> temporaryList = new List<Macd>();

            var ema12 =  calculateAllEMA(list, 12);
            var ema26 =  calculateAllEMA(list, 26);
            var ema60 =  calculateAllEMA(list, 60);
            var ema130 = calculateAllEMA(list, 130);
            var ema120 = calculateAllEMA(list, 120);
            var ema260 = calculateAllEMA(list, 260);


            for (var i = 0; i < list.Count; i++)
            {
                temporaryList.Add(new Macd { 
                    ID= list[i].ID,
                    Date = list[i].Date,
                    E12 = ema12[i],
                    E26 = ema26[i],
                    E60 = ema60[i],
                    E130 = ema130[i],
                    E120 = ema120[i],
                    E260 = ema260[i],
                    Type = symbolType
                });

                if (temporaryList.Count > 100)
                {
                    await dataSource.AddListAsync(temporaryList);
                    temporaryList = new List<Macd>();
                }
            }

            if (temporaryList.Count > 0)
                await dataSource.AddListAsync(temporaryList);

            return Result.Successful();
        }
        public List<double> calculateAllEMA(List<PriceMinutely> list, int period)
        {
            List<double> ema = new List<double>();
            double multiplier = 2.0 / (double)(period + 1);

            double sum = 0;
            for (var i = 0; i < period; i++)
            {
                sum += list[i].Close;
            }

            for (var i = 0; i < list.Count; i++)
            {
                if (i < period - 1)
                    ema.Add(list[i].Close);
                else if (i == period - 1)
                    ema.Add(sum / period);
                else
                    ema.Add((list[i].Close - ema[i - 1]) * multiplier + ema[i - 1]);
            }
            return ema;
        }
    }

}
