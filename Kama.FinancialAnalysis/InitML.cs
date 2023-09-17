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
            if (DbIndex.EurUsd != null)
                return;

            var sessions = await new WorkingHoursDataSource().GetSessionsAsync();
            if (!sessions.Success) System.Environment.Exit(500);
            DbIndex.Sessions = sessions.Data.ToList();

            var EurUsd = await priceMinutelyDataSource.ListAsync(new ListVM { PageIndex = 1, PageSize = 1000000000 }, SymbolType.eurusd);
            if (!EurUsd.Success) System.Environment.Exit(500);
            DbIndex.EurUsd = EurUsd.Data.ToList();

            //var XauUsd = await priceMinutelyDataSource.ListAsync(new ListVM { PageIndex = 1, PageSize = 1000000000 }, SymbolType.xauusd);
            //if (!XauUsd.Success) System.Environment.Exit(500);
            //DbIndex.XauUsd = XauUsd.Data.ToList();

            //var UsdChf = await priceMinutelyDataSource.ListAsync(new ListVM { PageIndex = 1, PageSize = 1000000000 }, SymbolType.usdchf);
            //if (!UsdChf.Success) System.Environment.Exit(500);
            //DbIndex.UsdChf = UsdChf.Data.ToList();

            //var EurJpy = await priceMinutelyDataSource.ListAsync(new ListVM { PageIndex = 1, PageSize = 1000000000 }, SymbolType.eurjpy);
            //if (!EurJpy.Success) System.Environment.Exit(500);
            //DbIndex.EurJpy = EurJpy.Data.ToList();

            //-----------------------

            //var UsdJpy = await priceMinutelyDataSource.ListAsync(new ListVM { PageIndex = 1, PageSize = 1000000000 }, SymbolType.usdjpy);
            //if (!UsdJpy.Success) System.Environment.Exit(500);
            //DbIndex.UsdJpy = UsdJpy.Data.ToList();

            //var GbpUsd = await priceMinutelyDataSource.ListAsync(new ListVM { PageIndex = 1, PageSize = 1000000000 }, SymbolType.gbpusd);
            //if (!GbpUsd.Success) System.Environment.Exit(500);
            //DbIndex.GbpUsd = GbpUsd.Data.ToList();

            //var UsdCad = await priceMinutelyDataSource.ListAsync(new ListVM { PageIndex = 1, PageSize = 1000000000 }, SymbolType.usdcad);
            //if (!UsdCad.Success) System.Environment.Exit(500);
            //DbIndex.UsdCad = UsdCad.Data.ToList();

            //var UsdSek = await priceMinutelyDataSource.ListAsync(new ListVM { PageIndex = 1, PageSize = 1000000000 }, SymbolType.usdsek);
            //if (!UsdSek.Success) System.Environment.Exit(500);
            //DbIndex.UsdSek = UsdSek.Data.ToList();

            ////-----------------------

            //var Dyx = await priceMinutelyDataSource.ListAsync(new ListVM { PageIndex = 1, PageSize = 1000000000 }, SymbolType.DYX);
            //if (!Dyx.Success) System.Environment.Exit(500);
            //DbIndex.Dyx = Dyx.Data.ToList();

            //var Nq100m = await priceMinutelyDataSource.ListAsync(new ListVM { PageIndex = 1, PageSize = 1000000000 }, SymbolType.nq100m);
            //if (!Nq100m.Success) System.Environment.Exit(500);
            //DbIndex.Nq100m = Nq100m.Data.ToList();

        }
    }
}