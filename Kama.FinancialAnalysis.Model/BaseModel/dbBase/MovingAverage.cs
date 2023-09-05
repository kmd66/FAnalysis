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
        
        public DateTime Date
        {
            get
            {
                if (ID == 0)
                    return DateTime.Now;
                var h = ID.ToString().Remove(0, 1);
                return h.UtcTime();
            }
        }

        public double M5 { get; set; }
        public double M10 { get; set; }
        public double M20 { get; set; }
        public double M30 { get; set; }
        public double H1 { get; set; }
    }
}
