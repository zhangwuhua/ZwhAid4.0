using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;

using ZwhAid.Model;

namespace ZwhAid
{
    public class TCPAsyncAid : SocketModel
    {
        private TcpListener tcp;
        public TcpListener Tcp
        {
            set { tcp = value; }
            get { return tcp; }
        }

        public TCPAsyncAid()
        {
            tcp = new TcpListener(IPEP);
        }

        public TCPAsyncAid(IPEndPoint ipep)
        {
            this.ipep = ipep;
            tcp = new TcpListener(IPEP);
        }

        public void Server(int count)
        {
            if (count>0)
            {
                clientMaxCount = count;
            }
            tcp.Start(ClientMaxCount);
            tcp.BeginAcceptTcpClient(new AsyncCallback(AcceptAC),Tcp);
        }

        private void AcceptAC(IAsyncResult iar)
        {
            try
            {                
            }
            catch { }
        }
    }
}
