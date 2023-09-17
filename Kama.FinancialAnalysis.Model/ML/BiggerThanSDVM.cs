using Kama.FinancialAnalysis.Dependency;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Kama.FinancialAnalysis.Model
{
    public class BiggerThanSDVM
    {
        public long FromID { get; set; }

        public long ToID { get; set; }
        
        public SymbolType Type { get; set; }

        public SessionType Session { get; set; }
    }
}
