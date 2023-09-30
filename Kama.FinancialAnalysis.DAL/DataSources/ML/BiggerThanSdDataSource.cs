using Kama.AppCore;
using Kama.FinancialAnalysis.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static System.Collections.Specialized.BitVector32;

namespace Kama.FinancialAnalysis.DAL
{
    public class BiggerThanSdDataSource : DataSource
    {
        public BiggerThanSdDataSource(): base()
        {
        }

        public async Task<Result<IEnumerable<BiggerThanSD>>> GetBiggerThanSDsAsync(BiggerThanSDVM model)
        {
            try
            {
                var result = (await pbl.GetBiggerThanSDsAsync(
                    _session: (byte)model.Session,
                    _type: (byte)model.Type
                    )).ToListActionResult<BiggerThanSD>();


                return result;
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public async Task<Result> AddBiggerThanSDsAsync(List<BiggerThanSD> model)
        {
            try
            {
                var result = (await pbl.AddBiggerThanSDsAsync(
                    _json: new Dependency.ObjectSerializer().Serialize(model)
                    )).ToActionResult();


                return result;
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public async Task<Result> AddBiggerThanSdOtherSymbolsAsync(List<BiggerThanSD> model)
        {
            try
            {
                var result = (await pbl.AddBiggerThanSdOtherSymbolsAsync(
                    _json: new Dependency.ObjectSerializer().Serialize(model)
                    )).ToActionResult();


                return result;
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public async Task<Result<IEnumerable<BiggerThanSD>>> ListAsync(BiggerThanSDVM model)
        {
            try
            {
                var result = (await pbl.ListBiggerThanSDAsync(
                    _session: (byte)model.Session,
                    _type: (byte)model.Type
                    )).ToListActionResult<BiggerThanSD>();


                return result;
            }
            catch (Exception e)
            {
                throw;
            }
        }

    }
}