using System;
using System.Runtime.InteropServices;
using System.Text;
using ZwhAid.Model;

namespace ZwhAid
{
    /// <summary>
    /// 共享内存
    /// </summary>
    public class MemoryShareAid : IOModel
    {
        #region 引用windows系统内置内存操作函数
        //hFile是创建共享文件的句柄。
        //lpFileMappingAttributes是文件共享的属性。
        //flProtect是当文件映射时读写文件的属性。
        //dwMaximumSizeHigh是文件共享的大小高位字节。
        //dwMaximumSizeLow是文件共享的大小低位字节。
        //lpName是共享文件对象名称。
        //hFileMappingObject是共享文件对象。
        //dwDesiredAccess是文件共享属性。
        //dwFileOffsetHigh是文件共享区的偏移地址。
        //dwFileOffsetLow是文件共享区的偏移地址。
        //dwNumberOfBytesToMap是共享数据长度。
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr SendMessage(IntPtr hWnd, int Msg, int wParam, IntPtr lParam);

        /// <summary>
        /// 创建内存文件
        /// </summary>
        /// <param name="hFile"></param>
        /// <param name="lpAttributes"></param>
        /// <param name="flProtect"></param>
        /// <param name="dwMaxSizeHi"></param>
        /// <param name="dwMaxSizeLow"></param>
        /// <param name="lpName"></param>
        /// <returns></returns>
        [DllImport("Kernel32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr CreateFileMapping(int hFile, IntPtr lpAttributes, uint flProtect, uint dwMaxSizeHi, uint dwMaxSizeLow, string lpName);

        [DllImport("Kernel32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr OpenFileMapping(int dwDesiredAccess, [MarshalAs(UnmanagedType.Bool)] bool bInheritHandle, string lpName);

        /// <summary>
        /// 创建内存视图
        /// </summary>
        /// <param name="hFileMapping"></param>
        /// <param name="dwDesiredAccess"></param>
        /// <param name="dwFileOffsetHigh"></param>
        /// <param name="dwFileOffsetLow"></param>
        /// <param name="dwNumberOfBytesToMap"></param>
        /// <returns></returns>
        [DllImport("Kernel32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr MapViewOfFile(IntPtr hFileMapping, uint dwDesiredAccess, uint dwFileOffsetHigh, uint dwFileOffsetLow, uint dwNumberOfBytesToMap);

        /// <summary>
        /// 卸载内存视图
        /// </summary>
        /// <param name="pvBaseAddress"></param>
        /// <returns></returns>
        [DllImport("Kernel32.dll", CharSet = CharSet.Auto)]
        public static extern bool UnMapViewOfFile(IntPtr pvBaseAddress);

        /// <summary>
        /// 关闭句柄
        /// </summary>
        /// <param name="handle"></param>
        /// <returns></returns>
        [DllImport("Kernel32.dll", CharSet = CharSet.Auto)]
        public static extern bool CloseHandle(IntPtr handle);

        /// <summary>
        /// 最近一次操作结果
        /// </summary>
        /// <returns></returns>
        [DllImport("kernel32.dll", EntryPoint = "GetLastError")]
        public static extern int GetLastError();
        #endregion

        #region 引用windows系统内存操作参数
        const int ERROR_ALREADY_EXISTS = 183;
        const int FILE_MAP_COPY = 0x0001;
        const int FILE_MAP_WRITE = 0x0002;
        const int FILE_MAP_READ = 0x0004;
        const int FILE_MAP_ALL_ACCESS = 0x0002 | 0x0004;
        const int PAGE_READONLY = 0x02;
        const int PAGE_READWRITE = 0x04;
        const int PAGE_WRITECOPY = 0x08;
        const int PAGE_EXECUTE = 0x10;
        const int PAGE_EXECUTE_READ = 0x20;
        const int PAGE_EXECUTE_READWRITE = 0x40;
        const int SEC_COMMIT = 0x8000000;
        const int SEC_IMAGE = 0x1000000;
        const int SEC_NOCACHE = 0x10000000;
        const int SEC_RESERVE = 0x4000000;
        const int INVALID_HANDLE_VALUE = -1;
        #endregion

        IntPtr IntPtrMemoryFile = IntPtr.Zero;
        IntPtr IntPtrData = IntPtr.Zero;
        bool m_bAlreadyExist = false;
        bool m_bInit = false;

        /// <summary>
        /// 构造函数
        /// </summary>
        public MemoryShareAid() { }

        /// <summary>
        /// 构造函数，共享内存初始化
        /// </summary>
        /// <param name="name"></param>
        public MemoryShareAid(string name)
        {
            this.capacity = BufferLength;
            Init(name, Capacity);
        }

        /// <summary>
        /// 构造函数，共享内存初始化
        /// </summary>
        /// <param name="name"></param>
        /// <param name="capacity"></param>
        public MemoryShareAid(string name, UInt64 capacity)
        {
            this.capacity = capacity;
            Init(name, capacity);
        }

        /// <summary>
        /// 析构函数
        /// </summary>
        ~MemoryShareAid() { Close(); }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="name">共享内存名称</param>
        /// <param name="capacity">共享内存大小</param>
        /// <returns></returns>
        public bool Init(string name, UInt64 capacity)
        {
            try
            {
                if (!string.IsNullOrEmpty(name))
                {
                    //创建内存共享体（CreateFileMapping）
                    IntPtrMemoryFile = CreateFileMapping(INVALID_HANDLE_VALUE, IntPtr.Zero, (uint)PAGE_READWRITE, 0, (uint)Capacity, name);
                    if (IntPtrMemoryFile == IntPtr.Zero)
                    {
                        m_bAlreadyExist = false;
                        m_bInit = false;
                        zBool = false;
                    }
                    else
                    {
                        if (GetLastError() == ERROR_ALREADY_EXISTS)
                        {
                            m_bAlreadyExist = true;
                        }
                        else
                        {
                            m_bAlreadyExist = false;
                        }
                        //创建内存映射（MapViewOfFile）
                        IntPtrData = MapViewOfFile(IntPtrMemoryFile, FILE_MAP_WRITE, 0, 0, (uint)Capacity);
                        if (IntPtrData == IntPtr.Zero)
                        {
                            m_bInit = false;
                            CloseHandle(IntPtrMemoryFile);
                            zBool = false;
                        }
                        else
                        {
                            m_bInit = true;
                            if (m_bAlreadyExist == false)
                            {
                                //初始化
                            }
                        }
                    }
                }
            }
            catch { }
            return ZBool;
        }

        /// <summary>
        /// 写入数据
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public bool Write(byte[] data)
        {
            try
            {
                zBool = Write(data, 0, data.Length);
            }
            catch { }
            return ZBool;
        }

        /// <summary>
        /// 写入数据
        /// </summary>
        /// <param name="data"></param>
        /// <param name="offset"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public bool Write(byte[] data, int offset, int length)
        {
            try
            {
                if ((uint)offset + (uint)length >= Capacity)
                {
                    Marshal.Copy(data, 0, IntPtrData, data.Length);
                    zBool = true;
                }
                else
                {
                    zBool = false;
                }
            }
            catch { }
            return ZBool;
        }

        /// <summary>
        /// 读取数据
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public bool Read(ref byte[] data)
        {
            try
            {
                zBool = Read(ref data, 0, data.Length);
            }
            catch { }
            return ZBool;
        }

        /// <summary>
        /// 读取数据
        /// </summary>
        /// <param name="data"></param>
        /// <param name="offset"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public bool Read(ref byte[] data, int offset, int length)
        {
            try
            {
                if ((uint)offset + (uint)length >= Capacity)
                {
                    Marshal.Copy(IntPtrData, data, offset, length);
                    zBool = true;
                }
            }
            catch { }
            return ZBool;
        }

        /// <summary>
        /// 读取数据
        /// </summary>
        /// <returns></returns>
        public string GetData()
        {
            try
            {
                zString = GetData(EN, 0, (int)BufferLength);
            }
            catch { }
            return ZString;
        }

        /// <summary>
        /// 读取数据
        /// </summary>
        /// <param name="offset"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public string GetData(int offset, int length)
        {
            try
            {
                zString = GetData(EN, offset, length);
            }
            catch { }
            return ZString;
        }

        /// <summary>
        /// 读取数据
        /// </summary>
        /// <param name="encode">为空时默认为UTF8</param>
        /// <param name="offset"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public string GetData(Encoding encode, int offset, int length)
        {
            try
            {
                if (encode != null)
                {
                    en = encode;
                }
                Buffer = new byte[length];
                zBool = Read(ref buffer, offset, length);
                if (ZBool)
                {
                    zString = EN.GetString(Buffer).TrimEnd('\0');
                }
            }
            catch { }
            return ZString;
        }

        /// <summary>
        /// 失效
        /// </summary>
        public void Close()
        {
            try
            {
                if (m_bInit)
                {
                    UnMapViewOfFile(IntPtrData);
                    CloseHandle(IntPtrMemoryFile);
                }
            }
            catch { }
        }
    }
}
