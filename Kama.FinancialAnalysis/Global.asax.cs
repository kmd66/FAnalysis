using Kama.FinancialAnalysis.DAL;
using Kama.FinancialAnalysis.Domain;
using Kama.FinancialAnalysis.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace Kama.FinancialAnalysis
{
    public class MvcApplication : System.Web.HttpApplication
    {
        public static bool IsInit= false;
        protected async void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            //---------------------

            AppProperty.SetInstance(System.Configuration.ConfigurationManager.AppSettings["AppPropertyPath"]);

            if (!IsInit)
            {
                await addDbIndex();
                //await addAllIndexs();
                await timer();
                IsInit = true;
            }
            //addDateFromCsvFli();

        }
        protected async void addDateFromCsvFlie()
        {
            //await new DataCollectionHistoryCsv().DoWork(SymbolType.eurusd, "D:\\prj\\forex\\csvFiles\\1_2019_2023-9-5_EurUsd.csv");
            //await new DataCollectionHistoryCsv().DoWork(SymbolType.xauusd, "D:\\prj\\forex\\csvFiles\\2_2019_2023-9-5_XauUsd.csv");
            //await new DataCollectionHistoryCsv().DoWork(SymbolType.usdchf, "D:\\prj\\forex\\csvFiles\\usdchf.csv");
            //await new DataCollectionHistoryCsv().DoWork(SymbolType.eurjpy, "D:\\prj\\forex\\csvFiles\\eurjpy.csv");

            //await new DataCollectionHistoryCsv().DoWork(SymbolType.usdjpy, "D:\\prj\\forex\\csvFiles\\5_2019_2023-9-5_usdjpy.csv");
            //await new DataCollectionHistoryCsv().DoWork(SymbolType.gbpusd, "D:\\prj\\forex\\csvFiles\\6_2019_2023-9-5_gbpusd.csv");
            //await new DataCollectionHistoryCsv().DoWork(SymbolType.usdcad, "D:\\prj\\forex\\csvFiles\\7_2019_2023-9-5_usdcad.csv");
            //await new DataCollectionHistoryCsv().DoWork(SymbolType.usdsek, "D:\\prj\\forex\\csvFiles\\8_2019_2023-9-5_usdsek.csv");
        }

        private async Task addDbIndex()
        {
            var priceMinutelyDataSource = new PriceMinutelyDataSource();
            if (DbIndex.EurUsd != null)
                return;

            var EurUsd = await priceMinutelyDataSource.ListAsync(new ListVM { PageIndex = 0, PageSize = 0 }, SymbolType.eurusd);
            if (!EurUsd.Success) System.Environment.Exit(500);
            DbIndex.EurUsd = EurUsd.Data.ToList();

            var XauUsd = await priceMinutelyDataSource.ListAsync(new ListVM { PageIndex = 0, PageSize = 0 }, SymbolType.xauusd);
            if (!XauUsd.Success) System.Environment.Exit(500);
            DbIndex.XauUsd = XauUsd.Data.ToList();

            var UsdChf = await priceMinutelyDataSource.ListAsync(new ListVM { PageIndex = 0, PageSize = 0 }, SymbolType.usdchf);
            if (!UsdChf.Success) System.Environment.Exit(500);
            DbIndex.UsdChf = UsdChf.Data.ToList();

            var EurJpy = await priceMinutelyDataSource.ListAsync(new ListVM { PageIndex = 0, PageSize = 0 }, SymbolType.eurjpy);
            if (!EurJpy.Success) System.Environment.Exit(500);
            DbIndex.EurJpy = EurJpy.Data.ToList();

            //-----------------------

            var UsdJpy = await priceMinutelyDataSource.ListAsync(new ListVM { PageIndex = 0, PageSize = 0 }, SymbolType.usdjpy);
            if (!UsdJpy.Success) System.Environment.Exit(500);
            DbIndex.UsdJpy = UsdJpy.Data.ToList();

            var GbpUsd = await priceMinutelyDataSource.ListAsync(new ListVM { PageIndex = 0, PageSize = 0 }, SymbolType.gbpusd);
            if (!GbpUsd.Success) System.Environment.Exit(500);
            DbIndex.GbpUsd = GbpUsd.Data.ToList();

            var UsdCad = await priceMinutelyDataSource.ListAsync(new ListVM { PageIndex = 0, PageSize = 0 }, SymbolType.usdcad);
            if (!UsdCad.Success) System.Environment.Exit(500);
            DbIndex.UsdCad = UsdCad.Data.ToList();

            var UsdSek = await priceMinutelyDataSource.ListAsync(new ListVM { PageIndex = 0, PageSize = 0 }, SymbolType.usdsek);
            if (!UsdSek.Success) System.Environment.Exit(500);
            DbIndex.UsdSek = UsdSek.Data.ToList();

            //-----------------------

            var Dyx = await priceMinutelyDataSource.ListAsync(new ListVM { PageIndex = 0, PageSize = 0 }, SymbolType.DYX);
            if (!Dyx.Success) System.Environment.Exit(500);
            DbIndex.Dyx = Dyx.Data.ToList();

            var Nd100m = await priceMinutelyDataSource.ListAsync(new ListVM { PageIndex = 0, PageSize = 0 }, SymbolType.nd100m);
            if (!Nd100m.Success) System.Environment.Exit(500);
            DbIndex.Nd100m = Nd100m.Data.ToList();

        }
        private async Task addAllIndexs()
        {

            await new MovingAverageService().AddReng(DbIndex.EurUsd);
            await new MovingAverageService().AddReng(DbIndex.XauUsd);
            await new MovingAverageService().AddReng(DbIndex.UsdChf);
            await new MovingAverageService().AddReng(DbIndex.EurJpy);

            await new StandardDeviationService().AddReng(DbIndex.EurUsd);
            await new StandardDeviationService().AddReng(DbIndex.XauUsd);
            await new StandardDeviationService().AddReng(DbIndex.UsdChf);
            await new StandardDeviationService().AddReng(DbIndex.EurJpy);

            await new PriceMinutelyService().AddAllDyx();
        }

        private async Task timer()
        {

            var priceMinutelyDataSource = new PriceMinutelyDataSource();
            if (DbIndex.EurUsd != null)
                return;

            await new DataCollectionHistory().DoWork(SymbolType.eurusd);
            await new DataCollectionHistory().DoWork(SymbolType.eurjpy);
            await new DataCollectionHistory().DoWork(SymbolType.usdchf);
            await new DataCollectionHistory().DoWork(SymbolType.xauusd);

            await new DataCollectionHistory().DoWork(SymbolType.usdjpy);
            await new DataCollectionHistory().DoWork(SymbolType.gbpusd);
            await new DataCollectionHistory().DoWork(SymbolType.usdcad);
            await new DataCollectionHistory().DoWork(SymbolType.usdsek);

            await new DataCollectionDaily(SymbolType.eurusd, "1500").DoWork();
            await new DataCollectionDaily(SymbolType.eurjpy, "1500").DoWork();
            await new DataCollectionDaily(SymbolType.usdchf, "1500").DoWork();
            await new DataCollectionDaily(SymbolType.xauusd, "1500").DoWork();

            await new DataCollectionDaily(SymbolType.usdjpy, "1500").DoWork();
            await new DataCollectionDaily(SymbolType.gbpusd, "1500").DoWork();
            await new DataCollectionDaily(SymbolType.usdcad, "1500").DoWork();
            await new DataCollectionDaily(SymbolType.usdsek, "1500").DoWork();

            await new PriceMinutelyService().AddAllDyx();

            new DataCollectionMinutely().Start();

            //new AddWorkingHoursTimer().Start();
        }
    }
}
