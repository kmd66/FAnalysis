using Kama.AppCore;
using Kama.FinancialAnalysis.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Kama.FinancialAnalysis.DAL
{
    public class MacdDataSource : DataSource
    {
        public MacdDataSource() : base()
        {
        }

        public async Task<Result> AddListAsync(List<Macd> list)
        {
            try
            {
                var t = new Dependency.ObjectSerializer().Serialize(list);
                var result = (await pbl.AddListMacdAsync(
                    _json: new Dependency.ObjectSerializer().Serialize(list)
                    )).ToActionResult();

                return result;
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public async Task<Result<IEnumerable<Macd>>> GetFromToAsync(MacdVM model)
        {
            try
            {

                var result = (await pbl.GetFromToMacdAsync(
                    _type: (byte)model.Type,
                    _fromDate: model.FromDate,
                    _toDate: model.ToDate
                    )).ToListActionResult<Macd>();

                return result;
            }
            catch (Exception e)
            {
                throw;
            }
        }

    }
}