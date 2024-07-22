using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Education2
{
    public partial class PersonalDocuments : System.Web.UI.Page
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

                BindFactoryData();
                ddlDept.Items.Insert(0, new ListItem("--請選擇--", ""));
                State();
                Bind(txtemp.Text.Trim(),ddlfactory.SelectedValue,ddlDept.SelectedValue, Dll_States.SelectedValue.Trim(), txtdatestar.Value.Trim(), txtdateend.Value.Trim());
            }
        }

        private void Bind(string emp, string factory,string dept, string state,string star,string end)
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

            string strWhere = "";
            if (!string.IsNullOrEmpty(emp))
            {
                strWhere = " and (a.EmpNo='" + emp + "' or a.EmpName='" + emp + "')";
            }

            if (!string.IsNullOrEmpty(state))
            {
                strWhere = strWhere + " and a.States='" + state + "'";
            }
            if (!string.IsNullOrEmpty(factory))
            {
                strWhere = strWhere + " and a.Factory='" + factory + "'";
            }
            if (!string.IsNullOrEmpty(dept))
            {
                strWhere = strWhere + " and a.Dept='" + dept + "'";
            }

            if (!string.IsNullOrEmpty(star) && string.IsNullOrEmpty(end))
            {

                star = DateTime.ParseExact(txtdatestar.Value.Trim(), "yyyyMMdd", System.Globalization.CultureInfo.CurrentCulture).ToShortDateString().ToString();
                

                strWhere = strWhere + " and Date>='" + star + "'";
            }
            else if (string.IsNullOrEmpty(star) && !string.IsNullOrEmpty(end))
            {
               
                end = DateTime.ParseExact(txtdateend.Value.Trim(), "yyyyMMdd", System.Globalization.CultureInfo.CurrentCulture).ToShortDateString().ToString();
                
                strWhere = strWhere + " and Date<='" + end + "'";
            }
            else if (!string.IsNullOrEmpty(star) && !string.IsNullOrEmpty(end))
            {
                star = DateTime.ParseExact(txtdatestar.Value.Trim(), "yyyyMMdd", System.Globalization.CultureInfo.CurrentCulture).ToShortDateString().ToString();
                end = DateTime.ParseExact(txtdateend.Value.Trim(), "yyyyMMdd", System.Globalization.CultureInfo.CurrentCulture).ToShortDateString().ToString();
                
                strWhere = strWhere + " and Date between '" + star + "' and '" + end + "'   ";
            }

            DataTable dt = DA.GetRows("select a.*,b.StateName,isnull(c.DeptName,a.Dept) as  Depts,UserName from  dbo.ReleaseApply  a inner join  [State] b   on a.States=b.StateID left join  dbo.HR_Dept c   on  a.Dept=c.DeptNo inner join  dbo.Users d on  a.CreateUser=d.UserID where 1=1 and a.CreateUser='"+userid+"' " + strWhere + " order by a.UpdateDate desc ").Tables[0];
            gdv1.DataSource = dt;
            gdv1.DataBind();
        }
        protected void gridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gdv1.PageIndex = e.NewPageIndex;
            Bind(txtemp.Text.Trim(), ddlfactory.SelectedValue, ddlDept.SelectedValue, Dll_States.SelectedValue.Trim(), txtdatestar.Value.Trim(), txtdateend.Value.Trim());
        }

        private void BindFactoryData()
        {
            ddlfactory.Items.Clear();
            DataTable dt = DA.GetRows("select distinct Factory from dbo.HR_Dept where  ISNULL(Factory,'')<>'' order by Factory").Tables[0];
            ddlfactory.DataSource = dt;
            ddlfactory.DataTextField = "Factory";
            ddlfactory.DataValueField = "Factory";
            ddlfactory.DataBind();
            ddlfactory.Items.Insert(0, new ListItem("--請選擇--", ""));
        }
        protected void gridView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (e.Row.Cells[8].Text == "待主管審核")
                    e.Row.Cells[8].ForeColor = System.Drawing.Color.Red;
                else
                    e.Row.Cells[8].ForeColor = System.Drawing.Color.Black;



                e.Row.Cells[10].ToolTip = e.Row.Cells[10].Text;

                if (e.Row.Cells[10].Text.Length > 15)
                {
                    e.Row.Cells[10].Text = e.Row.Cells[10].Text.Substring(0, 15) + "..";
                }

                e.Row.Cells[2].ToolTip = e.Row.Cells[2].Text;

                if (e.Row.Cells[2].Text.Length > 30)
                {
                    e.Row.Cells[2].Text = e.Row.Cells[2].Text.Substring(0, 30) + "..";
                }

                e.Row.Cells[3].ToolTip = e.Row.Cells[3].Text;

                if (e.Row.Cells[3].Text.Length > 30)
                {
                    e.Row.Cells[3].Text = e.Row.Cells[3].Text.Substring(0, 30) + "..";
                }


            }

        }
        private void State()
        {
            Dll_States.Items.Clear();
            DataTable dt = DA.GetRows("select * from State").Tables[0];
            Dll_States.DataSource = dt;
            Dll_States.DataTextField = "StateName";
            Dll_States.DataValueField = "StateID";
            Dll_States.DataBind();
        }
        protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
        {
            Bind(txtemp.Text.Trim(), ddlfactory.SelectedValue, ddlDept.SelectedValue, Dll_States.SelectedValue.Trim(), txtdatestar.Value.Trim(), txtdateend.Value.Trim());
        }

        protected void gridView_OnRowCreated(object sender, GridViewRowEventArgs e)
        {

        }

        protected void ddlfactory_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlfactory.SelectedIndex == 0)
            {
                BindFactoryData();
                ddlDept.Items.Insert(0, new ListItem("--請選擇--", ""));
            }
            else
            {
                BindDeptData(ddlfactory.SelectedValue);
            }
        }
        private void BindDeptData(string Factory)
        {
            ddlDept.Items.Clear();
            DataTable dt = DA.GetRows(" select * from dbo.HR_Dept where Factory='" + Factory + "' and isnull(DeptName,'')<>'-'  ").Tables[0];
            ddlDept.DataSource = dt;
            ddlDept.DataTextField = "DeptName";
            ddlDept.DataValueField = "DeptNo";
            ddlDept.DataBind();
            ddlDept.Items.Insert(0, new ListItem("--請選擇--", ""));
           
        }
    }
}