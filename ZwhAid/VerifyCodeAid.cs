using System;

namespace ZwhAid
{
    public class VerifyCodeAid : ZwhBase
    {
        public bool GetVerifyCode(string codeType,out string code)
        {
            code = string.Empty;

            try
            {
                Random r = new Random(1111111);
                int i = r.Next(9999999);
                code = i.ToString();
            }
            catch { }

            return ZBool;
        }
    }
}
