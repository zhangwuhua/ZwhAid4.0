using System;

using System.Data;
using System.Data.OleDb;

namespace ZwhAid
{
    public class ExcelAid : ZwhBase
    {
        /// <summary>
        /// Oledb读取Excel文件到DataSet中
        /// </summary>
        /// <param name="filePath">文件路径，全路径</param>
        /// <returns>DataSet</returns>
        public DataSet ToDataSet(string filePath)
        {
            DataSet ds = new DataSet();

            try
            {
                string fileName = string.Empty;
                if (!string.IsNullOrEmpty(filePath))
                {
                    if (filePath.Contains("."))
                    {
                        fileName = filePath.Substring(filePath.LastIndexOf('.'));
                        string fileType = System.IO.Path.GetExtension(fileName);
                        if (string.IsNullOrEmpty(fileType)) { return null; }
                        string connString = "Provider=Microsoft.ACE.OLEDB.12.0;" + "Data Source=" + filePath + ";" + ";Extended Properties=\"Excel 12.0;HDR=YES;IMEX=1\"";
                        string sql = "Select * FROM [{0}]";

                        OleDbConnection conn = null;
                        OleDbDataAdapter adapter = null;
                        DataTable dtSheetName = null;

                        try
                        {
                            // 初始化连接，并打开
                            conn = new OleDbConnection(connString);
                            conn.Open();

                            // 获取数据源的表定义元数据
                            string SheetName = "";
                            dtSheetName = conn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, new object[] { null, null, null, "TABLE" });

                            // 初始化OleDbDataAdapter
                            adapter = new OleDbDataAdapter();
                            for (int i = 0; i < dtSheetName.Rows.Count; i++)
                            {
                                SheetName = (string)dtSheetName.Rows[i]["TABLE_NAME"];

                                if (SheetName.Contains("$") && !SheetName.Replace("'", "").EndsWith("$"))
                                {
                                    continue;
                                }

                                adapter.SelectCommand = new OleDbCommand(String.Format(sql, SheetName), conn);
                                DataSet dsItem = new DataSet();
                                adapter.Fill(dsItem, SheetName);

                                ds.Tables.Add(dsItem.Tables[0].Copy());
                            }
                        }
                        catch (Exception ex)
                        {
                        }
                        finally
                        {
                            // 关闭连接
                            if (conn.State == ConnectionState.Open)
                            {
                                conn.Close();
                                adapter.Dispose();
                                conn.Dispose();
                            }
                        }
                    }
                }
            }
            catch { }

            return ds;
        }

        /// <summary>
        /// Oledb方式DataSet导入到Excel
        /// </summary>
        /// <param name="filePath">Excel文件路径，全路径</param>
        /// <param name="ds">需要导入的DataSet</param>
        public void ToExcel(string filePath, DataSet ds)
        {
            try
            {
                if (ds != null && ds.Tables.Count > 0)
                {
                    DataSet nds = new DataSet();
                    string connString = "Provider=Microsoft.ACE.OLEDB.12.0;" + "Data Source=" + filePath + ";" + ";Extended Properties=\"Excel 12.0;HDR=YES;IMEX=1\"";
                    foreach (DataTable dt in ds.Tables)
                    {
                        nds.Tables.Add(dt.Clone());
                        string sql = string.Empty;
                        if (dt.TableName.Contains("$"))
                        {
                            sql = "select * from [" + dt.TableName + "]";
                        }
                        else
                        {
                            sql = "select * from [" + dt.TableName + "$]";
                        }
                        OleDbConnection conn = new OleDbConnection(connString);
                        conn.Open();
                        OleDbDataAdapter adapter = new OleDbDataAdapter(sql, conn);
                        OleDbCommandBuilder builder = new OleDbCommandBuilder(adapter);
                        //QuotePrefix和QuoteSuffix主要是对builder生成InsertComment命令时使用。 
                        builder.QuotePrefix = "[";//起始位置
                        builder.QuoteSuffix = "]";//结束位置
                        adapter.Fill(nds, "Table1");
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            DataRow dr = nds.Tables[dt.TableName].NewRow();
                            for (int j = 0; j < dt.Columns.Count; j++)
                            {
                                dr[j] = dt.Rows[i][j];
                            }
                            nds.Tables[dt.TableName].Rows.Add(dr);
                        }
                        adapter.Update(nds, "Table1");
                        nds.AcceptChanges();
                        conn.Close();
                    }

                    using (OleDbConnection ole_conn = new OleDbConnection(connString))
                    {
                        ole_conn.Open();
                        using (OleDbCommand ole_cmd = ole_conn.CreateCommand())
                        {
                            ole_cmd.CommandText = "CREATE TABLE CustomerInfo ([CustomerID] VarChar,[Customer] VarChar)";
                            ole_cmd.ExecuteNonQuery();
                            ole_cmd.CommandText = "insert into CustomerInfo(CustomerID,Customer)values('DJ001','点击科技')";
                            ole_cmd.ExecuteNonQuery();
                            ole_conn.Close();
                        }
                    }
                }
            }
            catch { }
        }
    }
}
