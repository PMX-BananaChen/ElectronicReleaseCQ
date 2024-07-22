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
    public partial class Login : System.Web.UI.Page
    {
        DataSQL DA = new DataSQL();

    
 

        protected void Page_Load(object sender, EventArgs e)
        {

            if (!Page.IsPostBack)
            {

                string Acount = this.Request.ServerVariables["LOGON_USER"];
                Acount = @"PCN\chengjun.liao";
            
                if (!string.IsNullOrEmpty(Acount))
                    {
                        Acount=Acount.ToLower();
                        DataTable dt = DA.GetRows("select a.*,b.RoleID from dbo.Users a  inner join  UserRole b  on a.UserID=b.UserID where isnull(a.UserAccount,'')=  '" + Acount + "' and a.Enabled='0' ").Tables[0];
                        if (dt.Rows.Count > 0)
                        {
                            string userid = dt.Rows[0]["UserID"].ToString();
                            string roleid = dt.Rows[0]["RoleID"].ToString();

                            Session["language"] = userid;

                            if (roleid == "2" || roleid == "1")
                            {
                                Response.Redirect("ApprovePost.aspx");
                            }
                            else
                            {
                                Response.Redirect("UserApply.aspx");
                            }

                            //Response.Redirect("UserApply.aspx?UserID=" + userid + "");
                            
                        }
                        else
                        {
                            Response.Redirect("Error2.aspx");
                        }
                    }
                    else 
                    {
                        Response.Redirect("Error.aspx");
                    }

                 
                   
              
            }
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
           
            
        }  
    }
}