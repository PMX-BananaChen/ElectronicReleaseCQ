<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="Education2.Register1" %>

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
        .auto-style21 {
            width: 44px;
        }
        .auto-style32 {
            width: 200px;
        }
        .auto-style33 {
            width: 605px;
            height: 235px;
        }
        .auto-style34 {
            height: 33px;
        }
        .auto-style35 {
            font-size: medium;
        }
        .textbox{background:#FFFFFF url('/WebImg/login_6.gif') repeat-x; 
border:solid 1px #27B3FE; }
        .auto-style39 {
            width: 67px;
        }
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
        .auto-style43 {
            width: 44px;
            height: 21px;
        }
        .auto-style44 {
            width: 67px;
            height: 21px;
        }
        .auto-style45 {
            height: 21px;
            width: 264px;
        }
        .auto-style46 {
            width: 264px;
        }


        .auto-style47 {
            width: 67px;
            height: 37px;
            font-size: medium;
            color: #003366;
        }
        .auto-style48 {
            width: 44px;
            height: 37px;
        }
        .auto-style49 {
            height: 37px;
            width: 264px;
        }


        .auto-style50 {
            width: 44px;
            height: 42px;
        }
        .auto-style51 {
            width: 67px;
            height: 42px;
            font-size: medium;
            color: #003366;
        }
        .auto-style52 {
            height: 42px;
            width: 264px;
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
    <div style="height: 522px; width:100%; vertical-align:middle; "  >
    <br />   <br />   
        <br />

    <table border="0" style="border: 1px solid #C0C0C0; width: 545px; height: 442px; background-color: #f0f5f8;" 
            cellspacing="0" cellpadding="0" >
        <tr>
  <td align="center" class="auto-style33"  >
      <table cellpadding="0" style="border-bottom: 1px solid #C0C0C0; height: 40px; width: 506px;" >
          <tr>
              <td class="auto-style34" style="text-align: left"><strong style="text-align: left;"><span class="auto-style35">用户註冊</span> </strong></td>
          </tr>
      </table>
      <br />
    <table border="0" style=" width: 507px; height: 194px;" 
          cellspacing="0" cellpadding="0">
  <tr >
  <td class="auto-style19" colspan="4">
      <strong>
      &nbsp;&nbsp;&nbsp;&nbsp;
      </strong></td>
  </tr>
  <tr>
  <td class="auto-style48">
  </td>
  <td class="auto-style47">
                                            工 號:</td>
  <td class="auto-style49">
      <asp:TextBox ID="txtempno" CssClass="textbox" runat="server" Width="245px" Height="18px" AutoPostBack="True" OnTextChanged="txtempno_TextChanged"></asp:TextBox>
  </td>
  <td align="left" class="auto-style42" rowspan="8">
                                            &nbsp;<asp:Panel ID="Panel1" runat="server">
                                            </asp:Panel>
                                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                            </td>
  </tr>

  <tr>
  <td class="auto-style48">
      </td>
  <td class="auto-style47">
                                            姓 名:</td>
  <td class="auto-style49">
      <asp:TextBox ID="txtname" CssClass="textbox" runat="server" Width="245px" Height="18px" Enabled="False"></asp:TextBox>
  </td>
  </tr>

  <tr>
  <td class="auto-style48">
      </td>
  <td class="auto-style47">
                                            密 碼:</td>
  <td class="auto-style49">
      <asp:TextBox ID="txtpass" CssClass="textbox" runat="server" Width="245px" Height="18px" TextMode="Password"></asp:TextBox>
  </td>
  </tr>

  <tr>
  <td class="auto-style48">
      </td>
  <td class="auto-style47">
                                            郵 箱:</td>
  <td class="auto-style49">
      <asp:TextBox ID="txtmail" CssClass="textbox" runat="server" Width="245px" Height="18px"></asp:TextBox>
  </td>
  </tr>

  <tr>
  <td class="auto-style50">
      </td>
  <td class="auto-style51">
                                            權 限:</td>
  <td class="auto-style52">
      <asp:DropDownList ID="ddlrole"  CssClass="textbox" runat="server" Width="247px">
          <asp:ListItem Value="4">普通用戶</asp:ListItem>
      </asp:DropDownList>
  </td>
  </tr>

  <tr>
  <td class="auto-style43">
      </td>
  <td class="auto-style44">
      </td>
  <td class="auto-style45" >
  </tr>

  <tr>
  <td class="auto-style21">
      &nbsp;</td>
  <td class="auto-style39">
      &nbsp;</td>
  <td class="auto-style46" >
      <input type="button" value="立即註冊" runat="server"  onserverclick="submit1_Click"  class ="buttons"/>
      <input type="button" value="返    回" runat="server"  onserverclick="back_Click"  class="buttons"/></tr>

  <tr>
  <td class="auto-style21">
      &nbsp;</td>
  <td class="auto-style39">
      &nbsp;</td>
  <td class="auto-style46" >
      &nbsp;</tr>

  </table>
  </td>
        </tr>
  </table>
  <br />
  <br /> <br />
  <br />


    </div>
   </center>
    </form>
</body>
</html>
