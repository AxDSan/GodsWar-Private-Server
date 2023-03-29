using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LoginServer.Definitions
{
    class Bootlogo
    {
        public static void _Load()
        {
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.Title = "LoginServer " + Global.Versions.appVersion;
            Console.WriteLine("========================= LoginServer ============================");
            Console.WriteLine("=                         Version 0.1a                           =");
            Console.WriteLine("==================== Developed by IXD Team =======================");
            Console.ForegroundColor = ConsoleColor.Gray;
        }
    }
}
