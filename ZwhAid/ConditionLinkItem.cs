namespace ZwhAid
{
    /// <summary>
    /// 数据条件连接项
    /// </summary>
    public enum ConditionLinkItem
    {
        /// <summary>
        /// 空
        /// </summary>
        NULL=0,
        /// <summary>
        /// 与
        /// </summary>
        AND = 1,
        /// <summary>
        /// 或者
        /// </summary>
        OR = 2,
        /// <summary>
        /// 连接
        /// </summary>
        JOIN=3,
        /// <summary>
        /// 左连接
        /// </summary>
        LJOIN=4,
        /// <summary>
        /// 右连接
        /// </summary>
        RJOIN=5
    }
}
