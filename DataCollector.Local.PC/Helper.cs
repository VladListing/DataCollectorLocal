using System;
using System.IO;
using DataCollector.Local.PC.DAL;

namespace DataCollector.Local.PC
{
    static class Helper
    {
        const double MBdivider = 1048576.0;
        public static DiskStateRecord ToDiskStateRecord(this DriveInfo driveInfo, int s)
        {
            var result = new DiskStateRecord();
            result.DateTime = DateTime.Now;
            result.MachineName = Environment.MachineName;
            result.Session = s;
            result.DriveName = driveInfo.Name;
            result.DriveType = driveInfo.DriveType.ToString();
            result.VolumeLabel = driveInfo.VolumeLabel;
            result.DriveFormat = driveInfo.DriveFormat;
            result.TotalSize = driveInfo.TotalSize / MBdivider;
            result.FreeSize = driveInfo.TotalFreeSpace / MBdivider;

            return result;
        }
    }
}