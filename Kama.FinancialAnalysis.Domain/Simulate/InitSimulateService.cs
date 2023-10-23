
using Kama.FinancialAnalysis.Domain.Tr1;

namespace Kama.FinancialAnalysis.Domain
{
    public class InitSimulateService
    {
        Tr1.Tr1Helper tr1Helper = new Tr1.Tr1Helper();
        Tr1.Tr2Helper tr2Helper = new Tr1.Tr2Helper();
        DistanceMeasurementHelper _distanceMeasurementHelper = new DistanceMeasurementHelper();
        ZigZagHelper _zigZagHelper = new ZigZagHelper();

        public InitSimulateService()
        {
            Start();
        }
        private async void Start()
        {
            await tr1Helper.Start();
            //await tr2Helper.Start();
        }
    }
}
