using Kama.FinancialAnalysis.DAL;
using Kama.FinancialAnalysis.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace Kama.FinancialAnalysis
{
    public class InitML
    {
        public InitML()
        {
            Start();
        }
        private async void Start()
        {
            await addDbIndex();
            new Domain.InitMLService();
        }
        private async Task addDbIndex()
        {
            var priceMinutelyDataSource = new PriceMinutelyDataSource();
            if (DbIndex.EurJpy != null)
                return;

            var sessions = await new WorkingHoursDataSource().GetSessionsAsync();
            if (!sessions.Success) System.Environment.Exit(500);
            DbIndex.Sessions = sessions.Data.ToList();

            var XauUsd = await priceMinutelyDataSource.ListAsync(new ListVM { PageIndex = 1, PageSize = 1000000000 }, SymbolType.xauusd);
            if (!XauUsd.Success) System.Environment.Exit(500);
            DbIndex.XauUsd = XauUsd.Data.ToList();

            var UsdChf = await priceMinutelyDataSource.ListAsync(new ListVM { PageIndex = 1, PageSize = 1000000000 }, SymbolType.usdchf);
            if (!UsdChf.Success) System.Environment.Exit(500);
            DbIndex.UsdChf = UsdChf.Data.ToList();

            var EurJpy = await priceMinutelyDataSource.ListAsync(new ListVM { PageIndex = 1, PageSize = 1000000000 }, SymbolType.eurjpy);
            if (!EurJpy.Success) System.Environment.Exit(500);
            DbIndex.EurJpy = EurJpy.Data.ToList();

            //-----------------------

            var Dyx = await priceMinutelyDataSource.ListAsync(new ListVM { PageIndex = 1, PageSize = 1000000000 }, SymbolType.DYX);
            if (!Dyx.Success) System.Environment.Exit(500);
            DbIndex.Dyx = Dyx.Data.ToList();

            var Nq100m = await priceMinutelyDataSource.ListAsync(new ListVM { PageIndex = 1, PageSize = 1000000000 }, SymbolType.nq100m);
            if (!Nq100m.Success) System.Environment.Exit(500);
            DbIndex.Nq100m = Nq100m.Data.ToList();

        }
    }
}