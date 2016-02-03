using System.IO;
using System.Web;

namespace ZwhAid
{
    public class UpDownAid : ZwhBase
    {
        //public void DownLoad(HttpRequest request, HttpResponse response, string name, string path, long speed)
        //{
        //    try
        //    {
        //        if (string.IsNullOrEmpty(path))
        //        {
        //            path = System.Web.HttpContext.Current.Server.MapPath(name);
        //        }
        //        //BinaryReader br
        //        #region response
        //        response.Clear();
        //        response.AddHeader("Accept-Ranges", "bytes");
        //        response.AddHeader("Content-Length", "");
        //        response.AddHeader("Content-Range", "");
        //        response.AddHeader("Connection", "Keep-Alive");
        //        response.AddHeader("Content-Type", "application/octet-stream");
        //        response.AddHeader("Content-Disposition", "attachment;filename=" + path);
        //        //response.TransmitFile();
        //        //response.BinaryWrite();
        //        response.End();
        //        #endregion
        //    }
        //    catch { }
        //}

        private void DownLoad(HttpResponse response, string name, string path)
        {

            try
            {
                if (string.IsNullOrEmpty(path))
                {
                    path = System.Web.HttpContext.Current.Server.MapPath(name);
                }

                //以字符流的形式下载文件
                FileStream fs = new FileStream(path, FileMode.Open);
                byte[] bytes = new byte[(int)fs.Length];
                fs.Read(bytes, 0, bytes.Length);
                fs.Close();
                response.ContentType = "application/octet-stream";
                //通知浏览器下载文件而不是打开
                response.AddHeader("Content-Disposition", "attachment; filename=" + HttpUtility.UrlEncode(name, System.Text.Encoding.UTF8));
                response.BinaryWrite(bytes);
                response.Flush();
                response.End();
            }
            catch { }
        }

        public void UpLoad()
        {
        }
    }
}
