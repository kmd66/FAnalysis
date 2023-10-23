using Kama.FinancialAnalysis.Dependency;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Kama.FinancialAnalysis.Model
{
    public class BestPrice
    {
        public Guid ID { get; set; }

        public Guid TransactionID { get; set; }

        public long PriceID { get; set; }

        public PriceType Type { get; set; }

    }
}
