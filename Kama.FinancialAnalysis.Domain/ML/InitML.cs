
namespace Kama.FinancialAnalysis.Domain
{
    public class InitMLService
    {
        BiggerThanSDHelper _biggerThanSDHelper = new BiggerThanSDHelper();
        DistanceMeasurementHelper _distanceMeasurementHelper = new DistanceMeasurementHelper();
        ZigZagHelper _zigZagHelper = new ZigZagHelper();

        public InitMLService()
        {
            Start();
        }
        private async void Start()
        {
            //await _biggerThanSDHelper.Start();
            //await _distanceMeasurementHelper.Start();
            await _zigZagHelper.StartHistory();
        }
    }
}
