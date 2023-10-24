using Kama.FinancialAnalysis.Dependency;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Kama.FinancialAnalysis.Model
{
    public class PriceView
    {
        public long ID { get; set; }
        public DateTime Date { get; set; }
        public double Open { get; set; }
        public double Close { get; set; }
        public double Max { get; set; }
        public double Min { get; set; }

        public double M5 { get; set; }
        public double M30 { get; set; }
        public double H1 { get; set; }
        public double D { get; set; }

        public double R100 { get; set; }
        public double R500 { get; set; }
        public double R1000 { get; set; }

        public double ZigZag { get; set; }

        public long ZigZagID { get; set; }

        public double Rsi14 { get; set; }
        public double Rsi32{ get; set; }

        public double Cci14 { get; set; }
        public double Cci32 { get; set; }

        public BollingerBands BollingerBands { get; set; }

        public Ichimoku Ichimoku { get; set; }

        public Macd Macd { get; set; }

        public SessionOCType Session { get; set; }

        public BiggerThanSD BiggerThanSD { get; set; }

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
