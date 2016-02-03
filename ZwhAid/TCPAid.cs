using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace ZwhAid
{
    public class TCPAid : ZwhAid.Model.SocketModel
    {
        private Socket tcp;
        public Socket Tcp
        {
            set { tcp = value; }
            get { return tcp; }
        }

        public TCPAid()
        {
            tcp = new Socket(IPEP.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
        }

        public TCPAid(string ip, int port)
        {
            this.ipAddr = IPAddress.Parse(ip);
            this.port = port;
            ipep = new IPEndPoint(IPAddr, Port);
            tcp = new Socket(IPEP.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
        }

        public TCPAid(IPEndPoint ipep)
        {
            this.ipep = ipep;
            tcp = new Socket(IPEP.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
        }

        #region TCP Server
        public void TCPServer()
        {
            tcp.Bind(IPEP);
            tcp.Listen(0);
            Thread t = new Thread(ListenConnect);
            t.Start();
        }

        public void TCPServer(int count)
        {
            tcp.Bind(IPEP);
            tcp.Listen(count);
            Thread t = new Thread(ListenConnect);
            t.Start();
        }

        private void ListenConnect()
        {
            while (true)
            {
                Socket client = Tcp.Accept();
                remoteIPEP = (IPEndPoint)client.RemoteEndPoint;
                client.Send(sendByte);
                Thread rt = new Thread(SvrReceive);
                rt.Start(client);
            }
        }

        private void SvrReceive(object client)
        {
            Socket clientSocket = (Socket)client;
            while (true)
            {
                try
                {
                    receiveByte = new byte[bufferLength];
                    //通过clientSocket接收数据
                    int receiveNumber = clientSocket.Receive(receiveByte);
                }
                catch
                {
                    clientSocket.Shutdown(SocketShutdown.Both);
                    clientSocket.Close();
                    break;
                }
            }
        }
        #endregion

        #region TCP Client
        public bool TCP()
        {
            try
            {
                zBool = TCP(sendByte);
            }
            catch { }

            return ZBool;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sendByte">发送的信息，默认长度8192</param>
        /// <returns></returns>
        public bool TCP(byte[] sendByte)
        {
            try
            {
                this.sendByte = sendByte;
                if (Send(sendByte))
                {
                    receiveByte = new byte[bufferLength];
                    zBool = Receive(receiveByte);
                }
            }
            catch { }

            return ZBool;
        }

        public bool Connect()
        {
            try
            {
                tcp.Connect(ipAddr, port);
                if (tcp.Connected)
                {
                    zBool = true;
                }
            }
            catch { }

            return ZBool;
        }

        public void Send()
        {
            try
            {
                Send(SendByte);
            }
            catch { }
        }

        public bool Send(byte[] sendByte)
        {
            try
            {
                this.sendByte = sendByte;
                if (Connect())
                {
                    tcp.Send(SendByte, 0, SendByte.Length, SocketFlags.None);
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
                receiveByte = new byte[bufferLength];
                zBool = Receive(ReceiveByte);
            }
            catch { }

            return ZBool;
        }

        public bool Receive(byte[] receiveByte)
        {
            try
            {
                this.receiveByte = receiveByte;
                zText = new StringBuilder();
                while (tcp.Connected)
                {
                    int i = tcp.Receive(ReceiveByte, 0, ReceiveByte.Length, SocketFlags.None);
                    if (i > 0)
                    {
                        zText.Append(Encoding.Default.GetString(receiveByte).TrimEnd('\0'));
                    }
                    else
                    {
                        receiveMsg = zText.ToString();
                        Close();
                    }
                }
            }
            catch
            {
                Close();
            }

            return ZBool;
        }

        private void Close()
        {
            tcp.Shutdown(SocketShutdown.Both);
            tcp.Close();
        }
        #endregion
    }
}
