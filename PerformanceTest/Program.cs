using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
//using TWSE.SUV3.DBLibrary;

namespace PerformanceTest
{
    class Program
    {
        static void Main(string[] args)
        {

            int a = 0;
            Console.WriteLine($"a = {a}");
            PerformanceCounter cpu = new PerformanceCounter("Processor Information", "% Processor Time", "_Total");

            while (true)
            {
                Console.WriteLine(cpu.NextValue());
                Thread.Sleep(1000);
            }
            Console.Read();
        }

        //static void TypeAddTest()
        //{
        //    int count = 100000000;
        //    Stopwatch sw = new Stopwatch();
        //    sw.Start();
        //    Console.WriteLine("Fist");
        //    for (int i = 1; i < count; i++)
        //    {
        //        Int64 x = 5;
        //        Int64 y = 1;
        //        Int64 z = 10;
        //        decimal r = (x + y) / z;
        //        //Console.WriteLine(r);
        //    }
        //    sw.Stop();
        //    Console.WriteLine("First test using time [" + sw.ElapsedMilliseconds + " ms]");

        //    sw.Reset();
        //    sw.Start();
        //    Console.WriteLine("Second");
        //    for (int i = 1; i < count; i++)
        //    {
        //        Int64 x = 5;
        //        Int64 y = 1;
        //        Int64 z = 10;
        //        decimal r = (decimal)(x - y) / z;
        //        //Console.WriteLine(r);
        //    }
        //    sw.Stop();
        //    Console.WriteLine("Second test using time [" + sw.ElapsedMilliseconds + " ms]");

        //    sw.Reset();
        //    sw.Start();
        //    Console.WriteLine("Third");
        //    for (int i = 1; i < count; i++)
        //    {
        //        Int64 x = 5;
        //        Int64 y = 1;
        //        Int64 z = 10;
        //        decimal r = (x - y) / (decimal)z;
        //        //Console.WriteLine(r);
        //    }
        //    sw.Stop();
        //    Console.WriteLine("Third test using time [" + sw.ElapsedMilliseconds + " ms]");

        //    sw.Reset();
        //    sw.Start();
        //    Console.WriteLine("Second");
        //    for (int i = 1; i < count; i++)
        //    {
        //        decimal x = 5;
        //        decimal y = 1;
        //        decimal z = 10;
        //        decimal r = (x - y) / z;
        //        //Console.WriteLine(r);
        //    }
        //    sw.Stop();
        //    Console.WriteLine("Second test using time [" + sw.ElapsedMilliseconds + " ms]");

        //    sw.Reset();
        //    sw.Start();
        //    Console.WriteLine("Second");
        //    for (int i = 1; i < count; i++)
        //    {
        //        decimal x = 5;
        //        decimal y = 1;
        //        decimal z = 10;
        //        var r = (x - y) / z;
        //        //Console.WriteLine(r);
        //    }
        //    sw.Stop();
        //    Console.WriteLine("Second test using time [" + sw.ElapsedMilliseconds + " ms]");

        //}

        //static void RecordTest()
        //{
        //    Record record = new Record();
        //    record["X"] = new RecordField(RecordFieldType.INT64, 100);
        //    record["Y"] = new RecordField(RecordFieldType.INT64, 200);


        //    int count = 100000000;
        //    Stopwatch sw = new Stopwatch();
        //    Console.WriteLine("Fist");
        //    sw.Start();            
        //    for (int i = 0; i < count; i++)
        //    {                
        //        var x = record["X"];
        //        var y = record["Y"];
        //        var z = x / (decimal)y;
        //        //Console.WriteLine(z);
        //    }
        //    sw.Stop();
        //    Console.WriteLine("First test using time [" + sw.ElapsedMilliseconds + " ms]");

        //    Console.WriteLine("Second");

        //    sw.Reset();
        //    sw.Start();
        //    for (int i = 0; i < count; i++)
        //    {
        //        var x = record.Get<Int64>("X");
        //        var y = record.Get<decimal>("Y");
        //        var z = x / y;
        //        //Console.WriteLine(z);
        //    }
        //    sw.Stop();
        //    Console.WriteLine("Second test using time [" + sw.ElapsedMilliseconds + " ms]");


        //    Console.WriteLine("Third");
        //    sw.Reset();
        //    sw.Start();
        //    for (int i = 0; i < count; i++)
        //    {                
        //        var x = record.Get<Int64>("X");
        //        var y = record.Get<Int64>("Y");
        //        var z = x / (decimal)y;
        //        //Console.WriteLine(z);
        //    }
        //    sw.Stop();
        //    Console.WriteLine("Third test using time [" + sw.ElapsedMilliseconds + " ms]");

        //    Console.WriteLine("Fourth");

        //    sw.Reset();
        //    sw.Start();
        //    for (int i = 0; i < count; i++)
        //    {
        //        //Console.WriteLine(string.Format("VALUE = {0}", record["X"]));
        //        var x = record.Get<decimal>("X");
        //        var y = record.Get<decimal>("Y");
        //        var z = x / y;
        //        //Console.WriteLine(z);
        //    }
        //    sw.Stop();
        //    Console.WriteLine("Fourth test using time [" + sw.ElapsedMilliseconds + " ms]");
        //}

    }
}
