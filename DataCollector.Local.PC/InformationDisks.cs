using System;
using System.Globalization;
using System.IO;
using System.Data.SQLite;
using System.Timers;
using DataCollector.Local.PC.DAL;
using NLog;

namespace DataCollector.Local.PC
{
    internal class InformationDisks
    {
        private static readonly ILogger Logger = LogManager.GetLogger(nameof(InformationDisks));
        private readonly MySettings _settings;
        private readonly IRepository _repository;

        public InformationDisks(MySettings settings, IRepository repository)
        {
            _settings = settings;
            _repository = repository;
        }

        public void Timer()
        {
            var mTimer = new Timer { Interval = _settings.Interval }; 
            mTimer.Elapsed += ScanningDisks;
            mTimer.AutoReset = true;
            mTimer.Enabled = true;
        }
        
        private void ScanningDisks(object sender, ElapsedEventArgs e)
        {
            try
            {
                var allDrives = DriveInfo.GetDrives();


                foreach (var d in allDrives)
                {
                    if (d.IsReady != true) continue;

                    var stateRecord = d.ToDiskStateRecord();

                    _repository.AddRecord(stateRecord);
                }

                Logger.Info("added an entry to the database at:" + e.SignalTime);

            }
            catch (Exception ex)
            {
                Logger.Error(ex, "Disks scanning error");
            }
        }

    }
}
