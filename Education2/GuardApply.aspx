<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="GuardApply.aspx.cs" Inherits="Education2.GuardApply" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .auto-style2 {
            border-collapse: collapse;
            width: 100%;
            border-color: #dcdcdc;
            border-width: 0;
            height: 35px;
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
            width: 190px;
        }
        .auto-style29 {
            height: 32px;
            text-align: center;
            width: 232px;
        }
        .auto-style51 {
            height: 65px;
            font-size: x-large;
            color: #FF0000;
        }
        .auto-style61 {
            height: 35px;
            text-align: left;
        }
        .auto-style64 {
            height: 35px;
            text-align: center;
            width: 114px;
        }
        .auto-style66 {
            height: 35px;
            text-align: center;
        }
        .auto-style75 {
            width: 153px;
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
        .auto-style80 {
            width: 152px;
            height: 35px;
        }
        .auto-style82 {
            height: 45px;
            text-align: left;
        }
        .auto-style83 {
            height: 35px;
            text-align: center;
            width: 142px;
        }
        .auto-style84 {
            height: 35px;
        }
        .auto-style87 {
            height: 35px;
            text-align: left;
            width: 140px;
        }
        .auto-style88 {
            width: 153px;
            height: 35px;
        }
        .auto-style89 {
            height: 35px;
            text-align: center;
            width: 65px;
        }
        .auto-style90 {
            width: 142px;
            height: 35px;
        }
        .auto-style91 {
            width: 140px;
        }
        .auto-style92 {
            width: 139px;
            height: 35px;
        }
        .auto-style93 {
            height: 35px;
            text-align: center;
            width: 64px;
        }
        .auto-style94 {
            height: 35px;
            text-align: left;
            width: 148px;
        }
        .auto-style95 {
            width: 152px;
            height: 45px;
        }
        .auto-style96 {
            height: 45px;
            text-align: center;
            width: 142px;
        }
        .auto-style97 {
            height: 45px;
            text-align: center;
        }
        .auto-style98 {
            width: 152px;
            height: 9px;
        }
        .auto-style99 {
            height: 9px;
            text-align: center;
            width: 142px;
        }
        .auto-style100 {
            height: 9px;
            text-align: left;
        }
        .auto-style101 {
            height: 9px;
            text-align: center;
        }
    </style>

       <script type="text/javascript" >
        function myfun()
        {
            document.getElementById("qh_con2").style.display = "block";
            document.getElementById("mynav2").className = "nav_on2";
        }
        window.onload = myfun;



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
                if (fd > td && td != "") {
                    alert("選擇的時段不能小於開始時段！");
                    document.getElementById('ContentPlaceHolder1_txttimeend').value = "";
                    return false;
                }
            }
        }

    </script> 
       <link href="Css/btn_style.css" rel="stylesheet" />
     <script type="text/javascript" src="My97DatePicker/WdatePicker.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
      <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <br />
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
    <table cellspacing="1" class="auto-style2">
        <tr>
            <td class="auto-style51" colspan="6" style="text-align: center"><strong>放 行 條 警 衛 補 單</strong></td>
        </tr>
        <tr>
            <td class="auto-style80" ></td>
            <td style="text-align: center" class="auto-style83">工&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 號</td>
            <td class="auto-style94">
                <asp:TextBox ID="txtempno" runat="server" Width="120px" MaxLength="15" AutoPostBack="True" OnTextChanged="txtempno_TextChanged"></asp:TextBox>
            </td>
            <td class="auto-style64">姓&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 名</td>
            <td class="auto-style84">
                <asp:TextBox ID="txtempname" runat="server" Width="120px" MaxLength="15"></asp:TextBox>
            </td>
            <td class="auto-style66"></td>
        </tr>
        <tr>
            <td class="auto-style80"></td>
            <td class="auto-style83">B&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; U</td>
            <td class="auto-style94">
                <span class="auto-style1"><strong>
                <asp:DropDownList ID="ddlFactory" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlFactory2_SelectedIndexChanged" Width="140px">
                </asp:DropDownList>
                </strong></span>
            </td>
            <td class="auto-style64">部&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 門</td>
            <td class="auto-style84" colspan="2">
                <span class="auto-style1"><strong>
                <asp:DropDownList ID="ddlDept" runat="server" Width="250px">
                </asp:DropDownList>
                </strong></span>
            </td>
        </tr>
        </table>
    <table cellspacing="1" class="auto-style2">
        <tr>
            <td class="auto-style75" ></td>
            <td  style="text-align: center" class="auto-style91">郵&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 箱</td>
            <td >
                <asp:TextBox ID="txtmail" runat="server" Height="16px" Width="434px" MaxLength="45"></asp:TextBox>
            </td>
            <td></td>
        </tr>
        </table>
    <table cellspacing="1" class="auto-style2">
        <tr>
            <td class="auto-style88" ></td>
            <td style="text-align: center" class="auto-style90">放行日期</td>
            <td class="auto-style87">
                <input id="txtdatestar" runat="server" aria-checked="undefined" aria-disabled="False" class="Wdate" contenteditable="false" name="Text9" onclick="WdatePicker({ dateFmt: 'yyyyMMdd' })" type="text" visible="True" disabled="disabled" /></td>
            <td class="auto-style89">開始時段</td>
            <td class="auto-style92">
                <input id="txttimestar" runat="server" aria-checked="undefined" aria-disabled="False" class="Wdate" contenteditable="false" name="Text10" onclick="WdatePicker({ dateFmt: 'HHmm' })" type="text" visible="True" /></td>
            <td class="auto-style93">結束時段</td>
            <td class="auto-style61">
                <input id="txttimeend" runat="server" aria-checked="undefined" aria-disabled="False" class="Wdate" contenteditable="false" name="Text12" onclick="WdatePicker({ dateFmt: 'HHmm' }), dateDiffHours()" type="text" visible="True" /></td>
            <td class="auto-style61">&nbsp;</td>
        </tr>
        </table>
    <table cellspacing="1" class="auto-style2">
        <tr>
            <td class="auto-style98" ></td>
            <td style="text-align: center" class="auto-style99"></td>
            <td class="auto-style100">
            </td>
            <td class="auto-style101"></td>
        </tr>
        <tr>
            <td class="auto-style95" ></td>
            <td style="text-align: center" class="auto-style96">出行原因</td>
            <td class="auto-style82">
                <asp:TextBox ID="txtremark" runat="server" Width="438px" Height="47px" TextMode="MultiLine" MaxLength="100"></asp:TextBox>
            </td>
            <td class="auto-style97"></td>
        </tr>
        </table>
    <br />
    <br />
    <table cellspacing="1" class="auto-style2">
        <tr>
            <td class="auto-style23">&nbsp;</td>
            <td class="auto-style28">
                &nbsp;</td>
            <td class="auto-style29">
                <asp:Button ID="btnok" runat="server"  CssClass="Button02" OnClick="Button1_Click" Text=" 提 交 " style="height: 23px" />
            </td>
            <td class="auto-style9">
                <asp:Button ID="btnback" runat="server"  CssClass="Button02" Text=" 重 置 " OnClick="btnback_Click" />
            </td>
            <td class="、">
                &nbsp;</td>
            <td class="auto-style4">&nbsp;</td>
        </tr>
    </table>
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
             </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
