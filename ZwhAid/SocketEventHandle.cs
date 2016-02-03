using System.Text;

using ZwhAid.Model;

namespace ZwhAid
{
    /// <summary>
    /// 多态实现socket接收发送
    /// </summary>
    public class SocketEventHandle
    {
        protected IOCPTCPAid iocpa;

        protected IOCPToken token;
        public IOCPToken Token
        {
            set { token = value; }
            get { return token; }
        }

        public SocketEventHandle(IOCPTCPAid iocpa,IOCPToken token)
        {
            this.iocpa = iocpa;
            this.token = token;
        }

        public virtual bool Receive()
        {
            bool bl = false;
            try
            {
            }
            catch { }
            return bl;
        }
        
        public virtual bool Send()
        {
            bool bl = false;
            try
            {
                if (token.Client != null&&token.SendBuffer!=null)
                {
                    //bl = iocpa.SendAsyncEvent(token.Client, token.SendSAEA, token.SendBuffer, 0, token.SendBuffer.Length);
                }
                else
                {
                    bl = SendCallback();
                }
            }
            catch { }
            return bl;
        }

        /// <summary>
        /// 连续发送
        /// </summary>
        /// <returns></returns>
        public virtual bool SendCallback()
        {
            return true;
        }
        
        public virtual void Close() { }

        public bool ssa_rh(Encoding en, string msg)
        {
            bool bl = false;


            return bl;
        }

        public bool ssa_sh(byte[] msg)
        {
            bool bl = false;


            return bl;
        }
    }
}
