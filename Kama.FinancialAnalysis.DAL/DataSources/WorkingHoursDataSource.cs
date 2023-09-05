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

        public async Task<Result> AddAsync()
        {
            try
            {
                var result = (await pbl.AddWorkingHoursAsync()).ToActionResult();

                return result;
            }
            catch (Exception e)
            {
                throw;
            }
        }

    }
}