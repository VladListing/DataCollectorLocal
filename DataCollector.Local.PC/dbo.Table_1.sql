CREATE TABLE [dbo].[TableInfoDisks]
(
	[Id] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY DEFAULT NEWID(), 
    [localDateTime] DATETIME NULL, 
    [MachineName] TEXT NULL, 
    [DriveName] TEXT NULL, 
    [DriveType] TEXT NULL, 
    [VolumeLabel] TEXT NULL, 
    [DriveFormat] TEXT NULL, 
    [DriveTotalSize] REAL NULL, 
    [DriveTotalFreeSize] REAL NULL
)
