using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Education2
{
    public class DataSQL
    {
          string connstr = ConfigurationManager.ConnectionStrings["str"].ConnectionString;

        

        //DataSet 获取数据
        public DataSet GetRows(string sql)
        {
            SqlConnection con = new SqlConnection(connstr);
            con.Open();
            SqlDataAdapter sda = new SqlDataAdapter(sql, con);
            DataSet ds = new DataSet();
            sda.Fill(ds);
            con.Close();
            return ds;
        }

        public string Getcounts(string sql)
        {

            SqlConnection con = new SqlConnection(connstr);
            con.Open();
            SqlCommand com = new SqlCommand(sql, con);
            string a = Convert.ToString(com.ExecuteScalar());
            con.Close();
            return a;
        }

        public int GetcountsInt(string sql)
        {
            SqlConnection con = new SqlConnection(connstr);
            con.Open();
            SqlCommand com = new SqlCommand(sql, con);
            int b = Convert.ToInt32(com.ExecuteScalar());
            con.Close();
            return b;
        }


        public SqlDataReader ExecuteReader(string sql)
        {
            SqlConnection con = new SqlConnection(connstr);
            SqlCommand com = new SqlCommand(sql, con);
            try
            {
                con.Open();
                SqlDataReader myReader = com.ExecuteReader(CommandBehavior.CloseConnection);
                con.Close();
                return myReader;
            }
            catch (System.Data.SqlClient.SqlException e)
            {
                throw e;
            }
        }

        public string checkDate(string outDate)
        {
            try
            {
                SqlConnection con = new SqlConnection(connstr);
                DataTable dt = new DataTable();
                con.Open();
                SqlDataAdapter da = new SqlDataAdapter("p_checkDate", con);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;

                SqlParameter dateParameter = new SqlParameter("@outDate", outDate);
                dateParameter.Direction = ParameterDirection.Input;
                SqlParameter messageParameter = new SqlParameter("@message", SqlDbType.NVarChar, 100);
                messageParameter.Direction = ParameterDirection.Output;

                da.SelectCommand.Parameters.Add(dateParameter);
                da.SelectCommand.Parameters.Add(messageParameter);

                da.Fill(dt);
                con.Close();

                return da.SelectCommand.Parameters["@message"].Value.ToString();
            }
            catch (Exception e)
            {
                string mes = e.Message;
                return "系統出現問題，請找管理員處理";
            }

        }


    }
}