using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoginServer.Network
{
    class MSG_GAMESERVER_INFO
    {
        static int MAX_NAME = 32;
        MsgHead Head;
        char[] cIP = new char[MAX_NAME];
        uint uiPort;
        byte cID;                                 //ID del servidor
        char[] ServerName = new char[MAX_NAME];   //Nombre del servidor de juego
        byte cState;                              //0: inicio; 1: inactivo; 2: ocupado; 3: lleno; 4: cerrado
    }
}
