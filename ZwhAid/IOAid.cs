using System;
using System.Collections.Generic;
using System.IO;

namespace ZwhAid
{
    /// <summary>
    /// IO基本操作
    /// </summary>
    public class IOAid : ZwhBase
    {
        public IOAid() { }

        public IOAid(string path)
        {
            fileFullPath = path;
            if (fileFullPath.Contains("\\"))
            {
                filePath = fileFullPath.Substring(0, fileFullPath.LastIndexOf("\\"));
                fileName = fileFullPath.Substring(fileFullPath.LastIndexOf("\\"));
                if (filePath.Contains("."))
                {
                    fileName = fileName.Substring(0, fileName.Length - fileName.LastIndexOf(".") - 1);
                }
            }
        }

        private string fileName;
        public string FileName
        {
            set { fileName = value; }
            get { return fileName; }
        }

        private string filePath;
        public string FilePath
        {
            set { filePath = value; }
            get { return filePath; }
        }

        private string fileFullPath;
        public string FileFullPath
        {
            set { fileFullPath = value; }
            get { return fileFullPath; }
        }

        /// <summary>
        /// IOAid所在类库的路径
        /// </summary>
        /// <returns></returns>
        public string GetRootPath()
        {
            string path = string.Empty;

            try
            {
                path = AppDomain.CurrentDomain.BaseDirectory;
                filePath = path;
            }
            catch { }

            return path;
        }

        public string GetMapPath(string path)
        {
            try
            {
                zString = System.Web.HttpContext.Current.Server.MapPath(path);
                filePath = path;
            }
            catch { }

            return zString;
        }

        public bool CreateDirectory()
        {
            try
            {
                if (!Directory.Exists(filePath))
                {
                    Directory.CreateDirectory(filePath);
                }
                if (Directory.Exists(filePath))
                {
                    zBool = true;
                }
            }
            catch { }

            return ZBool;
        }

        public bool CreateDirectory(string directory)
        {
            try
            {
                filePath = directory;
                zBool = CreateDirectory();
            }
            catch { }

            return ZBool;
        }

        public bool CreateFile(bool cover)
        {
            try
            {
                if (cover)
                {
                    if (File.Exists(fileFullPath))
                    {
                        File.Delete(fileFullPath);
                    }
                    if (!File.Exists(fileFullPath))
                    {
                        using (StreamWriter sw = File.CreateText(fileFullPath))
                        {
                            sw.Close();
                            zBool = true;
                        }
                    }
                }
            }
            catch { }

            return ZBool;
        }

        public bool CreateFile(string file, bool cover)
        {
            try
            {
                fileFullPath = file;
                zBool = CreateFile(cover);
            }
            catch { }

            return ZBool;
        }

        public void AppendText(string text)
        {
            try
            {
                if (!File.Exists(fileFullPath))
                {
                    CreateFile(true);
                }
                if (File.Exists(fileFullPath))
                {
                    FileStream fs = new FileStream(fileFullPath, FileMode.Append, FileAccess.Write);
                    using (StreamWriter sw = new StreamWriter(fs))
                    {
                        sw.Write(text);
                        sw.Close();
                    }
                }
            }
            catch { }
        }

        public void AppendLineText(string text)
        {
            try
            {
                if (!File.Exists(fileFullPath))
                {
                    CreateFile(true);
                }
                if (File.Exists(fileFullPath))
                {
                    FileStream fs = new FileStream(fileFullPath, FileMode.Append, FileAccess.Write);
                    using (StreamWriter sw = new StreamWriter(fs))
                    {
                        sw.WriteLine(text);
                        sw.Close();
                    }
                }
            }
            catch { }
        }

        public string ReadAllText()
        {
            try
            {
                FileStream fs = new FileStream(fileFullPath, FileMode.Open, FileAccess.Read);
                using (StreamReader sr = new StreamReader(fs))
                {
                    zString = sr.ReadToEnd();
                    sr.Close();
                }
            }
            catch { }

            return zString;
        }

        public List<object> ReadLineText()
        {
            try
            {
                FileStream fs = new FileStream(fileFullPath, FileMode.Open, FileAccess.Read);
                using (StreamReader sr = new StreamReader(fs))
                {
                    while (!string.IsNullOrEmpty(sr.ReadLine()))
                    {
                        zLists.Add(sr.ReadLine());
                    }
                    sr.Close();
                }
            }
            catch { }

            return zLists;
        }

        public string ReadLastLine()
        {
            try
            {
                FileStream fs = new FileStream(fileFullPath, FileMode.Open, FileAccess.Read);
                using (StreamReader sr = new StreamReader(fs))
                {
                    while (sr.EndOfStream)
                    {
                        zString = sr.ReadLine();
                    }
                    sr.Close();
                }
            }
            catch { }

            return zString;
        }

        public byte[] ReadByteText()
        {
            try
            {
                zBytes = new byte[8192];
                FileStream fs = new FileStream(fileFullPath, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite);
                BinaryReader br = new BinaryReader(fs);
                br.Read(zBytes, 0, zBytes.Length);
            }
            catch { }

            return zBytes;
        }
    }
}
