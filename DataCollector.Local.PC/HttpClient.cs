using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Windows.Forms;

namespace DataCollector.Local.PC
{
    public class MyHttpClient
    {
        public void SendingData()
        {

            //WebClient myWebClient = new WebClient();
            //myWebClient.UploadFile("http://127.0.0.1:8080", @"D:\sender.txt");

            //MessageBox.Show("попытка отправки информации на сервер", "CollectorClient");

            //byte[] responseArray = myWebClient.UploadFile("http://127.0.0.1:8080", "POST", @"D:\sender.txt");

            // Decode and display the response.
            //Console.WriteLine("\nResponse Received.The contents of the file uploaded are:\n{0}",
            //System.Text.Encoding.ASCII.GetString(responseArray));


            //using (WebClient client = new WebClient())
            //{
            //    //var reqparm = new System.Collections.Specialized.NameValueCollection();
            //    //reqparm.Add("param1", "<any> kinds & of = ? strings");
            //    //reqparm.Add("param2", "escaping is already handled");


            //    //byte[] responsebytes = client.UploadValues("http://localhost", "POST", reqparm);

            //    byte[] responsebytes = client.UploadValues("http://127.0.0.1:8080", "POST", reqparm);

            //    string responsebody = Encoding.UTF8.GetString(responsebytes);
            //}


        }

    }
}
