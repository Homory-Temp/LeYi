<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ViewVideoMin.aspx.cs" Inherits="Go_ViewVideoMin" %>
<%@ Register Src="~/Control/XsfxPlayerA.ascx" TagPrefix="homory" TagName="XsfxPlayerA" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div style="width: 100%; text-align: center;">
    <homory:XsfxPlayerA runat="server" ID="player" />
    </div>
    </form>
</body>
</html>
