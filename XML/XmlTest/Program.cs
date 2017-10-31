using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace XmlTest
{
    class Program
    {
        static void Main(string[] args)
        {
            var serializer = new XmlSerializer(typeof(SysAlertValue));
            StreamReader reader = new StreamReader("Setting.xml");

            SysAlertValue info = (SysAlertValue)serializer.Deserialize(reader);
            reader.Close();
            Console.WriteLine($"CPU Usage [{info.CPU.Usage}], Temperature [{info.CPU.Temperature}]");
            Console.Read();
        }
    }
}
