using System.Collections.Generic;
using System.Text;
using System;
using System.IO;
using System.Linq;
using System.Collections;

namespace Kama.FinancialAnalysis.Model
{
    public class DbIndexStandardDeviation
    {
        public static List<StandardDeviation> Index;

        public static List<StandardDeviation> GetByType(SymbolType symbolType)
            => Index.Where(x => x.Type == symbolType).ToList();
    }
}