using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Education2
{
    public partial class Report : System.Web.UI.Page
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
                ddldept.Items.Insert(0, new ListItem("--請選擇--", ""));
                State();
                Bind(txtemp.Text.Trim(), Dll_States.SelectedValue.Trim(),ddlfactory.SelectedValue.Trim(), ddldept.SelectedValue.Trim(), txtdatestar.Value.Trim(), txtdateend.Value.Trim());
            }
        }

        protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
        {
            Bind(txtemp.Text.Trim(), Dll_States.SelectedValue.Trim(), ddlfactory.SelectedValue.Trim(), ddldept.SelectedValue.Trim(), txtdatestar.Value.Trim(), txtdateend.Value.Trim());
        
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

        private void BindDeptData(string Factory)
        {
            ddldept.Items.Clear();
            DataTable dt = DA.GetRows(" select * from dbo.HR_Dept where Factory='" + Factory + "' and isnull(DeptName,'')<>'-'  ").Tables[0];
            ddldept.DataSource = dt;
            ddldept.DataTextField = "DeptName";
            ddldept.DataValueField = "DeptNo";
            ddldept.DataBind();
            ddldept.Items.Insert(0, new ListItem("--請選擇--", ""));
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
        private void Bind(string emp, string state,string factory, string dept, string star, string end)
        {
            DataSQL DA = new DataSQL();

            string strWhere = "";
            if (!string.IsNullOrEmpty(emp))
            {
                strWhere = " and (a.EmpNo='" + emp + "' or a.EmpName='" + emp + "')";
            }

            if (!string.IsNullOrEmpty(state))
            {
                strWhere = strWhere + " and a.States='" + state + "'";
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

            if (!string.IsNullOrEmpty(factory))
            {
                strWhere = strWhere + " and a.Factory='" + factory + "'";
            }

           if (!string.IsNullOrEmpty(dept))
           {
               strWhere = strWhere + " and a.Dept='" + dept + "'";
           }
            
            //else
            //{
            //    //ToString().PadLeft(6, '0');
            //    DataTable user = DA.GetRows("select * from Users where UserID='" + userid + "'").Tables[0];
            //    string empno = user.Rows[0]["UserEmpNo"].ToString();
            //    strWhere = strWhere + " and a.ManagerNo='" + empno.ToString().PadLeft(8, '0') + "'";
            //}
            DataTable dt = DA.GetRows("select a.*,b.StateName,isnull(c.DeptName,a.Dept) as  Depts from  dbo.ReleaseApply  a inner join  [State] b   on a.States=b.StateID left join  dbo.HR_Dept c  on  a.Dept=c.DeptNo where 1=1 " + strWhere + " order by a.UpdateDate desc ").Tables[0];
            gdv1.DataSource = dt;
            gdv1.DataBind();
        }


        protected void gridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gdv1.PageIndex = e.NewPageIndex;
            Bind(txtemp.Text.Trim(), Dll_States.SelectedValue.Trim(), ddlfactory.SelectedValue.Trim(), ddldept.SelectedValue.Trim(), txtdatestar.Value.Trim(), txtdateend.Value.Trim());
        }

        protected void gridView_OnRowCreated(object sender, GridViewRowEventArgs e)
        {

        }

        protected void gridView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowIndex != -1)
            {
                int indexID = this.gdv1.PageIndex * this.gdv1.PageSize + e.Row.RowIndex + 1;
                e.Row.Cells[0].Text = indexID.ToString();
            }

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (e.Row.Cells[9].Text == "待主管審核")
                    e.Row.Cells[9].ForeColor = System.Drawing.Color.Red;
                else
                    e.Row.Cells[9].ForeColor = System.Drawing.Color.Black;
                e.Row.Cells[10].ToolTip = e.Row.Cells[10].Text;
                if (e.Row.Cells[10].Text.Length > 15)
                {
                    e.Row.Cells[10].Text = e.Row.Cells[10].Text.Substring(0, 15) + "..";
                }
                e.Row.Cells[3].ToolTip = e.Row.Cells[3].Text;
                if (e.Row.Cells[3].Text.Length > 30)
                {
                    e.Row.Cells[3].Text = e.Row.Cells[3].Text.Substring(0, 30) + "..";
                }
                e.Row.Cells[4].ToolTip = e.Row.Cells[4].Text;
                if (e.Row.Cells[4].Text.Length > 30)
                {
                    e.Row.Cells[4].Text = e.Row.Cells[4].Text.Substring(0, 30) + "..";
                }
            }
        }

        protected void ImageButton2_Click(object sender, ImageClickEventArgs e)
        {
            this.gdv1.AllowPaging = false;
            this.gdv1.AllowSorting = false;
            Bind(txtemp.Text.Trim(), Dll_States.SelectedValue.Trim(), ddlfactory.SelectedValue.Trim(), ddldept.SelectedValue.Trim(), txtdatestar.Value.Trim(), txtdateend.Value.Trim());
            toExcel(this.gdv1);
            this.gdv1.AllowPaging = true;
            this.gdv1.AllowSorting = true;
            Bind(txtemp.Text.Trim(), Dll_States.SelectedValue.Trim(), ddlfactory.SelectedValue.Trim(), ddldept.SelectedValue.Trim(), txtdatestar.Value.Trim(), txtdateend.Value.Trim());
        }


        public void toExcel(GridView gv)
        {
            Response.Charset = "UTF8";
            Response.ContentEncoding = System.Text.Encoding.UTF8;

            string fileName = "export.xls";
            //string style = @"<style> .text { mso-number-format:\@; } </script> ";
            Response.ClearContent();
            Response.AddHeader("content-disposition", "attachment; filename=" + fileName);
            Response.ContentType = "application/excel";
            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);
            this.gdv1.RenderControl(htw);
            //Response.Write(style);
            //Response.Write(sw.ToString());
            Response.Output.Write("<meta http-equiv=\"content-type\" content=\"application/ms-excel; charset=UTF-8\"/>");
            Response.Output.Write(sw.ToString());
            
            Response.End();
        }


        public override void VerifyRenderingInServerForm(Control control)
        {

            /* Verifies that the control is rendered */

        }

        protected void ddlfactory_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlfactory.SelectedIndex == 0)
            {
                BindFactoryData();
                ddldept.Items.Insert(0, new ListItem("--請選擇--", ""));
            }
            else
            {
                BindDeptData(ddlfactory.SelectedValue);
            }
        } 



    }
}