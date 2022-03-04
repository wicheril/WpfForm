using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql;

namespace WpfSql
{
    internal class BdSql
    {
        public string ConnString = "Host=localhost;Username=postgres;Password=61288f49;Database=student";
        public NpgsqlConnection Connect;
        public BdSql()
        { 
            Connect = new NpgsqlConnection(ConnString);
        }
        public void OpenCon()
        { 
            Connect.Open();
        }
        public void CloseCon()
        { 
            Connect.Close();
        }
    }
}
