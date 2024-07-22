<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="Education2.Login" %>

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
        .auto-style24 {
            width: 44px;
            height: 40px;
        }
        .auto-style26 {
            height: 40px;
            width: 279px;
        }
        .auto-style32 {
            width: 200px;
        }
        .auto-style33 {
            width: 605px;
            height: 254px;
        }
        .auto-style34 {
            height: 33px;
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


        .auto-style40 {
            width: 44px;
            height: 45px;
        }
        .auto-style41 {
            width: 67px;
            height: 45px;
            font-size: medium;
            color: #003366;
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
            width: 279px;
        }
        .auto-style46 {
            width: 279px;
        }


    </style>
</head>
<body style=" background-image: url('WebImg/bodyback.jpg')">

    <form id="from1" runat="server" defaultbutton="btnlogin" >
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
        <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true"> 

        </asp:ScriptManager>
        <br />   <br />

    <table border="0" style="border: 1px solid #C0C0C0; width: 545px; height: 302px; background-color: #f0f5f8;" 
            cellspacing="0" cellpadding="0" >
        <tr>
  <td align="center" class="auto-style33"  >
      <table cellpadding="0" style="border-bottom: 1px solid #C0C0C0; height: 30px; width: 506px;" >
          <tr>
              <td class="auto-style34" style="text-align: left"><strong style="text-align: left;">系統登錄中,請稍等 ... </strong></td>
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
  <td class="auto-style40">
  </td>
  <td class="auto-style41">
                                            &nbsp;</td>
  <td class="auto-style46">
      &nbsp;</td>
  <td align="left" class="auto-style42" rowspan="5">
                                            &nbsp;<asp:Panel ID="Panel1" runat="server">
                                            </asp:Panel>
                                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                            </td>
  </tr>

  <tr>
  <td class="auto-style24">
      &nbsp;</td>
  <td class="auto-style41">
                                            &nbsp;</td>
  <td class="auto-style26">
      &nbsp;</td>
  </tr>

  <tr>
  <td class="auto-style43">
      </td>
  <td class="auto-style44">
      </td>
  <td class="auto-style45" >
      &nbsp;</tr>

  <tr>
  <td class="auto-style21">
      &nbsp;</td>
  <td class="auto-style39">
      &nbsp;</td>
  <td class="auto-style46" >
  &nbsp;</tr>

  <tr>
  <td class="auto-style21">
      &nbsp;</td>
  <td class="auto-style39">
      &nbsp;</td>
  <td class="auto-style46" >
      &nbsp;</tr>

  </table>
      <br />
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
