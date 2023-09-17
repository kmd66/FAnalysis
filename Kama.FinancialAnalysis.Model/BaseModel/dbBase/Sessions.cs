using Kama.FinancialAnalysis.Dependency;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Kama.FinancialAnalysis.Model
{
    public class Sessions
    {
        public SessionType ID { get; set; }

        public string Open { get; set; }

        public string Close { get; set; }

        public List<int> GetTimeColse()
            => GetTime(Close);

        public List<int> GetTimeOpen()
            => GetTime(Open);

        private static List<int> GetTime(string s)
        {
            try
            {
                var t = s.Split(':');
                return new List<int>()
                {
                    int.Parse(t[0]),
                    int.Parse(t[1]),
                    int.Parse(t[2]),
                };
            }
            catch
            {
                return new List<int>() { 0, 0, 0 };
            }
        }
    }
}
