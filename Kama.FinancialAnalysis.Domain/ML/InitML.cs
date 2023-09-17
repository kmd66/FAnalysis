
namespace Kama.FinancialAnalysis.Domain
{
    public class InitMLService
    {
        BiggerThanSDHelper _biggerThanSDHelper = new BiggerThanSDHelper();

        public InitMLService()
        {
            Start();
        }
        private async void Start()
        {
            await _biggerThanSDHelper.Start(); 
        }
    }
}
