<%@ Page Language="C#" AutoEventWireup="true" CodeFile="查询.aspx.cs" Inherits="VIP_查询" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>查询</title>
</head>
<body style="text-align: center;">
    <form id="form" runat="server">
        <telerik:RadScriptManager ID="sm" runat="server" LoadScriptsBeforeUI="true" EnablePartialRendering="true">
            <Scripts>
                <asp:ScriptReference Assembly="Telerik.Web.UI" Name="Telerik.Web.UI.Common.Core.js" />
                <asp:ScriptReference Assembly="Telerik.Web.UI" Name="Telerik.Web.UI.Common.jQuery.js" />
                <asp:ScriptReference Assembly="Telerik.Web.UI" Name="Telerik.Web.UI.Common.jQueryPlugins.js" />
            </Scripts>
        </telerik:RadScriptManager>
        <h4>本页面每10秒自动刷新....</h4>
        <a href="机构.aspx">选机构</a>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<a href="用户.aspx">选用户</a>
        <telerik:RadAjaxPanel ID="ap" runat="server">
            <asp:Timer ID="timer" runat="server" Enabled="true" Interval="10000" OnTick="timer_Tick"></asp:Timer>
            <div style="margin-top: 120px;">
                <h1 id="info" runat="server"></h1>
            </div>
        </telerik:RadAjaxPanel>
    </form>
</body>
</html>
