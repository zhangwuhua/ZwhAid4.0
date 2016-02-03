namespace ZwhAid.Model
{
    /// <summary>
    /// 反射Property实体类
    /// </summary>
    public class PropertyModel:ZwhBase
    {
        protected string pn;
        public string PN
        {
            set { pn = value; }
            get { return pn; }
        }

        protected object pv;
        public object PV
        {
            set { pv = value; }
            get { return pv; }
        }

        protected string pt;
        public string PT
        {
            set { pt = value; }
            get { return pt; }
        }
    }
}
