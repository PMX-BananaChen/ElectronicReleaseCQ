using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Education2
{
    public class Vaisi_mainModel
    {
        public Vaisi_mainModel()
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
        private string keywords;

        public string Keywords
        {
            get { return keywords; }
            set { keywords = value; }
        }
        private string description;

        public string Description
        {
            get { return description; }
            set { description = value; }
        }
        private string abouts;

        public string Abouts
        {
            get { return abouts; }
            set { abouts = value; }
        }
    }
}