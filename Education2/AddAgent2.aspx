<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="AddAgent2.aspx.cs" Inherits="Education2.AddAgent2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .auto-style2 {
            border-collapse: collapse;
            width: 100%;
            border-color: #dcdcdc;
            border-width: 0;
            height: 70px;
        }
        .auto-style4 {
            height: 32px;
            text-align: center;
        }
        .auto-style9 {
            width: 319px;
        }
        .auto-style23 {
            height: 32px;
            text-align: center;
            width: 126px;
        }
        .auto-style28 {
            height: 32px;
            text-align: center;
            width: 182px;
        }
        .auto-style29 {
            height: 32px;
            text-align: center;
            width: 232px;
        }
        .auto-style51 {
            height: 26px;
            font-size: x-large;
            color: #FF0000;
        }
        #txtdatestar {
            width: 118px;
        }
        #txttimestar {
            width: 118px;
        }
        #txtdateend {
            width: 118px;
        }
        #txttimeend {
            width: 118px;
        }
        .auto-style95 {
            background: url('WebImg/main_61.gif') repeat-x 50% center;
            border: 1px solid #74BFE7;
            color: White;
            padding: 2px;
            font-size: small;
        }
        .auto-style96 {
            width: 100%;
            height: 35px;
        }
        .auto-style98 {
            text-align: center;
            width: 178px;
            font-size: small;
        }



        #txtdatestar0 {
            width: 118px;
        }
        #txtdateend0 {
            width: 118px;
        }
        .auto-style101 {
            width: 163px;
        }
        .auto-style103 {
            width: 108px;
            font-size: small;
        }
        .auto-style108 {
            width: 172px;
        }
        .auto-style110 {
            text-align: center;
            width: 140px;
            font-size: small;
        }
        .auto-style111 {
            font-size: small;
        }
        .auto-style112 {
            width: 58px;
        }
        .auto-style113 {
            width: 90px;
        }
        .auto-style114 {
            width: 782px;
        }
        #txttimestar0 {
            width: 118px;
        }
        #txttimeend0 {
            width: 118px;
        }
        .auto-style115 {
            width: 135px;
        }
        </style>

    

      <script type="text/javascript" >
          function myfun() {
              document.getElementById("qh_con3").style.display = "block";
              document.getElementById("mynav3").className = "nav_on2";
          }
          window.onload = myfun;

    </script> 
     <link href="Css/btn_style.css" rel="stylesheet" />
     <script type="text/javascript" src="My97DatePicker/WdatePicker.js"></script>
    <script language="JavaScript" type="text/javascript">


        function dateDiff() {

            var formDate = document.getElementById('ContentPlaceHolder1_txtdatestar').value;
            var toDate = document.getElementById('ContentPlaceHolder1_txtdateend').value;

            if (formDate == "") {

                alert("選擇的日期時間段無效");

                return false;

            }
            else {


                var fd = new Date(formDate.replace(/^(\d{4})(\d{2})(\d{2})$/, "$1/$2/$3"));

                var td = new Date(toDate.replace(/^(\d{4})(\d{2})(\d{2})$/, "$1/$2/$3"));


                //var fd = new Date(formDate); //new Date($("#txt_from").val());

                //var td = new Date(Date.parse(toDate.replace(/-/g, "/"))); //new Date($("#txt_to").val());


                var dataPoor = td.getTime() - fd.getTime();

                var days = parseInt(dataPoor / parseInt(24 * 3600 * 1000));

                if (fd > td) {
                    alert("選擇的日期時間段不能小於開始日期！");
                    document.getElementById('ContentPlaceHolder1_txtdateend').value = "";
                    return false;
                }
                else if (days > 90) {

                    alert("選擇的日期時間段不能超過90天！");
                    document.getElementById('ContentPlaceHolder1_txtdateend').value = "";
                    return false;
                }
            }
        }

        function dateDiffHours() {
            var formtime = document.getElementById('ContentPlaceHolder1_txttimestar').value;
            var totime = document.getElementById('ContentPlaceHolder1_txttimeend').value;

            if (formtime == "") {

                alert("選擇的時間段無效");

                return false;

            }
            else {
               
                var fd = Number(formtime.replace(":", ""));

                var td = Number(totime.replace(":", ""));


                if (document.getElementById("ContentPlaceHolder1_ckbox").checked) {
                    if (fd <= td && td != "") {
                        alert("跨夜狀態選擇的時段不能大於開始時段！");
                        document.getElementById('ContentPlaceHolder1_txttimeend').value = "";
                        return false;
                    }
                }


                else if (fd >= td && td != "") {
                    alert("選擇的時段不能小於開始時段！");
                    document.getElementById('ContentPlaceHolder1_txttimeend').value = "";
                    return false;
                }
            }
        }

        //function KeyDown()
        //{
        //    if (event.keyCode == 13)
        //    {
        //        alert("test！");
        //    }
        //}


</script> 


 

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <br />
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>

    
    <table cellspacing="1" class="auto-style2">
        <tr>
            <td class="auto-style51" style="text-align: center">
                <strong>代 理 管 理</strong></td>
        </tr>
        </table>
    <table cellpadding="0" cellspacing="0" class="auto-style96">
        <tr>
            <td class="auto-style103">&nbsp;</td>
            <td class="auto-style98">代理主管工號</td>
            <td class="auto-style108">
                <asp:TextBox ID="txtDLempno" runat="server" Width="140px" MaxLength="15" AutoPostBack="True" OnTextChanged="txtempno_TextChanged" CssClass="auto-style111"></asp:TextBox>
            </td>
            <td class="auto-style110">代理主管姓名</td>
            <td class="auto-style101">
                <asp:TextBox ID="txtDLempname" runat="server" Width="140px" MaxLength="15" CssClass="auto-style111"></asp:TextBox>
            </td>
            <td>
                &nbsp;</td>
        </tr>
    </table>
    <br class="auto-style111" />
    <table cellpadding="0" cellspacing="0" class="auto-style96">
        <tr>
            <td class="auto-style103">&nbsp;</td>
            <td class="auto-style98">代理主管郵箱</td>
            <td>
                <asp:TextBox ID="txtDLmail" runat="server" Height="16px" Width="456px" MaxLength="45" Enabled="False" CssClass="auto-style111"></asp:TextBox>
            </td>
        </tr>
    </table>
             <br class="auto-style111" />
    <table cellpadding="0" cellspacing="0" class="auto-style96">
        <tr>
            <td class="auto-style103">&nbsp;</td>
            <td class="auto-style98">開始日期</td>
            <td class="auto-style108">
                <input id="txtdatestar" runat="server" name="Text9"  onfocus="WdatePicker({ minDate:'%y-%M-%d',maxDate:'%y-%M-#{%d+90}',dateFmt: 'yyyyMMdd'  })" type="text" style="width: 140px" class="auto-style111" />
            </td>
            <td class="auto-style110">結束日期</td>
            <td class="auto-style101">
                <input id="txtdateend" runat="server" name="Text11" onclick="WdatePicker({ minDate: '%y-%M-%d', dateFmt: 'yyyyMMdd' }), dateDiff()"  type="text"  style="width: 140px" class="auto-style111" />
            </td>
            <td class="auto-style115">
               
                <asp:CheckBox ID="ckbox" runat="server" Text="是否跨夜" OnCheckedChanged="ckbox_CheckedChanged" AutoPostBack="True" />
               
            </td>
            <td>&nbsp;</td>
        </tr>
    </table>
    <br class="auto-style111" />
    <table cellpadding="0" cellspacing="0" class="auto-style96">
        <tr>
            <td class="auto-style103">&nbsp;</td>
            <td class="auto-style98">開始時段</td>
            <td class="auto-style108">
                <input id="txttimestar" runat="server"  class="auto-style111" name="Text10" onclick="WdatePicker({ dateFmt: 'HHmm' })" type="text"  style="width: 140px" /></td>
            <td class="auto-style110">結束時段</td>
            <td class="auto-style101">
                <input id="txttimeend" runat="server"  class="auto-style111"  name="Text12" onclick="WdatePicker({ dateFmt: 'HHmm' }), dateDiffHours()" type="text"  style="width: 140px" /></td>
            <td class="auto-style112">
                <asp:Button ID="btnadd" runat="server" CssClass="auto-style95"  OnClick="btnadd_Click" Text=" Add " style="height: 26px" />
            </td>
            <td><strong>單筆不能超過90天或90條</strong></td>
        </tr>
    </table>
    <br />
    <table cellspacing="1" class="auto-style2">
        <tr>
            <td class="auto-style113">
                <br />
                <br />
            </td>
            <td class="auto-style114">
                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" BorderColor="Silver" BorderStyle="Solid" DataKeyNames="ID" OnRowCancelingEdit="GridView1_RowCancelingEdit" OnRowDataBound="GridView1_RowDataBound" OnRowDeleting="GridView1_RowDeleting" OnRowEditing="GridView1_RowEditing" OnRowUpdating="GridView1_RowUpdating" style="margin-right: 0px" Width="808px">
                    <Columns>
                         <asp:BoundField DataField="" HeaderText="">
                        <HeaderStyle BorderColor="Silver" HorizontalAlign="Center" Width="30px" />
                        <ItemStyle BorderColor="Silver" HorizontalAlign="Center" />
                        </asp:BoundField>

                        <asp:BoundField DataField="DLNO" HeaderText="代理工號">
                        <HeaderStyle BorderColor="Silver" HorizontalAlign="Center" Width="100px" />
                        <ItemStyle BorderColor="Silver" HorizontalAlign="Center" />
                        </asp:BoundField>
                         <asp:BoundField DataField="DLName" HeaderText="代理姓名">
                        <HeaderStyle BorderColor="Silver" HorizontalAlign="Center" Width="100px" />
                        <ItemStyle BorderColor="Silver" HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="DLMail" HeaderText="代理郵箱">
                        <HeaderStyle BorderColor="Silver" HorizontalAlign="Center" Width="160px" />
                        <ItemStyle BorderColor="Silver" HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="Date" HeaderText="日期">
                        <HeaderStyle BorderColor="Silver" Height="30px" HorizontalAlign="Center" VerticalAlign="Middle" Width="110px" />
                        <ItemStyle BorderColor="Silver" Height="30px" HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="Star" HeaderText="開始時段">
                        <HeaderStyle BorderColor="Silver" HorizontalAlign="Center" Width="120px" />
                        <ItemStyle BorderColor="Silver" HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="End" HeaderText="結束時段">
                        <HeaderStyle BorderColor="Silver" HorizontalAlign="Center" Width="120px" />
                        <ItemStyle BorderColor="Silver" HorizontalAlign="Center" Width="120px" />
                        </asp:BoundField>
                        <asp:CommandField DeleteText="刪除" HeaderText="刪除" ShowDeleteButton="True">
                        <HeaderStyle BorderColor="Silver" HorizontalAlign="Center" Width="100px" />
                        <ItemStyle BorderColor="Silver" HorizontalAlign="Center" Width="100px" />
                        <%--  <ItemStyle HorizontalAlign="Center" />--%></asp:CommandField>
                    </Columns>
                </asp:GridView>
            </td>
            <td>&nbsp;</td>
        </tr>
        </table>
    <table cellspacing="1" class="auto-style2">
        <tr>
            <td class="auto-style23">&nbsp;&nbsp;</td>
            <td class="auto-style28">
                &nbsp;</td>
            <td class="auto-style29">
                <asp:Button ID="btnok" runat="server"  CssClass="Button02" OnClick="Button1_Click" Text=" 提 交 " Visible="False" />
            </td>
            <td class="auto-style9">
                <asp:Button ID="btnback" runat="server"   CssClass="Button02" Text=" 重 置 " OnClick="btnback_Click" Visible="False" />
            </td>
            <td class="、">
                &nbsp;</td>
            <td class="auto-style4">&nbsp;</td>
        </tr>
    </table>
    </ContentTemplate>
    </asp:UpdatePanel>
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
       
</asp:Content>
