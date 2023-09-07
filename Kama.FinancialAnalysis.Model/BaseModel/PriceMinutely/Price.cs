using Kama.FinancialAnalysis.Dependency;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Kama.FinancialAnalysis.Model
{
    public class Price
    {
        public DateTime Date { get; set; }
        public double Value { get; set; }

        public static Price InstanceFromCsvLine(string[] lineArrey)
        {
            var dt = ConvertDate(lineArrey);
            if (dt == null)
                return null;
            if (string.IsNullOrEmpty(lineArrey[2]) && string.IsNullOrEmpty(lineArrey[3]))
                return null;

            return new Price
            {
                Date= (DateTime)dt,
                Value = string.IsNullOrEmpty(lineArrey[3])? lineArrey[2].ToDouble(): lineArrey[3].ToDouble()
            };
        }
        private static DateTime? ConvertDate(string[] lineArrey)
        {
            try
            {
                var date = lineArrey[0].Replace('.', '-');
                var TimeArrey = lineArrey[1].Split(':');
                string dateTime = $"{date} {TimeArrey[0]}:{TimeArrey[1]}:00";
                DateTime dt = Convert.ToDateTime(dateTime);


                return dt;

            }
            catch
            {
                return null;
            }
        }
    }
}

