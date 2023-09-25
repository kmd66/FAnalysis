using DeviceDetectorNET.Class.Device;
using Kama.AppCore;
using Kama.FinancialAnalysis.DAL;
using Kama.FinancialAnalysis.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.UI;

namespace Kama.FinancialAnalysis.Domain
{
    public class BiggerThanSDHelper
    {
        BiggerThanSdDataSource _dataSource = new BiggerThanSdDataSource();
        public BiggerThanSDHelper()
        {
        }
        public async Task Start()
        {
            Start(SymbolType.DYX);
            Start(SymbolType.nq100m);
            Start(SymbolType.xauusd);
            Start(SymbolType.usdchf);
            await Start(SymbolType.eurjpy);

        }
        private async Task Start(SymbolType symbolType)
        {
            var londonResult = await _dataSource.GetBiggerThanSDsAsync(
                new BiggerThanSDVM { Session = SessionType.london, Type = symbolType }
            );
            await insertDb(londonResult.Data.ToList());

            var newYorkResult = await _dataSource.GetBiggerThanSDsAsync(
                new BiggerThanSDVM { Session = SessionType.newYork, Type = symbolType }
            );
            await insertDb(newYorkResult.Data.ToList());

            var sydneyResult = await _dataSource.GetBiggerThanSDsAsync(
                new BiggerThanSDVM { Session = SessionType.sydney, Type = symbolType }
            );
            await insertDb(sydneyResult.Data.ToList());

        }
        private async Task insertDb(List<BiggerThanSD> model)
        {
            if (model.Count == 0)
                return;
            int i = 0;
            var insertList = new List<BiggerThanSD>();
            var list = DbIndexPrice.GetByType(model[0].Type);
            list.Reverse();
            foreach (var item in model)
            {
                i++;
                if (i > 1000)
                {
                    i = 0;
                    await _dataSource.AddBiggerThanSDsAsync(insertList);
                    insertList = new List<BiggerThanSD>();
                }
                var maxMin = MinMAax(list, item.PriceID, item.Type);
                item.ID = Guid.NewGuid();
                item.MaxPriceID = maxMin[0];
                item.MinPriceID = maxMin[1];
                insertList.Add(item);
            }
            if(insertList.Count > 0)
                await _dataSource.AddBiggerThanSDsAsync(insertList); 
            list.Reverse();

        }
        public static long[] MinMAax(List<PriceMinutely>list, long priceID, SymbolType type)
        {
            var l = list.Where(x => x.ID > priceID && x.Type == type).Take(240).ToList();
            if (l.Count == 0)
                return new long[2] { 0, 0 };

            var itemMax = l.First(x => x.Close == l.Max(y => y.Close));
            var itemMin = l.First(x => x.Close == l.Min(y => y.Close));
            return new long[2] { itemMax.ID, itemMin.ID };
        }
    }
}
