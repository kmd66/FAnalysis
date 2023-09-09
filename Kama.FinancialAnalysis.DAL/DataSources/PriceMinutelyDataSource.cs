using Kama.AppCore;
using Kama.FinancialAnalysis.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kama.FinancialAnalysis.DAL
{
    public class PriceMinutelyDataSource : DataSource
    {
        public PriceMinutelyDataSource(): base()
        {
        }

        public async Task<Result<PriceMinutely>> AddAsync(PriceMinutely model)
        {
            try
            {
                var result = (await pbl.AddPriceMinutelyAsync(
                    _id: model.ID,
                    _date: model.Date,
                    _open: (float)model.Open,
                    _close: (float)model.Close,
                    _max: (float)model.Max,
                    _min: (float)model.Min,
                    _type:(byte)model.Type
                    )).ToActionResult<PriceMinutely>();

                return result;
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public async Task<Result<PriceMinutely>> AddListAsync(List<PriceMinutely> model, SymbolType type)
        {
            try
            {
                var result = (await pbl.AddPriceMinutelysAsync(
                    _json: new Dependency.ObjectSerializer().Serialize(model),
                    _type: (byte)type
                    )).ToActionResult<PriceMinutely>();

                //if (index && result.Success && type == SymbolType.eurusd)
                //{
                //    await new DAL.MovingAverageDataSource().AddListFromIdsAsync(model.Select(x => x.ID).ToList());
                //    if (model.Count <= 10)
                //        await new StandardDeviationDataSource().AddListAsync(model.Select(x => x.ID).ToList());
                //    else
                //        new StandardDeviationDataSource().AddListAsync(model.Select(x => x.ID).ToList());
                //}

                return result;
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public async Task<Result<IEnumerable<PriceMinutely>>> ListAsync(ListVM model, SymbolType type)
        {
            try
            {
                var result = (await pbl.GetPriceMinutelysAsync(
                    _pageIndex: model.PageIndex,
                    _pageSize: model.PageSize,
                    _type: (byte)type
                    )).ToListActionResult<PriceMinutely>();

                return result;
            }
            catch (Exception e)
            {
                throw;
            }
        }
    }
}