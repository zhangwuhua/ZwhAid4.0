using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZwhAid
{
    public enum ZwhQuery
    {
        /// <summary>
        /// 模糊
        /// </summary>
        Fuzzy,
        /// <summary>
        /// 精确
        /// </summary>
        Accurate,
        /// <summary>
        /// Null，非查询所用
        /// </summary>
        Null
    }
}
