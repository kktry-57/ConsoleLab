using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace delegateLab
{
    static public class Main
    {
        public static void Run()
        {
            lab c = new lab();
            c.method = (int x, int y) =>
            {
                return x > y ? y : x; ;
            };
            //c.method = new lab.MethodDelegate(min);
            Console.WriteLine(c.exc(1, 2));
            Console.WriteLine(c.exc(min, 1, 2));
            Console.Read();
            
        }
        static int min(int x, int y)
        {
            return x > y ? x : y;
        }
        
    }

    public class lab
    {
        public delegate int MethodDelegate(int x, int y);
        public MethodDelegate method;
       
        public lab()
        {            
            //Console.WriteLine(method(1, 2));
        }

        private int add(int x, int y)
        {
            return x + y;
        }

        public int exc(int x, int y)
        {
            return method(x, y);
        }
        public int exc(MethodDelegate _method ,int x, int y)
        {
            return _method(x, y);
        }
    }

}
