using Kama.AppCore;
using Kama.FinancialAnalysis.DAL;
using Kama.FinancialAnalysis.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kama.FinancialAnalysis.Domain
{
    public class RsiService
    {
        RsiDataSource dataSource = new RsiDataSource();
        List<PriceMinutely> data;
        public async Task<Result> AddAll(SymbolType symbolType)
        {
            data = DbIndexPrice.GetByType(symbolType);

            List<Rsi> temporaryList = new List<Rsi>();

            foreach (var item in data)
            {
                temporaryList.Add(new Rsi
                {
                    ID = item.ID,
                    Date = item.Date,
                    Value14 = calculateRsi(item, 14),
                    Value32 = calculateRsi(item, 32),
                    Value70 = calculateRsi(item, 70),
                    Type = symbolType
                });
                if (temporaryList.Count > 100)
                {
                    await dataSource.AddListAsync(temporaryList); 
                    temporaryList = new List<Rsi>();
                }
            }

            if (temporaryList.Count > 0)
                await dataSource.AddListAsync(temporaryList);

            return Result.Successful();
        }

        public double calculateRsi(PriceMinutely priceMinutely, int period)
        {
            var list = data.Where(x => x.ID < priceMinutely.ID).Take(period).ToList();
            if (list.Count < period )
                return 50;
            list.Reverse();
            var average = calculateAverageGainAndLoss(list, period);
            var rs = average.AverageGain / average.AverageLoss;
            return 100 - (100 / (1 + rs));

        }

        // محاسبه میانگین قیمت های صعودی و نزولی
        private dynamic calculateAverageGainAndLoss(List<PriceMinutely> list, int period)
        {
            double sumGain = 0;
            double sumLoss = 0;
            for (int i = 1; i < period; i++)
            {
                var priceDifference = list[i].Close - list[i - 1].Close;
                if (priceDifference > 0)
                {
                    sumGain += priceDifference;
                }
                else
                {
                    sumLoss += Math.Abs(priceDifference);
                }
            }
            double averageGain = sumGain / period;
            double averageLoss = sumLoss / period;

            return new { AverageGain= averageGain, AverageLoss= averageLoss  };
        }
    }

}
