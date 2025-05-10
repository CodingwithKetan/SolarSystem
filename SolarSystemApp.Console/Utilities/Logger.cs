using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolarSystemApp.Console.Utilities
{
    public static class Logger
    {
        public static readonly ILog Instance = LogManager.GetLogger(typeof(Program));
    }
}
