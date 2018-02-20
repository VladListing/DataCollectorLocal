using System;
using System.IO;
using System.Timers;
using DataCollector.Local.PC.DAL;
using NLog;
using NPoco;
using System.Net;
using System.Text;
using System.Collections.Generic;
using System.Linq;

namespace DataCollector.Local.PC
{
    internal class InformationDisks
    {
        private static readonly ILogger Logger = LogManager.GetLogger(nameof(InformationDisks));
        private readonly MySettings _settings;
        private readonly IRepository _repository;

        private int _counterSession;

        //delegate  List<DiskStateRecord> GetSelectionToSend(); // 1. Объявляем делегат

        public InformationDisks(MySettings settings, IRepository repository)
        {
            _settings = settings;
            _repository = repository;
        }

        public void Timer()
        {
            try
            {
                var mTimer = new Timer {Interval = _settings.Interval};
                mTimer.Elapsed += ScanningDisks;
                mTimer.AutoReset = true;
                mTimer.Enabled = true;
            }
            catch (Exception ex)
            {
                Logger.Error(ex, "metod Timer() error");
            }
        }

        public void TimerSelectionForTheServer()
        {
            try
            {
                var mTimerSelection = new Timer { Interval = _settings.SelectionForServerInterval };
                mTimerSelection.Elapsed += SelectionToSend;
                mTimerSelection.AutoReset = true;
                mTimerSelection.Enabled = true;
            }
            catch (Exception ex)
            {
                Logger.Error(ex, "metod TimerSelectionForTheServer() error");
            }
        }

        //метод конвертации из LIST в Byte[]
        public byte[] ConvertListTobyteArray(List<DiskStateRecord> obj){

            Encoding encode = Encoding.ASCII;

            List<byte> listByte = new List<byte>();
            string[] ResultCollectionArray = obj.Select(i => i.ToString()).ToArray<string>();

            foreach (var item in ResultCollectionArray)
            {
                foreach (byte b in encode.GetBytes(item))
                    listByte.Add(b);
            }
            return listByte.ToArray();
        }



        private void ScanningDisks(object sender, ElapsedEventArgs e)
        {
            _counterSession = _counterSession + 1;

            try
            {
                var allDrives = DriveInfo.GetDrives();
                
                foreach (var d in allDrives)
                {
                    if (d.IsReady != true) continue;

                    var stateRecord = d.ToDiskStateRecord(_counterSession);

                    _repository.AddRecord(stateRecord);
                }

                Logger.Info("added an entry to the database at:" + e.SignalTime);

            }
            catch (Exception ex)
            {
                Logger.Error(ex, "metod ScanningDisk() error");
            }
        }


        private void SelectionToSend(object sender, ElapsedEventArgs e)
        {
            
            try
            {
                var path = _settings.PachBd;

                using (IDatabase db = new Database($"Data Source={path}; Version=3;", DatabaseType.SQLite))
                {
                    var selectionLastSession = db.Query<DiskStateRecord> ().Where(x => x.Session == _counterSession).ToList();

                    Logger.Info("selected records to send to the server:" + e.SignalTime);
                    
                    //for testing
                    foreach (DiskStateRecord c in selectionLastSession)
                    {
                        Console.Write(" " + c.Id);
                        Console.Write(" " + c.DateTime);
                        Console.Write(" " + c.MachineName);
                        Console.Write(" " + c.Session);
                        Console.Write(" " + c.DriveName);
                        Console.Write(" " + c.DriveType);
                        Console.Write(" " + c.VolumeLabel);
                        Console.Write(" " + c.DriveFormat);
                        Console.Write(" " + c.TotalSize);
                        Console.Write(" " + c.FreeSize);
                        Console.WriteLine();
                    }

                    //попытка отправка на сервер
                    byte[] postData = ConvertListTobyteArray(selectionLastSession);
                    HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://192.168.56.2:8080");
                    request.Method = "POST";
                    request.ContentLength = postData.Length;
                    Console.WriteLine("количество в массиве:" +postData.Length);
                    //using (var writer = new StreamWriter(request.GetRequestStream(), Encoding.UTF8))
                    using (var stream = request.GetRequestStream())
                    {
                        stream.Write(postData, 0, postData.Length);
                    }
                    Logger.Info("данные отправленны на сервер через запрос POST:" + e.SignalTime);








                    //using (WebClient client = new WebClient())
                    //{
                    //    //var reqparm = new System.Collections.Specialized.NameValueCollection();
                    //    //reqparm.Add("param1", "<any> kinds & of = ? strings");
                    //    //reqparm.Add("param2", "escaping is already handled");


                    //    //byte[] responsebytes = client.UploadValues("http://localhost", "POST", reqparm);

                    //    //client.UploadData("http://127.0.0.1:8080", "POST", selectionLastSession);

                    //    //string responsebody = Encoding.UTF8.GetString(responsebytes);
                    //}

                    //MyHttpClient myHttpClient = new MyHttpClient();
                    //myHttpClient.SendingData();

                }

                
            }
            catch (Exception ex)
            {
                Logger.Error(ex, "metod SelectionToSend() error");
            }

        }
    }
}
