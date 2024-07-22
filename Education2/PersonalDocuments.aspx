<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="PersonalDocuments.aspx.cs" Inherits="Education2.PersonalDocuments" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">



    <link href="Css/Page_Style.css" rel="stylesheet" />
   <script type="text/javascript" src="My97DatePicker/WdatePicker.js"></script>

    <script type="text/javascript" >
        function myfun()
        {
            document.getElementById("qh_con4").style.display = "block";
            document.getElementById("mynav4").className = "nav_on2";
        }
        window.onload = myfun;

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
        .auto-style17 {
            font-size: x-large;
        }
        #txtdatestar0 {
            width: 85px;
        }
        .auto-style34 {
            width: 100%;
            height: 48px;
        }
        .auto-style43 {
            width: 30px;
            height: 50px;
        }
        .auto-style45 {
            width: 125px;
            height: 50px;
        }
        .auto-style46 {
            text-align: center;
            width: 67px;
            height: 50px;
        }
        .auto-style47 {
            width: 190px;
            height: 50px;
        }
        .auto-style49 {
            height: 50px;
        }
        .auto-style51 {
            height: 50px;
        }
        #txtdatestar0 {
            width: 85px;
        }
        .auto-style52 {
            width: 238px;
            height: 50px;
        }
        .auto-style53 {
            text-align: center;
            width: 122px;
            height: 50px;
        }
        .auto-style54 {
            width: 136px;
            height: 50px;
        }
        .auto-style55 {
            text-align: center;
            width: 57px;
            height: 50px;
        }
        .auto-style56 {
            text-align: center;
            width: 76px;
            height: 50px;
        }
        .auto-style57 {
            text-align: center;
            width: 121px;
            height: 50px;
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
                    <td class="auto-style43"></td>
                    <td class="auto-style53">
                        工號/姓名:</td>
                    <td class="auto-style54">
                        <asp:TextBox ID="txtemp" runat="server" Width="100px"></asp:TextBox>
                    </td>
                    <td class="auto-style55">
                        <asp:Label ID="lbfactory" runat="server" Text="BU"></asp:Label>
                    </td>
                    <td class="auto-style47">
                        <asp:DropDownList ID="ddlfactory" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlfactory_SelectedIndexChanged" Width="150px">
                        </asp:DropDownList>
                    </td>
                    <td class="auto-style56">
                        <asp:Label ID="lbdept" runat="server" Text="部門"></asp:Label>
                    </td>
                    <td class="auto-style49">
                        <asp:DropDownList ID="ddlDept" runat="server" Width="250px">
                        </asp:DropDownList>
                        &nbsp;</td>
                    <td class="auto-style51"></td>
                </tr>
            </table>
            <table class="auto-style34">
                <tr>
                    <td class="auto-style43"></td>
                    <td class="auto-style57">
                        狀態:</td>
                    <td class="auto-style45">
                        <asp:DropDownList ID="Dll_States" runat="server" Width="100px">
                        </asp:DropDownList>
                    </td>
                    <td class="auto-style46">放行日期:</td>
                    <td class="auto-style52">
                        <input id="txtdatestar" runat="server"  name="Text9" onclick="WdatePicker({ dateFmt: 'yyyyMMdd' })"  type="text"  style="width: 100px" />
                        <strong><span class="auto-style17">-<input id="txtdateend" runat="server"  name="Text10" onclick="WdatePicker({ dateFmt: 'yyyyMMdd' })" type="text"  style="width: 100px" /></span></strong></td>
                    <td class="auto-style51">
                        <asp:ImageButton ID="ImageButton3" runat="server" Height="32px" ImageUrl="~/WebImg/Search.gif" OnClick="ImageButton1_Click" Width="64px" />
                    </td>
                    <td class="auto-style51">
                        &nbsp;</td>
                    <td class="auto-style51"></td>
                </tr>
            </table>
    <asp:Panel ID="Panel1" runat="server" Height="350px">
        <table cellpadding="0" class="auto-style10">
            <tr>
                <td class="auto-style36">&nbsp;</td>
                <td class="auto-style12">
                    <asp:GridView ID="gdv1" runat="server" AllowPaging="True" AutoGenerateColumns="False" BackColor="#00CCFF" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="2" DataKeyNames="ID" ForeColor="Black" GridLines="Horizontal" OnPageIndexChanging="gridView_PageIndexChanging" OnRowCreated="gridView_OnRowCreated" OnRowDataBound="gridView_RowDataBound" PageSize="8" RowStyle-HorizontalAlign="Center" Width="938px">
                        <Columns>
                            <asp:BoundField DataField="EmpNo" HeaderText="工号" ItemStyle-HorizontalAlign="Center" SortExpression="EmpNo">
                            <HeaderStyle Height="35px" />
                            <ItemStyle Height="25px" HorizontalAlign="Center" Width="60px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="EmpName" HeaderStyle-Width="105px" HeaderText="姓名" ItemStyle-HorizontalAlign="Center" SortExpression="EmpName">
                            <HeaderStyle Width="105px" />
                            <ItemStyle HorizontalAlign="Center" Width="80px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Factory" HeaderText="BU">
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" Width="120px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Depts" HeaderText="部门" ItemStyle-HorizontalAlign="Center" SortExpression="Depts">
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" Width="150px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Date" DataFormatString="{0:yyyy-MM-dd}" HeaderText="通行日期" ItemStyle-HorizontalAlign="Center" SortExpression="Date">
                            <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="StarTime" HeaderText="开始时段" ItemStyle-HorizontalAlign="Center" SortExpression="StarTime">
                            <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="EndTime" HeaderText="结束时段" ItemStyle-HorizontalAlign="Center" SortExpression="EndTime">
                            <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Type" HeaderText="申请方式" SortExpression="Type" />
                            <asp:BoundField DataField="StateName" HeaderText="狀態" SortExpression="StateName" />
                            <asp:BoundField DataField="UserName" HeaderText="創建人" SortExpression="UserName" />
                         <asp:HyperLinkField  HeaderText="详细" ControlStyle-Width="50" DataNavigateUrlFields="ID"  DataNavigateUrlFormatString="PersonalDetail.aspx?id={0}" Text='查看' >
                    
                        <ControlStyle Width="50px"></ControlStyle>
                        <ItemStyle ForeColor="#0066FF" />
                        </asp:HyperLinkField>


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
