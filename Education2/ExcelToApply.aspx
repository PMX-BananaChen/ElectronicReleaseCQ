<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="ExcelToApply.aspx.cs" Inherits="Education2.ExcelToApply" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="Css/Page_Style.css" rel="stylesheet" />
   <script type="text/javascript" src="My97DatePicker/WdatePicker.js"></script>

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
        .auto-style18 {
            width: 100%;
            border-color: #dcdcdc;
            border-width: 0;
        }
        .auto-style25 {
            height: 29px;
            width: 328px;
        }
        .auto-style29 {
            width: 328px;
            text-align: center;
        }
        .auto-style32 {
            height: 29px;
        }
        .auto-style34 {
            width: 53px;
        }
        #txtdatestar0 {
            width: 85px;
        }
        .auto-style40 {
            height: 29px;
            width: 193px;
        }
        .auto-style48 {
            width: 193px;
            height: 32px;
            text-align: right;
        }
        .auto-style49 {
            width: 328px;
            text-align: left;
            height: 32px;
        }
        .auto-style50 {
            height: 32px;
            color: #FF0000;
            text-align: left;
        }
        .auto-style53 {
            height: 32px;
            width: 83px;
            text-align: center;
        }
        .auto-style56 {
            height: 32px;
            width: 110px;
            text-align: left;
        }
        .auto-style57 {
            width: 110px;
        }
        .auto-style58 {
            height: 29px;
            width: 110px;
        }
        .auto-style59 {
            height: 29px;
            width: 83px;
        }
        .auto-style60 {
            width: 83px;
        }
        .auto-style61 {
            width: 100%;
            height: 58px;
        }
        .auto-style62 {
            font-size: medium;
            color: #FF0000;
        }
        .auto-style63 {
            width: 193px;
        }
        .auto-style64 {
            font-size: medium;
        }
        </style>
         <script type="text/javascript" >
        function myfun()
        {
            document.getElementById("qh_con0").style.display = "block";
            document.getElementById("mynav0").className = "nav_on2";
        }
        window.onload = myfun;

    </script> 
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
  
            <table cellpadding="0" class="auto-style18">
                <tr>
                    <td class="auto-style40"></td>
                    <td class="auto-style25"></td>
                    <td class="auto-style59"></td>
                    <td class="auto-style58"></td>
                    <td class="auto-style32"></td>
                    <td class="auto-style32"></td>
                </tr>
                <tr>
                    <td class="auto-style48">
                        <a href="./ExcelTemplate/dzfxt.xls"><font color="blue"><u>點擊下載模板</u></font></a> </td>
                    <td class="auto-style49">
                        <asp:FileUpload ID="FileUpload1" runat="server" Height="24px" style="text-align: left" Width="312px" />
                    </td>
                    <td class="auto-style53">
                        <asp:Button ID="btnImport" runat="server" Height="25px" OnClick="btnImport_Click" Text="導入" Width="59px" />
                    </td>
                    <td class="auto-style56">
                        <asp:Button ID="btndrqr" runat="server" OnClick="btndrqr_Click" Text="確認導入" Visible="False" />
                    </td>
                    <td class="auto-style50" colspan="2">
                        EXCEL模板一定要為<strong><span class="auto-style64"> .xls</span></strong></td>
                </tr>
                <tr>
                    <td class="auto-style63">&nbsp;</td>
                    <td class="auto-style29">&nbsp;</td>
                    <td class="auto-style60">&nbsp;</td>
                    <td class="auto-style57">&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
            </table>
         <%--   <asp:Panel ID="Panel1" runat="server" Height="350px">--%>
                <table cellpadding="0" cellspacing="0" class="auto-style61">
                    <tr>
                        <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span class="auto-style62">&nbsp;&nbsp;&nbsp;&nbsp; <strong>注 :&nbsp; 導入 小於今日日期的單據 , 系統自動標識為補錄單據 ;日期規則 : 不能小於昨日與大於后31天的日期.否則導入失敗 ! </strong></span></td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                    </tr>
            </table>
                <table cellpadding="0" class="auto-style10">
                    <tr>
                        <td class="auto-style34">&nbsp;</td>
                        <td class="auto-style12">
                            <asp:GridView ID="gdv1" runat="server" AutoGenerateColumns="False" BackColor="#00CCFF" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="2" ForeColor="Black" GridLines="Horizontal" OnRowDataBound="gridView_RowDataBound" PageSize="8" RowStyle-HorizontalAlign="Center" Width="918px">
                                <Columns>
                                    <asp:BoundField DataField="Emp_No" HeaderText="工号" ItemStyle-HorizontalAlign="Center" SortExpression="Emp_No">
                                    <HeaderStyle Height="35px" />
                                    <ItemStyle Height="25px" HorizontalAlign="Center" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="EmpName" HeaderStyle-Width="105px" HeaderText="姓名" ItemStyle-HorizontalAlign="Center" SortExpression="EmpName">
                                    <HeaderStyle Width="105px" />
                                    <ItemStyle HorizontalAlign="Center" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Factory" HeaderText="BU">
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Dept" HeaderText="部门" ItemStyle-HorizontalAlign="Center" SortExpression="Dept">
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="DeptName" HeaderText="部門名稱" SortExpression="DeptName" />
                                    <asp:BoundField DataField="Date" DataFormatString="{0:yyyy-MM-dd}" HeaderText="通行日期" ItemStyle-HorizontalAlign="Center" SortExpression="Date">
                                    <ItemStyle HorizontalAlign="Center" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="StarTime" HeaderText="开始时段" ItemStyle-HorizontalAlign="Center" SortExpression="StarTime">
                                    <ItemStyle HorizontalAlign="Center" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="EndTime" HeaderText="结束时段" ItemStyle-HorizontalAlign="Center" SortExpression="EndTime">
                                    <ItemStyle HorizontalAlign="Center" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Remark" HeaderText="出行原因" SortExpression="Remark" />
                                    <asp:BoundField DataField="ManagerNo" HeaderText="主管Code" SortExpression="ManagerNo" />
                                    <asp:BoundField DataField="ManagerName" HeaderText="主管名稱" SortExpression="ManagerName" />
                                </Columns>
                                <FooterStyle BackColor="#CCCC99" ForeColor="Black" />
                                <HeaderStyle BackColor="#16ACFF" Font-Bold="True" ForeColor="White" Height="25px" />
                                <PagerStyle BackColor="White" BorderStyle="None" CssClass="bubufxPagerCss" ForeColor="Black" HorizontalAlign="Right" />
                                <RowStyle HorizontalAlign="Center" BackColor="White" />
                                <SelectedRowStyle BackColor="#CC3333" Font-Bold="True" ForeColor="White" />
                                <SortedAscendingCellStyle BackColor="#F7F7F7" />
                                <SortedAscendingHeaderStyle BackColor="#4B4B4B" />
                                <SortedDescendingCellStyle BackColor="#E5E5E5" />
                                <SortedDescendingHeaderStyle BackColor="#242121" />
                            </asp:GridView>
                        </td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="auto-style34">&nbsp;</td>
                        <td class="auto-style12">
                            &nbsp;</td>
                        <td>&nbsp;</td>
                    </tr>
                </table>
        <br />
           <%-- </asp:Panel>--%>
    <br />
    <br />
    <br />
    
     <%--   </ContentTemplate>
    </asp:UpdatePanel>--%>
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
    <br />
          
</asp:Content>
