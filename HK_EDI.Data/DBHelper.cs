using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.Common;
using System.Configuration;
namespace HK_EDI.Data
{
    public class DBHelper
    {
        public static string ConnectionString { get; set; }
        public static string ConnectionStringRead { get; set; }
        private static DbProviderFactory DbProviderFactory = DbProviderFactories.GetFactory("System.Data.SqlClient");

        #region CreateConnection

        /// <summary>
        /// CreateConnection
        /// </summary>
        /// <returns>DbConnection</returns>
        public static DbConnection CreateConnection(bool IsRead=false )
        {
            DbConnection Conn = DbProviderFactory.CreateConnection();
            if (IsRead && !string.IsNullOrEmpty( ConnectionStringRead))
            {
                Conn.ConnectionString = ConnectionStringRead;
            }
            else
            {
                Conn.ConnectionString = ConnectionString;
            }
            return Conn;
        }

        #endregion

        #region CreateCommand

        /// <summary>
        /// CreateCommand
        /// </summary>
        /// <returns>DbCommand</returns>
        public static DbCommand CreateCommand()
        {
            return DbProviderFactory.CreateCommand();
        }

        /// <summary>
        /// CreateCommand
        /// </summary>
        /// <param name="strSql">string</param>
        /// <returns>DbCommand</returns>
        public static DbCommand CreateCommand(string strSql)
        {
            DbCommand Cmd = DBHelper.DbProviderFactory.CreateCommand();
            Cmd.CommandText = strSql;
            return Cmd;
        }

        /// <summary>
        /// CreateCommand
        /// </summary>
        /// <param name="conn">DbConnection</param>
        /// <param name="strSql">string</param>
        /// <returns>DbCommand</returns>
        public static DbCommand CreateCommand(DbConnection conn, string strSql)
        {
            DbCommand Cmd = DbProviderFactory.CreateCommand();
            Cmd.Connection = conn;
            Cmd.CommandText = strSql;
            return Cmd;
        }

        #endregion

        #region CreateDataAdapter

        /// <summary>
        /// CreateDataAdapter
        /// </summary>
        /// <returns>DbDataAdapter</returns>
        public static DbDataAdapter CreateDataAdapter()
        {
            return DbProviderFactory.CreateDataAdapter();
        }

        /// <summary>
        /// CreateDataAdapter
        /// </summary>
        /// <param name="cmd">DbCommand</param>
        /// <returns>DbDataAdapter</returns>
        public static DbDataAdapter CreateDataAdapter(DbCommand cmd)
        {
            DbDataAdapter Dap = DbProviderFactory.CreateDataAdapter();
            Dap.SelectCommand = cmd;
            return Dap;
        }

        #endregion

        #region CreateParameter

        /// <summary>
        /// CreateParameter
        /// </summary>
        /// <param name="cmd">DbCommand</param>
        /// <param name="parameterName">string</param>
        /// <param name="parameterValue">object</param>
        public static void CreateParameter(DbCommand cmd, string parameterName, object parameterValue)
        {
            DbParameter parameter = cmd.CreateParameter();
            parameter.ParameterName = parameterName;
            parameter.Value = parameterValue;
            cmd.Parameters.Add(parameter);
        }

        public static void CreateParameter(DbCommand cmd, string parameterName, object parameterValue, DbType parameterType)
        {
            DbParameter parameter = cmd.CreateParameter();
            parameter.ParameterName = parameterName;
            parameter.Value = parameterValue;
            parameter.DbType = parameterType;
            cmd.Parameters.Add(parameter);
        }

        public static void CreateParameter(DbCommand cmd, DbParameterCollection dbParameterCollection)
        {
            foreach (DbParameter dbParameter in dbParameterCollection)
            {
                cmd.Parameters.Add(dbParameter);
            }
        }

        #endregion

        #region ExecuteReader

        /// <summary>
        /// ExecuteReader
        /// </summary>
        /// <param name="strSql">string</param>
        /// <returns>DbDataReader</returns>
        public static DbDataReader ExecuteReader(string strSql)
        {
            DbDataReader result;
            using (DbConnection Conn = CreateConnection())
            {
                using (DbCommand Cmd = CreateCommand(Conn, strSql))
                {
                    try
                    {
                        Conn.Open();
                        result = Cmd.ExecuteReader(CommandBehavior.CloseConnection);
                    }
                    catch (SqlException e)
                    {
                        throw new Exception(e.Message);
                    }
                    finally
                    {
                        Conn.Close();
                    }
                }
            }
            return result;
        }

        /// <summary>
        /// ExecuteReader
        /// </summary>
        /// <param name="Cmd">DbCommand</param>
        /// <returns>DbDataReader</returns>
        public static DbDataReader ExecuteReader(DbCommand Cmd)
        {
            DbDataReader result;
            using (DbConnection Conn = CreateConnection())
            {
                Cmd.Connection = Conn;
                try
                {
                    Conn.Open();
                    result = Cmd.ExecuteReader(CommandBehavior.CloseConnection);
                }
                catch (SqlException e)
                {
                    throw new Exception(e.Message);
                }
                finally
                {
                    Conn.Close();
                }
            }
            return result;
        }

        #endregion

        #region ExecuteNonQuery

        /// <summary>
        /// ExecuteNonQuery
        /// </summary>
        /// <param name="strSql">string</param>
        /// <returns>int</returns>
        public static int ExecuteNonQuery(string strSql, bool isRead = false)
        {
            int result = 0;
            using (DbConnection Conn = CreateConnection(isRead))
            {
                using (DbCommand Cmd = CreateCommand(Conn, strSql))
                {
                    try
                    {
                        Conn.Open();
                        result = Cmd.ExecuteNonQuery();
                    }
                    catch (SqlException e)
                    {
                        throw new Exception(e.Message + "|" + strSql);
                    }
                    finally
                    {
                        Conn.Close();
                    }
                }
            }
            return result;
        }

        public static int ExecuteNonQuery(string strSql, System.Data.Common.DbParameter[] param, int timeOut, bool isRead = false)
        {
            return ExecuteNonQuery(strSql, CommandType.Text, param, timeOut, isRead);
        }

        public static int ExecuteNonQuery(string strSql, System.Data.Common.DbParameter[] param, bool isRead = false)
        {
            return ExecuteNonQuery(strSql, CommandType.Text, param, 1800, isRead);
        }
        public static int ExecuteNonTranQuery(string strSql, System.Data.Common.DbParameter[] param, bool isRead = false)
        {
            return ExecuteNonTranQuery1(strSql, CommandType.Text, param, 3600, IsolationLevel.ReadCommitted, isRead);
        }

        public static int ExecuteNonQuery(string strSql, CommandType cmdType, System.Data.Common.DbParameter[] param, bool isRead = false)
        {
            return ExecuteNonQuery(strSql, cmdType, param, 1800, isRead);
        }

        /// <summary>
        /// ExecuteNonQuery
        /// </summary>
        /// <param name="strSql">string</param>
        /// <returns>int</returns>
        public static int ExecuteNonQuery(string strSql, CommandType cmdType, System.Data.Common.DbParameter[] param, int timeOut, bool isRead = false)
        {
            int result = 0;
            using (DbConnection Conn = CreateConnection(isRead))
            {
                using (DbCommand Cmd = CreateCommand(Conn, strSql))
                {
                    Cmd.CommandType = cmdType;
                    Cmd.CommandTimeout = timeOut;
                    AttachParameters(Cmd, param);
                    try
                    {
                        Conn.Open();
                        result = Cmd.ExecuteNonQuery();
                        Cmd.Parameters.Clear();
                    }
                    catch (SqlException e)
                    {
                        throw new Exception(e.Message + "|" + strSql);
                    }
                    finally
                    {
                        Conn.Close();
                    }
                }
            }
            return result;
        }
        public static int ExecuteNonTranQuery1(string strSql, CommandType cmdType, System.Data.Common.DbParameter[] param, int timeOut, IsolationLevel level, bool isRead = false)
        {
            int result = 0;
            using (DbConnection Conn = CreateConnection())
            {
                using (DbCommand Cmd = CreateCommand(Conn, strSql))
                {
                    Cmd.CommandType = cmdType;
                    Cmd.CommandTimeout = timeOut;
                    AttachParameters(Cmd, param);

                    Conn.Open();
                    Cmd.Connection = Conn;
                    DbTransaction tran = Conn.BeginTransaction(level);
                    Cmd.Transaction = tran;
                    try
                    {
                      
                        result = Cmd.ExecuteNonQuery();
                        Cmd.Parameters.Clear();
                        tran.Commit();
                    }
                    catch (SqlException e)
                    {
                        tran.Rollback();
                        throw new Exception(e.Message + "|" + strSql);
                    }
                    finally
                    {
                        Conn.Close();
                    }
                }
            }
            return result;
        }

        /// <summary>
        /// ExecuteNonQuery
        /// </summary>
        /// <param name="Cmd">DbCommand</param>
        /// <returns>int</returns>
        public static int ExecuteNonQuery(DbCommand Cmd)
        {
            int result = 0;
            using (DbConnection Conn = CreateConnection())
            {
                Cmd.Connection = Conn;
                try
                {
                    Conn.Open();
                    result = Cmd.ExecuteNonQuery();
                }
                catch (SqlException e)
                {
                    throw new Exception(e.Message + "|" + Cmd.CommandText);
                }
                finally
                {
                    Conn.Close();
                }
            }
            return result;
        }

        #endregion

        #region ExecuteScalar

        /// <summary>
        /// ExecuteScalar
        /// </summary>
        /// <param name="strSql">string</param>
        /// <returns>string</returns>
        public static string ExecuteScalar(string strSql)
        {
            object result = null;
            using (DbConnection Conn = CreateConnection())
            {
                using (DbCommand Cmd = CreateCommand(Conn, strSql))
                {
                    try
                    {
                        Conn.Open();
                        result = Cmd.ExecuteScalar();
                        if (result == null)
                        {
                            result = "";
                        }
                    }
                    catch (SqlException e)
                    {
                        throw new Exception(e.Message);
                    }
                    finally
                    {
                        Conn.Close();
                    }
                }
            }
            return result.ToString();
        }

        /// <summary>
        /// ExecuteScalar
        /// </summary>
        /// <param name="cmd">DbCommand</param>
        /// <returns>string</returns>
        public static string ExecuteScalar(DbCommand Cmd)
        {
            object result = null;
            using (DbConnection Conn = CreateConnection())
            {
                Cmd.Connection = Conn;
                try
                {
                    Conn.Open();
                    result = Cmd.ExecuteScalar();
                    if (result == null)
                    {
                        result = "";
                    }
                }
                catch (SqlException e)
                {
                    throw new Exception(e.Message);
                }
                finally
                {
                    Conn.Close();
                }
            }
            return result.ToString();
        }

        public static object ExecuteScalar(string strSql, CommandType cmdType, System.Data.Common.DbParameter[] param)
        {
            object ob;
            using (DbConnection Conn = CreateConnection())
            {
                using (DbCommand Cmd = CreateCommand(Conn, strSql))
                {
                    Cmd.CommandType = cmdType;
                    Cmd.CommandTimeout = 3000;
                    AttachParameters(Cmd, param);
                    try
                    {
                        Conn.Open();
                        ob = Cmd.ExecuteScalar();
                        Cmd.Parameters.Clear();
                    }
                    catch (SqlException e)
                    {
                        throw new Exception(e.Message + "|" + strSql);
                    }
                    finally
                    {
                        Conn.Close();
                    }
                }
            }
            return ob;
        }

        #endregion

        #region ExecuteDataTable

        public static DataTable ExecuteDataTable(string strSql, bool isRead=false )
        {
            return ExecuteDataTable(strSql, CommandType.Text, null, 1800, isRead);
        }

        public static DataTable ExecuteDataTable(string strSql, int timeOut, bool isRead = false)
        {
            return ExecuteDataTable(strSql, CommandType.Text, null, timeOut,isRead);
        }

        public static DataTable ExecuteDataTable(string strSql, DbParameter[] param, bool isRead=false )
        {
            return ExecuteDataTable(strSql, CommandType.Text, param, 1800, isRead);
        }

        public static DataTable ExecuteDataTable(string strSql, CommandType type, DbParameter[] param, bool isRead=false )
        {
            return ExecuteDataTable(strSql, type, param, 1800, isRead);
        }

        public static DataTable ExecuteDataTable(string strSql, DbParameter[] param, int timeOut, bool isRead=false )
        {
            return ExecuteDataTable(strSql, CommandType.Text, param, timeOut, isRead);
        }

        /// <summary>
        /// ExecuteDataTable
        /// </summary>
        /// <param name="strSql">string</param>
        /// <returns>DataTable</returns>
        public static DataTable ExecuteDataTable(string strSql, CommandType cmdType, DbParameter[] param, int timeOut,bool isRead=false )
        {
            DataTable result = new DataTable();
            using (DbConnection Conn = CreateConnection(isRead))
            {
                using (DbCommand Cmd = CreateCommand(Conn, strSql))
                {
                    Cmd.CommandType = cmdType;
                    Cmd.CommandTimeout = timeOut;
                    AttachParameters(Cmd, param);
                    using (DbDataAdapter Dap = CreateDataAdapter(Cmd))
                    {
                        try
                        {
                            Conn.Open();
                            Dap.Fill(result);
                        }
                        catch (SqlException e)
                        {
                            throw new Exception(e.Message);
                        }
                        finally
                        {
                            Conn.Close();
                        }
                    }
                }
            }
            return result;
        }

        /// <summary>
        /// ExecuteDataTable
        /// </summary>
        /// <param name="Cmd">DbCommand</param>
        /// <returns>DataTable</returns>
        public static DataTable ExecuteDataTable(DbCommand Cmd, bool isRead=false )
        {
            DataTable result = new DataTable();
            using (DbConnection Conn = CreateConnection(isRead))
            {
                Cmd.Connection = Conn;
                using (DbDataAdapter Dap = CreateDataAdapter(Cmd))
                {
                    try
                    {
                        Conn.Open();
                        Dap.Fill(result);
                    }
                    catch (SqlException e)
                    {
                        throw new Exception(e.Message);
                    }
                    finally
                    {
                        Conn.Close();
                    }
                }
            }
            return result;
        }

        #endregion

        #region ExecuteDataSet

        /// <summary>
        /// ExecuteDataSet
        /// </summary>
        /// <param name="strSql">string</param>
        /// <returns>DataSet</returns>
        public static DataSet ExecuteDataSet(string strSql, int outTime, bool isRead=false )
        {
            DataSet result = new DataSet();
            using (DbConnection Conn = CreateConnection(isRead))
            {
                using (DbCommand Cmd = CreateCommand(Conn, strSql))
                {
                    Cmd.CommandTimeout = outTime;
                    using (DbDataAdapter Dap = CreateDataAdapter(Cmd))
                    {
                        try
                        {
                            Conn.Open();
                            Dap.Fill(result);
                        }
                        catch (SqlException e)
                        {
                            throw new Exception(e.Message);
                        }
                        finally
                        {
                            Conn.Close();
                        }
                    }
                }
            }
            return result;
        }

        /// <summary>
        /// ExecuteDataSet
        /// </summary>
        /// <param name="strSql">string</param>
        /// <returns>DataSet</returns>
        public static DataSet ExecuteDataSet(string strSql, bool isRead=false )
        {
            return ExecuteDataSet(strSql, 1800, isRead);
        }

        /// <summary>
        /// ExecuteDataSet
        /// </summary>
        /// <param name="strSql">string</param>
        /// <returns>DataSet</returns>
        public static DataSet ExecuteDataSet(string strSql, System.Data.Common.DbParameter[] param, bool isRead=false )
        {
            return ExecuteDataSet(strSql, CommandType.Text, param, 1800, isRead);
        }

        /// <summary>
        /// ExecuteDataSet
        /// </summary>
        /// <param name="strSql">string</param>
        /// <returns>DataSet</returns>
        public static DataSet ExecuteDataSet(string strSql, System.Data.Common.DbParameter[] param, int outTime, bool isRead=false )
        {
            return ExecuteDataSet(strSql, CommandType.Text, param, outTime, isRead);
        }

        /// <summary>
        /// ExecuteDataSet
        /// </summary>
        /// <param name="strSql">string</param>
        /// <returns>DataSet</returns>
        public static DataSet ExecuteDataSet(string strSql, CommandType cmdType, System.Data.Common.DbParameter[] param, int outTime, bool isRead=false )
        {
            DataSet result = new DataSet();
            using (DbConnection Conn = CreateConnection(isRead))
            {
                using (DbCommand Cmd = CreateCommand(Conn, strSql))
                {
                    Cmd.CommandType = cmdType;
                    Cmd.CommandTimeout = outTime;
                    AttachParameters(Cmd, param);
                    using (DbDataAdapter Dap = CreateDataAdapter(Cmd))
                    {
                        try
                        {
                            Conn.Open();
                            Dap.Fill(result);
                            Cmd.Parameters.Clear();
                        }
                        catch (SqlException e)
                        {
                            throw new Exception(e.Message);
                        }
                        finally
                        {
                            Conn.Close();
                        }
                    }
                }
            }
            return result;
        }

        /// <summary>
        /// ExecuteDataSet
        /// </summary>
        /// <param name="Cmd">DbCommand</param>
        /// <returns>DataSet</returns>
        public static DataSet ExecuteDataSet(DbCommand Cmd, bool isRead=false )
        {
            DataSet result = new DataSet();
            using (DbConnection Conn = CreateConnection(isRead))
            {
                Cmd.Connection = Conn;
                using (DbDataAdapter Dap = CreateDataAdapter(Cmd))
                {
                    try
                    {
                        Conn.Open();
                        Dap.Fill(result);
                    }
                    catch (SqlException e)
                    {
                        throw new Exception(e.Message);
                    }
                    finally
                    {
                        Conn.Close();
                    }
                }
            }
            return result;
        }

        #endregion

        #region ExecuteTransaction
        public static int ExecuteNonTranQuery(List<string> strSql)
        {
            return ExecuteNonTranQuery(strSql, IsolationLevel.ReadCommitted);
        }

        /// <summary>
        /// ExecuteNonQuery
        /// </summary>
        /// <param name="strSql">string</param>
        /// <returns>int</returns>
        public static int ExecuteNonTranQuery(List<string> strSql, IsolationLevel level)
        {
            int result = 0;
            using (DbConnection Conn = CreateConnection())
            {
                using (DbCommand Cmd = CreateCommand())
                {
                    Conn.Open();
                    Cmd.Connection = Conn;
                    DbTransaction tran = Conn.BeginTransaction(level);
                    Cmd.Transaction = tran;
                    try
                    {
                        foreach (var item in strSql)
                        {
                            Cmd.CommandText = item;
                            result += Cmd.ExecuteNonQuery();
                        }
                        tran.Commit();
                    }
                    catch (SqlException e)
                    {
                        tran.Rollback();
                        throw new Exception(e.Message + "|" + strSql);
                    }
                    finally
                    {
                        Conn.Close();
                    }
                }
            }
            return result;
        }
        #endregion
        /// <summary>
        /// 将DbParameter参数数组(参数值)分配给DbCommand命令.
        /// 这个方法将给任何一个参数分配DBNull.Value;
        /// 该操作将阻止默认值的使用.
        /// </summary>
        /// <param name="command">命令名</param>
        /// <param name="commandParameters">DbParameters数组</param>
        public static void AttachParameters(DbCommand command, DbParameter[] commandParameters)
        {
            //if (command == null) throw new ArgumentNullException("command");
            if (commandParameters != null)
            {
                foreach (DbParameter p in commandParameters)
                {
                    if (p != null)
                    {
                        // 检查未分配值的输出参数,将其分配以DBNull.Value.
                        if ((p.Direction == ParameterDirection.InputOutput || p.Direction == ParameterDirection.Input) &&
                            (p.Value == null))
                        {
                            p.Value = DBNull.Value;
                        }
                        command.Parameters.Add(p);
                    }
                }
            }
        }
    }

}