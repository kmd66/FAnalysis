using Kama.AppCore;
using Kama.FinancialAnalysis.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace Kama.FinancialAnalysis.DAL
{
    public class RsiDataSource : DataSource
    {
        public RsiDataSource() : base()
        {
        }

        public async Task<Result> AddListAsync(List<Rsi> list)
        {
            try
            {
                var result = (await pbl.AddListRsiAsync(
                    _json: new Dependency.ObjectSerializer().Serialize(list)
                    )).ToActionResult();

                return result;
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public async Task<Result<IEnumerable<Rsi>>> GetListAsync(RsiVM model)
        {
            try
            {
                var result = (await pbl.GetListRsiAsync(
                    _pageIndex: model.PageIndex,
                    _pageSize: model.PageSize,
                    _type: (byte)model.Type
                    )).ToListActionResult<Rsi>();

                return result;
            }
            catch (Exception e)
            {
                throw;
            }
        }

    }
}