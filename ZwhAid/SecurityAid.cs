using System.Net;
using System.ServiceModel.Web;

namespace ZwhAid
{
    public class SecurityAid : ZwhBase
    {
        public bool CheckAuthorization(string auths)
        {
            var ctx = WebOperationContext.Current;
            var auth = ctx.IncomingRequest.Headers[HttpRequestHeader.Authorization];
            if (string.IsNullOrEmpty(auth) || auth != "fangxing/123")
            {
                ctx.OutgoingResponse.StatusCode = HttpStatusCode.MethodNotAllowed;
                return false;
            }
            return true;
        }
    }
}
