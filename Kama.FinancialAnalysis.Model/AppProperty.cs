using System.Collections.Generic;
using System.Text;
using System;
using System.IO;

namespace Kama.FinancialAnalysis.Model
{
    public class AppProperty
    {

        public static AppProperty Instance { get; private set; }

        public static void SetInstance(string appPropertyPath)
        {
            string readText = File.ReadAllText(appPropertyPath);
            var obj = new Dependency.ObjectSerializer().Deserialize<dynamic>(readText);
            Instance = new AppProperty
            {
                ConnectionString = obj.ConnectionString,
                PythonExePath = obj.PythonExePath,
                FileDirectory = obj.FileDirectory,
                PythonScriptDirectory = obj.FileDirectory + obj.PythonScriptDirectory,
                HistoryDirectory = obj.FileDirectory + obj.HistoryDirectory,
                PriceMinutelyFile = obj.FileDirectory + obj.PriceMinutelyFile,
                PriceDailyFile = obj.FileDirectory + obj.PriceDailyFile,
            };
        }

        public string ConnectionString { get; private set; }

        public string PythonExePath { get; private set; }

        public string FileDirectory { get; private set; }

        public string PythonScriptDirectory { get; private set; }

        public string HistoryDirectory{ get; private set; }
        
        public string PriceMinutelyFile { get; private set; }

        public string PriceDailyFile { get; private set; }
    }
}