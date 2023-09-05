using Kama.FinancialAnalysis.Dependency;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Kama.FinancialAnalysis.Model
{
    public class WorkingHours
    {
        public long ID { get; set; }
        public DateTime Date { get; set; }
        public double LndOpen { get; set; }
        public double LndClose { get; set; }
        public double NykOpen { get; set; }
        public double NykClose { get; set; }
        public double TokOpen { get; set; }
        public double TokCLose { get; set; }
    }
}
