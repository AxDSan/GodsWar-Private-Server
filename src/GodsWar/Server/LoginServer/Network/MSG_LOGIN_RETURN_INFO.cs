using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoginServer.Network
{
    public class MSG_LOGIN_RETURN_INFO : Packet
    {
        public MsgHead Head;
        public byte ucInfo;   /*0: el ID no está registrado; 
                         1: el inicio de sesión es exitoso; 
                         2: inicio de sesión repetido; 
                         3: error de contraseña; 
                         4: error de versión*/
    }
}
