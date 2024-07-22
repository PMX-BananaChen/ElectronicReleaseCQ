<%@ Page Title="" Language="C#"   MasterPageFile="~/MasterPage.Master"   EnableEventValidation="false"  AutoEventWireup="true" CodeBehind="Report.aspx.cs" Inherits="Education2.Report" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

      <script type="text/javascript">

          function CheckAll(oCheckbox) {
              var GridView1 = document.getElementById("<%=gdv1.ClientID %>");
              for (i = 1; i < GridView1.rows.length - 1; i++) {

                  GridView1.rows[i].cells[0].getElementsByTagName("INPUT")[0].checked = oCheckbox.checked;
              }
          }

        function myfun()
        {
            document.getElementById("qh_con2").style.display = "block";
            document.getElementById("mynav2").className = "nav_on2";
        }
        window.onload = myfun;



    </script>

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
        .auto-style17 {
            font-size: x-large;
        }
        .auto-style18 {
            width: 100%;
            border-color: #dcdcdc;
            border-width: 0;
        }
        .auto-style32 {
            height: 19px;
        }
        #txtdatestar0 {
            width: 85px;
        }
        .auto-style40 {
            height: 19px;
            width: 162px;
        }
        .auto-style46 {
            width: 30px;
            height: 32px;
        }
        .auto-style48 {
            width: 162px;
            height: 32px;
        }
        .auto-style50 {
            height: 32px;
        }
        .auto-style51 {
            height: 19px;
            width: 30px;
        }
        .auto-style52 {
            height: 19px;
            width: 248px;
        }
        .auto-style53 {
            height: 32px;
            width: 248px;
        }
        .auto-style56 {
            height: 32px;
            width: 150px;
            text-align: center;
        }
        .auto-style58 {
            height: 19px;
            width: 150px;
        }
        .auto-style60 {
            width: 30px;
            height: 39px;
        }
        .auto-style62 {
            width: 162px;
            height: 39px;
        }
        .auto-style64 {
            height: 39px;
            width: 248px;
        }
        .auto-style65 {
            height: 39px;
            width: 150px;
            text-align: center;
        }
        .auto-style66 {
            height: 39px;
        }
        .auto-style67 {
            width: 919px;
        }
        .auto-style69 {
            height: 32px;
            width: 88px;
            text-align: center;
        }
        .auto-style70 {
            height: 39px;
            width: 88px;
            text-align: center;
        }
        .auto-style71 {
            height: 19px;
            width: 97px;
        }
        .auto-style72 {
            height: 32px;
            width: 97px;
            text-align: center;
        }
        .auto-style73 {
            height: 39px;
            width: 97px;
            text-align: center;
        }
        .auto-style74 {
            height: 19px;
            width: 88px;
        }
        </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
 
  <%--  <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <br />
      <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>--%>
            <table cellpadding="0" class="auto-style18">
                <tr>
                    <td class="auto-style51"></td>
                    <td class="auto-style74"></td>
                    <td class="auto-style40"></td>
                    <td class="auto-style71"></td>
                    <td class="auto-style52"></td>
                    <td class="auto-style58"></td>
                    <td class="auto-style32"></td>
                    <td class="auto-style32"></td>
                </tr>
                <tr>
                    <td class="auto-style46"></td>
                    <td class="auto-style69">
                        B&nbsp; U</td>
                    <td class="auto-style48">
                        <asp:DropDownList ID="ddlfactory" runat="server" Width="92px" OnSelectedIndexChanged="ddlfactory_SelectedIndexChanged" AutoPostBack="True">
                        </asp:DropDownList>
                    </td>
                    <td class="auto-style72">部 &nbsp; 門</td>
                    <td class="auto-style53">
                        <asp:DropDownList ID="ddldept" runat="server" Width="230px">
                        </asp:DropDownList>
                    </td>
                    <td class="auto-style56">工號 /姓名</td>
                    <td class="auto-style50">
                        <asp:TextBox ID="txtemp" runat="server" Width="131px"></asp:TextBox>
                    </td>
                    <td class="auto-style50">
                    </td>
                    <td class="auto-style50"></td>
                </tr>
                <tr>
                    <td class="auto-style60"></td>
                    <td class="auto-style70">狀&nbsp; 態</td>
                    <td class="auto-style62">
                        <asp:DropDownList ID="Dll_States" runat="server" Width="100px">
                        </asp:DropDownList>
                    </td>
                    <td class="auto-style73">放行日期:</td>
                    <td class="auto-style64">
                        <input id="txtdatestar" runat="server"  name="Text9" onclick="WdatePicker({ dateFmt: 'yyyyMMdd' })"  type="text"  style="width: 100px" />
                        <strong><span class="auto-style17">-<input id="txtdateend" runat="server"  name="Text10" onclick="WdatePicker({ dateFmt: 'yyyyMMdd' })" type="text"  style="width: 100px" /></span></strong></td>
                    <td class="auto-style65"><strong><span class="auto-style17">
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:ImageButton ID="ImageButton1" runat="server" Height="32px" ImageUrl="~/WebImg/Search.gif" OnClick="ImageButton1_Click" Width="64px" />
                        </span></strong></td>
                    <td class="auto-style66">
                        <strong><span class="auto-style17">
                        &nbsp;&nbsp;
                        <asp:ImageButton ID="ImageButton5" runat="server" Height="36px" ImageUrl="~/WebImg/export.gif" OnClick="ImageButton2_Click" Width="65px" />
                        </span></strong>
                    </td>
                    <td class="auto-style66"></td>
                    <td class="auto-style66"></td>
                </tr>
                </table>
         <%--   <asp:Panel ID="Panel1" runat="server" Height="350px">--%>
               
                              <br />
                      
                            <div style="margin: 0 auto; " class="auto-style67">
                              <div id="div1" runat="server" style="overflow:scroll;width:100%; height:480px;">
                            <asp:GridView ID="gdv1" runat="server" AllowPaging="True" AutoGenerateColumns="False" BackColor="#00CCFF" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="2" DataKeyNames="ID" ForeColor="Black" GridLines="Horizontal" OnPageIndexChanging="gridView_PageIndexChanging" OnRowCreated="gridView_OnRowCreated" OnRowDataBound="gridView_RowDataBound" PageSize="15" RowStyle-HorizontalAlign="Center" Width="1500px">
                                <Columns>
                                    <asp:BoundField>
                                    <HeaderStyle Width="80px" />
                                    <ItemStyle Width="80px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="EmpNo" HeaderText="工号" ItemStyle-HorizontalAlign="Center" SortExpression="EmpNo">
                                    <HeaderStyle Height="35px" />
                                    <ItemStyle Height="25px" HorizontalAlign="Center" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="EmpName" HeaderStyle-Width="105px" HeaderText="姓名" ItemStyle-HorizontalAlign="Center" SortExpression="EmpName">
                                    <HeaderStyle Width="105px" />
                                    <ItemStyle HorizontalAlign="Center"  />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Factory" HeaderText="BU">
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Depts" HeaderText="部门" ItemStyle-HorizontalAlign="Center" SortExpression="Depts">
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center"  />
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
                                    <asp:BoundField DataField="Remark" HeaderText="出行原因" SortExpression="Remark" />
                                    <asp:BoundField DataField="AuditUserName" HeaderText="簽核人" />
                                    <asp:BoundField DataField="AuditDate" HeaderText="簽核時間" DataFormatString="{0:yyyy-MM-dd HH:mm}" />
                                    <asp:BoundField DataField="BackUserName" HeaderText="退回人" />
                                    <asp:BoundField DataField="BackRemark" HeaderText="退回原因" />
                                    <asp:BoundField DataField="BackDate" HeaderText="退回時間" DataFormatString="{0:yyyy-MM-dd HH:mm}" />
                                </Columns>
                                <FooterStyle BackColor="#CCCC99" ForeColor="Black" />
                                <HeaderStyle BackColor="#16ACFF" Font-Bold="True" ForeColor="White" Height="25px" />
                                <PagerStyle BackColor="White" BorderStyle="None" CssClass="bubufxPagerCss" ForeColor="Black" HorizontalAlign="Left" />
                                <RowStyle HorizontalAlign="Center" BackColor="White" />
                                <SelectedRowStyle BackColor="#CC3333" Font-Bold="True" ForeColor="White" />
                                <SortedAscendingCellStyle BackColor="#F7F7F7" />
                                <SortedAscendingHeaderStyle BackColor="#4B4B4B" />
                                <SortedDescendingCellStyle BackColor="#E5E5E5" />
                                <SortedDescendingHeaderStyle BackColor="#242121" />
                            </asp:GridView>
                                     </div> 
                                </div> 
        
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
             <%-- </ContentTemplate>
    </asp:UpdatePanel>--%>
</asp:Content>
