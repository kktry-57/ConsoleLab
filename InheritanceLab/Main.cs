using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InheritanceLab
{
    public static class Main
    {
        public static void Run()
        {
            Child c1 = new Child(1);
            Child c2 = new Child(2);
            Child c3 = new Child(3);

            c1.show();
            c2.show();
            c3.show();

            c2.add();
            c3.show();
            

            Console.Read();
        }
    }


    public class basic
    {
        protected int id = 0;
        protected int value = 0;
        static protected int static_value = 0;

    }

    public class Child : basic
    {
        public Child(int _id)
        {
            id = _id;
        }

        public void add()
        {
            value++;
            static_value++;
            show();
        }

        public void show()
        {
            Console.WriteLine(string.Format("id=【{0}】, value=【{1}】, static value=【{2}】", id, value, static_value));
        }
    }
}
