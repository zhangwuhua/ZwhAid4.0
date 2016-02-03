using System.Web;

namespace ZwhAid
{
    public class SafetyVerifyAid:ZwhBase
    {
        public bool SSLVerify(HttpContext context)
        {
            try
            {
                HttpClientCertificate hcc = context.Request.ClientCertificate;

            }
            catch { }

            return ZBool;
        }
    }
}
