using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace ZwhAid
{
    public class SocketServerAid : Model.SocketModel
    {
        public event ReceiveHandler rh;
        public event SendHandler sh;
        ManualResetEvent mre = new ManualResetEvent(false);

        public SocketServerAid() { }

        public SocketServerAid(ProtocolType pt)
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

        public SocketServerAid(ProtocolType pt, IPEndPoint ipep)
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

        public SocketServerAid(ProtocolType pt, IPAddress ipAddress, int port)
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

        public SocketServerAid(ProtocolType pt, string ip, int port)
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

        public void Server()
        {
            socket.Bind((EndPoint)IPEP);
            socket.Listen(0);
            //ThreadPool.SetMaxThreads(10, 10);
            //bool bl = ThreadPool.QueueUserWorkItem(new WaitCallback(Accept));
            //if (!bl) { return; }
            Listen();
        }

        public void Server(int count)
        {
            socket.Bind((EndPoint)IPEP);
            socket.Listen(count);
            //ThreadPool.SetMaxThreads(10, 10);
            //bool bl = ThreadPool.QueueUserWorkItem(new WaitCallback(Accept));
            //if (!bl) { return; }
            Listen();
        }

        private void Listen()
        {
            while (true)
            {
                client = socket.Accept();
                remoteIPEP = (IPEndPoint)Client.RemoteEndPoint;
                if (Client != null)
                {
                    Thread t = new Thread(new ThreadStart(Accept));
                    t.Start();
                }
            }
        }

        private void Listen(int connects)
        {
        }

        private void Accept()
        {
            remoteIPEP = (IPEndPoint)Client.RemoteEndPoint;
            while (Client.Connected)
            {
                //SocketEventHandle seh = new SocketEventHandle();
                
            }
        }

        public bool Receive()
        {
            if (Pt == ProtocolType.Tcp) { zBool = TcpReceive(); }
            else if (Pt == ProtocolType.Udp) { zBool = UdpReceive(); }
            else { }

            return ZBool;
        }

        private bool TcpReceive()
        {
            if (Client == null) { client = Sockets; }
            receiveByte = new byte[bufferLength];
            zText = new StringBuilder();
            int length = 0;
            while (flag == 0)
            {
                length = Client.Receive(receiveByte, 0, receiveByte.Length, SocketFlags.None);
                if (length > 0)
                {
                    zText.Append(EN.GetString(receiveByte).TrimEnd('\0'));
                    if (zText.ToString().EndsWith("\r\n\\zwhend"))
                    {
                        receiveMsg = zText.ToString();
                        //触发事件进行接收处理
                        if (rh != null)
                        {
                            bool bl = rh(EN, receiveMsg);
                            if (bl)
                            {
                                length = 0;
                                flag = byte.MaxValue;
                            }
                        }
                    }
                }
                else
                {
                    receiveMsg = zText.ToString();
                    //触发事件进行接收处理
                    if (rh != null)
                    {
                        bool bl = rh(EN, receiveMsg);
                        if (bl)
                        {
                            length = 0;
                            flag = byte.MaxValue;
                        }
                    }
                }
            }

            return ZBool;
        }

        private bool UdpReceive()
        {
            if (Client == null) { client = Sockets; }
            receiveByte = new byte[bufferLength];
            zText = new StringBuilder();
            int length = 0;
            EndPoint ep = (EndPoint)RemoteIPEP;
            while (flag == 0)
            {
                length = Client.ReceiveFrom(receiveByte, 0, receiveByte.Length, SocketFlags.None, ref ep);
                if (length > 0)
                {
                    zText.Append(EN.GetString(receiveByte).TrimEnd('\0'));
                }
                else
                {
                    receiveMsg = zText.ToString();
                    //触发事件进行接收处理
                    if (rh != null)
                    {
                        bool bl = rh(EN, receiveMsg);
                        if (bl)
                        {
                            length = 0;
                            flag = byte.MaxValue;
                        }
                    }
                }
            }

            return ZBool;
        }

        public bool Send()
        {
            ////注册发送事件
            //SocketEventHandle seh = new SocketEventHandle();
            ////sh += new SendHandle(seh.Send);

            //if (Pt == ProtocolType.Tcp) { zBool = TcpSend(); }
            //else if (Pt == ProtocolType.Udp) { zBool = UdpSend(); }
            //else { }

            return ZBool;
        }

        private bool TcpSend()
        {
            if (sh != null)
            {
                //sh(sendByte);

                if (SendByte != null)
                {
                    if (Client == null) { client = Sockets; }
                    Client.Send(sendByte, 0, sendByte.Length, SocketFlags.None);
                }

            }
            return ZBool;
        }

        private bool UdpSend()
        {
            if (sh != null)
            {
                //sh(sendByte);

                if (SendByte != null)
                {
                    if (Client == null) { client = Sockets; }
                    EndPoint ep = (EndPoint)RemoteIPEP;
                    Client.SendTo(sendByte, 0, sendByte.Length, SocketFlags.None, ep);
                }
            }

            return ZBool;
        }

        public void Close()
        {
            Sockets.Shutdown(SocketShutdown.Both);
            Sockets.Close();
            Sockets = null;
        }

        public void Close(Socket s)
        {
            s.Shutdown(SocketShutdown.Both);
            s.Close();
            s = null;
        }
    }
}
