using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Education2
{
    public partial class AddAgent2 : System.Web.UI.Page
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

                DataTable dt = new DataTable();
                DataColumn dc = null;
                dc = dt.Columns.Add("ID", Type.GetType("System.Int32"));
                dc.AutoIncrement = true;//自动增加
                dc.AutoIncrementSeed = 1;//起始为1
                dc.AutoIncrementStep = 1;//步长为1
                dc.AllowDBNull = false;//
                dt.Columns.Add("DLNO", Type.GetType("System.String"));
                dt.Columns.Add("DLName", Type.GetType("System.String"));
                dt.Columns.Add("DLMail", Type.GetType("System.String"));
                dt.Columns.Add("CKstate", Type.GetType("System.String"));
                dt.Columns.Add("Date", Type.GetType("System.String"));
                dt.Columns.Add("Star", Type.GetType("System.String"));
                dt.Columns.Add("End", Type.GetType("System.String"));
                ViewState["dt"] = dt;
            }
        }

        protected void btnadd_Click(object sender, EventArgs e)
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

            if (txtDLempno.Text.Trim()==""||txtDLempname.Text.Trim()==""||txtDLmail.Text.Trim()==""||txtdatestar.Value.Trim() == "" || txtdateend.Value.Trim() == "" || txttimestar.Value.Trim() == "" || txttimeend.Value.Trim() == "")
            {
                ScriptManager.RegisterClientScriptBlock(UpdatePanel1, typeof(UpdatePanel), "提示", "alert('資料不能為空');", true);

            }
            else
            {
              

                DataTable dt0 = DA.GetRows("select *  from  dbo.users where  userid='" + userid + "' ").Tables[0];

                string empno="";
                string empname = "";
                string mail = "";
                if (dt0.Rows.Count > 0)
                {
                    empno = dt0.Rows[0]["UserEmpNo"].ToString();
                    empname = dt0.Rows[0]["UserName"].ToString();
                    mail = dt0.Rows[0]["Email"].ToString();
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(UpdatePanel1, typeof(UpdatePanel), "提示", "alert('無法獲取到登錄的賬號！');", true);
                    return;
                }

                if (txtDLempno.Text.Trim() == empno)
                {
                    ScriptManager.RegisterClientScriptBlock(UpdatePanel1, typeof(UpdatePanel), "提示", "alert('代理人不能與當前用戶為同一個人!');", true);
                    return;
                }

                

                string time1 = txtdatestar.Value.Trim();
                string time2 = txtdateend.Value.Trim();
                string time3 = txttimestar.Value.Trim();
                string time4 = txttimeend.Value.Trim();
                int a = Convert.ToInt32(time1.Replace("-", ""));
                int b = Convert.ToInt32(time2.Replace("-", ""));
                int c2 = Convert.ToInt32(time3.Replace(":", ""));
                int d = Convert.ToInt32(time4.Replace(":", ""));

                if (a > b)
                {
                    //ScriptManager.RegisterStartupScript(UpdatePanel1, this.GetType(), "alert", "alert('日期时间段不能为空!'')", true);
                    ScriptManager.RegisterClientScriptBlock(UpdatePanel1, typeof(UpdatePanel), "提示", "alert('開始日期不能大於結束日期!');", true);
                    return;
                }
                //如果跨頁並且開始時段與結束時段不相等 則OK
                if (ckbox.Checked == true && c2!=d)
                {
                    if (c2 <= d)
                    {
                        ScriptManager.RegisterClientScriptBlock(UpdatePanel1, typeof(UpdatePanel), "提示", "alert('跨夜狀態選擇的時段不能大於開始時段!');", true);
                        return;
                    }
                }
                else if (c2 >= d )
                {
                    ScriptManager.RegisterClientScriptBlock(UpdatePanel1, typeof(UpdatePanel), "提示", "alert('開始時段不能大於或等於結束時段!');", true);
                    return;
                }

                DateTime star = DateTime.ParseExact(txtdatestar.Value.Trim(), "yyyyMMdd", System.Globalization.CultureInfo.CurrentCulture);


                DateTime end = DateTime.ParseExact(txtdateend.Value.Trim(), "yyyyMMdd", System.Globalization.CultureInfo.CurrentCulture);


                DateTime star2 = DateTime.ParseExact(txttimestar.Value.Trim(), "HHmm", System.Globalization.CultureInfo.CurrentCulture);

                DateTime end2 = DateTime.ParseExact(txttimeend.Value.Trim(), "HHmm", System.Globalization.CultureInfo.CurrentCulture);

                //DateTime star = Convert.ToDateTime(txtdatestar.Value.Trim());
                //DateTime end = Convert.ToDateTime(txtdateend.Value.Trim());

                DateTime now2 = Convert.ToDateTime(DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd"));

                if (star <= now2 || end <= now2)
                {
                    ScriptManager.RegisterClientScriptBlock(UpdatePanel1, typeof(UpdatePanel), "提示", "alert('開始與結束日期不能小於今天!');", true);
                    return;
                }

                DataTable table = ViewState["dt"] as DataTable;
                if (table.Rows.Count >= 90)
                {
                    //ScriptManager.RegisterStartupScript(UpdatePanel1, this.GetType(), "alert", "alert('每次申请不能超过10条!'')", true);
                    ScriptManager.RegisterClientScriptBlock(UpdatePanel1, typeof(UpdatePanel), "提示", "alert('每次申请不能超过90条!');", true);
                }
              
                else
                {
                    TimeSpan c = end - star;
                    int count = Convert.ToInt32(c.Days.ToString()) + 1;
                    int tbcount = table.Rows.Count;
                    if ((tbcount + count) > 90)
                    {
                        ScriptManager.RegisterClientScriptBlock(UpdatePanel1, typeof(UpdatePanel), "提示", "alert('每次申请不能超过90条!');", true);
                        return;
                    }
                    for (DateTime dt = star; dt <= end; dt = dt.AddDays(1))
                    {

                        string ck = "0";
                        if (ckbox.Checked == true)
                        {
                            ck = "1";
                        }
                        string days = dt.ToString("yyyy-MM-dd");
                     


                        DataRow newRow;
                        newRow = (ViewState["dt"] as DataTable).NewRow();
                        newRow["DLNO"] = txtDLempno.Text.Trim();
                        newRow["DLName"] = txtDLempname.Text.Trim();
                        newRow["DLMail"] = txtDLmail.Text.Trim();
                        newRow["CKstate"] = ck;
                        
                        newRow["Date"] = dt.ToString("yyyy-MM-dd");
                        newRow["Star"] = dt.ToString("yyyy-MM-dd")+" "+star2.ToString("HH:mm");

                        string star3 = dt.ToString("yyyy-MM-dd") + " " + star2.ToString("HH:mm");
                        string end3 = "";

                        if (ckbox.Checked == true)
                        {
                            newRow["End"] = dt.AddDays(1).ToString("yyyy-MM-dd") + " " + end2.ToString("HH:mm");
                            end3 = dt.AddDays(1).ToString("yyyy-MM-dd") + " " + end2.ToString("HH:mm");
                        }
                        else
                        {
                            newRow["End"] = dt.ToString("yyyy-MM-dd") + " " + end2.ToString("HH:mm");
                            end3 = dt.ToString("yyyy-MM-dd") + " " + end2.ToString("HH:mm");
                        }


                        DataRow[] drs = table.Select(" (( Star<= '" + star3 + "' and '" + star3 + "' <=End)  or  (Star<= '" + end3 + "' and '" + end3 + "' <=End)  or ( '" + star3 + "'<=Star and Star<='" + end3 + "') or ( '" + star3 + "'<=End and End<='" + end3 + "'))  ");
                        if (drs.Length > 0)
                        {
                            ScriptManager.RegisterClientScriptBlock(UpdatePanel1, typeof(UpdatePanel), "提示", "alert('您添加了重複或交叉時間段的代理!');", true);
                            return;
                        }
                     

                        (ViewState["dt"] as DataTable).Rows.Add(newRow);
                    }
                    GridView1.DataSource = ViewState["dt"] as DataTable;
                    GridView1.DataBind();
                    btnok.Visible = true;
                    btnback.Visible = true;
                }
            }
        }

        protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            DataTable table = ViewState["dt"] as DataTable;
            table.Rows[e.RowIndex].Delete();
            GridView1.EditIndex = -1;

            if (table.Rows.Count < 1)
            {
                btnok.Visible = false;
                btnback.Visible = false;
            }

            GridView1.DataSource = ViewState["dt"] as DataTable;
            GridView1.DataBind();

        }

        protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {

        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowIndex != -1)
            {
                int indexID = this.GridView1.PageIndex * this.GridView1.PageSize + e.Row.RowIndex + 1;
                e.Row.Cells[0].Text = indexID.ToString();
            }



           
        }

        protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
        {

        }

        protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {

        }

        protected void txtempno_TextChanged(object sender, EventArgs e)
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

            DataTable dt = DA.GetRows("select * from dbo.Users where cast(UserEmpNo as int )='" + txtDLempno.Text.Trim() + "' and Enabled='0'  ").Tables[0];

            DataTable table = ViewState["dt"] as DataTable;
            table.Rows.Clear();
            GridView1.DataSource = ViewState["dt"] as DataTable;
            GridView1.DataBind();
            btnok.Visible = false;
            btnback.Visible = false;


            if (dt.Rows.Count > 0)
            {
                txtDLempno.Text = dt.Rows[0]["UserEmpNo"].ToString();
                txtDLempname.Text = dt.Rows[0]["UserName"].ToString();
                txtDLmail.Text = dt.Rows[0]["Email"].ToString();


           

                DataTable dt2 = DA.GetRows("select *  from  dbo.users where  userid='" + userid + "'  ").Tables[0];

                if (dt.Rows.Count > 0)
                {
                    string empno = dt.Rows[0]["UserEmpNo"].ToString();
                    string empno2 = dt2.Rows[0]["UserEmpNo"].ToString();
                   
                    if (empno == empno2)
                    {
                        ScriptManager.RegisterClientScriptBlock(UpdatePanel1, typeof(UpdatePanel), "提示", "alert('代理人不能為當前用戶!');", true);
                        txtDLempno.Text = "";
                        txtDLempname.Text = "";
                        txtDLmail.Text = "";
                      
                        return;
                    }
                    
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(UpdatePanel1, typeof(UpdatePanel), "提示", "alert('無法獲取到當前用戶的域賬號!');", true);
                    txtDLempno.Text = "";
                    txtDLempname.Text = "";
                    txtDLmail.Text = "";
                   
                    return;
                }
            }
            else
            {
                txtDLempno.Text = "";
                txtDLempname.Text = "";
                txtDLmail.Text = "";
               

                //ScriptManager.RegisterStartupScript(UpdatePanel1, this.GetType(), "alert", "alert('工號不存在,請手工輸入姓名與郵箱!'')", true);
                ScriptManager.RegisterClientScriptBlock(UpdatePanel1, typeof(UpdatePanel), "提示", "alert('工號不存在系統內或已離職!');", true);
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


            if (GridView1.Rows.Count == 0)
            {

                ScriptManager.RegisterClientScriptBlock(UpdatePanel1, typeof(UpdatePanel), "提示", "alert('請將填寫的資料Add進表格中!');", true);
                return;
            }
            else
            {
                if (GridView1.Rows.Count > 0)
                {
                  
                    DataTable dt0 = DA.GetRows("select *  from  dbo.users where  userid='" + userid + "' ").Tables[0];

                    string empno = "";
                    string empname = "";
                    string mail = "";
                  
                    if (dt0.Rows.Count > 0)
                    {
                        empno = dt0.Rows[0]["UserEmpNo"].ToString();
                        empname = dt0.Rows[0]["UserName"].ToString();
                        mail = dt0.Rows[0]["Email"].ToString();
                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(UpdatePanel1, typeof(UpdatePanel), "提示", "alert('無法獲取到登錄的賬號！');", true);
                        return;
                    }



                    for (int i = 0; i < this.GridView1.Rows.Count; i++)
                    {
                        if (!string.IsNullOrEmpty(userid))
                        {

                            string dlempno=GridView1.Rows[i].Cells[1].Text;
                            string dlempname=GridView1.Rows[i].Cells[2].Text;
                            string dlmail=GridView1.Rows[i].Cells[3].Text;
                            string date2 = GridView1.Rows[i].Cells[4].Text;
                            string star2 = GridView1.Rows[i].Cells[5].Text;
                            string end2 = GridView1.Rows[i].Cells[6].Text;
                            DataTable dt = DA.GetRows("select *  from  dbo.Agent where     Enabled='0' and  ManagerNo='" + empno + "' and  Dates= '" + date2 + "'  and  (( StarTime<= '" + star2 + "' and '" + star2 + "' <=EndTime)  or  (StarTime<= '" + end2 + "' and '" + end2 + "' <=EndTime)  or ( '" + star2 + "'<=StarTime and StarTime<='" + end2 + "') or ( '" + star2 + "'<=EndTime and EndTime<='" + end2 + "') )").Tables[0];
                            if (dt.Rows.Count > 0)
                            {
                                ScriptManager.RegisterClientScriptBlock(UpdatePanel1, typeof(UpdatePanel), "提示", "alert('該时段已存在代理!');", true);
                                return;
                            }

                          
                        }  
                    }


                    for (int i = 0; i < this.GridView1.Rows.Count; i++)
                    {
                        if (!string.IsNullOrEmpty(userid))
                        {
                            string dlempno = GridView1.Rows[i].Cells[1].Text;
                            string dlempname = GridView1.Rows[i].Cells[2].Text;
                            string dlmail = GridView1.Rows[i].Cells[3].Text;
                            string date2 = GridView1.Rows[i].Cells[4].Text;
                            string star2 = GridView1.Rows[i].Cells[5].Text;
                            string end2 = GridView1.Rows[i].Cells[6].Text;


                            DA.ExecuteReader("insert into   Agent  (ManagerNo,ManagerName,ManagerMail,DLManagerNo,DLManagerName,DLManagerMail,Dates,StarTime,EndTime,UpdateUser,Updatedate,Enabled) values ('" + empno + "',N'" + empname + "','" + mail + "','" + dlempno + "',N'" + dlempname + "','" + dlmail + "','" + date2 + "','" + star2 + "','" + end2 + "','" + userid + "',getdate(),'0') ");
                           
                        }
                    }

                    DA.ExecuteReader("update  a  set  a.RoleID='2' from   UserRole a  inner join   Users  b  on a.UserID=b.UserID  where b.UserEmpNo='" + txtDLempno.Text.Trim() + "' and  a.RoleID not in ('1','2') ");
                    ScriptManager.RegisterClientScriptBlock(UpdatePanel1, typeof(UpdatePanel), "提示", "alert('新增成功!'); location.replace('Agent.aspx');", true);
                }
            }
        }

        protected void btnback_Click(object sender, EventArgs e)
        {

        }

      

        protected void ckbox_CheckedChanged(object sender, EventArgs e)
        {
            txttimestar.Value = "";
            txttimeend.Value = "";
        }
    }
}