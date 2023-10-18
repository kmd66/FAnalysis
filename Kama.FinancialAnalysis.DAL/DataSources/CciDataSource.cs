using Kama.AppCore;
using Kama.FinancialAnalysis.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Kama.FinancialAnalysis.DAL
{
    public class CciDataSource : DataSource
    {
        public CciDataSource() : base()
        {
        }

        public async Task<Result> AddListAsync(List<Cci> list)
        {
            try
            {
                var result = (await pbl.AddListCciAsync(
                    _json: new Dependency.ObjectSerializer().Serialize(list)
                    )).ToActionResult();

                return result;
            }
            catch (Exception e)
            {
                throw;
            }
        }

    }
}