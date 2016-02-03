using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace ZwhAid
{
    /// <summary>
    /// 基础信息类。若在同一实例中进行多个方法的操作，当需要互不影响时需要重新初始化或赋值，即不保证类型安全
    /// </summary>
    public class ZwhBase
    {
        /// <summary>
        /// 标识
        /// </summary>
        public byte flag = 0;

        /// <summary>
        /// 是与否，对与错，成功与失败
        /// </summary>
        protected bool zBool;
        /// <summary>
        /// 是与否，对与错，成功与失败
        /// </summary>
        public bool ZBool
        {
            set { zBool = value; }
            get { return zBool; }
        }

        /// <summary>
        /// 数字
        /// </summary>
        protected int zInt;
        /// <summary>
        /// 数字
        /// </summary>
        public int ZInt
        {
            set { zInt = value; }
            get { return zInt; }
        }

        /// <summary>
        /// 数字
        /// </summary>
        protected Int64 zLong;
        /// <summary>
        /// 数字
        /// </summary>
        public Int64 ZLong
        {
            set { zLong = value; }
            get { return zLong; }
        }

        /// <summary>
        /// 字符串
        /// </summary>
        protected string zString;
        /// <summary>
        /// 字符串
        /// </summary>
        public string ZString
        {
            set { zString = value; }
            get { return zString; }
        }

        /// <summary>
        /// StringBuilder
        /// </summary>
        protected StringBuilder zText = new StringBuilder();
        /// <summary>
        /// StringBuilder
        /// </summary>
        public StringBuilder ZText
        {
            set { zText = value; }
            get { return zText; }
        }

        /// <summary>
        /// DataTable
        /// </summary>
        protected DataTable zTable = new DataTable();
        /// <summary>
        /// DataTable
        /// </summary>
        public DataTable ZTable
        {
            set { zTable = value; }
            get { return zTable; }
        }

        /// <summary>
        /// DataSet
        /// </summary>
        protected DataSet zSet = new DataSet();
        /// <summary>
        /// DataSet
        /// </summary>
        public DataSet ZSet
        {
            set { zSet = value; }
            get { return zSet; }
        }

        /// <summary>
        /// Object
        /// </summary>
        protected object zObject;
        /// <summary>
        /// Object
        /// </summary>
        public object ZObject
        {
            set { zObject = value; }
            get { return zObject; }
        }

        /// <summary>
        /// List<object>
        /// </summary>
        protected List<object> zLists;
        /// <summary>
        /// List<object>
        /// </summary>
        public List<object> ZLists
        {
            set { zLists = value; }
            get { return zLists; }
        }

        /// <summary>
        /// 字节数组
        /// </summary>
        protected byte[] zBytes;
        /// <summary>
        /// 字节数组
        /// </summary>
        public byte[] ZBytes
        {
            set { zBytes = value; }
            get { return zBytes; }
        }

        /// <summary>
        /// 路径
        /// </summary>
        protected string url;
        /// <summary>
        /// 路径
        /// </summary>
        public string URL
        {
            set { url = value; }
            get { return url; }
        }
    }
}
