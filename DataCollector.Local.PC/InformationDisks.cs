using System;
using System.Globalization;
using System.IO;
using System.Data.SQLite;
using System.Timers;
using NLog;

namespace DataCollector.Local.PC
{
    internal class InformationDisks
    {
        public void Timer()
        {
            var mTimer = new Timer { Interval = 30000 }; 
            mTimer.Elapsed += ScanningDisks;
            mTimer.AutoReset = true;
            mTimer.Enabled = true;
        }
        
        public void ScanningDisks(object sender, ElapsedEventArgs e)
        {
            try
            {
                
                var localDateTime = DateTime.Now.ToString(CultureInfo.CurrentCulture);
                var allDrives = DriveInfo.GetDrives();
                var settings = MySettings.Load();

                //connection to base SQLite
                var databaseName = settings.PachBd;
                var connection = new SQLiteConnection($"Data Source={databaseName};");
                connection.Open();

                foreach (var d in allDrives)
                {
                    const double mBdivider = 1048576.0;
                    var machineName = Environment.MachineName;
                    var driveName = d.Name;
                    var driveType = d.DriveType.ToString();

                    if (d.IsReady != true) continue;
                    var volumeLabel = d.VolumeLabel;
                    var driveFormat = d.DriveFormat;
                    var driveTotalSize = d.TotalSize / mBdivider;
                    var driveTotalFreeSize = d.TotalFreeSpace / mBdivider;

                    //write in base SQLite
                    var command = new SQLiteCommand(
                        "INSERT INTO 'DisksPc' ( 'LocalDateTime', 'MachineName', 'DriveName', 'DriveType', 'VolumeLabel', 'DriveFormat', 'DriveTotalSize', 'DriveTotalFreeSize')  " +
                        "VALUES ( '" + localDateTime + "' , '" + machineName + "', '" + driveName + "','" + driveType +
                        "' ,'" + volumeLabel + "', '" + driveFormat + "', '" + driveTotalSize + "', '" +
                        driveTotalFreeSize + "');", connection);
                    command.ExecuteNonQuery();
                }

                connection.Close();

                var logger = LogManager.GetCurrentClassLogger();
                logger.Info("added an entry to the database at:" + e.SignalTime);

            }
            catch (Exception ex)
            {
                var logger = LogManager.GetCurrentClassLogger();
                logger.Error(ex.Message);


                //logger.Debug("debug message");
                //logger.Info("info message");
                //logger.Warn("warn message");
                //logger.Error("error message");
                //logger.Fatal("fatal message");
            }

        }
    }
}
