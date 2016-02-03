using System;
using System.Reflection;

namespace ZwhAid
{
    public class GenerRefAid<T> : ZwhBase where T : class
    {
        private PropertyInfo[] properties;
        public string GetClassName(T t)
        {
            ZwhBase zb = new ZwhBase();

            try
            {
                Type type = typeof(T);
                zb.ZString = type.Name;
                properties = type.GetProperties();
            }
            catch { }

            return zb.ZString;
        }
    }
}
