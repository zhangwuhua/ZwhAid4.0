using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;

namespace ZwhAid
{
    public class EntityAid<TModel> where TModel : class,new()
    {
        public List<TModel> DataTableToEntity(DataTable dt)
        {
            Type type = typeof(TModel);
            List<TModel> list = new List<TModel>();

            foreach (DataRow row in dt.Rows)
            {
                PropertyInfo[] pArray = type.GetProperties();
                TModel t = new TModel();
                foreach (PropertyInfo p in pArray)
                {
                    //if (row[p.Name] is Int64)
                    //{
                    //    p.SetValue(t, Convert.ToInt32(row[p.Name]), null);
                    //    continue;
                    //}
                    if (!(row[p.Name] is DBNull))
                    {
                        p.SetValue(t, row[p.Name], null);
                    }
                }
                list.Add(t);
            }
            return list;
        }
    }
}
