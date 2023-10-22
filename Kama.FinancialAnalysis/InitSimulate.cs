using Kama.FinancialAnalysis.DAL;
using Kama.FinancialAnalysis.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace Kama.FinancialAnalysis
{
    public class InitSimulate
    {
        public InitSimulate()
        {
            Start();
        }
        private async void Start()
        {
            await addDbIndex();
            if (!Init.IsInit)
            {
                await addDbIndex();
                //await addAllIndexs();
                //await timer();
                Init.IsInit = true;
            }
            new Domain.InitMLService();
        }
        private async Task addDbIndex()
        {
            var priceMinutelyDataSource = new PriceMinutelyDataSource();
            if (DbIndexPrice.EurJpy != null)
                return;

            var sessions = await new WorkingHoursDataSource().GetSessionsAsync();
            if (!sessions.Success) System.Environment.Exit(500);
            DbIndexPrice.Sessions = sessions.Data.ToList();

            //-----------------------

            var XauUsd = await priceMinutelyDataSource.ListAsync(new ListVM { PageIndex = 1, PageSize = 1000000000 }, SymbolType.xauusd);
            if (!XauUsd.Success) System.Environment.Exit(500);
            DbIndexPrice.XauUsd = XauUsd.Data.ToList();

        }
    }
}