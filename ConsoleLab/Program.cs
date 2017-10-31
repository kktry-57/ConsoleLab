using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleLab
{
    class Program
    {
        static void Main(string[] args)
        {
            //InheritanceLab.Main.Run(); //繼承測試
            //delegateLab.Main.Run();  //委派
            //OperatorLab.Main.Run();
            try
            {
                DateTime t1 = DateTime.Parse(@"2016-06-16 17:19:39.6146");
                DateTime t2 = DateTime.Parse(@"2016-06-16 17:21:39.7240");
                Console.WriteLine((t2-t1).TotalSeconds);

                Console.WriteLine(findCommon(8,24));

            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                Console.WriteLine("任意鍵結束......");
                Console.ReadKey();
            }
            AppDomain.CurrentDomain.ProcessExit += (sender, eargs) =>
            {
  
                Console.WriteLine("Byebye");
                Console.Read();
            };
            
            

        }
        static int findCommon(int m, int n)
        {
            while (m % n != 0)
            {
                int t = n;
                n = m % n;
                m = t;
            }
            return n;
        }

        public static void TEST()
        {
            //Dictionary<string, Dictionary<string, Dictionary<string, string>>> d1 = new Dictionary<string, Dictionary<string, Dictionary<string, string>>>();
            //Dictionary<string, string> d2 = new Dictionary<string, string>();
            //for(int i = 0; i <=100; i++)
            //{
            //    string _temp = i.ToString().PadLeft('0');
            //    d1.Add(_temp, new Dictionary<string, Dictionary<string,string>>(_temp, new Dictionary<string, string>(_temp, _temp)));
            //    d2.Add(_temp, _temp);
            //}



        }


    }
}
