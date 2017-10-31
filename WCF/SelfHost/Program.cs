using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.Text;
using System.Threading.Tasks;

namespace SelfHost
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("Start WCF Server");
                //Test1();
                TCPBinding();
            }
            finally
            {
                Console.Read();
            }


        }

        /// <summary>
        /// 同一服務在Server端同時透過程式與config定義address，最終會以config為準
        /// </summary>
        static void Test1()
        {
            //config by program
            Uri baseAddress = new Uri("http://localhost:9000/HelloService");
            ServiceHost host_configbyprogram = new ServiceHost(typeof(HelloWorldService), baseAddress);
            // Enable metadata publishing.
            ServiceMetadataBehavior smb = new ServiceMetadataBehavior();
            smb.HttpGetEnabled = true;
            smb.MetadataExporter.PolicyVersion = PolicyVersion.Policy15;
            host_configbyprogram.Description.Behaviors.Add(smb);
            host_configbyprogram.Open();

            //ServiceHost confighost = new ServiceHost(typeof(HelloWorldService));
            //confighost.Open();

            //Console.WriteLine("The service is ready at {0}", baseAddress);
            Console.WriteLine("Press <Enter> to stop the service.");
            Console.ReadLine();

            // Close the Servicehost_configbyprogram.
            //host_configbyprogram.Close();
        }


        /// <summary>
        /// 使用NETTCP Binding
        /// </summary>
        static void TCPBinding()
        {
            try
            { 
                Console.WriteLine("NET TCP Binding");
                ServiceHost confighost = new ServiceHost(typeof(HelloWorldService));            
                confighost.Open();
            }catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
