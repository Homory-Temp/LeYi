<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ToVerify.aspx.cs" Inherits="Go.GoToVerify" %>

<!DOCTYPE html>

<html>
<head runat="server">
	<meta charset="utf-8" />
	<meta http-equiv="X-UA-Compatible" content="IE=Edge,Chrome=1" />
	<meta name="viewport" content="width=device-width,initial-scale=1,minimum-scale=1,maximum-scale=1" />
	<title>注册</title>
	<link href="../Content/Semantic/css/semantic.min.css" rel="stylesheet" />
	<link href="../Content/Homory/css/common.css" rel="stylesheet" />
	<link href="../Content/Sso/css/sign.css" rel="stylesheet" />
	<link href="../Content/Sso/css/register.css" rel="stylesheet" />
	<script src="../Content/jQuery/jquery.min.js"></script>
	<script src="../Content/Semantic/javascript/semantic.min.js"></script>
	<script src="../Content/Homory/js/common.js"></script>
	<script src="../Content/Homory/js/notify.min.js"></script>
	<script src="../Content/Sso/js/toVerify.js"></script>
    <!--[if lt IE 9]>
	    <script src="../Content/Homory/js/respond.min.js"></script>
    <![endif]-->
</head>
<body>
	<form id="form" runat="server">
		<telerik:RadScriptManager runat="server"></telerik:RadScriptManager>
		<div class="ui page grid">
			<div class="column">
				<img class="ui image" src="../Common/配置/SsoLogo.png" />
			</div>
		</div>
		<div class="ui page grid">
			<div class="column">
				<div class="ui teal inverted center aligned segment">
					<label>用户注册</label>
				</div>
			</div>
		</div>
		<div class="ui page grid">
			<div class="column">
				<div class="ui center aligned column">
					<div class="ui small steps">
						<div class="ui step">
							填写注册信息
						</div>
						<div class="ui active step">
							验证注册邮箱
						</div>
						<div class="ui step">
							用户注册成功
						</div>
					</div>
				</div>
			</div>
		</div>
		<telerik:RadAjaxPanel ID="areaAction" runat="server">
			<div class="ui two column stackable center aligned page grid">
				<div class="column">
					<div class="ui success icon message">
						<i class="mail icon"></i>
						<div class="content">
							<div class="header">
								验证邮箱后即可成为平台用户啦！
							</div>
							<br />
							<asp:Button ID="toSend" runat="server" CssClass="ui small green button" Text="发送验证邮件" OnClick="toSend_OnClick"></asp:Button>
							<div id="sent" class="ui small green button" style="display: none;"><span>验证邮件已发送，（<span id="tick">30</span>）秒后可重新发送。</span></div>
						</div>
					</div>
				</div>
			</div>
			<telerik:RadButton ID="postRedo" runat="server" Style="display: none;" Text="Redo" AutoPostBack="True" OnClick="postRedo_OnClick" CommandName="allowReSend" />
			<telerik:RadScriptBlock runat="server">
				<script type="text/javascript">
					function reVerify() {
						$("#<%=postRedo.ClientID%>").click();
					}
				</script>
			</telerik:RadScriptBlock>
		</telerik:RadAjaxPanel>
	</form>
</body>
</html>
