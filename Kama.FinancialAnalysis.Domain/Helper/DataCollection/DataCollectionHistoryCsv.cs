using DeviceDetectorNET;
using Kama.AppCore.IOC;
using Kama.FinancialAnalysis.Dependency;
using Kama.FinancialAnalysis.Model;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Information;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Math;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kama.FinancialAnalysis.Domain
{
    public class DataCollectionHistoryCsv
    {
        DAL.PriceMinutelyDataSource priceMinutelyDataSource = new DAL.PriceMinutelyDataSource();

        List<PriceMinutely> priceMinutelyes = new List<PriceMinutely>();
        List<Price> prices = new List<Price>();

        public async Task DoWork(SymbolType symbol, string FilePath)
        {

            try
            {
                priceMinutelyes = new List<PriceMinutely>();
                using (StreamReader sr = new StreamReader(FilePath))
                {
                    string currentLine;
                    while ((currentLine = sr.ReadLine()) != null)
                    {
                        var lineArrey = currentLine.Split('\t');
                        var p = Price.InstanceFromCsvLine(lineArrey);

                        if (p != null)
                        {
                            if (prices.Count == 0 || prices.Last().Date == p.Date)
                            {
                                prices.Add(p);
                            }
                            else
                            {
                                addPriceMinutelyes(symbol);
                                prices.Add(p);

                            }

                            if (priceMinutelyes.Count > 999)
                            {
                                await priceMinutelyDataSource.AddListAsync(priceMinutelyes, symbol, false);
                                priceMinutelyes = new List<PriceMinutely>();
                            }

                        }

                    }
                    if (prices.Count > 0)
                        addPriceMinutelyes(symbol);

                    if (priceMinutelyes.Count > 0)
                        await new DAL.PriceMinutelyDataSource().AddListAsync(priceMinutelyes, symbol);
                }
            }
            catch (Exception e) { }
        }
        private void addPriceMinutelyes(SymbolType symbol)
        {
            var priceMinutely = new PriceMinutely
            {
                ID = prices.First().Date.TOID(symbol),
                Date = prices.First().Date,
                Open = prices.First().Value ,
                Close= prices.Last().Value,
                Max = prices.Max(x => x.Value),
                Min = prices.Min(x => x.Value),
                Type = symbol
            };

            priceMinutelyes.Add(priceMinutely);
            prices = new List<Price>();
        }
    }

}
