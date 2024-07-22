using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Education2
{
    public partial class AgentDetail : System.Web.UI.Page
    {
          
        DataSQL DA = new DataSQL();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                string userid = null;

                if (Session["language"] == null)
                {
                    Response.Redirect("Error3.aspx");
                    return;
                }
                else
                {
                    userid = Session["language"].ToString();
                }

                string id = this.Request.QueryString["id"].ToString();
                DataTable dt = DA.GetRows("select Convert(varchar(10),Star,120) as Star2, Convert(varchar(10),[End],120) as End2,*  from  dbo.Agent where  ID='" + id + "'").Tables[0];
                 if (dt.Rows.Count > 0)
                 {
                     txtempno.Text = dt.Rows[0]["ManagerNo"].ToString();
                     txtempname.Text = dt.Rows[0]["ManagerName"].ToString();
                     txtmail.Text = dt.Rows[0]["ManagerMail"].ToString();

                     txtDLempno.Text = dt.Rows[0]["DLManagerNo"].ToString();
                     txtDLempname.Text = dt.Rows[0]["DLManagerName"].ToString();
                     txtDLmail.Text = dt.Rows[0]["DLManagerMail"].ToString();
                  

                     txtdatestar.Value = dt.Rows[0]["Star2"].ToString().Replace("-", "");
                     txtdateend.Value = dt.Rows[0]["End2"].ToString().Replace("-", "");

                     rdagent.SelectedValue = dt.Rows[0]["Enabled"].ToString();
                     if (rdagent.SelectedValue == "0")
                     {
                         txtdatestar.Disabled = false;
                         txtdateend.Disabled = false;
                         txtDLempno.Enabled = true;
                         txtDLempname.Enabled = true;
                         txtDLmail.Enabled = true;

                         this.txtdatestar.Style.Add("background-color", "#FFFFFF");
                         this.txtdateend.Style.Add("background-color", "#FFFFFF");
                         //txtdateend.BackColor = System.Drawing.Color.FromArgb(255, 255, 255);
                         txtDLempno.BackColor = System.Drawing.Color.FromArgb(255, 255, 255);
                         txtDLempname.BackColor = System.Drawing.Color.FromArgb(255, 255, 255);
                         txtDLmail.BackColor = System.Drawing.Color.FromArgb(255, 255, 255);
                     }
                     else
                     {
                         txtdatestar.Disabled = true;
                         txtdateend.Disabled = true;
                         txtDLempno.Enabled = false;
                         txtDLempname.Enabled = false;
                         txtDLmail.Enabled = false;
                         this.txtdatestar.Style.Add("background-color", "#FFCCCC");
                         this.txtdateend.Style.Add("background-color", "#FFCCCC");
                         txtDLempno.BackColor = System.Drawing.Color.FromArgb(255, 202, 202);
                         txtDLempname.BackColor = System.Drawing.Color.FromArgb(255, 202, 202);
                         txtDLmail.BackColor = System.Drawing.Color.FromArgb(255, 202, 202);
                     }





                     //BindFactoryData();
                     //if (ddlFactory.Items.FindByValue(dt.Rows[0]["BU"].ToString()) != null)
                     //{
                     //    ddlFactory.Items.FindByValue(dt.Rows[0]["BU"].ToString()).Selected = true;
                     //    BindDeptData(ddlFactory.SelectedValue);
                     //    if (ddlDept.Items.FindByValue(dt.Rows[0]["Dept_No"].ToString()) != null)
                     //    {
                     //        ddlDept.Items.FindByValue(dt.Rows[0]["Dept_No"].ToString()).Selected = true;
                     //    }
                     //}
                 }
            }
        }

        protected void txtempno_TextChanged1(object sender, EventArgs e)
        {
            DataTable dt = DA.GetRows("select * from dbo.HR_Employee where Emp_OutDate>GETDATE()  and  isnull(Emp_No,'')='" + txtempno.Text.Trim() + "'  ").Tables[0];

            if (dt.Rows.Count > 0)
            {
                txtempname.Text = dt.Rows[0]["Emp_Name"].ToString();
                txtmail.Text = dt.Rows[0]["Emp_eMail"].ToString();

                //BindFactoryData();
                //if (ddlFactory.Items.FindByValue(dt.Rows[0]["Factory"].ToString()) != null)
                //{
                //    ddlFactory.Items.FindByValue(dt.Rows[0]["Factory"].ToString()).Selected = true;

                //    BindDeptData(ddlFactory.SelectedValue);
                //    if (ddlDept.Items.FindByValue(dt.Rows[0]["Dept_No"].ToString()) != null)
                //    {
                //        ddlDept.Items.FindByValue(dt.Rows[0]["Dept_No"].ToString()).Selected = true;
                //    }
                //}
            }
            else
            {
                txtempno.Text = "";
                txtempname.Text = "";
                txtmail.Text = "";
                
                //ScriptManager.RegisterStartupScript(UpdatePanel1, this.GetType(), "alert", "alert('工號不存在,請手工輸入姓名與郵箱!'')", true);
                ScriptManager.RegisterClientScriptBlock(UpdatePanel1, typeof(UpdatePanel), "提示", "alert('工號不存在!');", true);
            }
        }

        //private void BindFactoryData()
        //{
        //    ddlFactory.Items.Clear();
        //    DataTable dt = DA.GetRows("select distinct Factory from dbo.HR_Dept where  ISNULL(Factory,'')<>'' order by Factory").Tables[0];
        //    ddlFactory.DataSource = dt;
        //    ddlFactory.DataTextField = "Factory";
        //    ddlFactory.DataValueField = "Factory";
        //    ddlFactory.DataBind();
        //    ddlFactory.Items.Insert(0, new ListItem("--請選擇--", ""));
        //}

        //private void BindDeptData(string Factory)
        //{
        //    ddlDept.Items.Clear();
        //    DataTable dt = DA.GetRows(" select * from dbo.HR_Dept where Factory='" + Factory + "' and isnull(DeptName,'')<>'-'  ").Tables[0];
        //    ddlDept.DataSource = dt;
        //    ddlDept.DataTextField = "DeptName";
        //    ddlDept.DataValueField = "DeptNo";
        //    ddlDept.DataBind();
        //    ddlDept.Items.Insert(0, new ListItem("--請選擇--", ""));
        //}


     
        //protected void ddlfactory_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    if (ddlFactory.SelectedIndex == 0)
        //    {
        //        BindFactoryData();
        //        ddlDept.Items.Insert(0, new ListItem("--請選擇--", ""));
        //    }
        //    else
        //    {
        //        BindDeptData(ddlFactory.SelectedValue);
        //    }
        //}

        protected void btnsubmit_Click(object sender, EventArgs e)
        {

            if (rdagent.SelectedValue == "0")
            {
                if (txtempno.Text.Trim() == "" || txtempname.Text.Trim() == "" || txtmail.Text.Trim() == "" || txtdatestar.Value.Trim() == "" || txtdateend.Value.Trim() == "" || txtDLempno.Text.Trim() == "" || txtDLempname.Text.Trim() == "" || txtDLmail.Text.Trim() == "")
                {
                    ScriptManager.RegisterClientScriptBlock(UpdatePanel1, typeof(UpdatePanel), "提示", "alert('代理信息不能為空!');", true);
                    return;
                }
                else
                {
                    string userid = null;

                    if (Session["language"] == null)
                    {
                        Response.Redirect("Error3.aspx");
                        return;
                    }
                    else
                    {
                        userid = Session["language"].ToString();
                    }

                    if (!string.IsNullOrEmpty(userid))
                    {
                        DateTime star = DateTime.ParseExact(txtdatestar.Value.Trim(), "yyyyMMdd", System.Globalization.CultureInfo.CurrentCulture);
                        DateTime end = DateTime.ParseExact(txtdateend.Value.Trim(), "yyyyMMdd", System.Globalization.CultureInfo.CurrentCulture);
                        string star2 = star.ToString("yyyy-MM-dd");
                        string end2 = end.ToString("yyyy-MM-dd");

                        string id = this.Request.QueryString["id"].ToString();

                        DA.ExecuteReader("update  Agent set  DLManagerNo='" + txtDLempno.Text.Trim() + "',DLManagerName='" + txtDLempname.Text.Trim() + "', DLManagerMail=  '" + txtDLmail.Text.Trim() + "',Star='" + star2 + "',[End]='" + end2 + "',UpdateDate=getdate(),UpdateUser='" + userid + "',Enabled='0'  where   ID='" + id + "' ");
                        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "click", "alert('已修改完成!');location.replace('Agent.aspx');", true);

                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(UpdatePanel1, typeof(UpdatePanel), "提示", "alert('無法抓取到修改用戶的ID!');", true);
                        return;
                    }

                }
            }
            else
            {

                string userid = null;

                if (Session["language"] == null)
                {
                    Response.Redirect("Error3.aspx");
                    return;
                }
                else
                {
                    userid = Session["language"].ToString();
                }
                if (!string.IsNullOrEmpty(userid))
                {
                    

                    string id = this.Request.QueryString["id"].ToString();

                    DA.ExecuteReader("update  Agent set  DLManagerNo=null,DLManagerName=null, DLManagerMail=null,Star=null,[End]=null,UpdateDate=getdate(),UpdateUser='" + userid + "' ,Enabled='1' where   ID='" + id + "' ");
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "click", "alert('已修改完成!');location.replace('Agent.aspx');", true);

                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(UpdatePanel1, typeof(UpdatePanel), "提示", "alert('無法抓取到修改用戶的ID!');", true);
                    return;
                }

            }


            
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Response.Redirect("Agent.aspx");
        }

        protected void RadioButtonList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (rdagent.SelectedValue == "0")
            {
               
                txtdatestar.Disabled = false;
                txtdateend.Disabled = false;
                txtDLempno.Enabled = true;
                txtDLempname.Enabled = true;
                txtDLmail.Enabled = true;

                this.txtdatestar.Style.Add("background-color", "#FFFFFF");
                this.txtdateend.Style.Add("background-color", "#FFFFFF");
                //txtdateend.BackColor = System.Drawing.Color.FromArgb(255, 255, 255);
                txtDLempno.BackColor = System.Drawing.Color.FromArgb(255, 255, 255);
                txtDLempname.BackColor = System.Drawing.Color.FromArgb(255, 255, 255);
                txtDLmail.BackColor = System.Drawing.Color.FromArgb(255, 255, 255);
            }
            else
            {
                txtdatestar.Value = "";
                txtdateend.Value = "";
                txtDLempno.Text="";
                txtDLempname.Text="";
                txtDLmail.Text="";
                txtdatestar.Disabled = true;
                txtdateend.Disabled = true;
                txtDLempno.Enabled =false;
                txtDLempname.Enabled = false;
                txtDLmail.Enabled = false;
                this.txtdatestar.Style.Add("background-color", "#FFCCCC");
                this.txtdateend.Style.Add("background-color", "#FFCCCC");
                txtDLempno.BackColor=System.Drawing.Color.FromArgb(255, 202, 202);
                txtDLempname.BackColor = System.Drawing.Color.FromArgb(255, 202, 202);
                txtDLmail.BackColor = System.Drawing.Color.FromArgb(255, 202, 202);
            }
        } 
    }
}