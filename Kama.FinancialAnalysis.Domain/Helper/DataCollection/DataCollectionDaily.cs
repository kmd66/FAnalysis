using Kama.FinancialAnalysis.Model;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Kama.FinancialAnalysis.Domain
{
    public class DataCollectionDaily
    {
        SymbolType _symbol;
        string _script = "";
        string _fromDate = "1";
        string _lenDate = "5";
        string _priceDailyFile = AppProperty.Instance.PriceDailyFile;
        public DataCollectionDaily(SymbolType symbol, string lenDate = "1500")
        {
            _priceDailyFile = _priceDailyFile + symbol + ".txt";
            _symbol = symbol;
            _script = $"{AppProperty.Instance.PythonScriptDirectory}getData.py";
            _lenDate = lenDate;
        }
        public async Task DoWork()
        {
            var psi = new ProcessStartInfo();
            psi.FileName =AppProperty.Instance.PythonExePath;
            psi.UseShellExecute = false;
            psi.CreateNoWindow = true;
            psi.RedirectStandardOutput = true;
            psi.RedirectStandardError = true;

            psi.Arguments = $"\"{_script}\" \"{_symbol.ToString().ToUpper()}\" \"{_fromDate}\" \"{_lenDate}\" \"{_priceDailyFile}\"";
            var errors = "";
            var results = "";

            using (var process = Process.Start(psi))
            {
                errors = process.StandardError?.ReadToEnd();
                results = process.StandardOutput.ReadToEnd();
            }

            if (errors == "" && results.Contains("success"))
            {
                string readText = File.ReadAllText(_priceDailyFile);
                var list = PriceMinutely.ListFromJson(readText, _symbol);
                await new PriceMinutelyService().AddListAsync(list, _symbol);
            }
        }
    }
}
