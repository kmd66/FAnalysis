using Kama.AppCore;
using Kama.FinancialAnalysis.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Kama.FinancialAnalysis.DAL
{
    public class MovingAverageDataSource : DataSource
    {
        public MovingAverageDataSource(): base()
        {
        }

        //public async Task<Result> AddListFromIdsAsync(List<long> ids)
        //{
        //    try
        //    {
        //        //var result = (await pbl.AddMovingAveragesAsync(
        //        //    _json: new Dependency.ObjectSerializer().Serialize(ids)
        //        //    )).ToActionResult();

        //        return Result.Successful();
        //    }
        //    catch (Exception e)
        //    {
        //        throw;
        //    }
        //}
        public async Task<Result<IEnumerable<MovingAverage>>> GetEmptys(List<long> ids)
        {
            try
            {
                var result = (await pbl.GetEmptysMovingAverageAsync(
                    _json: new Dependency.ObjectSerializer().Serialize(ids)
                    )).ToListActionResult<MovingAverage>();

                return result;
            }
            catch (Exception e)
            {
                throw;
            }
        }
        public async Task<Result> AddListAsync(List<MovingAverage> ids)
        {
            try
            {
                var result = (await pbl.AddListMovingAverageAsync(
                    _json: new Dependency.ObjectSerializer().Serialize(ids)
                    )).ToActionResult();

                return result;
            }
            catch (Exception e)
            {
                throw;
            }
        }

        //public async Task<Result> AddAllAsync()
        //{
        //    try
        //    {
        //        //var result = (await pbl.AddAllMovingAverageAsync()).ToActionResult();

        //        return Result.Successful();
        //    }
        //    catch (Exception e)
        //    {
        //        throw;
        //    }
        //}

    }
}