<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="Agent.aspx.cs" Inherits="Education2.Agent" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <link href="Css/Page_Style.css" rel="stylesheet" />
   <script type="text/javascript" src="My97DatePicker/WdatePicker.js"></script>

    <script type="text/javascript" >
        function myfun() {
            document.getElementById("qh_con3").style.display = "block";
            document.getElementById("mynav3").className = "nav_on2";
        }
        window.onload = myfun;
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

    
    <style type="text/css">

        td { font-size: 9pt}
        #txt_startDate {
            width: 86px;
        }
        #txt_startDate0 {
            width: 86px;
        }
        #txtdatestar {
            width: 85px;
        }
        #txtdatestar0 {
            width: 85px;
        }
        .border {
            width: 993px;
        }
        #txtdatestar0 {
            width: 85px;
        }
        .auto-style34 {
            width: 100%;
            height: 48px;
        }
        .auto-style49 {
            height: 40px;
        text-align: right;
        width: 198px;
    }
        #txtdatestar0 {
            width: 85px;
        }
        .auto-style58 {
            width: 123px;
            height: 40px;
        }
        .auto-style62 {
            width: 177px;
            height: 40px;
        }
        .auto-style69 {
            width: 181px;
            height: 40px;
        }
        .auto-style70 {
            text-align: center;
            width: 222px;
            height: 40px;
        }
        .auto-style72 {
            text-align: center;
            width: 330px;
            height: 40px;
        }
        .auto-style73 {
            height: 40px;
        }
        .auto-style74 {
            width: 274px;
            height: 40px;
        }
        .auto-style75 {
            width: 283px;
            height: 40px;
        }
        .auto-style76 {
            text-align: center;
            width: 304px;
            height: 40px;
        }
    </style>
  
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <br />
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>

               <table class="auto-style34">
                <tr>
                    <td class="auto-style75"></td>
                    <td class="auto-style70">
                        開始日期</td>
                    <td class="auto-style74">
                        <input id="txtdatestar" runat="server" name="Text9"  onfocus="WdatePicker({ minDate:'%y-%M-%d',dateFmt: 'yyyyMMdd'  })" type="text"  style="width: 148px" />
                    </td>
                    <td class="auto-style76">
                        &nbsp;</td>
                    <td class="auto-style72">結束日期</td>
                    <td class="auto-style73" colspan="3">
                        <input id="txtdateend" runat="server" name="Text11" onclick="WdatePicker({ dateFmt: 'yyyyMMdd' }), dateDiff()"  type="text"  style="width: 148px" />
                    </td>
                    <td class="auto-style73"></td>
                </tr>
                   <tr>
                       <td class="auto-style75"></td>
                       <td class="auto-style70">代理主管工號</td>
                       <td class="auto-style74">
                           <asp:TextBox ID="txtDLempno" runat="server" Width="149px"></asp:TextBox>
                           <asp:ImageButton ID="ImageButton8" runat="server" Height="16px" ImageUrl="~/WebImg/add.png" OnClick="ImageButton8_Click" Width="16px" />
                       </td>
                       <td class="auto-style76">&nbsp;</td>
                       <td class="auto-style72">代理主管姓名</td>
                       <td class="auto-style62">
                           <asp:TextBox ID="txtDLempname" runat="server" Width="149px"></asp:TextBox>
                       </td>
                       <td class="auto-style58">
                           <asp:ImageButton ID="ImageButton4" runat="server" Height="32px" ImageUrl="~/WebImg/Search.gif" OnClick="ImageButton1_Click" Width="64px" />
                       </td>
                       <td class="auto-style49"></td>
                       <td class="auto-style73"></td>
                   </tr>
            </table>
    <asp:Panel ID="Panel1" runat="server" Height="350px">
        <table cellpadding="0" class="auto-style10">
            <tr>
                <td class="auto-style36">&nbsp;</td>
                <td class="auto-style12">
                    <asp:GridView ID="gdv1" runat="server" AllowPaging="True" AutoGenerateColumns="False" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="2" DataKeyNames="ID" ForeColor="Black" GridLines="Horizontal" OnPageIndexChanging="gridView_PageIndexChanging" OnRowCreated="gridView_OnRowCreated" OnRowDataBound="gridView_RowDataBound" PageSize="8" RowStyle-HorizontalAlign="Center" Width="938px" OnRowDeleting="gdv1_RowDeleting">
                        <Columns>
                            <asp:BoundField>
                            <ItemStyle Width="30px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="DLManagerNo" HeaderText="代理主管工号" ItemStyle-HorizontalAlign="Center" SortExpression="DLManagerNo">
                            <ItemStyle HorizontalAlign="Center" />
                            <ItemStyle Height="25px" HorizontalAlign="Center"  />
                            <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="DLManagerName" HeaderText="代理主管姓名" ItemStyle-HorizontalAlign="Center" SortExpression="DLManagerName">
                            <HeaderStyle />
                            <ItemStyle HorizontalAlign="Center" />
                            <HeaderStyle HorizontalAlign="Center"  />
                            </asp:BoundField>
                            <asp:BoundField DataField="Dates" HeaderText="代理日期" SortExpression="Dates" DataFormatString="{0:yyyy-MM-dd}">
                            </asp:BoundField>
                            <asp:BoundField DataField="StarTime" HeaderText="開始時段" SortExpression="StarTime">
                            </asp:BoundField>
                            <asp:BoundField DataField="EndTime" HeaderText="結束時段" SortExpression="EndTime" />
                       <%--  <asp:HyperLinkField  HeaderText="详细" DataNavigateUrlFields="ID"  DataNavigateUrlFormatString="AgentDetail.aspx?id={0}" Text='查看' >
                    
                        <ControlStyle Width="50px"></ControlStyle>
                        <ItemStyle ForeColor="#0066FF" />
                        </asp:HyperLinkField>--%>


                            <asp:CommandField DeleteText="刪除" HeaderText="刪除" ShowDeleteButton="True" >
                            <HeaderStyle Width="60px" />
                            <ItemStyle Font-Italic="False" Font-Overline="False" Font-Underline="False" ForeColor="#0000CC" Width="60px" />
                            </asp:CommandField>


                        </Columns>
                        <FooterStyle BackColor="#CCCC99" ForeColor="Black" />
                        <HeaderStyle BackColor="#16ACFF" Font-Bold="True" ForeColor="White" Height="35px" />
                        <PagerStyle BackColor="White" BorderStyle="None" CssClass="bubufxPagerCss" ForeColor="Black" HorizontalAlign="Right" />
                        <RowStyle BackColor="White" HorizontalAlign="Center" />
                        <SelectedRowStyle BackColor="#CC3333" Font-Bold="True" ForeColor="White" />
                        <SortedAscendingCellStyle BackColor="#F7F7F7" />
                        <SortedAscendingHeaderStyle BackColor="#4B4B4B" />
                        <SortedDescendingCellStyle BackColor="#E5E5E5" />
                        <SortedDescendingHeaderStyle BackColor="#242121" />
                    </asp:GridView>
                </td>
                <td class="auto-style23">&nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style36">&nbsp;</td>
                <td class="auto-style12">
                    &nbsp;</td>
                <td class="auto-style23">&nbsp;</td>
            </tr>
        </table>
        <br />



   </asp:Panel>
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
    <br />
    <br />
    <br />
</asp:Content>
