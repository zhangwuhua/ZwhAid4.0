using System.Net.Sockets;
using System.Collections.Generic;

namespace ZwhAid
{
    /// <summary>
    /// Socket IOCP SocketAsyncEventArgs对象池
    /// </summary>
    public class IOCPSAEAPool
    {
        /// <summary>
        /// SocketAsyncEventArgs队列
        /// </summary>
        Stack<IOCPToken> saeaStack;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="capacity">对象池SocketAsyncEventArgs数量</param>
        public IOCPSAEAPool(int capacity)
        {
            saeaStack = new Stack<IOCPToken>(capacity);
        }

        /// <summary>
        /// 入
        /// </summary>
        /// <param name="saea"></param>
        public void Push(IOCPToken saea)
        {
            if (saea != null)
            {
                lock (saeaStack)
                {
                    saeaStack.Push(saea);
                }
            }
        }

        /// <summary>
        /// 出
        /// </summary>
        /// <returns></returns>
        public IOCPToken Pop()
        {
            lock (saeaStack)
            {
                return saeaStack.Pop();
            }
        }

        /// <summary>
        /// 计数
        /// </summary>
        public int Count
        {
            get { return saeaStack.Count; }
        }
    }
}
