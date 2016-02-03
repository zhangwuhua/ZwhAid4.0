using System;
using System.Web;
using System.Web.SessionState;

namespace ZwhAid
{
    public class ASPNETSessionId : SessionIDManager
    {
        public override string CreateSessionID(HttpContext context)
        {
            return Guid.NewGuid().ToString();
        }

        public override bool Validate(string id)
        {
            try
            {
                Guid guid = new Guid(id);
                if (id == guid.ToString())
                    return true;
            }
            catch
            {
            }

            return false;
        }
    }
}
