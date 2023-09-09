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
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            //---------------------

            AppProperty.SetInstance(System.Configuration.ConfigurationManager.AppSettings["AppPropertyPath"]);



            //timer();

            //addDateFromCsvFli();

        }
        protected void timer()
        {

            new DataCollectionHistory().DoWork(SymbolType.eurusd);
            new DataCollectionHistory().DoWork(SymbolType.eurjpy);
            new DataCollectionHistory().DoWork(SymbolType.usdchf);
            new DataCollectionHistory().DoWork(SymbolType.xauusd);

            new DataCollectionDaily(SymbolType.eurusd, "1500").DoWork();
            new DataCollectionDaily(SymbolType.eurjpy, "1500").DoWork();
            new DataCollectionDaily(SymbolType.usdchf, "1500").DoWork();
            new DataCollectionDaily(SymbolType.xauusd, "1500").DoWork();

            new DataCollectionMinutely(SymbolType.eurusd).Start();
            new DataCollectionMinutely(SymbolType.eurjpy).Start();
            new DataCollectionMinutely(SymbolType.usdchf).Start();
            new DataCollectionMinutely(SymbolType.xauusd).Start();
      
            new AddWorkingHoursTimer().Start();
        }
        protected async void addDateFromCsvFli()
        {
            await new DataCollectionHistoryCsv().DoWork(SymbolType.eurusd, "D:\\prj\\forex\\csvFiles\\1_2019_2023-9-5_EurUsd.csv");
            new DataCollectionHistoryCsv().DoWork(SymbolType.xauusd, "D:\\prj\\forex\\csvFiles\\2_2019_2023-9-5_XauUsd.csv");
            await new DataCollectionHistoryCsv().DoWork(SymbolType.usdchf, "D:\\prj\\forex\\csvFiles\\3_2019_2021-12-11_usdchf.csv");
            await new DataCollectionHistoryCsv().DoWork(SymbolType.eurjpy, "D:\\prj\\forex\\csvFiles\\4_2019_2022-4-28_eurjpy.csv");

            await new DataCollectionHistoryCsv().DoWork(SymbolType.usdjpy, "D:\\prj\\forex\\csvFiles\\5_2019_2023-9-5_usdjpy.csv");
            await new DataCollectionHistoryCsv().DoWork(SymbolType.gbpusd, "D:\\prj\\forex\\csvFiles\\6_2019_2023-9-5_gbpusd.csv");
            await new DataCollectionHistoryCsv().DoWork(SymbolType.usdcad, "D:\\prj\\forex\\csvFiles\\7_2019_2023-9-5_usdcad.csv");
            await new DataCollectionHistoryCsv().DoWork(SymbolType.usdsek, "D:\\prj\\forex\\csvFiles\\8_2019_2023-9-5_usdsek.csv");
        }
    }
}
