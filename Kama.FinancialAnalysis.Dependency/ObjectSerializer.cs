using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kama.FinancialAnalysis.Dependency
{
    public class ObjectSerializer
    {
        public T Deserialize<T>(string serializedValue)
            => Newtonsoft.Json.JsonConvert.DeserializeObject<T>(serializedValue);

        public string Serialize(object obj)
        {
            if (obj == null)
                return null;

            return Newtonsoft.Json.JsonConvert.SerializeObject(obj);
        }
    }
}
