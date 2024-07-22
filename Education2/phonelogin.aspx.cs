using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace Education2
{
    public partial class phonelogin : System.Web.UI.Page
    {
        DataSQL DA = new DataSQL();

         string userid;


        protected void Page_Load(object sender, EventArgs e)
        {

        }


        protected void submit1_Click(object sender, EventArgs e)
        {

        }


        protected void register_Click(object sender, EventArgs e)
        {
            Response.Redirect("Register.aspx?");
        }

        protected void btnlogin_Click(object sender, EventArgs e)
        {
            try
            {
                DataSQL DA = new DataSQL();
                DataTable dt = DA.GetRows("select * from dbo.Users where Email= '" + txtuser.Text.Trim() + "' and Enabled='0' and PassWord='" + txtpass.Text.Trim() + "' ").Tables[0];
                if (txtuser.Text.Trim() == "")
                {
                    RegisterStartupScript("", "<script>alert('用戶名不能為空!')</script>");
                }
                else if (txtpass.Text.Trim() == "")
                {
                    RegisterStartupScript("", "<script>alert('密碼不能為空!')</script>");
                }
                else if (dt.Rows.Count > 0)
                {
                    userid = dt.Rows[0]["UserID"].ToString();
                    //Response.Redirect("UserApply.aspx?UserID=" + userid + "");
                    Response.Redirect("ApprovePhone.aspx");
                }
                else
                {
                    RegisterStartupScript("", "<script>alert('用戶名密碼匹配錯誤')</script>");
                }
            }
            catch
            {
                RegisterStartupScript("", "<script>alert('用戶名不存在')</script>");
            }

        }
    }
}