namespace ZwhAid.Model
{
    /// <summary>
    /// Excel操作实体类
    /// </summary>
    public class ExcelModel
    {
        /// <summary>
        /// Excel文件名
        /// </summary>
        protected string excelName;
        /// <summary>
        /// Excel文件名
        /// </summary>
        public string ExcelName
        {
            set { excelName = value; }
            get { return excelName; }
        }

        /// <summary>
        /// Excel Sheet文件名
        /// </summary>
        protected string sheetName;
        /// <summary>
        /// Excel Sheet文件名
        /// </summary>
        public string SheetName
        {
            set { sheetName = value; }
            get { return sheetName; }
        }
    }
}
