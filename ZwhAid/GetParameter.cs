using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;

namespace ZwhAid
{
    public class GetParameter<Model> where Model : class,new()
    {
        public static void GetSQLParameter(PropertyInfo[] properties, Model model, out List<SqlParameter> lsp)
        {
            lsp = new List<SqlParameter>();

            try
            {
                foreach (PropertyInfo pi in properties)
                {
                    string fn = pi.Name;

                    SqlParameter sp = new SqlParameter();
                    sp.ParameterName = "@" + fn;

                    object obj = pi.GetValue(model, null);
                    if (obj != null)
                    {
                        if (pi.PropertyType.FullName.Contains("System.Boolean"))
                        {
                            if (!string.IsNullOrEmpty(obj.ToString()))
                            {
                                sp.DbType = DbType.Boolean;
                                sp.SqlValue = (Boolean)obj;
                                lsp.Add(sp);
                            }
                        }
                        else
                        {
                            switch (pi.PropertyType.FullName)
                            {
                                case "System.String":
                                    if (!string.IsNullOrEmpty(obj.ToString()))
                                    {
                                        sp.DbType = DbType.String;
                                        sp.SqlValue = obj.ToString();
                                        lsp.Add(sp);
                                    }
                                    break;
                                case "System.Boolean":
                                    if (!string.IsNullOrEmpty(obj.ToString()))
                                    {
                                        sp.DbType = DbType.Boolean;
                                        sp.SqlValue = (Boolean)obj;
                                        lsp.Add(sp);
                                    }
                                    break;
                                case "System.Byte":
                                    if (!string.IsNullOrEmpty(obj.ToString()) && !obj.ToString().Equals("0"))
                                    {
                                        sp.DbType = DbType.Byte;
                                        sp.SqlValue = (Byte)obj;
                                        lsp.Add(sp);
                                    }
                                    break;
                                case "System.Int16":
                                    if (!string.IsNullOrEmpty(obj.ToString()) && !obj.ToString().Equals("0"))
                                    {
                                        sp.DbType = DbType.Int16;
                                        sp.SqlValue = (Int16)obj;
                                        lsp.Add(sp);
                                    }
                                    break;
                                case "System.Int32":
                                    if (!string.IsNullOrEmpty(obj.ToString()) && !obj.ToString().Equals("0"))
                                    {
                                        sp.DbType = DbType.Int32;
                                        sp.SqlValue = (Int32)obj;
                                        lsp.Add(sp);
                                    }
                                    break;
                                case "System.Int64":
                                    if (!string.IsNullOrEmpty(obj.ToString()) && !obj.ToString().Equals("0"))
                                    {
                                        sp.DbType = DbType.Int64;
                                        sp.SqlValue = (Int64)obj;
                                        lsp.Add(sp);
                                    }
                                    break;
                                case "System.int":
                                    if (!string.IsNullOrEmpty(obj.ToString()) && !obj.ToString().Equals("0"))
                                    {
                                        sp.DbType = DbType.Int32;
                                        sp.SqlValue = (int)obj;
                                        lsp.Add(sp);
                                    }
                                    break;
                                case "System.Guid":
                                    if (!obj.ToString().Equals(Guid.Empty.ToString()) && !string.IsNullOrEmpty(obj.ToString()))
                                    {
                                        sp.DbType = DbType.Guid;
                                        sp.SqlValue = Guid.Parse(obj.ToString());
                                        lsp.Add(sp);
                                    }
                                    break;
                                case "System.DateTime":
                                    if (!obj.ToString().Equals(DateTime.MinValue.ToString()) && !string.IsNullOrEmpty(obj.ToString()))
                                    {
                                        sp.DbType = DbType.DateTime;
                                        sp.SqlValue = obj.ToString();
                                        lsp.Add(sp);
                                    }
                                    break;
                            }
                        }
                    }
                }
            }
            catch { }
        }

        public static void GetSQLParameterEmpty(Assembly aby,PropertyInfo[] properties, Model model, out List<SqlParameter> lsp)
        {
            lsp = new List<SqlParameter>();

            //try
            //{
            //    foreach (PropertyInfo pi in properties)
            //    {
            //        string fn = pi.Name;

            //        SqlParameter sp = new SqlParameter();
            //        sp.ParameterName = "@" + fn;

            //        object[] attrs = pi.GetCustomAttributes(typeof(SMEChinaModel.FieldInfo), true);//与获得的实体类在一个类库，或者引入
            //        if (attrs.Length > 0)
            //        {
            //            SMEChinaModel.FieldInfo attr = attrs[0] as SMEChinaModel.FieldInfo;
            //            if (attr != null)
            //            {
            //                if (attr.Isoutparam)
            //                {
            //                    sp.Direction = ParameterDirection.Output;
            //                }
            //                int length;
            //                if (attr.Type.ToLower().StartsWith("n"))
            //                {
            //                    length = attr.Length / 2;
            //                }
            //                else
            //                {
            //                    length = attr.Length;
            //                }
            //                sp.Size = length;
            //            }
            //        }


            //        object obj = pi.GetValue(model, null);
            //        if (pi.PropertyType.FullName.Contains("System.Boolean"))
            //        {
            //            sp.DbType = DbType.Boolean;
            //            if (obj != null)
            //            {
            //                sp.SqlValue = (Boolean)obj;
            //            }
            //            else
            //            {
            //                sp.SqlValue = DBNull.Value;
            //            }
            //            lsp.Add(sp);
            //        }
            //        else
            //        {
            //            switch (pi.PropertyType.FullName)
            //            {
            //                case "System.String":
            //                    sp.DbType = DbType.String;
            //                    if (obj != null)
            //                    {
            //                        sp.SqlValue = obj.ToString();
            //                    }
            //                    else
            //                    {
            //                        sp.SqlValue = DBNull.Value;
            //                    }
            //                    lsp.Add(sp);
            //                    break;
            //                case "System.Boolean":
            //                    sp.DbType = DbType.Boolean;
            //                    if (obj != null)
            //                    {
            //                        sp.SqlValue = (Boolean)obj;
            //                    }
            //                    else
            //                    {
            //                        sp.SqlValue = DBNull.Value;
            //                    }
            //                    lsp.Add(sp);
            //                    break;
            //                case "System.Byte":
            //                    sp.DbType = DbType.Byte;
            //                    if (obj != null)
            //                    {
            //                        sp.SqlValue = (Byte)obj;
            //                    }
            //                    else
            //                    {
            //                        sp.SqlValue = DBNull.Value;
            //                    }
            //                    lsp.Add(sp);
            //                    break;
            //                case "System.Int16":
            //                    sp.DbType = DbType.Int16;
            //                    if (obj != null)
            //                    {
            //                        sp.SqlValue = (Int16)obj;
            //                    }
            //                    else
            //                    {
            //                        sp.SqlValue = DBNull.Value;
            //                    }
            //                    lsp.Add(sp);
            //                    break;
            //                case "System.Int32":
            //                    sp.DbType = DbType.Int32;
            //                    if (obj != null)
            //                    {
            //                        sp.SqlValue = (Int32)obj;
            //                    }
            //                    else
            //                    {
            //                        sp.SqlValue = DBNull.Value;
            //                    }
            //                    lsp.Add(sp);
            //                    break;
            //                case "System.Int64":
            //                    sp.DbType = DbType.Int64;
            //                    if (obj != null)
            //                    {
            //                        sp.SqlValue = (Int64)obj;
            //                    }
            //                    else
            //                    {
            //                        sp.SqlValue = DBNull.Value;
            //                    }
            //                    lsp.Add(sp);
            //                    break;
            //                case "System.int":
            //                    sp.DbType = DbType.Int32;
            //                    if (obj != null)
            //                    {
            //                        sp.SqlValue = (int)obj;
            //                    }
            //                    else
            //                    {
            //                        sp.SqlValue = DBNull.Value;
            //                    }
            //                    lsp.Add(sp);
            //                    break;
            //                case "System.Guid":
            //                    sp.DbType = DbType.Guid;
            //                    if (obj != null && Guid.Parse(obj.ToString()) != Guid.Empty)
            //                    {
            //                        sp.SqlValue = Guid.Parse(obj.ToString());
            //                    }
            //                    else
            //                    {
            //                        sp.SqlValue = DBNull.Value;
            //                    }
            //                    lsp.Add(sp);
            //                    break;
            //                case "System.DateTime":
            //                    sp.DbType = DbType.DateTime;
            //                    if (obj != null)
            //                    {
            //                        sp.SqlValue = obj.ToString();
            //                    }
            //                    else
            //                    {
            //                        sp.SqlValue = DBNull.Value;
            //                    }
            //                    lsp.Add(sp);
            //                    break;
            //                default:
            //                    sp.DbType = DbType.String;
            //                    if (obj != null)
            //                    {
            //                        sp.SqlValue = obj.ToString();
            //                    }
            //                    else
            //                    {
            //                        sp.SqlValue = DBNull.Value;
            //                    }
            //                    lsp.Add(sp);
            //                    break;
            //            }
            //        }
            //    }
            //}
            //catch { }
        }
    }
}
