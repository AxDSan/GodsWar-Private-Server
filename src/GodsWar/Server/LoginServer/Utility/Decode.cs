using System;
using System.IO;
using System.Net.Sockets;
using LoginServer.Utility;

namespace LoginServer
{
    public partial class Systems
    {
        public class Decode
        {
            public UInt16 dataSize;
            public byte[] tempbuff;

            public ushort opcode;
            public byte[] buffer;
            public Socket Client;
            public object Networking;
            public object Packet;
            MemoryStream ms;
            BinaryReader br;

            public Decode(byte[] buffer)
            {
                try
                {
                    byte[] _tempbuff = new byte[buffer.Length];
                    EncDec endc = new EncDec();
                    _tempbuff = endc.Crypt(buffer);
                    ms = new MemoryStream(_tempbuff);
                    br = new BinaryReader(ms);
                    dataSize = (ushort)br.ReadInt16();
                    tempbuff = new byte[dataSize];
                    Buffer.BlockCopy(_tempbuff, 0, tempbuff, 0, dataSize);
                    br.Close();
                    ms.Close();
                    br.Dispose();
                    ms.Dispose();
                    _tempbuff = null;
                    endc = null;
                }
                catch (Exception) { }
            }

            public Decode(Socket wSock, byte[] buffer, Client net, object packetf)
            {
                try
                {
                    Packet = packetf;

                    ms = new MemoryStream(buffer);
                    br = new BinaryReader(ms);

                    dataSize = (ushort)br.ReadInt16();

                    byte[] b = new byte[dataSize-4];
                    Array.Copy(buffer, 4, b, 0, dataSize-4);

                    this.buffer = b;
                    opcode = br.ReadUInt16();

                    Client = wSock;
                    Networking = net;
                }
                catch (Exception)
                {
                }
            }
            public Decode(Socket wSock, byte[] buffer, Client net, object packetf, int size)
            {
                try
                {
                    Packet = packetf;

                    ms = new MemoryStream(buffer);
                    br = new BinaryReader(ms);

                    byte[] b = new byte[size];
                    Array.Copy(buffer, 0, b, 0, size);

                    this.buffer = b;
                    opcode = 0;

                    Client = wSock;
                    Networking = net;
                }
                catch (Exception)
                {
                }
            }
            public static string StringToPack(byte[] buff)
            {
                string s = null;
                foreach (byte b in buff) s += b.ToString("X2");
                return s;
            }
        }
    }
}
