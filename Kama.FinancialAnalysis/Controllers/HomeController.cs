using Kama.FinancialAnalysis.DAL;
using Kama.FinancialAnalysis.Domain;
using Kama.FinancialAnalysis.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Kama.FinancialAnalysis.Controllers
{
    public class HomeController : Controller
    {
        public async Task<ActionResult> Index()
        {

            ViewBag.Message = "اطلاعات دریافت شد";

            while(true) {

                await Task.Delay(1000);
                if (MvcApplication.IsInit)
                    break;
            }

            return View();
        }

    }
}