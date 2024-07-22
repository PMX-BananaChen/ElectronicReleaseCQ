using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Education2
{
    public partial class GuardApply : System.Web.UI.Page
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

                txtdatestar.Value = DateTime.Now.ToString("yyyyMMdd");
                BindFactoryData();
                ddlDept.Items.Insert(0, new ListItem("--請選擇--", ""));
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
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

            if (txtempno.Text.Trim() == "" || txtempname.Text.Trim() == "" || ddlFactory.SelectedValue.Trim() == "" || ddlDept.SelectedValue.Trim() == "" || txtmail.Text.Trim() == "" || txtdatestar.Value.Trim() == "" || txttimestar.Value.Trim() == "" || txttimeend.Value.Trim() == "" || txtremark.Text.Trim() == "")
            {
                ScriptManager.RegisterStartupScript(UpdatePanel1, this.GetType(), "alert", "alert('資料不能為空!')", true);
               
                return;
            }
            else
            {

                string date = DateTime.ParseExact(txtdatestar.Value.Trim(), "yyyyMMdd", System.Globalization.CultureInfo.CurrentCulture).ToShortDateString().ToString();
                string star = DateTime.ParseExact(txttimestar.Value.Trim(), "HHmm", System.Globalization.CultureInfo.CurrentCulture).ToString("HH:mm");
                string end = DateTime.ParseExact(txttimeend.Value.Trim(), "HHmm", System.Globalization.CultureInfo.CurrentCulture).ToString("HH:mm");

                ////疫情期間只能申請當天至第三個工作日
                string message = DA.checkDate(date);
                if (!string.IsNullOrEmpty(message))
                {
                    ScriptManager.RegisterClientScriptBlock(UpdatePanel1, typeof(UpdatePanel), "提示", "alert('" + message + "');", true);
                    return;
                }

                DA.ExecuteReader("insert into ReleaseApply (EmpNo,EmpName,Factory,Dept,Mail,Date,StarTime,EndTime,States,Remark,Type,CreateUser,CreateDate,UpdateDate) values ('" + txtempno.Text.Trim() + "','" + txtempname.Text.Trim() + "','" + ddlFactory.SelectedValue.Trim() + "','" + ddlDept.SelectedValue.Trim() + "','" + txtmail.Text.Trim() + "','" + date + "','" + star + "','" +end + "',0,'" + txtremark.Text.Trim() + "','警衛補單','" + userid + "',getdate(),getdate()) ");
                ScriptManager.RegisterStartupScript(UpdatePanel1, this.GetType(), "alert", "alert('登記成功!')", true);
      
            }
        }

        protected void btnback_Click(object sender, EventArgs e)
        {
            txtempno.Text = "";
            txtempname.Text = "";
            ddlFactory.SelectedValue = "";
            ddlDept.SelectedValue = "";
            txtmail.Text = "";
            txtremark.Text = "";
            txtdatestar.Value = "";
       
            txttimestar.Value = "";
            txttimeend.Value = "";
        }

        private void BindFactoryData()
        {
            ddlFactory.Items.Clear();
            DataTable dt = DA.GetRows("select distinct Factory from dbo.HR_Dept").Tables[0];
            ddlFactory.DataSource = dt;
            ddlFactory.DataTextField = "Factory";
            ddlFactory.DataValueField = "Factory";
            ddlFactory.DataBind();
            ddlFactory.Items.Insert(0, new ListItem("--請選擇--", ""));
        }

        private void BindDeptData(string Factory)
        {
            ddlDept.Items.Clear();
            DataTable dt = DA.GetRows(" select * from dbo.HR_Dept where Factory='" + Factory + "'").Tables[0];
            ddlDept.DataSource = dt;
            ddlDept.DataTextField = "DeptName";
            ddlDept.DataValueField = "DeptNo";
            ddlDept.DataBind();
            ddlDept.Items.Insert(0, new ListItem("--請選擇--", ""));
        }

        protected void ddlFactory2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlFactory.SelectedIndex == 0)
            {
                BindFactoryData();
                ddlDept.Items.Insert(0, new ListItem("--請選擇--", ""));
            }
            else
            {
                BindDeptData(ddlFactory.SelectedValue);
            }
        }

        protected void txtempno_TextChanged(object sender, EventArgs e)
        {
            if (txtempno.Text.Trim() == "")
            {
                ScriptManager.RegisterStartupScript(UpdatePanel1, this.GetType(), "alert", "alert('資料不能為空!')", true);
          
                return;
            }
            else
            {
                DataSQL DA = new DataSQL();


                DataTable dt = DA.GetRows("select * from dbo.HR_Employee where isnull(Emp_No,'')='" + txtempno.Text.Trim() + "'  ").Tables[0];

                if (dt.Rows.Count > 0)
                {
                    txtempname.Text = dt.Rows[0]["Emp_Name"].ToString();
                    txtmail.Text = dt.Rows[0]["Emp_eMail"].ToString();


                    BindFactoryData();
                    if (ddlFactory.Items.FindByValue(dt.Rows[0]["Factory"].ToString()) != null)
                    {
                        ddlFactory.Items.FindByValue(dt.Rows[0]["Factory"].ToString()).Selected = true;

                        BindDeptData(ddlFactory.SelectedValue);
                        if (ddlDept.Items.FindByValue(dt.Rows[0]["Dept_No"].ToString()) != null)
                        {
                            ddlDept.Items.FindByValue(dt.Rows[0]["Dept_No"].ToString()).Selected = true;
                        }
                    }
                }
                else
                {
                    txtempname.Text = "";
                    txtmail.Text = "";
                    ddlFactory.SelectedValue = "";
                    ddlDept.SelectedValue = "";
                    ScriptManager.RegisterStartupScript(UpdatePanel1, this.GetType(), "alert", "alert('工號不存在,請手工輸入姓名與郵箱!')", true);
           
                }
            }
        }
    }
}