<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PublishPush.aspx.cs" Inherits="Popup.PopupPublishPush" %>

<link href="../Style/common.css" rel="stylesheet" />

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
	<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
	<title>呈送</title>
	<style>
		.relPos {
			position: relative;
		}
	</style>
</head>
<body>
	<form id="form" runat="server">
		<telerik:RadScriptManager runat="server" ID="sm"></telerik:RadScriptManager>
		<telerik:RadAjaxPanel runat="server" ID="popup_publish_push_panel">
			<div style="margin:10px 0px 10px 0px;"><input type="text" id="filter" runat="server" />
			<a id="filterGo" runat="server" onserverclick="filterGo_OnServerClick" style="height:15px;">搜索</a></div>
			<asp:Repeater runat="server" ID="result">
				<HeaderTemplate>
					<table style="margin-right: 4px;">
				</HeaderTemplate>
				<ItemTemplate>
					<tr>
						<td height="25">
							<img src='<%# string.Format("../Image/img/{0}.jpg", Eval("Thumbnail")) %>' width="16" height="16" />
						</td>
						<td>&nbsp;<%# Eval("Title") %> 
						</td>
						<td style="width: 150px; text-align: center;">&nbsp;时间：
														     <em><%# ((DateTime)Eval("Time")).ToString("yyyy-MM-dd") %></em>
						</td>
						<td  style="width: 70px; text-align: right;">
							<a id="btnDo" itemid='<%# Eval("Id") %>' runat="server" OnServerClick="btnDo_OnServerClick"><%# ((Homory.Model.Resource)Container.DataItem).ResourceCatalog.Count(o=>o.CatalogId==CatalogId && o.State == Homory.Model.State.启用)==0?"呈送":"取消呈送" %></a>
						</td>
					</tr>
				</ItemTemplate>
				<FooterTemplate>
					</table>
				</FooterTemplate>
			</asp:Repeater>
		</telerik:RadAjaxPanel>
          <div style="margin:10px 0px 0px 20px;">
		<a onclick="RadClose();">确定</a>
              </div>
		<script>
			function GetRadWindow() {
				var oWindow = null;
				if (window.radWindow) oWindow = window.radWindow;
				else if (window.frameElement.radWindow) oWindow = window.frameElement.radWindow;
				return oWindow;
			}

			function RadCloseRebind() {
				GetRadWindow().BrowserWindow.pushPopped();
			}

			function RadClose() {
				GetRadWindow().close();
			}
		</script>
	</form>
</body>
</html>
