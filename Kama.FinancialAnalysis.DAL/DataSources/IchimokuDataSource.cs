using Kama.AppCore;
using Kama.FinancialAnalysis.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Kama.FinancialAnalysis.DAL
{
    public class IchimokuDataSource : DataSource
    {
        public IchimokuDataSource() : base()
        {
        }

        public async Task<Result> AddListAsync(List<Ichimoku> list)
        {
            try
            {
                var result = (await pbl.AddListIchimokuAsync(
                    _json: new Dependency.ObjectSerializer().Serialize(list)
                    )).ToActionResult();

                return result;
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public async Task<Result<IEnumerable<Ichimoku>>> GetFromToAsync(IchimokuVM model)
        {
            try
            {

                var result = (await pbl.GetFromToIchimokuAsync(
                    _type: (byte)model.Type,
                    _fromDate: model.FromDate,
                    _toDate: model.ToDate
                    )).ToListActionResult<Ichimoku>();

                return result;
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public async Task<Result<IEnumerable<Ichimoku>>> ListAsync(IchimokuVM model)
        {
            try
            {
                var result = (await pbl.GetListIchimokuAsync(
                    _pageIndex: model.PageIndex,
                    _pageSize: model.PageSize,
                    _type: (byte)model.Type
                    )).ToListActionResult<Ichimoku>();

                return result;
            }
            catch (Exception e)
            {
                throw;
            }
        }

    }
}