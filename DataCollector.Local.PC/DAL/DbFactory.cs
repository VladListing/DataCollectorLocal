using System;
using System.Data;
using System.Data.Common;
using System.Data.SQLite;
using System.IO;
using NLog;
using NPoco;

namespace DataCollector.Local.PC.DAL
{
    class DbFactory : IDbFactory
    {
        private readonly MySettings _settings;

        public DbFactory(MySettings settings)
        {
            _settings = settings;
            Init();
        }

        private void Init()
        {
            try
            {
                var path = _settings.PachBd;

                if (File.Exists(path))
                {
                    //SQLiteConnection.(path);
                    File.Delete(path);
                }

                if (!File.Exists(path))
                {
                    SQLiteConnection.CreateFile(path);
                }

                var factory = (SQLiteFactory) DbProviderFactories.GetFactory("System.Data.SQLite");
                using (var connection = (SQLiteConnection) factory.CreateConnection())
                {
                    if (connection == null) return;
                    connection.ConnectionString = $"Data Source={path}; Version=3;";
                    connection.Open();

                    using (var command = new SQLiteCommand(connection))
                    {
                        command.CommandText =

                            @"CREATE TABLE IF NOT EXISTS DisksPc(Id INTEGER PRIMARY KEY AUTOINCREMENT, 
                      localDateTime TEXT(10), 
                      MachineName TEXT(10),
                      Session INT(10),
                      DriveName TEXT(10), 
                      DriveType TEXT(10), 
                      VolumeLabel TEXT(10), 
                      DriveFormat TEXT(10), 
                      DriveTotalSize REAL(10, 1), 
                      DriveTotalFreeSize REAL(10, 1))";

                        command.CommandType = CommandType.Text;
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                var logger = LogManager.GetLogger(nameof(DbFactory));
                logger.Error(ex, "Db initialization error");
                throw;
            }
        }

        public IDatabase CreateDatabase()
        {
            return new Database(ConnectionString(), DatabaseType.SQLite);
        }

        private string ConnectionString()
        {
            return $"Data Source={_settings.PachBd}; Version=3;";
        }
    }
}