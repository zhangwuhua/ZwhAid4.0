using System;
using System.IO;
using System.IO.MemoryMappedFiles;
using System.Runtime.InteropServices;
using System.Text;

using ZwhAid.Model;

namespace ZwhAid
{
    /// <summary>
    /// MemoryMappedFiles
    /// </summary>
    public class MemoryFileAid : IOModel
    {
        string file = string.Empty;
        MemoryMappedFile mmf;

        /// <summary>
        /// 构造函数
        /// </summary>
        public MemoryFileAid() { }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="file"></param>
        public MemoryFileAid(string file) { }

        /// <summary>
        /// 非持久化内存映射文件写入
        /// </summary>
        /// <param name="name"></param>
        /// <param name="capacity"></param>
        /// <param name="buffer"></param>
        public void Write(string name, Int64 capacity, byte[] buffer)
        {
            using (mmf = MemoryMappedFile.CreateNew(name, capacity, MemoryMappedFileAccess.ReadWrite))
            {
                using (MemoryMappedViewStream mmvs = mmf.CreateViewStream())
                {
                    using (BinaryWriter bw = new BinaryWriter(mmvs, EN))
                    {
                        bw.Write(buffer);
                    }
                }
            }
        }

        /// <summary>
        /// 非持久化内存映射文件写入
        /// </summary>
        /// <param name="name"></param>
        /// <param name="capacity"></param>
        /// <param name="en"></param>
        /// <param name="buffer"></param>
        public void Write(string name, Int64 capacity, Encoding en, byte[] buffer)
        {
            using (mmf = MemoryMappedFile.CreateNew(name, capacity, MemoryMappedFileAccess.ReadWrite))
            {
                using (MemoryMappedViewStream mmvs = mmf.CreateViewStream())
                {
                    using (BinaryWriter bw = new BinaryWriter(mmvs, en))
                    {
                        bw.Write(buffer);
                    }
                }
            }
        }
        
        /// <summary>
        /// 内存映射文件读取
        /// </summary>
        /// <param name="name">内存映射文件名称</param>
        /// <param name="capacity">内存映射文件大小</param>
        /// <param name="en">编码</param>
        public byte[] Read(string name, Int64 capacity, Encoding en)
        {
            byte[] buffer = new byte[capacity];
            using (mmf = MemoryMappedFile.OpenExisting(name))
            {
                using (MemoryMappedViewStream mmvs = mmf.CreateViewStream())
                {
                    using (BinaryReader br = new BinaryReader(mmvs, en))
                    {
                        int length = int.Parse(capacity.ToString());
                        br.Read(buffer, 0, length);
                    }
                }
            }
            return buffer;
        }

        /// <summary>
        /// 持久化内存映射文件写入
        /// </summary>
        /// <param name="name"></param>
        /// <param name="offset"></param>
        /// <param name="size"></param>
        /// <param name="kv">共享的结构体</param>
        public void PWrite(string name, Int64 offset, Int64 size, KV kv)
        {
            using (mmf = MemoryMappedFile.CreateFromFile(file, FileMode.Open, name, (long)capacity, MemoryMappedFileAccess.ReadWrite))
            {
                using (MemoryMappedViewAccessor mmva = mmf.CreateViewAccessor(offset, size))
                {
                    int length = Marshal.SizeOf(typeof(KV));
                    for (long i = 0; i < size; i += length)
                    {
                        char c = mmva.ReadChar(i);
                        mmva.Write(i, ref c);
                    }
                }
            }
        }

        /// <summary>
        /// 持久化内存映射文件读取
        /// </summary>
        /// <param name="name"></param>
        /// <param name="capacity"></param>
        /// <param name="en"></param>
        /// <returns></returns>
        public byte[] PRead(string name, Int64 capacity, Encoding en)
        {
            byte[] buffer = new byte[capacity];
            try
            {
                using (mmf = MemoryMappedFile.OpenExisting(name))
                {
                    using (MemoryMappedViewAccessor mmva = mmf.CreateViewAccessor())
                    {
                    }
                }
            }
            catch { }
            return buffer;
        }

        /// <summary>
        /// 关闭
        /// </summary>
        public void Close()
        {
            mmf.Dispose();
        }
    }

    /// <summary>
    /// 用于共享所需的结构体
    /// </summary>
    public struct KV
    {
        /// <summary>
        /// 键
        /// </summary>
        public string key;
        /// <summary>
        /// 值
        /// </summary>
        public object value;
    }
}
