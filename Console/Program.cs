
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Console1
{
    class Program
    {
        static void Main(string[] args)
        {
            //Class1 a = new Class1();
            //Console.WriteLine(a.Add(1,2));
            //Console.Read();

            Assembly asm = Assembly.LoadFrom(@"C:\Users\KK\Documents\visual studio 2015\Projects\ClassLibrary1\ClassLibrary1\bin\Debug\ClassLibrary1.dll");
            //Assembly asm = Assembly.LoadFrom(@".\ClassLibrary2.dll");
            Type type = asm.GetType("ClassLibrary1.Class1");
            MethodInfo mth = type.GetMethod("Add");
            object obj = asm.CreateInstance(type.FullName);
            int i = (int)mth.Invoke(obj, new object[] { 1,4});
            Console.WriteLine(i);
            Console.Read();
           
        }
    }
}
