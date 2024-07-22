using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
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
    public partial class ExcelToApply : System.Web.UI.Page
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

            }

        }

        protected void btnImport_Click(object sender, EventArgs e)
        {
            if (FileUpload1.HasFile == false)//HasFile用来检查FileUpload是否有指定文件
            {
                RegisterStartupScript("", "<script>alert('请您选择Excel文件')</script>");

                return;//当无文件时,返回
            }
            string IsXls = System.IO.Path.GetExtension(FileUpload1.FileName).ToString().ToLower();//System.IO.Path.GetExtension获得文件的扩展名
            if (IsXls == ".xlsx" || IsXls == ".xls")
            {
                string time = "";
                string filename = FileUpload1.FileName;              //获取Execle文件名  DateTime日期函数
                string savePath = Server.MapPath("./Upload/" + filename);  //Server.MapPath 获得虚拟服务器相对路径


                FileUpload1.SaveAs(savePath);                        //SaveAs 将上传的文件内容保存在服务器上
                DataSet ds = ExcelSqlConnection(savePath, filename, IsXls);           //调用自定义方法
                DataRow[] dr = ds.Tables[0].Select();            //定义一个DataRow数组
                int rowsnum = ds.Tables[0].Rows.Count;
                if (rowsnum == 0)
                {

                    RegisterStartupScript("", "<script>alert('Excel表为空表,无数据!')</script>");
                }
                else
                {
                    //導入前清空臨時表
                    DataTable dt = new DataTable();
                    DataColumn dc = null;

                    dc = dt.Columns.Add("ID", Type.GetType("System.Int32"));
                    dc.AutoIncrement = true;//自动增加
                    dc.AutoIncrementSeed = 1;//起始为1
                    dc.AutoIncrementStep = 1;//步长为1
                    dc.AllowDBNull = false;//
                    dt.Columns.Add("Emp_No", Type.GetType("System.String"));
                    dt.Columns.Add("EmpName", Type.GetType("System.String"));
                    dt.Columns.Add("Factory", Type.GetType("System.String"));
                    dt.Columns.Add("Dept", Type.GetType("System.String"));
                    dt.Columns.Add("DeptName", Type.GetType("System.String"));
                    dt.Columns.Add("ManagerNo", Type.GetType("System.String"));
                    dt.Columns.Add("ManagerName", Type.GetType("System.String"));
                    dt.Columns.Add("Date", Type.GetType("System.String"));
                    dt.Columns.Add("StarTime", Type.GetType("System.String"));
                    dt.Columns.Add("EndTime", Type.GetType("System.String"));
                    dt.Columns.Add("Remark", Type.GetType("System.String"));
                    for (int i = 0; i < dr.Length; i++)
                    {
                        //前面除了你需要在建立一个“upfiles”的文件夹外，其他的都不用管了，你只需要通过下面的方式获取Excel的值，然后再将这些值用你的方式去插入到数据库里面
                        string empno = dr[i]["工號"].ToString();

                        empno = empno.PadLeft(4, '0');


                        string empname = "";
                        //string factory = dr[i]["BU"].ToString();
                        //string dept = dr[i]["部門編碼"].ToString();

                        //dept=dept.PadLeft(8, '0');

                        if (string.IsNullOrEmpty(dr[i]["申請日期"].ToString()))
                        {
                            RegisterStartupScript("", "<script>alert('申請日期不能為空!')</script>");
                            return;
                        }
                        else if (string.IsNullOrEmpty(dr[i]["開始時段"].ToString()))
                        {
                            RegisterStartupScript("", "<script>alert('開始時段不能為空!')</script>");
                            return;
                        }
                        else if (string.IsNullOrEmpty(dr[i]["結束時段"].ToString()))
                        {
                            RegisterStartupScript("", "<script>alert('結束時段不能為空!')</script>");
                            return;
                        }
                        else if (string.IsNullOrEmpty(empno))
                        {
                            RegisterStartupScript("", "<script>alert('工號不能為空!')</script>");
                            return;
                        }
                        //else if (string.IsNullOrEmpty(factory))
                        //{
                        //    RegisterStartupScript("", "<script>alert('BU不能為空!')</script>");
                        //    return;
                        //}
                        //else if (string.IsNullOrEmpty(dept))
                        //{
                        //    RegisterStartupScript("", "<script>alert('部門不能為空!')</script>");
                        //    return;
                        //}


                        DateTime date0;
                        DateTime time1;
                        DateTime time2;
                        try
                        {
                             date0 = DateTime.ParseExact(dr[i]["申請日期"].ToString(), "yyyyMMdd", System.Globalization.CultureInfo.CurrentCulture);
                             time1 = DateTime.ParseExact(dr[i]["開始時段"].ToString(), "HHmm", System.Globalization.CultureInfo.CurrentCulture);
                             time2 = DateTime.ParseExact(dr[i]["結束時段"].ToString(), "HHmm", System.Globalization.CultureInfo.CurrentCulture);
                             if (time1 >= time2)

                             {
                                 RegisterStartupScript("", "<script>alert('結束日期不能小於或者等於開始日期!')</script>");
                                 return;
                             }
                        }
                        catch
                        {
                            RegisterStartupScript("", "<script>alert('日期或時段格式錯誤!')</script>");
                            return;
                        }

                       

                        string date = date0.ToString("yyyy-MM-dd");
                        string star = time1.ToString("HH:mm");
                        string end = time2.ToString("HH:mm");
                        string remark = dr[i]["申請原因"].ToString();
                        string deptname = "";
                        string factory = "";
                        string dept = "";
                        string area = "";
                       


                        DataTable dt2 = DA.GetRows("select * from  dbo.HR_Employee  where Emp_OutDate>GETDATE()   and Area in ('CQ') and Emp_Title not in ('P8','P8-1','P8-2','P9','P10','P11','P12','L8','L8-1','L8-2','L9','L10','L11','L12')   and   Emp_No='" + empno + "' ").Tables[0];
                        if (dt2.Rows.Count > 0)
                        {
                            empname = dt2.Rows[0]["Emp_Name"].ToString();
                            factory = dt2.Rows[0]["Factory"].ToString();
                            dept = dt2.Rows[0]["Dept_No"].ToString();
                            area = dt2.Rows[0]["Area"].ToString();

                            if (string.IsNullOrEmpty(empname))
                            {
                                RegisterStartupScript("", "<script>alert('無法找到您的姓名!')</script>");
                                return;
                            }
                            if (string.IsNullOrEmpty(factory))
                            {
                                RegisterStartupScript("", "<script>alert('無法找到您的BU!')</script>");
                                return;
                            }
                            if (string.IsNullOrEmpty(dept))
                            {
                                RegisterStartupScript("", "<script>alert('無法找到您的部門!')</script>");
                                return;
                            }
                            //if (string.IsNullOrEmpty(area))
                            //{
                            //    RegisterStartupScript("", "<script>alert('無法找到您的歸屬公司!')</script>");
                            //    return;
                            //}
                        }
                        else
                        { 
                             RegisterStartupScript("", "<script>alert('"+empno+"該工號不存在或超過7職等以上')</script>");
                             return;
                        }

                        dept=dept.PadLeft(8, '0');
                        DataTable dt3 = DA.GetRows("select * from  dbo.HR_Employee  where  Emp_OutDate>GETDATE()  and  Emp_No='" + empno + "' and   Dept_No='" + dept + "'  ").Tables[0];
                        if (dt3.Rows.Count > 0)
                        {
                            DataTable dt4 = DA.GetRows("select * from  dbo.HR_Dept  where DeptNo='" + dept + "' ").Tables[0];
                            if (dt4.Rows.Count > 0)
                            {
                                deptname = dt4.Rows[0]["DeptName"].ToString();
                            }
                        }

                        string managerno = "";
                        if (area == "TYM")
                        { 
                            managerno = manaTYM(empno, dept);
                        }
                        else 
                        {
                            managerno = mana(empno, dept);
                        }

                        

                        //個人代簽
                        DataTable dqperson = DA.GetRows("select * from  dbo.Allograph_Person  where  cast(emp_no as int)='" + empno.TrimStart('0') + "'").Tables[0];
                        if (dqperson.Rows.Count > 0)
                        {
                            managerno = dqperson.Rows[0]["ManagerNo"].ToString();
                        }

                        //代理
                        DateTime now = DateTime.Now;

                        string TimeB = now.ToString("yyyy-MM-dd");
                        string TimeC = now.ToString("yyyy-MM-dd HH:mm");
                        DataTable dtagent = DA.GetRows("select * from  dbo.Agent  where ManagerNo='" + managerno + "' and   Enabled='0'  and    (StarTime<='" + TimeC + "' and [EndTime]>='" + TimeC + "' )  ").Tables[0];
                        if (dtagent.Rows.Count > 0)
                        {
                            managerno = dtagent.Rows[0]["DLManagerNo"].ToString();
                        }


                        if (managerno == "01000001" || managerno == "01004421")
                        {

                            RegisterStartupScript("", "<script>alert('Excel未抓取到部门主管，请联系!')</script>");
                            return;
                        }



                        string managername = "";
                        DataTable dt5 = DA.GetRows("select * from  dbo.Users where UserEmpNo='" + managerno + "' ").Tables[0];
                        if (dt5.Rows.Count > 0)
                        {
                            managername = dt5.Rows[0]["userName"].ToString();
                        }

                        //疫情期間只能申請當天至第三個工作日
                        string message = DA.checkDate(date);
                        if (!string.IsNullOrEmpty(message))
                        {
                            RegisterStartupScript("", "<script>alert('" + message + "')</script>");
                            return;
                        }

                        DataRow newRow;
                        newRow = dt.NewRow();
                        newRow["Emp_No"] = empno;
                        newRow["EmpName"] = empname;
                        newRow["Factory"] = factory;
                        newRow["Dept"] = dept;
                        newRow["DeptName"] = deptname;
                        newRow["ManagerNo"] = managerno;
                        newRow["ManagerName"] = managername;
                        newRow["Date"] = date;
                        newRow["StarTime"] = star;
                        newRow["EndTime"] = end;
                        newRow["Remark"] = remark;
                        dt.Rows.Add(newRow);
                        //Response.Write("<script>alert('导入内容:" + ex.Message + "')</script>");
                    }


                    string now2 = DateTime.Now.AddDays(-2).ToString("yyyy-MM-dd");
                    string atertime = DateTime.Now.AddDays(+30).ToString("yyyy-MM-dd");
                    DataRow[] counts = dt.Select("Date<='" + now2 + "'");
                    DataRow[] counts2 = dt.Select("Date>'" + atertime + "'");

                    int cc = counts.Length;
                    int aftercc = counts2.Length;
                    if (cc > 0)
                    {
                        RegisterStartupScript("", "<script>alert('Excel資料存在小於昨日日期的單據，請刪除!')</script>");
                        return;
                    }
                    else
                        if (aftercc > 0)
                    {
                        RegisterStartupScript("", "<script>alert('Excel資料存在大於當天日期31天的單據，請刪除!')</script>");
                        return;
                    }


                    gdv1.DataSource = dt;
                    gdv1.DataBind();
                    btndrqr.Visible = true;
                }
            }
            else
            {
                RegisterStartupScript("", "<script>alert('只可以选择Excel文件')</script>");
                return;//当选择的不是Excel文件时,返回
            }
        }
        public static System.Data.DataSet ExcelSqlConnection(string filepath, string tableName, string strEName)
        {
            //string strCon = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + filepath + ";Extended Properties='Excel 8.0;HDR=YES;IMEX=1'";
            string strCon = "";
            if (strEName == ".xls")
            {
                strCon = " Provider = Microsoft.Jet.OLEDB.4.0 ; Data Source = " + filepath + ";Extended Properties='Excel 8.0;HDR=YES'";
             
            }
            else if (strEName == ".xlsx")
            {
                strCon = " Provider = Microsoft.ACE.OLEDB.12.0 ; Data Source= " + filepath + ";Extended Properties='Excel 12.0 Xml;HDR=YES'";
            }
            else
            {
                return null;
            }

           

            OleDbConnection ExcelConn = new OleDbConnection(strCon);
            try
            {
                string strCom = string.Format("SELECT * FROM [Sheet1$]");
                ExcelConn.Open();
                OleDbDataAdapter myCommand = new OleDbDataAdapter(strCom, ExcelConn);
                DataSet ds = new DataSet();
                myCommand.Fill(ds, "[" + tableName + "$]");
                ExcelConn.Close();
              
                return ds;
        }
            catch
            {
                ExcelConn.Close();
               
                return null;

               
            }
}




        public  string mana  (string empno,string dept)
        {

            string manager = "";

            DataTable dt = DA.GetRows("select * from  dbo.HRDepartment  where DepartmentCode='" + dept + "' ").Tables[0];

            DataTable dt0 = DA.GetRows("select * from  dbo.Allograph  where Dept_No='" + dept + "' ").Tables[0];
            DataTable dttitle = DA.GetRows("select * from  dbo.HR_Employee  where ISNULL(Emp_No,'')='" + empno + "' and  Emp_Title='W0' ").Tables[0];
            //如果存在部門代簽,直接選擇主管(W0職等除外)
            if (dt0.Rows.Count > 0 && dttitle.Rows.Count < 1)
            {
                manager = dt0.Rows[0]["ManagerNo"].ToString(); ;
            }
            //判斷部門是否存在

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
                #region
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
                #region
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

                #region  如果存在代簽是W0員工找不到主管的人員
                if (dt0.Rows.Count > 0 && dttitle.Rows.Count > 0 && (manager == "00000000" || manager == ""))
                {
                    manager = dt0.Rows[0]["ManagerNo"].ToString(); ;
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

            return manager;

        }


        public string manaTYM(string empno, string dept)
        {

            string manager = "";

            DataTable dt = DA.GetRows("select * from  dbo.HRDepartment  where DepartmentCode='" + dept + "' ").Tables[0];

            DataTable dt0 = DA.GetRows("select * from  dbo.Allograph  where Dept_No='" + dept + "' ").Tables[0];
            DataTable dttitle = DA.GetRows("select * from  dbo.HR_Employee  where ISNULL(Emp_No,'')='" + empno + "' and  Emp_Title in ('W1','W2','W3','W4','P1-1','P1-2') ").Tables[0];
            //如果存在部門代簽,直接選擇主管(W0職等除外)
            if (dt0.Rows.Count > 0 && dttitle.Rows.Count < 1)
            {
                manager = dt0.Rows[0]["ManagerNo"].ToString(); ;
            }
            //判斷部門是否存在

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
                #region
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
                #region
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

                #region  如果存在代簽是W0員工找不到主管的人員
                if (dt0.Rows.Count > 0 && dttitle.Rows.Count > 0 && (manager == "00000000" || manager == ""))
                {
                    manager = dt0.Rows[0]["ManagerNo"].ToString(); ;
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

            return manager;

        }


        protected void gridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

        }

        protected void gridView_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }

        

        protected void btndrqr_Click(object sender, EventArgs e)
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
           

            DataTable dt = new DataTable();
            dt.Columns.Add("ManagerNo", Type.GetType("System.String"));

           
            for (int i = 0; i < this.gdv1.Rows.Count; i++)
            {

                gdv1.Rows[i].Cells[0].Text = gdv1.Rows[i].Cells[0].Text.Trim().Replace("&nbsp;", "");
                gdv1.Rows[i].Cells[3].Text = gdv1.Rows[i].Cells[3].Text.Trim().Replace("&nbsp;", "");
                gdv1.Rows[i].Cells[4].Text = gdv1.Rows[i].Cells[4].Text.Trim().Replace("&nbsp;", "");
                gdv1.Rows[i].Cells[8].Text = gdv1.Rows[i].Cells[8].Text.Trim().Replace("&nbsp;", "");
                gdv1.Rows[i].Cells[9].Text = gdv1.Rows[i].Cells[9].Text.Trim().Replace("&nbsp;", "");
                gdv1.Rows[i].Cells[10].Text = gdv1.Rows[i].Cells[10].Text.Trim().Replace("&nbsp;", "");
             



                gdv1.Rows[i].Cells[2].Text = gdv1.Rows[i].Cells[2].Text.Trim().Replace("&amp;", "&");
                gdv1.Rows[i].Cells[3].Text = gdv1.Rows[i].Cells[3].Text.Trim().Replace("&amp;", "&");
                gdv1.Rows[i].Cells[4].Text = gdv1.Rows[i].Cells[4].Text.Trim().Replace("&amp;", "&");


                if (gdv1.Rows[i].Cells[1].Text.Trim() == "" )
                {
                    RegisterStartupScript("", "<script>alert('姓名不能為空或工號為找到對應人員資料')</script>");
                    return;

                }
                else if (gdv1.Rows[i].Cells[4].Text.Trim() == "")
                {
                    RegisterStartupScript("", "<script>alert('部門名稱未找到或該工號人員的部門編號與資料庫不匹配')</script>");
                    return;

                }

                else if (gdv1.Rows[i].Cells[0].Text.Trim() == ""  || gdv1.Rows[i].Cells[2].Text.Trim() == "" || gdv1.Rows[i].Cells[3].Text.Trim() == ""  || gdv1.Rows[i].Cells[5].Text.Trim() == "" || gdv1.Rows[i].Cells[7].Text.Trim() == ""  || gdv1.Rows[i].Cells[9].Text.Trim() == "" || gdv1.Rows[i].Cells[10].Text.Trim() == "")
                {
                    RegisterStartupScript("", "<script>alert('需導入的資料不能為空或未能找到對應部門的資料!請重新整理資料再導入')</script>");
                    return;

                }

               

                DataRow[] drs = dt.Select("ManagerNo = '" + gdv1.Rows[i].Cells[9].Text + "'");
                if (drs.Length <1)
                {
                    DataRow newRow;
                    newRow = dt.NewRow();
                    newRow["ManagerNo"] = gdv1.Rows[i].Cells[9].Text;
                    dt.Rows.Add(newRow);
                }
                   

            }
           
            DateTime now = DateTime.Now;
            string TimeA = now.ToString("yyyy-MM-dd HH:mm:ss");
            DateTime dt1 = DateTime.Parse(now.ToString("yyyy-MM-dd"));

            string apptype = "";
            for (int i = 0; i < this.gdv1.Rows.Count; i++)
            {
                DateTime dt2 = DateTime.Parse(now.ToString(gdv1.Rows[i].Cells[5].Text));
                if (dt2 < dt1)
                {
                    apptype = "補單";
                }
                else
                {
                    apptype = "正常申請";
                }

                 string mail = "";
                 DataTable dt3 =DA.GetRows("select * from  dbo.Users where UserID='" + userid + "' ").Tables[0];
                 if (dt3.Rows.Count > 0)
                 {
                     mail = dt3.Rows[0]["Email"].ToString();
                 }

               

                 DA.ExecuteReader("insert into ReleaseApply (EmpNo,EmpName,Factory,Dept,Mail,Date,StarTime,EndTime,States,Remark,Type,CreateUser,CreateUserEmpNo,CreateDate,ManagerNo,UpdateDate) values ('" + gdv1.Rows[i].Cells[0].Text + "',N'" + gdv1.Rows[i].Cells[1].Text + "','" + gdv1.Rows[i].Cells[2].Text + "','" + gdv1.Rows[i].Cells[3].Text + "','" + mail + "','" + gdv1.Rows[i].Cells[5].Text + "','" + gdv1.Rows[i].Cells[6].Text + "','" + gdv1.Rows[i].Cells[7].Text + "',1,N'" + gdv1.Rows[i].Cells[8].Text + "','" + apptype + "','" + userid + "','" + createempno + "','" + TimeA + "','" + gdv1.Rows[i].Cells[9].Text + "',getdate())  ");
            }


            if (dt.Rows.Count > 0)
            {
                string loginname = ""; string loginmail = ""; string loginempid = "";
                DataTable login = DA.GetRows("select * from  dbo.Users where UserID='" + userid + "' ").Tables[0];
                if (login.Rows.Count > 0)
                {
                    loginname = login.Rows[0]["UserName"].ToString();
                    loginempid = login.Rows[0]["UserEmpNo"].ToString();
                    loginmail = login.Rows[0]["Email"].ToString();
                }


                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DataTable dt4 = DA.GetRows("select * from  dbo.Users  where UserEmpNo='" + dt.Rows[i]["ManagerNo"] + "' ").Tables[0];
                    string a = dt4.Rows[0]["Email"].ToString();
                    string b = dt4.Rows[0]["UserEmpno"].ToString();

                    DA.ExecuteReader("exec Excel_MailManager '" + dt4.Rows[i]["UserEmpNo"].ToString() + "',N'" + dt4.Rows[i]["userName"].ToString() + "',N'" + loginname + "','" + loginmail + "','" + TimeA + "' ");






                }
            }
            RegisterStartupScript("", "<script>alert('批量申請成功!')</script>");
        }


        public void WXmessage(string id, string empno, string content, string link_url, string title, string createuser)
        {

            string url = "http://wx.primax.com.cn/FangXing/Api/sendCardMessage";

            string md5 = "apply_id=" + id + "&content=" + content + "&empno=" + empno + "&link_url=" + link_url + "&title=" + title;
            string aa = md5 + "&appkey=aXkzUlJBelJiU3BTQlpMU3B6MENLZUZhcDQ1TDNseko5SXVHVzI";

            string bb = UserMd5(aa);
            string cc = md5 + "&sign=" + bb;
            string dd = Post(url, cc);

            DA.ExecuteReader("insert into WX_To_Message (Emp_No,DocumentCreateUser,DocumentCreateDate,CreateDate,types) values ('" + empno + "','" + createuser + "',null,getdate(),'Excel0')  ");

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

        
    }
}