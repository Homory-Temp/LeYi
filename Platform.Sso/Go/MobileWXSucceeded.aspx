<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MobileWXSucceeded.aspx.cs" Inherits="Go.GoMobileWXSucceeded" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Login</title>
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <script type="application/x-javascript"> addEventListener("load", function() { setTimeout(hideURLbar, 0); }, false); function hideURLbar(){ window.scrollTo(0,1); } </script>
    <link href="css/style.css" rel='stylesheet' type='text/css' />

    <script type="text/javascript" src="js/jquery.min.js"></script>
</head>
<body style="min-height: 600px;">
    <script type="text/javascript">
        if (getCookie("client_tick") == null)
            setCookie("client_tick", 0);
    </script>
    <h1 style="padding-top: 20px;">
        <img alt="" src="images/SsoLogoLX.png" style="width: 90%;" /></h1>
    <div class="login-form" style="margin: 30px auto 0 auto; width: 86%;">
        <div class="close" style="width: 100%; text-align: left;">在职教工信息提交</div>
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
                <input id="code_btn" name="textx" type="text" readonly="readonly" value="提交成功" style="cursor: pointer; border: 1px solid dimgray; width: 50%; text-align: center; height: auto;" onclick="send_code();" /><br /><br />
            </telerik:RadAjaxPanel>
        </form>
    </div>
    <div style="clear: both"></div>
    <br />
    <br />
    <div class="bottom2016">
        Copyright &copy;2016 梁溪教育筹备组<br />技术支持：北京金和网络
    </div>
    <style>
        html input:-webkit-autofill, html textarea:-webkit-autofill, html select:-webkit-autofill {
            background-color: transparent;
        }

        html input[type=text] {
            color: dimgray;
        }

        html input[name=textx] {
            color: green;
            background-image: none;
            background-color: transparent;
        }
    </style>
</body>
</html>
