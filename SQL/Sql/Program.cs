using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Sql
{
    class Program
    {
        public class result
        {
            public string msg;
            public string rtncode;
            public long spTime = -1;
        }
        static ConcurrentDictionary<string, result> dic = new ConcurrentDictionary<string, result>();
        static StreamWriter sw1;
        static string connectionStr;
        static object lck = new object();
        static int run = 0;
        static int error = 0;
        static void Main(string[] args)
        {
            try
            {
                if (File.Exists("error.txt")) File.Delete("error.txt");
                sw1 = new StreamWriter("error.txt", true);
                int num;
                if (!int.TryParse(args[0], out num)) { Console.WriteLine("參數輸入[" + num + "]格式不正確，應為數字。"); Console.ReadKey(); return; }
                Stopwatch sw = new Stopwatch();
                sw.Start();

                connectionStr = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString1"].ToString();
                GetAlert("select distinct top(" + args[1] + ")  AlertTime from PRA022_MeetAlertRule where AlertCode = 'a943c' AND AnalysisCompleteMark = 'Y'  and AlertDate = " + args[0]);
                Console.WriteLine($"total run is [{dic.Count}]");
                foreach (var item in dic.Keys)
                {
                    //Thread.Sleep(500);
                    dosql(item);
                }

                while (true)
                {
                    int cnt = dic.Values.Count((x) => x.spTime != -1);
                    Console.WriteLine($"{DateTime.Now.ToString("hh:MM:ss.fff")}  已完成[{cnt} / {dic.Count}]");
                    if (cnt == dic.Count) break;
                    StreamWriter swt = new StreamWriter("status.txt");
                    foreach (var item in dic.Where((x) => x.Value.spTime == -1))
                    {
                        swt.WriteLine("Still Running");
                        swt.WriteLine($"{DateTime.Now.ToString("hh:MM:ss.fff")}  AlertTime [{item.Key.ToString().PadLeft(12, '0')}");
                    }
                    swt.Close();
                    SpinWait.SpinUntil( () => dic.Count(x => x.Value.spTime == -1) == 0, 10000);
                    Thread.Sleep(10000);
                }
                sw.Stop();                
                PrintResult(dic);
                sw1.Close();
                Console.WriteLine($"{DateTime.Now.ToString("hh:MM:ss.fff")}  done");
               
            }catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            Console.Read();
        }

        static void PrintResult(ConcurrentDictionary<string, result> src)
        {
            StreamWriter sw = new StreamWriter("log.txt");
            while (src.Count(x => x.Value.spTime == -1) > 0) ;
            foreach (KeyValuePair<string, result> item in src)
            {
                //Console.WriteLine($"AlertTime [{item.Key.ToString().PadLeft(12, '0')}, Spend Time [{item.Value.ToString("###,###,###.###")}]]");
                sw.WriteLine($"{DateTime.Now.ToString("hh:MM:ss.fff")}  AlertTime [{item.Key.ToString().PadLeft(12, '0')}], Spend Time [{item.Value.spTime.ToString("###,###,###.###")}], rtncode [{item.Value.rtncode}], msg [{item.Value.msg}]");
               
            }
            sw.Close();
        }
        static void dosql(string time)
        {
            Thread t = new Thread(() => {
                Stopwatch sw = new Stopwatch();
                sw.Start();
                var connectionStr = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString1"].ToString();
                SqlConnection sqlConnection1 = new SqlConnection(connectionStr);

                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "update PRA022_MeetAlertRule set AnalysisCompleteMark = 'N' where AlertCode = 'a943c'  and AlertTime = " + time + " exec sp_SUV_IntradayAnalysis_n943C " ;



                cmd.Connection = sqlConnection1;
                cmd.CommandTimeout = int.MaxValue;
                
                sqlConnection1.Open();
                try { 
                    lock(lck)
                    { run++;  Console.WriteLine("以運行" + run); }
                    var dr = cmd.ExecuteReader();
                
                    DataTable dt = new DataTable();
                    dt.Load(dr);
                    sqlConnection1.Close();
                    sw.Stop();
                    Console.WriteLine(time + " Done;");
                    var r = dic[time];
                    r.spTime = sw.ElapsedMilliseconds / 1000;
                    r.msg = dt.Rows[0]["ReturnValue"].ToString();
                    r.rtncode = dt.Rows[0]["ReturnValue"].ToString();
                }
                catch (Exception ex)
                {
                    

                    Console.WriteLine($"{DateTime.Now.ToString("hh:MM:ss.fff")} AltertTime [{time}], {ex.Message}");
                    sw1.WriteLine($"{DateTime.Now.ToString("hh:MM:ss.fff")} AltertTime [{time}], {ex.Message}");

                    dic[time].spTime = -2;
                    error++;
                }
            });

            t.Start();
        }

        static void GetAlert(string sql)
        {
            
            SqlConnection sqlConnection1 = new SqlConnection(connectionStr);

            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = sql; 


            cmd.Connection = sqlConnection1;

            sqlConnection1.Open();
            var dr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(dr);
            foreach (DataRow row in dt.Rows)
            {
                Console.WriteLine(row["AlertTime"]);
                dic.TryAdd(row["AlertTime"].ToString(), new result());
            }


            sqlConnection1.Close();
            
        }
    }
}
