using System;
using System.Globalization;
using System.IO;
using System.Data.SQLite;
using System.Timers;

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

            Console.WriteLine("Press the Enter key to exit the program ");
            Console.ReadLine();
        }
        
        public void ScanningDisks(object sender, ElapsedEventArgs e)
        {
            var localDateTime = DateTime.Now.ToString(CultureInfo.CurrentCulture);

            var allDrives = DriveInfo.GetDrives();
           
            //connection to base SQLite
            const string databaseName = @"D:\_LISTING_\GIT\Prod\DataCollectorLocal\DataCollector.Local.PC\bin\Debug\PcInfo.db";
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
                var command = new SQLiteCommand("INSERT INTO 'Info' ( 'LocalDateTime', 'MachineName', 'DriveName', 'DriveType', 'VolumeLabel', 'DriveFormat', 'DriveTotalSize', 'DriveTotalFreeSize')  " +
                                                          "VALUES ( '" + localDateTime + "' , '" + machineName + "', '" + driveName + "','" + driveType + "' ,'" + volumeLabel + "', '" + driveFormat + "', '" + driveTotalSize + "', '" + driveTotalFreeSize + "');", connection);
                command.ExecuteNonQuery();
            }

            connection.Close();

            Console.WriteLine("The Elapsed event was raised at {0}", e.SignalTime);
            
        }
    }
}
