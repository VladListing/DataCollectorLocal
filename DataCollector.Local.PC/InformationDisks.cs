using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace DataCollector.Local.PC
{
    internal class InformationDisks
    {
        //DataContext db = new DataContext(@"D:\_LISTING_\GIT\Prod\DataCollectorLocal\DataCollector.Local.PC\DataLocal.mdf");

        private DateTime _localDateTime = DateTime.Now;

        private string _MachineName = null;
        private string _DriveName = null;
        private string _DriveType = null;
        private string _VolumeLabel = null;
        private string _DriveFormat = null;
        private double _DriveTotalSize = 0.0;
        private double _DriveTotalFreeSize = 0.0;
        

        public void ScanningDisks()
        {
            var allDrives = DriveInfo.GetDrives();

            foreach (var d in allDrives)
            {
                   _MachineName = Environment.MachineName;
                   _DriveName = d.Name;
                   _DriveType = d.DriveType.ToString();
                   
                Console.WriteLine("{0}", _localDateTime.ToString(CultureInfo.CurrentCulture));
                Console.WriteLine("MachineName: {0}", Environment.MachineName);
                Console.WriteLine("Drive {0}", d.Name);
                Console.WriteLine("  Drive type: {0}", d.DriveType);

                if (d.IsReady == true)
                {
                           _VolumeLabel = d.VolumeLabel;
                           _DriveFormat = d.DriveFormat;
                           _DriveTotalSize = d.TotalSize / 1048576.0;
                           _DriveTotalFreeSize = d.TotalFreeSpace / 1048576.0;

                    Console.WriteLine("  Volume label: {0}", d.VolumeLabel);
                    Console.WriteLine("  File system: {0}", d.DriveFormat);
                    Console.WriteLine("  Total size of drive:{0:0.##} Mb ",d.TotalSize/ 1048576.0);
                    Console.WriteLine("  Total available space:{0:0.##} Mb",d.TotalFreeSpace / 1048576.0);

                    Console.WriteLine();
                }



            }
            Console.ReadLine();
        }
    }
}
