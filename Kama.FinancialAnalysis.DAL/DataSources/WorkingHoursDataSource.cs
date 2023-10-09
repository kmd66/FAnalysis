using Kama.AppCore;
using Kama.FinancialAnalysis.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Kama.FinancialAnalysis.DAL
{
    public class WorkingHoursDataSource : DataSource
    {
        public WorkingHoursDataSource(): base()
        {
        }

        public async Task<Result<IEnumerable<Sessions>>> GetSessionsAsync()
        {
            try
            {
                var result = (await pbl.GetSessionsAsync()).ToListActionResult<Sessions>();

                return result;
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public async Task<Result> AddAsync(SymbolType symbolType)
        {
            try
            {
                var result = (await pbl.AddWorkingHoursAsync(
                    _type:(byte)symbolType
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