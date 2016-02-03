using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace ZwhAid
{
    /// <summary>
    /// 自定义键值数据类型
    /// </summary>
    [Serializable]
    public class ZWHKV
    {
    }

    /// <summary>
    /// 自定义键值数据类型
    /// </summary>
    /// <typeparam name="ZKey"></typeparam>
    /// <typeparam name="ZDbType"></typeparam>
    /// <typeparam name="ZValue"></typeparam>
    [Serializable]
    public class ZWHKV<ZKey,ZDbType,ZValue>
    {
        private ZKey key;
        public ZKey Key
        {
            get { return key; }
        }

        private ZDbType dbType;
        public ZDbType DbType
        {
            get { return dbType; }
        }

        private ZValue value;
        public ZValue Value
        {
            get { return value; }
        }

        public ZWHKV(ZKey key, ZDbType dbType, ZValue value)
        {
            this.key = key;
            this.dbType = dbType;
            this.value = value;
        }

        public override string ToString()
        {
            StringBuilder s =new  StringBuilder();
            s.Append('[');
            if (Key != null)
            {
                s.Append(Key.ToString());
            }
            s.Append(", ");
            if (DbType != null)
            {
                s.Append(DbType.ToString());
            }
            s.Append(", ");
            if (Value != null)
            {
                s.Append(Value.ToString());
            }
            s.Append(']');
            return s.ToString();
        }
    }
}
