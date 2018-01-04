using NLog;
using System;

namespace DataCollector.Local.PC
{
    internal class Program
    {
        private static void Main()
        {
            var logger = LogManager.GetCurrentClassLogger();
            logger.Trace("program start");

            try
            {
                
                var createBd = new CreateBd();
                createBd.Init();
            
                var informationDisks = new InformationDisks();
                informationDisks.Timer();

            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
            }
            
            Console.ReadKey(true);
            logger.Trace("program stop");
        }
    }
}
