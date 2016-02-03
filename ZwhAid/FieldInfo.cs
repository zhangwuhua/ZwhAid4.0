using System;

namespace ZwhAid
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true, Inherited = true)]
    public class FieldInfo : Attribute
    {
        private string type;
        public string Type
        {
            set { type = value; }
            get { return type; }
        }

        private int length;
        public int Length
        {
            set { length = value; }
            get { return length; }
        }

        private string des;
        public string Des
        {
            set { des = value; }
            get { return des; }
        }

        private bool isoutparam;
        public bool Isoutparam
        {
            set { isoutparam = value; }
            get { return isoutparam; }
        }
    }
}
