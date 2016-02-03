using System.Runtime.Serialization;

namespace ZwhAid
{
    [DataContract]
    [KnownType(typeof(WcfCustomType))]
    public class WcfCustomType
    {
        [DataMember]
        public object RequestData;
        [DataMember]
        public object ResponseData;
    }
}
