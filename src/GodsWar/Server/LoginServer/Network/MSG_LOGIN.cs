using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoginServer.Network
{
    public class MSG_LOGIN
    {
        static int MAX_NAME = 32;
        MsgHead Head;
        char[] Name = new char[MAX_NAME];
        char[] cPassWord = new char[MAX_NAME];
        long fVersion;
    }
}
