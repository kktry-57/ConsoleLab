using LineMessagingAPISDK;
using LineMessagingAPISDK.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LineCenter
{
    public class LineMessagePackage
    {
        public string to = "";
        public LineMessage messages = new LineMessage();
        public LineMessagePackage(string uid, string type, string msg)
        {
            to = uid;
            messages = new LineMessage(type, msg);
        }        
    }

    public class LineMessage
    {
        public string type = "";
        public string text = "";
        public LineMessage() { }
        public LineMessage(string _type, string _text)
        {
            type = _type;
            text = _text;
        }
    }
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }


        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                TextMessage mes = new TextMessage("HI");
                string userId = UID.Text;
                string word = MSG.Text;
                LineClient lineClient = new LineClient("kSvSpnbbrpmxsSYMXigQVmOV2OC4uAD4nMRes2FDVy06UJD9DckvawZx2zXyTF8RZWii35XrvcHd+xkgq7EDgmO++XfG+KziUiIKZWrDWLDpuTDi9nQVVOnR3Tc5TTQohJ7i3jcw+/p3XJVk4eWGnAdB04t89/1O/w1cDnyilFU=");
                PushMessage msg = new PushMessage();
                msg.To = userId;
                msg.Messages.Add(mes);
                lineClient.PushAsync(msg);
            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private string SendRequest(Uri uri, byte[] jsonDataBytes, string contentType, string method, string authorization)
        {
            WebRequest req = WebRequest.Create(uri);
            req.ContentType = contentType;
            req.Method = method;
            req.ContentLength = jsonDataBytes.Length;
            req.Headers.Add("Authorization", authorization);

            var stream = req.GetRequestStream();
            stream.Write(jsonDataBytes, 0, jsonDataBytes.Length);
            stream.Close();

            WebResponse response = req.GetResponse();
            MessageBox.Show(((HttpWebResponse)response).StatusDescription);
            stream = response.GetResponseStream();
            StreamReader reader = new StreamReader(stream);
            string responseFromServer = reader.ReadToEnd();

            return responseFromServer;
        }
    }
}
