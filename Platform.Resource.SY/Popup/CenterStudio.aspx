<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CenterStudio.aspx.cs" Inherits="Popup.PopupCenterStudio" %>

<%@ Import Namespace="System.Web.Configuration" %>

<%@ Register Src="~/Control/CommonPushX.ascx" TagPrefix="homory" TagName="CommonPushX" %>

<!DOCTYPE html>

<html>
<head runat="server">
	<title>资源呈送</title>
	<meta http-equiv="Content-Type" content="text/html; charset=UTF-8">
	<meta http-equiv="Pragma" content="no-cache">
	<script src="../Script/jquery.min.js"></script>
	<link rel="stylesheet" href="../Style/common.css">
	<link rel="stylesheet" href="../Style/common(1).css">
	<link rel="stylesheet" href="../Style/index1.css">
	<link rel="stylesheet" href="../Style/2.css" id="skinCss">

</head>
<body class="srx-phome">
	<form runat="server">
		<telerik:RadScriptManager ID="Rsm" runat="server">
			<Scripts>
				<asp:ScriptReference Assembly="Telerik.Web.UI" Name="Telerik.Web.UI.Common.Core.js" />
				<asp:ScriptReference Assembly="Telerik.Web.UI" Name="Telerik.Web.UI.Common.jQuery.js" />
				<asp:ScriptReference Assembly="Telerik.Web.UI" Name="Telerik.Web.UI.Common.jQueryInclude.js" />
			</Scripts>
		</telerik:RadScriptManager>
		<telerik:RadWindowManager runat="server" ID="Rwm" Skin="Metro">
			<Windows>
                <telerik:RadWindow ID="popup_push" runat="server" OnClientClose="pushPopped" Title="呈送" ReloadOnShow="True" Width="600" Height="300" Top="0" Left="0" ShowContentDuringLoad="false" VisibleStatusbar="false" Behaviors="Move,Close" Modal="True" CenterIfModal="False" Localization-Close="关闭">
				</telerik:RadWindow>
			</Windows>
		</telerik:RadWindowManager>
		<script>
			function popupPush(url) {
				window.radopen(url, "popup_push");
				return false;
			}
		</script>
		<telerik:RadCodeBlock runat="server">
			<script>

				function pushPopped(sender, e) {
					var toRefresh = $find("<%= pushPanel.ClientID %>");
					toRefresh.ajaxRequest("PushRefresh");
				}
			</script>
		</telerik:RadCodeBlock>
<telerik:RadAjaxPanel runat="server" ID="pushPanel" OnAjaxRequest="pushPanel_OnAjaxRequest">

								<homory:CommonPushX runat="server" ID="CommonPush" />
									</telerik:RadAjaxPanel>
	</form>
</body>
</html>













