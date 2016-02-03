using System;

namespace ZwhAid
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Property, AllowMultiple = true, Inherited = true)]
    public class TableInfo : Attribute
    {
        private string tableName;
        public string TableName
        {
            set { tableName = value; }
            get { return tableName; }
        }

        private string primaryKey;
        public string PrimaryKey
        {
            set { primaryKey = value; }
            get { return primaryKey; }
        }
    }
}
