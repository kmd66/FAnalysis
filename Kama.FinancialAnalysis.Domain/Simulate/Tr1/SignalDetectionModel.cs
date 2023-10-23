using Kama.FinancialAnalysis.Model;

namespace Kama.FinancialAnalysis.Domain.Tr1
{
    public class SignalModel
    {
        public PriceMinutely TransactionItem { get; set; }
        public Macd MacdItem { get; set; }
    }
}