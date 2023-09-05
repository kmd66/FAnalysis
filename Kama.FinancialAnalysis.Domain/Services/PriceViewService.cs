using Kama.AppCore;
using Kama.AppCore.IOC;
using Kama.FinancialAnalysis.DAL;
using Kama.FinancialAnalysis.Model;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Kama.FinancialAnalysis.Domain
{
    public class PriceViewService
    {
        public async Task<Result<PriceView>> ListViewAsync(PriceViewVM model)
        {
            var dataSource = new PriceViewDataSource();

            var baseResult = await dataSource.ListPriceViewBase(model);
            if (!baseResult.Success)
                return Result<PriceView>.Failure(message: baseResult.Message);

            if(model.Type != SymbolType.eurusd)
                return Result<PriceView>.Successful(data: new PriceView
                {
                    Bases = baseResult.Data.ToList()
                });

            var movingAverageResult = await dataSource.ListMovingAverage(model);
            if (!movingAverageResult.Success)
                return Result<PriceView>.Failure(message: movingAverageResult.Message);

            var StandardDeviationResult = await dataSource.ListStandardDeviation(model);
            if (!StandardDeviationResult.Success)
                return Result<PriceView>.Failure(message: StandardDeviationResult.Message);

            var workingHourResult = await dataSource.LastWorkingHours();
            if (!workingHourResult.Success)
                return Result<PriceView>.Failure(message: workingHourResult.Message);


            return Result<PriceView>.Successful(data:new PriceView
            {
                Bases = baseResult.Data.ToList(),
                MovingAverages = movingAverageResult.Data.ToList(),
                StandardDeviations = StandardDeviationResult.Data.ToList(),
                WorkingHour = workingHourResult.Data,
            });


        }
    }
}
