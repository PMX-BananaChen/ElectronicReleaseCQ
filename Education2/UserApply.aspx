<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="UserApply.aspx.cs" Inherits="Education2.UserApply" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .auto-style2 {
            border-collapse: collapse;
            width: 100%;
            border-color: #dcdcdc;
            border-width: 0;
            height: 70px;
        }

        .auto-style4 {
            height: 32px;
            text-align: center;
        }

        .auto-style9 {
            width: 319px;
        }

        .auto-style23 {
            height: 32px;
            text-align: center;
            width: 126px;
        }

        .auto-style28 {
            height: 32px;
            text-align: center;
            width: 182px;
        }

        .auto-style29 {
            height: 32px;
            text-align: center;
            width: 232px;
        }

        .auto-style51 {
            height: 65px;
            font-size: x-large;
            color: #FF0000;
        }

        .auto-style52 {
            width: 181px;
        }

        #txtdatestar {
            width: 118px;
        }

        #txttimestar {
            width: 118px;
        }

        #txtdateend {
            width: 118px;
        }

        #txttimeend {
            width: 118px;
        }

        .auto-style95 {
            background: url('WebImg/main_61.gif') repeat-x 50% center;
            border: 1px solid #74BFE7;
            color: White;
            padding: 2px;
            font-size: small;
        }

        .auto-style96 {
            width: 100%;
            height: 35px;
        }

        .auto-style98 {
            text-align: center;
            width: 178px;
            font-size: small;
        }



        #txtdatestar0 {
            width: 118px;
        }

        #txtdateend0 {
            width: 118px;
        }

        .auto-style101 {
            width: 163px;
        }

        .auto-style103 {
            width: 108px;
            font-size: small;
        }

        .auto-style108 {
            width: 172px;
        }

        .auto-style110 {
            text-align: center;
            width: 140px;
            font-size: small;
        }

        .auto-style111 {
            font-size: small;
            height: 20px;
        }
        .auto-style112 {
            width: 53px;
        }
    </style>



    <script type="text/javascript">
        function myfun() {
            document.getElementById("qh_con0").style.display = "block";
            document.getElementById("mynav0").className = "nav_on2";
        }
        window.onload = myfun;

    </script>
    <link href="Css/btn_style.css" rel="stylesheet" />
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
                else if (days > 30) {

                    alert("選擇的日期時間段不能超過30天！");
                    document.getElementById('ContentPlaceHolder1_txtdateend').value = "";
                    return false;
                }
            }
        }

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
                if (fd > td && td != "") {
                    alert("選擇的時段不能小於開始時段！");
                    document.getElementById('ContentPlaceHolder1_txttimeend').value = "";
                    return false;
                }
            }
        }

        //function KeyDown()
        //{
        //    if (event.keyCode == 13)
        //    {
        //        alert("test！");
        //    }
        //}


    </script>




</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <br />
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>


            <table cellspacing="1" class="auto-style2">
                <tr>
                    <td class="auto-style51" style="text-align: center">
                        <strong>放 行 條 申 請</strong></td>
                </tr>
            </table>
            <table cellpadding="0" cellspacing="0" class="auto-style96">
                <tr>
                    <td class="auto-style103">&nbsp;</td>
                    <td class="auto-style98">工&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 號</td>
                    <td class="auto-style108">
                        <asp:TextBox ID="txtempno" runat="server" Width="140px" MaxLength="15" AutoPostBack="True" OnTextChanged="txtempno_TextChanged" CssClass="auto-style111"></asp:TextBox>
                    </td>
                    <td class="auto-style110">姓&nbsp;&nbsp;&nbsp; &nbsp; 名</td>
                    <td class="auto-style101">
                        <asp:TextBox ID="txtempname" runat="server" Width="140px" MaxLength="15" CssClass="auto-style111"></asp:TextBox>
                    </td>
                    <td>
                        <asp:Button ID="btnadd0" runat="server" CssClass="auto-style95" OnClick="btnadd0_Click" Text="批量導入" Height="22px" Width="61px" />
                    </td>
                </tr>
            </table>
            <br class="auto-style111" />
            <table cellpadding="0" cellspacing="0" class="auto-style96">
                <tr>
                    <td class="auto-style103">&nbsp;</td>
                    <td class="auto-style98">B &nbsp;&nbsp;&nbsp; &nbsp; U</td>
                    <td class="auto-style108">

                        <asp:DropDownList ID="ddlFactory" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlFactory2_SelectedIndexChanged" Width="140px" CssClass="auto-style111" Enabled="False">
                        </asp:DropDownList>

                    </td>
                    <td class="auto-style110">部&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 門</td>
                    <td>

                        <asp:DropDownList ID="ddlDept" runat="server" Width="250px" OnSelectedIndexChanged="ddlDept_SelectedIndexChanged" CssClass="auto-style111" Enabled="False">
                        </asp:DropDownList>

                    </td>
                </tr>
            </table>
            <br class="auto-style111" />
            <table cellpadding="0" cellspacing="0" class="auto-style96">
                <tr>
                    <td class="auto-style103">&nbsp;</td>
                    <td class="auto-style98">郵&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 箱</td>
                    <td>
                        <asp:TextBox ID="txtmail" runat="server" Height="16px" Width="456px" MaxLength="45" Enabled="False" CssClass="auto-style111"></asp:TextBox>
                    </td>
                </tr>
            </table>
            <br class="auto-style111" />
            <table cellpadding="0" cellspacing="0" class="auto-style96">
                <tr>
                    <td class="auto-style103">&nbsp;</td>
                    <td class="auto-style98">出行原因</td>
                    <td>
                        <asp:TextBox ID="txtremark" runat="server" Height="16px" Width="457px" MaxLength="50" CssClass="auto-style111"></asp:TextBox>
                    </td>
                </tr>
            </table>
            <br class="auto-style111" />
            <table cellpadding="0" cellspacing="0" class="auto-style96">
                <tr>
                    <td class="auto-style103">&nbsp;</td>
                    <td class="auto-style98">開始日期</td>
                    <td class="auto-style108">
                        <input id="txtdatestar" runat="server" name="Text9"  onclick="WdatePicker({ dateFmt: 'yyyyMMdd' })" onfocus="WdatePicker({ minDate:'%y-%M-#{%d-1}',dateFmt: 'yyyyMMdd'  })" type="text"  style="width: 140px" class="auto-style111" /></td>

                    <%--<input id="txtdatestar" runat="server" name="Text9" onclick="WdatePicker({ dateFmt: 'yyyyMMdd' })"  onfocus="WdatePicker({ minDate:'%y-%M-#{%d-1}',dateFmt: 'yyyyMMdd'  })" type="text" style="width: 100px" />--%>

                    <td class="auto-style110">結束日期</td>
                    <td>
                        <input id="txtdateend" runat="server" name="Text11" onclick="WdatePicker({ dateFmt: 'yyyyMMdd' }), dateDiff()"  onfocus="WdatePicker({ minDate:'%y-%M-#{%d-1}',dateFmt: 'yyyyMMdd'  })" type="text" style="width: 140px" class="auto-style111" /></td>
                    <td class="auto-style111">&nbsp;</td>
                </tr>
            </table>
            <br class="auto-style111" />
            <table cellpadding="0" cellspacing="0" class="auto-style96">
                <tr>
                    <td class="auto-style103">&nbsp;</td>
                    <td class="auto-style98">開始時段</td>
                    <td class="auto-style108">
                        <input id="txttimestar" runat="server" class="auto-style111"  name="Text10" onclick="WdatePicker({ dateFmt: 'HHmm' })" type="text"  style="width: 140px" /></td>
                    <td class="auto-style110">結束時段</td>
                    <td class="auto-style101">
                        <input id="txttimeend" runat="server" class="auto-style111"  name="Text12" onclick="WdatePicker({ dateFmt: 'HHmm' }), dateDiffHours()" type="text"  style="width: 140px" /></td>
                    <td class="auto-style112">
                        <asp:Button ID="btnadd" runat="server" CssClass="auto-style95" OnClick="btnadd_Click" Text="預覽" Style="height: 26px" />
                    </td>
                    <td>
                        <asp:Label ID="lb1" runat="server" ForeColor="#FF3300"></asp:Label>
                    </td>
                </tr>
            </table>
            <br />
            <table cellspacing="1" class="auto-style2">
                <tr>
                    <td class="auto-style52">
                        <br />
                        <br />
                    </td>
                    <td>
                        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" BorderColor="Silver" BorderStyle="Solid" DataKeyNames="ID" OnRowCancelingEdit="GridView1_RowCancelingEdit" OnRowDataBound="GridView1_RowDataBound" OnRowDeleting="GridView1_RowDeleting" OnRowEditing="GridView1_RowEditing" OnRowUpdating="GridView1_RowUpdating" Style="margin-right: 0px" Width="566px">
                            <Columns>
                                <asp:BoundField DataField="Date" HeaderText="日期">
                                    <HeaderStyle BorderColor="Silver" Height="30px" HorizontalAlign="Center" VerticalAlign="Middle" Width="110px" />
                                    <ItemStyle BorderColor="Silver" Height="30px" HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Star" HeaderText="開始時段">
                                    <HeaderStyle BorderColor="Silver" HorizontalAlign="Center" Width="130px" />
                                    <ItemStyle BorderColor="Silver" HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="End" HeaderText="結束時段">
                                    <HeaderStyle BorderColor="Silver" HorizontalAlign="Center" Width="120px" />
                                    <ItemStyle BorderColor="Silver" HorizontalAlign="Center" Width="120px" />
                                </asp:BoundField>
                                <asp:CommandField DeleteText="刪除" HeaderText="刪除" ShowDeleteButton="True">
                                    <HeaderStyle BorderColor="Silver" HorizontalAlign="Center" Width="120px" />
                                    <ItemStyle BorderColor="Silver" HorizontalAlign="Center" Width="120px" />
                                    <%--  <ItemStyle HorizontalAlign="Center" />--%>
                                </asp:CommandField>
                            </Columns>
                        </asp:GridView>
                    </td>
                    <td>&nbsp;</td>
                </tr>
            </table>
            <table cellspacing="1" class="auto-style2">
                <tr>
                    <td class="auto-style23">&nbsp;&nbsp;</td>
                    <td class="auto-style28">&nbsp;</td>
                    <td class="auto-style29">
                        <asp:Button ID="btnok" runat="server" CssClass="Button02" OnClick="Button1_Click" Text=" 提 交 " Visible="False" />
                    </td>
                    <td class="auto-style9">
                        <asp:Button ID="btnback" runat="server" CssClass="Button02" Text=" 重 置 " OnClick="btnback_Click" Visible="False" />
                    </td>
                    <td class="、">&nbsp;</td>
                    <td class="auto-style4">&nbsp;</td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
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
