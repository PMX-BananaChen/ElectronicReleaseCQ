<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm2.aspx.cs" Inherits="Education2.WebForm2" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>


    <script language="javascript">

    
    </script>

    <title></title>



</head>
<body>
    
 
      <link href="Css/Vaisi_Style.css" rel="stylesheet" />
    <form id="form1" runat="server">
    <div>
    <ul>
<li><a onclick="ajaxgetlist(1);" href="javascript:void(0);" class="" id="page1">1</a></li>
<li><a onclick="ajaxgetlist(2);" href="javascript:void(0);" class="" id="page2">2</a></li>
<li><a onclick="ajaxgetlist(3);" href="javascript:void(0);" class="" id="page3">3</a></li>
<li><a onclick="ajaxgetlist(4);" href="javascript:void(0);" class="" id="page4">4</a></li>
<li><a onclick="ajaxgetlist(5);" href="javascript:void(0);" class="" id="page5">5<asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Button" />
    </a></li>
<li><a onclick="ajaxgetlist(6);" href="javascript:void(0);" class="" id="page6">6</a></li>
</ul>
    </div>
    </form>
</body>
</html>
