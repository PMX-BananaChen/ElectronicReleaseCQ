using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Education2
{
    public partial class ApprovePost : System.Web.UI.Page
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


                DataTable dt = DA.GetRows("select * from UserRole where UserID='" + userid + "'").Tables[0];

                if (dt.Rows.Count > 0)
                {
                    string role = dt.Rows[0]["RoleID"].ToString();
                    BindFactoryData();
                    ddlDept.Items.Insert(0, new ListItem("--請選擇--", ""));
                    //if (role == "1")
                    //{
                    //    imgbtn1.Visible = false;
                    //    imgbtn2.Visible = false;
                    //}

                    
                }


                DataTable dt2 = DA.GetRows("select * from Users where UserID='" + userid + "'").Tables[0];
                if (dt2.Rows.Count > 0)
                {
                   string name = dt2.Rows[0]["UserName"].ToString();
                }

                State();

              
                Bind(txtemp.Text.Trim(), Dll_States.SelectedValue.Trim(), ddlfactory.SelectedValue.Trim(), ddlDept.SelectedValue.Trim(), txtdatestar.Value.Trim(), txtdateend.Value.Trim());
            }
        }

        protected void gridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gdv1.PageIndex = e.NewPageIndex;
            Bind(txtemp.Text.Trim(), Dll_States.SelectedValue.Trim(), ddlfactory.SelectedValue.Trim(), ddlDept.SelectedValue.Trim(), txtdatestar.Value.Trim(), txtdateend.Value.Trim());
        }

        protected void gridView_OnRowCreated(object sender, GridViewRowEventArgs e)
        {

        }

        protected void gridView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (e.Row.Cells[9].Text == "待主管審核")
                    e.Row.Cells[9].ForeColor = System.Drawing.Color.Red;
                else
                    e.Row.Cells[9].ForeColor = System.Drawing.Color.Black;


                e.Row.Cells[10].ToolTip = e.Row.Cells[10].Text;

                if (e.Row.Cells[10].Text.Length > 10)
                {
                    e.Row.Cells[10].Text = e.Row.Cells[10].Text.Substring(0, 10) + "..";
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


                if (Dll_States.SelectedValue == "1")
                {
                    e.Row.Cells[11].Visible = false;
                    gdv1.HeaderRow.Cells[11].Visible = false;
                    e.Row.Cells[12].Visible = false;
                    gdv1.HeaderRow.Cells[12].Visible = false;


                }
                else if (Dll_States.SelectedValue == "2")
                {

                    e.Row.Cells[11].Visible = false;
                    gdv1.HeaderRow.Cells[11].Visible = false;
                    e.Row.Cells[12].Visible = true;
                    gdv1.HeaderRow.Cells[12].Visible = true;
                }
                else
                {
                    e.Row.Cells[11].Visible = true;
                    gdv1.HeaderRow.Cells[11].Visible = true;
                    e.Row.Cells[12].Visible = false;
                    gdv1.HeaderRow.Cells[12].Visible = false;
                }

            }
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

        private void State()
        {
            Dll_States.Items.Clear();
            DataTable dt = DA.GetRows("select * from State where StateID not in ('0')").Tables[0];
            Dll_States.DataSource = dt;
            Dll_States.DataTextField = "StateName";
            Dll_States.DataValueField = "StateID";
            Dll_States.DataBind();
        }
        private void Bind(string emp, string state, string factory,string dept,string star,string end)
        {
            DataSQL DA = new DataSQL();
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

            string strWhere="";
            if (!string.IsNullOrEmpty(emp))
            {
                strWhere = " and (a.EmpNo='" + emp + "' or a.EmpName='" + emp + "')";
            }

            if (!string.IsNullOrEmpty(state))
            {
                strWhere = strWhere + " and a.States='"+state+"'";
            }

            if (!string.IsNullOrEmpty(star) &&  string.IsNullOrEmpty(end))
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



            string role = "";
            DataTable dtrole = DA.GetRows("select * from UserRole where UserID='" + userid + "'").Tables[0];

            if (dtrole.Rows.Count > 0)
            {
                role = dtrole.Rows[0]["RoleID"].ToString();
            }



            //if (role == "1")
            //{

            //    if (!string.IsNullOrEmpty(factory))
            //    {
            //        strWhere = strWhere + " and a.Factory='" + factory + "'";
            //    }

            //    if (!string.IsNullOrEmpty(dept))
            //    {
            //        strWhere = strWhere + " and a.Dept='" + dept + "'";
            //    }
            //}


            //else
            //{
                if (!string.IsNullOrEmpty(factory))
                {
                    strWhere = strWhere + " and a.Factory='" + factory + "'";
                }

                if (!string.IsNullOrEmpty(dept))
                {
                    strWhere = strWhere + " and a.Dept='" + dept + "'";
                }

                //ToString().PadLeft(6, '0');
                DataTable user = DA.GetRows("select * from Users where UserID='" + userid + "'").Tables[0];

                string empno = user.Rows[0]["UserEmpNo"].ToString();
                strWhere = strWhere + " and a.ManagerNo='" + empno.ToString().PadLeft(8, '0') + "'";
            //}


            DataTable dt = DA.GetRows("select a.*,b.StateName,Convert(varchar(10),a.BackDate,120) as BackDate2,Convert(varchar(10),a.AuditDate,120) as AuditDate2,isnull(c.DeptName,a.Dept) as  Depts from  dbo.ReleaseApply  a inner join  [State] b   on a.States=b.StateID left join  dbo.HR_Dept c  on  a.Dept=c.DeptNo where 1=1 " + strWhere + " order by  a.UpdateDate desc ").Tables[0];
            if (Dll_States.SelectedValue == "1" && dt.Rows.Count > 0)
            {
                imgbtn1.Visible = true;
                imgbtn2.Visible = true;
            }
            else
            {
                imgbtn1.Visible = false;
                imgbtn2.Visible = false;
            }
            gdv1.DataSource = dt;
            gdv1.DataBind();
        }
        
        protected void ImageButton2_Click(object sender, ImageClickEventArgs e)
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

            string name = "";
            string loginempid = "";
            DataTable dtname = DA.GetRows("select * from Users where UserID='" + userid + "'").Tables[0];

            if (dtname.Rows.Count > 0)
            {
                name = dtname.Rows[0]["UserName"].ToString();
                loginempid = dtname.Rows[0]["UserEmpno"].ToString();
            }

          

            lbback.Visible = false;
            txtback.Visible = false;
            btnback.Visible = false;

            string mail = "";

            int count = 0;
            DateTime now = DateTime.Now;
            string TimeA = now.ToString("yyyy-MM-dd HH:mm:ss");

            for (int i = 0; i < this.gdv1.Rows.Count; i++)
            {
                string ID = this.gdv1.DataKeys[i][0].ToString();
                CheckBox CKButton = (CheckBox)this.gdv1.Rows[i].Cells[0].FindControl("DeleteThis");
                if (CKButton.Checked)
                {
                   
                    DA.ExecuteReader("update dbo.ReleaseApply set  States='2',AuditDate=getdate(),AuditUser='" + userid + "',AuditUserName=N'"+ name + "',UpdateDate=getdate()  where ID='" + ID + "' ");
                    count = count + 1;

                    string empno0 = "";
                    string createdate = "";

                 
                  
                    
                    DataTable dt=DA.GetRows("select * from   dbo.ReleaseApply a inner join dbo.Users b  on a.CreateUser=b.UserID where a.ID='" + ID + "' ").Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                        mail = mail+dt.Rows[0]["Email"].ToString()+";";
                    }

                }
            }

            if (count == 0)
            {
                //RegisterStartupScript("", "<script>alert('请勾选需要审核的单据!')</script>");
                ScriptManager.RegisterStartupScript(UpdatePanel1, this.GetType(), "alert", "alert('请勾选需要审核的单据!')", true);


                return;
            }
            else
            {
                
                string mail2=Main(mail);

                DA.ExecuteReader("exec Mail '審核完成','" + mail2 + "','','" + TimeA + "',N'" + name + "','' ");

                ScriptManager.RegisterStartupScript(UpdatePanel1, this.GetType(), "alert", "alert('已成功审核 " + count + "條放行單!')", true);

                Bind(txtemp.Text.Trim(), Dll_States.SelectedValue.Trim(), ddlfactory.SelectedValue.Trim(), ddlDept.SelectedValue.Trim(), txtdatestar.Value.Trim(), txtdateend.Value.Trim());
            }

        }



        public void WXmessage(string id, string empno, string content, string link_url, string title, string documentcreatedate, string createuser,string types)
        {
            string url = "http://wx.primax.com.cn/FangXing/Api/sendCardMessage";
            string md5 = "apply_id=" + id + "&content=" + content + "&empno=" + empno + "&link_url=" + link_url + "&title=" + title;
            string aa = md5 + "&appkey=aXkzUlJBelJiU3BTQlpMU3B6MENLZUZhcDQ1TDNseko5SXVHVzI";
            string bb = UserMd5(aa);
            string cc = md5 + "&sign=" + bb;
            string dd = Post(url, cc);

            DA.ExecuteReader("insert into WX_To_Message (Emp_No,DocumentCreateUser,DocumentCreateDate,CreateDate,types) values ('" + empno + "','" + createuser + "','" + documentcreatedate + "',getdate(),'"+types+"' ) ");

        }
        public static string Post(string url, string content)
        {
            //content=HttpUtility.UrlDecode(content, Encoding.UTF8);

            string result = "";
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
            req.Method = "POST";
            req.ContentType = "application/x-www-form-urlencoded;charset=utf-8";

            #region 添加Post 参数
            byte[] data = Encoding.UTF8.GetBytes(content);
            req.ContentLength = data.Length;
            using (Stream reqStream = req.GetRequestStream())
            {
                reqStream.Write(data, 0, data.Length);
                reqStream.Close();
            }
            #endregion


            HttpWebResponse resp = (HttpWebResponse)req.GetResponse();
            Stream stream = resp.GetResponseStream();
            //获取响应内容
            using (StreamReader reader = new StreamReader(stream, Encoding.UTF8))
            {
                result = reader.ReadToEnd();
            }
            return result;
        }

        public static string UserMd5(string str)
        {
            string cl = str;
            string pwd = "";
            MD5 md5 = MD5.Create();//实例化一个md5对像
            // 加密后是一个字节类型的数组，这里要注意编码UTF8/Unicode等的选择　
            byte[] s = md5.ComputeHash(Encoding.UTF8.GetBytes(cl));
            // 通过使用循环，将字节类型的数组转换为字符串，此字符串是常规字符格式化所得
            for (int i = 0; i < s.Length; i++)
            {
                // 将得到的字符串使用十六进制类型格式。格式后的字符是小写的字母，如果使用大写（X）则格式后的字符是大写字符 
                pwd = pwd + s[i].ToString("x2");

            }
            return pwd;
        }

        protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
        {
            lbback.Visible = false;
            txtback.Visible = false;
            btnback.Visible = false;


            
            Bind(txtemp.Text.Trim(), Dll_States.SelectedValue.Trim(), ddlfactory.SelectedValue.Trim(), ddlDept.SelectedValue.Trim(), txtdatestar.Value.Trim(), txtdateend.Value.Trim());
        }

        protected void ImageButton4_Click(object sender, ImageClickEventArgs e)
        {
            //string EmpNOstr = "";

            //for (int i = 0; i < this.gdv1.Rows.Count; i++)
            //{
            //    string id = this.gdv1.DataKeys[i][0].ToString();
            //    string PostId = this.gdv1.DataKeys[i][1].ToString();
            //    CheckBox CKButton = (CheckBox)this.gdv1.Rows[i].Cells[0].FindControl("checks");
            //    if (CKButton.Checked)
            //    {
            //        EmpNOstr += this.gdv1.Rows[i].Cells[1].Text.ToString() + "*" + this.gridView.Rows[i].Cells[2].Text.ToString() + "*" + this.gridView.Rows[i].Cells[7].Text.ToString() + "*" + id + "*" + PostId + ",";
            //    }

            //}

            //this.Response.Redirect("~/Bills/ApprovePostDetail.aspx?EmpNoStr=" + EmpNOstr);         
        }

      

        protected void Unnamed1_Click(object sender, ImageClickEventArgs e)
        {
           
            int count = 0;
            for (int i = 0; i < this.gdv1.Rows.Count; i++)
            {
                string ID = this.gdv1.DataKeys[i][0].ToString();
                CheckBox CKButton = (CheckBox)this.gdv1.Rows[i].Cells[0].FindControl("DeleteThis");
                if (CKButton.Checked)
                {
                    lbback.Visible = true;
                    txtback.Visible = true;
                    btnback.Visible = true;
                    count = count + 1;
                  
                }
            }

            if (count == 0)
            {
                //RegisterStartupScript("", "<script>alert('请勾选需要审核的单据!')</script>");
                ScriptManager.RegisterStartupScript(UpdatePanel1, this.GetType(), "alert", "alert('请勾选需要审核的单据!')", true);
                return;
            }
           


        }


        protected string Main(string str)
        {
        
            string[] arr = str.Split(";".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
            StringBuilder sb = new StringBuilder();
            Hashtable ht = new Hashtable();
            foreach (string s in arr)
            {
                if (!ht.ContainsKey(s))
                {
                    sb.AppendFormat("{0};", s); ht.Add(s, string.Empty);
                }
            }
            string a = sb.ToString();
            return a;

        }

        protected void btnback_Click(object sender, EventArgs e)
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

            string name = "";
            string loginempid = "";
            DataTable dtname = DA.GetRows("select * from Users where UserID='" + userid + "'").Tables[0];

            if (dtname.Rows.Count > 0)
            {

                name = dtname.Rows[0]["UserName"].ToString();
                loginempid = dtname.Rows[0]["UserEmpNo"].ToString();
            }

            if (txtback.Text.Trim() == "")
            {
                ScriptManager.RegisterStartupScript(UpdatePanel1, this.GetType(), "alert", "alert('退回意見不能為空!')", true);
                return;
            }
            string mail3 = "";
            int count = 0;
            DateTime now = DateTime.Now;
            string TimeA = now.ToString("yyyy-MM-dd HH:mm:ss");

            for (int i = 0; i < this.gdv1.Rows.Count; i++)
            {
                string ID = this.gdv1.DataKeys[i][0].ToString();
                CheckBox CKButton = (CheckBox)this.gdv1.Rows[i].Cells[0].FindControl("DeleteThis");
                if (CKButton.Checked)
                {

                    lbback.Visible = false;
                    txtback.Visible = false;
                    btnback.Visible = false;

                    DA.ExecuteReader("update dbo.ReleaseApply set  States='3',BackUser='"+userid+"',BackUserName=N'"+name+"',BackDate=getdate(),BackRemark=N'" + txtback.Text.Trim() + "',UpdateDate=getdate() where ID='" + ID + "' ");

                    count = count + 1;


                    string empno0 = "";
                    string createdate = "";

                   


                    DataTable dt = DA.GetRows("select * from   dbo.ReleaseApply a inner join dbo.Users b  on a.CreateUser=b.UserID where a.ID='" + ID + "' ").Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                        mail3 = mail3 + dt.Rows[0]["Email"].ToString() + ";";
                    }
 
                }

              


            }
            
            if (count == 0)
            {
                //RegisterStartupScript("", "<script>alert('请勾选需要审核的单据!')</script>");
                ScriptManager.RegisterStartupScript(UpdatePanel1, this.GetType(), "alert", "alert('请勾选需要审核的单据!')", true);
                return;
            }
            else
            {
                
                
                string mail4 = Main(mail3);

                DA.ExecuteReader("exec Mail '退單','" + mail4 + "','','" + TimeA + "',N'" + name + "',N'" + txtback.Text.Trim()+ "' ");
                ScriptManager.RegisterStartupScript(UpdatePanel1, this.GetType(), "alert", "alert('已退回 " + count + "條放行單!')", true);
                Bind(txtemp.Text.Trim(), Dll_States.SelectedValue.Trim(), ddlfactory.SelectedValue.Trim(), ddlDept.SelectedValue.Trim(), txtdatestar.Value.Trim(), txtdateend.Value.Trim());

            }


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