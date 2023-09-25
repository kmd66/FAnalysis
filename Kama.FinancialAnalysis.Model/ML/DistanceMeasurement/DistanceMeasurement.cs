using Kama.FinancialAnalysis.Dependency;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Kama.FinancialAnalysis.Model
{
    public class DistanceMeasurement
    {
        public Guid ID { get; set; }
        public Guid BiggerThanSdID { get; set; }
        public double Rate { get; set; }
        public double Macd{ get; set; }
        public double UpGs { get; set; }
        public double DownGs { get; set; }
        public SymbolType Type { get; set; }
        public SessionType Session { get; set; }
    }
}
