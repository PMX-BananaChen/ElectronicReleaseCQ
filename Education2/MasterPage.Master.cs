using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;

namespace Education2
{
    public partial class MasterPage : System.Web.UI.MasterPage
    {
        private XmlDocument xmlDoc;
        public string show0 = "", show1 = "", show2 = "", show3 = "", show4 = "", show5 = "", show6 = "";

        
        protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
        {

        }

        protected void Page_Load(object sender, EventArgs e)
        {
            string aaaa = "";
            //if (!Page.IsPostBack)
            //{

            string a = "test11";
            if (Session["language"] == null)
            {
                Response.Redirect("Error3.aspx");
                return;
            }
            else
            {
                a = Session["language"].ToString();
            }


                  DataSQL DA = new DataSQL();
                  DataTable dt = DA.GetRows("select * from UserRole where UserID='"+a+"'").Tables[0];
                  if (dt.Rows.Count > 0)
                  {
                      string aa = this.nav.ClientID;

                      
                      
                   
                      string role = dt.Rows[0]["RoleID"].ToString();
                      if (role == "1")
                      {
                          this.show0 = " style=\"display:True\"";
                          this.show1 = " style=\"display:True\"";
                          //this.show2 = " style=\"display:True\"";
                          this.show3 = " style=\"display:True\"";
                          this.show4 = " style=\"display:True\"";
                          this.show5 = " style=\"display:True\"";

                        
                      }
                      else if (role == "2")
                      {
                          this.show0 = " style=\"display:True\"";
                          this.show1 = " style=\"display:True\"";
                          //this.show2 = " style=\"display:none\"";
                          this.show3 = " style=\"display:none\"";
                          this.show4 = " style=\"display:True\"";
                          this.show5 = " style=\"display:True\"";
                          this.nav.ID = "nav2";
                         
                      }
                      else if (role == "3")
                      {
                          this.show0 = " style=\"display:True\"";
                          this.show1 = " style=\"display:True\"";
                          //this.show2 = " style=\"display:True\"";
                          this.show3 = " style=\"display:True\"";
                          this.show4 = " style=\"display:True\"";
                          this.show5 = " style=\"display:True\"";
                      }
                      else
                      {
                          this.show0 = " style=\"display:True\"";
                          this.show1 = " style=\"display:none\"";
                          //this.show2 = " style=\"display:none\"";
                          this.show3 = " style=\"display:none\"";
                          this.show4 = " style=\"display:none\"";
                          this.show5 = " style=\"display:True\"";
                          this.nav.ID = "nav3";
                      }
                  }
                  else
                  {
                      this.show0 = " style=\"display:True\"";
                      this.show1 = " style=\"display:none\"";
                      //this.show2 = " style=\"display:none\"";
                      this.show3 = " style=\"display:none\"";
                      this.show4 = " style=\"display:none\"";
                      this.show5 = " style=\"display:True\"";
                      this.nav.ID = "nav3";
                  }
            //}
          
        }


        private void bind_1()
        {
            //this.show3 = " style=\"display:none\"";
            //this.show1 = " style=\"display:True\"";
        }
            
    }
}