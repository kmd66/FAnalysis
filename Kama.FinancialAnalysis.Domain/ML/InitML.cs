
namespace Kama.FinancialAnalysis.Domain
{
    public class InitMLService
    {
        BiggerThanSDHelper _biggerThanSDHelper = new BiggerThanSDHelper();
        DistanceMeasurementHelper _distanceMeasurementHelper = new DistanceMeasurementHelper();

        public InitMLService()
        {
            Start();
        }
        private async void Start()
        {
            //await _biggerThanSDHelper.Start();
            await _distanceMeasurementHelper.Start();
        }
    }
}
