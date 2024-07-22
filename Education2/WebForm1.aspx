<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="Education2.WebForm1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
   
    <script src="http://tjs.sjs.sinajs.cn/open/api/js/wb.js" type="text/javascript" charset="utf-8"></script>
   
    


    <div id="show_pro_img">
    </div>
    <div class="container_1">
        <div class="centert_one_1">
            <a href="http://www.baidu.com" target="_blank">
            <img alt="爱斯网络" src="WebImg/one_block_1.png" /> </a>
        </div>
        <div class="centert_one_1">
            <a href="http://www.baidu.com" target="_blank">
            <img alt="爱斯网络" src="WebImg/one_block_2.png" /></a>
        </div>
        <div class="centert_one_1">
            <a href="http://www.baidu.com" target="_blank">
            <img alt="爱斯网络" src="WebImg/one_block_3.png" /></a>
        </div>
        <div class="centert_one_2">
            <a href="http://www.baidu.com" target="_blank">
            <img alt="爱斯网络" src="WebImg/one_block_4.png" /></a>
        </div>
    </div>
     <div class="container_2">
        <div class="centert_two_1">
            <span class="yinying">公司简介</span><br />
            <p>
                        <%=about %>
                    </p>
            <div class="more">
                <a href="http://localhost:19317/About.aspx">more...</a>
            </div>
        </div>
        <div class="centert_two_1">
            <div class="centert_two_1_1">
                <div id="demo" class="demo">
                    <div id="indemo" class="indemo">
                        <div id="demo1" class="demo1" runat="server" >
                        </div>
                        <div id="demo2" class="demo2">
                        </div>
                    </div>
                </div>
            </div>
            <div class="centert_two_1_2">
                <p>
                            <%=case1 %>&gt;
                </p>
            </div>
            <div class="more">
                <a href="#">more...</a>
            </div>
        </div>
        <div class="centert_two_1">
            <div class="centert_two_1_1">
                <div id="demo_1" class="demo">
                    <div id="indemo_1" class="indemo">
                        <div id="demo1_1" runat="server" class="demo1">
                        </div>
                        <div id="demo2_1" class="demo2">
                        </div>
                    </div>
                </div>
            </div>
            <div class="centert_two_1_2">
                <p>
                            <%=case2 %>
                        </p>
            </div>
            <div class="more">
                <a href="#">more...</a>
            </div>
        </div>
    </div>
    <div class="clear">
            </div>

    <div class="container_2">
        <div class="centert_two_2">
             <div id="demo_2" class="demo3">
                    <div id="indemo_2" class="indemo">
                        <div id="demo1_2" class="demo1" runat="server" >
                        </div>
                        <div id="demo2_2" class="demo2">
                        </div>
                    </div>
                </div>
        </div>
         <div class="centert_two_1_2">
                <p>
                            <%=case3 %>
                </p>
            </div>
       
    </div>

   
  
            <div class="container_3">
                <div class="centert_three_1">
                    <span class="yinying">新闻中心</span><br />
                    <ul id="addli3" runat="server">
                    </ul>
                    <div class="more">
                        <a href="#">more...</a>
                    </div>
                </div>
                <div class="centert_three_2">
                    <div class="centert_three_2_1">
                        <a href="<%=friend4.Htmlurl %>" target="_blank">
                            <img src="<%=friend4.Imgurl %>" alt="爱斯网络" /></a>
                    </div>
                    <div class="centert_three_2_1">
                        <a href="<%=friend5.Htmlurl %>" target="_blank">
                            <img src="<%=friend5.Imgurl %>" alt="爱斯网络" /></a>
                    </div>
                </div>
            </div>
            <div class="clear">
            </div>
            <div class="container_4">
                <b class="b1"></b><b class="b2 d1"></b><b class="b3 d1"></b><b class="b4 d1"></b>
                <div class="b d1 k">
                    <div class="container_4_1">
                        <img src="UpdateImg/1_83.gif" alt="爱斯网络战略合作伙伴" />
                        <dl>
                            <dd id="addli4" runat="server">
                            </dd>
                        </dl>
                    </div>
                </div>
                <b class="b4b d1"></b><b class="b3b d1"></b><b class="b2b d1"></b><b class="b1b">
                </b>
            </div>
            <div class="clear">
            </div>
            <div class="container_5">
                <b class="b1"></b><b class="b2 d1"></b><b class="b3 d1"></b><b class="b4 d1"></b>
                <div class="b d1 k">
                    <div class="container_5_1">
                        <span class="yinying">友情链接</span><br />
                        <hr />
                        <ul id="addli5" runat="server">
                        </ul>
                    </div>
                </div>
                <b class="b4b d1"></b><b class="b3b d1"></b><b class="b2b d1"></b><b class="b1b">
                </b>
            </div>
   
    

    <script type="text/javascript">
        var cacheBuster = "?t=" + Date.parse(new Date());
        var stageW = 1000; //"100%";
        var stageH = 300; //"100%";
        var attributes = {};
        attributes.id = 'show_pro_img';
        attributes.name = attributes.id;
        var params = {};
        params.bgcolor = "#ffffff";
        var flashvars = {};
        flashvars.componentWidth = stageW;
        flashvars.componentHeight = stageH;
        flashvars.pathToFiles = "WebImg/Img/";
        flashvars.xmlPath = "lrtk.xml";
        swfobject.embedSWF("WebImg/Img/preview.swf" + cacheBuster, attributes.id, stageW, stageH, "9.0.124", "WebImg/Img//expressInstall.swf", flashvars, params);

        var speed = 50;
        var tab = document.getElementById("demo");
        var tab1 = document.getElementById("ContentPlaceHolder1_demo1");
        var tab2 = document.getElementById("demo2");
        tab2.innerHTML = tab1.innerHTML;

        var tabb = document.getElementById("demo_1");
        var tabb1 = document.getElementById("ContentPlaceHolder1_demo1_1");
        var tabb2 = document.getElementById("demo2_1");
        tabb2.innerHTML = tabb1.innerHTML;

        var tabbb = document.getElementById("demo_2");
        var tabbb1 = document.getElementById("ContentPlaceHolder1_demo1_2");
        var tabbb2 = document.getElementById("demo2_2");
        tabbb2.innerHTML = tabbb1.innerHTML;

        function Marquee() {
            if (tab2.offsetWidth - tab.scrollLeft <= 0)
                tab.scrollLeft -= tab1.offsetWidth
            else {
                tab.scrollLeft++;
            }

                if (tabb2.offsetWidth - tabb.scrollLeft <= 0)
                    tabb.scrollLeft -= tabb1.offsetWidth
                else {
                    tabb.scrollLeft++;
                }

                if (tabbb2.offsetWidth - tabbb.scrollLeft <= 0)
                    tabbb.scrollLeft -= tabbb1.offsetWidth
                else {
                    tabbb.scrollLeft++;
                }
        }
        var MyMar = setInterval(Marquee, speed);
        tab.onmouseover = function () { clearInterval(MyMar) };
        tabb.onmouseover = function () { clearInterval(MyMar) };
        tabbb.onmouseover = function () { clearInterval(MyMar) };
        tab.onmouseout = function () { MyMar = setInterval(Marquee, speed) };
        tabb.onmouseout = function () { MyMar = setInterval(Marquee, speed) };
        tabbb.onmouseout = function () { MyMar = setInterval(Marquee, speed) };

    </script>

</asp:Content>
