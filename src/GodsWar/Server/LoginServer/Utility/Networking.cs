using System;
using System.Net;
using System.Net.Sockets;
using LoginServer.Utility;

namespace LoginServer
{
    public partial class Systems
    {
        public static int MAX_BUFFER = 8192;
        public class Server
        {
            public delegate void dReceive(Decode de);
            public delegate void dConnect(ref object de, Client net);
            public delegate void dError(Exception ex);
            public delegate void dDisconnect(object o);

            public event dConnect OnConnect;
            public event dError OnError;

            Socket serverSocket;

            public void Start(string ip, int PORT)
            {
                IPAddress myIP = IPAddress.Any;
                if (ip != "")
                {
                    myIP = IPAddress.Parse(ip);
                }
                IPEndPoint EndPoint = new IPEndPoint(myIP, PORT);
                
                serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

                try
                {
                    serverSocket.Bind(EndPoint);
                    serverSocket.Listen(5);
                    serverSocket.BeginAccept(new AsyncCallback(ClientConnect), null);
                }
                catch (SocketException ex)
                {
                    if (ex.ErrorCode == 10049)
                    {
                    }
                    else if (ex.ErrorCode == 10048)
                    {
                    } 
                    else 
                    {
                    }
                }
                catch (Exception ex)
                {
                    OnError(ex);
                }
                finally { }
            }

            private void ClientConnect(IAsyncResult ar)
            {
                try
                {
                    Socket wSocket = serverSocket.EndAccept(ar);
                    wSocket.DontFragment = false;


                    object p = null;
                    Client client = new Client();
                    try
                    {
                        OnConnect(ref p, client);
                    }
                    catch (Exception)
                    {
                        
                    }

                    client.Packets = p;
                    client.clientSocket = wSocket;

                    serverSocket.BeginAccept(new AsyncCallback(ClientConnect), null);
                    try
                    {
                        wSocket.BeginReceive(client.tmpbuf, 0, client.tmpbuf.Length, SocketFlags.None, new AsyncCallback(client.ReceiveData), wSocket);
                    }
                    catch (SocketException){}
                    catch (Exception)
                    {
                    }
                    
                }
                catch (ObjectDisposedException)
                {
                }
                catch (Exception ex)
                {
                    OnError(ex);
                }

            }
        }
        public class Client
        {
            public delegate void dReceive(Decode de);
            public delegate void dDisconnect(object o);

            public static event dReceive OnReceiveData;
            public static event dDisconnect OnDisconnect;
            public Socket clientSocket;

            public object Packets { get; set; }
            public int bufCount = 0;
            public byte[] buffer = new byte[MAX_BUFFER];
            public byte[] tmpbuf = new byte[255];
            public int Version = 0;

            public void ReceiveData(IAsyncResult ar)
            {

                Socket wSocket = (Socket)ar.AsyncState;
                try
                {
                    if (wSocket.Connected)
                    {
                        int recvSize = wSocket.EndReceive(ar);  // get the count of received bytes

                        bool checkData = true;
                        if (recvSize > 0)
                        {
                            if ((recvSize + bufCount) > MAX_BUFFER)  // that may be a try to force buffer overflow, we don't allow that ;)
                            {
                                checkData = false;
                                LocalDisconnect(wSocket);
                            }
                            else
                            {  // we have something in input buffer and it is not beyond our limits
                                Buffer.BlockCopy(tmpbuf, 0, buffer, bufCount, recvSize); // copy the new data to our buffer
                                bufCount += recvSize; // increase our buffer-counter
                            }
                        }
                        else
                        {   // 0 bytes received, this should be a disconnect
                            checkData = false;
                            LocalDisconnect(wSocket);
                        }

                        while (checkData) // repeat while we have data
                        {
                            checkData = false;
                            if (bufCount >= 4) // a minimum of 4 byte is required for us
                            {
                                byte[] newtmp = new byte[bufCount];
                                Buffer.BlockCopy(buffer, 0, newtmp, 0, bufCount);
                                LogDebug.HexDump(newtmp, 16, true, true);
                                Decode de = new Decode(buffer); // only get the size first.
                                Decode tmp_de = new Decode(tmpbuf);
                                LogConsole.Show("TMP BUFFER OPCODE: {0}", tmp_de.opcode);
                                LogConsole.Show("bufCount: {0} dataSize: {1}", bufCount, de.dataSize);
                                if (bufCount >= (de.dataSize - 2))  // It's a complete packet. Call the handler.
                                {
                                    de = new Decode(wSocket, de.tempbuff, this, Packets);  // build up the Decode structure for next step
                                    OnReceiveData(de); // call the handling routine
                                    bufCount -= (de.dataSize); // decrease buffer-counter
                                    if (bufCount >= 0) // was the buffer greater than what the packet needs? then it may be the next packet.
                                    {
                                        Buffer.BlockCopy(buffer, 2 + de.dataSize, buffer, 0, bufCount); // move the rest to buffer start
                                        checkData = true; // loop for next packet
                                    }
                                }
                                else
                                {
                                    byte[] _tempddd = new byte[bufCount];
                                    EncDec c = new EncDec();
                                    byte[] dddxx = c.Crypt(buffer);
                                    Buffer.BlockCopy(dddxx, 0, _tempddd, 0, de.dataSize);    
                                    LogConsole.Show("bufCount: {0} dataSize: {1}", bufCount, de.dataSize);
                                    LogDebug.HexDump(_tempddd, 16, true, true);
                                }
                                de = null;
                            }
                        }
                        // start the next async read
                        if (wSocket.Connected)
                        {
                            wSocket.BeginReceive(tmpbuf, 0, tmpbuf.Length, SocketFlags.None, new AsyncCallback(ReceiveData), wSocket);
                        }
                    }
                    else
                    {
                        LocalDisconnect(wSocket);
                    }
                }
                catch (SocketException)  // explicit handling of SocketException
                {
                    LocalDisconnect(wSocket);
                }
                catch (Exception ex) // other exceptions
                {
                    LogConsole.Show("Exception Occurred! {0}", ex.Message);
                    LocalDisconnect(wSocket);
                }
            }

            public void Send(byte[] buff)
            {
                try
                {
                    if (buff!=null && buff.Length>0 && clientSocket.Connected)
                    {
                        clientSocket.Send(buff);
                    }
                }
                catch (Exception)
                {
                }
            }
            public void SendC(byte[] buff)
            {
                try
                {
                    if (buff != null && buff.Length > 0 && clientSocket.Connected)
                    {
                        EncDec EncDec = new Utility.EncDec();
                        clientSocket.Send(EncDec.Crypt(buff));
                        EncDec = null;
                    }
                }
                catch (Exception)
                {
                }
            }

            void LocalDisconnect(Socket s)
            {
                if (s != null)
                {
                    try
                    {
                        if (OnDisconnect != null)
                        {
                            OnDisconnect(this.Packets);
                        }
                    }
                    catch (Exception)
                    {
                    }
                }
            }

            public void Disconnect(Socket s)
            {
                if (s.Connected)
                {
                    s.Shutdown(SocketShutdown.Both);
                    s.Disconnect(true);
                    s.Close();
                }
            }
        }
    }
}
