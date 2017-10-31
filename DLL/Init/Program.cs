using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Init
{
    class Program
    {
        static void Main(string[] args)
        {

            long size = 2073741824;            
            try
            {

                OutofMemoryBySelf(size);
                OutofMemoryByRef(size);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            Console.WriteLine("End");
            Console.Read();
        }
        /// <summary>
        /// 在這邊使用超過32位元可用記憶體
        /// </summary>
        /// <param name="size"></param>
       static  void OutofMemoryBySelf(long size)
        {
            var array = new byte[size];
            Console.WriteLine("done size:" + size.ToString("###,###,###"));
            //Console.Read();
        }
        /// <summary>
        /// 再參考元件中使用超過32位元可用記憶體
        /// </summary>
        /// <param name="size"></param>
        static void OutofMemoryByRef(long size)
        {
            DLL.Class1.A(size);
        }

    }
}
