using System.Collections.Generic;
using System.Text;
using System;
using System.IO;
using System.Linq;
using System.Collections;

namespace Kama.FinancialAnalysis.Model
{
    public class DbIndexMovingAverage
    {
        public static List<MovingAverage> Index;

        public static List<MovingAverage> GetByType(SymbolType symbolType)
            => Index.Where(x => x.Type == symbolType).ToList();
    }
}