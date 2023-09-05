using Kama.FinancialAnalysis.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Kama.FinancialAnalysis.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {

            //var s = "[1692023520, 1.09391, 1.09391, 1.0937000000000001, 1.0937000000000001, 53, 0, 0]";
            //var t = PriceMinutely.InstanceFromJson(s);

            //var s1 = "[[1692023520, 1.09391, 1.09391, 1.0937000000000001, 1.0937000000000001, 53, 0, 0], [1692023580, 1.0937000000000001, 1.09378, 1.09368, 1.09378, 32, 0, 0], [1692023640, 1.09378, 1.0939, 1.09377, 1.09381, 33, 0, 0], [1692023700, 1.09382, 1.09389, 1.09374, 1.09387, 62, 0, 0], [1692023760, 1.09387, 1.0939, 1.09375, 1.09381, 55, 0, 0], [1692023820, 1.09381, 1.09399, 1.09381, 1.09396, 34, 0, 0], [1692023880, 1.09396, 1.0941, 1.0939, 1.09391, 58, 0, 0], [1692023940, 1.09391, 1.09391, 1.0937999999999999, 1.09382, 43, 0, 0], [1692024000, 1.09381, 1.09393, 1.09381, 1.09388, 63, 0, 0], [1692024060, 1.09388, 1.09391, 1.09381, 1.0939, 41, 0, 0]]";
            //var t1 = PriceMinutely.ListFromJson(s1);

            //var j = new Dependency.ObjectSerializer().Serialize(t1);

            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}