using DeviceDetectorNET.Results;
using Kama.AppCore;
using Kama.AppCore.IOC;
using Kama.FinancialAnalysis.DAL;
using Kama.FinancialAnalysis.Model;
using OfficeOpenXml.FormulaParsing.Excel.Functions.DateTime;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Information;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Logical;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kama.FinancialAnalysis.Domain
{
    public class PriceMinutelyIndexService
    {
        public async Task<Result> AddListAsync(List<PriceMinutely> model, SymbolType type)
        {
            var dataSource = await new DAL.PriceMinutelyDataSource().AddListAsync(model, type);
            if (dataSource.Success)
            {
                var dbList = DbIndex.GetByType(type);
                dbList.AddRange(model);
                dbList = dbList.GroupBy(p => p.ID).Select(grp => grp.First()).OrderByDescending(x => x.ID).ToList();
                if (type == SymbolType.eurusd || type == SymbolType.xauusd ||
                    type == SymbolType.usdchf || type == SymbolType.eurjpy)
                {
                    await new MovingAverageService().AddNullReng(model);
                    await new StandardDeviationService().AddNullReng(model);
                }
            }


            return dataSource;
        }

        #region Dyx
        public async Task<Result> AddAllDyx()
        {
            List<PriceMinutely> temporaryList = new List<PriceMinutely>();
            int i = 0;

            var addList = (from p in DbIndex.EeurUsd
                           join pl in DbIndex.Dyx
                             on p.ID equals pl.ID into pp
                           from pl in pp.DefaultIfEmpty()
                           where pl == null
                           select p).ToList();

            return await AddRengDyx(addList);
        }

        public async Task<Result> AddRengDyx(List<PriceMinutely> l)
        {
            var dataSource = new DAL.PriceMinutelyDataSource();
            List<PriceMinutely> temporaryList = new List<PriceMinutely>();
            int i = 0;

            var addList = (from p in l
                           join pl in DbIndex.Dyx
                             on p.ID equals pl.ID into pp
                           from pl in pp.DefaultIfEmpty()
                           where pl == null
                           select p).ToList();

            foreach (var eurUsd in addList)
            {
                i++;
                if (i > 1000)
                {
                    await dataSource.AddListAsync(temporaryList, SymbolType.DYX);
                    i = 0;
                    temporaryList = new List<PriceMinutely>();
                }
                temporaryList.Add(ComputingDyx(eurUsd));
            }

            if (temporaryList.Count > 0)
                await dataSource.AddListAsync(temporaryList, SymbolType.DYX);

            DbIndex.Dyx.AddRange(addList);
            DbIndex.Dyx = DbIndex.Dyx.GroupBy(p => p.ID).Select(grp => grp.First()).OrderByDescending(x => x.ID).ToList();

            return Result.Successful();
        }
        public PriceMinutely ComputingDyx(PriceMinutely eurUsd)
        {

            var usdchf = DbIndex.UsdChf.FirstOrDefault(x => x.Date == eurUsd.Date);
            var usdjpy = DbIndex.UsdJpy.FirstOrDefault(x => x.Date == eurUsd.Date);
            var gbpusd = DbIndex.GbpUsd.FirstOrDefault(x => x.Date == eurUsd.Date);
            var usdcad = DbIndex.UsdCad.FirstOrDefault(x => x.Date == eurUsd.Date);
            var usdsek = DbIndex.UsdSek.FirstOrDefault(x => x.Date == eurUsd.Date);

            double p = 0;

            if (usdchf != null && usdjpy != null && gbpusd != null
                && usdcad != null && usdsek != null)
            {
                p = 50.14348112 * Math.Pow(eurUsd.Close, -0.576) *
                    Math.Pow(usdjpy.Close, 0.136) *
                    Math.Pow(gbpusd.Close, -0.119) *
                    Math.Pow(usdcad.Close, 0.091) *
                    Math.Pow(usdsek.Close, 0.042) *
                    Math.Pow(usdchf.Close, 0.0361);
            }
            return new PriceMinutely
            {
                ID = eurUsd.ID,
                Date = eurUsd.Date,
                Close = p,
                Type = SymbolType.DYX
            };

        }
        #endregion
    }
}
