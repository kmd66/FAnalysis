using Kama.FinancialAnalysis.Dependency;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Kama.FinancialAnalysis.Model
{
    public class Rsi
    {
        public long ID { get; set; }

        public DateTime Date { get; set; }

        public double Value14 { get; set; }
        
        public double Value32 { get; set; }
        
        public double Value70{ get; set; }

        public SymbolType Type { get; set; }
    }
}
