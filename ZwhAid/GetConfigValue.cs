using System;
using System.IO;
using System.Xml;

namespace ZwhAid
{
    /// <summary>
    /// 获取zwh.xml配置文件的值
    /// </summary>
    public static class GetConfigValue
    {
        /// <summary>
        /// 获得zwh.config中特定key的值
        /// </summary>
        /// <param name="appKey">zwh.config中特定key或ZWH节点下的xpath</param>
        /// <returns>zwh.config中特定key对应的值</returns>
        public static string GetAppValue(string appKey)
        {
            string app = string.Empty;

            try
            {
                string path = string.Empty;
                path = AppDomain.CurrentDomain.BaseDirectory;
                if (!string.IsNullOrEmpty(path))
                {
                    if (path.EndsWith("\\"))
                    {
                        path = path + "zwh.xml";
                        if (File.Exists(path))
                        {
                            try
                            {
                                XMLAid xa = new XMLAid(path);
                                app = xa.GetValue(@"/ZWH/" + appKey + "");
                            }
                            catch (XmlException ex)
                            {
                                throw new XmlException(ex.Message);
                            }
                        }
                    }
                    else
                    {
                        if (File.Exists(path + @"\zwh.xml"))
                        {
                            try
                            {
                                XMLAid xa = new XMLAid(path);
                                app = xa.GetValue(@"/ZWH/" + appKey + "");
                            }
                            catch (XmlException ex)
                            {
                                throw new XmlException(ex.Message);
                            }
                        }
                    }
                }
            }
            catch { }

            return app;
        }
    }
}
