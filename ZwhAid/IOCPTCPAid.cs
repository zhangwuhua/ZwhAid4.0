using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Net;
using System.Threading;

using ZwhAid.Model;

namespace ZwhAid
{
    /// <summary>
    /// Socket TCP的IOCP操作
    /// </summary>
    public class IOCPTCPAid : SocketModel, IDisposable
    {
        /// <summary>
        /// 连接事件
        /// </summary>
        public event ConnectHandler ah;
        /// <summary>
        /// 发送事件
        /// </summary>
        public event SendHandler sh;
        /// <summary>
        /// 接收事件
        /// </summary>
        public event ReceiveHandler rh;
        /// <summary>
        /// 关闭事件
        /// </summary>
        public event CloseHandler ch;

        #region Fields
        /// <summary>
        /// 信号量
        /// </summary>
        Semaphore sem;
        /// <summary>
        /// 缓冲区管理
        /// </summary>
        IOCPBufferManager bm;
        /// <summary>
        /// SocketAsyncEventArgs对象池
        /// </summary>
        IOCPSAEAPool saeap;

        private bool disposed = false;
        #endregion

        #region Properties
        /// <summary>
        /// socket服务是否已运行
        /// </summary>
        public bool IsRunning { get; private set; }
        #endregion

        #region 构造函数
        /// <summary>
        /// 异步IOCP SOCKET服务器
        /// </summary>
        /// <param name="listenPort">监听的端口</param>
        /// <param name="maxClient">最大的客户端数量</param>
        public IOCPTCPAid(int listenPort, int maxClient)
                : this(IPAddress.Any, listenPort, maxClient)
        {
        }

        /// <summary>
        /// 异步Socket TCP服务器
        /// </summary>
        /// <param name="localEP">监听的终结点</param>
        /// <param name="maxClient">最大客户端数量</param>
        public IOCPTCPAid(IPEndPoint localEP, int maxClient)
                : this(localEP.Address, localEP.Port, maxClient)
        {
        }

        /// <summary>
        /// 异步Socket TCP服务器
        /// </summary>
        /// <param name="localIPAddress">监听的IP地址</param>
        /// <param name="listenPort">监听的端口</param>
        /// <param name="maxClient">最大客户端数量</param>
        public IOCPTCPAid(IPAddress localIPAddress, int listenPort, int maxClient)
        {
            try
            {
                this.ipAddr = localIPAddress;
                this.Port = listenPort;
                this.en = Encoding.Default;
                this.clientMaxCount = maxClient;
                this.ipep = new IPEndPoint(IPAddr, Port);

                socket = new Socket(localIPAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

                bm = new IOCPBufferManager(BufferLength * ClientMaxCount * 2, BufferLength);
                saeap = new IOCPSAEAPool(ClientMaxCount);
                sem = new Semaphore(ClientMaxCount, ClientMaxCount);
            }
            catch { }
        }
        #endregion

        #region 初始化
        /// <summary>
        /// 初始化buffer管理、SocketAsyncEventArgs操作完成事件、SocketAsyncEventArgs对象池管理
        /// </summary>
        public void Init()
        {
            bm.InitBuffer();
            IOCPToken token;
            for (int i = 0; i < ClientMaxCount; i++)
            {
                token = new IOCPToken();
                token.ReceiveSAEA.Completed += new EventHandler<SocketAsyncEventArgs>(SAEACompleted);
                token.SendSAEA.Completed += new EventHandler<SocketAsyncEventArgs>(SAEACompleted);
                token.Client = null;
                bm.SetBuffer(token.ReceiveSAEA);
                bm.SetBuffer(token.SendSAEA);
                saeap.Push(token);
            }
        }
        #endregion

        #region 开始
        /// <summary>
        /// socket tcp启动
        /// </summary>
        public void Start()
        {
            if (!IsRunning)
            {
                Init();
                IsRunning = true;
                socket = new Socket(IPEP.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
                if (IPEP.AddressFamily == AddressFamily.InterNetworkV6)
                {
                    socket.SetSocketOption(SocketOptionLevel.IPv6, (SocketOptionName)27, false);
                    socket.Bind(new IPEndPoint(IPAddress.IPv6Any, IPEP.Port));
                }
                else
                {
                    socket.Bind(IPEP);
                }
                socket.Listen(this.ClientMaxCount);
                StartAccept(null);
            }
        }

        /// <summary>
        /// 混合服务协议socket开启
        /// </summary>
        /// <param name="si"></param>
        private void MixStart(SocketItem si)
        {
            if (!IsRunning)
            {
                this.si = si;
                Init();
                IsRunning = true;
                socket = new Socket(IPEP.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
                if (IPEP.AddressFamily == AddressFamily.InterNetworkV6)
                {
                    socket.SetSocketOption(SocketOptionLevel.IPv6, (SocketOptionName)27, false);
                    socket.Bind(new IPEndPoint(IPAddress.IPv6Any, IPEP.Port));
                }
                else
                {
                    socket.Bind(IPEP);
                }
                socket.Listen(this.ClientMaxCount);
                StartAccept(null);
            }
        }
        #endregion

        #region 停止
        /// <summary>
        /// socket tcp停止
        /// </summary>
        public void Stop()
        {
            if (IsRunning)
            {
                IsRunning = false;
                socket.Close();
                //TODO 关闭对所有客户端的连接

            }
        }
        #endregion

        #region Accept
        /// <summary>
        /// 开始接受客户端socket
        /// </summary>
        /// <param name="saea">SocketAsyncEventArg associated with the completed accept operation.</param>
        private void StartAccept(SocketAsyncEventArgs saea)
        {
            try
            {
                if (saea == null)
                {
                    saea = new SocketAsyncEventArgs();
                    saea.Completed += new EventHandler<SocketAsyncEventArgs>(OnAcceptCompleted);
                }
                else
                {
                    //释放上次操作socket
                    saea.AcceptSocket = null;
                }
                //获得信号量
                sem.WaitOne();
                //接受客户端socket
                if (!socket.AcceptAsync(saea))
                {
                    ProcessAccept(saea);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 接受客户端socket完成处理
        /// </summary>
        /// <param name="sender">事件</param>
        /// <param name="e">SocketAsyncEventArgs</param>
        private void OnAcceptCompleted(object sender, SocketAsyncEventArgs e)
        {
            ProcessAccept(e);
        }

        /// <summary>
        /// socket accept
        /// </summary>
        /// <param name="saea">accept SocketAsyncEventArgs</param>
        private void ProcessAccept(SocketAsyncEventArgs saea)
        {
            try
            {
                if (saea.SocketError == SocketError.Success)
                {
                    Socket s = saea.AcceptSocket;
                    if (s != null && s.Connected)
                    {
                        //添加原子操作
                        Interlocked.Increment(ref clientCount);
                        //远程节点
                        remoteIPEP = (IPEndPoint)s.RemoteEndPoint;
                        //对象池取出句柄并赋值
                        IOCPToken token = saeap.Pop();
                        token.Client = s;
                        //Accept事件调用
                        Accept(clientCount, RemoteIPEP);
                        //接收
                        if (!s.ReceiveAsync(token.ReceiveSAEA))
                        {
                            ProcessReceive(token.ReceiveSAEA);
                        }
                        StartAccept(saea);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void Accept(int clientCount, IPEndPoint ipep)
        {
            if (ah != null)
            {
                ah(clientCount, ipep);
            }
        }
        #endregion

        #region 发送数据
        /// <summary>
        /// 异步的发送数据
        /// </summary>
        /// <param name="saea"></param>
        /// <param name="data"></param>
        public void Send(SocketAsyncEventArgs saea)
        {
            try
            {
                IOCPToken token = saea.UserToken as IOCPToken;
                Socket s = token.Client;
                if (s != null && token.SendSAEA.SocketError == SocketError.Success)
                {
                    if (s.Connected)
                    {
                        //设置发送数据
                        //Array.Copy(data, 0, saea.Buffer, 0, data.Length);
                        token.SendSAEA.SetBuffer(SendByte, 0, SendByte.Length);

                        //继续处理发送
                        if (!s.SendAsync(token.SendSAEA))
                        {
                            ProcessSend(token.SendSAEA);
                        }
                        else
                        {
                            Close(token);
                        }
                    }
                }
                //else
                //{
                //    Close(token);
                //}
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 同步的使用socket发送数据
        /// </summary>
        /// <param name="socket"></param>
        /// <param name="buffer"></param>
        /// <param name="offset"></param>
        /// <param name="size"></param>
        /// <param name="timeout"></param>
        public void Send(Socket socket, byte[] buffer, int offset, int size, int timeout)
        {
            socket.SendTimeout = 0;
            int startTickCount = Environment.TickCount;
            int sent = 0;
            do
            {
                if (Environment.TickCount > startTickCount + timeout)
                {
                    //throw new Exception("Timeout.");
                }
                try
                {
                    sent += socket.Send(buffer, offset + sent, size - sent, SocketFlags.None);
                }
                catch (SocketException ex)
                {
                    if (ex.SocketErrorCode == SocketError.WouldBlock ||
                    ex.SocketErrorCode == SocketError.IOPending ||
                    ex.SocketErrorCode == SocketError.NoBufferSpaceAvailable)
                    {
                        Thread.Sleep(30);
                    }
                    else
                    {
                        throw ex;
                    }
                }
            } while (sent < size);
        }

        /// <summary>
        /// socket send
        /// </summary>
        /// <param name="saea">与发送完成操作相关联的SocketAsyncEventArg对象</param>
        private void ProcessSend(SocketAsyncEventArgs saea)
        {
            try
            {
                IOCPToken token = saea.UserToken as IOCPToken;
                Socket s = token.Client;
                if (s != null && token.SendSAEA.SocketError == SocketError.Success && token.SendSAEA.BytesTransferred <= 0)
                {
                    if (!s.ReceiveAsync(token.ReceiveSAEA))
                    {
                        ProcessReceive(token.ReceiveSAEA);
                    }
                }
                else
                {
                    Close(token);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 发送事件委托处理
        /// </summary>
        /// <param name="msg"></param>
        public void Send(byte[] msg)
        {
            if (sh != null)
            {
                this.sh(ref msg);
                sendByte = new byte[BufferLength];
                Array.Copy(msg, sendByte, msg.Length);
            }
        }
        #endregion

        #region 接收数据
        /// <summary>
        /// socket receive
        /// </summary>
        /// <param name="saea">receive SocketAsyncEventArgs</param>
        private void ProcessReceive(SocketAsyncEventArgs saea)
        {
            try
            {
                IOCPToken token = saea.UserToken as IOCPToken;
                if (token.Client != null && token.ReceiveSAEA.BytesTransferred > 0 && token.ReceiveSAEA.SocketError == SocketError.Success)
                {
                    Socket s = token.Client;
                    if (s.Available == 0)
                    {
                        //获得接收缓存区数据
                        byte[] data = new byte[token.ReceiveSAEA.BytesTransferred];
                        Array.Copy(token.ReceiveSAEA.Buffer, token.ReceiveSAEA.Offset, data, 0, data.Length);

                        zText.Append(EN.GetString(data));
                        receiveMsg = ZText.ToString();
                        //事件处理接收数据
                        Receive(EN, ReceiveMsg);
                        ////事件获取发送数据
                        Send(SendByte);
                        if (SendByte != null)
                        {
                            sh = null;
                        }
                        //发送
                        Send(token.SendSAEA);
                        //初始化接收信息字段
                        zText = new StringBuilder();
                    }
                    else
                    {
                        //接收数据
                        byte[] data = new byte[token.ReceiveSAEA.BytesTransferred];
                        Array.Copy(token.ReceiveSAEA.Buffer, token.ReceiveSAEA.Offset, data, 0, data.Length);
                        zText.Append(EN.GetString(data));
                    }
                    if (s.Connected)
                    {
                        //继续处理接收
                        if (!s.ReceiveAsync(token.ReceiveSAEA))
                        {
                            ProcessReceive(token.ReceiveSAEA);
                        }
                    }
                    //else
                    //{
                    //    Close(token);
                    //}
                }
                else
                {
                    Close(token);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 接收事件委托处理
        /// </summary>
        /// <param name="en"></param>
        /// <param name="msg"></param>
        public void Receive(Encoding en, string msg)
        {
            if (rh != null)
            {
                this.rh(EN, msg);
            }
        }
        #endregion

        #region 回调函数
        /// <summary>
        /// SocketAsyncEventArgs请求时调用此事件
        /// </summary>
        /// <param name="sender">激发事件的对象</param>
        /// <param name="saea">激发事件的SocketAsyncEventArgs</param>
        private void SAEACompleted(object sender, SocketAsyncEventArgs saea)
        {
            try
            {
                IOCPToken token = saea.UserToken as IOCPToken;
                switch (saea.LastOperation)
                {
                    case SocketAsyncOperation.Accept:
                        ProcessAccept(saea);
                        break;
                    case SocketAsyncOperation.Receive:
                        ProcessReceive(saea);
                        break;
                    case SocketAsyncOperation.Send:
                        ProcessSend(saea);
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region 关闭
        /// <summary>
        /// 关闭已接受客户端连接的socket
        /// </summary>
        /// <param name="token"></param>
        private void Close(IOCPToken token)
        {
            try
            {
                Socket s = token.Client;
                if (s != null)
                {
                    try
                    {
                        s.Shutdown(SocketShutdown.Both);
                    }
                    catch (Exception) { }
                    finally
                    {
                        s.Close();
                    }
                    //去除原子操作
                    Interlocked.Decrement(ref clientCount);
                    //释放句柄
                    token.Client = null;
                    //释放信号量
                    sem.Release();
                    //入SocketAsyncEventArgs对象池
                    saeap.Push(token);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Dispose
        /// <summary>
        /// Performs application-defined tasks associated with freeing, 
        /// releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources
        /// </summary>
        /// <param name="disposing"><c>true</c> to release 
        /// both managed and unmanaged resources; <c>false</c> 
        /// to release only unmanaged resources.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    try
                    {
                        Stop();
                        if (socket != null)
                        {
                            socket = null;
                        }
                    }
                    catch (SocketException ex)
                    {
                        //TODO 事件
                    }
                }
                disposed = true;
            }
        }
        #endregion
    }
}
