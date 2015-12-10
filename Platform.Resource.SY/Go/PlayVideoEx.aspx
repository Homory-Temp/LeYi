<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PlayVideoEx.aspx.cs" Inherits="PlayVideoEx" %>

<%@ Register Src="~/Control/XsfxPlayerEx.ascx" TagPrefix="homory" TagName="XsfxPlayerEx" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml" style="margin: 0; padding: 0;">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <base target="_top" />
</head>
<body style="margin: 0; padding: 0;">
    <form id="form1" runat="server">
        <homory:XsfxPlayerEx runat="server" ID="player" />
    </form>
</body>
</html>
