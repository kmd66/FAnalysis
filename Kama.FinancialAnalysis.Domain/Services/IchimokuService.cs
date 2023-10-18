using Kama.AppCore;
using Kama.FinancialAnalysis.DAL;
using Kama.FinancialAnalysis.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kama.FinancialAnalysis.Domain
{
    public class IchimokuService
    {
        IchimokuDataSource dataSource = new IchimokuDataSource();
        
        public async Task<Result> AddAll(SymbolType symbolType)
        {
            var data = DbIndexPrice.GetByType(symbolType);

            List<Ichimoku> temporaryList = new List<Ichimoku>();

            foreach (var item in data)
            {
                var p1_1 = data.Where(x => x.ID < item.ID).Take(9).ToList();
                var p1_2 = data.Where(x => x.ID < item.ID).Take(26).ToList();
                var p1_3 = data.Where(x => x.ID < item.ID).Take(52).ToList();

                var p2_1 = data.Where(x => x.ID < item.ID).Take(45).ToList();
                var p2_2 = data.Where(x => x.ID < item.ID).Take(130).ToList();
                var p2_3 = data.Where(x => x.ID < item.ID).Take(260).ToList();

                if (p2_3.Count > 0)
                {
                    temporaryList.Add(new Ichimoku
                    {
                        ID = item.ID,
                        Date = item.Date,
                        Max9 = p1_1.Max(x => x.Close),
                        Max26 = p1_2.Max(x => x.Close),
                        Max52 = p1_3.Max(x => x.Close),
                        Max45 = p2_1.Max(x => x.Close),
                        Max130 = p2_2.Max(x => x.Close),
                        Max260 = p2_3.Max(x => x.Close),
                        
                        Min9 = p1_1.Min(x => x.Close),
                        Min26 = p1_2.Min(x => x.Close),
                        Min52 = p1_3.Min(x => x.Close),
                        Min45 = p2_1.Min(x => x.Close),
                        Min130 = p2_2.Min(x => x.Close),
                        Min260 = p2_3.Min(x => x.Close),
                        Type = symbolType
                    });
                }
                if (temporaryList.Count > 100)
                {
                    await dataSource.AddListAsync(temporaryList); 
                    temporaryList = new List<Ichimoku>();
                }
            }

            if (temporaryList.Count > 0)
                await dataSource.AddListAsync(temporaryList);

            return Result.Successful();
        }

    }

}
