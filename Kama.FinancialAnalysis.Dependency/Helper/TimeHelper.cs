using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kama.FinancialAnalysis.Dependency
{
    public static class TimeHelper
    {
        public static DateTime LocalTime(this string timeInTicksString) =>
            TimeZoneInfo.ConvertTimeFromUtc(timeInTicksString.UtcTime(), TimeZoneInfo.Local);

        public static DateTime UtcTime(this string timeInTicksString) =>
            DateTimeOffset.FromUnixTimeMilliseconds(timeInTicksString.ToLong()).DateTime;
    }
}
