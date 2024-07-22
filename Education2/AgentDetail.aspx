<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="AgentDetail.aspx.cs" Inherits="Education2.AgentDetail" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript" >
        function myfun() {
            document.getElementById("qh_con3").style.display = "block";
            document.getElementById("mynav3").className = "nav_on2";
        }
        window.onload = myfun;

    </script> 


    <style type="text/css">
        .auto-style2 {
            width: 100%;
        }
        .auto-style3 {
            text-align: center;
        }
        .auto-style9 {
            height: 32px;
        }
        .auto-style10 {
            text-align: center;
            height: 32px;
        }
        .auto-style11 {
            text-align: center;
            height: 33px;
        }
        .auto-style19 {
            text-align: left;
            height: 33px;
            width: 304px;
        }
        .auto-style23 {
            height: 32px;
            width: 171px;
        }
        .auto-style24 {
            width: 171px;
            height: 20px;
        }
        .auto-style28 {
            text-align: center;
            height: 33px;
            width: 171px;
        }
        .auto-style29 {
            text-align: center;
            width: 171px;
            height: 32px;
        }
        .auto-style30 {
            text-align: center;
            width: 171px;
        }
        .auto-style31 {
            text-align: left;
            height: 32px;
            }
        .auto-style32 {
            text-align: left;
            width: 211px;
        }
        .auto-style34 {
            text-align: left;
            height: 33px;
            }
        .auto-style35 {
            text-align: left;
            height: 32px;
            width: 304px;
        }
        .auto-style36 {
            text-align: left;
            width: 304px;
        }
        .auto-style41 {
            text-align: left;
            height: 33px;
        }
        .auto-style42 {
            text-align: center;
            width: 171px;
            height: 12px;
        }
        .auto-style43 {
            text-align: center;
            height: 12px;
        }
        .auto-style44 {
            text-align: left;
            width: 211px;
            height: 12px;
        }
        .auto-style45 {
            text-align: left;
            width: 304px;
            height: 12px;
        }
        .auto-style50 {
            height: 20px;
        }
        .auto-style51 {
            text-align: center;
            height: 20px;
        }
        .auto-style52 {
            text-align: left;
            width: 211px;
            height: 20px;
        }
        .auto-style53 {
            text-align: left;
            width: 304px;
            height: 20px;
        }
        .auto-style54 {
            text-align: center;
            width: 171px;
            height: 20px;
        }
        .auto-style57 {
            text-align: left;
            height: 20px;
        }
    </style>
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

             }
         }




</script> 
   <script type="text/javascript" src="My97DatePicker/WdatePicker.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <br />
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <br />
            <table cellpadding="0" cellspacing="0" class="auto-style2">
                <tr>
                    <td class="auto-style23"></td>
                    <td class="auto-style10" style="background-color: #33CCCC">
                        代理信息</td>
                    <td class="auto-style31">
                        &nbsp;</td>
                    <td class="auto-style10">&nbsp;</td>
                    <td class="auto-style35">
                        &nbsp;</td>
                    <td class="auto-style9"></td>
                    <td class="auto-style9"></td>
                </tr>
                <tr>
                    <td class="auto-style24"></td>
                    <td class="auto-style51"></td>
                    <td class="auto-style52"></td>
                    <td class="auto-style51"></td>
                    <td class="auto-style53"></td>
                    <td class="auto-style50"></td>
                    <td class="auto-style50"></td>
                </tr>
                <tr>
                    <td class="auto-style23">&nbsp;</td>
                    <td class="auto-style10">
                        是否啟用代理</td>
                    <td class="auto-style31">
                        <asp:RadioButtonList ID="rdagent" runat="server" AutoPostBack="True" OnSelectedIndexChanged="RadioButtonList1_SelectedIndexChanged" RepeatDirection="Horizontal">
                            <asp:ListItem Selected="True" Value="0">啟用</asp:ListItem>
                            <asp:ListItem Value="1">取消</asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                    <td class="auto-style10">
                        &nbsp;</td>
                    <td class="auto-style35">
                        &nbsp;</td>
                    <td class="auto-style9">&nbsp;</td>
                    <td class="auto-style9">&nbsp;</td>
                </tr>
                <tr>
                    <td class="auto-style24"></td>
                    <td class="auto-style51"></td>
                    <td class="auto-style57"></td>
                    <td class="auto-style51"></td>
                    <td class="auto-style53"></td>
                    <td class="auto-style50"></td>
                    <td class="auto-style50"></td>
                </tr>
                <tr>
                    <td class="auto-style23">&nbsp;</td>
                    <td class="auto-style10">主管工號</td>
                    <td class="auto-style31">
                        <asp:TextBox ID="txtempno" runat="server" Enabled="False" MaxLength="30" OnTextChanged="txtempno_TextChanged1" Width="100px"></asp:TextBox>
                    </td>
                    <td class="auto-style10">主管姓名</td>
                    <td class="auto-style35">
                        <asp:TextBox ID="txtempname" runat="server" Enabled="False" MaxLength="30" Width="100px"></asp:TextBox>
                    </td>
                    <td class="auto-style9">&nbsp;</td>
                    <td class="auto-style9">&nbsp;</td>
                </tr>
                <tr>
                    <td class="auto-style24"></td>
                    <td class="auto-style51"></td>
                    <td class="auto-style52"></td>
                    <td class="auto-style51"></td>
                    <td class="auto-style53"></td>
                    <td class="auto-style50"></td>
                    <td class="auto-style50"></td>
                </tr>
                <tr>
                    <td class="auto-style23"></td>
                    <td class="auto-style10">主管郵箱</td>
                    <td class="auto-style31" colspan="3">
                        <asp:TextBox ID="txtmail" runat="server" Enabled="False" MaxLength="50" Width="460px"></asp:TextBox>
                    </td>
                    <td class="auto-style9"></td>
                    <td class="auto-style9"></td>
                </tr>
                <tr>
                    <td class="auto-style54"></td>
                    <td class="auto-style51"></td>
                    <td class="auto-style52"></td>
                    <td class="auto-style51"></td>
                    <td class="auto-style53"></td>
                    <td class="auto-style51"></td>
                    <td class="auto-style51"></td>
                </tr>
                <tr>
                    <td class="auto-style29">&nbsp;</td>
                    <td class="auto-style10">開始日期</td>
                    <td class="auto-style31">
                        <input id="txtdatestar" runat="server" name="Text9"  onfocus="WdatePicker({ minDate:'%y-%M-%d',dateFmt: 'yyyyMMdd'  })" type="text" aria-checked="undefined" aria-disabled="False" contenteditable="false" visible="True" style="width: 140px" />
                    </td>
                    <td class="auto-style10">結束日期</td>
                    <td class="auto-style35">
                        <input id="txtdateend" runat="server" name="Text11" onclick="WdatePicker({ dateFmt: 'yyyyMMdd' }), dateDiff()"  type="text" aria-checked="undefined" aria-disabled="False" contenteditable="false" visible="True" style="width: 140px" />
                    </td>
                    <td class="auto-style10">&nbsp;</td>
                    <td class="auto-style10">&nbsp;</td>
                </tr>
                <tr>
                    <td class="auto-style54"></td>
                    <td class="auto-style51"></td>
                    <td class="auto-style52"></td>
                    <td class="auto-style51"></td>
                    <td class="auto-style53"></td>
                    <td class="auto-style51"></td>
                    <td class="auto-style51"></td>
                </tr>
                <tr>
                    <td class="auto-style29"></td>
                    <td class="auto-style10">代理主管工號</td>
                    <td class="auto-style31">
                        <asp:TextBox ID="txtDLempno" runat="server" AutoPostBack="True" MaxLength="30" OnTextChanged="txtempno_TextChanged1" Width="100px"></asp:TextBox>
                    </td>
                    <td class="auto-style10">代理主管姓名</td>
                    <td class="auto-style35">
                        <asp:TextBox ID="txtDLempname" runat="server" Enabled="False" MaxLength="30" Width="100px"></asp:TextBox>
                    </td>
                    <td class="auto-style10"></td>
                    <td class="auto-style10"></td>
                </tr>
                <tr>
                    <td class="auto-style54"></td>
                    <td class="auto-style51"></td>
                    <td class="auto-style52"></td>
                    <td class="auto-style51"></td>
                    <td class="auto-style53"></td>
                    <td class="auto-style51"></td>
                    <td class="auto-style51"></td>
                </tr>
                <tr>
                    <td class="auto-style28"></td>
                    <td class="auto-style11">代理主管郵箱</td>
                    <td class="auto-style34" colspan="3">
                        <asp:TextBox ID="txtDLmail" runat="server" MaxLength="50" Width="454px" Enabled="False"></asp:TextBox>
                    </td>
                    <td class="auto-style11"></td>
                    <td class="auto-style11"></td>
                </tr>
                <tr>
                    <td class="auto-style42"></td>
                    <td class="auto-style43"></td>
                    <td class="auto-style44"></td>
                    <td class="auto-style43"></td>
                    <td class="auto-style45"></td>
                    <td class="auto-style43"></td>
                    <td class="auto-style43"></td>
                </tr>
                <tr>
                    <td class="auto-style42">&nbsp;</td>
                    <td class="auto-style43">&nbsp;</td>
                    <td class="auto-style44">&nbsp;</td>
                    <td class="auto-style43">&nbsp;</td>
                    <td class="auto-style45">&nbsp;</td>
                    <td class="auto-style43">&nbsp;</td>
                    <td class="auto-style43">&nbsp;</td>
                </tr>
            </table>
    <br />
  <hr/>
                <table cellpadding="0" cellspacing="0" class="auto-style2">
                    <tr>
                        <td class="auto-style30">&nbsp;</td>
                        <td class="auto-style3">&nbsp;</td>
                        <td class="auto-style32">&nbsp;</td>
                        <td class="auto-style3">&nbsp;</td>
                        <td class="auto-style36">&nbsp;</td>
                        <td class="auto-style3">&nbsp;</td>
                        <td class="auto-style3">&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="auto-style28"></td>
                        <td class="auto-style11">&nbsp;</td>
                        <td class="auto-style34">&nbsp;</td>
                        <td class="auto-style11">
                            <asp:Button ID="btnsubmit" runat="server" CssClass="Button02" OnClick="btnsubmit_Click" Text="修  改" />
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Button ID="btnok" runat="server" CssClass="Button02" OnClick="Button1_Click" Text="返  回" />
                            &nbsp;&nbsp;&nbsp;&nbsp;
                        </td>
                        <td class="auto-style19">&nbsp;</td>
                        <td class="auto-style11"></td>
                        <td class="auto-style11"></td>
                    </tr>
                    <tr>
                        <td class="auto-style28">&nbsp;</td>
                        <td class="auto-style11">&nbsp;</td>
                        <td class="auto-style41" colspan="3">&nbsp;</td>
                        <td class="auto-style11">&nbsp;</td>
                        <td class="auto-style11">&nbsp;</td>
                    </tr>
                </table>
         
    <br />
    <br />
    <br />
     </ContentTemplate>
    </asp:UpdatePanel>
    <br />
    <br />
    <br />
</asp:Content>

