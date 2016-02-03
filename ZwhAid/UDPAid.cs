using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace ZwhAid
{
    public class UDPAid : ZwhAid.Model.SocketModel
    {
        private Socket udp;
        public Socket Udp
        {
            set { udp = value; }
            get { return udp; }
        }

        public UDPAid()
        {
            udp = new Socket(IPEP.AddressFamily, SocketType.Rdm, ProtocolType.Udp);
        }

        public UDPAid(IPAddress ipAddress, int port)
        {
            this.ipAddr = ipAddress;
            this.port = port;
            ipep = new IPEndPoint(IPAddr, Port);
            udp = new Socket(IPEP.AddressFamily, SocketType.Rdm, ProtocolType.Udp);
        }

        public UDPAid(IPEndPoint ipep)
        {
            this.ipep = ipep;
            udp = new Socket(IPEP.AddressFamily, SocketType.Rdm, ProtocolType.Udp);
        }

        public void UDPServer()
        {
            udp.Bind((EndPoint)IPEP);
            udp.Listen(0);
            Thread t = new Thread(ListenConnect);
            t.Start();
        }

        public void UDPServer(int count)
        {
            udp.Bind((EndPoint)IPEP);
            udp.Listen(count);
            Thread t = new Thread(ListenConnect);
            t.Start();
        }

        private void ListenConnect()
        {
            while (true)
            {
                Socket client = Udp.Accept();
                remoteIPEP = (IPEndPoint)client.RemoteEndPoint;
                client.Send(sendByte);
                //Thread rt = new Thread(SvrReceive);
                //rt.Start(client);
            }
        }

    }
}
