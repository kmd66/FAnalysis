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
            if (!Init.IsInit)
            {
                await addDbIndex();
                //await addAllIndexs();
                //await timer();
                Init.IsInit = true;
                new Domain.InitSimulateService();
            }
        }
        private async Task addDbIndex()
        {
            var priceMinutelyDataSource = new PriceMinutelyDataSource();
            if (DbIndexPrice.XauUsd != null)
                return;

            var sessions = await new WorkingHoursDataSource().GetSessionsAsync();
            if (!sessions.Success) System.Environment.Exit(500);
            DbIndexPrice.Sessions = sessions.Data.ToList();

            var XauUsd = await priceMinutelyDataSource.ListAsync(new ListVM { PageIndex = 1, PageSize = 1000000000 }, SymbolType.xauusd);
            if (!XauUsd.Success) System.Environment.Exit(500);
            DbIndexPrice.XauUsd = XauUsd.Data.ToList();

            //var RsiXauUsd = await new RsiDataSource().ListAsync(new RsiVM { PageIndex = 1, PageSize = 1000000000, Type = SymbolType.xauusd });
            //if (!RsiXauUsd.Success) System.Environment.Exit(500);
            //DbIndexRsi.XauUsd = RsiXauUsd.Data.ToList();

            var CciXauUsd = await new CciDataSource().ListAsync(new CciVM { PageIndex = 1, PageSize = 1000000000, Type = SymbolType.xauusd });
            if (!CciXauUsd.Success) System.Environment.Exit(500);
            DbIndexCci.XauUsd = CciXauUsd.Data.ToList();

            var MacdXauUsd = await new MacdDataSource().ListAsync(new MacdVM { PageIndex = 1, PageSize = 1000000000, Type = SymbolType.xauusd });
            if (!MacdXauUsd.Success) System.Environment.Exit(500);
            DbIndexMacd.XauUsd = MacdXauUsd.Data.ToList();

            //var IchimokuXauUsd = await new IchimokuDataSource().ListAsync(new IchimokuVM { PageIndex = 1, PageSize = 1000000000, Type = SymbolType.xauusd });
            //if (!IchimokuXauUsd.Success) System.Environment.Exit(500);
            //DbIndexIchimoku.XauUsd = IchimokuXauUsd.Data.ToList();

            //var BollingerBandsXauUsd = await new BollingerBandsDataSource().ListAsync(new BollingerBandsVM { PageIndex = 1, PageSize = 1000000000, Type = SymbolType.xauusd });
            //if (!BollingerBandsXauUsd.Success) System.Environment.Exit(500);
            //DbIndexBollingerBands.XauUsd = BollingerBandsXauUsd.Data.ToList();

        }
    }
}