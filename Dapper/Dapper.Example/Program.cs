using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dapper.Example
{
    class Program
    {
        public static string Connection = ConfigurationManager.ConnectionStrings["Connection"].ConnectionString;
        public static Func<DbConnection> ConnectionFactory = () => new SqlConnection(Connection);
        static void Main(string[] args)
        {
            StrongTypeQ();
            
        }

        public static void DynamicQ()
        {
            using (var connection = ConnectionFactory())
            {
                connection.Open();
                var invoices = connection.Query("Select * from Invoice").ToList();
            }
        }

        public static void StrongTypeQ()
        {
            using (var connection = ConnectionFactory())
            {
                connection.Open();
                var obj = new Invoice();
                

                var invoices = connection.Query<Invoice>("Select * from Invoice").ToList();
                foreach(var item in invoices)
                {
                    foreach(var property in obj.GetType().GetProperties())
                    {
                        Console.WriteLine(property.GetValue(obj));
                    }
                }
                Console.Read();
            }
        }
    }
    public class Invoice 
    {        
        public string InvoiceID { get; set; }
        public string Kind;
        public string Code;

    }
}
