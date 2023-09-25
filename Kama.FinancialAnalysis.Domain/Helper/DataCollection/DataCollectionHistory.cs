using DeviceDetectorNET;
using Kama.AppCore.IOC;
using Kama.FinancialAnalysis.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Kama.FinancialAnalysis.Domain
{
    public class DataCollectionHistory
    {
        public async Task DoWork(SymbolType symbol)
        {
            var service = new PriceMinutelyService();

            var historyDirectory = $"{AppProperty.Instance.HistoryDirectory}{symbol}\\";
            List<PriceMinutely> list = new List<PriceMinutely>();
            List<List<PriceMinutely>> insertList = new List<List<PriceMinutely>>();
            DirectoryInfo d = new DirectoryInfo(historyDirectory);

            FileInfo[] Files = d.GetFiles(/*"*.txt"*/); 
            string str = "";
            foreach (FileInfo file in Files)
            {
                str = str + ", " + file.Name;
                string readText = File.ReadAllText(historyDirectory + file.Name);
                list.AddRange(PriceMinutely.ListFromJson(readText, symbol));
            }
            list = list.GroupBy(x => x.ID).Select(g => g.First()).ToList();

            List<PriceMinutely> temporaryList = new List<PriceMinutely>();
            int i = 0;
            foreach (var obj in list)
            {
                i++;
                if (i > 1000)
                {
                    i = 0;
                    insertList.Add(temporaryList);
                    temporaryList = new List<PriceMinutely>();
                }
                temporaryList.Add(obj);
            }
            insertList.Add(temporaryList);

            foreach (FileInfo file in Files)
                File.Delete(historyDirectory + file.Name);

            if (insertList.Count == 1 && insertList[0].Count == 0)
                return;

            var dbList = DbIndexPrice.GetByType(symbol);


            foreach (var insert in insertList)
                await service.AddListAsync(insert, symbol);

        }
    }
}
