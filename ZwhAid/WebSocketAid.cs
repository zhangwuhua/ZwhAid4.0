using System;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace ZwhAid
{
    /// <summary>
    /// WebSocket操作
    /// </summary>
    public class WebSocketAid : ZwhBase
    {
        private string sk = @"Sec\-WebSocket\-Key:(.*?)\r\n";
        /// <summary>
        /// Sec-WebSocket-Key正则表达式
        /// </summary>
        public string SK
        {
            set { sk = value; }
            get { return sk; }
        }

        private string mk = "258EAFA5-E914-47DA-95CA-C5AB0DC85B11";
        /// <summary>
        /// 
        /// </summary>
        public string MK
        {
            set { mk = value; }
            get { return mk; }
        }

        /// <summary>
        /// 生成Sec-WebSocket-Accept Key
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        public string SecurityKey(string msg)
        {
            try
            {
                string text = string.Empty;
                text = msg.TrimEnd('\0');
                Match match = Regex.Match(text, SK);
                if (match != null)
                {
                    string key = Regex.Replace(match.Value, SK, "$1").Trim();
                    if (!string.IsNullOrEmpty(key))
                    {
                        zBytes = SHA1.Create().ComputeHash(Encoding.ASCII.GetBytes(key + mk));
                        if (ZBytes != null)
                        {
                            zString = Convert.ToBase64String(ZBytes);
                        }
                    }
                }
            }
            catch { }
            return ZString;
        }

        /// <summary>
        /// 打包WebSocket响应header
        /// </summary>
        /// <param name="key">Sec-WebSocket-Accept Key</param>
        /// <returns></returns>
        public string PackSK(string key)
        {
            try
            {
                zText = new StringBuilder();
                zText.Append("HTTP/1.1 101 Switching Protocols" + Environment.NewLine);
                zText.Append("Upgrade: websocket" + Environment.NewLine);
                zText.Append("Connection: Upgrade" + Environment.NewLine);
                zText.Append("Sec-WebSocket-Accept: " + key + Environment.NewLine + Environment.NewLine);
                zString = zText.ToString();
            }
            catch { }
            return ZString;
        }
    }
}
