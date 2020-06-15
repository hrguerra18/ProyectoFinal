using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace DAL
{
     public class ConnectionManager
    {
        public String ConnectionString;
        public SqlConnection Connection;

        public ConnectionManager(string connectionString)
        {
            ConnectionString = connectionString;
            Connection = new SqlConnection(connectionString);

        }

        public void Open() 
        {
            Connection.Open();
        }
        public void Closed() 
        {
            Connection.Close();
        }




    }
}
