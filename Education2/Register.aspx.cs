using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Education2
{
    public partial class Register1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
               
            }
        }

        private void role()
        {
            DataSQL DA = new DataSQL();
            DataTable dt = DA.GetRows("select * from dbo.Role").Tables[0];
            ddlrole.Items.Clear();

            ddlrole.DataSource = dt;
            ddlrole.DataTextField = "RoleName";
            ddlrole.DataValueField = "RoleID";
            ddlrole.DataBind();
            ddlrole.Items.Insert(0, new ListItem("--請選擇--", ""));
        }

       

        protected void submit1_Click(object sender, EventArgs e)
        {
            if (txtempno.Text.Trim() == "" || txtname.Text.Trim() == "" || txtmail.Text.Trim() == "" || txtpass.Text.Trim() == "" || ddlrole.Text.Trim() == "")
            {
                RegisterStartupScript("", "<script>alert('資料不能為空')</script>");
                return;
            }
            else
            {
                DataSQL DA = new DataSQL();


                DataTable yz = DA.GetRows("select * from dbo.Users where  UserEmpNo='" + txtempno.Text.Trim() + "' ").Tables[0];

                if (yz.Rows.Count > 0)
                {
                    RegisterStartupScript("", "<script>alert('工號存在')</script>");
                    return;
                }



                DA.ExecuteReader("insert into Users (UserEmpNo,UserName,PassWord,Email) values ('" + txtempno.Text.Trim() + "','" + txtname.Text.Trim() + "','" + txtpass.Text.Trim() + "','" + txtmail.Text.Trim() + "')");
                DataTable dt = DA.GetRows("select * from dbo.Users where UserEmpNo='" + txtempno.Text.Trim() + "' and PassWord='" + txtpass.Text.Trim() + "' ").Tables[0];
                int userid = Convert.ToInt32(dt.Rows[0]["UserID"].ToString());
                DA.ExecuteReader("insert into UserRole (UserID,RoleID) values ('" + userid + "','" + ddlrole.SelectedValue.Trim() + "')");
                RegisterStartupScript("", "<script>alert('創建成功')</script>");
            }
        }

        protected void back_Click(object sender, EventArgs e)
        {
            Response.Redirect("Login.aspx?");
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


                DataTable dt = DA.GetRows("select * from dbo.HR_Employee where Emp_OutDate>GETDATE()  and isnull(Emp_No,'')='" + txtempno.Text.Trim() + "'  ").Tables[0];

                if (dt.Rows.Count > 0)
                {
                  
                    txtname.Text = dt.Rows[0]["Emp_Name"].ToString();
                    txtmail.Text = dt.Rows[0]["Emp_eMail"].ToString();
                }
                else
                {
                    txtname.Text = "";
                    txtempno.Text = "";
                    RegisterStartupScript("", "<script>alert('該人員工號不存在,請重新輸入!')</script>");
                }
            }

        }

    }
}