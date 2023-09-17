using Kama.AppCore;
using Kama.FinancialAnalysis.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kama.FinancialAnalysis.DAL
{
    public class PriceViewDataSource : DataSource
    {
        public PriceViewDataSource(): base()
        {
        }

        public async Task<Result<IEnumerable<PriceView>>> ListPriceViewBase(PriceViewVM model)
        {
            try
            {

                var result = (await pbl.GetPriceViewBasesAsync(
                    _type:(byte)model.Type,
                    _fromDate: model.FromDate,
                    _toDate: model.ToDate
                    )).ToListActionResult<PriceView>();

                return result;
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public async Task<Result<IEnumerable<MovingAverage>>> ListMovingAverage(PriceViewVM model)
        {
            try
            {
                var result = (await pbl.GetMovingAveragesAsync(
                    _pageIndex: model.PageIndex,
                    _pageSize: model.PageSize,
                    _type: (byte)model.Type

                    )).ToListActionResult<MovingAverage>();

                return result;
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public async Task<Result<IEnumerable<StandardDeviation>>> ListStandardDeviation(PriceViewVM model)
        {
            try
            {
                var result = (await pbl.GetStandardDeviationsAsync(
                    _pageIndex: model.PageIndex,
                    _pageSize: model.PageSize,
                    _type:(byte)model.Type
                    )).ToListActionResult<StandardDeviation>();

                return result;
            }
            catch (Exception e)
            {
                throw;
            }
        }
        public async Task<Result<PriceViewBase>> GetLast(SymbolType type)
        {
            try
            {
                var result = (await pbl.GetLastPriceMinutelyAsync(
                    _type: (byte)type
                    )).ToActionResult<PriceViewBase>();

                return result;
            }
            catch (Exception e)
            {
                throw;
            }
        }

        //public async Task<Result<WorkingHours>> LastWorkingHours()
        //{
        //    try
        //    {
        //        var result = (await pbl.GetLastWorkingHourAsync()).ToActionResult<WorkingHours>();

        //        return result;
        //    }
        //    catch (Exception e)
        //    {
        //        throw;
        //    }
        //}

    }
}