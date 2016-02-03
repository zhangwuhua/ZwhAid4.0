using System.Net.Sockets;

namespace ZwhAid
{
    /// <summary>
    /// IOCP句柄对象
    /// </summary>
    public class IOCPToken
    {
        #region 字段属性
        /// <summary>
        /// 接收数据的SocketAsyncEventArgs
        /// </summary>
        private SocketAsyncEventArgs receiveSAEA;
        public SocketAsyncEventArgs ReceiveSAEA
        {
            set { receiveSAEA = value; }
            get { return receiveSAEA; }
        }

        /// <summary>
        /// 发送数据的SocketAsyncEventArgs
        /// </summary>
        private SocketAsyncEventArgs sendSAEA;
        public SocketAsyncEventArgs SendSAEA
        {
            set { sendSAEA = value; }
            get { return sendSAEA; }
        }

        private int rLength;
        /// <summary>
        /// 接收缓存大小
        /// </summary>
        public int RLength
        {
            set { rLength = value; }
            get { return rLength; }
        }

        private int sLength;
        /// <summary>
        /// 发送缓存大小
        /// </summary>
        public int SLength
        {
            set { sLength = value; }
            get { return sLength; }
        }

        /// <summary>
        /// 接收数据的缓冲
        /// </summary>
        private byte[] receiveBuffer;
        public byte[] ReceiveBuffer
        {
            set { receiveBuffer = value; }
            get { return receiveBuffer; }
        }

        /// <summary>
        /// 发送数据的缓冲
        /// </summary>
        private byte[] sendBuffer;
        public byte[] SendBuffer
        {
            set { sendBuffer = value; }
            get { return sendBuffer; }
        }

        private SocketEventHandle seh;
        /// <summary>
        /// socket连接、接收、发送处理
        /// </summary>
        public SocketEventHandle SEH
        {
            set { seh = value; }
            get { return seh; }
        }

        private Socket client;
        /// <summary>
        /// 连接的Socket
        /// </summary>
        public Socket Client
        {
            get { return client; }
            set
            {
                client = value;
                //清理缓存
                if (client==null)
                {
                }
                seh = null;
                receiveSAEA.AcceptSocket = client;
                sendSAEA.AcceptSocket = client;
            }
        }
        #endregion

        /// <summary>
        /// 构造函数
        /// </summary>
        public IOCPToken()
        {
            rLength = 512;
            sLength = 1024;
            TokenInit();
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="rLength"></param>
        /// <param name="sLength"></param>
        public IOCPToken(int rLength, int sLength)
        {
            this.rLength = rLength;
            this.sLength = sLength;
            TokenInit();
        }

        /// <summary>
        /// IOCP句柄对象初始化
        /// </summary>
        private void TokenInit()
        {
            client = null;
            receiveSAEA = new SocketAsyncEventArgs();
            receiveSAEA.UserToken = this;
            sendSAEA = new SocketAsyncEventArgs();
            sendSAEA.UserToken = this;
        }
    }
}
