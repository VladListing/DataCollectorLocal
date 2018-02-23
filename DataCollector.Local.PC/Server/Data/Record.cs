using System;

namespace Server.Data
{
    public class Record
    {
        public int Id { get; set; }
        public DateTime DateTime { get; set; }
        public string MachineName { get; set; }
        public string DriveName { get; set; }
        public string DriveType { get; set; }
        public string VolumeLabel { get; set; }
        public string DriveFormat { get; set; }
        public double TotalSize { get; set; }
        public double FreeSize { get; set; }

        public bool IsArchived { get; set; }

        public override string ToString()
        {
            return
                $"ID:{Id}; Time:{DateTime}; Machine:{MachineName}; Drive:{DriveName}; Free Size: {FreeSize}/{TotalSize}";
        }
    }
}