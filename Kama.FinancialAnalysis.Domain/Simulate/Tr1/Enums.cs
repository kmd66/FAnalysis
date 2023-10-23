namespace Kama.FinancialAnalysis.Domain.Tr1
{
    public enum PositionType : byte
    {
        WaitingForSignal = 0,
        WaitingForTransaction = 1,
        Transaction = 2,
        CloseTransaction = 3,
    }
}