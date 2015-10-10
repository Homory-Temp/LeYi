<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PlayVideoX.aspx.cs" Inherits="Go_PlayVideoX" %>

<%@ Register Src="~/Control/XsfxPlayerX.ascx" TagPrefix="homory" TagName="XsfxPlayerX" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml" style="margin:0;padding:0;">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <base target="_top" />
</head>
<body style="text-align: center; width: 100%;margin:0;padding:0;">
    <form id="form1" runat="server">
    <div style="margin:0;padding:0;width:680px;height:444px;overflow:hidden;">
        <homory:XsfxPlayerX runat="server" ID="player" />
    </div>
    </form>
</body>
</html>
