using System;
using System.Text;

namespace ZwhAid.Model
{
    /// <summary>
    /// IO实体类
    /// </summary>
    public class IOModel : ZwhBase
    {
        /// <summary>
        /// 编码，默认Encoding.UTF8
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
        /// 可供操作的内存大小，默认0x00800000
        /// </summary>
        protected UInt64 capacity = 0x00800000;
        /// <summary>
        /// 可供操作的内存大小，默认0x00800000
        /// </summary>
        public UInt64 Capacity
        {
            set { capacity = value; }
            get { return capacity; }
        }

        /// <summary>
        /// 单个缓存区大小
        /// </summary>
        protected uint bufferLength = 1024;
        /// <summary>
        /// 单个缓存区大小
        /// </summary>
        public uint BufferLength
        {
            set { bufferLength = value; }
            get { return bufferLength; }
        }

        /// <summary>
        /// 单个缓存区
        /// </summary>
        protected byte[] buffer = new byte[1024];
        /// <summary>
        /// 单个缓存区
        /// </summary>
        public byte[] Buffer
        {
            set { buffer = value; }
            get { return buffer; }
        }
    }
}
