using Kama.FinancialAnalysis.Dependency;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Kama.FinancialAnalysis.Model
{
    public class Ichimoku
    {
        public long ID { get; set; }

        public DateTime Date { get; set; }

        public double Max9 { get; set; }
        public double Max26 { get; set; }
        public double Max52 { get; set; }
        public double Max45 { get; set; }
        public double Max130 { get; set; }
        public double Max260 { get; set; }


        public double Min9 { get; set; }
        public double Min26 { get; set; }
        public double Min52 { get; set; }
        public double Min45 { get; set; }
        public double Min130 { get; set; }
        public double Min260{ get; set; }

        public double Tenkan9 { get =>(Max9 + Min9) / 2;  }
        public double Kijun9 { get => (Max26 + Min26) / 2; }
        public double SenkouA9 { get => (Tenkan9 + Kijun9) / 2; }
        public double SenkouB9 { get => (Max52 + Min52) / 2; }

        public double Tenkan45 { get => (Max45 + Min45) / 2; }
        public double Kijun45 { get => (Max130 + Min130) / 2; }
        public double SenkouA45 { get => (Tenkan45 + Kijun45) / 2; }
        public double SenkouB45 { get => (Max260 + Min260) / 2; }

        public SymbolType Type { get; set; }
    }
}
