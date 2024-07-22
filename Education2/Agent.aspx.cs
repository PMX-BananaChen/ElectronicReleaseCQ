using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Education2
{
    public partial class Agent : System.Web.UI.Page
    {
          
        DataSQL DA = new DataSQL();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                //userid = Login.userid;
                //if (userid == null)
                //{
                //    Response.Redirect("Error3.aspx");
                //    return;
                //}

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

               

                //BindFactoryData();
                //ddlDept.Items.Insert(0, new ListItem("--請選擇--", ""));
                DateTime now = DateTime.Now;
                txtdatestar.Value = now.ToString("yyyyMMdd");
                this.Page.Form.DefaultButton = ImageButton4.ClientID.Replace('_', '$'); //设置默认按钮
                Bind(txtdatestar.Value, txtdateend.Value,  txtDLempno.Text.Trim(), txtDLempname.Text.Trim());
            }
        }

        private void Bind(string star, string end, string dlmanagerno, string dlmanagername)
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


         
            DataTable dt0 = DA.GetRows("select *  from  dbo.users where  userid='" + userid + "' ").Tables[0];

            string empno = "";
       
            if (dt0.Rows.Count > 0)
            {
                empno = dt0.Rows[0]["UserEmpNo"].ToString();
              
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(UpdatePanel1, typeof(UpdatePanel), "提示", "alert('無法獲取到登錄的賬號！');", true);
                return;
            }

            string strWhere = "";


            //string managerno2 = managerno;

            //if (!string.IsNullOrEmpty(managerno))
            //{
            //    managerno2 = managerno.ToString().PadLeft(8, '0');
            //}

            string dlmanagerno2 = dlmanagerno;

            if (!string.IsNullOrEmpty(dlmanagerno))
            {
                dlmanagerno2 = dlmanagerno.ToString().PadLeft(8, '0');
            }


            //if (!string.IsNullOrEmpty(managerno2))
            //{
            //    strWhere = strWhere + " and ManagerNo='" + managerno2 + "'";
            //}
            //if (!string.IsNullOrEmpty(managername))
            //{
            //    strWhere = strWhere + " and ManagerName='" + managername + "'";
            //}
            if (!string.IsNullOrEmpty(dlmanagerno2))
            {
                strWhere = strWhere + " and DLManagerNo='" + dlmanagerno2 + "'";
            }
            if (!string.IsNullOrEmpty(dlmanagername))
            {
                strWhere = strWhere + " and DLManagerName='" + dlmanagername + "'";
            }


            if (!string.IsNullOrEmpty(star) && string.IsNullOrEmpty(end))
            {
                star = DateTime.ParseExact(txtdatestar.Value.Trim(), "yyyyMMdd", System.Globalization.CultureInfo.CurrentCulture).ToShortDateString().ToString();

                strWhere = strWhere + " and Dates>='" + star + "'";
            }
            else if (string.IsNullOrEmpty(star) && !string.IsNullOrEmpty(end))
            {

                end = DateTime.ParseExact(txtdateend.Value.Trim(), "yyyyMMdd", System.Globalization.CultureInfo.CurrentCulture).ToShortDateString().ToString();


                strWhere = strWhere + " and Dates<='" + end + "'";
            }
            else if (!string.IsNullOrEmpty(star) && !string.IsNullOrEmpty(end))
            {
                star = DateTime.ParseExact(txtdatestar.Value.Trim(), "yyyyMMdd", System.Globalization.CultureInfo.CurrentCulture).ToShortDateString().ToString();
                end = DateTime.ParseExact(txtdateend.Value.Trim(), "yyyyMMdd", System.Globalization.CultureInfo.CurrentCulture).ToShortDateString().ToString();

                strWhere = strWhere + " and Dates>='" + star + "'  and  Dates<='" + end + "'   ";
            }


           
     
            DataTable dt = DA.GetRows("select * from  Agent where 1=1  " + strWhere + "  and   Enabled='0'   and ManagerNo='" + empno + "'  order by Dates  ").Tables[0];
            gdv1.DataSource = dt;
            gdv1.DataBind();

          
        }
        protected void gridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gdv1.PageIndex = e.NewPageIndex;
            Bind(txtdatestar.Value, txtdateend.Value,  txtDLempno.Text.Trim(), txtDLempname.Text.Trim());
        }

        //private void BindFactoryData()
        //{
        //    ddlfactory.Items.Clear();
        //    DataTable dt = DA.GetRows("select distinct Factory from dbo.HR_Dept where  ISNULL(Factory,'')<>'' order by Factory").Tables[0];
        //    ddlfactory.DataSource = dt;
        //    ddlfactory.DataTextField = "Factory";
        //    ddlfactory.DataValueField = "Factory";
        //    ddlfactory.DataBind();
        //    ddlfactory.Items.Insert(0, new ListItem("--請選擇--", ""));
        //}
        protected void gridView_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowIndex != -1)
            {
                int indexID = this.gdv1.PageIndex * this.gdv1.PageSize + e.Row.RowIndex + 1;
                e.Row.Cells[0].Text = indexID.ToString();
            }

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //if (e.Row.Cells[8].Text == "待主管審核")
                //    e.Row.Cells[8].ForeColor = System.Drawing.Color.Red;
                //else
                //    e.Row.Cells[8].ForeColor = System.Drawing.Color.Black;



                //e.Row.Cells[3].ToolTip = e.Row.Cells[3].Text;

                //if (e.Row.Cells[3].Text.Length > 15)
                //{
                //    e.Row.Cells[3].Text = e.Row.Cells[3].Text.Substring(0, 15) + "..";
                //}

                ((LinkButton)(e.Row.Cells[6].Controls[0])).Attributes.Add("onclick", "return confirm('确认删除吗？')");
                DateTime now = DateTime.Now;
                DateTime TimeA = Convert.ToDateTime(now.ToString("yyyy-MM-dd"));

                if (e.Row.RowType == DataControlRowType.DataRow)
                {

                    DateTime Timeb = Convert.ToDateTime(e.Row.Cells[3].Text);

                    if (TimeA > Timeb)
                    {
                        e.Row.Cells[6].Visible = false;

                    }


                }

            }

        }
      
        protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
        {
            Bind(txtdatestar.Value, txtdateend.Value,  txtDLempno.Text.Trim(), txtDLempname.Text.Trim());
        }

        protected void gridView_OnRowCreated(object sender, GridViewRowEventArgs e)
        {

        }

        //protected void ddlfactory_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    if (ddlfactory.SelectedIndex == 0)
        //    {
        //        BindFactoryData();
        //        ddlDept.Items.Insert(0, new ListItem("--請選擇--", ""));
        //    }
        //    else
        //    {
        //        BindDeptData(ddlfactory.SelectedValue);
        //    }
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

        protected void gdv1_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            string id = this.gdv1.DataKeys[e.RowIndex].Values[0].ToString();
            if (!string.IsNullOrEmpty(id))
            {
                DA.ExecuteReader("update Agent set Enabled='1'  where   ID='" + id + "' ");
                gdv1.EditIndex = -1;
                Bind(txtdatestar.Value, txtdateend.Value, txtDLempno.Text.Trim(), txtDLempname.Text.Trim());
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "click", "alert('刪除成功!');location.replace('Agent.aspx');", true);

            }

        }

        protected void ImageButton8_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("AddAgent2.aspx");
        }

        protected void txtempno_TextChanged(object sender, EventArgs e)
        {

        }
    }
}