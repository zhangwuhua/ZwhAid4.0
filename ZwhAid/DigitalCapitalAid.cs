namespace ZwhAid
{
    public class DigitalCapitalAid : ZwhBase
    {
        public string DC(string digital)
        {
            string foreString = string.Empty;
            string backString = string.Empty;
            if(!string.IsNullOrEmpty(digital))
            {
                if (digital.Contains("."))
                {
                    foreString = digital.Substring(0, digital.IndexOf(".") + 1);
                    backString = digital.Substring(digital.IndexOf(".") + 1);
                }
                else
                {
                    foreString = digital;
                }
            }

            return ZString;
        }

        public string DigitalCapital(string digital)
        {
            if (!string.IsNullOrEmpty(digital))
            {
                switch (digital)
                {
                    case "1":
                        zString = "壹";
                        break;
                    case "2":
                        zString = "贰";
                        break;
                    case "3":
                        zString = "叁";
                        break;
                    case "4":
                        zString = "肆";
                        break;
                    case "5":
                        zString = "伍";
                        break;
                    case "6":
                        zString = "陆";
                        break;
                    case "7":
                        zString = "柒";
                        break;
                    case "8":
                        zString = "捌";
                        break;
                    case "9":
                        zString = "玖";
                        break;
                    case "0":
                        zString = "零";
                        break;
                    case "10":
                        zString = "拾";
                        break;
                    case "100":
                        zString = "佰";
                        break;
                    case "1000":
                        zString = "仟";
                        break;
                    case "10000":
                        zString = "万";
                        break;
                    case "100000000":
                        zString = "亿";
                        break;
                    case "十":
                        zString = "拾";
                        break;
                    case "百":
                        zString = "佰";
                        break;
                    case "千":
                        zString = "仟";
                        break;
                    case "万":
                        zString = "万";
                        break;
                    case "亿":
                        zString = "亿";
                        break;
                }
            }
            return ZString;
        }
    }
}
