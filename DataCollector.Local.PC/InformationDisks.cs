using System;
using System.IO;
using System.Timers;
using DataCollector.Local.PC.DAL;
using NLog;
using NPoco;
using System.Collections.Generic;

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
            //List<DiskStateRecord> selectionLastSession;

            try
            {
                var path = _settings.PachBd;

                using (IDatabase db = new Database($"Data Source={path}; Version=3;", DatabaseType.SQLite))
                {
                    var selectionLastSession = db.Query<DiskStateRecord>().Where(x => x.Session == _counterSession)
                        .ToList();

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
                }

                Logger.Info("selected records to send to the server:" + e.SignalTime);
            }
            catch (Exception ex)
            {
                Logger.Error(ex, "metod GetSelectionToSend() error");
            }

        }
    }
}
