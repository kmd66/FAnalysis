using System.Collections.Generic;
using System.Text;
using System;
using System.IO;
using System.Linq;
using System.Collections;

namespace Kama.FinancialAnalysis.Model
{
    public class DbIndex
    {
        public static List<PriceMinutely> EurUsd;
        public static List<PriceMinutely> XauUsd;
        public static List<PriceMinutely> UsdChf;
        public static List<PriceMinutely> EurJpy;

        public static List<PriceMinutely> UsdJpy;
        public static List<PriceMinutely> GbpUsd;
        public static List<PriceMinutely> UsdCad;
        public static List<PriceMinutely> UsdSek;

        public static List<PriceMinutely> Nq100m;
        public static List<PriceMinutely> Dyx;

        public static List<Sessions> Sessions;

        public static List<PriceMinutely> GetByType(SymbolType symbolType)
        {

            switch (symbolType)
            {
                case SymbolType.eurusd:
                    return EurUsd;
                case SymbolType.xauusd:
                    return XauUsd;
                case SymbolType.usdchf:
                    return UsdChf;
                case SymbolType.eurjpy:
                    return EurJpy;

                case SymbolType.usdjpy:
                    return UsdJpy;
                case SymbolType.gbpusd:
                    return GbpUsd;
                case SymbolType.usdcad:
                    return UsdCad;
                case SymbolType.usdsek:
                    return UsdSek;

                case SymbolType.nq100m:
                    return Nq100m;
                case SymbolType.DYX:
                    return Dyx;

            }
            return EurUsd;
        }

        public static void AddRange(List<PriceMinutely> list)
        {
            if (list.Count == 0)
                return;

            switch (list[0].Type)
            {
                case SymbolType.eurusd:
                    EurUsd.AddRange(list);
                    EurUsd = EurUsd.GroupBy(p => p.ID).Select(grp => grp.First()).OrderByDescending(x => x.ID).ToList();
                    break;
                case SymbolType.xauusd:
                    XauUsd.AddRange(list);
                    XauUsd = XauUsd.GroupBy(p => p.ID).Select(grp => grp.First()).OrderByDescending(x => x.ID).ToList();
                    break;
                case SymbolType.usdchf:
                    UsdCad.AddRange(list);
                    UsdCad = UsdCad.GroupBy(p => p.ID).Select(grp => grp.First()).OrderByDescending(x => x.ID).ToList();
                    break;
                case SymbolType.eurjpy:
                    EurJpy.AddRange(list);
                    EurJpy = EurJpy.GroupBy(p => p.ID).Select(grp => grp.First()).OrderByDescending(x => x.ID).ToList();
                    break;
              
                case SymbolType.usdjpy:
                    UsdJpy.AddRange(list);
                    UsdJpy = UsdJpy.GroupBy(p => p.ID).Select(grp => grp.First()).OrderByDescending(x => x.ID).ToList();
                    break;
                case SymbolType.gbpusd:
                    GbpUsd.AddRange(list);
                    GbpUsd = GbpUsd.GroupBy(p => p.ID).Select(grp => grp.First()).OrderByDescending(x => x.ID).ToList();
                    break;
                case SymbolType.usdcad:
                    UsdCad.AddRange(list);
                    UsdCad = UsdCad.GroupBy(p => p.ID).Select(grp => grp.First()).OrderByDescending(x => x.ID).ToList();
                    break;
                case SymbolType.usdsek:
                    UsdSek.AddRange(list);
                    UsdSek = UsdSek.GroupBy(p => p.ID).Select(grp => grp.First()).OrderByDescending(x => x.ID).ToList();
                    break;

                case SymbolType.nq100m:
                    Nq100m.AddRange(list);
                    Nq100m = Nq100m.GroupBy(p => p.ID).Select(grp => grp.First()).OrderByDescending(x => x.ID).ToList();
                    break;
                case SymbolType.DYX:
                    Dyx.AddRange(list);
                    Dyx = Dyx.GroupBy(p => p.ID).Select(grp => grp.First()).OrderByDescending(x => x.ID).ToList();
                    break;

            }
        }

        public static Sessions GetSession(byte symbolType)
            => Sessions.FirstOrDefault(x => (byte)x.ID == symbolType);
    }
}