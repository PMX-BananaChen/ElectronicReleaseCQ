<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="User.aspx.cs" Inherits="Education2.User" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
    .auto-style4 {
        height: 40px;
    }




    /*.Button02
{
	background: url(../WebImg/main_61.gif) repeat-x 50% center;
	height: 20px;
	border: 1px solid #74BFE7;
	color: White;
	padding: 2px;
}*/

        .auto-style5 {
            height: 40px;
            width: 117px;
        }
        .auto-style7 {
            height: 40px;
            width: 56px;
        }
        .auto-style8 {
            width: 42%;
            height: 40px;
        }
        .auto-style9 {
            width: 46%;
            height: 40px;
        }

    </style>

 <link href="Css/btn_style.css" rel="stylesheet" />

       <script type="text/javascript" >
        function myfun()
        {
            document.getElementById("qh_con3").style.display = "block";
            document.getElementById("mynav3").className = "nav_on2";
        }
        window.onload = myfun;

    </script> 

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
 <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <br />
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
    <p>
    <br />
</p>
<div>
    <br />
    <br />
    <table border="0" cellpadding="0" cellspacing="0" height="25" width="100%">
        <tr>
            <td align="right" class="auto-style8">工 號：</td>
            <td class="auto-style4">
                <asp:TextBox ID="txtempno" runat="server" Width="150px" OnTextChanged="txtempno_TextChanged" AutoPostBack="True"></asp:TextBox>
                <font color="#FF0000">*</font></td>
        </tr>
        <tr>
            <td align="right" class="auto-style8">姓 名：</td>
            <td class="auto-style4">
                <asp:TextBox ID="txtname" runat="server" Width="150px" Enabled="False"></asp:TextBox>
                <font color="#FF0000">*</font></td>
        </tr>
        <tr>
            <td align="right" class="auto-style8">密 碼：</td>
            <td class="auto-style4">
                <asp:TextBox ID="txtpass" runat="server" Width="150px"></asp:TextBox>
                <font color="#FF0000">*</font></td>
        </tr>
        <tr>
            <td align="right" class="auto-style8">郵 箱：</td>
            <td class="auto-style4">
                <asp:TextBox ID="txtmail" runat="server" Width="150px"></asp:TextBox>
                <font color="#FF0000">*</font></td>
        </tr>
        <tr>
            <td align="right" class="auto-style8">權 限：</td>
            <td class="auto-style4">
                <asp:DropDownList ID="ddlrole" runat="server" Width="155px">
                </asp:DropDownList>
                <font color="#FF0000">*</font></td>
        </tr>
        </table>
</div>
<br />
    <table border="0" cellpadding="0" cellspacing="0" height="25" width="100%">
        <tr>
            <td align="right" class="auto-style9">
                <asp:Button ID="QuickSearchButton2" runat="server" CssClass="Button02"  Text=" 新 增 " ValidationGroup="qc" OnClick="QuickSearchButton_Click" />
            </td>
            <td class="auto-style7">
                &nbsp;</td>
            <td class="auto-style5">
                <asp:Button ID="QuickSearchButton1" runat="server" CssClass="Button02"  Text=" 取 消  " ValidationGroup="qc" OnClick="QuickSearchButton_Click" />
            </td>
            <td class="auto-style4">
                &nbsp;</td>
        </tr>
        </table>
<br />
<br />
     </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
