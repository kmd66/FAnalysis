using Kama.FinancialAnalysis.Dependency;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Kama.FinancialAnalysis.Model
{
    public class MovingAverage
    {
        public long ID { get; set; }

        public DateTime Date { get; set; }

        public double M5 { get; set; }
        public double M30 { get; set; }
        public double H1 { get; set; }
        public double D { get; set; }
        public SymbolType Type { get; set; }
    }
}
