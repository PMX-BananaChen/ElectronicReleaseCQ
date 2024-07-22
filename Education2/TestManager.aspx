<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TestManager.aspx.cs" Inherits="Education2.TestManager" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">

<head runat="server">
    <title></title>
    <style type="text/css" >

        .style1
        {
            height: 36px;
        }
        .auto-style19 {
            height: 20px;
            color: #FF0000;
            font-size: small;
            text-align: left;
        }
        .auto-style32 {
            width: 200px;
        }
        .auto-style33 {
            width: 605px;
            height: 220px;
        }
        .auto-style34 {
            height: 33px;
        }
        .auto-style35 {
            font-size: medium;
        }
        .textbox{background:#FFFFFF url('/WebImg/login_6.gif') repeat-x; 
border:solid 1px #27B3FE; }
         .buttons
    {
        background: url(../WebImg/btn_bg.gif);
        width: 125px;
        height: 36px;
        font-size: 14px;
        font-weight: bold;
        color: #fff;
        border:none; 

    }

        .btnBox {
    margin: 30px 0 0 50px;
    height:36px;
}


        .auto-style42 {
        }
        .auto-style44 {
            width: 67px;
            height: 21px;
        }
        .auto-style45 {
            height: 21px;
            width: 315px;
        }
        

        .auto-style47 {
            width: 67px;
            height: 37px;
            font-size: medium;
            color: #003366;
        }
        .auto-style48 {
            width: 61px;
            height: 37px;
        }
        .auto-style49 {
            height: 37px;
            width: 315px;
        }


        #from1 {
            height: 501px;
        }


        .auto-style50 {
            width: 61px;
            height: 21px;
        }


        </style>
</head>
<body style=" background-image: url('WebImg/bodyback.jpg')">

    <form id="from1" runat="server">
      <div>
      <table border="0" style="border-color: #008080; width: 500px; height: 97px; "
            cellspacing="0" cellpadding="0">
    <tr>

    <td class="auto-style32">
        &nbsp;</td>
    <td  style="width: 300px">
        <asp:Image ID="Image1" runat="server" ImageUrl="~/WebImg/logo4.png" />
        </td>
    </tr>
    </table>
  </div>

    <center>
    <div style="height: 413px; width:100%; vertical-align:middle; "  >
    <br />   
       
            <ContentTemplate>
                <table border="0" cellpadding="0" cellspacing="0" style="border: 1px solid #C0C0C0; width: 545px; height: 366px; background-color: #f0f5f8;">
                    <tr>
                        <td align="center" class="auto-style33">
                            <table cellpadding="0" style="border-bottom: 1px solid #C0C0C0; height: 40px; width: 506px;">
                                <tr>
                                    <td class="auto-style34" style="text-align: left"><strong style="text-align: left;"><span class="auto-style35">部門主管驗證</span> </strong></td>
                                </tr>
                            </table>
      <br />
                            <table border="0" cellpadding="0" cellspacing="0" style=" width: 507px; height: 194px;">
                                <tr>
                                    <td class="auto-style19" colspan="4"><strong>&nbsp;&nbsp;&nbsp;&nbsp; </strong></td>
                                </tr>
                                <tr>
                                    <td class="auto-style48"></td>
                                    <td class="auto-style47">工 號:</td>
                                    <td class="auto-style49">
                                        <asp:TextBox ID="txtempno" runat="server" AutoPostBack="True" CssClass="textbox" Height="18px" OnTextChanged="txtempno_TextChanged" Width="245px"></asp:TextBox>
                                    </td>
                                    <td align="left" class="auto-style42" rowspan="8">&nbsp;<asp:Panel ID="Panel1" runat="server">
                                        </asp:Panel>
                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </td>
                                </tr>
                                <tr>
                                    <td class="auto-style48"></td>
                                    <td class="auto-style47">姓 名:</td>
                                    <td class="auto-style49">
                                        <asp:TextBox ID="txtname" runat="server" CssClass="textbox" Enabled="False" Height="18px" Width="245px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="auto-style48"></td>
                                    <td class="auto-style47">廠 區:</td>
                                    <td class="auto-style49">
                                        <asp:DropDownList ID="ddlFactory" runat="server" AutoPostBack="True" CssClass="textbox" OnSelectedIndexChanged="ddlFactory_SelectedIndexChanged" Width="247px">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="auto-style48"></td>
                                    <td class="auto-style47">部 門:</td>
                                    <td class="auto-style49">
                                        <asp:DropDownList ID="ddlDept" runat="server" CssClass="textbox" Width="247px">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="auto-style50">&nbsp;</td>
                                    <td class="auto-style44">&nbsp;</td>
                                    <td class="auto-style45">&nbsp;</td>
                                </tr>
                                <tr>
                                    <td class="auto-style50"></td>
                                    <td class="auto-style44"></td>
                                    <td class="auto-style45">
                                        <asp:Button ID="btnlogin" runat="server" CssClass="buttons" OnClick="btnlogin_Click" Text="驗    證" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="auto-style50">&nbsp;</td>
                                    <td class="auto-style44">&nbsp;</td>
                                    <td class="auto-style45">
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td class="auto-style50">&nbsp;</td>
                                    <td class="auto-style44">&nbsp;</td>
                                    <td class="auto-style45">
                                        <asp:Label ID="lb1" runat="server" style="font-weight: 700; color: #FF0000"></asp:Label>
                                    </td>
                                </tr>
                                </table>
                        </td>
                    </tr>
                </table>
            </ContentTemplate>

        <br />   
        <br />

  <br />


    </div>
   </center>
    </form>
</body>
</html>
