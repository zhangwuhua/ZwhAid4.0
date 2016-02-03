using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

using ZwhAid.Model;

namespace ZwhAid
{
    public class SocketAsyncAid : SocketModel
    {
        public SocketAsyncAid(ProtocolType pt)
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

        public SocketAsyncAid(ProtocolType pt, IPEndPoint ipep)
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

        public SocketAsyncAid(ProtocolType pt, IPAddress ipAddress, int port)
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

        public SocketAsyncAid(ProtocolType pt, string ip, int port)
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
            ThreadPool.QueueUserWorkItem(new WaitCallback(Accept));
        }

        public void Server(int count)
        {
            socket.Bind((EndPoint)IPEP);
            socket.Listen(count);
            //ThreadPool.SetMaxThreads(10, 10);
            bool bl = ThreadPool.QueueUserWorkItem(new WaitCallback(Accept));
            if (!bl) { return; }
        }

        private void Accept(object obj)
        {
            IAsyncResult iar = socket.BeginAccept(new AsyncCallback(AcceptAC),null);
        }

        private void AcceptAC(IAsyncResult iar)
        {
        }

        public bool Connect()
        {
            socket.Connect((EndPoint)IPEP);
            if (socket.Connected)
            {
                zBool = true;
            }

            return ZBool;
        }

        public bool Receive()
        {
            if (Pt == ProtocolType.Tcp) { zBool=TcpReceive(socket); }
            else if (Pt == ProtocolType.Udp) { zBool = UdpReceive(socket); }
            else { }

            return ZBool;
        }

        private bool Receive(Socket client)
        {
            if (Pt == ProtocolType.Tcp) { zBool = TcpReceive(client); }
            else if (Pt == ProtocolType.Udp) { zBool = UdpReceive(client); }
            else { }

            return ZBool;
        }

        private bool TcpReceive(Socket client)
        {
            if (client == null) { client = Sockets; }
            receiveByte = new byte[bufferLength];
            zText = new StringBuilder();
            int length = 0;
            while (client.Connected)
            {
                IAsyncResult iar = client.BeginReceive(receiveByte, 0, receiveByte.Length, SocketFlags.None, new AsyncCallback(TcpReceiveAC), null);
                if (length > 0)
                {
                    zText.Append(Encoding.UTF8.GetString(receiveByte).TrimEnd('\0'));
                }
                else
                {
                    receiveMsg = zText.ToString();
                    length = 0;
                    //接收完事件

                    Close(client);
                }
            }

            return ZBool;
        }

        private void TcpReceiveAC(IAsyncResult iar)
        {
        }

        private bool UdpReceive(Socket client)
        {
            if (client == null) { client = Sockets; }
            receiveByte = new byte[bufferLength];
            zText = new StringBuilder();
            int length = 0;
            EndPoint ep = (EndPoint)RemoteIPEP;
            while (client.Connected)
            {
                IAsyncResult iar = client.BeginReceiveFrom(receiveByte, 0, receiveByte.Length, SocketFlags.None, ref ep, new AsyncCallback(UdpReceiveAC), null);
                if (length > 0)
                {
                    zText.Append(Encoding.UTF8.GetString(receiveByte).TrimEnd('\0'));
                }
                else
                {
                    receiveMsg = zText.ToString();
                    length = 0;
                    //接收完事件

                    Close(client);
                }
            }

            return ZBool;
        }

        private void UdpReceiveAC(IAsyncResult iar)
        {
        }

        public bool Send()
        {
            if (Pt == ProtocolType.Tcp) { zBool=TcpSend(socket); }
            else if (Pt == ProtocolType.Udp) { zBool = UdpSend(socket); }
            else { }

            return ZBool;
        }

        private bool Send(Socket client)
        {
            if (Pt == ProtocolType.Tcp) { zBool = TcpSend(client); }
            else if (Pt == ProtocolType.Udp) { zBool = UdpSend(client); }
            else { }

            return ZBool;
        }

        private bool TcpSend(Socket client)
        {
            if (client == null) { client = Sockets; }
            client.BeginSend(sendByte, 0, sendByte.Length, SocketFlags.None, new AsyncCallback(TcpSendAC), null);

            return ZBool;
        }

        private void TcpSendAC(IAsyncResult iar)
        {
        }

        private bool UdpSend(Socket client)
        {
            if (client == null) { client = Sockets; }
            EndPoint ep = (EndPoint)RemoteIPEP;
            client.BeginSendTo(sendByte, 0, sendByte.Length, SocketFlags.None, ep, new AsyncCallback(UdpSendAC), null);

            return ZBool;
        }

        private void UdpSendAC(IAsyncResult iar)
        {
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
