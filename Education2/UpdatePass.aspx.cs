using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Education2
{
    public partial class UpdatePass : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void txtempno_TextChanged(object sender, EventArgs e)
        {
            if (txtempno.Text.Trim() == "")
            {
                RegisterStartupScript("", "<script>alert('資料不能為空')</script>");
                return;
            }
            else
            {
                DataSQL DA = new DataSQL();


                DataTable dt = DA.GetRows("select * from dbo.Users where UserEmpNo='" + txtempno.Text.Trim() + "'  ").Tables[0];

                if (dt.Rows.Count > 0)
                {
                   
                }
                else
                {
                    txtempno.Text = "";
                    RegisterStartupScript("", "<script>alert('該人員工號不存在,請返回界面點擊註冊!')</script>");
                }
            }
        }

        protected void submit1_Click(object sender, EventArgs e)
        {
            if (txtempno.Text.Trim() == "" || txtold.Text.Trim() == "" || txtpass2.Text.Trim() == "" || txtpass.Text.Trim() == "")
            {
                RegisterStartupScript("", "<script>alert('資料不能為空')</script>");
                return;
            }
            else
            {
                DataSQL DA = new DataSQL();


                DataTable dt = DA.GetRows("select * from dbo.Users where  UserEmpNo='" + txtempno.Text.Trim() + "' ").Tables[0];
                DataTable dt2 = DA.GetRows("select * from dbo.Users where  UserEmpNo='" + txtempno.Text.Trim() + "' and PassWord='" + txtold.Text.Trim() + "' ").Tables[0];
                if (dt.Rows.Count < 1)
                {
                    RegisterStartupScript("", "<script>alert('工號不存在,請返回頁面點擊註冊')</script>");
                    return;
                }
                else if (dt2.Rows.Count < 1)
                {
                    RegisterStartupScript("", "<script>alert('密碼錯誤,請重新輸入!')</script>");
                    return;
                }
                else if (txtpass.Text.Trim() != txtpass2.Text.Trim())
                {
                    RegisterStartupScript("", "<script>alert('兩次密碼輸入不一致,請重新輸入!')</script>");
                    return;
                }
                else if (txtpass.Text.Trim() == txtold.Text.Trim())
                {
                    RegisterStartupScript("", "<script>alert('新密碼不能與舊密碼相同!')</script>");
                    return;
                }
                else
                {
                    DA.ExecuteReader("update Users set  PassWord='" + txtpass.Text.Trim() + "'  where UserEmpNo='" + txtempno.Text.Trim() + "' ");

                    RegisterStartupScript("", "<script>alert('修改成功')</script>");
                }
            }
        }

        protected void back_Click(object sender, EventArgs e)
        {
            Response.Redirect("Login.aspx?");
        }

    }
}