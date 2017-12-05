using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace DataCollector.Local.PC
{
    internal class InformationDisks
    {
        private DateTime _localDateTime = DateTime.Now;

        private string _machineName;
        private string _driveName = null;
        private string _driveType = null;
        private string _volumeLabel = null;
        private string _driveFormat = null;
        private double _driveTotalSize = 0.0;
        private double _driveTotalFreeSize = 0.0;
        

        public void ScanningDisks()
        {
            var allDrives = DriveInfo.GetDrives();

            using (var conn = new SqlConnection())
            {
                conn.ConnectionString =
               @"Data Source=(LocalDb)\MSSQLLocalDB;Initial Catalog=.;Integrated Security=SSPI;AttachDBFilename=|DataDirectory|\DataBase.mdf";

                conn.Open();

                string strSQL = "SELECT * FROM TableInfoDisks";
                SqlCommand myCommand = new SqlCommand(strSQL, conn);
                SqlDataReader dr = myCommand.ExecuteReader();
                while (dr.Read())
                {
                    Console.WriteLine($"ID: {dr[0]}; LocalDataTime: {dr[1]}; MachineName: {dr[2]}; DriveName: {dr[3]}");
                }

                conn.Close();
            }

            foreach (var d in allDrives)
            {
                   _machineName = Environment.MachineName;
                   _driveName = d.Name;
                   _driveType = d.DriveType.ToString();
                   
                Console.WriteLine("{0}", _localDateTime.ToString(CultureInfo.CurrentCulture));
                Console.WriteLine("MachineName: {0}", Environment.MachineName);
                Console.WriteLine("Drive {0}", d.Name);
                Console.WriteLine("  Drive type: {0}", d.DriveType);

                if (d.IsReady == true)
                {
                           _volumeLabel = d.VolumeLabel;
                           _driveFormat = d.DriveFormat;
                           _driveTotalSize = d.TotalSize / 1048576.0;
                           _driveTotalFreeSize = d.TotalFreeSpace / 1048576.0;

                    Console.WriteLine("  Volume label: {0}", d.VolumeLabel);
                    Console.WriteLine("  File system: {0}", d.DriveFormat);
                    Console.WriteLine("  Total size of drive:{0:0.##} Mb ",d.TotalSize/ 1048576.0);
                    Console.WriteLine("  Total available space:{0:0.##} Mb",d.TotalFreeSpace / 1048576.0);

                    Console.WriteLine();
                }
                
                // Create a new row.
                //DataBaseDataSet.TableInfoDisksRow newTableInfoDisksRow;
                // newTableInfoDisksRow = dataBaseDataSet.TableInfoDisks.NewTableInfoDisksRow();
                //newTableInfoDisksRow._MachineName = "test";
                //newTableInfoDisksRow._DriveName = "test";

                // Add the row to the Region table
                //this.dataBaseDataSet.TableInfoDisks.Rows.Add(newTableInfoDisksRow);

                // Save the new row to the database
                //this.tableInfoDisksTableAdapter.Update(this.dataBaseDataSet.TableInfoDisks);

            }
            Console.ReadLine();
        }
    }
}
