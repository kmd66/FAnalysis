using Kama.FinancialAnalysis.Dependency;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kama.FinancialAnalysis.Model
{
    public class StandardDeviation
    {
        public long ID { get; set; }

        public DateTime Date { get; set; }

        public double R100 { get; set; }
        public double R500 { get; set; }
        public double R1000 { get; set; }
        public SymbolType Type { get; set; }
    }
}
