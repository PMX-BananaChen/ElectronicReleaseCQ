using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.OleDb;
using System.Data;
using System.Web.UI.HtmlControls;
using System.Xml;

namespace Education2
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        SqlHelper DA = new SqlHelper();
        
        public string about = "";
        public string case1 = "", case2 = "",case3="";
        public static Vaisi_friendModel friend = null, friend1 = null, friend2 = null, friend3 = null, friend4 = null, friend5 = null;

        protected void Page_Load(object sender, EventArgs e)
        {
            bind_1();
            bind_2();
            //bind_3();
            bind_5();
            bind_9();
            bind_10();


        }
        private void bind_5()
        {
            string sql = "select * from vaisi_friend where id=5";
            OleDbDataReader reader = DA.GetScalarList(sql);
            if (reader.Read())
            {
                friend4 = new Vaisi_friendModel();
                friend4.Imgurl = (string)reader["imgurl"];
                friend4.Htmlurl = (string)reader["htmlurl"];
                reader.Close();
            }
            string sql1 = "select * from vaisi_friend where id=6";
            OleDbDataReader reader1 = DA.GetScalarList(sql1);
            if (reader1.Read())
            {
                friend5 = new Vaisi_friendModel();
                friend5.Imgurl = (string)reader1["imgurl"];
                friend5.Htmlurl = (string)reader1["htmlurl"];
                reader1.Close();
            }
        }

        // 顯示Table圖
        //private void bind_3()
        //{
        //    string sql = "select [description] from vaisi_main where id=2";
        //    OleDbDataReader reader = DA.GetScalarList(sql);
        //    Vaisi_mainModel sb = null;
        //    if (reader.Read())
        //    {
        //        sb = new Vaisi_mainModel();
        //        sb.Description = (string)reader["description"];
        //        reader.Close();
        //    }
        //    if (sb.Description == "1")//显示
        //    {
        //        string sql2 = "select * from vaisi_friend where type=3";
        //        IList<Vaisi_friendModel> list = new List<Vaisi_friendModel>();
        //        DataTable table = SqlHelper.GetScalarListTable(sql2);
        //        foreach (DataRow row in table.Rows)
        //        {
        //            Vaisi_friendModel fd = new Vaisi_friendModel();
        //            fd.Id = Convert.ToInt32(row["id"]);
        //            fd.Type = Convert.ToInt32(row["type"]);
        //            fd.Title = Convert.ToString(row["title"]);
        //            fd.Imgurl = Convert.ToString(row["imgurl"]);
        //            fd.Htmlurl = Convert.ToString(row["htmlurl"]);
        //            list.Add(fd);
        //        }
        //        for (int i = 0; i < list.Count; i++)
        //        {
        //            Vaisi_friendModel fdd = null;
        //            fdd = new Vaisi_friendModel();
        //            fdd = list[i];
        //            Literal li = new Literal();
        //            li.Text = string.Format("<li><a href='" + fdd.Htmlurl.ToString() + "' target='_blank'><img src='" + fdd.Imgurl.ToString() + "' alt='爱斯网络' /></a></li>");
        //            this.addli1.Controls.Add((Control)li);
        //        }

        //        string sql3 = "select * from vaisi_friend where type=4";
        //        IList<Vaisi_friendModel> list1 = new List<Vaisi_friendModel>();
        //        DataTable table1 = SqlHelper.GetScalarListTable(sql3);
        //        foreach (DataRow row in table1.Rows)
        //        {
        //            Vaisi_friendModel fd1 = new Vaisi_friendModel();
        //            fd1.Id = Convert.ToInt32(row["id"]);
        //            fd1.Type = Convert.ToInt32(row["type"]);
        //            fd1.Title = Convert.ToString(row["title"]);
        //            fd1.Imgurl = Convert.ToString(row["imgurl"]);
        //            fd1.Htmlurl = Convert.ToString(row["htmlurl"]);
        //            list1.Add(fd1);
        //        }
        //        for (int i = 0; i < list1.Count; i++)
        //        {
        //            Vaisi_friendModel fdd1 = null;
        //            fdd1 = new Vaisi_friendModel();
        //            fdd1 = list1[i];
        //            Literal li = new Literal();
        //            li.Text = string.Format("<li><a href='" + fdd1.Htmlurl.ToString() + "' target='_blank'><img src='" + fdd1.Imgurl.ToString() + "' alt='爱斯网络' /></a></li>");
        //            this.addli2.Controls.Add((Control)li);
        //        }

        //    }
        //    else if (sb.Description == "0")//隐藏
        //    {
        //        this.container_6.Visible = false;
        //    }
        //}

        private void bind_1()
        {
            string sql = "select * from vaisi_friend where id=1";
            OleDbDataReader reader = DA.GetScalarList(sql);
            if (reader.Read())
            {
                friend = new Vaisi_friendModel();
                friend.Imgurl = (string)reader["imgurl"];
                friend.Htmlurl = (string)reader["htmlurl"];
                reader.Close();
            }
            string sql2 = "select * from vaisi_friend where id=2";
            OleDbDataReader reader2 = DA.GetScalarList(sql2);
            if (reader2.Read())
            {
                friend1 = new Vaisi_friendModel();
                friend1.Imgurl = (string)reader2["imgurl"];
                friend1.Htmlurl = (string)reader2["htmlurl"];
                reader2.Close();
            }
            string sql3 = "select * from vaisi_friend where id=3";
            OleDbDataReader reader3 = DA.GetScalarList(sql3);
            if (reader3.Read())
            {
                friend2 = new Vaisi_friendModel();
                friend2.Imgurl = (string)reader3["imgurl"];
                friend2.Htmlurl = (string)reader3["htmlurl"];
                reader3.Close();
            }
            string sql4 = "select * from vaisi_friend where id=4";
            OleDbDataReader reader4 = DA.GetScalarList(sql4);
            if (reader4.Read())
            {
                friend3 = new Vaisi_friendModel();
                friend3.Imgurl = (string)reader4["imgurl"];
                friend3.Htmlurl = (string)reader4["htmlurl"];
                reader4.Close();
            }
        }

        private void bind_2()
        {
            string sql = "select abouts from vaisi_main where id=2 ";
            OleDbDataReader reader = DA.GetScalarList(sql);
            if (reader.Read())
            {
                about = (string)reader["abouts"];
                if (about.Length > 150)
                {
                    about = about.Substring(0, 150) + "......";
                }
                reader.Close();
            }
        }
        //private void bind_10()
        //{
        //    string sql = "select * from vaisi_friend where type=3";

        //    IList<Vaisi_friendModel> list = new List<Vaisi_friendModel>();



        //    DataTable table = SqlHelper.GetScalarListTable(sql);
        //    foreach (DataRow row in table.Rows)
        //    {
        //        Vaisi_friendModel nw = new Vaisi_friendModel();

        //        nw.Id = Convert.ToInt32(row["id"]);
        //        nw.Type = Convert.ToInt32(row["type"]);
        //        nw.Title = Convert.ToString(row["title"]);
        //        nw.Imgurl = Convert.ToString(row["imgurl"]);
        //        nw.Htmlurl = Convert.ToString(row["htmlurl"]);

        //        //nw.Contents = Convert.ToString(row["contents"]);
        //        //if (nw.Contents.Length > 62)
        //        //{
        //        //    nw.Contents = nw.Contents.Substring(0, 62) + "...";
        //        //}
        //        list.Add(nw);
        //    }
        //    for (int i = 0; i < list.Count; i++)
        //    {
        //        Vaisi_friendModel nww = null;
        //        nww = new Vaisi_friendModel();
        //        nww = list[i];

        //        if (i <= 4)
        //        {
        //            Literal li = new Literal();
        //            li.Text = string.Format("<a href='" + nww.Htmlurl.ToString() + "' target='_blank'  ><img src='" + nww.Imgurl + "' width='100' height='110' border='0' alt='" + nww.Title + "'  /> </a>");
        //            this.demo1_2.Controls.Add((Control)li);
        //        }
        //        else
        //        {
        //            Literal li = new Literal();
        //            li.Text = string.Format("<a href='" + nww.Htmlurl.ToString() + "' target='_blank'><img src='" + nww.Imgurl + "' width='100' height='110' border='0' alt='" + nww.Title + "' /></a>");
        //            this.demo1_2.Controls.Add((Control)li);
        //        }
        //    }
        //}

        private void bind_9()
        {
            string sql = "select top 10 * from vaisi_case order by id desc";
            IList<Vaisi_caseModel> list = new List<Vaisi_caseModel>();
            DataTable table = SqlHelper.GetScalarListTable(sql);
            foreach (DataRow row in table.Rows)
            {
                Vaisi_caseModel nw = new Vaisi_caseModel();
                nw.Id = Convert.ToInt32(row["id"]);
                nw.Title = Convert.ToString(row["title"]);
                nw.Imgurl = Convert.ToString(row["imgurl"]);
                nw.Contents = Convert.ToString(row["contents"]);
                if (nw.Contents.Length > 62)
                {
                    nw.Contents = nw.Contents.Substring(0, 62) + "...";
                }
                list.Add(nw);
            }
            for (int i = 0; i < list.Count; i++)
            {
                Vaisi_caseModel nww = null;
                nww = new Vaisi_caseModel();
                nww = list[i];
                if (i == 0)
                {
                    case1 = nww.Contents;
                }
                else if (i == 1)
                {
                    case2 = nww.Contents;
                }
                if (i <= 4)
                {
                    Literal li = new Literal();
                    li.Text = string.Format("<a href='#'><img src='" + nww.Imgurl + "' width='150' height='94' border='0' alt='" + nww.Title + "' /></a>");
                    this.demo1.Controls.Add((Control)li);
                }
                else
                {
                    Literal li = new Literal();
                    li.Text = string.Format("<a href='#'><img src='" + nww.Imgurl + "' width='150' height='94' border='0' alt='" + nww.Title + "' /></a>");
                    this.demo1_1.Controls.Add((Control)li);
                }
            }
        }


        private void bind_10()
        {
            string sql = "select * from vaisi_friend where type=3";

            IList<Vaisi_friendModel> list = new List<Vaisi_friendModel>();

          
            DataTable table = SqlHelper.GetScalarListTable(sql);
            foreach (DataRow row in table.Rows)
            {
                Vaisi_friendModel nw = new Vaisi_friendModel();
                nw.Id = Convert.ToInt32(row["id"]);
                nw.Type = Convert.ToInt32(row["type"]);
                nw.Title = Convert.ToString(row["title"]);
                nw.Imgurl = Convert.ToString(row["imgurl"]);
                nw.Htmlurl = Convert.ToString(row["htmlurl"]);
               
                list.Add(nw);
            }
            for (int i = 0; i < list.Count; i++)
            {
                Vaisi_friendModel nww = null;
                nww = new Vaisi_friendModel();
                nww = list[i];
                
                if (i <= 4)
                {
                    Literal li = new Literal();
                    li.Text = string.Format("<a href='"+nww.Htmlurl.ToString()+"' target='_blank'><img src='" + nww.Imgurl + "' width='112' height='100' border='0' alt='" + nww.Title + "' /></a>");
                    this.demo1_2.Controls.Add((Control)li);
                }
                else
                {
                    Literal li = new Literal();
                    li.Text = string.Format("<a href='"+nww.Htmlurl.ToString()+"' target='_blank'><img src='" + nww.Imgurl + "' width='112' height='100' border='0' alt='" + nww.Title + "' /></a>");
                    this.demo1_2.Controls.Add((Control)li);
                }
            }
        }
    }

    
}