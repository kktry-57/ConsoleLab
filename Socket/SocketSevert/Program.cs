using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Threading;

namespace SocketSevert
{
    class Program
    {
        static object newSocketLock = new object();
        static List<SocketServices> sockets = new List<SocketServices>();
        static Socket acceptor;
        static Socket server;
        static CancellationTokenSource cts;
        static Queue<string> MsgToSend;
        static string ID = "";
        static void Main(string[] args)
        {
            ID = Guid.NewGuid().ToString();
            Console.WriteLine("Start Server, ID:" + ID);
            cts = new CancellationTokenSource();
            MsgToSend = new Queue<string>();
            acceptor = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            acceptor.Bind(new IPEndPoint(IPAddress.Any, 8000));
            acceptor.Listen(0);
            Task.Factory.StartNew(Listening);

            while (true)
            {

                Console.Write("輸入對象:");
                int toClient;
                try
                {
                    toClient = int.Parse(Console.ReadLine());
                    if (toClient < -1 || toClient >= sockets.Count) throw new Exception();
                }catch(Exception ex)
                {
                    Console.WriteLine(string.Format("輸入範圍應為-1~{0}", sockets.Count-1));
                    continue;
                }

                Console.Write("輸入訊息:");
                string msg = Console.ReadLine();
                //MsgToSend.Enqueue(msg);
                if (toClient == -1)
                {
                    foreach (var item in sockets)
                    {
                        item.MsgToSend.Enqueue(msg);
                    }
                }
                else
                {
                    sockets[toClient].MsgToSend.Enqueue(msg);
                }
            }


        }
        static void Listening()
        {
            while(!cts.IsCancellationRequested)
            {
                Socket s = acceptor.Accept();
                Task.Factory.StartNew(VerifyUser, s);
                //sockets.Add(new SocketServices(ID, cts.Token, s));
                

            }
        }
        static void VerifyUser(object _socket)
        {
            var socket = _socket as Socket;
            //socket.Disconnect(false);
            lock (newSocketLock)
            {
                sockets.Add(new SocketServices(ID, cts.Token, socket));
                Console.WriteLine("建立新連線，已連線個數" + sockets.Count);
            }

        }
       
    }
    public class SocketServices
    {
        string ID;
        public Queue<string> MsgToSend;
        static CancellationToken cts;
        private Socket socket;
        public SocketServices(string _id, CancellationToken _cts, Socket _s)
        {
            cts = _cts;
            MsgToSend = new Queue<string>();
            ID = _id;
            socket = _s;
            Task.Factory.StartNew(Sender);
            Task.Factory.StartNew(Reciver);
        }
        void Reciver()
        {
            try
            {
                while (!cts.IsCancellationRequested)
                {
                    byte[] msg = new byte[1024];
                    socket.Receive(msg);
                    Console.WriteLine("接收訊息:" + Encoding.GetEncoding("big5").GetString(msg).Replace("\0", ""));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("接收發生錯誤，" + ex.Message);
            }
        }

        void Sender()
        {
            try
            {
                while (!cts.IsCancellationRequested)
                {
                    SpinWait.SpinUntil(() => MsgToSend.Count > 0);
                    string msg = MsgToSend.Dequeue();
                    byte[] sendingMsg = Encoding.GetEncoding("Big5").GetBytes(ID + ":" + msg);
                    Console.WriteLine(string.Format("發送訊息:{0}", msg));
                    socket.Send(sendingMsg);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("接收發生錯誤，" + ex.Message);
            }
        }

    }
}
