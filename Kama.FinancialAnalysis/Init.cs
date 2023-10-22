using Kama.FinancialAnalysis.DAL;
using Kama.FinancialAnalysis.Domain;
using Kama.FinancialAnalysis.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace Kama.FinancialAnalysis
{
    public class Init
    {
        public static bool IsInit = false;
        public Init()
        {
            Start();
        }
        private async void Start()
        {

            //await addDateFromCsvFlie();
            if (!IsInit)
            {
                await addDbIndex();
                //await addAllIndexs();
                //await timer();
                IsInit = true;
            }
        }
        private async Task addDateFromCsvFlie()
        {
            //new DataCollectionHistoryCsv().DoWork(SymbolType.eurusd, "D:\\prj\\forex\\csvFiles\\2019-20230913\\EURUSD.csv");
            //new DataCollectionHistoryCsv().DoWork(SymbolType.xauusd, "D:\\prj\\forex\\csvFiles\\2019-20230913\\XAUUSD.csv");
            //new DataCollectionHistoryCsv().DoWork(SymbolType.usdchf, "D:\\prj\\forex\\csvFiles\\2019-20230913\\USDCHF.csv");
            //new DataCollectionHistoryCsv().DoWork(SymbolType.eurjpy, "D:\\prj\\forex\\csvFiles\\2019-20230913\\EURJPY.csv");
            //new DataCollectionHistoryCsv().DoWork(SymbolType.usdjpy, "D:\\prj\\forex\\csvFiles\\2019-20230913\\USDJPY.csv");
            //new DataCollectionHistoryCsv().DoWork(SymbolType.gbpusd, "D:\\prj\\forex\\csvFiles\\2019-20230913\\GBPUSD.csv");
            //new DataCollectionHistoryCsv().DoWork(SymbolType.usdcad, "D:\\prj\\forex\\csvFiles\\2019-20230913\\USDCAD.csv");
            //new DataCollectionHistoryCsv().DoWork(SymbolType.usdsek, "D:\\prj\\forex\\csvFiles\\2019-20230913\\usdsek.csv");
            //await new DataCollectionHistoryCsv().DoWork(SymbolType.nq100m, "D:\\prj\\forex\\csvFiles\\2019-20230913\\nq100m.csv");
        }

        private async Task addDbIndex()
        {
            var priceMinutelyDataSource = new PriceMinutelyDataSource();
            if (DbIndexPrice.EurJpy != null)
                return;

            var sessions = await new WorkingHoursDataSource().GetSessionsAsync();
            if (!sessions.Success) System.Environment.Exit(500);
            DbIndexPrice.Sessions = sessions.Data.ToList();

            var XauUsd = await priceMinutelyDataSource.ListAsync(new ListVM { PageIndex = 1, PageSize = 10000 }, SymbolType.xauusd);
            if (!XauUsd.Success) System.Environment.Exit(500);
            DbIndexPrice.XauUsd = XauUsd.Data.ToList();


            var UsdChf = await priceMinutelyDataSource.ListAsync(new ListVM { PageIndex = 1, PageSize = 10000 }, SymbolType.usdchf);
            if (!UsdChf.Success) System.Environment.Exit(500);
            DbIndexPrice.UsdChf = UsdChf.Data.ToList();

            var EurJpy = await priceMinutelyDataSource.ListAsync(new ListVM { PageIndex = 1, PageSize = 10000 }, SymbolType.eurjpy);
            if (!EurJpy.Success) System.Environment.Exit(500);
            DbIndexPrice.EurJpy = EurJpy.Data.ToList();

           // -----------------------

            var Dyx = await priceMinutelyDataSource.ListAsync(new ListVM { PageIndex = 1, PageSize = 10000 }, SymbolType.DYX);
            if (!Dyx.Success) System.Environment.Exit(500);
            DbIndexPrice.Dyx = Dyx.Data.ToList();

            var Nq100m = await priceMinutelyDataSource.ListAsync(new ListVM { PageIndex = 1, PageSize = 10000 }, SymbolType.nq100m);
            if (!Nq100m.Success) System.Environment.Exit(500);
            DbIndexPrice.Nq100m = Nq100m.Data.ToList();

        }

        private async Task addAllIndexs()
        {

            //await new RsiService().AddAll(SymbolType.xauusd)
            //await new CciService().AddAll(SymbolType.xauusd);
            //await new IchimokuService().AddAll(SymbolType.xauusd);
            //await new BollingerBandsService().AddAll(SymbolType.xauusd);
            //await new MacdService().AddAll(SymbolType.xauusd);

            new MovingAverageService().AddReng(DbIndexPrice.XauUsd);
            new MovingAverageService().AddReng(DbIndexPrice.UsdChf);
            new MovingAverageService().AddReng(DbIndexPrice.EurJpy);
            new MovingAverageService().AddReng(DbIndexPrice.Nq100m);
            await new MovingAverageService().AddReng(DbIndexPrice.Dyx);

            new StandardDeviationService().AddReng(DbIndexPrice.XauUsd);
            new StandardDeviationService().AddReng(DbIndexPrice.UsdChf);
            new StandardDeviationService().AddReng(DbIndexPrice.EurJpy);
            new StandardDeviationService().AddReng(DbIndexPrice.Nq100m);
            await new StandardDeviationService().AddReng(DbIndexPrice.Dyx);
        }

        private async Task timer()
        {

            var priceMinutelyDataSource = new PriceMinutelyDataSource();
            if (IsInit)
                return;

            await new DataCollectionHistory().DoWork(SymbolType.eurjpy);
            await new DataCollectionHistory().DoWork(SymbolType.usdchf);
            await new DataCollectionHistory().DoWork(SymbolType.xauusd);
            await new DataCollectionHistory().DoWork(SymbolType.nq100m);

            await new DataCollectionDaily(SymbolType.eurjpy, "1500").DoWork();
            await new DataCollectionDaily(SymbolType.usdchf, "1500").DoWork();
            await new DataCollectionDaily(SymbolType.xauusd, "1500").DoWork();


            new DataCollectionMinutely().Start();

            //new AddWorkingHoursTimer().Start();
        }
    }
}