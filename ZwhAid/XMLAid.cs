using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;
using System.Xml;

namespace ZwhAid
{
    /// <summary>
    /// XML基本操作
    /// </summary>
    public class XMLAid : ZwhBase
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public XMLAid() { }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="file"></param>
        public XMLAid(string file)
        {
            xmlFile = file;
            if (File.Exists(file))
            {
                xd = new XmlDocument();
                xd.Load(file);
            }
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="xmlText"></param>
        public XMLAid(StringBuilder xmlText)
        {
            if (xmlText != null)
            {
                zString = xmlText.ToString();
                if (!string.IsNullOrEmpty(ZString))
                {
                    xd = new XmlDocument();
                    xd.LoadXml(ZString);
                }
            }
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="inStream"></param>
        public XMLAid(Stream inStream)
        {
            try
            {
                xd = new XmlDocument();
                xd.Load(inStream);
            }
            catch { }
        }

        private string xmlFile;
        /// <summary>
        /// XML文件
        /// </summary>
        public string XMLFile
        {
            set { xmlFile = value; }
            get { return xmlFile; }
        }

        private XmlDocument xd;
        /// <summary>
        /// XML文档
        /// </summary>
        public XmlDocument XD
        {
            set { xd = value; }
            get { return xd; }
        }

        private XmlElement xe;
        /// <summary>
        /// XML元素
        /// </summary>
        public XmlElement XE
        {
            set { xe = value; }
            get { return xe; }
        }

        private XmlNode xn;
        /// <summary>
        /// XML节点
        /// </summary>
        public XmlNode XN
        {
            set { xn = value; }
            get { return xn; }
        }

        private XmlNodeList xnl;
        /// <summary>
        /// XML节点列表
        /// </summary>
        public XmlNodeList XNL
        {
            set { xnl = value; }
            get { return xnl; }
        }

        #region 创建XML
        public bool CreateXML(string file, string name)
        {
            try
            {
                xmlFile = file;
                IOAid io = new IOAid();
                io.CreateFile(xmlFile, true);
                xe = xd.CreateElement(name);
                xd.AppendChild(xe);
                zBool = true;
            }
            catch { }

            return ZBool;
        }
        #endregion

        #region 增加
        public void AddElement(string name)
        {
            AddElement(name, null);
        }

        public void AddElement(string name, string ns)
        {
            AddElement(null, name, ns);
        }

        public void AddElement(string prefix, string name, string ns)
        {
            try
            {
                xe = xd.CreateElement(prefix, name, ns);
                xd.AppendChild(xe);
                SaveXML();
            }
            catch { }
        }

        public void AddElement(string name, object obj)
        {
            try
            {
                xe = xd.CreateElement(name);
                xe.InnerText = obj.ToString();
                xd.AppendChild(xe);
                SaveXML();
            }
            catch { }
        }

        public void AddElement(string name, Dictionary<string, object> dic, object obj)
        {
            try
            {
                xe = xd.CreateElement(name);
                foreach (KeyValuePair<string, object> kvp in dic)
                {
                    if (kvp.Value != null)
                    {
                        xe.SetAttribute(kvp.Key, kvp.Value.ToString());
                    }
                }
                xe.InnerText = obj.ToString();
                xd.AppendChild(xe);
                SaveXML();
            }
            catch { }
        }
        #endregion

        #region 查询
        public bool Exists(string xpath)
        {
            try
            {
                xn = xd.SelectSingleNode(xpath);
                if (xn != null)
                {
                    zBool = true;
                }
            }
            catch { }

            return ZBool;
        }

        public bool ExistsID(string id)
        {
            try
            {
                xe = xd.GetElementById(id);
                if (xe != null && !xe.IsEmpty)
                {
                    zBool = true;
                }
            }
            catch { }

            return ZBool;
        }

        public bool ExistsName(string name)
        {
            try
            {
                xnl = xd.GetElementsByTagName(name);
                if (xnl != null && xnl.Count > 0)
                {
                    zBool = true;
                }
            }
            catch { }

            return ZBool;
        }

        /// <summary>
        /// 是否存在对应的节点及值
        /// </summary>
        /// <param name="xmlNode">name及值</param>
        /// <returns></returns>
        public bool Exists(Dictionary<string, object> xmlNode)
        {
            try
            {
                if (xmlNode != null)
                {
                    foreach (KeyValuePair<string, object> kvp in xmlNode)
                    {
                        if (kvp.Value != null)
                        {
                            if (ExistsID(kvp.Key))
                            {
                                xnl = xd.GetElementsByTagName(kvp.Key);
                                foreach (XmlNode xn in xnl)
                                {
                                    if (xn.InnerText.Equals(kvp.Value.ToString()))
                                    {
                                        zBool = true;
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch { }

            return ZBool;
        }

        /// <summary>
        /// 获得xpath下的节点名称与值
        /// </summary>
        /// <param name="xpath"></param>
        /// <returns></returns>
        public Dictionary<string, object> GetNodeValueList(string xpath)
        {
            Dictionary<string, object> dic = new Dictionary<string, object>();
            try
            {
                xnl = xd.SelectNodes(xpath);
                if (xnl != null)
                {
                    foreach (XmlNode xn in xnl[0].ChildNodes)
                    {
                        dic.Add(xn.Name, xn.InnerText);
                    }
                }
            }
            catch { }
            return dic;
        }

        public string GetValue(string xpath)
        {
            try
            {
                xn = xd.SelectSingleNode(xpath);
                zString = xn.InnerText;
            }
            catch { }

            return zString;
        }

        public List<object> GetValueList(string xpath)
        {
            try
            {
                xnl = xd.SelectNodes(xpath);
                if (xnl != null)
                {
                    foreach (XmlNode xn in xnl)
                    {
                        zLists.Add(xn.InnerText);
                    }
                }
            }
            catch { }

            return zLists;
        }

        public DataTable GetValueTable(string xpath)
        {
            try
            {
                xnl = xd.SelectNodes(xpath);
                if (xnl != null)
                {
                    zTable = new DataTable();
                    int i = 0;
                    foreach (XmlNode xn in xnl)
                    {
                        XmlNodeList cxnl = xn.ChildNodes;
                        if (i == 0)
                        {
                            for (int j = 0; j < cxnl.Count; j++)
                            {
                                string cn = string.Empty;
                                cn = cxnl.Item(j).Name;
                                zTable.Columns.Add(cn);
                            }
                            i++;
                        }
                        if (cxnl != null)
                        {
                            DataRow dr = zTable.NewRow();
                            foreach (XmlNode cxn in cxnl)
                            {
                                for (int k = 0; k < zTable.Columns.Count; k++)
                                {
                                    if (zTable.Columns[k].ColumnName.Equals(cxn.Name))
                                    {
                                        dr[cxn.Name] = cxn.InnerText;
                                    }
                                }
                            }
                            zTable.Rows.Add(dr);
                        }
                    }
                }
            }
            catch { }

            return ZTable;
        }

        public string GetValueByName(string name)
        {
            try
            {
                xnl = xd.GetElementsByTagName(name);
                if (xnl != null)
                {
                    zString = xnl.Item(0).InnerText;
                }
            }
            catch { }

            return zString;
        }

        public List<object> GetValueListByName(string name)
        {
            try
            {
                xnl = xd.GetElementsByTagName(name);
                if (xnl != null)
                {
                    foreach (XmlNode xn in xnl)
                    {
                        zLists.Add(xn.InnerText);
                    }
                }
            }
            catch { }

            return zLists;
        }

        public string GetAttributeValue(string xpath, string attri)
        {
            try
            {
                zString = xd.SelectSingleNode(xpath).Attributes[attri].Value;
            }
            catch { }

            return ZString;
        }
        #endregion

        #region 修改
        public void UpdateValue(string xpath, object obj)
        {
            try
            {
                if (Exists(xpath))
                {
                    xn.InnerText = obj.ToString();
                    SaveXML();
                }
            }
            catch { }
        }
        #endregion

        #region 删除
        public void DeleteElement(string xpath)
        {
            try
            {
                if (Exists(xpath))
                {
                    xn.RemoveAll();
                }
            }
            catch { }
        }

        public void DeleteElement(string xpath, Dictionary<string, object> dic)
        {
            try
            {
                if (Exists(xpath))
                {
                    if (xn != null)
                    {
                        xnl = xn.ChildNodes;
                        foreach (XmlNode xnode in xnl)
                        {
                            foreach (KeyValuePair<string, object> kvp in dic)
                            {
                                if (xnode.Name.Equals(kvp.Key) && xnode.InnerText.Equals(kvp.Value.ToString()))
                                {
                                    xnode.RemoveAll();
                                }
                            }
                        }
                    }
                }
            }
            catch { }
        }
        #endregion

        /// <summary>
        /// 保存XML
        /// </summary>
        public void SaveXML()
        {
            try
            {
                if (!string.IsNullOrEmpty(xmlFile))
                {
                    xd.Save(xmlFile);
                }
            }
            catch { }
        }
    }
}
