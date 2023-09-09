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
    }
}
