using Kama.FinancialAnalysis.Dependency;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Kama.FinancialAnalysis.Model
{
    public class BiggerThanSD
    {
        public Guid ID { get; set; }
        public long PriceID { get; set; }
        public double R1000 { get; set; }
        public double Rate { get; set; }
        public SymbolType Type { get; set; }
        public SessionType Session { get; set; }
        public long MaxPriceID { get; set; }
        public long MinPriceID { get; set; }
    }
}
