<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ViewAudioMin.aspx.cs" Inherits="Go_ViewAudioMin" %>

<%@ Register Src="~/Control/XsfxPlayerA.ascx" TagPrefix="homory" TagName="XsfxPlayerA" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div style="width: 100%; text-align: center;">
            <audio id="ap" runat="server" controls="controls">
            </audio>
        </div>
    </form>
</body>
</html>
