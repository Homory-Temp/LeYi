<%@ Page Language="C#" AutoEventWireup="true" CodeFile="GroupManage.aspx.cs" Inherits="Popup_GroupManage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
	<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
	<title></title>
	<link href="../Style/common.css" rel="stylesheet" />
</head>
<body style="width: 840px; overflow: visible;">
	<form id="form1" runat="server">
		<telerik:RadScriptManager runat="server" ID="sm"></telerik:RadScriptManager>
		<div style="width: 840px; overflow: visible;">
			<telerik:RadAjaxPanel runat="server" ID="panel">
				<table style="vertical-align: top; margin-left: 10px; line-height: 30px;">
					<tr>
						<td>
							<asp:Label runat="server" Text="名称："></asp:Label>
						</td>
						<td>
							<telerik:RadTextBox ID="name" runat="server" MaxLength="16" Width="160"></telerik:RadTextBox>
						</td>
					</tr>
					<tr>
						<td>
							<asp:Label runat="server" Text="简介："></asp:Label>
						</td>
						<td>
							<telerik:RadTextBox ID="intro" runat="server" Width="360"></telerik:RadTextBox>
						</td>
					</tr>
					<tr>
						<td>
							<asp:Label runat="server" Text="学科："></asp:Label>
						</td>
						<td>
							<telerik:RadComboBox runat="server" AppendDataBoundItems="True" ID="publish_course" AutoPostBack="True" Width="140" DataTextField="Name" DataValueField="Id" DataFieldID="Id">
								<Items>
									<telerik:RadComboBoxItem runat="server" Text="" Value="00000000-0000-0000-0000-000000000000" Selected="True" />
								</Items>
							</telerik:RadComboBox>
						</td>
					</tr>
					<tr>
						<td>
							<asp:Label runat="server" Text="年级："></asp:Label>
						</td>
						<td>
							<telerik:RadComboBox runat="server" AppendDataBoundItems="True" ID="publish_grade" AutoPostBack="True" Width="140" DataTextField="Name" DataValueField="Id" DataFieldID="Id">
								<Items>
									<telerik:RadComboBoxItem runat="server" Text="" Value="00000000-0000-0000-0000-000000000000" Selected="True" />
								</Items>
							</telerik:RadComboBox>
						</td>
					</tr>
					<tr>
						<td style="vertical-align: top;">
							<asp:Label runat="server" Text="头像："></asp:Label>
						</td>
						<td style="width: 750px;">
							<telerik:RadListView ID="icons" runat="server" OnNeedDataSource="icons_NeedDataSource">
								<ItemTemplate>
									<asp:ImageButton CommandName="Select" runat="server" ImageUrl='<%# Container.DataItem %>' Width="40" Height="40" Style="cursor: pointer;" />
								</ItemTemplate>
								<SelectedItemTemplate>
									<asp:ImageButton runat="server" ImageUrl='<%# Container.DataItem %>' Width="36" Height="36" Style="cursor: pointer;" BorderColor="#227dc5" BorderStyle="Solid" BorderWidth="2" />
								</SelectedItemTemplate>
							</telerik:RadListView>
						</td>
					</tr>
					<tr>
						<td>&nbsp;</td>
						<td>&nbsp;</td>
					</tr>
					<tr>
						<td colspan="2" style="text-align: center;">
							<a id="btnCreate" runat="server" class="button24 srx-ciptbox-submit" target="_self" onserverclick="btnCreate_ServerClick" style="width: 60px;"><em>确认</em></a>
						</td>
					</tr>
				</table>
			</telerik:RadAjaxPanel>
		</div>
		<script>
			function GetRadWindow() {
				var oWindow = null;
				if (window.radWindow) oWindow = window.radWindow;
				else if (window.frameElement.radWindow) oWindow = window.frameElement.radWindow;
				return oWindow;
			}

			function RadCloseRebind() {
				RadClose();
				GetRadWindow().BrowserWindow.groupCreated();
			}

			function RadClose() {
				GetRadWindow().close();
			}
		</script>
	</form>
</body>
</html>
