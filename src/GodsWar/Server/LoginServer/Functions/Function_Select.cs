using System;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Threading;
using LoginServer.Utility;
using LoginServer.Definitions;

namespace LoginServer
{
    public partial class Systems
    {        
        public static void HandleOpCodes(Decode decode)
        {
            try
            {
                Systems sys = (Systems)decode.Packet;
                sys.PacketInformation = decode;
                PacketReader reader = new PacketReader(sys.PacketInformation.buffer);
                LogDebug.HexDump(sys.PacketInformation.buffer, 16, true);
                LogDebug.Show("Opcode: {0}", decode.opcode);
                OpCodes opc = (OpCodes)decode.opcode;
                if (opc == OpCodes._MSG_LOGIN)
                {
                    string usernameShift = reader.String(32);
                    string passwordMd5 = reader.String(32);
                    reader.Skip(4);
                    string clientMac = reader.String(32);
                    reader.Skip(32);
                    uint unk3 = reader.UInt32();
                    StringShift shift = new StringShift();
                    string username = shift.Parser(usernameShift);
                    LogDebug.Show("username: {0}", username);
                    LogDebug.Show("password_md5: {0}", passwordMd5);
                    LogDebug.Show("MAC: {0}", clientMac);
                    LogDebug.Show("unk3: {0}", unk3);
                    int res = UserLogin(username, passwordMd5, clientMac);
                    switch (res)
                    {
                        case (int)AuthenticationStatus.OK:
                            {
                                sys.client.SendC(ServerList());
                                break;
                            }
                        case (int)AuthenticationStatus.BANNED:
                            {
                                sys.client.SendC(UserFail(0xF0, Reason.BANNED));
                                break;
                            }
                        default:
                            {
                                sys.client.SendC(UserFail(0xF0, Reason.AUTH_FAILED));
                                break;
                            }
                    }
                }
                else
                {
                    LogConsole.Show("Default Opcode: {0:X} - {1}", decode.opcode, opc);
                    LogDebug.HexDump(sys.PacketInformation.buffer, 16, true, false);
                }
            }
            catch (Exception)
            {
                // ignored
            }
        }
    }
}

