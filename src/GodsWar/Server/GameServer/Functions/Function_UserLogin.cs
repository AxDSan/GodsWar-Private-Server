using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameServer
{
    public partial class Systems
    {
        public enum Reason
        {
            BANNED = 2,
            AUTH_FAILED = 0
        }

        public enum AuthenticationStatus
        {
            BANNED = -1,
            OK = 1,
            ERROR = 2
        }

        public static int UserLogin(string _username, string _password, string _mac="")
        {
            int resp = (int)AuthenticationStatus.OK;
            SQLResult result = Program._SQL.Select("SELECT * FROM users WHERE username=? AND password=?", _username, _password);
            if (result.Count != 0)
            {
                var player_role = result.ReadAllValuesFromField("role")[0].ToString();
                if (int.Parse(player_role) == (int)AuthenticationStatus.BANNED)
                {
                    resp = (int)AuthenticationStatus.BANNED;
                }
            }
            else
            {
                // ERROR!
                resp = (int)AuthenticationStatus.ERROR;
            }
            return resp;
        }

        public static byte[] UserFail(byte _code)
        {
            PacketWriter pack = new PacketWriter();
            pack.Create(0x03);
            pack.Byte(0x00);
            pack.Byte(_code);
            return pack.GetBytes();
        }

        public static byte[] UserFail(byte _code, Reason _reason)
        {
            PacketWriter pack = new PacketWriter();
            pack.Create(0x03);
            pack.Byte((byte)_reason);
            pack.Byte(_code);
            return pack.GetBytes();
        }
    }
}
