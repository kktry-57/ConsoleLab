using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OperatorLab
{
    public static class Main
    {
        public static void Run()
        {
            myClass p1 = new myClass(1, 1);
            myClass p2 = new myClass(1, 1);
            Console.WriteLine(string.Format("p1 = ({0},{1})", p1.x, p1.y));
            Console.WriteLine(string.Format("p2 = ({0},{1})", p2.x, p2.y));
            Console.WriteLine(string.Format("p1 + p2 = ({0},{1})", (p1 + p2).x, (p1 + p2).y));
        }
    }
    public class myClass
    {

        public int x { get; set; }
        public int y { get; set; }
        public myClass(int _x, int _y)
        {
            x = _x;
            y = _y;
        }

        public static myClass operator +(myClass p1, myClass p2)
        {
            return new myClass(p1.x + p2.x, p1.y + p2.y);

        }

    }
}
