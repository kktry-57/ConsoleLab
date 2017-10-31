using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLL
{
    public static class Class1
    {
        static public void A(long size)
        {
            var array = new byte[size];
            Console.WriteLine("DLL done size:" + size.ToString("###,###,###"));
        }
    }
}
