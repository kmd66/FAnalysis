using Kama.FinancialAnalysis.Dependency;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Kama.FinancialAnalysis.Model
{
    public class BollingerBands
    {

        public BollingerBands()
        {

        }
        public static BollingerBands InstanceFromPriceMinutely(PriceMinutely item, SymbolType symbolType)
        {
            return new BollingerBands
            {
                ID = item.ID,
                Date = item.Date,
                A20 = item.Close,
                U20 = item.Close,
                L20 = item.Close,
                A30 = item.Close,
                U30 = item.Close,
                L30 = item.Close,
                A40 = item.Close,
                U40 = item.Close,
                L40 = item.Close,
                Type = symbolType
            };
        }
        public long ID { get; set; }

        public DateTime Date { get; set; }

        public double A20 { get; set; }
        public double U20 { get; set; }
        public double L20 { get; set; }
        public double A30 { get; set; }
        public double U30 { get; set; }
        public double L30 { get; set; }
        public double A40 { get; set; }
        public double U40 { get; set; }
        public double L40 { get; set; }


        public SymbolType Type { get; set; }
    }
}
