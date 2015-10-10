<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Rooms.aspx.cs" Inherits="Go.GoRooms" %>

<%@ Register Src="~/Control/SideBar.ascx" TagPrefix="homory" TagName="SideBar" %>

<!DOCTYPE html>

<html>
<head runat="server">
	<meta charset="utf-8" />
	<meta http-equiv="X-UA-Compatible" content="IE=Edge,Chrome=1" />
	<meta name="viewport" content="width=device-width,initial-scale=1,minimum-scale=1,maximum-scale=1" />
	<title>基础平台</title>
	<script src="../Content/jQuery/jquery.min.js"></script>
    <link href="../assets/css/bootstrap.min.css" rel="stylesheet" />
    <link href="../assets/css/style-responsive.css" rel="stylesheet" />
    <link href="../assets/css/style.css" rel="stylesheet" />
    <script src="../assets/js/bootstrap.min.js"></script>
    <link href="../Content/Semantic/css/semantic.min.css" rel="stylesheet" />
    <link href="../Content/Homory/css/common.css" rel="stylesheet" />
    <link href="../Content/Core/css/common.css" rel="stylesheet" />
    <script src="../Content/Semantic/javascript/semantic.min.js"></script>
    <script src="../Content/Homory/js/common.js"></script>
    <script src="../Content/Homory/js/notify.min.js"></script>
    <!--[if lt IE 9]>
	    <script src="../Content/Homory/js/html5shiv.js"></script>
	    <script src="../Content/Homory/js/respond.min.js"></script>
    <![endif]-->
</head>
<body>
	<form id="formHome" runat="server">
		<div>
			<homory:SideBar runat="server" ID="SideBar" />
		</div>
		<telerik:RadAjaxLoadingPanel ID="loading" runat="server">
			<i class="ui huge teal loading icon" style="margin-top: 50px;"></i>
			<div>&nbsp;</div>
			<div style="color: #564F8A; font-size: 16px;">正在加载 请稍候....</div>
		</telerik:RadAjaxLoadingPanel>
		<telerik:RadAjaxPanel ID="panel" runat="server" CssClass="ui center aligned page grid" style="margin:0;padding:0;" LoadingPanelID="loading">
			<div class="column">
				<telerik:RadGrid ID="grid" runat="server" AutoGenerateColumns="false" LocalizationPath="../Language" AllowSorting="True" PageSize="20" GridLines="None" OnNeedDataSource="grid_NeedDataSource" OnBatchEditCommand="grid_BatchEditCommand" OnItemCreated="grid_ItemCreated">
					<MasterTableView EditMode="Batch" DataKeyNames="Id" CommandItemDisplay="Top" HorizontalAlign="NotSet" ShowHeader="true" ShowHeadersWhenNoRecords="true" NoMasterRecordsText="">
						<BatchEditingSettings EditType="Row" OpenEditingEvent="DblClick" />
						<HeaderStyle HorizontalAlign="Center" />
						<Columns>
							<telerik:GridTemplateColumn HeaderText="序号 *" HeaderStyle-Width="60" ItemStyle-Width="60" DataField="Ordinal" SortExpression="Ordinal" UniqueName="Ordinal">
								<ItemTemplate>
									<asp:Label runat="server" Text='<%# Eval("Ordinal") %>'></asp:Label>
								</ItemTemplate>
								<InsertItemTemplate>
									<telerik:RadNumericTextBox ID="Ordinal" runat="server" EnabledStyle-HorizontalAlign="Center" Width="46" MinValue="1" MaxValue="999999" AllowOutOfRangeAutoCorrect="true" Value='<%# Bind("Ordinal") %>'>
										<NumberFormat DecimalDigits="0" AllowRounding="true" />
									</telerik:RadNumericTextBox>
								</InsertItemTemplate>
							</telerik:GridTemplateColumn>
							<telerik:GridTemplateColumn HeaderText="名称 *" HeaderStyle-Width="80" ItemStyle-Width="80" DataField="Name" SortExpression="Name" UniqueName="Name">
								<ItemTemplate>
									<asp:Label runat="server" Text='<%# Eval("Name") %>'></asp:Label>
								</ItemTemplate>
								<EditItemTemplate>
									<telerik:RadTextBox ID="Name" runat="server" EnabledStyle-HorizontalAlign="Center" Width="64" MaxLength="16" Text='<%# Bind("Name") %>'>
									</telerik:RadTextBox>
								</EditItemTemplate>
							</telerik:GridTemplateColumn>
							<telerik:GridTemplateColumn HeaderText="简介 *" DataField="Description" SortExpression="Description" UniqueName="Description">
								<ItemTemplate>
									<asp:Label runat="server" Text='<%# Eval("Description") %>'></asp:Label>
								</ItemTemplate>
								<EditItemTemplate>
									<telerik:RadTextBox ID="Description" runat="server" EnabledStyle-HorizontalAlign="Center" Width="128" MaxLength="1000" Text='<%# Bind("Description") %>'>
									</telerik:RadTextBox>
								</EditItemTemplate>
							</telerik:GridTemplateColumn>
							<telerik:GridTemplateColumn HeaderText="链接 *" DataField="Url" SortExpression="Url" UniqueName="Url">
								<ItemTemplate>
									<asp:Label runat="server" Text='<%# Eval("Url") %>'></asp:Label>
								</ItemTemplate>
								<EditItemTemplate>
									<telerik:RadTextBox ID="Url" runat="server" EnabledStyle-HorizontalAlign="Center" Width="128" MaxLength="128" Text='<%# Bind("Url") %>'>
									</telerik:RadTextBox>
								</EditItemTemplate>
							</telerik:GridTemplateColumn>
							<telerik:GridTemplateColumn HeaderText="状态" HeaderStyle-Width="80" ItemStyle-Width="80" DataField="State" SortExpression="State" UniqueName="State">
								<ItemTemplate>
									<asp:Label ID="stateLabel" runat="server" Text='<%# Eval("State") %>'></asp:Label>
								</ItemTemplate>
								<EditItemTemplate>
									<telerik:RadComboBox ID="State" runat="server" Width="64" EnableTextSelection="true" Text='<%# Eval("State") %>'>
										<Items>
											<telerik:RadComboBoxItem Text="" Value="-1" />
											<telerik:RadComboBoxItem Text="启用" Value="1" />
											<telerik:RadComboBoxItem Text="停用" Value="4" />
											<telerik:RadComboBoxItem Text="删除" Value="5" />
										</Items>
									</telerik:RadComboBox>
								</EditItemTemplate>
							</telerik:GridTemplateColumn>
						</Columns>
					</MasterTableView>
				</telerik:RadGrid>
			</div>
		</telerik:RadAjaxPanel>
	</form>
</body>
</html>
