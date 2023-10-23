using Kama.FinancialAnalysis.Dependency;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Kama.FinancialAnalysis.Model
{
    public class Transaction
    {
        public Guid ID { get; set; }

        public long SignalPriceID { get; set; }

        public long StartPriceID { get; set; }

        public long EndPriceID { get; set; }

        public double Profit { get; set; }

        public TransactionType Type { get; set; }

        public bool Returned { get; set; }
    }
}
