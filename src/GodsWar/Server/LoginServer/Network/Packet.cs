using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoginServer.Network
{
    public class Packet
    {
        public byte[] end()
        {
            return new byte[1] { 0x00 };
        }
    }
}
