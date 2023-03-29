using LoginServer.Definitions;
using LoginServer.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoginServer
{
    public partial class Systems
    {
        public enum Reason
        {
            BANNED = 0x02,
            AUTH_FAILED = 0x00
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

        public static bool JudgeValidStr(string name, string password)
        {
            //Construir mensaje de retorno de inicio de sesión
            MSG_LOGIN_RETURN_INFO Login_info = new MSG_LOGIN_RETURN_INFO();
            Login_info.Head.usSize = 0;
            Login_info.Head.usType = (ushort)OpCodes._MSG_LOGIN_RETURN_INFO;

            //Límite de longitud
            /*if (name.Length > MAX_ACCOUNT_SIZE || password.Length > MAX_ACCOUNT_SIZE)
            {
                Login_info.ucInfo = 0;
                //SEND
                return false;
            }*/

            //Limite de cuenta
            /*
             * FALTA VALIDAR CHAR TEXTO
             * SEND
             * */
            return true;
        }

        public static void CompleteLogin(Client client)
        {
            MSG_LOGIN_RETURN_INFO Login_info = new MSG_LOGIN_RETURN_INFO();
            Login_info.Head.usSize = 0;
            Login_info.Head.usType = (ushort)OpCodes._MSG_LOGIN_RETURN_INFO;
            Login_info.ucInfo = 0x01;
            client.SendC(Login_info.end());
        }

        public static byte[] UserFail(byte _code)
        {
            MSG_LOGIN_RETURN_INFO Login_info = new MSG_LOGIN_RETURN_INFO();

            Login_info.Head.usSize = 0;
            Login_info.Head.usType = (ushort)OpCodes._MSG_LOGIN_RETURN_INFO;

            PacketWriter pack = new PacketWriter();
            pack.Create((ushort)OpCodes._MSG_LOGIN_RETURN_INFO);
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
