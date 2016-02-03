using System.Text.RegularExpressions;

namespace ZwhAid
{
    public class RegularAid : ZwhBase
    {
        #region 正则表达式
        static string email = @"^[0-9a-zA-Z_\.\-]+|\w+@[0-9a-zA-Z\-]+.([a-zA-Z0-9]{2,5})+$";
        //static string mobile = @"\d{3}?\d{11}";
        static string dianxin = @"^1[3578][01379]\d{8}$";
        static string liantong = @"^1[3578][01379]\d{8}$";
        static string yidong = @"^(134[012345678]\d{7}|1[34578][012356789]\d{8})$";   
        static string tele = @"^(\d{7,8}|\d{3,4}-\d{7,8}|\d{3,4}-\d{7,8}-\d{1,4}|\d{7,8}-\d{1,4})$";
        static string ipv4 = @"^(2[0-4]\d|25[0-5]|[01]?\d\d?\.){3}2[0-4]\d|25[0-5]|[01]?\d\d?$";
        static string qq = @"[1-9]\d{4,}";
        static string zipcode = @"[1-9]\d{5}(?!\d)";
        #endregion

        #region 输入的字符串与对应格式比较匹配
        public bool IsEmail(string input)
        {
            try
            {
                zBool = Regex.IsMatch(input, email);
            }
            catch { }

            return ZBool;
        }

        public bool IsMobile(string input)
        {
            try
            {
                if (Regex.IsMatch(input, dianxin) || Regex.IsMatch(input, liantong) || Regex.IsMatch(input, yidong))
                {
                    zBool = true;
                }
                else
                {
                    zBool = false;
                }
            }
            catch { }

            return ZBool;
        }

        public bool IsTele(string input)
        {
            try
            {
                zBool = Regex.IsMatch(input, tele);
            }
            catch { }

            return ZBool;
        }

        public bool IsIPv4(string input)
        {
            try
            {
                zBool = Regex.IsMatch(input, ipv4);
            }
            catch { }

            return ZBool;
        }

        public bool IsQQ(string input)
        {
            try
            {
                zBool = Regex.IsMatch(input, qq);
            }
            catch { }

            return ZBool;
        }

        public bool IsZipcode(string input)
        {
            try
            {
                zBool = Regex.IsMatch(input, zipcode);
            }
            catch { }

            return ZBool;
        }
        #endregion
    }
}
