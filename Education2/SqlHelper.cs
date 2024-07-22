using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Web;

namespace Education2
{
    public class SqlHelper
    {
        private static OleDbConnection connection;

        public static OleDbConnection Connection
        {
            get
            {
                string connectionstring = ConfigurationManager.ConnectionStrings["Accesscon"].ConnectionString;

                if (connection == null)
                {
                    connection = new OleDbConnection(connectionstring);
                    connection.Open();
                }
                return SqlHelper.connection;
            }

        }

        public static readonly string ConnectionString = ConfigurationManager.ConnectionStrings["Accesscon"].ConnectionString;
        public static readonly OleDbConnection con = new OleDbConnection(ConnectionString);


        /// <summary>
        ///只支持SQL语句，类型，用于增删改 
        /// </summary>
        /// <param name="commandText"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        public int ExecuteNonQuery(string commandText, CommandType ct)
        {

            OleDbCommand cmd = new OleDbCommand(commandText, Connection);
            try
            {
                cmd.CommandType = ct;
                return cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {
                return -1;
            }
            finally
            {
                con.Close();
            }

        }




        /// <summary>
        /// 查询返回影响数
        /// </summary>
        /// <param name="safeSql"></param>
        /// <returns></returns>
        public static int GetScalar(string safeSql)
        {
            try
            {

                OleDbCommand cmd = new OleDbCommand(safeSql, Connection);
                int result = Convert.ToInt32(cmd.ExecuteScalar());
                return result;
            }
            catch (Exception)
            {
                return -1;
            }
            finally
            {
                con.Close();
            }
        }

        /// <summary>
        /// 查询返回 OleDbDataReader
        /// </summary>
        /// <param name="safeSql"></param>
        /// <returns></returns>
        public OleDbDataReader GetScalarList(string safeSql)
        {
            try
            {

                OleDbCommand cmd = new OleDbCommand(safeSql, Connection);
                OleDbDataReader reader = cmd.ExecuteReader();
                return reader;
            }
            catch (Exception)
            {
                return null;
            }
            finally
            {
                con.Close();
            }
        }

        /// <summary>
        /// 分页专用
        /// </summary>
        /// <param name="safeSql"></param>
        /// <returns></returns>
        public static OleDbDataAdapter GetScalarListpage(string safeSql)
        {
            try
            {
                OleDbDataAdapter sda = new OleDbDataAdapter(safeSql, Connection);
                return sda;
            }
            catch (Exception)
            {
                return null;
            }
            finally
            {
                con.Close();
            }
        }


        /// <summary>
        /// 返回datatable
        /// </summary>
        /// <param name="safeSql"></param>
        /// <returns></returns>
        public static  DataTable GetScalarListTable(string safeSql)
        {
            try
            {
                DataSet ds = new DataSet();
                OleDbCommand cmd = new OleDbCommand(safeSql, Connection);
                OleDbDataAdapter da = new OleDbDataAdapter(cmd);
                da.Fill(ds);
                return ds.Tables[0];
            }
            catch (Exception)
            {
                return null;
            }
            finally
            {
                con.Close();
            }
        }
    }
}