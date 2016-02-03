namespace ZwhAid
{
    /// <summary>
    /// 数据条件项
    /// </summary>
    public enum ConditionItem
    {
        /// <summary>
        /// 空
        /// </summary>
        NULL = 0,
        /// <summary>
        /// 等于
        /// </summary>
        EQ =1,
        /// <summary>
        /// 不等于
        /// </summary>
        NEQ=2,
        /// <summary>
        /// 大于
        /// </summary>
        GT=3,
        /// <summary>
        /// 小于
        /// </summary>
        LT=4,
        /// <summary>
        /// 大于等于
        /// </summary>
        GTEQ=5,
        /// <summary>
        /// 小于等于
        /// </summary>
        LTEQ=6,
        /// <summary>
        /// 类似于
        /// </summary>
        LIKE =7
    }
}
