using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Education2
{
    public class Vaisi_friendModel
    {
        public Vaisi_friendModel()
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
        private int type;

        public int Type
        {
            get { return type; }
            set { type = value; }
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
        private string htmlurl;

        public string Htmlurl
        {
            get { return htmlurl; }
            set { htmlurl = value; }
        }
        private int number;

        public int Number
        {
            get { return number; }
            set { number = value; }
        }
    }
}