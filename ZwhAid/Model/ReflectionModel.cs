using System.Reflection;

namespace ZwhAid.Model
{
    /// <summary>
    /// 反射实体类
    /// </summary>
    public class ReflectionModel:ZwhBase
    {
        protected string cn;
        public string CN
        {
            set { cn = value; }
            get { return cn; }
        }

        protected PropertyInfo cp;
        public PropertyInfo CP
        {
            set { cp = value; }
            get { return cp; }
        }

        protected PropertyInfo[] cps;
        public PropertyInfo[] CPS
        {
            set { cps = value; }
            get { return cps; }
        }

        protected PropertyModel pm;
        public PropertyModel PM
        {
            set { pm = value; }
            get { return pm; }
        }
    }
}
