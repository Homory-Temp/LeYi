<%@ Page Language="C#" AutoEventWireup="true" CodeFile="C6Action.aspx.cs" Inherits="Go.GoC6Action" %>

<%@ Register Src="~/Control/C6Action.ascx" TagPrefix="homory" TagName="C6Action" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <title>资源平台</title>
    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8">
    <meta http-equiv="Pragma" content="no-cache">
    <link href="../Style/common.css" rel="stylesheet" />
    <link href="../Style/article.css" rel="stylesheet" />
    <link href="../Style/plaza.css" rel="stylesheet" />
    <script src="../Script/JQ.js"></script>
    <script src="../Script/jquery.tab.js"></script>
    <script src="../Script/logger.js"></script>
    <script src="../Script/zzsc.js"></script>
    <script src="../Script/jquery.min1.js"></script>
    <script src="../Script/jquery.min.js"></script>
    <script type="text/javascript" src="js/JQ.js"></script>
    <script type="text/javascript" src="js/zzsc.js"></script>
    <script src="js/jquery.min.js"></script>
    <script src="js/logger.js"></script>
    <script src="js/bds_s_v2.js"></script>
</head>
<body class="srx-plogin">
    <form runat="server">
        <telerik:RadScriptManager ID="Rsm" runat="server">
            <Scripts>
                <asp:ScriptReference Assembly="Telerik.Web.UI" Name="Telerik.Web.UI.Common.Core.js" />
                <asp:ScriptReference Assembly="Telerik.Web.UI" Name="Telerik.Web.UI.Common.jQuery.js" />
                <asp:ScriptReference Assembly="Telerik.Web.UI" Name="Telerik.Web.UI.Common.jQueryInclude.js" />
            </Scripts>
        </telerik:RadScriptManager>
        <homory:C6Action runat="server" ID="C6Action" />
    </form>
</body>
</html>
