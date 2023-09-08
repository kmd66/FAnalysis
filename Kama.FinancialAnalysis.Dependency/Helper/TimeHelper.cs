using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kama.FinancialAnalysis.Dependency
{
    public static class TimeHelper
    {
        private static readonly DateTime Epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        public static DateTime LocalTime(this string timeInTicksString) =>
            TimeZoneInfo.ConvertTimeFromUtc(timeInTicksString.UtcTime(), TimeZoneInfo.Local);

        public static DateTime UtcTime(this string timeInTicksString) =>
            DateTimeOffset.FromUnixTimeMilliseconds(timeInTicksString.ToLong()).DateTime;

        public static long TOID(this DateTime dt, dynamic symbol) {

            DateTime Epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            TimeSpan elapsedTime =dt - Epoch;

            var l = ((long)elapsedTime.TotalSeconds + (byte)symbol).ToString();

            StringBuilder id = new StringBuilder("0000000000000");
            for (int i = 0; i < l.Length; i++)
                id[i] = l[i];

            return id.ToString().ToLong();
        }
            
    }
}
