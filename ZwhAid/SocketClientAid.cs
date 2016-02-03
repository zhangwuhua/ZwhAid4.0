using System.Net;
using System.Net.Sockets;
using System.Text;

namespace ZwhAid
{
    public class SocketClientAid : ZwhAid.Model.SocketModel
    {
        public SocketClientAid(ProtocolType pt)
        {
            this.pt = pt;
            if (Pt == ProtocolType.Tcp)
            {
                socket = new Socket(IPEP.AddressFamily, SocketType.Stream, pt);
            }
            else if (Pt == ProtocolType.Udp)
            {
                socket = new Socket(IPEP.AddressFamily, SocketType.Dgram, pt);
            }
            else { }
        }

        public SocketClientAid(ProtocolType pt, IPEndPoint ipep)
        {
            this.ipep = ipep;
            this.pt = pt;
            if (Pt == ProtocolType.Tcp)
            {
                socket = new Socket(IPEP.AddressFamily, SocketType.Stream, pt);
            }
            else if (Pt == ProtocolType.Udp)
            {
                socket = new Socket(IPEP.AddressFamily, SocketType.Dgram, pt);
            }
            else { }
        }

        public SocketClientAid(ProtocolType pt, IPAddress ipAddress, int port)
        {
            this.ipAddr = ipAddress;
            this.port = port;
            ipep = new IPEndPoint(IPAddr, Port);
            this.pt = pt;
            if (Pt == ProtocolType.Tcp)
            {
                socket = new Socket(IPEP.AddressFamily, SocketType.Stream, pt);
            }
            else if (Pt == ProtocolType.Udp)
            {
                socket = new Socket(IPEP.AddressFamily, SocketType.Dgram, pt);
            }
            else { }
        }

        public SocketClientAid(ProtocolType pt, string ip, int port)
        {
            this.ip = ip;
            this.ipAddr = IPAddress.Parse(IP);
            this.port = port;
            ipep = new IPEndPoint(IPAddr, Port);
            this.pt = pt;
            if (Pt == ProtocolType.Tcp)
            {
                socket = new Socket(IPEP.AddressFamily, SocketType.Stream, pt);
            }
            else if (Pt == ProtocolType.Udp)
            {
                socket = new Socket(IPEP.AddressFamily, SocketType.Dgram, pt);
            }
            else { }
        }

        public bool Connect()
        {
            try
            {
                socket.Connect((EndPoint)IPEP);
                if (socket.Connected)
                {
                    zBool = true;
                }
            }
            catch { }

            return ZBool;
        }

        public bool Receive()
        {
            try
            {
                if (Pt == ProtocolType.Tcp) { zBool = TcpReceive(endFlag); }
                else if (Pt == ProtocolType.Udp) { zBool = UdpReceive(endFlag); }
                else { }
            }
            catch { }

            return ZBool;
        }

        public bool Receive(string endflag)
        {
            try
            {
                endFlag = endflag;
                if (Pt == ProtocolType.Tcp) { zBool = TcpReceive(endFlag); }
                else if (Pt == ProtocolType.Udp) { zBool = UdpReceive(endFlag); }
                else { }
            }
            catch { }

            return ZBool;
        }

        private bool TcpReceive(string endflag)
        {
            try
            {
                endFlag = endflag;
                receiveByte = new byte[bufferLength];
                zText = new StringBuilder();
                int length = 0;
                while (flag == 0)
                {
                    socket.ReceiveTimeout = ReceiveTimeOut;
                    length = Sockets.Receive(receiveByte, 0, receiveByte.Length, SocketFlags.None);
                    if (length > 0)
                    {
                        zText.Append(EN.GetString(receiveByte).TrimEnd('\0'));
                        if (!string.IsNullOrEmpty(EndFlag))
                        {
                            if (zText.ToString().EndsWith(EndFlag))//("\r\n\\<zwhend"))
                            {
                                receiveMsg = zText.ToString();
                                length = 0;
                                flag = byte.MaxValue;
                            }
                        }
                        else
                        {
                            receiveMsg = zText.ToString();
                            length = 0;
                            flag = byte.MaxValue;
                        }
                    }
                    else
                    {
                        receiveMsg = zText.ToString();
                        length = 0;
                        flag = byte.MaxValue;
                    }
                }
                if (!string.IsNullOrEmpty(receiveMsg))
                {
                    zBool = true;
                }
            }
            catch { }

            return ZBool;
        }

        private bool UdpReceive(string endflag)
        {
            try
            {
                endFlag = endflag;
                receiveByte = new byte[bufferLength];
                zText = new StringBuilder();
                int length = 0;
                EndPoint ep = (EndPoint)RemoteIPEP;
                while (flag == 0)
                {
                    socket.ReceiveTimeout = ReceiveTimeOut;
                    length = Sockets.ReceiveFrom(receiveByte, 0, receiveByte.Length, SocketFlags.None, ref ep);
                    if (length > 0)
                    {
                        zText.Append(EN.GetString(receiveByte).TrimEnd('\0'));
                        if (!string.IsNullOrEmpty(EndFlag))
                        {
                            if (zText.ToString().EndsWith(EndFlag))//("\r\n\\zwhend"))
                            {
                                receiveMsg = zText.ToString();
                                length = 0;
                                flag = byte.MaxValue;
                            }
                        }
                        else
                        {
                            receiveMsg = zText.ToString();
                            length = 0;
                            flag = byte.MaxValue;
                        }
                    }
                    else
                    {
                        receiveMsg = zText.ToString();
                        length = 0;
                        flag = byte.MaxValue;
                    }
                }
                if (!string.IsNullOrEmpty(receiveMsg))
                {
                    zBool = true;
                }
            }
            catch { }

            return ZBool;
        }

        public bool Send()
        {
            try
            {
                if (Pt == ProtocolType.Tcp) { zBool = TcpSend(); }
                else if (Pt == ProtocolType.Udp) { zBool = UdpSend(); }
                else { }
            }
            catch { }

            return ZBool;
        }

        public bool Send(byte[] msg)
        {
            try
            {
                sendByte = new byte[BufferLength];
                sendByte = msg;
                if (Pt == ProtocolType.Tcp) { zBool = TcpSend(); }
                else if (Pt == ProtocolType.Udp) { zBool = UdpSend(); }
                else { }
            }
            catch { }

            return ZBool;
        }

        private bool TcpSend()
        {
            try
            {
                int length = 0;
                socket.SendTimeout = SendTimeOut;
                length = Sockets.Send(sendByte, 0, sendByte.Length, SocketFlags.None);
                if (length == sendByte.Length)
                {
                    zBool = true;
                }
            }
            catch { }

            return ZBool;
        }

        private bool UdpSend()
        {
            try
            {
                if (!string.IsNullOrEmpty(SendMsg))
                {
                    EndPoint ep = (EndPoint)RemoteIPEP;
                    byte[] msg = EN.GetBytes(SendMsg);
                    if (msg.Length <= BufferLength)
                    {
                        sendByte = msg;
                    }
                    socket.SendTimeout = SendTimeOut;
                    for (int i = BufferLength; i <= msg.Length; i += BufferLength)
                    {
                        Sockets.SendTo(sendByte, 0, sendByte.Length, SocketFlags.None, ep);
                    }
                    zBool = true;
                }
            }
            catch { }

            return ZBool;
        }

        public void Close()
        {
            Sockets.Shutdown(SocketShutdown.Both);
            Sockets.Close();
            Sockets = null;
        }
    }
}
