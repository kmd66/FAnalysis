using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kama.FinancialAnalysis.Dependency
{
    public static class SringHelper
    {
        public static long ToLong(this string timeInTicksString)
        {
            try
            {
                return long.Parse(timeInTicksString);
            }
            catch
            {
                return 0;
            }
        }
        public static double ToDouble(this string timeInTicksString)
        {
            try
            {
                return double.Parse(timeInTicksString);
            }
            catch
            {
                return 0;
            }
        }
    }
}
