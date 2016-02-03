using System.Net.Security;
using System.Security.Cryptography.X509Certificates;

namespace ZwhAid
{
    /// <summary>
    /// SSL/TSL操作
    /// </summary>
    public class SSLAid : ZwhBase
    {
        private string cerName;

        public SSLAid() { }

        public SSLAid(string fileName)
        {
            cerName = fileName;
        }

        public byte[] Read()
        {
            try
            {
                IOAid io = new IOAid(cerName);
                zBytes = io.ReadByteText();
            }
            catch { }
            return ZBytes;
        }

        public byte[] Read(string fileName)
        {
            try
            {
                cerName = fileName;
                Read();
            }
            catch { }
            return ZBytes;
        }

        public X509Certificate GetX509()
        {
            X509Certificate xc = new X509Certificate();
            try
            {
                byte[] x509 = Read();
                xc.Import(x509, "", X509KeyStorageFlags.DefaultKeySet);
            }
            catch { }
            return xc;
        }

        public X509Certificate2 GetX5092()
        {
            X509Certificate2 xc = new X509Certificate2();
            try
            {
                byte[] x509 = Read();
                xc.Import(x509, "", X509KeyStorageFlags.DefaultKeySet);
            }
            catch { }
            return xc;
        }

        public X509Store GetX509Store()
        {
            X509Store store = new X509Store();
            try
            {
                X509Certificate2 x5092 = GetX5092();
                store.Open(OpenFlags.MaxAllowed);
                store.Add(x5092);
                store.Close();
            }
            catch { }
            return store;
        }

        public void HttpSSL(string requestURL, string certURL)
        {
            //Uri uri = new Uri(requestURL.Replace("http", "https"));
            //HttpWebRequest httpRequest = (HttpWebRequest)HttpWebRequest.Create(uri);
            //X509Certificate cer = X509Certificate.CreateFromCertFile(certURL);
            //httpRequest.ClientCertificates.Add(cer);
            //HttpWebResponse response = (HttpWebResponse)httpRequest.GetResponse();
            //Stream stream = response.GetResponseStream();

            ////验证服务器证书回调方法 
            //ServicePointManager.ServerCertificateValidationCallback = new System.Net.Security.RemoteCertificateValidationCallback(CheckValidationResult);
            ////创建HttpWebRequest对象 
            //HttpWebRequest https = (HttpWebRequest)HttpWebRequest.Create(requestURL.Replace("http", "https"));// ("https://localhost/restful/test/BBBAAA");
            ////创建证书 
            ////X509Certificate obj509 = new X509Certificate(AppDomain.CurrentDomain.BaseDirectory + "cert\\ccc.cer");//写入正确的证书路径
            //X509Certificate obj509 = new X509Certificate(certURL);//写入正确的证书路径
            ////添加证书到HTTP请求中 
            //https.ClientCertificates.Add(obj509);
            //https.Method = "GET";
            ////获取请求返回的数据 
            //HttpWebResponse response = (HttpWebResponse)https.GetResponse();
            ////读取返回的信息 
            //StreamReader sr = new StreamReader(response.GetResponseStream(), true);
            //int count;
            //char[] ReadBuf = new char[1024];
            //do
            //{
            //    count = sr.Read(ReadBuf, 0, 1024);
            //    if (0 != count)
            //    {
            //        //Label3.Text = new string(ReadBuf);
            //    }
            //} while (count > 0);
        }

        //重写证书验证方法，总是返回TRUE，解决未能为SSL/TLS安全通道建立信任关系的问题 
        public bool CheckValidationResult(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors errors)
        {
            //总是返回TRUE 
            return true;
        }
    }
}
