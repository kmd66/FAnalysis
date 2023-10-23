using Kama.AppCore;
using Kama.FinancialAnalysis.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kama.FinancialAnalysis.DAL
{
    public class TransactionDataSource : DataSource
    {
        public TransactionDataSource(): base()
        {
        }

        public async Task<Result> AddBestPriceAsync(BestPrice model)
        {
            try
            {
                var result = (await sim.AddBestPriceAsync(
                    _id: model.ID,
                    _transavtion: model.TransactionID,
                    _priceID: model.PriceID,
                    _type: (byte)model.Type
                    )).ToListActionResult<PriceView>();

                return result;
            }
            catch (Exception e)
            {
                throw;
            }
        }
        public async Task<Result> AddTransactionAsync(Transaction model)
        {
            try
            {
                var result = (await sim.AddTransactionAsync(
                    _id: model.ID,
                    _signalPriceID: model.SignalPriceID,
                    _startPriceID: model.StartPriceID,
                    _endPriceID: model.EndPriceID,
                    _profit: (float)model.Profit,
                    _type: (byte)model.Type,
                    _returned:model.Returned
                    )).ToListActionResult<PriceView>();

                return result;
            }
            catch (Exception e)
            {
                throw;
            }
        }

    }
}