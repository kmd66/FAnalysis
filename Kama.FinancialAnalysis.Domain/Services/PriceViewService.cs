using DeviceDetectorNET.Class.Device;
using Kama.AppCore;
using Kama.AppCore.IOC;
using Kama.FinancialAnalysis.DAL;
using Kama.FinancialAnalysis.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kama.FinancialAnalysis.Domain
{
    public class PriceViewService
    {
        PriceViewDataSource _dataSource = new PriceViewDataSource();
        public async Task<Result<IEnumerable<PriceView>>> ListViewAsync(PriceViewVM model)
        {

            var dayIndex = model.PageIndex < 1 ? 0 : (model.PageIndex - 1) * -1;
            var closeTime = DbIndexPrice.GetSession((byte)model.Type).GetTimeColse();

            var resultLastItem = await _dataSource.GetLast(model.Type);
           
            model.ToDate = resultLastItem.Data.Date.AddDays(dayIndex);
            int y = model.ToDate .Date.Year;
            int m = model.ToDate .Date.Month;
            int d = model.ToDate.Date.Day;
            model.FromDate = new DateTime(y, m, d, closeTime[0], closeTime[1], 0);
            model.FromDate = model.FromDate.AddHours(-1);
            model.ToDate = model.FromDate.AddHours(25);

            var result = await _dataSource.ListPriceViewBase(model);
            if(!result.Success)
                return Result<IEnumerable<PriceView>>.Failure(message: result.Message);
            
            await addSession(result.Data, model.Type);

            var resultBiggerThanSD = await _dataSource.GetBiggerThanSDBetweenIDAsync(new BiggerThanSDVM { 
                FromID = result.Data.Min(x => x.ID),
                ToID= result.Data.Max(x => x.ID),
                Type = model.Type
            });
            foreach(var item in resultBiggerThanSD.Data.ToList())
            {
                var p = result.Data.FirstOrDefault(x => x.ID == item.PriceID);
                if (p != null)
                    p.BiggerThanSD = item;
            }

            return result;
        }

        public async Task<Result<List<List<PriceViewBase>>>> FromTOAllSymbol(PriceViewVM model)
        {
            var list = new List<List<PriceViewBase>>();

            model.ToDate = model.ToDate.Year != 1 ? model.ToDate : model.FromDate.AddMinutes(400);

            model.Type = SymbolType.xauusd;
            var result = await _dataSource.GetFromTOPriceMinutelysAsync(model);
            if (!result.Success)
                return Result<List<List<PriceViewBase>>>.Failure(message: result.Message);
            list.Add(result.Data.ToList());


            model.Type = SymbolType.usdchf;
            result = await _dataSource.GetFromTOPriceMinutelysAsync(model);
            if (!result.Success)
                return Result<List<List<PriceViewBase>>>.Failure(message: result.Message);
            list.Add(result.Data.ToList());


            model.Type = SymbolType.eurjpy;
            result = await _dataSource.GetFromTOPriceMinutelysAsync(model);
            if (!result.Success)
                return Result<List<List<PriceViewBase>>>.Failure(message: result.Message);
            list.Add(result.Data.ToList());


            model.Type = SymbolType.nq100m;
            result = await _dataSource.GetFromTOPriceMinutelysAsync(model);
            if (!result.Success)
                return Result<List<List<PriceViewBase>>>.Failure(message: result.Message);
            list.Add(result.Data.ToList());


            model.Type = SymbolType.DYX;
            result = await _dataSource.GetFromTOPriceMinutelysAsync(model);
            if (!result.Success)
                return Result<List<List<PriceViewBase>>>.Failure(message: result.Message);
            list.Add(result.Data.ToList());

            return Result<List<List<PriceViewBase>>>.Successful(data:list);
        }

        private async Task addSession(IEnumerable<PriceView> result, SymbolType type)
        {
            var closeTime = DbIndexPrice.GetSession((byte)type).GetTimeColse();

            var nykOpenTime = DbIndexPrice.GetSession((byte)SessionType.newYork).GetTimeColse();
            var lonOpenTime = DbIndexPrice.GetSession((byte)SessionType.london).GetTimeColse();
            var sydOpenTime = DbIndexPrice.GetSession((byte)SessionType.sydney).GetTimeColse();
            var nykCloseTime = DbIndexPrice.GetSession((byte)SessionType.newYork).GetTimeOpen();
            var lonCloseTime = DbIndexPrice.GetSession((byte)SessionType.london).GetTimeOpen();
            var sydCloseTime = DbIndexPrice.GetSession((byte)SessionType.sydney).GetTimeOpen();
            foreach (var item in result.ToList())
            {

                if (item.Date.Hour == closeTime[0] && item.Date.Minute == closeTime[1])
                    item.Session = SessionOCType.dayOpen;

                if (item.Date.Hour == nykOpenTime[0] && item.Date.Minute == nykOpenTime[1])
                    item.Session = SessionOCType.newYorkOpen;
                if (item.Date.Hour == lonOpenTime[0] && item.Date.Minute == lonOpenTime[1])
                    item.Session = SessionOCType.londonOpen;
                if (item.Date.Hour == sydOpenTime[0] && item.Date.Minute == sydOpenTime[1])
                    item.Session = SessionOCType.sydneyOpen;

                if (item.Date.Hour == nykCloseTime[0] && item.Date.Minute == nykCloseTime[1])
                    item.Session = SessionOCType.newYorkClose;
                if (item.Date.Hour == lonCloseTime[0] && item.Date.Minute == lonCloseTime[1])
                    item.Session = SessionOCType.londonClose;
                if (item.Date.Hour == sydCloseTime[0] && item.Date.Minute == sydCloseTime[1])
                    item.Session = SessionOCType.sydneyClose;
            }
        }

    }
}
