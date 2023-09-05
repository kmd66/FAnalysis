using Kama.FinancialAnalysis.DAL;
using Kama.FinancialAnalysis.Model;
using OfficeOpenXml.FormulaParsing.Excel.Functions.DateTime;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Math;
using System;
using System.Diagnostics;
using System.IO;
using System.Text.RegularExpressions;
using System.Timers;
using System.Web.UI.WebControls;

namespace Kama.FinancialAnalysis.Domain
{
    public class AddWorkingHoursTimer
    {
        private Timer _timer;
        public AddWorkingHoursTimer()
        {
            _timer = new Timer();
            _timer.Interval = 43200000;
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
        private void DoWork()
            => new WorkingHoursDataSource().AddAsync();
    }
}
