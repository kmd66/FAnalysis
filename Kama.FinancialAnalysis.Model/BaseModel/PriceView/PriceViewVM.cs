using Kama.FinancialAnalysis.Dependency;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Kama.FinancialAnalysis.Model
{
    public class PriceViewVM : ListVM
    {
        public PriceViewVM()
        {

        }
        public DateTime? Date { get; set; }
        public SymbolType Type { get; set; }
        public DateTime FromDate{ get; set; }
        public DateTime ToDate { get; set; }
    }
}
