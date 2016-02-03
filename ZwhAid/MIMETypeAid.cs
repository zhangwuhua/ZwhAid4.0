namespace ZwhAid
{
    public static class MIMEType
    {
        public static string GetMIME(MIMEItem me)
        {
            switch (me)
            {
                case MIMEItem.bin:
                    return "application/octet-stream";
                case MIMEItem.html:
                    return "text/html";
                case MIMEItem.json:
                    return "application/json";
                case MIMEItem.text:
                    return "text/plain";
                default:
                    return "application/octet-stream";
            }
        }
    }
}
