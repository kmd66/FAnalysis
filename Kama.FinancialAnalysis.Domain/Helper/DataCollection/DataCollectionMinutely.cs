using Kama.FinancialAnalysis.Model;
using OfficeOpenXml.FormulaParsing.Excel.Functions.DateTime;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Math;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Timers;
using System.Web.UI.WebControls;

namespace Kama.FinancialAnalysis.Domain
{
    public class DataCollectionMinutely
    {
        private Timer _timer;
        SymbolType _symbol ;
        string _script = "";
        string _fromDate = "1";
        string _lenDate = "5";
        string _priceMinutelyFile = AppProperty.Instance.PriceMinutelyFile;
        public DataCollectionMinutely(SymbolType symbol)
        {
            _priceMinutelyFile = _priceMinutelyFile + symbol + ".txt";
            _symbol = symbol;
            _script = $"{AppProperty.Instance.PythonScriptDirectory}getData.py";
            _timer = new Timer();
            _timer.Interval = 30000;
            _timer.Elapsed += _timer_Elapsed; 
        }
        private void _timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            Stop();
            DoWork();
            Start();
        }

        public void Start()
        {
            DoWork();
            _timer.Start();
        }

        public void Stop() => _timer.Stop();
        private async void DoWork()
        {
            var psi = new ProcessStartInfo();
            psi.FileName =AppProperty.Instance.PythonExePath;
            psi.UseShellExecute = false;
            psi.CreateNoWindow = true;
            psi.RedirectStandardOutput = true;
            psi.RedirectStandardError = true;

            psi.Arguments = $"\"{_script}\" \"{_symbol.ToString().ToUpper()}\" \"{_fromDate}\" \"{_lenDate}\" \"{_priceMinutelyFile}\"";
            var errors = "";
            var results = "";

            using (var process = Process.Start(psi))
            {
                errors = process.StandardError.ReadToEnd();
                results = process.StandardOutput.ReadToEnd();
            }

            if (errors == "" && results.Contains("success"))
            {
                string readText = File.ReadAllText(_priceMinutelyFile);
                var list = PriceMinutely.ListFromJson(readText, _symbol);
                new DAL.PriceMinutelyDataSource().AddListAsync(list, _symbol);
            }
        }
    }
}
