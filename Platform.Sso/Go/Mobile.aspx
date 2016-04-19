<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Mobile.aspx.cs" Inherits="Go.GoMobile" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Login</title>
    <meta name="viewport" content="width=device-width, initial-scale=1"/>
    <script type="application/x-javascript"> addEventListener("load", function() { setTimeout(hideURLbar, 0); }, false); function hideURLbar(){ window.scrollTo(0,1); } </script>
    <link href="css/style.css" rel='stylesheet' type='text/css' />

    <script type="text/javascript" src="js/jquery.min.js"></script>

</head>
<body>

    <h1>
        <img alt="" src="images/SsoLogoLX.png" /></h1>
    <div class="login-form">
        <div class="close">用户登录 </div>
        <div class="head-info">
            <label class="lbl-1"></label>
            <label class="lbl-2"></label>
            <label class="lbl-3"></label>
        </div>
        <div class="clear"></div>
        <div class="avtar">
            <img alt="" src="images/avtar.png" /></div>
        <form runat="server">
            <telerik:RadScriptManager runat="server"></telerik:RadScriptManager>
        <telerik:RadAjaxPanel ID="areaAction" runat="server">
            <input id="userName" runat="server" type="text" class="text" value="用户名" onfocus="if(this.value=='用户名') this.value = '';" onblur="if (this.value == '') {this.value = '用户名';}"/>
            <div id="kDiv" class="">
                <input id="userPassword" runat="server" type="password" value="密码" onkeypress="pwdInput();" /></div>
        <div class="signin">
            <input id="buttonSign" runat="server" onserverclick="buttonSign_OnClick" type="submit" value="登录" /></div>
        </telerik:RadAjaxPanel>
                                <telerik:RadCodeBlock runat="server">
                                    <script type="text/javascript">
                                        function GetUrlParms() {
                                            var args = new Object();
                                            var query = location.search.substring(1);
                                            var pairs = query.split("&");
                                            for (var i = 0; i < pairs.length; i++) {
                                                var pos = pairs[i].indexOf('=');
                                                if (pos == -1) continue;
                                                var argname = pairs[i].substring(0, pos);
                                                var value = pairs[i].substring(pos + 1);
                                                args[argname] = unescape(value);
                                            }
                                            return args;
                                        }
                                        var args = new Object();
                                        args = GetUrlParms();
                                        var u = args["Name"];
                                        var p = args["Password"];
                                        if (u) {
                                            if (p) {
                                                $("#userName").val(decodeURIComponent(u));
                                                $("#userPassword").val(decodeURIComponent(p));
                                                __doPostBack("<%= buttonSign.ClientID %>", "");
                                            }
                                        }

                                        function pwdError() {
                                            $("#userPassword").val('');
                                            $("#kDiv").addClass("key");
                                            $("#userPassword").focus();
                                        }

                                        function pwdInput() {
                                            $("#kDiv").removeClass("key");
                                        }
                                    </script>
                                </telerik:RadCodeBlock>
        </form>
    </div>
    <div style="clear: both"></div>
    <br />
    <br />
    <div class="bottom2016">
        Homory Temp
    </div>
<style>
  html input:-webkit-autofill, html textarea:-webkit-autofill, html select:-webkit-autofill {
    background-color: transparent;
  }
</style>
</body>
</html>
