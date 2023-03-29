using System;
using System.Linq;
using System.Text;
using System.IO;
using System.Net;
using System.Collections.Generic;

namespace LoginServer
{
    public partial class Systems
    {
        public static Dictionary<int, Systems.SRX_Serverinfo> GSList = new Dictionary<int, Systems.SRX_Serverinfo>();

        public class SRX_Serverinfo
        {
            public UInt16 id = 1;
            public UInt16 port = 6001;
            public UInt16 ipcport = 6001;
            public UInt16 maxSlots = 500;
            public UInt16 usedSlots = 0;

            public UInt16 maxSalas = 2000;
            public UInt16 usedSala = 0;

            public string name = "Loginserver";
            public string ip = "192.168.1.5";
            public string wan = "192.168.1.5";
            public string extip = "1";
            public string code = "";
            public bool lan_wan = false;

            public byte status = 0;

            public int Version = 0;

            public DateTime lastPing=DateTime.MinValue;
        }

        public static SRX_Serverinfo GetServerByEndPoint(string ip, int port)
        {
            SRX_Serverinfo GS = null;

            foreach (KeyValuePair<int, Systems.SRX_Serverinfo> GSI in GSList)
            {
                if (GSI.Value.ip == ip && GSI.Value.ipcport == port)
                {
                    GS = GSI.Value;
                }
            }
            return GS;
        }

        public static void CheckServerExpired(int seconds)
        {
        }

        public static int LoadServers(string serverFile, UInt16 defaultPort)
        {
            try
            {
                if (File.Exists(Environment.CurrentDirectory + @"\Settings\" + serverFile))
                {
                    Systems.Ini ini = new Systems.Ini(Environment.CurrentDirectory + @"\Settings\" + serverFile);
                    string[] sList = null;
                    sList = ini.GetEntryNames("SERVERS");
                    if (sList!=null && sList.Length > 0)
                    {
                        foreach (string sectname in sList)
                        {
                            string sName = ini.GetValue("SERVERS", sectname, "");
                            Systems.SRX_Serverinfo SServerInfo = new Systems.SRX_Serverinfo();
                            SServerInfo.id = Convert.ToUInt16(ini.GetValue(sName, "id", 0));
                            SServerInfo.ip = ini.GetValue(sName, "ip", "192.168.1.5");
                            SServerInfo.wan = ini.GetValue(sName, "wan", "192.168.1.5");
                            SServerInfo.name = ini.GetValue(sName, "name", sName);
                            SServerInfo.port = Convert.ToUInt16(ini.GetValue(sName, "port", defaultPort));
                            SServerInfo.ipcport = Convert.ToUInt16(ini.GetValue(sName, "ipcport", "6001"));
                            SServerInfo.code = ini.GetValue(sName, "code", "");
                            SServerInfo.lan_wan = ini.GetValue(sName, "lan_wan", "0") == "1" ? true : false;
                            SServerInfo.Version = Convert.ToInt32(ini.GetValue(sName, "version", 0));
                            if (SServerInfo.ip == "" || SServerInfo.port == 0 || SServerInfo.id == 0 || SServerInfo.ipcport == 0 || GSList.ContainsKey(SServerInfo.id))
                            {
                                LogDebug.Show("IPC: Error on Server " + sName + " in " + serverFile + ": field missing or id already in use!");
                                SServerInfo = null;
                            }
                            else
                            {
                                GSList.Add(SServerInfo.id, SServerInfo);
                            }
                        }
                    }
                    if (GSList.Count() > 0)
                    {
                        string servers = "Server";
                        if (GSList.Count > 1) servers = "Servers";
                        LogConsole.Show("Loaded " + GSList.Count() + " " + servers + " from server settings");
                    }
                    else
                    {
                        Systems.SRX_Serverinfo GServer = new Systems.SRX_Serverinfo();
                        GServer.id = 1;
                        GServer.ip = "192.168.1.5";
                        if (Global.Network.multihomed)
                        {
                            //Multihomed
                        }
                        else
                        {
                            GServer.extip = Global.Network.LocalIP;
                        }
                        GServer.name = "[SERVER] Default";
                        GServer.port = defaultPort;
                        GServer.ipcport = 6001;
                        GServer.code = "xdxdxdxdxdxd";
                        GSList.Add(GServer.id, GServer);
                    }
                    sList = null;
                    ini = null;
                    return GSList.Count();
                }
                else
                {
                    Systems.SRX_Serverinfo GServer = new Systems.SRX_Serverinfo();
                    GServer.id = 1;
                    GServer.ip = "192.168.1.5";
                    if (Global.Network.multihomed)
                    {
                        //Multihomed
                    }
                    else
                    {
                        //No servers
                        GServer.extip = Global.Network.LocalIP;
                    }
                    GServer.name = "[SERVER "+ Global.Versions.appVersion +"]";
                    GServer.port = defaultPort;
                    GServer.ipcport = 6001;
                    GServer.code = "";
                    GSList.Add(GServer.id, GServer);
                    return -1;
                }
            }
            catch (Exception ex)
            {
                LogConsole.Show("Error loading GameServer settings " + ex + "");
                return -2;
            }
        }
    }
}
