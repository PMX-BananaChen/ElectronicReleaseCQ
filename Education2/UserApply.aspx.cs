using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
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
    public partial class UserApply : System.Web.UI.Page
    {

        DataSQL DA = new DataSQL();
        protected void Page_Load(object sender, EventArgs e)
        {
            this.Page.Form.DefaultButton = btnadd.ClientID.Replace('_', '$'); //设置默认按钮

            if (!Page.IsPostBack)
            {
                
                DataTable dt = new DataTable();
                DataColumn dc = null;
                dc = dt.Columns.Add("ID", Type.GetType("System.Int32"));
                dc.AutoIncrement = true;//自动增加
                dc.AutoIncrementSeed = 1;//起始为1
                dc.AutoIncrementStep = 1;//步长为1
                dc.AllowDBNull = false;//
                dt.Columns.Add("Date", Type.GetType("System.String"));
                dt.Columns.Add("Star", Type.GetType("System.String"));
                dt.Columns.Add("End", Type.GetType("System.String"));
                ViewState["dt"] = dt;
                BindFactoryData();
                ddlDept.Items.Insert(0, new ListItem("--請選擇--", ""));

                string userid="";
                if (Session["language"] == null)
                {
                    Response.Redirect("Error3.aspx");
                    return;
                }
                else
                {
                    userid = Session["language"].ToString();
                }



                //userid = Login.userid;
                //if (userid == null)
                //{
                //    Response.Redirect("Error3.aspx");
                //    return;
                //}
                DataTable dt2 = DA.GetRows("select * from dbo.Users where userid='" +userid + "'  ").Tables[0];
                if (dt2.Rows.Count > 0)
                {
                    txtmail.Text = dt2.Rows[0]["Email"].ToString();
                }

            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {

            string userid = null;
            string createempno = null;

            if (Session["language"] == null)
            {
                Response.Redirect("Error3.aspx");
                return;
            }
            else
            {
                userid = Session["language"].ToString();
                DataTable create = DA.GetRows("select * from  users where Userid='" + userid + "' ").Tables[0];
                if (create.Rows.Count > 0)
                {
                    createempno = create.Rows[0]["userEmpno"].ToString();
                }
           
            }

            
            lb1.Text = "";
             if (GridView1.Rows.Count == 0)
            {

                ScriptManager.RegisterClientScriptBlock(UpdatePanel1, typeof(UpdatePanel), "提示", "alert('請將填寫的資料Add進表格中!');", true);
                return;
            }
             else if (txtempno.Text.Trim() == "" || txtempname.Text.Trim() == "" || ddlFactory.SelectedValue.Trim() == "" || ddlDept.SelectedValue.Trim() == "" || txtmail.Text.Trim() == "" || txtdatestar.Value.Trim() == "" || txtdateend.Value.Trim() == "" || txttimestar.Value.Trim() == "" || txttimeend.Value.Trim() == ""||txtremark.Text.Trim()=="")
            {

                //ScriptManager.RegisterStartupScript(UpdatePanel1, this.GetType(), "alert", "alert('資料不能為空!'')", true);

                ScriptManager.RegisterClientScriptBlock(UpdatePanel1, typeof(UpdatePanel), "提示", "alert('資料不能為空!');", true);

                return;
            }
            else
            {
              
                int count = 0;

                string manager="";
                DataTable area = DA.GetRows("select * from HR_Employee where isnull(Area,'')='TYM' AND emp_no='" + txtempno.Text.Trim() + "'").Tables[0];
                if (area.Rows.Count > 0)
                {
                     manager = GetManagerTYM(txtempno.Text.Trim(), ddlDept.SelectedValue.Trim());
                }
                else
                {
                     manager = GetManager(txtempno.Text.Trim(), ddlDept.SelectedValue.Trim());
                }



                //s manager = GetManager(txtempno.Text.Trim(),ddlDept.SelectedValue.Trim());
                DateTime now = DateTime.Now;
                string TimeA = now.ToString("yyyy-MM-dd HH:mm:ss");
             
                string TimeB = now.ToString("yyyy-MM-dd");
                string TimeC = now.ToString("HH:mm");
                string empno = txtempno.Text.Trim();
                string empname = txtempname.Text.Trim();






                //個人代簽
                DataTable dqperson = DA.GetRows("select * from  dbo.Allograph_Person  where  cast(emp_no as int)='" + empno.TrimStart('0') + "'").Tables[0];
                if (dqperson.Rows.Count > 0)
                {
                    manager = dqperson.Rows[0]["ManagerNo"].ToString();
                }


                if (manager == "01000001" || manager == "01004421")
                {
                    ScriptManager.RegisterClientScriptBlock(UpdatePanel1, typeof(UpdatePanel), "提示", "alert('未抓取到部门主管,请联系HR!');", true);
                    return;
                }


                 //代理
                DataTable dtagent = DA.GetRows("select * from  dbo.Agent  where ManagerNo='" + manager + "' and   Enabled='0' and     (StarTime<='" + TimeA + "' and [EndTime]>='" + TimeA + "' )  ").Tables[0];
                if (dtagent.Rows.Count > 0)
                {
                    manager = dtagent.Rows[0]["DLManagerNo"].ToString();
                }

                if (manager == "" || manager == "00000000")
                {
               
                    ScriptManager.RegisterClientScriptBlock(UpdatePanel1, typeof(UpdatePanel), "提示", "alert('未抓取到部门主管,请联系HR!');", true);
                    return;
                }
                else if (GridView1.Rows.Count > 0)
                {
                    TimeA = now.ToString("yyyy-MM-dd HH:mm:ss");
                    string apptype = "";
                    DateTime dt1 = DateTime.Parse(now.ToString("yyyy-MM-dd"));
                    //疫情期間只能申請當天至第三個工作日
                    for (int i = 0; i < this.GridView1.Rows.Count; i++)
                    {
                        string message = DA.checkDate(GridView1.Rows[i].Cells[0].Text);
                        if (!string.IsNullOrEmpty(message))
                        {
                            ScriptManager.RegisterClientScriptBlock(UpdatePanel1, typeof(UpdatePanel), "提示", "alert('" + message + "');", true);
                            return;
                        }
                    }

                    for (int i = 0; i < this.GridView1.Rows.Count; i++)
                    {
                        DateTime dt2 = DateTime.Parse(now.ToString(GridView1.Rows[i].Cells[0].Text));
                        if (dt2 < dt1)
                        {
                            apptype = "補單";
                        }
                        else
                        {
                            apptype = "正常申請";
                        }
                        DA.ExecuteReader("insert into ReleaseApply (EmpNo,EmpName,Factory,Dept,Mail,Date,StarTime,EndTime,States,Remark,Type,CreateUser,CreateUserEmpNo,CreateDate,ManagerNo,UpdateDate) values ('" + txtempno.Text.Trim() + "',N'" + txtempname.Text.Trim() + "','" + ddlFactory.SelectedValue.Trim() + "','" + ddlDept.SelectedValue.Trim() + "','" + txtmail.Text.Trim() + "','" + GridView1.Rows[i].Cells[0].Text + "','" + GridView1.Rows[i].Cells[1].Text + "','" + GridView1.Rows[i].Cells[2].Text + "',1,N'" + txtremark.Text.Trim() + "','" + apptype + "','" + userid + "','" + createempno + "','" + TimeA + "','" + manager + "',getdate())  ");
                        count = i + 1;

                        //增加发给企业微信
                        DataTable newReleasea = DA.GetRows("select id,ManagerNo,EmpName from dbo.ReleaseApply where States=1 and EmpNo='" + txtempno.Text.Trim() + "' and Date='" + GridView1.Rows[i].Cells[0].Text + "' and StarTime='" + GridView1.Rows[i].Cells[1].Text + "' ").Tables[0];
                        string wxTitle = "待审核通知";
                        string wxMsg = "";
                        string wxSendType = "newRelease";
                        string wxRemark = "";
                        string id = "";
                        int ManagerNo = 0;
                        string EmpName = "";
                        wxMsg = "您有一條新的申請待簽核！\n";
                        if (newReleasea.Rows.Count > 0)
                        {
                            id = newReleasea.Rows[0]["id"].ToString();
                            ManagerNo = int.Parse(newReleasea.Rows[0]["ManagerNo"].ToString().ToString());
                            EmpName = newReleasea.Rows[0]["EmpName"].ToString();

                            String wxUrl = "http://wx2.primax.com.cn/newRelease/actioncheck?id=" + id;
                            if (!string.IsNullOrWhiteSpace(id))
                            {
                                wxMsg += "申請單號:" + id + "\n";
                            }
                            if (!string.IsNullOrWhiteSpace(EmpName))
                            {
                                wxMsg += "申請員工:" + EmpName + "\n";
                            }
                            wxMsg += "操作時間:" + DateTime.Now + "\n";
                            DA.ExecuteReader("insert into [10.40.50.107].[ReportCenter].[dbo].[WX_Message](EmpNo,Title,Msg,Url,SendType,Remark) values ('" + ManagerNo + "',N'" + wxTitle + "',N'" + wxMsg + "',N'" + wxUrl + "',N'" + wxSendType + "',N'" + wxRemark + "')");
                        }
                    
                    }
                }

                string loginempid = ""; string loginname = ""; string loginmail = "";

                //string linkurl = "http://wx.primax.com.cn/FangXing/Order/checkApply.html";
                DataTable login = DA.GetRows("select * from  dbo.Users where UserID='" + userid + "' ").Tables[0];
                if (login.Rows.Count > 0)
                {
                    loginempid = login.Rows[0]["userEmpNo"].ToString();
                    loginname = login.Rows[0]["UserName"].ToString();
                    loginmail = login.Rows[0]["Email"].ToString();
                }

                DA.ExecuteReader("exec MailManager '" + manager + "','" + userid + "','" + count + "','" + TimeA + "','" + txtempno.Text.Trim() + "','" + loginmail + "' ");

                txtempno.Text = "";
                txtempname.Text = "";
                ddlFactory.SelectedValue = "";
                ddlDept.SelectedValue = "";

                txtremark.Text = "";
                txtdatestar.Value = "";
                txtdateend.Value = "";
                txttimestar.Value = "";
                txttimeend.Value = "";

                DataTable table = ViewState["dt"] as DataTable;
                table.Rows.Clear();
                btnok.Visible = false;
                btnback.Visible = false;
                GridView1.DataSource = ViewState["dt"] as DataTable;
                GridView1.DataBind();


                string tzname = "";
                DataTable tz = DA.GetRows("select * from  dbo.Users  where UserEmpNo='" + manager + "'  ").Tables[0];
                if (tz.Rows.Count > 0)
                {
                    tzname = tz.Rows[0]["UserName"].ToString();
                }

                lb1.Text = "申請成功待"+tzname+"簽核";


                //ScriptManager.RegisterStartupScript(UpdatePanel1, this.GetType(), "alert", "alert('成功申請 " + count + "條放行單,待主管簽核!')", true);
                ScriptManager.RegisterClientScriptBlock(UpdatePanel1, typeof(UpdatePanel), "提示", "alert('成功申請 " + count + "條放行單,待" + tzname + "簽核!');", true);
            } 
        }

        protected void btnadd_Click(object sender, EventArgs e)
        {
            if (txtdatestar.Value.Trim() == "" || txtdateend.Value.Trim() == "" || txttimestar.Value.Trim() == "" || txttimeend.Value.Trim() == "")
            {
                
                
                ScriptManager.RegisterClientScriptBlock(UpdatePanel1, typeof(UpdatePanel), "提示", "alert('資料不能為空');", true);

            }
            else
            {

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

                if (c2 >= d)
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

                DateTime now2 = Convert.ToDateTime(DateTime.Now.AddDays(-2).ToString("yyyy-MM-dd"));

                if (star <= now2 || end <= now2)
                {
                    ScriptManager.RegisterClientScriptBlock(UpdatePanel1, typeof(UpdatePanel), "提示", "alert('開始與結束日期不能小於昨日!');", true);
                    return;
                }

                DataTable table = ViewState["dt"] as DataTable;
                if (table.Rows.Count >= 10)
                {
                    //ScriptManager.RegisterStartupScript(UpdatePanel1, this.GetType(), "alert", "alert('每次申请不能超过10条!'')", true);
                    ScriptManager.RegisterClientScriptBlock(UpdatePanel1, typeof(UpdatePanel), "提示", "alert('每次申请不能超过10条!');", true);


                }

                else if (txtdatestar.Value.Trim() == txtdateend.Value.Trim())
                {
                    DataRow newRow;
                    newRow = (ViewState["dt"] as DataTable).NewRow();
                    newRow["Date"] = star.ToString("yyyy-MM-dd");
                    newRow["Star"] = star2.ToString("HH:mm");
                    newRow["End"] = end2.ToString("HH:mm");
                    (ViewState["dt"] as DataTable).Rows.Add(newRow);
                    GridView1.DataSource = ViewState["dt"] as DataTable;
                    GridView1.DataBind();
                    btnok.Visible = true;
                    btnback.Visible = true;
                }
                else
                {
                    TimeSpan c = end - star;
                    int count = Convert.ToInt32(c.Days.ToString()) + 1;
                    int tbcount = table.Rows.Count;
                    if ((tbcount + count) > 10)
                    {
                        ScriptManager.RegisterClientScriptBlock(UpdatePanel1, typeof(UpdatePanel), "提示", "alert('每次申请不能超过10条!');", true);
                        return;
                    }
                    for (DateTime dt = star; dt <= end; dt = dt.AddDays(1))
                    {
                        DataRow newRow;
                        newRow = (ViewState["dt"] as DataTable).NewRow();
                        newRow["Date"] = dt.ToString("yyyy-MM-dd");
                        newRow["Star"] = star2.ToString("HH:mm");
                        newRow["End"] = end2.ToString("HH:mm");
                        (ViewState["dt"] as DataTable).Rows.Add(newRow); 
                    }
                    GridView1.DataSource = ViewState["dt"] as DataTable;
                    GridView1.DataBind();
                    btnok.Visible = true;
                    btnback.Visible = true;
                }
            }    
        }


        public void WXmessage(string id, string empno, string content, string link_url, string title,string createuser)
        {

            string url = "http://wx.primax.com.cn/FangXing/Api/sendCardMessage";

            string md5 = "apply_id=" + id + "&content=" + content + "&empno="+empno+  "&link_url=" + link_url + "&title=" + title ;
            string aa = md5 + "&appkey=aXkzUlJBelJiU3BTQlpMU3B6MENLZUZhcDQ1TDNseko5SXVHVzI";

            string bb = UserMd5(aa);
            string cc = md5 + "&sign=" + bb;
            string dd=Post(url, cc);


            DA.ExecuteReader("insert into WX_To_Message (Emp_No,DocumentCreateUser,DocumentCreateDate,CreateDate,types) values ('" + empno + "','" + createuser + "',getdate(),getdate(),'0')  ");
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


        public string GetManager(string empno, string dept)
        {
            string manager = "00000000";
            if (!string.IsNullOrEmpty(dept))
            {
                DataTable dt0 = DA.GetRows("select * from  dbo.Allograph  where Dept_No='" + dept + "' ").Tables[0];
                DataTable dt = DA.GetRows("select * from  dbo.HRDepartment  where DepartmentCode='" + dept + "' ").Tables[0];
                DataTable dttitle = DA.GetRows("select * from  dbo.HR_Employee  where ISNULL(Emp_No,'')='" + empno + "' and  Emp_Title='W0' ").Tables[0];
                //如果存在部門代簽,直接選擇主管(W0職等除外)
                if (dt0.Rows.Count > 0 && dttitle.Rows.Count < 1)
                {
                    manager = dt0.Rows[0]["ManagerNo"].ToString();
                }
                else if (dt.Rows.Count > 0)
                {

                    manager = dt.Rows[0]["MasterCode"].ToString();
                    string deptno = dt.Rows[0]["DepartmentCode"].ToString();
                    string parent = dt.Rows[0]["ParentCode"].ToString();
                    string managername = dt.Rows[0]["MasterName"].ToString();

                    string managerp0 = "";
                    string p0 = "";

                    //主管職等
                    DataTable dt01 = DA.GetRows("select * from  dbo.HRUser  where UserCode='" + manager + "' ").Tables[0];
                    //申請人職等
                    DataTable dt02 = DA.GetRows("select * from  dbo.HR_Employee  where Emp_No='" + empno + "' ").Tables[0];


                    if (dt01.Rows.Count > 0)
                    {
                        managerp0 = dt01.Rows[0]["TitleCode"].ToString();
                    }

                    if (dt02.Rows.Count > 0)
                    {
                        p0 = dt02.Rows[0]["Emp_Title"].ToString();
                    }


                    #region
                    if (manager == "00000000")
                    {
                        while (manager == "00000000")
                        {
                            DataTable dt2 = DA.GetRows("select * from  dbo.HRDepartment  where DepartmentCode='" + parent + "' ").Tables[0];
                            if (dt2.Rows.Count > 0)
                            {
                                manager = dt2.Rows[0]["MasterCode"].ToString();
                                managername = dt2.Rows[0]["MasterName"].ToString();

                                //如果部門主管為空繼續找到上級部門繼續循環

                                if (manager == "00000000")
                                {
                                    parent = dt2.Rows[0]["ParentCode"].ToString();
                                }
                                else
                                {
                                    string managerp = "";
                                    string p = "";
                                    //主管職等
                                    DataTable dt3 = DA.GetRows("select * from  dbo.HRUser  where UserCode='" + manager + "' ").Tables[0];
                                    //申請人職等
                                    DataTable dt4 = DA.GetRows("select * from  dbo.HR_Employee  where Emp_No='" + empno + "' ").Tables[0];

                                    if (dt3.Rows.Count > 0)
                                    {
                                        managerp = dt3.Rows[0]["TitleCode"].ToString();
                                    }
                                    else
                                    {
                                        manager = "00000000";
                                        break;
                                    }

                                    if (dt4.Rows.Count > 0)
                                    {
                                        p = dt4.Rows[0]["Emp_Title"].ToString();
                                    }
                                    //如果申請人跟部門主管是同一個人

                                    if (manager.TrimStart('0') == empno.TrimStart('0'))
                                    {
                                        parent = dt2.Rows[0]["ParentCode"].ToString();
                                        manager = "00000000";
                                    }

                                    //如果不是作業員且主管為理級以下      直接人員課級起， 間接人員理級起
                                    else if (managerp == "E" && p != "W0")
                                    {
                                        parent = dt2.Rows[0]["ParentCode"].ToString();
                                        manager = "00000000";
                                    }
                                }
                            }
                            else
                            {
                                break;
                            }
                        }
                    }
                    #endregion

                    #region //如果當前申請人部門主管就是申請人則找到上一級部門主管
                    //如果當前申請人部門主管就是申請人則找到上一級部門主管  
                    else if (manager.TrimStart('0') == empno.TrimStart('0'))
                    {
                        manager = "00000000";
                        while (manager == "00000000")
                        {
                            DataTable dt2 = DA.GetRows("select * from  dbo.HRDepartment  where DepartmentCode='" + parent + "' ").Tables[0];
                            if (dt2.Rows.Count > 0)
                            {
                                manager = dt2.Rows[0]["MasterCode"].ToString();
                                managername = dt2.Rows[0]["MasterName"].ToString();

                                //如果部門主管為空繼續找到上級部門繼續循環

                                if (manager == "00000000")
                                {
                                    parent = dt2.Rows[0]["ParentCode"].ToString();
                                }
                                else
                                {
                                    //主管職等
                                    DataTable dt3 = DA.GetRows("select * from  dbo.HRUser  where UserCode='" + manager + "' ").Tables[0];
                                    //申請人職等
                                    DataTable dt4 = DA.GetRows("select * from  dbo.HR_Employee  where Emp_No='" + empno + "' ").Tables[0];

                                    string managerp = "";
                                    string p = "";
                                    if (dt3.Rows.Count > 0)
                                    {
                                        managerp = dt3.Rows[0]["TitleCode"].ToString();
                                    }
                                    else
                                    {
                                        manager = "00000000";
                                        break;
                                    }

                                    if (dt4.Rows.Count > 0)
                                    {
                                        p = dt4.Rows[0]["Emp_Title"].ToString();
                                    }

                                    if (manager.TrimStart('0') == empno.TrimStart('0'))
                                    {
                                        parent = dt2.Rows[0]["ParentCode"].ToString();
                                        manager = "00000000";
                                    }
                                    //如果不是作業員且主管為理級以下      直接人員課級起， 間接人員理級起
                                    else if (managerp == "E" && p != "W0")
                                    {
                                        parent = dt2.Rows[0]["ParentCode"].ToString();
                                        manager = "00000000";
                                    }
                                }
                            }
                            else
                            {

                                break;
                            }
                        }

                    }
                    #endregion
                    #region  不是WO職等為課長或P主管時
                    else if (managerp0 == "E" && p0 != "W0")
                    {
                        manager = "00000000";
                        while (manager == "00000000")
                        {

                            DataTable dt2 = DA.GetRows("select * from  dbo.HRDepartment  where DepartmentCode='" + parent + "' ").Tables[0];
                            if (dt2.Rows.Count > 0)
                            {
                                manager = dt2.Rows[0]["MasterCode"].ToString();
                                managername = dt2.Rows[0]["MasterName"].ToString();

                                //如果部門主管為空繼續找到上級部門繼續循環

                                if (manager == "00000000")
                                {
                                    parent = dt2.Rows[0]["ParentCode"].ToString();
                                }
                                else
                                {
                                    //主管職等
                                    DataTable dt3 = DA.GetRows("select * from  dbo.HRUser  where UserCode='" + manager + "' ").Tables[0];
                                    //申請人職等
                                    DataTable dt4 = DA.GetRows("select * from  dbo.HR_Employee  where Emp_No='" + empno + "' ").Tables[0];

                                    string managerp = "";
                                    string p = "";
                                    if (dt3.Rows.Count > 0)
                                    {
                                        managerp = dt3.Rows[0]["TitleCode"].ToString();
                                    }
                                    else
                                    {
                                        manager = "00000000";
                                        break;
                                    }



                                    if (dt4.Rows.Count > 0)
                                    {
                                        p = dt4.Rows[0]["Emp_Title"].ToString();
                                    }

                                    if (manager.TrimStart('0') == empno.TrimStart('0'))
                                    {
                                        parent = dt2.Rows[0]["ParentCode"].ToString();
                                        manager = "00000000";
                                    }
                                    //如果不是作業員且主管為理級以下      直接人員課級起， 間接人員理級起
                                    else if (managerp == "E" && p != "W0")
                                    {
                                        parent = dt2.Rows[0]["ParentCode"].ToString();
                                        manager = "00000000";
                                    }

                                }

                            }
                            else
                            {

                                break;
                            }
                        }
                    }

                    #endregion
                    //2017-07-31  新增  如果主管工號存在但無法抓取到職等時返回空
                    else if (manager != "00000000" && managerp0 == "")
                    {
                        manager = "00000000";
                    }





                    #region  如果存在代簽是W0員工找不到主管的人員
                    if (dt0.Rows.Count > 0 && dttitle.Rows.Count > 0 && (manager == "00000000" || manager == ""))
                    {
                        manager = dt0.Rows[0]["ManagerNo"].ToString();
                    }
                    // 無理級主管部門 WO員工 如果找不到課長就給代簽
                    else if (dt0.Rows.Count > 0 && dttitle.Rows.Count > 0)
                    {
                        DataTable dt5 = DA.GetRows("select * from  dbo.HRUser  where userCode='" + manager + "'  and TitleCode='E' and Title='課長' ").Tables[0];
                        if (dt5.Rows.Count > 0)
                        {
                            manager = dt5.Rows[0]["userCode"].ToString();
                        }
                        else
                        {
                            manager = dt0.Rows[0]["ManagerNo"].ToString();
                        }

                    }


                    #endregion

                }
            }
            return manager;
        }


        public string GetManagerTYM(string empno, string dept)
        {
            string manager = "00000000";
            if (!string.IsNullOrEmpty(dept))
            {
                DataTable dt0 = DA.GetRows("select * from  dbo.Allograph  where Dept_No='" + dept + "' ").Tables[0];
                DataTable dt = DA.GetRows("select * from  dbo.HRDepartment  where DepartmentCode='" + dept + "' ").Tables[0];
                DataTable dttitle = DA.GetRows("select * from  dbo.HR_Employee  where ISNULL(Emp_No,'')='" + empno + "' and  Emp_Title in ('W1','W2','W3','W4','P1-1','P1-2') ").Tables[0];
                //如果存在部門代簽,直接選擇主管(W0職等除外)
                if (dt0.Rows.Count > 0 && dttitle.Rows.Count < 1)
                {
                    manager = dt0.Rows[0]["ManagerNo"].ToString();
                }
                else if (dt.Rows.Count > 0)
                {

                    manager = dt.Rows[0]["MasterCode"].ToString();
                    string deptno = dt.Rows[0]["DepartmentCode"].ToString();
                    string parent = dt.Rows[0]["ParentCode"].ToString();
                    string managername = dt.Rows[0]["MasterName"].ToString();

                    string managerp0 = "";
                    string p0 = "";

                    //主管職等
                    DataTable dt01 = DA.GetRows("select * from  dbo.HRUser  where UserCode='" + manager + "' ").Tables[0];
                    //申請人職等
                    DataTable dt02 = DA.GetRows("select * from  dbo.HR_Employee  where Emp_No='" + empno + "' ").Tables[0];


                    if (dt01.Rows.Count > 0)
                    {
                        managerp0 = dt01.Rows[0]["TitleCode"].ToString();
                    }

                    if (dt02.Rows.Count > 0)
                    {
                        p0 = dt02.Rows[0]["Emp_Title"].ToString();
                    }


                    #region
                    if (manager == "00000000")
                    {
                        while (manager == "00000000")
                        {
                            DataTable dt2 = DA.GetRows("select * from  dbo.HRDepartment  where DepartmentCode='" + parent + "' ").Tables[0];
                            if (dt2.Rows.Count > 0)
                            {
                                manager = dt2.Rows[0]["MasterCode"].ToString();
                                managername = dt2.Rows[0]["MasterName"].ToString();

                                //如果部門主管為空繼續找到上級部門繼續循環

                                if (manager == "00000000")
                                {
                                    parent = dt2.Rows[0]["ParentCode"].ToString();
                                }
                                else
                                {
                                    string managerp = "";
                                    string p = "";
                                    //主管職等
                                    DataTable dt3 = DA.GetRows("select * from  dbo.HRUser  where UserCode='" + manager + "' ").Tables[0];
                                    //申請人職等
                                    DataTable dt4 = DA.GetRows("select * from  dbo.HR_Employee  where Emp_No='" + empno + "' ").Tables[0];

                                    if (dt3.Rows.Count > 0)
                                    {
                                        managerp = dt3.Rows[0]["TitleCode"].ToString();
                                    }
                                    else
                                    {
                                        manager = "00000000";
                                        break;
                                    }

                                    if (dt4.Rows.Count > 0)
                                    {
                                        p = dt4.Rows[0]["Emp_Title"].ToString();
                                    }
                                    //如果申請人跟部門主管是同一個人

                                    if (manager.TrimStart('0') == empno.TrimStart('0'))
                                    {
                                        parent = dt2.Rows[0]["ParentCode"].ToString();
                                        manager = "00000000";
                                    }

                                    //如果不是作業員且主管為理級以下      直接人員課級起， 間接人員理級起
                                    else if (managerp == "E" && p != "W1" && p != "W2" && p != "W3" && p != "W4" && p != "P1-1" && p != "P1-2")
                                    {
                                        parent = dt2.Rows[0]["ParentCode"].ToString();
                                        manager = "00000000";
                                    }
                                }
                            }
                            else
                            {
                                break;
                            }
                        }
                    }
                    #endregion

                    #region //如果當前申請人部門主管就是申請人則找到上一級部門主管
                    //如果當前申請人部門主管就是申請人則找到上一級部門主管  
                    else if (manager.TrimStart('0') == empno.TrimStart('0'))
                    {
                        manager = "00000000";
                        while (manager == "00000000")
                        {
                            DataTable dt2 = DA.GetRows("select * from  dbo.HRDepartment  where DepartmentCode='" + parent + "' ").Tables[0];
                            if (dt2.Rows.Count > 0)
                            {
                                manager = dt2.Rows[0]["MasterCode"].ToString();
                                managername = dt2.Rows[0]["MasterName"].ToString();

                                //如果部門主管為空繼續找到上級部門繼續循環

                                if (manager == "00000000")
                                {
                                    parent = dt2.Rows[0]["ParentCode"].ToString();
                                }
                                else
                                {
                                    //主管職等
                                    DataTable dt3 = DA.GetRows("select * from  dbo.HRUser  where UserCode='" + manager + "' ").Tables[0];
                                    //申請人職等
                                    DataTable dt4 = DA.GetRows("select * from  dbo.HR_Employee  where Emp_No='" + empno + "' ").Tables[0];

                                    string managerp = "";
                                    string p = "";
                                    if (dt3.Rows.Count > 0)
                                    {
                                        managerp = dt3.Rows[0]["TitleCode"].ToString();
                                    }
                                    else
                                    {
                                        manager = "00000000";
                                        break;
                                    }

                                    if (dt4.Rows.Count > 0)
                                    {
                                        p = dt4.Rows[0]["Emp_Title"].ToString();
                                    }

                                    if (manager.TrimStart('0') == empno.TrimStart('0'))
                                    {
                                        parent = dt2.Rows[0]["ParentCode"].ToString();
                                        manager = "00000000";
                                    }
                                    //如果不是作業員且主管為理級以下      直接人員課級起， 間接人員理級起
                                    else if (managerp == "E" && p != "W1" && p != "W2" && p != "W3" && p != "W4" && p != "P1-1" && p != "P1-2")
                                    {
                                        parent = dt2.Rows[0]["ParentCode"].ToString();
                                        manager = "00000000";
                                    }
                                }
                            }
                            else
                            {

                                break;
                            }
                        }

                    }
                    #endregion
                    #region  不是WO職等為課長或P主管時
                    else if (managerp0 == "E" && p0 != "W1" && p0 != "W2" && p0 != "W3" && p0 != "W4" && p0 != "P1-1" && p0 != "P1-2")
                    {
                        manager = "00000000";
                        while (manager == "00000000")
                        {

                            DataTable dt2 = DA.GetRows("select * from  dbo.HRDepartment  where DepartmentCode='" + parent + "' ").Tables[0];
                            if (dt2.Rows.Count > 0)
                            {
                                manager = dt2.Rows[0]["MasterCode"].ToString();
                                managername = dt2.Rows[0]["MasterName"].ToString();

                                //如果部門主管為空繼續找到上級部門繼續循環

                                if (manager == "00000000")
                                {
                                    parent = dt2.Rows[0]["ParentCode"].ToString();
                                }
                                else
                                {
                                    //主管職等
                                    DataTable dt3 = DA.GetRows("select * from  dbo.HRUser  where UserCode='" + manager + "' ").Tables[0];
                                    //申請人職等
                                    DataTable dt4 = DA.GetRows("select * from  dbo.HR_Employee  where Emp_No='" + empno + "' ").Tables[0];

                                    string managerp = "";
                                    string p = "";
                                    if (dt3.Rows.Count > 0)
                                    {
                                        managerp = dt3.Rows[0]["TitleCode"].ToString();
                                    }
                                    else
                                    {
                                        manager = "00000000";
                                        break;
                                    }



                                    if (dt4.Rows.Count > 0)
                                    {
                                        p = dt4.Rows[0]["Emp_Title"].ToString();
                                    }

                                    if (manager.TrimStart('0') == empno.TrimStart('0'))
                                    {
                                        parent = dt2.Rows[0]["ParentCode"].ToString();
                                        manager = "00000000";
                                    }
                                    //如果不是作業員且主管為理級以下      直接人員課級起， 間接人員理級起
                                    else if (managerp == "E" && p != "W1" && p != "W2" && p != "W3" && p != "W4" && p != "P1-1" && p != "P1-2")
                                    {
                                        parent = dt2.Rows[0]["ParentCode"].ToString();
                                        manager = "00000000";
                                    }

                                }

                            }
                            else
                            {

                                break;
                            }
                        }
                    }

                    #endregion
                    //2017-07-31  新增  如果主管工號存在但無法抓取到職等時返回空
                    else if (manager != "00000000" && managerp0 == "")
                    {
                        manager = "00000000";
                    }





                    #region  如果存在代簽是W0員工找不到主管的人員
                    if (dt0.Rows.Count > 0 && dttitle.Rows.Count > 0 && (manager == "00000000" || manager == ""))
                    {
                        manager = dt0.Rows[0]["ManagerNo"].ToString();
                    }
                    // 無理級主管部門 WO員工 如果找不到課長就給代簽
                    else if (dt0.Rows.Count > 0 && dttitle.Rows.Count > 0)
                    {
                        DataTable dt5 = DA.GetRows("select * from  dbo.HRUser  where userCode='" + manager + "'  and TitleCode='E' and Title='課長' ").Tables[0];
                        if (dt5.Rows.Count > 0)
                        {
                            manager = dt5.Rows[0]["userCode"].ToString();
                        }
                        else
                        {
                            manager = dt0.Rows[0]["ManagerNo"].ToString();
                        }

                    }


                    #endregion

                }
            }
            return manager;
        }




       

       

        private void BindFactoryData()
        {
            ddlFactory.Items.Clear();
            DataTable dt = DA.GetRows("select distinct Factory from dbo.HR_Dept where  ISNULL(Factory,'')<>'' order by Factory").Tables[0];
            ddlFactory.DataSource = dt;
            ddlFactory.DataTextField = "Factory";
            ddlFactory.DataValueField = "Factory";
            ddlFactory.DataBind();
            ddlFactory.Items.Insert(0, new ListItem("--請選擇--", ""));
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
                               

        protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {

        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
           
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

        protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
        {

        }

        protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {

        }

        protected void Button2_Click(object sender, EventArgs e)
        {

        }

        protected void txtempno_TextChanged(object sender, EventArgs e)
        {

            lb1.Text = "";
            DataTable table = ViewState["dt"] as DataTable;
            table.Rows.Clear();
            btnok.Visible = false;
            btnback.Visible = false;
            GridView1.DataSource = ViewState["dt"] as DataTable;
            GridView1.DataBind();


            if (txtempno.Text.Trim() == "")
            {
                //ScriptManager.RegisterStartupScript(UpdatePanel1, this.GetType(), "alert", "alert('資料不能為空!'')", true);
                ScriptManager.RegisterClientScriptBlock(UpdatePanel1, typeof(UpdatePanel), "提示", "alert('資料不能為空!');", true);
                return;
            }
            else
            {

                string empno = txtempno.Text.Trim();
                string empno2=empno.ToString().PadLeft(4,'0');

                DataSQL DA = new DataSQL();

                DataTable dt = DA.GetRows("select * from dbo.HR_Employee where Emp_OutDate>GETDATE()  and Area in ('CQ') and   Emp_Title not in ('P8','P8-1','P8-2','P9','P10','P11','P12','L8','L8-1','L8-2','L9','L10','L11','L12') and  isnull(Emp_No,'')='" + empno2 + "'  ").Tables[0];

                if (dt.Rows.Count > 0)
                {
                    txtempno.Text = dt.Rows[0]["Emp_No"].ToString();
                    txtempname.Text = dt.Rows[0]["Emp_Name"].ToString();
                

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
                    txtempno.Text = "";
                    txtempname.Text = "";
                   
                    ddlFactory.SelectedValue = "";
                    ddlDept.SelectedValue = "";
                    //ScriptManager.RegisterStartupScript(UpdatePanel1, this.GetType(), "alert", "alert('工號不存在,請手工輸入姓名與郵箱!'')", true);
                    ScriptManager.RegisterClientScriptBlock(UpdatePanel1, typeof(UpdatePanel), "提示", "alert('工號不存在!');", true);
                }
            }
        }

        protected void btnback_Click(object sender, EventArgs e)
        {
            txtempno.Text = "";
            txtempname.Text = "";
            ddlFactory.SelectedValue = "";
            ddlDept.SelectedValue = "";

            txtremark.Text = "";
            txtdatestar.Value = "";
            txtdateend.Value = "";
            txttimestar.Value = "";
            txttimeend.Value = "";

            DataTable table = ViewState["dt"] as DataTable;
            table.Rows.Clear();
            btnok.Visible = false;
            btnback.Visible = false;
            GridView1.DataSource = ViewState["dt"] as DataTable;
            GridView1.DataBind();
        }

        protected void ddlDept_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        protected void Button1_Click1(object sender, EventArgs e)
        {
            //if (ddlDept.SelectedValue != "")
            //{
            //    DataTable dt = DA.GetRows("select * from  dbo.HRDepartment  where DepartmentCode='" + ddlDept.SelectedValue + "' ").Tables[0];
            //    if (dt.Rows.Count > 0)
            //    {

            //        string manager = dt.Rows[0]["MasterCode"].ToString();
            //        string deptno = dt.Rows[0]["DepartmentCode"].ToString();
            //        string parent = dt.Rows[0]["ParentCode"].ToString();
            //        string managername = dt.Rows[0]["MasterName"].ToString();
            //        if (manager == "00000000")
            //        {
            //            while (manager == "00000000")
            //            {
                            
            //                DataTable dt2 = DA.GetRows("select * from  dbo.HRDepartment  where DepartmentCode='" + parent + "' ").Tables[0];
            //                if (dt2.Rows.Count > 0)
            //                {
            //                    manager = dt2.Rows[0]["MasterCode"].ToString();
            //                    managername = dt2.Rows[0]["MasterName"].ToString();

            //                   //如果部門主管為空繼續找到上級部門繼續循環

            //                    if (manager == "00000000")
            //                    {
            //                        parent = dt2.Rows[0]["ParentCode"].ToString();
            //                    }
            //                    else
            //                    {
            //                        if (manager.TrimStart('0') == txtempno.Text.Trim())
            //                        {
            //                            parent = dt2.Rows[0]["ParentCode"].ToString();
            //                            manager = "00000000";
            //                        }
            //                    }
            //                }
            //                else
            //                {
            //                    Button1.Text = "未找到";
            //                    break;
            //                }
            //            }
            //        }
            //        //如果當前申請人部門主管就是申請人則找到上一級部門主管  
            //        else  if (manager.TrimStart('0') == txtempno.Text.Trim())
            //        {
            //            manager = "00000000";
            //            while (manager == "00000000")
            //            {

            //                DataTable dt2 = DA.GetRows("select * from  dbo.HRDepartment  where DepartmentCode='" + parent + "' ").Tables[0];
            //                if (dt2.Rows.Count > 0)
            //                {
            //                    manager = dt2.Rows[0]["MasterCode"].ToString();
            //                    managername = dt2.Rows[0]["MasterName"].ToString();

            //                    //如果部門主管為空繼續找到上級部門繼續循環

            //                    if (manager == "00000000")
            //                    {
            //                        parent = dt2.Rows[0]["ParentCode"].ToString();
            //                    }
            //                    else
            //                    {
            //                        if (manager.TrimStart('0') == txtempno.Text.Trim())
            //                        {
            //                            parent = dt2.Rows[0]["ParentCode"].ToString();
            //                            manager = "00000000";
            //                        }

            //                    }

            //                }
            //                else
            //                {
            //                    Button1.Text = "未找到";
            //                    break;
            //                }
            //            }

            //        }
            //        Button1.Text = managername;

            //    }

            //}
        }


        protected void btnadd0_Click(object sender, EventArgs e)
        {
            Response.Redirect("ExcelToApply.aspx");
        }
    }
}