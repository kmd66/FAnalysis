using Kama.FinancialAnalysis.Dependency;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Kama.FinancialAnalysis.Model
{
    public class ZigZag
    {
        public long ID { get; set; }
        public DateTime Date { get; set; }
        public long Approved { get; set; }
        public bool Up { get; set; }
        public double Value { get; set; }
        public SymbolType Type { get; set; }
    }
}
