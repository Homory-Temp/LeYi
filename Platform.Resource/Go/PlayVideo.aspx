<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PlayVideo.aspx.cs" Inherits="Go_PlayVideo" %>

<%@ Register Src="~/Control/XsfxPlayerCut.ascx" TagPrefix="homory" TagName="XsfxPlayerCut" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <base target="_top" />
</head>
<body style="text-align: center; width: 100%; margin: 0; padding: 0;">
    <form id="form1" runat="server">
    <div style="margin: 0; padding: 0;">
        <homory:XsfxPlayerCut runat="server" ID="player" />
    </div>
    </form>
</body>
</html>
