using System.Net;
using System.Net.Sockets;
using System.Text;

namespace ZwhAid.Model
{
    /// <summary>
    /// Socket操作实体类
    /// </summary>
    public class SocketModel : ZwhBase
    {
        /// <summary>
        /// IP
        /// </summary>
        protected string ip;
        /// <summary>
        /// IP
        /// </summary>
        public string IP
        {
            set { ip = value; }
            get { return ip; }
        }

        /// <summary>
        /// IP
        /// </summary>
        protected IPAddress ipAddr;
        /// <summary>
        /// IP
        /// </summary>
        public IPAddress IPAddr
        {
            set { ipAddr = value; }
            get { return ipAddr; }
        }

        /// <summary>
        /// 域名
        /// </summary>
        protected string host;
        /// <summary>
        /// 域名
        /// </summary>
        public string Host
        {
            set { host = value; }
            get { return host; }
        }

        /// <summary>
        /// 端口号
        /// </summary>
        protected int port;
        /// <summary>
        /// 端口号
        /// </summary>
        public int Port
        {
            set { port = value; }
            get { return port; }
        }

        /// <summary>
        /// IP终结点
        /// </summary>
        protected IPEndPoint ipep;
        /// <summary>
        /// IP终结点
        /// </summary>
        public IPEndPoint IPEP
        {
            set { ipep = value; }
            get { return ipep; }
        }

        /// <summary>
        /// 连接另一方IP终结点
        /// </summary>
        protected IPEndPoint remoteIPEP;
        /// <summary>
        /// 连接另一方IP终结点
        /// </summary>
        public IPEndPoint RemoteIPEP
        {
            set { remoteIPEP = value; }
            get { return remoteIPEP; }
        }

        /// <summary>
        /// 网络流
        /// </summary>
        protected NetworkStream ns;
        /// <summary>
        /// 网络流
        /// </summary>
        public NetworkStream NS
        {
            set { ns = value; }
            get { return ns; }
        }

        /// <summary>
        /// 客户端最大连接数
        /// </summary>
        protected int clientMaxCount;
        /// <summary>
        /// 客户端最大连接数
        /// </summary>
        public int ClientMaxCount
        {
            set { clientMaxCount = value; }
            get { return clientMaxCount; }
        }

        /// <summary>
        /// 客户端当前连接数
        /// </summary>
        protected int clientCount = 0;
        /// <summary>
        /// 客户端当前连接数
        /// </summary>
        public int ClientCount
        {
            set { clientCount = value; }
            get { return clientCount; }
        }

        /// <summary>
        /// 缓存区大小
        /// </summary>
        protected int bufferLength = 8192;
        /// <summary>
        /// 缓存区大小
        /// </summary>
        public int BufferLength
        {
            set { bufferLength = value; }
            get { return bufferLength; }
        }

        /// <summary>
        /// 接收超时时间
        /// </summary>
        protected int receiveTimeOut = 300000;
        /// <summary>
        /// 接收超时时间
        /// </summary>
        public int ReceiveTimeOut
        {
            set { receiveTimeOut = value; }
            get { return receiveTimeOut; }
        }

        /// <summary>
        /// 发送超时时间
        /// </summary>
        protected int sendTimeOut = 300000;
        /// <summary>
        /// 发送超时时间
        /// </summary>
        public int SendTimeOut
        {
            set { sendTimeOut = value; }
            get { return sendTimeOut; }
        }

        /// <summary>
        /// 发送缓存区
        /// </summary>
        protected byte[] sendByte;
        /// <summary>
        /// 发送缓存区
        /// </summary>
        public byte[] SendByte
        {
            set { sendByte = value; }
            get { return sendByte; }
        }

        /// <summary>
        /// 接收缓存区
        /// </summary>
        protected byte[] receiveByte;
        /// <summary>
        /// 接收缓存区
        /// </summary>
        public byte[] ReceiveByte
        {
            set { receiveByte = value; }
            get { return receiveByte; }
        }

        /// <summary>
        /// 结束标志
        /// </summary>
        protected string endFlag;
        /// <summary>
        /// 结束标志
        /// </summary>
        public string EndFlag
        {
            set { endFlag = value; }
            get { return endFlag; }
        }

        /// <summary>
        /// 编码
        /// </summary>
        protected Encoding en = Encoding.UTF8;
        /// <summary>
        /// 编码
        /// </summary>
        public Encoding EN
        {
            set { en = value; }
            get { return en; }
        }

        /// <summary>
        /// 接收信息
        /// </summary>
        protected string receiveMsg;
        /// <summary>
        /// 接收信息
        /// </summary>
        public string ReceiveMsg
        {
            set { receiveMsg = value; }
            get { return receiveMsg; }
        }

        /// <summary>
        /// 发送信息
        /// </summary>
        protected string sendMsg;
        /// <summary>
        /// 发送信息
        /// </summary>
        public string SendMsg
        {
            set { sendMsg = value; }
            get { return sendMsg; }
        }

        /// <summary>
        /// Socket
        /// </summary>
        protected Socket socket;
        /// <summary>
        /// Socket
        /// </summary>
        public Socket Sockets
        {
            set { socket = value; }
            get { return socket; }
        }

        /// <summary>
        /// 建立连接的Socket
        /// </summary>
        protected Socket client;
        /// <summary>
        /// 建立连接的Socket
        /// </summary>
        public Socket Client
        {
            set { client = value; }
            get { return client; }
        }

        /// <summary>
        /// 协议
        /// </summary>
        protected ProtocolType pt;
        /// <summary>
        /// 协议
        /// </summary>
        public ProtocolType Pt
        {
            set { pt = value; }
            get { return pt; }
        }

        /// <summary>
        /// socket类型
        /// </summary>
        protected SocketType st;
        /// <summary>
        /// socket类型
        /// </summary>
        public SocketType St
        {
            set { st = value; }
            get { return st; }
        }

        /// <summary>
        /// 当前处理连接并操作中的SocketAsyncEventArgs
        /// </summary>
        protected SocketAsyncEventArgs saeaCurrent;
        /// <summary>
        /// 当前处理连接并操作中的SocketAsyncEventArgs
        /// </summary>
        public SocketAsyncEventArgs SaeaCurrent
        {
            set { saeaCurrent = value; }
            get { return saeaCurrent; }
        }

        /// <summary>
        /// socket服务运行协议
        /// </summary>
        protected SocketItem si;
        /// <summary>
        /// socket服务运行协议
        /// </summary>
        public SocketItem SI
        {
            set { si = value; }
            get { return si; }
        }

        /// <summary>
        /// 连接委托
        /// </summary>
        /// <param name="clientCount"></param>
        /// <param name="ipep"></param>
        /// <returns></returns>
        public delegate bool ConnectHandler(int clientCount,IPEndPoint ipep);
        /// <summary>
        /// 接收委托
        /// </summary>
        /// <param name="en"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public delegate bool ReceiveHandler(Encoding en, string msg);
        /// <summary>
        /// 发送委托
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        public delegate bool SendHandler(ref byte[] msg);
        /// <summary>
        /// 关闭委托
        /// </summary>
        /// <param name="ipep"></param>
        /// <returns></returns>
        public delegate bool CloseHandler(IPEndPoint ipep);
    }
}
