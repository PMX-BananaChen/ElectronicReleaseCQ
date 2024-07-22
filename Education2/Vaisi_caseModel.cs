using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Education2
{
    public class Vaisi_caseModel
    {
        public Vaisi_caseModel()
        {
            //
            //TODO: 在此处添加构造函数逻辑
            //
        }
        private int id;

        public int Id
        {
            get { return id; }
            set { id = value; }
        }
        private string title;

        public string Title
        {
            get { return title; }
            set { title = value; }
        }
        private string imgurl;

        public string Imgurl
        {
            get { return imgurl; }
            set { imgurl = value; }
        }
        private string contents;

        public string Contents
        {
            get { return contents; }
            set { contents = value; }
        }
    }
}