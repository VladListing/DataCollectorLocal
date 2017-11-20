using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataCollector.Local.PC
{
    internal class InformationDisks
    {

        public void ScanningDisks()
        {
            var allDrives = DriveInfo.GetDrives();

            foreach (var d in allDrives)
            {
                Console.WriteLine("MachineName: {0}", Environment.MachineName);
                Console.WriteLine("Drive {0}", d.Name);
                Console.WriteLine("  Drive type: {0}", d.DriveType);
                if (d.IsReady == true)
                {
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
