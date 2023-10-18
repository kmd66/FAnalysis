using Kama.FinancialAnalysis.Dependency;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Kama.FinancialAnalysis.Model
{
    public class Macd
    {
        public long ID { get; set; }

        public DateTime Date { get; set; }

        public double E12 { get; set; }
        public double E26 { get; set; }
        public double E60 { get; set; }
        public double E130 { get; set; }
        public double E120 { get; set; }
        public double E260  { get; set; }

        public double M12 { get => E12 - E26; }
        public double M60 { get => E60 - E130; }
        public double M120 { get => E120 - E260; }

        public SymbolType Type { get; set; }
    }
}
