using Kama.FinancialAnalysis.Dependency;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Kama.FinancialAnalysis.Model
{
    public class PriceView
    {
        public List<PriceViewBase> Bases { get; set; }
        public List<MovingAverage> MovingAverages { get; set; }
        public List<StandardDeviation> StandardDeviations { get; set; }
        public WorkingHours WorkingHour { get; set; }
    }

    public class PriceViewBase
    {
        public long ID { get; set; }
        public DateTime Date { get; set; }
        public double Open { get; set; }
        public double Close { get; set; }
        public double Max { get; set; }
        public double Min { get; set; }
        public bool Asc { get; set; }
    }
}
