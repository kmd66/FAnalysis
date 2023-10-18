using Kama.AppCore;
using Kama.FinancialAnalysis.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Kama.FinancialAnalysis.DAL
{
    public class BollingerBandsDataSource : DataSource
    {
        public BollingerBandsDataSource() : base()
        {
        }

        public async Task<Result> AddListAsync(List<BollingerBands> list)
        {
            try
            {
                var result = (await pbl.AddListBollingerBandsAsync(
                    _json: new Dependency.ObjectSerializer().Serialize(list)
                    )).ToActionResult();

                return result;
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public async Task<Result<IEnumerable<BollingerBands>>> GetFromToAsync(BollingerBandsVM model)
        {
            try
            {

                var result = (await pbl.GetFromToBollingerBandsAsync(
                    _type: (byte)model.Type,
                    _fromDate: model.FromDate,
                    _toDate: model.ToDate
                    )).ToListActionResult<BollingerBands>();

                return result;
            }
            catch (Exception e)
            {
                throw;
            }
        }

    }
}