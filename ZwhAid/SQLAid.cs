using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace ZwhAid
{
    /// <summary>
    /// SQL Server基本操作
    /// </summary>
    public class SQLAid : ZwhBase
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public SQLAid()
        {
            try
            {
                connectionString = GetDB.GetSQLString(null);
            }
            catch { }
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="cs"></param>
        public SQLAid(string cs)
        {
            if (!string.IsNullOrEmpty(cs))
            {
                connectionString = cs;
            }
            else
            {
                connectionString = GetDB.GetSQLString(null);
            }
        }

        private string connectionString;
        /// <summary>
        /// SQL SERVER数据库连接字符串
        /// </summary>
        public string ConnectionString
        {
            set { connectionString = value; }
            get { return connectionString; }
        }

        SqlConnection connection;
        private bool Connection()
        {
            bool bl = false;

            try
            {
                connection = new SqlConnection(connectionString);
                connection.Open();
                if (connection.State == ConnectionState.Open || connection.State == ConnectionState.Connecting)
                {
                    bl = true;
                }
            }
            catch { }

            return bl;
        }

        private SqlCommand GetSqlComand(string cmdText)
        {
            SqlCommand command = new SqlCommand();

            try
            {
                Connection();
                command = new SqlCommand(cmdText, connection);
            }
            catch { }

            return command;
        }

        private void Close()
        {
            if (connection.State == ConnectionState.Open || connection.State == ConnectionState.Connecting)
            {
                connection.Close();
            }
        }

        #region 执行SQL语句
        /// <summary>
        /// 批量写入
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="dt">元数据</param>
        /// <param name="lists">映射关系</param>
        /// <returns>是否成功</returns>
        public bool SQLBulkCopy(string tableName, DataTable dt, List<SqlBulkCopyColumnMapping> lists)
        {
            try
            {
                if (Connection())
                {
                    using (SqlBulkCopy sbc = new SqlBulkCopy(ConnectionString))
                    {
                        sbc.BatchSize = 1000;
                        sbc.BulkCopyTimeout = 60;
                        sbc.DestinationTableName = tableName;
                        if (lists != null)
                        {
                            foreach (SqlBulkCopyColumnMapping sbcm in lists)
                            {
                                sbc.ColumnMappings.Add(sbcm);
                            }
                        }
                        sbc.WriteToServer(dt);
                        zBool = true;
                    }
                }
            }
            catch { }

            Close();

            return ZBool;
        }

        /// <summary>
        /// 存在
        /// </summary>
        /// <param name="cmdText">执行命令</param>
        /// <returns>自定义类型</returns>
        public ZwhBase Exists(string cmdText)
        {
            ZwhBase zb = new ZwhBase();

            try
            {
                List<SqlParameter> lists = null;
                zb = Exists(cmdText, lists);
            }
            catch { }

            Close();

            return zb;
        }

        /// <summary>
        /// 存在
        /// </summary>
        /// <param name="cmdText">执行命令</param>
        /// <param name="lists">SqlParameter集合</param>
        /// <returns>自定义类型</returns>
        public ZwhBase Exists(string cmdText, List<SqlParameter> lists)
        {
            ZwhBase zb = new ZwhBase();

            try
            {
                SqlCommand command = GetSqlComand(cmdText);
                if (lists != null)
                {
                    foreach (SqlParameter sp in lists)
                    {
                        if (sp != null && sp.Value != null)
                        {
                            command.Parameters.Add(sp);
                        }
                    }
                }
                zObject = command.ExecuteScalar();
                if (zObject != null)
                {
                    zb.ZObject = zObject;
                    zb.ZBool = true;
                }
            }
            catch { }

            Close();

            return zb;
        }

        /// <summary>
        /// 值
        /// </summary>
        /// <param name="cmdText">执行命令</param>
        /// <returns>Object</returns>
        public object GetValue(string cmdText)
        {
            try
            {
                List<SqlParameter> lists = null;
                zObject = Exists(cmdText, lists).ZObject;
            }
            catch { }

            Close();

            return ZObject;
        }

        /// <summary>
        /// 值
        /// </summary>
        /// <param name="cmdText">执行命令</param>
        /// <param name="lists">SqlParameter集合</param>
        /// <returns>Object</returns>
        public object GetValue(string cmdText, List<SqlParameter> lists)
        {
            try
            {
                zObject = Exists(cmdText, lists).ZObject;
            }
            catch { }

            Close();

            return ZObject;
        }

        /// <summary>
        /// 存在
        /// </summary>
        /// <param name="cmdText">执行命令</param>
        /// <returns>是否</returns>
        public bool ExistsJudge(string cmdText)
        {
            try
            {
                List<SqlParameter> lists = null;
                zBool = Exists(cmdText, lists).ZBool;
            }
            catch { }

            Close();

            return ZBool;
        }

        /// <summary>
        /// 存在
        /// </summary>
        /// <param name="cmdText">执行命令</param>
        /// <param name="lists">SqlParameter集合</param>
        /// <returns>是否</returns>
        public bool ExistsJudge(string cmdText, List<SqlParameter> lists)
        {
            try
            {
                zBool = Exists(cmdText, lists).ZBool;
            }
            catch { }

            Close();

            return ZBool;
        }

        /// <summary>
        /// 执行SQL语句
        /// </summary>
        /// <param name="cmdText">执行命令</param>
        /// <returns>是否成功</returns>
        public bool ExecuteSQL(string cmdText)
        {
            try
            {
                List<SqlParameter> lists = null;
                zBool = ExecuteSQL(cmdText, lists);
            }
            catch { }

            return ZBool;
        }

        /// <summary>
        /// 执行SQL语句
        /// </summary>
        /// <param name="cmdText">执行命令</param>
        /// <param name="lists">SqlParameter集合</param>
        /// <returns>是否成功</returns>
        public bool ExecuteSQL(string cmdText, List<SqlParameter> lists)
        {
            try
            {
                SqlCommand command = GetSqlComand(cmdText);
                if (lists != null)
                {
                    foreach (SqlParameter sp in lists)
                    {
                        if (sp != null && sp.Value != null)
                        {
                            command.Parameters.Add(sp);
                        }
                    }
                }
                zInt = command.ExecuteNonQuery();
                if (zInt > 0)
                {
                    zBool = true;
                }
            }
            catch { }

            Close();

            return ZBool;
        }

        /// <summary>
        /// 执行SQL语句
        /// </summary>
        /// <param name="cmdText">执行命令</param>
        /// <returns>DataSet</returns>
        public DataSet ExecuteDS(string cmdText)
        {
            try
            {
                List<SqlParameter> lists = null;
                zSet = ExecuteZBDS(cmdText, lists).ZSet;
            }
            catch { }

            Close();

            return ZSet;
        }

        /// <summary>
        /// 执行SQL语句
        /// </summary>
        /// <param name="cmdText">执行命令</param>
        /// <param name="lists">SqlParameter集合</param>
        /// <returns>DataSet</returns>
        public DataSet ExecuteDS(string cmdText, List<SqlParameter> lists)
        {
            try
            {
                zSet = ExecuteZBDS(cmdText, lists).ZSet;
            }
            catch { }

            Close();

            return ZSet;
        }

        /// <summary>
        /// 执行SQL语句
        /// </summary>
        /// <param name="cmdText">执行命令</param>
        /// <returns>DataTable</returns>
        public DataTable ExecuteDT(string cmdText)
        {
            try
            {
                List<SqlParameter> lists = null;
                zTable = ExecuteZBDT(cmdText, lists).ZTable;
            }
            catch { }

            return ZTable;
        }

        /// <summary>
        /// 执行SQL语句
        /// </summary>
        /// <param name="cmdText">执行命令</param>
        /// <param name="lists">SqlParameter集合</param>
        /// <returns>DataTable</returns>
        public DataTable ExecuteDT(string cmdText, List<SqlParameter> lists)
        {
            try
            {
                zTable = ExecuteZBDT(cmdText, lists).ZTable;
            }
            catch { }

            Close();

            return ZTable;
        }

        /// <summary>
        /// 执行SQL语句
        /// </summary>
        /// <param name="cmdText">执行命令</param>
        /// <returns>自定义类型</returns>
        public ZwhBase ExecuteZBDS(string cmdText)
        {
            ZwhBase zb = new ZwhBase();

            try
            {
                List<SqlParameter> lists = null;
                zb = ExecuteZBDS(cmdText, lists);
            }
            catch { }

            Close();

            return zb;
        }

        /// <summary>
        /// 执行SQL语句
        /// </summary>
        /// <param name="cmdText">执行命令</param>
        /// <param name="lists">SqlParameter集合</param>
        /// <returns>自定义类型</returns>
        public ZwhBase ExecuteZBDS(string cmdText, List<SqlParameter> lists)
        {
            ZwhBase zb = new ZwhBase();

            try
            {
                SqlCommand command = GetSqlComand(cmdText);
                if (lists != null)
                {
                    foreach (SqlParameter sp in lists)
                    {
                        if (sp != null && sp.Value != null)
                        {
                            command.Parameters.Add(sp);
                        }
                    }
                }
                SqlDataAdapter sda = new SqlDataAdapter(command);
                zb.ZSet = new DataSet();
                zb.ZLong = sda.Fill(zb.ZSet);
            }
            catch { }

            Close();

            return zb;
        }

        /// <summary>
        /// 执行SQL语句
        /// </summary>
        /// <param name="cmdText">执行命令</param>
        /// <returns>自定义类型</returns>
        public ZwhBase ExecuteZBDT(string cmdText)
        {
            ZwhBase zb = new ZwhBase();

            try
            {
                List<SqlParameter> lists = null;
                zb = ExecuteZBDT(cmdText, lists);
            }
            catch { }

            Close();

            return zb;
        }

        /// <summary>
        /// 执行SQL语句
        /// </summary>
        /// <param name="cmdText">执行命令</param>
        /// <param name="lists">SqlParameter集合</param>
        /// <returns>自定义类型</returns>
        public ZwhBase ExecuteZBDT(string cmdText, List<SqlParameter> lists)
        {
            ZwhBase zb = new ZwhBase();

            try
            {
                SqlCommand command = GetSqlComand(cmdText);
                if (lists != null)
                {
                    foreach (SqlParameter sp in lists)
                    {
                        if (sp != null && sp.Value != null)
                        {
                            command.Parameters.Add(sp);
                        }
                    }
                }
                SqlDataAdapter sda = new SqlDataAdapter(command);
                zb.ZTable = new DataTable();
                zb.ZLong = sda.Fill(zb.ZTable);
            }
            catch { }

            Close();

            return zb;
        }

        /// <summary>
        /// 执行SQL语句
        /// </summary>
        /// <param name="cmdText">执行命令</param>
        /// <param name="lists">SqlParameter集合</param>
        /// <param name="tableName">表名</param>
        /// <returns>自定义类型</returns>
        public ZwhBase ExecuteZB(string cmdText, List<SqlParameter> lists, string tableName)
        {
            ZwhBase zb = new ZwhBase();

            try
            {
                SqlCommand command = GetSqlComand(cmdText);
                if (lists != null)
                {
                    foreach (SqlParameter sp in lists)
                    {
                        if (sp != null && sp.Value != null)
                        {
                            command.Parameters.Add(sp);
                        }
                    }
                }
                SqlDataAdapter sda = new SqlDataAdapter(command);
                zb.ZSet = new DataSet();
                zb.ZLong = sda.Fill(zb.ZSet);
                if (zb.ZInt > 0)
                {
                    zb.ZBool = true;
                }
                if (zb.ZSet != null && zb.ZSet.Tables.Count > 0)
                {
                    if (!string.IsNullOrEmpty(tableName))
                    {
                        zTable = zb.ZSet.Tables[tableName];
                    }
                    else
                    {
                        zTable = zb.ZSet.Tables[0];
                    }
                }
            }
            catch { }

            Close();

            return zb;
        }
        #endregion

        #region 存储过程
        /// <summary>
        /// 执行存储过程
        /// </summary>
        /// <param name="spName">存储过程名称</param>
        /// <param name="parameters">参数</param>
        /// <returns>是否成功</returns>
        public bool ExecuteSP(string spName, List<SqlParameter> parameters)
        {
            bool bl = false;

            try
            {
                object returnValue = null;
                ExecuteSP(spName, parameters, out returnValue);
                bl = true;
            }
            catch { }

            Close();

            return bl;
        }

        /// <summary>
        /// 执行存储过程
        /// </summary>
        /// <param name="spName">存储过程名称</param>
        /// <param name="parameters">参数</param>
        /// <param name="returnValue">返回值</param>
        /// <returns>是否成功</returns>
        public bool ExecuteSP(string spName, List<SqlParameter> parameters, out object returnValue)
        {
            bool bl = false;
            returnValue = null;
            Dictionary<string, object> dic = new Dictionary<string, object>();

            try
            {
                ExecuteSP(spName, parameters, out returnValue, out dic);
                bl = true;
            }
            catch { }

            Close();

            return bl;
        }

        /// <summary>
        /// 执行存储过程
        /// </summary>
        /// <param name="spName">存储过程名称</param>
        /// <param name="parameters">参数</param>
        /// <param name="returnValue">返回值</param>
        /// <param name="outPut">输出参数值</param>
        /// <returns>是否成功</returns>
        public bool ExecuteSP(string spName, List<SqlParameter> parameters, out object returnValue, out Dictionary<string, object> outPut)
        {
            bool bl = false;
            returnValue = null;
            outPut = new Dictionary<string, object>();

            try
            {
                SqlCommand command = GetSqlComand(spName);
                command.CommandType = CommandType.StoredProcedure;
                if (parameters != null)
                {
                    foreach (SqlParameter parameter in parameters)
                    {
                        command.Parameters.Add(parameter);
                    }
                }
                command.ExecuteNonQuery();
                foreach (SqlParameter parameter in parameters)
                {
                    if (parameter.Direction == ParameterDirection.Output)
                    {
                        outPut.Add(parameter.ParameterName, command.Parameters[parameter.ParameterName].Value);
                        //if (output != null && output.Count > 0)
                        //{
                        //    foreach (KeyValuePair<string, object> kvp in output)
                        //    {
                        //        if (kvp.Key == parameter.ParameterName)
                        //        {
                        //            output[parameter.ParameterName] = command.Parameters[parameter.ParameterName].Value;
                        //        }
                        //    }
                        //}
                    }
                    if (parameter.Direction == ParameterDirection.ReturnValue)
                    {
                        returnValue = command.Parameters[parameter.ParameterName].Value;
                    }
                }
                bl = true;
            }
            catch { }

            Close();

            return bl;
        }

        /// <summary>
        /// 执行存储过程
        /// </summary>
        /// <param name="spName">存储过程名称</param>
        /// <param name="parameters">参数</param>
        /// <returns>DataTable</returns>
        public DataTable ExecuteSPDT(string spName, List<SqlParameter> parameters)
        {
            object returnValue = null;
            Dictionary<string, object> outPut = new Dictionary<string, object>();

            try
            {
                zTable = ExecuteSPDT(spName, parameters, out returnValue, out outPut);
            }
            catch { }

            Close();

            return ZTable;
        }

        /// <summary>
        /// 执行存储过程
        /// </summary>
        /// <param name="spName">存储过程名称</param>
        /// <param name="parameters">参数</param>
        /// <param name="returnValue">返回值</param>
        /// <returns>DataTable</returns>
        public DataTable ExecuteSPDT(string spName, List<SqlParameter> parameters, out object returnValue)
        {
            returnValue = null;
            Dictionary<string, object> outPut = new Dictionary<string, object>();

            try
            {
                zTable = ExecuteSPDT(spName, parameters, out returnValue, out outPut);
            }
            catch { }

            Close();

            return ZTable;
        }

        /// <summary>
        /// 执行存储过程
        /// </summary>
        /// <param name="spName">存储过程名称</param>
        /// <param name="parameters">参数</param>
        /// <param name="returnValue">返回值</param>
        /// <param name="outPut">输出参数值</param>
        /// <returns>DataTable</returns>
        public DataTable ExecuteSPDT(string spName, List<SqlParameter> parameters, out object returnValue, out Dictionary<string, object> outPut)
        {
            returnValue = null;
            outPut = null;

            try
            {
                SqlCommand command = GetSqlComand(spName);
                command.CommandType = CommandType.StoredProcedure;
                if (parameters != null)
                {
                    foreach (SqlParameter parameter in parameters)
                    {
                        command.Parameters.Add(parameter);
                    }
                }
                SqlDataAdapter da = new SqlDataAdapter(command);
                zTable = new DataTable();
                da.Fill(zTable);
                foreach (SqlParameter parameter in parameters)
                {
                    if (parameter.Direction == ParameterDirection.Output)
                    {
                        outPut.Add(parameter.ParameterName, command.Parameters[parameter.ParameterName].Value);
                        //if (output != null && output.Count > 0)
                        //{
                        //    foreach (KeyValuePair<string, object> kvp in output)
                        //    {
                        //        if (kvp.Key == parameter.ParameterName)
                        //        {
                        //            output[parameter.ParameterName] = command.Parameters[parameter.ParameterName].Value;
                        //        }
                        //    }
                        //}
                    }
                    if (parameter.Direction == ParameterDirection.ReturnValue)
                    {
                        returnValue = command.Parameters[parameter.ParameterName].Value;
                    }
                }
            }
            catch { }

            return ZTable;
        }

        /// <summary>
        /// 执行存储过程
        /// </summary>
        /// <param name="spName">存储过程名称</param>
        /// <param name="parameters">参数</param>
        /// <returns>DataSet</returns>
        public DataSet ExecuteSPDS(string spName, List<SqlParameter> parameters)
        {
            try
            {
                object returnValue = null;
                zSet = ExecuteSPDS(spName, parameters, out returnValue);
            }
            catch { }

            Close();

            return ZSet;
        }

        /// <summary>
        /// 执行存储过程
        /// </summary>
        /// <param name="spName">存储过程名称</param>
        /// <param name="parameters">参数</param>
        /// <param name="returnValue">返回值</param>
        /// <returns>DataSet</returns>
        public DataSet ExecuteSPDS(string spName, List<SqlParameter> parameters, out object returnValue)
        {
            returnValue = null;

            try
            {
                SqlCommand command = GetSqlComand(spName);
                command.CommandType = CommandType.StoredProcedure;
                if (parameters != null)
                {
                    foreach (SqlParameter parameter in parameters)
                    {
                        command.Parameters.Add(parameter);
                    }
                }
                SqlDataAdapter da = new SqlDataAdapter(command);
                da.Fill(zSet);
                foreach (SqlParameter parameter in parameters)
                {
                    if (parameter.Direction == ParameterDirection.ReturnValue)
                    {
                        returnValue = command.Parameters[parameter.ParameterName].Value;
                    }
                }
            }
            catch { }

            Close();

            return ZSet;
        }

        /// <summary>
        /// 执行存储过程
        /// </summary>
        /// <param name="spName">存储过程名称</param>
        /// <param name="parameters">参数</param>
        /// <param name="returnValue">返回值</param>
        /// <param name="outPut">输出参数值</param>
        /// <returns>DataSet</returns>
        public DataSet ExecuteSPDS(string spName, List<SqlParameter> parameters, out object returnValue, out Dictionary<string, object> outPut)
        {
            returnValue = null;
            outPut = null;

            try
            {
                SqlCommand command = GetSqlComand(spName);
                command.CommandType = CommandType.StoredProcedure;
                if (parameters != null)
                {
                    foreach (SqlParameter parameter in parameters)
                    {
                        command.Parameters.Add(parameter);
                    }
                }
                SqlDataAdapter da = new SqlDataAdapter(command);
                zSet = new DataSet();
                da.Fill(zSet);
                foreach (SqlParameter parameter in parameters)
                {
                    if (parameter.Direction == ParameterDirection.ReturnValue)
                    {
                        returnValue = command.Parameters[parameter.ParameterName].Value;
                    }
                    if (parameter.Direction == ParameterDirection.Output)
                    {
                        outPut.Add(parameter.ParameterName, command.Parameters[parameter.ParameterName].Value);
                    }
                }
            }
            catch { }

            Close();

            return ZSet;
        }
        #endregion
    }
}
