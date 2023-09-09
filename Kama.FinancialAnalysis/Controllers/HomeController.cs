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
            await addDbIndex();
            ViewBag.Message = "اطلاعات دریافت شد";

            return View();
        }

        private async Task addDbIndex()
        {
            var priceMinutelyDataSource = new PriceMinutelyDataSource();
            if (DbIndex.EeurUsd != null)
                return;

            var EeurUsd = await priceMinutelyDataSource.ListAsync(new ListVM { PageIndex = 0, PageSize = 0 }, SymbolType.eurusd);
            if (!EeurUsd.Success) System.Environment.Exit(500);
            DbIndex.EeurUsd = EeurUsd.Data.ToList();

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


          //await  new PriceMinutelyIndexService().AddAllDxy();
        }
    }
}