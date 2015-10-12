<%@ Page Language="C#" AutoEventWireup="true" CodeFile="HomeMobile.aspx.cs" Inherits="HomeMobile" %>

<%@ Register Src="~/Menu/MenuMobile.ascx" TagPrefix="homory" TagName="Menu" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <meta http-equiv="x-ua-compatible" content="ie=edge, chrome=1" />
    <meta name="viewport" content="width=device-width, initial-scale=1, minimum-scale=1, maximum-scale=1" />

    <title>物资管理 - 仓库首页</title>



    <!--[if lt IE 9]>
        <script src="../Assets/javascripts/html5.js"></script>
    <![endif]-->
    <!--[if (gt IE 8) | (IEMobile)]><!-->
    <meta name="viewport" content="width=device-width, initial-scale=1.0, maximum-scale=1.0, user-scalable=0">
    <meta name="apple-mobile-web-app-capable" content="yes">
    <meta name="layoutmode" content="standard">
    <meta name="apple-mobile-web-app-status-bar-style" content="black">
    <meta name="renderer" content="webkit">
    <script type="text/javascript">

        //html root的字体计算应该放在最前面，这样计算就不会有误差了/
        var _htmlFontSize = (function () {
            var clientWidth = document.documentElement ? document.documentElement.clientWidth : document.body.clientWidth;
            if (clientWidth > 640) clientWidth = 640;
            document.documentElement.style.fontSize = clientWidth * 1 / 16 + "px";
            return clientWidth * 1 / 16;
        })();



    </script>
    <link href="../css/m.css" rel="stylesheet" type="text/css" />
    <script src="../js/jquery-1.11.0.min.js" type="text/javascript"></script>
</head>
<body>
    <form id="form" runat="server">
        <telerik:RadScriptManager ID="sm" runat="server"></telerik:RadScriptManager>

        <telerik:RadAjaxPanel ID="ap" runat="server" CssClass="grid-container">
            <div id="frame">

                <div id="top">

                    <a id="logo" href="../Storage/StorageMobile">
                        <img src="../images/home.png" align="top"></a>

                <%--   <a id="home" runat="server">云物资管理</a>--%>
                    仓库切换：<telerik:RadComboBox ID="switcher" runat="server" DataTextField="Name" DataValueField="Id" AutoPostBack="true" OnSelectedIndexChanged="switcher_SelectedIndexChanged"></telerik:RadComboBox>
            

                        <span id="list"><a href="javascript:window.history.back();">
                            <img src="../images/goback.png" ></a></span>

                    </div>
                <section class="home clearfix">
                    <ul class="icon-items clearfix">
                        <li class="icon-default"><a id="cubeLink_a1_cubeNav313" runat="server">
                            <br />
                            物资盘库</a></li>
                        <li class="icon-new"><a id="cubeLink_a2_cubeNav313" runat="server">
                            <br />
                            物资查询</a></li>

                        <li class="icon-house"><a id="cubeLink_a3_cubeNav313" runat="server">
                            <br />
                            借用查询</a></li>
                        <li class="icon-sale"><a id="cubeLink_a4_cubeNav313" runat="server">
                            <br />
                            流通查询</a></li>


                    </ul>
                </section>

            </div>
        </telerik:RadAjaxPanel>
    </form>
    <script src="../Assets/javascripts/jquery.js"></script>
    <script src="../Assets/javascripts/common.js"></script>
</body>
</html>
