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
            // await addAllIndexs();
            await timer();

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

        }
        private async Task addAllIndexs()
        {

            await new MovingAverageService().AddReng(DbIndex.EeurUsd);
            await new MovingAverageService().AddReng(DbIndex.XauUsd);
            await new MovingAverageService().AddReng(DbIndex.UsdChf);
            await new MovingAverageService().AddReng(DbIndex.EurJpy);

            await new StandardDeviationService().AddReng(DbIndex.EeurUsd);
            await new StandardDeviationService().AddReng(DbIndex.XauUsd);
            await new StandardDeviationService().AddReng(DbIndex.UsdChf);
            await new StandardDeviationService().AddReng(DbIndex.EurJpy);

            await new PriceMinutelyIndexService().AddAllDyx();
        }

        private async Task timer()
        {

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

            await new PriceMinutelyIndexService().AddAllDyx();

            new DataCollectionMinutely(SymbolType.eurusd).Start();
            new DataCollectionMinutely(SymbolType.eurjpy).Start();
            new DataCollectionMinutely(SymbolType.usdchf).Start();
            new DataCollectionMinutely(SymbolType.xauusd).Start();

            //new AddWorkingHoursTimer().Start();
        }
    }
}