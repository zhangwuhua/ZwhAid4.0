using System;
using System.Collections.Generic;
using System.Reflection;

using ZwhAid.Model;

namespace ZwhAid
{
    /// <summary>
    /// 反射操作
    /// </summary>
    public class ReflectionAid<Model> : ReflectionModel where Model : class, new()
    {
        public List<ReflectionModel> GetReflexInfo(Model model)
        {
            List<ReflectionModel> rms = new List<ReflectionModel>();
            try
            {
                ReflectionModel rm = new ReflectionModel();
                GetMemberInfo();
                if (CPS != null && CPS.Length > 0)
                {
                    for (int i = 0; i < CPS.Length; i++)
                    {
                        rm = new ReflectionModel();
                        rm.CN = CN;
                        rm.CPS = CPS;
                        rm.PM = GetProperty(model, CPS[i]);
                        rms.Add(rm);
                    }
                }
                else
                {
                    rm.CN = CN;
                    rm.CPS = CPS;
                    rms.Add(rm);
                }
            }
            catch { }
            return rms;
        }

        public void GetMemberInfo()
        {
            Type t = typeof(Model);
            cn = t.Name;
            cps = t.GetProperties();
        }

        public PropertyModel GetProperty(Model model, PropertyInfo pi)
        {
            PropertyModel pm = new PropertyModel();
            try
            {
                pm.PN = pi.Name;
                pm.PV = pi.GetValue(model, null);
                pm.PT = pi.PropertyType.FullName;
                this.pm = pm;
            }
            catch { }
            return pm;
        }
    }
}
