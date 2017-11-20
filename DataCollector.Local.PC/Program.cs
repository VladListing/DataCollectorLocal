using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataCollector.Local.PC
{
    internal class Program
    {
        private static void Main(string[] args)
        {
               var informationDisks = new InformationDisks();
            informationDisks.ScanningDisks();
        }
    }
}
