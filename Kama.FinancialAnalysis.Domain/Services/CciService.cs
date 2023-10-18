using Kama.AppCore;
using Kama.FinancialAnalysis.DAL;
using Kama.FinancialAnalysis.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kama.FinancialAnalysis.Domain
{
    public class CciService
    {
        CciDataSource dataSource = new CciDataSource();
        List<PriceMinutely> data;
        public async Task<Result> AddAll(SymbolType symbolType)
        {
            data = DbIndexPrice.GetByType(symbolType);

            List<Cci> temporaryList = new List<Cci>();

            foreach (var item in data)
            {
                temporaryList.Add(new Cci
                {
                    ID = item.ID,
                    Date = item.Date,
                    Value14 = calculateCci(item, 14),
                    Value32 = calculateCci(item, 32),
                    Value70 = calculateCci(item, 70),
                    Type = symbolType
                });
                if (temporaryList.Count > 100)
                {
                    await dataSource.AddListAsync(temporaryList); 
                    temporaryList = new List<Cci>();
                }
            }

            if (temporaryList.Count > 0)
                await dataSource.AddListAsync(temporaryList);

            return Result.Successful();
        }

        public double calculateCci(PriceMinutely priceMinutely, int period)
        {
            var list = data.Where(x => x.ID < priceMinutely.ID).Take(period).ToList();
            if (list.Count < period )
                return 50;
            list.Reverse();

            var typicalPrice = (priceMinutely.Max + priceMinutely.Min + priceMinutely.Close) / 3;
            var sma = calculateSMA(list, period);
            var meanDeviation = calculateMeanDeviation(list, period, sma);
            var t = (typicalPrice - sma) / (0.015 * meanDeviation);
            return (typicalPrice - sma) / (0.015 * meanDeviation);

        }

        // محاسبه میانگین ساده قیمت باز، بالا و پایانی
        private double calculateSMA(List<PriceMinutely> list, int period)
        {
            double sum = 0;
            for (int i = 0; i < period; i++)
            {
                sum += (list[i].Max + list[i].Min + list[i].Close) / 3;
            }
            return sum / period;
        }

        // محاسبه انحراف معیار
        private double calculateMeanDeviation(List<PriceMinutely> list, int period, double sma)
        {
            double sum = 0;
            for (int i = 0; i < period; i++)
            {
                sum += Math.Abs((list[i].Max + list[i].Min + list[i].Close) / 3 - sma);
            }
            return sum / period;
        }

    }

}
