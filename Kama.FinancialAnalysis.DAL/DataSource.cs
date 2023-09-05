using Kama.FinancialAnalysis.Model;
using System;
using System.Data.SqlClient;
using System.Runtime.CompilerServices;

namespace Kama.FinancialAnalysis.DAL
{
    public abstract class DataSource
    {
        public DataSource()
        {
            pbl = new PBL(AppProperty.Instance.ConnectionString);
        }

        public PBL pbl;

    }
}
