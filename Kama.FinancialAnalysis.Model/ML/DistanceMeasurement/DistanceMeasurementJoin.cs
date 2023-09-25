using Kama.FinancialAnalysis.Dependency;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Kama.FinancialAnalysis.Model
{
    public class DistanceMeasurementJoin
    {
        public Guid BiggerThanSdID { get; set; }
        public PriceMinutely Price { get; set; }
        public long pMAxID { get; set; }
        public long pMinID { get; set; }
        public double R1000 { get; set; }
        public double D { get; set; }
    }
}
