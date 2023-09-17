using Kama.FinancialAnalysis.Dependency;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Kama.FinancialAnalysis.Model
{
    public class PriceMinutely
    {
        public PriceMinutely()
        {

        }

        public long ID { get; set; }
        public DateTime Date { get; set; }
        public double Open { get; set; }
        public double Close { get; set; }
        public double Max { get; set; }
        public double Min { get; set; }
        public SymbolType Type { get; set; }

        public static PriceMinutely InstanceFromListString(List<string> obj, SymbolType type)
        {
            StringBuilder id = new StringBuilder("0000000000000");
            for (int i = 0; i < obj[0].Length; i++)
                id[i] = obj[0][i];

            var idLong = id.ToString().ToLong() + (byte)type;

            return new PriceMinutely
            {
                ID = idLong,
                Date = id.ToString().UtcTime(),
                Open = Math.Round(obj[1].ToDouble(), 5),
                Max = Math.Round(obj[2].ToDouble(), 5),
                Min = Math.Round(obj[3].ToDouble(), 5),
                Close = Math.Round(obj[4].ToDouble(), 5),
                Type = type
            };
        }
        public static PriceMinutely InstanceFromJson(string json, SymbolType type)
        {
            var objectSerializer = new Dependency.ObjectSerializer();
            var deserialize = objectSerializer.Deserialize<List<string>>(json);
            return InstanceFromListString(deserialize, type);
        }
        public static List<PriceMinutely> ListFromJson(string json, SymbolType type)
        {
            var list = new List<PriceMinutely>();
            var objectSerializer = new Dependency.ObjectSerializer();
            var deserialize = objectSerializer.Deserialize<List<List<string>>>(json);
            foreach (var obj in deserialize)
                list.Add(InstanceFromListString(obj, type));
            return list;
        }
    }
}
