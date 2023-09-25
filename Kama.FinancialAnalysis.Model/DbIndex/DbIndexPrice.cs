using System.Collections.Generic;
using System.Text;
using System;
using System.IO;
using System.Linq;
using System.Collections;

namespace Kama.FinancialAnalysis.Model
{
    public class DbIndexPrice
    {
        public static List<PriceMinutely> XauUsd;
        public static List<PriceMinutely> UsdChf;
        public static List<PriceMinutely> EurJpy;

        public static List<PriceMinutely> Nq100m;
        public static List<PriceMinutely> Dyx;

        public static List<Sessions> Sessions;

        public static List<PriceMinutely> All(SymbolType filterSymbolType)
        {
            var l = new List<PriceMinutely>();

            if (filterSymbolType != SymbolType.xauusd)
                l.AddRange(XauUsd);
            if (filterSymbolType != SymbolType.usdchf)
                l.AddRange(UsdChf);
            if (filterSymbolType != SymbolType.eurjpy)
                l.AddRange(EurJpy);
            if (filterSymbolType != SymbolType.nq100m)
                l.AddRange(Nq100m);
            if (filterSymbolType != SymbolType.DYX)
                l.AddRange(Dyx);

            return l;
        }

        public static List<PriceMinutely> GetByType(SymbolType symbolType)
        {

            switch (symbolType)
            {
                case SymbolType.xauusd:
                    return XauUsd;
                case SymbolType.usdchf:
                    return UsdChf;
                case SymbolType.eurjpy:
                    return EurJpy;


                case SymbolType.nq100m:
                    return Nq100m;
                case SymbolType.DYX:
                    return Dyx;

            }
            return EurJpy;
        }

        public static void AddRange(List<PriceMinutely> list)
        {
            if (list.Count == 0)
                return;

            switch (list[0].Type)
            {
                case SymbolType.xauusd:
                    XauUsd.AddRange(list);
                    XauUsd = XauUsd.GroupBy(p => p.ID).Select(grp => grp.First()).OrderByDescending(x => x.ID).ToList();
                    break;
                case SymbolType.usdchf:
                    UsdChf.AddRange(list);
                    UsdChf = UsdChf.GroupBy(p => p.ID).Select(grp => grp.First()).OrderByDescending(x => x.ID).ToList();
                    break;
                case SymbolType.eurjpy:
                    EurJpy.AddRange(list);
                    EurJpy = EurJpy.GroupBy(p => p.ID).Select(grp => grp.First()).OrderByDescending(x => x.ID).ToList();
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