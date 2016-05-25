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
            if ($("#phone").val().length != 11) {
                alert("请输入正确的手机号码");
                return;
            }
            var url = "../Go/IDCard";
            $.get(url, $("#idcs").val().replace("身份证号：", ""), function (d) {
                if (d == "OK") {
                    if (getCookie("client_tick") == 0) {
                        do_send();
                        setCookie("client_tick", 30);
                        $("#code_btn").css('color', 'dimgray');
                        $("#code_btn").val('重新发送（' + getCookie("client_tick") + '秒）');
                        tick_handler = setInterval(send_tick, 1000);
                    }
                } else {
                    alert("您的身份证号人事系统中不存在，请联系单位人事秘书。");
                }
            });
            return;
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

        var chars = ['0', '1', '2', '3', '4', '5', '6', '7', '8', '9'];

        function generateMixed(n) {
            var res = "";
            for (var i = 0; i < n ; i++) {
                var id = Math.ceil(Math.random() * 9);
                res += chars[id];
            }
            return res;
        }

        function do_send() {
            var no = $("#phone").val().replace("手机号码：", "").trim();
            var gen = generateMixed(6);
            $("#gen_no").val(gen);
            var msg = encodeURIComponent("验证码：" + gen + "，30分钟内有效");
            var url = "Msg.aspx?phones=" + no + "&msg=" + msg;
            var xmlhttp;
            if (window.XMLHttpRequest) {
                xmlhttp = new XMLHttpRequest();
            }
            else {
                xmlhttp = new ActiveXObject("Microsoft.XMLHTTP");
            }
            xmlhttp.open("GET", url + "&homory=" + Math.random(), true);
            xmlhttp.send();
            //$.get(url);
        }

        function send_tick() {
            if (parseInt(getCookie("client_tick")) > 1) {
                setCookie("client_tick", parseInt(getCookie("client_tick")) - 1);
                $("#code_btn").val('重新发送（' + getCookie("client_tick") + '秒）');
            }
            else {
                clearInterval(tick_handler);
                setCookie("client_tick", 0);
                $("#code_btn").css('color', 'red');
                $("#code_btn").val('发送验证码');
            }
        }

        function check_post() {
            if ($("#phone").val().length != 11) {
                alert("请输入正确的手机号码");
                return false;
            }
            if ($("#gen_no").val() == $("#code").val() && ($("#gen_no").val().trim().length == 6)) {
                return true;
            } else {
                alert("请输入手机收到的验证码");
                return false;
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
        <input type="hidden" id="xp" value="lxjy6688" />
    <div class="login-form">
        <div class="close" style="width: 100%; text-align: left;">在职教工信息登记（更正） </div>
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
                <input id="gen_no" runat="server" type="hidden" />
                <input id="idcs" runat="server" type="text" class="text" readonly="readonly" />
                <input id="p" runat="server" type="text" class="text" readonly="readonly" />
                <input id="phone" runat="server" type="text" class="text" />
                <input id="code" runat="server" type="text" class="text" />
                <input id="code_btn" name="textx" type="text" readonly="readonly" value="发送验证码" style="cursor: pointer; border: 1px solid dimgray; width: auto; text-align: center; height: auto;" onclick="send_code();" />
                <div class="signin" style="margin-top: 12px;">
                    <input id="buttonSign" runat="server" onserverclick="buttonSign_OnClick" type="submit" value="提交" onclick="return check_post();" />
                </div>
            </telerik:RadAjaxPanel>
        </form>
    </div>
    <div style="clear: both"></div>
    <br />
    <br />
    <div class="bottom2016">
        Copyright &copy;2016 梁溪区教育局  技术支持电话：82795099
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
        if (getCookie("client_tick") && getCookie("client_tick") > 0) {
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
