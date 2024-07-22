<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Error2.aspx.cs" Inherits="Education2.Error2" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">

<head runat="server">
<title></title>
<style type="text/css">
    body{font-size: 9pt;line-height: 16pt;font-family: "Tahoma" , "宋体";text-decoration: none;}
    table{font-size: 9pt;line-height: 16pt;font-family: "Tahoma" , "宋体";text-decoration: none;}
    td{font-size: 9pt;line-height: 16pt;font-family: "Tahoma" , "宋体";text-decoration: none;}
    body{background-color: #ffffff;}
    a{font-size: 9pt;line-height: 16pt;font-family: "Tahoma" , "宋体";text-decoration: none;}
    a:hover{font-size: 9pt;color: #0188d2;line-height: 16pt;font-family: "Tahoma" , "宋体";text-decoration: underline;}
    h1{font-size: 9pt;font-family: "Tahoma" , "宋体";}
    .auto-style2 {
        width: 424px;
        height: 460px;
    }
    .auto-style4 {
        font-size: large;
    }
    .auto-style5 {
        height: 414px;
        width: 398px;
    }
</style>
</head>
<html xmlns="http://www.w3.org/1999/xhtml">
<body>
    <form id="form1" runat="server">
<table cellspacing="0" width="800px" align="center" border="0" cepadding="0" style="font-size: 9pt;line-height: 16pt;font-family:; height: 527px;"Tahoma" , "宋体";text-decoration: none;">
    <tr>
        <td valign="top" align="middle" class="auto-style2"><img  src="UpdateImg/error.jpg"  border="0" class="auto-style5"></td>
        <td><span style="color:Red;font-weight:bold;" class="auto-style4">
            抱歉！<br />
            您的域賬號未配置進系統請聯繫HR！</span>&nbsp;<br />
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <br />
            <asp:ImageButton ID="ImageButton1" runat="server" Height="68px" ImageUrl="~/UpdateImg/NoPowerIndex.PNG" OnClick="ImageButton1_Click" Width="161px" />
            </td>
    </tr>
    </table>
    </form>
</body>
</html>