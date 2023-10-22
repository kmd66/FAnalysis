using Kama.FinancialAnalysis.DAL;
using Kama.FinancialAnalysis.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace Kama.FinancialAnalysis
{
    public class InitML
    {
        public InitML()
        {
            Start();
        }
        private async void Start()
        {
            await addDbIndex();
            if (!Init.IsInit)
            {
                await addDbIndex();
                //await addAllIndexs();
                //await timer();
                Init.IsInit = true;
            }
            new Domain.InitMLService();
        }
        private async Task addDbIndex()
        {
            var priceMinutelyDataSource = new PriceMinutelyDataSource();
            if (DbIndexPrice.EurJpy != null)
                return;

            var sessions = await new WorkingHoursDataSource().GetSessionsAsync();
            if (!sessions.Success) System.Environment.Exit(500);
            DbIndexPrice.Sessions = sessions.Data.ToList();

            //-----------------------

            var XauUsd = await priceMinutelyDataSource.ListAsync(new ListVM { PageIndex = 1, PageSize = 1000000000 }, SymbolType.xauusd);
            if (!XauUsd.Success) System.Environment.Exit(500);
            DbIndexPrice.XauUsd = XauUsd.Data.ToList();

            //var UsdChf = await priceMinutelyDataSource.ListAsync(new ListVM { PageIndex = 1, PageSize = 1000000000 }, SymbolType.usdchf);
            //if (!UsdChf.Success) System.Environment.Exit(500);
            //DbIndexPrice.UsdChf = UsdChf.Data.ToList();

            //var EurJpy = await priceMinutelyDataSource.ListAsync(new ListVM { PageIndex = 1, PageSize = 1000000000 }, SymbolType.eurjpy);
            //if (!EurJpy.Success) System.Environment.Exit(500);
            //DbIndexPrice.EurJpy = EurJpy.Data.ToList();

            ////-----------------------

            //var Dyx = await priceMinutelyDataSource.ListAsync(new ListVM { PageIndex = 1, PageSize = 1000000000 }, SymbolType.DYX);
            //if (!Dyx.Success) System.Environment.Exit(500);
            //DbIndexPrice.Dyx = Dyx.Data.ToList();

            //var Nq100m = await priceMinutelyDataSource.ListAsync(new ListVM { PageIndex = 1, PageSize = 1000000000 }, SymbolType.nq100m);
            //if (!Nq100m.Success) System.Environment.Exit(500);
            //DbIndexPrice.Nq100m = Nq100m.Data.ToList();

            ////-----------------------

            ////BiggerThanSdDataSource biggerThanSdDataSource = new BiggerThanSdDataSource();
            ////var biggerThanSDs = await biggerThanSdDataSource.ListAsync(new BiggerThanSDVM());
            ////if (!biggerThanSDs.Success) System.Environment.Exit(500);
            ////DbIndexBiggerThanSD.Index = biggerThanSDs.Data.ToList();

            //MovingAverageDataSource movingAverageDataSource = new MovingAverageDataSource();
            //var movingAverage = await movingAverageDataSource.ListAsync(SymbolType.Unknown);
            //if (!movingAverage.Success) System.Environment.Exit(500);
            //DbIndexMovingAverage.Index = movingAverage.Data.ToList();

            //StandardDeviationDataSource standardDeviationDataSource = new StandardDeviationDataSource();
            //var standardDeviation = await standardDeviationDataSource.ListAsync(SymbolType.Unknown);
            //if (!standardDeviation.Success) System.Environment.Exit(500);
            //DbIndexStandardDeviation.Index = standardDeviation.Data.ToList();

        }
    }
}