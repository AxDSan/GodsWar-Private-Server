using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LoginServer.Global
{
    public static class Versions
    {
        public static string appVersion = "1.0.0";
        public static int clientVersion = 920;
    }

    public static class Network
    {
        public static string LocalIP = "";
        public static bool multihomed = false;
    }
    public static class Product
    {
        public static string Productname = "SERVER";
        public static string Homepage = "http://www.xfsgames.com.ar";
        public static string Prefix = "[SERVER]: ";
    }
}
