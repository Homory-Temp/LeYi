<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Register.aspx.cs" Inherits="Go.GoRegister" %>

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
    <!--[if lt IE 9]>
	    <script src="../Content/Homory/js/respond.min.js"></script>
    <![endif]-->
</head>
<body>
	<form id="form" runat="server">
		<telerik:RadScriptManager runat="server"></telerik:RadScriptManager>
		<telerik:RadAjaxManager ID="ajaxManager" runat="server" EnablePageHeadUpdate="false">
			<AjaxSettings>
				<telerik:AjaxSetting AjaxControlID="ajaxManager">
					<UpdatedControls>
						<telerik:AjaxUpdatedControl ControlID="viewer" />
						<telerik:AjaxUpdatedControl ControlID="upload" />
					</UpdatedControls>
				</telerik:AjaxSetting>
			</AjaxSettings>
		</telerik:RadAjaxManager>
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
						<div class="ui active step">
							填写注册信息
						</div>
						<div class="ui step">
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
			<div class="ui two column stackable page grid">
				<div class="column">
					<div class="ui form segment">
						<label class="ui teal ribbon label width100">电子邮箱</label>
						<div class="field padTop15">
							<div class="ui left labeled icon input">
								<input id="userEmail" runat="server" type="text" value="" maxlength="256" />
								<i class="mail icon"></i>
								<div class="ui corner label">
									<i class="icon asterisk"></i>
								</div>
							</div>
						</div>
						<label class="ui teal ribbon label width100">密码</label>
						<div class="field padTop15">
							<div class="ui left labeled icon input">
								<input id="userPassword" type="password" runat="server" value="" maxlength="256" />
								<i class="lock icon"></i>
								<div class="ui corner label">
									<i class="icon asterisk"></i>
								</div>
							</div>
						</div>
						<label class="ui teal ribbon label width100">密码确认</label>
						<div class="field padTop15">
							<div class="ui left labeled icon input">
								<input id="userPasswordRepeat" type="password" value="" runat="server" maxlength="256" />
								<i class="lock icon"></i>
								<div class="ui corner label">
									<i class="icon asterisk"></i>
								</div>
							</div>
						</div>
						<label class="ui teal ribbon label width100">昵称</label>
						<div class="field padTop15">
							<div class="ui left labeled icon input">
								<input id="userName" type="text" value="" maxlength="64" runat="server" />
								<i class="user icon"></i>
							</div>
						</div>
					</div>
				</div>
				<div class="column">
					<div class="ui form segment">
						<label class="ui teal ribbon label width100">真实姓名</label>
						<div class="field padTop15">
							<div class="ui left labeled icon input">
								<input id="userRealName" type="text" value="" maxlength="16" runat="server" />
								<i class="phone icon"></i>
							</div>
						</div>
						<label class="ui teal ribbon label width100">个人头像</label>
						<div class="field padTop15">
							<div>
								<telerik:RadScriptBlock runat="server">
									<script type="text/javascript">
										function OnClientFilesUploaded(sender, args) {
											$find('<%=ajaxManager.ClientID %>').ajaxRequest();
										}
									</script>
								</telerik:RadScriptBlock>
								<asp:Image ID="viewer" runat="server" Width="100" Height="100" />
								<telerik:RadAsyncUpload ID="upload" runat="server" MultipleFileSelection="Disabled" Skin="BlackMetroTouch" HideFileInput="true" AutoAddFileInputs="false" TemporaryFolder="~/Common/头像/用户" TargetFolder="~/Common/头像/用户" Localization-Cancel="取消" Localization-Remove="移除" Localization-Select="选择" OnClientFilesUploaded="OnClientFilesUploaded" OnFileUploaded="upload_FileUploaded">
								</telerik:RadAsyncUpload>
							</div>
						</div>
						<label class="ui teal ribbon label width100">个人描述</label>
						<div class="field padTop15">
							<div class="ui left labeled icon input">
								<input id="userDescription" runat="server" type="text" value="" maxlength="1024" />
								<i class="pencil icon"></i>
							</div>
						</div>
					</div>
				</div>
			</div>
			<div class="ui page grid">
				<div class="center aligned column">
					<asp:Button ID="buttonRegister" runat="server" CssClass="ui teal circular button" Text="注册" OnClick="buttonRegister_OnClick"></asp:Button>
					&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Button ID="buttonBack" runat="server" CssClass="ui teal circular button" Text="返回" OnClick="buttonBack_OnClick"></asp:Button>
				</div>
			</div>
		</telerik:RadAjaxPanel>
	</form>
</body>
</html>
