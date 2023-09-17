//using Microsoft.Owin;
//using Microsoft.Owin.Security.OAuth;
//using Owin;
//using System.Web.Http;

//[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(Kama.Aro.Salary.WebApp.Startup), "Starting")]
//[assembly: WebActivatorEx.PostApplicationStartMethod(typeof(Kama.Aro.Salary.WebApp.Startup), "Started")]
//[assembly: WebActivatorEx.ApplicationShutdownMethod(typeof(Kama.Aro.Salary.WebApp.Startup), "Stop")]

//[assembly: OwinStartup(typeof(Kama.Aro.Salary.WebApp.Startup))]

//namespace Kama.Aro.Salary.WebApp
//{
//    public class Startup
//    {
//        private static void RegisterTools(AppCore.IOC.IContainer container)
//        {
           
//        }

//        public static void Starting()
//        {
//        }

//        public static void Started()
//        {

//        }

//        public static void Stop() { }

//        public void Configuration(IAppBuilder app)
//        {
            
//            HttpConfiguration config = new HttpConfiguration();
//            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions { });
//            WebApiConfig.Register(config);
//            RouteConfig.RegisterRoutes(System.Web.Routing.RouteTable.Routes);
//            app.UseWebApi(config);

//            app.MapSignalR();
//        }
//    }
//}