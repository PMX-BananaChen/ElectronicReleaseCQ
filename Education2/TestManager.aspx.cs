using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Education2
{
    public partial class TestManager : System.Web.UI.Page
    {
        DataSQL DA = new DataSQL();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                BindFactoryData();
                ddlDept.Items.Insert(0, new ListItem("--請選擇--", ""));
            }
        }

        protected void ddlFactory_SelectedIndexChanged(object sender, EventArgs e)
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

        protected void txtempno_TextChanged(object sender, EventArgs e)
        {
            if (txtempno.Text.Trim() == "")
            {
                RegisterStartupScript("", "<script>alert('資料不能為空')</script>");
                return;
            }
            else
            {
                DataSQL DA = new DataSQL();

                DataTable dt = DA.GetRows("select * from dbo.HR_Employee where Emp_OutDate>GETDATE()   and isnull(Emp_No,'')='" + txtempno.Text.Trim() + "'  ").Tables[0];

                if (dt.Rows.Count > 0)
                {
                    txtname.Text = dt.Rows[0]["Emp_Name"].ToString();

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
                    txtname.Text = "";
                    ddlFactory.SelectedValue = "";
                    ddlDept.SelectedValue = "";
                    RegisterStartupScript("", "<script>alert('工號不存在')</script>");
                }
            }
        }

        protected void btnlogin_Click(object sender, EventArgs e)
        {
            lb1.Text = "";
            if (ddlDept.SelectedValue != "")
            {
                DataTable dt0 = DA.GetRows("select * from  dbo.Allograph  where Dept_No='" + ddlDept.SelectedValue + "' ").Tables[0];
                DataTable dt = DA.GetRows("select * from  dbo.HRDepartment  where DepartmentCode='" + ddlDept.SelectedValue + "' ").Tables[0];
                DataTable dttitle = DA.GetRows("select * from  dbo.HR_Employee  where ISNULL(Emp_No,'')='" + txtempno.Text.Trim() + "' and  Emp_Title='W0' ").Tables[0];

                //代簽

                #region 部門代簽并不為W0
                if (dt0.Rows.Count > 0 && dttitle.Rows.Count < 1)
                {


                    string manager = dt0.Rows[0]["ManagerNo"].ToString();
                    string empno = txtempno.Text.Trim();
                    string managername = "";

                    //個人代簽
                    DataTable dqperson = DA.GetRows("select * from  dbo.Allograph_Person  where  cast(emp_no as int)='" + empno.TrimStart('0') + "'").Tables[0];
                    if (dqperson.Rows.Count > 0)
                    {
                        manager = dqperson.Rows[0]["ManagerNo"].ToString();
                        managername = dqperson.Rows[0]["ManagerName"].ToString();

                    }

                    //代理
                    DateTime now = DateTime.Now;

                    string TimeB = now.ToString("yyyy-MM-dd");
                    string TimeC = now.ToString("HH:mm");
                    DataTable dtagent = DA.GetRows("select * from  dbo.Agent  where ManagerNo='" + manager + "' and   Enabled='0'  and   Convert(varchar(10),Dates,120)= '" + TimeB + "'   and  (StarTime<='" + TimeC + "' and [EndTime]>='" + TimeC + "' )  ").Tables[0];

                    if (dtagent.Rows.Count > 0)
                    {
                        btnlogin.Text = dtagent.Rows[0]["DLManagerName"].ToString();
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(managername))
                        {
                             btnlogin.Text = managername;
                        }
                        else 
                        {
                          btnlogin.Text = dt0.Rows[0]["ManagerName"].ToString();
                        }
                      
                    }




                }
                #endregion
                else if (dt.Rows.Count > 0)
                {

                    string manager = dt.Rows[0]["MasterCode"].ToString();
                    string deptno = dt.Rows[0]["DepartmentCode"].ToString();
                    string parent = dt.Rows[0]["ParentCode"].ToString();
                    string managername = dt.Rows[0]["MasterName"].ToString();




                    string managerp0 = "";
                    string p0 = "";

                    //主管職等
                    DataTable dt01 = DA.GetRows("select * from  dbo.HRUser  where UserCode='" + manager + "' ").Tables[0];
                    //申請人職等
                    DataTable dt02 = DA.GetRows("select * from  dbo.HR_Employee  where Emp_No='" + txtempno.Text.Trim() + "' ").Tables[0];


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
                                    DataTable dt4 = DA.GetRows("select * from  dbo.HR_Employee  where Emp_No='" + txtempno.Text.Trim() + "' ").Tables[0];

                                    if (dt3.Rows.Count > 0)
                                    {
                                        managerp = dt3.Rows[0]["TitleCode"].ToString();
                                    }

                                    if (dt4.Rows.Count > 0)
                                    {
                                        p = dt4.Rows[0]["Emp_Title"].ToString();
                                    }
                                    //如果申請人跟部門主管是同一個人

                                    if (manager.TrimStart('0') == txtempno.Text.Trim().TrimStart('0'))
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

                    #region 如果當前申請人部門主管就是申請人則找到上一級部門主管
                    //如果當前申請人部門主管就是申請人則找到上一級部門主管  
                    else if (manager.TrimStart('0') == txtempno.Text.Trim().TrimStart('0'))
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
                                    DataTable dt4 = DA.GetRows("select * from  dbo.HR_Employee  where Emp_No='" + txtempno.Text.Trim() + "' ").Tables[0];

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

                                    if (manager.TrimStart('0') == txtempno.Text.Trim().TrimStart('0'))
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

                    #region 如果為課長并不等WO 繼續循環

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
                                    DataTable dt4 = DA.GetRows("select * from  dbo.HR_Employee  where Emp_No='" + txtempno.Text.Trim() + "' ").Tables[0];

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

                                    if (manager.TrimStart('0') == txtempno.Text.Trim().TrimStart('0'))
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
                        manager = dt0.Rows[0]["ManagerNo"].ToString();
                        managername = dt0.Rows[0]["ManagerName"].ToString();
                    }
                    else if (dt0.Rows.Count > 0 && dttitle.Rows.Count > 0)
                    {
                        DataTable dt5 = DA.GetRows("select * from  dbo.HRUser  where userCode='" + manager + "'  and TitleCode='E' and Title='課長' ").Tables[0];
                        if (dt5.Rows.Count > 0)
                        {
                            manager = dt5.Rows[0]["userCode"].ToString();
                            managername = dt5.Rows[0]["userName"].ToString();
                        }
                        else
                        {
                            manager = dt0.Rows[0]["ManagerNo"].ToString();
                            managername = dt0.Rows[0]["ManagerName"].ToString();
                        }

                    }


                    #endregion





                    string empno = txtempno.Text.Trim();


                    //個人代簽
                    DataTable dqperson = DA.GetRows("select * from  dbo.Allograph_Person  where  cast(emp_no as int)='" + empno.TrimStart('0') + "'").Tables[0];
                    if (dqperson.Rows.Count > 0)
                    {
                        manager = dqperson.Rows[0]["ManagerNo"].ToString();
                        managername = dqperson.Rows[0]["ManagerName"].ToString();
                    }


                    //代理
                    DateTime now = DateTime.Now;

                    string TimeB = now.ToString("yyyy-MM-dd");

                    string TimeC = now.ToString("yyyy-MM-dd HH:mm");
                    DataTable dtagent = DA.GetRows("select * from  dbo.Agent  where ManagerNo='" + manager + "'  and   Enabled='0' and   Convert(varchar(10),Dates,120)= '" + TimeB + "'   and  (StarTime<='" + TimeC + "' and [EndTime]>='" + TimeC + "' )  ").Tables[0];

                    if (dtagent.Rows.Count > 0)
                    {
                        managername = dtagent.Rows[0]["DLManagerName"].ToString();
                    }
                    //
                    btnlogin.Text = managername;
                }
            }


        }
    }
}
