﻿using System;
using NPoco;

namespace DataCollector.Local.PC.DAL
{
    [TableName("DisksPc")]
    public class DiskStateRecord
    {
        public int Id { get; set; }
        [Column("localDateTime")]
        public DateTime DateTime { get; set; }
        public string MachineName { get; set; }
        public string DriveName { get; set; }
        public string DriveType { get; set; }
        public string VolumeLabel { get; set; }
        public string DriveFormat { get; set; }
        [Column("DriveTotalSize")]
        public double TotalSize { get; set; }
        [Column("DriveTotalFreeSize")]
        public double FreeSize { get; set; }

        public bool IsArchived { get; set; }

        public override string ToString()
        {
            return
                $"ID:{Id}; Time:{DateTime}; Machine:{MachineName}; Drive:{DriveName}; Free Size: {FreeSize}/{TotalSize}";
        }
    }
}