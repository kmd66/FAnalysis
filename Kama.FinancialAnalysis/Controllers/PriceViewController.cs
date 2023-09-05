﻿using Kama.FinancialAnalysis.Domain;
using Kama.FinancialAnalysis.Model;
using Kama.Library.Helper.DynamicReport;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Kama.FinancialAnalysis.Controllers
{
    public class PriceViewController : Controller
    {
        public PriceViewController()
        {
            _service = new PriceViewService();
        }

        PriceViewService _service = new PriceViewService();

        public async Task<ActionResult> Index(int i = 1)
        {
            return View();
        }

        public async Task<ActionResult> OderSymbol(int i = 1)
        {
            return View();
        }

        [HttpPost]
        public async Task<JsonResult> ListView(PriceViewVM model)
        {
            model.PageSize = 1440;
            var result = await _service.ListViewAsync(model);

            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}