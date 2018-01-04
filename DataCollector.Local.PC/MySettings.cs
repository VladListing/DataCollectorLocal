using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NLog;

namespace DataCollector.Local.PC
{
    class MySettings : Settings<MySettings>
    {
        public string PachBd = null;
    }
}
