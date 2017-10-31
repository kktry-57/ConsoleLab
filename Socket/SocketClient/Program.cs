using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SocketClient
{
    class Program
    {
        static Socket sender;
        static CancellationTokenSource cts;
        static Queue<string> MsgToSend;
        static string ID = "";
        static void Main(string[] args)
        {
            ID = Guid.NewGuid().ToString();
            Console.WriteLine("Start Client, ID:" + ID);
            //cts = new CancellationTokenSource();
            MsgToSend = new Queue<string>();
            
            
            Connect();


            while (true)
            {
                Console.Write("輸入訊息:");
                string msg = Console.ReadLine();
                MsgToSend.Enqueue(msg);
            }



        }
        static void Connect()
        {

            if (cts != null && !cts.IsCancellationRequested)
            {
                cts.Cancel();
                Thread.Sleep(5000);
            }
            cts = new CancellationTokenSource();

Reconnect:
            try
            {
                sender = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                sender.Connect("127.0.0.1", 8000);

            }
            catch (Exception ex)
            {
                Console.WriteLine("發生錯誤" + ex.Message);
                    
                Thread.Sleep(1000);
                goto Reconnect;
            }
            Task.Factory.StartNew(HeartBeat);
            Task.Factory.StartNew(Reciver);
            Task.Factory.StartNew(Sender);

        }
        static void Reciver()
        {
            try
            {
                while (!cts.IsCancellationRequested)
                {
                    byte[] msg = new byte[1024];
                    sender.Receive(msg);
                    Console.WriteLine("接收訊息:" + Encoding.GetEncoding("big5").GetString(msg).Replace("\0", ""));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("接收發生錯誤，" + ex.Message);     
            }
        }

        static void Sender()
        {
            try
            {
                while (!cts.IsCancellationRequested)
                {
                    SpinWait.SpinUntil(() => MsgToSend.Count > 0);
                    string msg = MsgToSend.Dequeue();
                    byte[] sendingMsg = Encoding.GetEncoding("Big5").GetBytes(ID + ":" + msg);
                    Console.WriteLine(string.Format("發送訊息:{0}", msg));
                    sender.Send(sendingMsg);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("發送發生錯誤，" + ex.Message);
                Connect();
            }
        }

        static void HeartBeat()
        {
            while(!cts.IsCancellationRequested)
            {
                MsgToSend.Enqueue("HBT");
                SpinWait.SpinUntil(() => cts.IsCancellationRequested, 3000);
                
            }
        }
    }
}
