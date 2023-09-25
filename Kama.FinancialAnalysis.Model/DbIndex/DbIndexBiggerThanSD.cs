using System.Collections.Generic;
using System.Text;
using System;
using System.IO;
using System.Linq;
using System.Collections;

namespace Kama.FinancialAnalysis.Model
{
    public class DbIndexBiggerThanSD
    {
        public static List<BiggerThanSD> Index;

        public static List<BiggerThanSD> GetByType(SymbolType symbolType,SessionType sessionType)
            => Index.Where(x => x.Type == symbolType && x.Session == sessionType).ToList();

        public static void AddRange(List<PriceMinutely> list)
        {
        }

    }
}