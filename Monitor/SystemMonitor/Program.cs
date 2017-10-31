using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SystemMonitor
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                SysInfo info = OpenHardwardMonitors.GetSystemInfo();
                var jason = JsonConvert.SerializeObject(info);
                Console.WriteLine(jason);
                //System.Xml.XmlDocument xml = JsonConvert.DeserializeXmlNode(jason);
                foreach (var cpu in info.CPU)
                {
                    Console.WriteLine($"CPU NAME [{cpu.Name}]");
                    foreach (var core in cpu.Cores)
                    {
                        Console.WriteLine($"CPU NAME [{core.Name}], Usage [{core.UsageOfCPU}], Tempreature [{core.Temperature}]");
                        Console.WriteLine("---------------------------------------------------");
                    }

                    Console.WriteLine("======================================================");
                }
                foreach(var hd in info.HD_Physic)
                {
                    Console.WriteLine($"HD NAME [{hd.Name}], Usage [{hd.UsageOfSpace}], Tempreature [{hd.Temperature}]");
                    Console.WriteLine("======================================================");
                    
                }

                foreach (var hd in info.HD_Logical)
                {
                    Console.WriteLine($"HD NAME [{hd.Name}], Usage [{hd.UsageOfSpace}], Total Size [{hd.MaxSpace}]");
                    Console.WriteLine("======================================================");

                }
                Thread.Sleep(2000);
            }
            Console.ReadKey();
        }
    }
}
