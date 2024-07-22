<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="PersonalDetail.aspx.cs" Inherits="Education2.PersonalDetail" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">


      <script type="text/javascript" >
        function myfun()
        {
            document.getElementById("qh_con4").style.display = "block";
            document.getElementById("mynav4").className = "nav_on2";
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
        .auto-style5 {
            text-align: center;
            height: 18px;
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
        .auto-style22 {
            text-align: left;
            height: 18px;
            width: 304px;
        }
        .auto-style23 {
            height: 32px;
            width: 171px;
        }
        .auto-style24 {
            width: 171px;
        }
        .auto-style26 {
            text-align: center;
            height: 18px;
            width: 171px;
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
            width: 211px;
        }
        .auto-style32 {
            text-align: left;
            width: 211px;
        }
        .auto-style33 {
            text-align: left;
            height: 18px;
            width: 211px;
        }
        .auto-style34 {
            text-align: left;
            height: 33px;
            width: 211px;
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
        .Button02 {
            height: 26px;
        }
    </style>
    
     <script language="JavaScript" type="text/javascript">

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
                if (fd > td && td!="") {
                    alert("選擇的時段不能小於開始時段！");
                    document.getElementById('ContentPlaceHolder1_txttimeend').value = "";
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
                    <td class="auto-style10">
                        工號</td>
                    <td class="auto-style31">
                        <asp:TextBox ID="txtempno" runat="server" Enabled="False" MaxLength="49"></asp:TextBox>
                    </td>
                    <td class="auto-style10">姓名</td>
                    <td class="auto-style35">
                        <asp:TextBox ID="txtempname" runat="server" Enabled="False" MaxLength="49"></asp:TextBox>
                    </td>
                    <td class="auto-style9"></td>
                    <td class="auto-style9"></td>
                </tr>
                <tr>
                    <td class="auto-style24">&nbsp;</td>
                    <td class="list_it_right_title">&nbsp;</td>
                    <td class="auto-style32">&nbsp;</td>
                    <td class="list_it_right_title">&nbsp;</td>
                    <td class="auto-style36">&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td class="auto-style29"></td>
                    <td class="auto-style10">BU</td>
                    <td class="auto-style31">
                        <asp:TextBox ID="txtfactory" runat="server" Enabled="False" MaxLength="150"></asp:TextBox>
                    </td>
                    <td class="auto-style10">部門</td>
                    <td class="auto-style35">
                        <asp:TextBox ID="txtdept" runat="server" Enabled="False" MaxLength="150"></asp:TextBox>
                    </td>
                    <td class="auto-style10"></td>
                    <td class="auto-style10"></td>
                </tr>
                <tr>
                    <td class="auto-style26"></td>
                    <td class="auto-style5"></td>
                    <td class="auto-style33"></td>
                    <td class="auto-style5"></td>
                    <td class="auto-style22"></td>
                    <td class="auto-style5"></td>
                    <td class="auto-style5"></td>
                </tr>
                <tr>
                    <td class="auto-style23"></td>
                    <td class="auto-style10">申請方式</td>
                    <td class="auto-style31">
                        <asp:TextBox ID="txtapply" runat="server" Enabled="False" MaxLength="49"></asp:TextBox>
                    </td>
                    <td class="auto-style10">通行日期</td>
                    <td class="auto-style35">
                        <input id="txtpassdate" runat="server" name="Text9"  onfocus="WdatePicker({ minDate:'%y-%M-#{%d-1}'  })"  type="text"  visible="True" style="width: 140px" disabled="disabled" />
                    </td>
                    <td class="auto-style9"></td>
                    <td class="auto-style9"></td>
                </tr>
                <tr>
                    <td class="auto-style24">&nbsp;</td>
                    <td class="list_it_right_title">&nbsp;</td>
                    <td class="auto-style32">&nbsp;</td>
                    <td class="list_it_right_title">&nbsp;</td>
                    <td class="auto-style36">&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td class="auto-style23"></td>
                    <td class="auto-style10">開始時段</td>
                    <td class="auto-style31">
                        <input id="txttimestar" runat="server" name="Text10"  type="text"  visible="True" style="width: 140px" class="Wdate"  onclick="WdatePicker({ dateFmt: 'HH:mm' })" disabled="disabled" />
                    </td>
                    <td class="auto-style10">結束時段</td>
                    <td class="auto-style35">
                        <input id="txttimeend" runat="server" name="Text12"  onclick="WdatePicker({ dateFmt: 'HH:mm' }), dateDiffHours()"  type="text"  visible="True" style="width: 140px" class="Wdate" disabled="disabled" />
                    </td>
                    <td class="auto-style9"></td>
                    <td class="auto-style9"></td>
                </tr>
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
                    <td class="auto-style29"></td>
                    <td class="auto-style10">創建人</td>
                    <td class="auto-style31">
                        <asp:TextBox ID="createuser" runat="server" Enabled="False" MaxLength="49"></asp:TextBox>
                    </td>
                    <td class="auto-style10">創建日期</td>
                    <td class="auto-style35">
                        <asp:TextBox ID="createdate" runat="server" Enabled="False"></asp:TextBox>
                    </td>
                    <td class="auto-style10"></td>
                    <td class="auto-style10"></td>
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
                    <td class="auto-style28"></td>
                    <td class="auto-style11">目前狀態</td>
                    <td class="auto-style34">
                        <asp:TextBox ID="txtstate" runat="server" Enabled="False"></asp:TextBox>
                    </td>
                    <td class="auto-style11">&nbsp;</td>
                    <td class="auto-style19">&nbsp;</td>
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
                    <td class="auto-style28"></td>
                    <td class="auto-style11">放行原因</td>
                    <td class="auto-style41" colspan="3">
                        <asp:TextBox ID="txtremark" runat="server" TextMode="MultiLine" Width="482px" MaxLength="200"></asp:TextBox>
                    </td>
                    <td class="auto-style11"></td>
                    <td class="auto-style11"></td>
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
            <asp:Panel ID="Panel1" runat="server" Visible="False">
                <table cellpadding="0" cellspacing="0" class="auto-style2">
                    <tr>
                        <td class="auto-style29"></td>
                        <td class="auto-style10" style="background-color: #33CCCC">退回信息</td>
                        <td class="auto-style31"></td>
                        <td class="auto-style10"></td>
                        <td class="auto-style35"></td>
                        <td class="auto-style10"></td>
                        <td class="auto-style10"></td>
                    </tr>
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
                        <td class="auto-style11">退回人</td>
                        <td class="auto-style34">
                            <asp:TextBox ID="txtbackuser" runat="server"></asp:TextBox>
                        </td>
                        <td class="auto-style11">退回時間</td>
                        <td class="auto-style19">
                            <asp:TextBox ID="txtbackdate" runat="server"></asp:TextBox>
                        </td>
                        <td class="auto-style11"></td>
                        <td class="auto-style11"></td>
                    </tr>
                    <tr>
                        <td class="auto-style28">&nbsp;</td>
                        <td class="auto-style11">退回原因</td>
                        <td class="auto-style41" colspan="3">
                            <asp:TextBox ID="txtbackremark" runat="server" TextMode="MultiLine" Width="482px"></asp:TextBox>
                        </td>
                        <td class="auto-style11">&nbsp;</td>
                        <td class="auto-style11">&nbsp;</td>
                    </tr>
                </table>
            </asp:Panel>
            <asp:Panel ID="Panel2" runat="server" Visible="False">
                <table cellpadding="0" cellspacing="0" class="auto-style2">
                    <tr>
                        <td class="auto-style29"></td>
                        <td class="auto-style10" style="background-color: #33CCCC">審核信息</td>
                        <td class="auto-style31">&nbsp;</td>
                        <td class="auto-style10">&nbsp;</td>
                        <td class="auto-style35">&nbsp;</td>
                        <td class="auto-style10"></td>
                        <td class="auto-style10"></td>
                    </tr>
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
                        <td class="auto-style11">審核人</td>
                        <td class="auto-style34">
                            <asp:TextBox ID="txtaudituser" runat="server"></asp:TextBox>
                        </td>
                        <td class="auto-style11">審核時間</td>
                        <td class="auto-style19">
                            <asp:TextBox ID="txtauditdate" runat="server"></asp:TextBox>
                        </td>
                        <td class="auto-style11"></td>
                        <td class="auto-style11"></td>
                    </tr>
                </table>
            </asp:Panel>
    <br />
            <asp:Panel ID="Panel3" runat="server">
                <table cellpadding="0" cellspacing="0" class="auto-style2">
                    <tr>
                        <td class="auto-style30">&nbsp;</td>
                        <td class="auto-style3">&nbsp;</td>
                        <td class="auto-style32">&nbsp;</td>
                        <td class="list_it_right_title">&nbsp;</td>
                        <td class="auto-style36">&nbsp;</td>
                        <td class="auto-style3">&nbsp;</td>
                        <td class="auto-style3">&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="auto-style28"></td>
                        <td class="auto-style11">&nbsp;</td>
                        <td class="auto-style34">&nbsp;</td>
                        <td>
                            <asp:Button ID="btnsubmit" runat="server" CssClass="Button02" OnClick="btnsubmit_Click" Text="重新提交" Visible="False" />
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:Button ID="btnok" runat="server" CssClass="Button02" OnClick="Button1_Click" Text="返  回" />
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
            </asp:Panel>
    <br />
    <br />
    <br />
     </ContentTemplate>
    </asp:UpdatePanel>
    <br />
    <br />
    <br />
</asp:Content>
