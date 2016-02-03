namespace ZwhAid
{
    /// <summary>
    /// 返回为HTML时不包括最外层定义
    /// </summary>
    public enum BllReturnType
    {
        /// <summary>
        /// 默认C#DataTable序列化文本
        /// </summary>
        NULL,
        XML,
        JSON,
        HTMLTABLE,
        HTMLTBODY,
        HTMLSELECT,
        HTMLSELECTOPTION,
        HTMLP,
        HTMLLABEL,
        HTMLSPAN,
        HTMLINPUT,
        HTML
    }
}
