using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Education2
{
    public partial class Test : System.Web.UI.Page
    {
        
        DataSQL DA = new DataSQL();

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            DataTable dt = DA.GetRows("select * from  dbo.HR_Employee  where Area='CQ' ").Tables[0];
            for (int i=0;i< dt.Rows.Count; i++)
            {
                string empno = dt.Rows[i]["Emp_No"].ToString();
                string deptno = dt.Rows[i]["Dept_No"].ToString();
                string manager = GetManager(empno, deptno);

                DataTable dt2 = DA.GetRows("select * from  dbo.Allograph_Person  where  cast(emp_no as int)='" + empno + "'").Tables[0];
                if (dt2.Rows.Count > 0)
                {
                    manager = dt2.Rows[0]["ManagerNo"].ToString();
                }


                if (!string.IsNullOrEmpty(empno))

                {
                    DA.ExecuteReader("update HR_Employee set   Test1='"+manager+"' where  Emp_No='"+empno+"' ");
                }

            }


            RegisterStartupScript("","<scrit>alert('批量更新成功')</script>");
            
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
              // 如果不存在代簽或者WO的開始循環
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

                    //主管職等
                    if (dt01.Rows.Count > 0)
                    {
                        managerp0 = dt01.Rows[0]["TitleCode"].ToString();
                    }
                    //申請人職等
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
                                    else if (managerp == "E" && p == "W0")
                                    {
                                        DataTable dtE = DA.GetRows("select * from  dbo.HR_Employee  where RIGHT('00000000'+CAST(Emp_No AS nvarchar(50)),8 )='" + manager + "' and isnull(Emp_Title,'')  in  ('W0','W1','W1-','W2','W2-','W3','P0','P1','P1-','P2','P2-','P3','P4','P5','P6','P7','P8','P9','P10','L3') ").Tables[0];
                                        if (dtE.Rows.Count > 0)
                                        {
                                            parent = dt2.Rows[0]["ParentCode"].ToString();
                                            manager = "00000000";
                                        }

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
                                    else if (managerp == "E" && p == "W0")
                                    {
                                        DataTable dtE = DA.GetRows("select * from  dbo.HR_Employee  where RIGHT('00000000'+CAST(Emp_No AS nvarchar(50)),8 )='" + manager + "' and isnull(Emp_Title,'')  in  ('W0','W1','W1-','W2','W2-','W3','P0','P1','P1-','P2','P2-','P3','P4','P5','P6','P7','P8','P9','P10','L3') ").Tables[0];
                                        if (dtE.Rows.Count > 0)
                                        {
                                            parent = dt2.Rows[0]["ParentCode"].ToString();
                                            manager = "00000000";
                                        }

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
                        //如果有E先檢查是不是課長,如果不是則循環找到   沒有一樣給代簽
                        DataTable dt5 = DA.GetRows("select * from  dbo.HRUser  where userCode='" + manager + "'  and TitleCode='E'  ").Tables[0];
                        if (dt5.Rows.Count > 0)
                        {

                           
                            DataTable dtE = DA.GetRows("select * from  dbo.HR_Employee  where RIGHT('00000000'+CAST(Emp_No AS nvarchar(50)),8 )='" + manager + "' and isnull(Emp_Title,'')  in  ('W0','W1','W1-','W2','W2-','W3','P0','P1','P1-','P2','P2-','P3','P4','P5','P6','P7','P8','P9','P10','L3') ").Tables[0];
                            //如果不是課長就循環
                            if (dtE.Rows.Count > 0)
                            {
                                string title_p = "0";
                                while (title_p == "0")
                                {
                                    DataTable dt6 = DA.GetRows("select * from  dbo.HRDepartment  where DepartmentCode='" + parent + "' ").Tables[0];
                                    if (dt6.Rows.Count > 0)
                                    {
                                        manager = dt6.Rows[0]["MasterCode"].ToString();
                                        managername = dt6.Rows[0]["MasterName"].ToString();
                                        parent = dt6.Rows[0]["ParentCode"].ToString();

                                        if (manager != "00000000")
                                        {
                                            DataTable dt7 = DA.GetRows("select * from  dbo.HRUser  where userCode='" + manager + "'  and TitleCode='E'  ").Tables[0];
                                            //如果存在課級的
                                            if (dt7.Rows.Count > 0)
                                            {
                                                DataTable dtE2 = DA.GetRows("select * from  dbo.HR_Employee  where RIGHT('00000000'+CAST(Emp_No AS nvarchar(50)),8 )='" + manager + "' and isnull(Emp_Title,'')  in  ('W0','W1','W1-','W2','W2-','W3','P0','P1','P1-','P2','P2-','P3','P4','P5','P6','P7','P8','P9','P10','L3') ").Tables[0];
                                                //如果是課長
                                                if (dtE2.Rows.Count == 0)
                                                {
                                                    title_p = "1";
                                                    manager = dt6.Rows[0]["MasterCode"].ToString();
                                                    break;
                                                }
                                            }
                                            //如果已經過了課級 則給代簽
                                            else
                                            {
                                                title_p = "1";
                                                manager = dt0.Rows[0]["ManagerNo"].ToString();
                                            }
                                        }

                                    }
                                    //如果 循環到沒有組織了 就給代簽
                                    else
                                    {
                                        title_p = "1";
                                        manager = dt0.Rows[0]["ManagerNo"].ToString();
                                    }
                                }
                            }
                            //如果E是課長就給課長
                            else
                            {
                                manager = dt5.Rows[0]["userCode"].ToString();
                            }                        
                        }
                       //如果不存在E 就給代簽
                        else
                        {
                            manager = dt0.Rows[0]["ManagerNo"].ToString();
                        }
                    }
                    #endregion
                    #region 不存在代理 找到課長的人員 進一步判斷 
                    else if (dt0.Rows.Count == 0 && dttitle.Rows.Count > 0)
                    { 
                        //如果有E先檢查是不是課長,如果不是則循環找到   
                        DataTable dt5 = DA.GetRows("select * from  dbo.HRUser  where userCode='" + manager + "'  and TitleCode='E'  ").Tables[0];
                        if (dt5.Rows.Count > 0)
                        {
                             DataTable dtE = DA.GetRows("select * from  dbo.HR_Employee  where RIGHT('00000000'+CAST(Emp_No AS nvarchar(50)),8 )='" + manager + "' and isnull(Emp_Title,'')  in  ('W0','W1','W1-','W2','W2-','W3','P0','P1','P1-','P2','P2-','P3','P4','P5','P6','P7','P8','P9','P10','L3') ").Tables[0];
                            //如果不是課長就循環
                             if (dtE.Rows.Count > 0)
                             {
                                 string title_p = "0";
                                 while (title_p == "0")
                                 {
                                     DataTable dt6 = DA.GetRows("select * from  dbo.HRDepartment  where DepartmentCode='" + parent + "' ").Tables[0];
                                     if (dt6.Rows.Count > 0)
                                     {
                                         manager = dt6.Rows[0]["MasterCode"].ToString();
                                         managername = dt6.Rows[0]["MasterName"].ToString();
                                         parent = dt6.Rows[0]["ParentCode"].ToString();

                                         if (manager != "00000000")
                                         {
                                             DataTable dt7 = DA.GetRows("select * from  dbo.HRUser  where userCode='" + manager + "'  and TitleCode='E'  ").Tables[0];
                                             //如果存在課級的
                                             if (dt7.Rows.Count > 0)
                                             {
                                                 DataTable dtE2 = DA.GetRows("select * from  dbo.HR_Employee  where RIGHT('00000000'+CAST(Emp_No AS nvarchar(50)),8 )='" + manager + "' and isnull(Emp_Title,'')  in  ('W0','W1','W1-','W2','W2-','W3','P0','P1','P1-','P2','P2-','P3','P4','P5','P6','P7','P8','P9','P10','L3') ").Tables[0];
                                                 //如果是課長
                                                 if (dtE2.Rows.Count == 0)
                                                 {
                                                     title_p = "1";
                                                     manager = dt6.Rows[0]["MasterCode"].ToString();
                                                     break;
                                                 }
                                             }
                                             //如果已經過了課級 
                                             else
                                             {
                                                 title_p = "1";
                                             }
                                         }


                                     }
                                     //如果 循環到沒有組織了 
                                     else
                                     {
                                         title_p = "1";
                                         manager = "00000000";
                                     }
                                 }
                             }
                             else
                             {
                                 manager = dt5.Rows[0]["userCode"].ToString();
                             }
                        }
                    }
                      #endregion
                }

            }
            return manager;
        }
    }
}