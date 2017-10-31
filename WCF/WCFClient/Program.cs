using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace WCFClient
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Start WCF Cleint");
            //Test1();
            //TCPBinding();
            TCPBindingWithConfig();



        }

        /// <summary>
        /// 同一服務在Server端同時透過程式與config定義address，最終會以config為準
        /// </summary>
        static void Test1()
        {
            try
            {
                BasicHttpBinding binding = new BasicHttpBinding();
                EndpointAddress endpoint = new EndpointAddress(new Uri("http://localhost:9000/HelloService"));
                Console.WriteLine("Service Address:" + endpoint.ToString());
                ChannelFactory<Services.IHelloWorldService> chanFac = new ChannelFactory<Services.IHelloWorldService>(binding, endpoint);
                Services.IHelloWorldService clientProxy = chanFac.CreateChannel();
                Console.WriteLine(clientProxy.SayHello("Program Binding."));

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }


            try
            {
                BasicHttpBinding binding = new BasicHttpBinding();
                EndpointAddress endpoint = new EndpointAddress(new Uri("http://localhost:9001/HelloHttp"));
                Console.WriteLine("Service Address:" + endpoint.ToString());
                ChannelFactory<Services.IHelloWorldService> chanFac = new ChannelFactory<Services.IHelloWorldService>(binding, endpoint);
                Services.IHelloWorldService clientProxy = chanFac.CreateChannel();
                Console.WriteLine(clientProxy.SayHello("Program Binding."));

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                Console.Read();
            }
        }


        /// <summary>
        /// 使用NETTCP Binding
        /// </summary>
        static void TCPBinding()
        {
            try
            {
                NetTcpBinding binding = new NetTcpBinding();
                EndpointAddress endpoint = new EndpointAddress(new Uri("net.tcp://localhost:9002/HelloTcp"));
                Console.WriteLine("Service Address:" + endpoint.ToString());
                ChannelFactory<Services.IHelloWorldService> chanFac = new ChannelFactory<Services.IHelloWorldService>(binding, endpoint);
                Services.IHelloWorldService clientProxy = chanFac.CreateChannel();
                Console.WriteLine(clientProxy.SayHello("Program Binding."));

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                Console.Read();
            }
        }
        /// <summary>
        /// 使用NETTCP Binding
        /// </summary>
        static void TCPBindingWithConfig()
        {
            try
            {
                Console.WriteLine("【TCPBindingWithConfig】");                
                ChannelFactory<Services.IHelloWorldService> chanFac = new ChannelFactory<Services.IHelloWorldService>("myNetTcp");
                Services.IHelloWorldService clientProxy = chanFac.CreateChannel();
                Console.WriteLine(clientProxy.SayHello("TCPBindingWithConfig."));

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                Console.Read();
            }
        }
        
    }
}
