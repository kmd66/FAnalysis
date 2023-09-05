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
            timer();
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
    }
}
