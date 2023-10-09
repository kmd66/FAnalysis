using Kama.AppCore;
using Kama.FinancialAnalysis.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kama.FinancialAnalysis.DAL
{
    public class ZigZagDataSource : DataSource
    {
        public ZigZagDataSource(): base()
        {
        }

        public async Task<Result> Add(ZigZag model)
        {
            try
            {

                var result = (await pbl.AddZigZagAsync(
                    _id:model.ID,
                    _date:model.Date,
                    _approved:model.Approved,
                    _up:model.Up,
                    _type: (byte)model.Type
                    )).ToListActionResult<PriceView>();

                return result;
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public async Task<Result> AddList(List<ZigZag> model)
        {
            try
            {
                var result = (await pbl.AddListZigZagAsync(
                    _json: new Dependency.ObjectSerializer().Serialize(model)
                    )).ToListActionResult<PriceView>();

                return result;
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public async Task<Result<IEnumerable<ZigZag>>> GetFromToAsync(ZigZagVM model)
        {
            try
            {

                var result = (await pbl.GetFromToZigZagAsync(
                    _type: (byte)model.Type,
                    _fromDate: model.FromDate,
                    _toDate: model.ToDate
                    )).ToListActionResult<ZigZag>();

                return result;
            }
            catch (Exception e)
            {
                throw;
            }
        }

    }
}