using System;
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
    public partial class PersonalDetail : System.Web.UI.Page
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
                DataTable dt = DA.GetRows("select a.*,b.UserName,c.StateName,Convert(varchar(10),CreateDate,120) as CreateDate2, Convert(varchar(10),Date,120) as Date2,d.DeptName as DeptName2 from  dbo.ReleaseApply a inner join  dbo.Users b  on  a.CreateUser=b.UserID   inner join State c  on a.States=c.StateID left join  HR_Dept d  on a.Dept=d.DeptNo where  a.ID='" + id + "'").Tables[0];
                if (dt.Rows.Count > 0)
                {
                    string   state = dt.Rows[0]["States"].ToString();
                    if (state == "2")
                    {
                        Panel2.Visible = true;
                        txtaudituser.Text = dt.Rows[0]["AuditUserName"].ToString();
                        txtauditdate.Text = dt.Rows[0]["AuditDate"].ToString();
                    }
                    else if (state == "3")
                    {
                        btnsubmit.Visible = true;
                        Panel1.Visible = true;
                        txtbackuser.Text = dt.Rows[0]["BackUserName"].ToString();
                        txtbackdate.Text = dt.Rows[0]["BackDate"].ToString();
                        txtbackremark.Text = dt.Rows[0]["BackRemark"].ToString();
                        txtpassdate.Disabled=false;
                        txttimestar.Disabled = false;
                        txttimeend.Disabled = false;
                       
                    }
                    txtempno.Text = dt.Rows[0]["EmpNo"].ToString();
                    txtempname.Text= dt.Rows[0]["EmpName"].ToString();
                    txtfactory.Text = dt.Rows[0]["Factory"].ToString();
                    txtdept.Text = dt.Rows[0]["DeptName2"].ToString();
                    txtapply.Text = dt.Rows[0]["Type"].ToString();
                    txtpassdate.Value = dt.Rows[0]["Date2"].ToString();
                    txttimestar.Value = dt.Rows[0]["StarTime"].ToString();
                    txttimeend.Value = dt.Rows[0]["EndTime"].ToString();
                    createuser.Text = dt.Rows[0]["UserName"].ToString();
                    createdate.Text = dt.Rows[0]["CreateDate2"].ToString();
                    txtstate.Text = dt.Rows[0]["StateName"].ToString();
                    txtremark.Text = dt.Rows[0]["Remark"].ToString();
                }

              
                DataTable dt2 = DA.GetRows("select * from Users where UserID='" + userid + "'").Tables[0];
                if (dt2.Rows.Count > 0)
                {
                   string  name = dt2.Rows[0]["UserName"].ToString();
                }

            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Response.Redirect("PersonalDocuments.aspx");
        }

        protected void btnsubmit_Click(object sender, EventArgs e)
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




            if (txtpassdate.Value == "" || txttimestar.Value == "" || txttimeend.Value == "")
            {
                ScriptManager.RegisterClientScriptBlock(UpdatePanel1, typeof(UpdatePanel), "提示", "alert('通行日期時段不能為空!');", true);
                return;
            }
            else if (txtremark.Text.Trim() == "")
            {
                ScriptManager.RegisterClientScriptBlock(UpdatePanel1, typeof(UpdatePanel), "提示", "alert('放行原因不能為空!');", true);
                return;
            }
            else
            {
                DateTime pass = Convert.ToDateTime(txtpassdate.Value.Trim());
                DateTime now2 = Convert.ToDateTime(DateTime.Now.AddDays(-2).ToString("yyyy-MM-dd"));
                string time1 = txttimestar.Value.Trim();
                string time2 = txttimeend.Value.Trim();
                int a = Convert.ToInt32(time1.Replace(":", ""));
                int b = Convert.ToInt32(time2.Replace(":", ""));

                if (pass <= now2)
                {
                    ScriptManager.RegisterClientScriptBlock(UpdatePanel1, typeof(UpdatePanel), "提示", "alert('開始與結束日期不能小於昨日!');", true);
                    return;
                }

                if (a >= b)
                {
                    //ScriptManager.RegisterStartupScript(UpdatePanel1, this.GetType(), "alert", "alert('日期时间段不能为空!'')", true);
                    ScriptManager.RegisterClientScriptBlock(UpdatePanel1, typeof(UpdatePanel), "提示", "alert('開始時段不能大於或等於結束時段!');", true);
                    return;
                }
                if (txtremark.Text.Trim() == "")
                {
                    //ScriptManager.RegisterStartupScript(UpdatePanel1, this.GetType(), "alert", "alert('日期时间段不能为空!'')", true);
                    ScriptManager.RegisterClientScriptBlock(UpdatePanel1, typeof(UpdatePanel), "提示", "alert('放行原因不能為空!');", true);
                    return;
                }
                DateTime now = DateTime.Now;
                DateTime dt1 = DateTime.Parse(now.ToString("yyyy-MM-dd"));
                DateTime dt2 = DateTime.Parse(now.ToString(txtpassdate.Value));
                string apptype = null;
                if (dt2 < dt1)
                {
                    apptype = "補單";
                }
                else
                {
                    apptype = "正常申請";
                }


                string id = this.Request.QueryString["id"].ToString();

                DataTable dt = DA.GetRows("select * from ReleaseApply a  inner join Users b on a.ManagerNo=b.UserEmpNo  where ID='" + id + "'").Tables[0];
                if (dt.Rows.Count > 0)
                {
                    string TimeA = now.ToString("yyyy-MM-dd HH:mm:ss");
                    string TimeB = now.ToString("yyyy-MM-dd");
                    string TimeC = now.ToString("yyyy-MM-dd HH:mm");
                    string BackRemark = dt.Rows[0]["BackRemark"].ToString();
                    //string managerno = dt.Rows[0]["ManagerNo"].ToString();
                    string mail = dt.Rows[0]["Email"].ToString();
                    string empno = dt.Rows[0]["EmpNo"].ToString();
                    string empname = dt.Rows[0]["EmpName"].ToString();
                    string deptno = dt.Rows[0]["Dept"].ToString();


                    DataTable dthr = DA.GetRows("select * from HR_Employee   where Emp_No='" + empno + "'").Tables[0];
                    string area = "";
                    if (dthr.Rows.Count > 0)
                    {
                        string nowdeptno = dthr.Rows[0]["Dept_No"].ToString();
                        area = dthr.Rows[0]["Area"].ToString();
                        if (deptno != nowdeptno)
                        {
                            ScriptManager.RegisterClientScriptBlock(UpdatePanel1, typeof(UpdatePanel), "提示", "alert('由於你目前所在的部門編碼與之前的有差異,無法提交,請直接重新申請');", true);
                            return;
                        }

                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(UpdatePanel1, typeof(UpdatePanel), "提示", "alert('無法獲取到您目前所在的部門請聯繫HR');", true);
                        return;
                    }



                    string manager = "";
                    if (area == "TYM")
                    {
                        manager = GetManagerTYM(empno, deptno);
                    }
                    else
                    { 
                        manager = GetManager(empno, deptno);
                    }

                   


                    //個人代簽
                    DataTable dqperson = DA.GetRows("select * from  dbo.Allograph_Person  where  cast(emp_no as int)='" + empno.TrimStart('0') + "'").Tables[0];
                    if (dqperson.Rows.Count > 0)
                    {
                        manager = dqperson.Rows[0]["ManagerNo"].ToString();
                    }

                    //驗證是否有代理

                    DataTable dtagent = DA.GetRows("select * from  dbo.Agent  where ManagerNo='" + manager + "' and   Enabled='0'     and  (StarTime<='" + TimeC + "' and [EndTime]>='" + TimeC + "' )  ").Tables[0];

                    if (dtagent.Rows.Count > 0)
                    {
                        manager = dtagent.Rows[0]["DLManagerNo"].ToString();
                    }

                    if (manager == "01000001" || manager == "01004421")
                    {
                        ScriptManager.RegisterClientScriptBlock(UpdatePanel1, typeof(UpdatePanel), "提示", "alert('未抓取到部门主管,请联系HR!');", true);
                        return;
                    }


                    if (manager == "" || manager == "00000000")
                    {
                        ScriptManager.RegisterClientScriptBlock(UpdatePanel1, typeof(UpdatePanel), "提示", "alert('無法抓取到主管請聯繫HR');", true);
                        return;
                    }
                    string name = ""; string loginempid = "";  
                    DataTable dtname = DA.GetRows("select * from Users where UserID='" + userid + "'").Tables[0];
                    if (dtname.Rows.Count > 0)
                    {
                         name = dtname.Rows[0]["UserName"].ToString();
                         loginempid = dtname.Rows[0]["userEmpNo"].ToString();
                        
                    }

                    DA.ExecuteReader("update  ReleaseApply set  Type='" + apptype + "',States='1', Date=  '" + txtpassdate.Value + "',StarTime='" + txttimestar.Value + "',EndTime='" + txttimeend.Value + "',ManagerNo='" + manager + "',Remark=N'" + txtremark.Text.Trim() + "',UpdateDate=getdate(),RecreateDate=getdate() where   ID='" + id + "' ");

                 

                    DA.ExecuteReader("exec Mail '重新提交','" + mail + "','" + manager + "','" + TimeA + "',N'" + name + "',N'" + BackRemark + "' ");
                }

                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "click", "alert('已重新提交完成!');location.replace('PersonalDocuments.aspx');", true);


            }

        }



        public void WXmessage(string id, string empno, string content, string link_url, string title, string createuser)
        {

            string url = "http://wx.primax.com.cn/FangXing/Api/sendCardMessage";

            string md5 = "apply_id=" + id + "&content=" + content + "&empno=" + empno + "&link_url=" + link_url + "&title=" + title;
            string aa = md5 + "&appkey=aXkzUlJBelJiU3BTQlpMU3B6MENLZUZhcDQ1TDNseko5SXVHVzI";

            string bb = UserMd5(aa);
            string cc = md5 + "&sign=" + bb;
            string dd = Post(url, cc);

            DA.ExecuteReader("insert into WX_To_Message (Emp_No,DocumentCreateUser,DocumentCreateDate,CreateDate,types) values ('" + empno + "','" + createuser + "',null,getdate(),'0')  ");

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
    }


}