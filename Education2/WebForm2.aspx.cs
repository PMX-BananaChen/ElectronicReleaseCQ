using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Education2
{
    public partial class WebForm2 : System.Web.UI.Page
    {
        DataSQL DA = new DataSQL();

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            DataTable dt2 = DA.GetRows("select  * from HR_Employee  where  Factory not in ('TYM DG') ").Tables[0];

             for (int i = 0; i < dt2.Rows.Count; i++)
             {

                 string empno = dt2.Rows[i]["emp_no"].ToString();
                 string deptno = dt2.Rows[i]["Dept_No"].ToString();
                 string manager = GetManager(empno, deptno);
                 if (manager == "" || manager == "00000000")
                 {

                 }
                 else
                 {
                     DA.ExecuteReader("update HR_Employee set  Mempno= '" + manager + "'  where  Emp_No= '" + empno + "' ");
                 }


                 //DA.ExecuteReader("update HR_Employee set  Mempno= '" + manager + "'  where  Emp_No= '" + empno + "' ");


             }
            RegisterStartupScript("", "<script>alert('成功!')</script>");
                

        }


        public string GetManager(string empno, string dept)
        {


            string manager = "00000000";
            if (!string.IsNullOrEmpty(dept))
            {
                DataTable dt0 = DA.GetRows("select * from  dbo.Agent  where Dept_No='" + dept + "' ").Tables[0];
                DataTable dt = DA.GetRows("select * from  dbo.HRDepartment  where DepartmentCode='" + dept + "' ").Tables[0];


                if (dt0.Rows.Count > 0)
                {
                    manager = dt0.Rows[0]["ManagerNo"].ToString(); ;
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

                                    if (manager.TrimStart('0') == empno)
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

                    #region
                    //如果當前申請人部門主管就是申請人則找到上一級部門主管  
                    else if (manager.TrimStart('0') == empno)
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

                                    if (manager.TrimStart('0') == empno)
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

                                    if (manager.TrimStart('0') == empno)
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

                   
                }
            }
            return manager;
        }
    


    }
}


