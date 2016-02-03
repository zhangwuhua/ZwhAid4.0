
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZwhAid
{
    /// <summary>
    /// 1、获得数据库类别，
    /// 获得方式：内存、zwh.xml，
    /// 内存地址名称为无-的32位GUID或自定义唯一标识符，
    /// 内存byte长度：2；
    /// 
    /// 2、获得数据库连接字符串，
    /// 获得方式：内存、zwh.xml，
    /// 内存地址名称为无-的32位GUID或自定义唯一标识符，
    /// 内存byte长度：1024。
    /// </summary>
    public class GetDB
    {
        /// <summary>
        /// 获得存储体连接字符串
        /// </summary>
        /// <param name="key">为空时获取基库的类别</param>
        /// <returns></returns>
        public static string GetDBString(string key)
        {
            string md = string.Empty;
            MemoryShareAid msa;
            try
            {
                try
                {
                    if (string.IsNullOrEmpty(key))
                    {
                        msa = new MemoryShareAid("ZWHBASEDB", 1024);
                    }
                    else
                    {
                        msa = new MemoryShareAid(key, 1024);
                    }
                    md = msa.GetData();
                }
                catch { }
                if (string.IsNullOrEmpty(md))
                {
                    string appvalue = string.Empty;
                    if (string.IsNullOrEmpty(key))
                    {
                        appvalue = GetConfigValue.GetAppValue("DB/BASEDB");
                    }
                    else
                    {
                        appvalue = GetConfigValue.GetAppValue("DB/" + key);
                    }
                    if (!string.IsNullOrEmpty(appvalue))
                    {
                        md = appvalue;
                    }
                    //else
                    //{
                    //    md = "server=.;database=ZWH;User ID=ZWH;Password=1234QWER!@#$;";
                    //}
                }
            }
            catch { }
            return md;
        }

        /// <summary>
        /// 获得存储体类别
        /// </summary>
        /// <param name="key">为空时获取基库的类别</param>
        /// <returns></returns>
        public static MUType GetDBType(string key)
        {
            MUType mut = MUType.NULL;
            MemoryShareAid msa;
            try
            {
                string md = string.Empty;
                try
                {
                    if (string.IsNullOrEmpty(key))
                    {
                        msa = new MemoryShareAid("ZWHBASEDBTYPE", 2);
                    }
                    else
                    {
                        msa = new MemoryShareAid(key, 2);
                    }
                    md = msa.GetData();
                }
                catch { }
                if (string.IsNullOrEmpty(md))
                {
                    string appvalue = string.Empty;
                    if (string.IsNullOrEmpty(key))
                    {
                        appvalue = GetConfigValue.GetAppValue("DBTYPE/BASEDB");
                    }
                    else
                    {
                        appvalue = GetConfigValue.GetAppValue("DBTYPE/" + key);
                    }
                    if (!string.IsNullOrEmpty(appvalue))
                    {
                        md = appvalue;
                    }
                    //else
                    //{
                    //    md = "1";
                    //}
                }
                if (!string.IsNullOrEmpty(md))
                {
                    mut = DBTypeSwitch.GetDBType(int.Parse(md));
                }
            }
            catch { }
            return mut;
        }

        /// <summary>
        /// 获得SQLSERVER数据库连接字符串
        /// </summary>
        /// <param name="key">为空时获取基库的类别</param>
        /// <returns></returns>
        public static string GetSQLString(string key)
        {
            string md = string.Empty;
            MemoryShareAid msa;
            try
            {
                try
                {
                    if (string.IsNullOrEmpty(key))
                    {
                        msa = new MemoryShareAid("ZWHBASEDB", 1024);
                    }
                    else
                    {
                        msa = new MemoryShareAid(key, 1024);
                    }
                    md = msa.GetData();
                }
                catch { }
                if (string.IsNullOrEmpty(md))
                {
                    string appvalue = string.Empty;
                    if (string.IsNullOrEmpty(key))
                    {
                        appvalue = GetConfigValue.GetAppValue("DB/BASEDB");
                    }
                    else
                    {
                        appvalue = GetConfigValue.GetAppValue("DB/" + key);
                    }
                    if (!string.IsNullOrEmpty(appvalue))
                    {
                        md = appvalue;
                    }
                    //else
                    //{
                    //    md = "server=.;database=ZWH;User ID=ZWH;Password=1234QWER!@#$;";
                    //}
                }
            }
            catch { }
            return md;
        }

        /// <summary>
        /// 获得mongodb数据库的连接字符串
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string GetMongoDBString(string key)
        {
            string md = string.Empty;
            MemoryShareAid msa;
            try
            {
                try
                {
                    if (string.IsNullOrEmpty(key))
                    {
                        msa = new MemoryShareAid("ZWHBASEDB", 1024);
                    }
                    else
                    {
                        msa = new MemoryShareAid(key, 1024);
                    }
                    md = msa.GetData();
                }
                catch { }
                if (string.IsNullOrEmpty(md))
                {
                    string appvalue = string.Empty;
                    if (string.IsNullOrEmpty(key))
                    {
                        appvalue = GetConfigValue.GetAppValue("DB/BASEDB");
                    }
                    else
                    {
                        appvalue = GetConfigValue.GetAppValue("DB/" + key);
                    }
                    if (!string.IsNullOrEmpty(appvalue))
                    {
                        md = appvalue;
                    }
                    //else
                    //{
                    //    md = "server=.;database=ZWH;User ID=ZWH;Password=1234QWER!@#$;";
                    //}
                }
            }
            catch { }
            return md;
        }
    }
}
