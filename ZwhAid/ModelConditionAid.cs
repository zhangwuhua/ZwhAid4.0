using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace ZwhAid
{
    /// <summary>
    /// 数据库条件组成操作，组成的条件取Condition
    /// </summary>
    /// <typeparam name="Model"></typeparam>
    public class ModelConditionAid<Model> : ZwhBase where Model : class, new()
    {
        private string condition;
        /// <summary>
        /// 条件
        /// </summary>
        public string Condition
        {
            set { condition = value; }
            get { return condition; }
        }

        /// <summary>
        /// 条件设置
        /// </summary>
        /// <param name="cli"></param>
        /// <param name="ci"></param>
        /// <param name="zkvs"></param>
        public void SetParam(ConditionLinkItem cli, ConditionItem ci, List<ZWHKV<string, DbType, object>> zkvs)
        {
            try
            {
                zText = new StringBuilder();
                zText.Append(condition);

                if (zkvs.Count > 0)
                {
                    switch (cli)
                    {
                        case ConditionLinkItem.AND:
                            for (int i = 0; i < zkvs.Count; i++)
                            {
                                zText.Append(GetCondition(ci, zkvs[i]) + " AND ");
                            }
                            break;
                    }
                }

                condition = zText.ToString();
            }
            catch { }
        }

        private string GetCondition(ConditionItem ci, ZWHKV<string, DbType, object> zkv)
        {
            try
            {
                switch (ci)
                {
                    case ConditionItem.EQ:
                        zText.Append(zkv.Key + "=" + GetDbType(zkv.Value, zkv.DbType));
                        break;
                    case ConditionItem.GT:
                        zText.Append(zkv.Key + ">" + GetDbType(zkv.Value, zkv.DbType));
                        break;
                    case ConditionItem.GTEQ:
                        zText.Append(zkv.Key + ">=" + GetDbType(zkv.Value, zkv.DbType));
                        break;
                    case ConditionItem.LT:
                        zText.Append(zkv.Key + "<" + GetDbType(zkv.Value, zkv.DbType));
                        break;
                    case ConditionItem.LTEQ:
                        zText.Append(zkv.Key + "<=" + GetDbType(zkv.Value, zkv.DbType));
                        break;
                    case ConditionItem.NEQ:
                        zText.Append(zkv.Key + "<>" + GetDbType(zkv.Value, zkv.DbType));
                        break;
                }
                zString = ZText.ToString();
            }
            catch { }
            return ZString;
        }

        private string GetDbType(object value, DbType dbt)
        {
            try
            {
                if (value != null)
                {
                    zString = string.Empty;
                    switch (dbt)
                    {
                        case DbType.Boolean:
                            zString = "'" + value + "'";
                            break;
                        case DbType.Date:
                            zString = "'" + value + "'";
                            break;
                        case DbType.DateTime:
                            zString = "'" + value + "'";
                            break;
                        case DbType.DateTime2:
                            zString = "'" + value + "'";
                            break;
                        case DbType.DateTimeOffset:
                            zString = "'" + value + "'";
                            break;
                        case DbType.Decimal:
                            zString = decimal.Parse(value.ToString()).ToString();
                            break;
                        case DbType.Double:
                            zString = Double.Parse(value.ToString()).ToString();
                            break;
                        case DbType.Guid:
                            zString = "'" + value + "'";
                            break;
                        case DbType.Int16:
                            zString = Int16.Parse(value.ToString()).ToString();
                            break;
                        case DbType.Int32:
                            zString = Int32.Parse(value.ToString()).ToString();
                            break;
                        case DbType.Int64:
                            zString = Int64.Parse(value.ToString()).ToString();
                            break;
                        case DbType.String:
                            zString = "'" + value + "'";
                            break;
                        case DbType.Time:
                            zString = "'" + value + "'";
                            break;
                        case DbType.UInt16:
                            zString = UInt16.Parse(value.ToString()).ToString();
                            break;
                        case DbType.UInt32:
                            zString = UInt32.Parse(value.ToString()).ToString();
                            break;
                        case DbType.UInt64:
                            zString = UInt64.Parse(value.ToString()).ToString();
                            break;
                    }
                }
            }
            catch { }
            return ZString;
        }
    }
}
