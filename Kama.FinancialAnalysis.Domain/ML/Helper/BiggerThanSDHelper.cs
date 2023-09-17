﻿using DeviceDetectorNET.Class.Device;
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
            var eurudLondonResult = await _dataSource.GetBiggerThanSDsAsync(
                new BiggerThanSDVM { Session= SessionType.london, Type = SymbolType.eurusd }
            );
            await insertDb(eurudLondonResult.Data.ToList());

            var eurudNewYorkResult = await _dataSource.GetBiggerThanSDsAsync(
                new BiggerThanSDVM { Session = SessionType.newYork, Type = SymbolType.eurusd }
            );
            await insertDb(eurudNewYorkResult.Data.ToList());

        }
        private async Task insertDb(List<BiggerThanSD> model)
        {
            int i = 0;
            var insertList = new List<BiggerThanSD>();
            var list = DbIndex.GetByType(model[0].Type);
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
                var maxMin = MinMAax(list, item.PriceID);
                item.ID = Guid.NewGuid();
                item.MaxPriceID = maxMin[0];
                item.MinPriceID = maxMin[1];
                insertList.Add(item);
            }
            if(insertList.Count > 0)
                await _dataSource.AddBiggerThanSDsAsync(insertList); 
            list.Reverse();

        }
        private long[] MinMAax(List<PriceMinutely>list, long priceID)
        {
            var l = list.Where(x => x.ID > priceID).Take(240).ToList();
            if (l.Count == 0)
                return new long[2] { 0, 0 };

            var itemMax = l.First(x => x.Close == l.Max(y => y.Close));
            var itemMin = l.First(x => x.Close == l.Min(y => y.Close));
            return new long[2] { itemMax.ID, itemMin.ID };
        }
    }
}
