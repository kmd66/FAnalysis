using System.Collections.Generic;
using System.Text;
using System;
using System.IO;

namespace Kama.FinancialAnalysis.Model
{
    public class DbIndex
    {
        public static List<PriceMinutely> EeurUsd;
        public static List<PriceMinutely> XauUsd;
        public static List<PriceMinutely> UsdChf;
        public static List<PriceMinutely> EurJpy;

        public static List<PriceMinutely> UsdJpy;
        public static List<PriceMinutely> GbpUsd;
        public static List<PriceMinutely> UsdCad;
        public static List<PriceMinutely> UsdSek;

        public static List<PriceMinutely> Nd100m;
        public static List<PriceMinutely> Dyx;

        public static List<PriceMinutely> GetByType(SymbolType symbolType)
        {
            if (symbolType == SymbolType.xauusd)
                return DbIndex.XauUsd;
            else if (symbolType == SymbolType.usdchf)
                return DbIndex.UsdChf;
            else if (symbolType == SymbolType.eurjpy)
                return DbIndex.EurJpy;
            else
                return DbIndex.EeurUsd;
        }
    }
}