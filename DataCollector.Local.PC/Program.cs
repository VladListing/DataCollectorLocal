using NLog;
using System;
using DataCollector.Local.PC.DAL;
using DataCollector.Local.PC.Services;

namespace DataCollector.Local.PC
{
    internal class Program
    {
        private static void Main()
        {
            Console.SetWindowSize(150, 50);

            var logger = LogManager.GetCurrentClassLogger();
            logger.Trace("program start");

            try
            {
                var settings = SettingsLoader<MySettings>.Load();

                IDbFactory dbFactory = new DbFactory(settings);
                IRepository repository = new Repository(dbFactory);
                ISender sender = new Sender(settings);
            
                var informationDisks = new InformationDisks(settings, sender, repository);
                informationDisks.Timer();
                informationDisks.TimerSelectionForTheServer();
            }
            catch (Exception ex)
            {
                logger.Error(ex);
            }
            
            Console.ReadKey(true);
            logger.Trace("program stop");
        }
    }
}
