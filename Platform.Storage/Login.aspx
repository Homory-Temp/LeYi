<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<link href="../css/m.css" rel="stylesheet" type="text/css" />
<script src="../js/jquery-1.11.0.min.js" type="text/javascript"></script>

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


<body >
    <form id="form" runat="server">
        <telerik:RadScriptManager ID="sm" runat="server"></telerik:RadScriptManager>
        <telerik:RadAjaxPanel ID="panel" runat="server">


            <div id="frame">

	<div id="top">

		<a id="logo" href="../Storage/StorageMobile"><img src="../images/home.png" align="top"></a>

		<a id="title">物资管理登录</a>

	

		<span id="list"><a href="javascript:window.history.back();"><img src="../images/goback.png" align="top"></a></span>

	</div>

	<div id="con">

		<div id="login_div">

		
		<div class="userline_info"></div>

		<div class="userline"><div class="userline_1">用户名&nbsp;&nbsp;&nbsp;&nbsp;</div><div class="userline_2"><input type="text" id="name" runat="server" placeholder="请输入用户名/手机/邮箱"/></div><div class="userline_3"><img src="../images/login.png"></div></div>

		<div class="userline_info" id="username_info"></div>

		<div class="userline"><div class="userline_1 c1">密码&nbsp;&nbsp;&nbsp;&nbsp;</div><div class="userline_2"><input id="password" runat="server" type="password" placeholder="请输入密码"/></div><div class="userline_3"><img src="../images/lock.png"></div></div>

		<div class="userline_info" id="pass_info"></div>

		<div class="login_button" ><input  type="button" id="go" runat="server" onserverclick="go_ServerClick" value="登 录" style="width:100%;height:100%;background:none;border:none;color:#ffffff;font-size:16px;"></div>



		</div>

	</div>




                             

        </telerik:RadAjaxPanel>
    </form>
</body>
</html>
