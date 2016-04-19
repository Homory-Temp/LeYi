<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Mobile.aspx.cs" Inherits="Go.GoMobile" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Login</title>
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <script type="application/x-javascript"> addEventListener("load", function() { setTimeout(hideURLbar, 0); }, false); function hideURLbar(){ window.scrollTo(0,1); } </script>
    <link href="css/style.css" rel='stylesheet' type='text/css' />

    <script type="text/javascript" src="js/jquery.min.js"></script>

    <script type="text/javascript">
        var cookieDays = 30;

        var tick_handler;

        function send_code() {
            if (getCookie("client_tick") == 0) {
                setCookie("client_tick", 30);
                $("#code_btn").css('color', 'dimgray');
                $("#code_btn").val('重新发送（' + getCookie("client_tick") + '秒）');
                tick_handler = setInterval(send_tick, 1000);
            }
        }

        function setCookie(name, value) {
            var exp = new Date();
            exp.setTime(exp.getTime() + cookieDays * 24 * 60 * 60 * 1000);
            document.cookie = name + "=" + escape(value) + ";expires=" + exp.toGMTString();
        }

        function getCookie(name) {
            var arr, reg = new RegExp("(^| )" + name + "=([^;]*)(;|$)");
            if (arr = document.cookie.match(reg))
                return unescape(arr[2]);
            else
                return null;
        }

        function send_tick() {
            if ( parseInt(getCookie("client_tick")) > 1) {
                setCookie("client_tick", parseInt(getCookie("client_tick"))-1);
                $("#code_btn").val('重新发送（' + getCookie("client_tick") + '秒）');
            }
            else {
                clearInterval(tick_handler);
                setCookie("client_tick", 0);
                $("#code_btn").css('color', 'red');
                $("#code_btn").val('发送验证码');
            }
        }
    </script>
</head>
<body>
    <script type="text/javascript">
        if (getCookie("client_tick") == null)
            setCookie("client_tick", 0);
    </script>
    <h1>
        <img alt="" src="images/SsoLogoLX.png" /></h1>
    <div class="login-form">
        <div class="close" style="width: 100%; text-align: left;">用户初始化（<span id="idcs" runat="server"></span>） </div>
        <div class="head-info">
            <label class="lbl-1"></label>
            <label class="lbl-2"></label>
            <label class="lbl-3"></label>
        </div>
        <div class="clear"></div>
        <div class="avtar" style="display: none;">
            <img alt="" src="images/avtar.png" />
        </div>
        <form runat="server">
            <telerik:RadScriptManager runat="server"></telerik:RadScriptManager>
            <telerik:RadAjaxPanel ID="areaAction" runat="server">
                <input id="phone" runat="server" type="text" class="text" />
                <input id="reset" runat="server" type="text" class="text" />
                <input id="code" runat="server" type="text" class="text" />
                <input id="code_btn" name="textx" type="text" readonly="readonly" value="发送验证码" style="cursor: pointer; border: 1px solid dimgray; width: auto; text-align: center; height: auto;" onclick="send_code();" />
                <div class="signin" style="margin-top: 12px;">
                    <input id="buttonSign" runat="server" onserverclick="buttonSign_OnClick" type="submit" value="保存" />
                </div>
            </telerik:RadAjaxPanel>
            <telerik:RadCodeBlock runat="server">
                <script type="text/javascript">
                    function pwdError() {
                    }

                    //if (!getCookie("client_tick")) {
                    //    s_calc = 0;
                    //    setCookie("client_tick", 0);
                    //} else {
                    //    s_calc = getCookie("client_tick");
                    //}

                    //if (s_calc == 0) {
                    //    $("#code_btn").css('color', 'red');
                    //    $("#code_btn").val('发送验证码');
                    //} else {
                    //    $("#code_btn").css('color', 'dimgray');
                    //    $("#code_btn").val('重新发送（' + s_calc + '秒）');
                    //    setInterval(send_tick, 1000);
                    //}
                </script>
            </telerik:RadCodeBlock>
        </form>
    </div>
    <div style="clear: both"></div>
    <br />
    <br />
    <div class="bottom2016">
        Copyright &copy;2016 梁溪教育筹备组  技术支持：北京金和网络
    </div>
    <style>
        html input:-webkit-autofill, html textarea:-webkit-autofill, html select:-webkit-autofill {
            background-color: transparent;
        }

        html input[type=text] {
            color: dimgray;
        }

        html input[name=textx] {
            color: red;
            background-image: none;
            background-color: transparent;
        }
    </style>
    <script type="text/javascript">
        if (getCookie("client_tick")) {
            $("#code_btn").css('color', 'dimgray');
            $("#code_btn").val('重新发送（' + getCookie("client_tick") + '秒）');
            tick_handler = setInterval(send_tick, 1000);
        } else {
            $("#code_btn").css('color', 'red');
            $("#code_btn").val('发送验证码');
        }
    </script>
</body>
</html>
