﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="StudioCatalog.aspx.cs" Inherits="Extended.ExtendedStudioCatalog" %>

<%@ Import Namespace="Homory.Model" %>

<!DOCTYPE html>

<html>
<head runat="server">
	<meta charset="utf-8" />
	<meta http-equiv="X-UA-Compatible" content="IE=Edge,Chrome=1" />
	<meta name="viewport" content="width=device-width,initial-scale=1,minimum-scale=1,maximum-scale=1" />
	<title>栏目管理</title>
	<link href="../Content/Semantic/css/semantic.min.css" rel="stylesheet" />
	<link href="../Content/Homory/css/common.css" rel="stylesheet" />
	<link href="../Content/Core/css/common.css" rel="stylesheet" />
	<script src="../Content/jQuery/jquery.min.js"></script>
	<script src="../Content/Semantic/javascript/semantic.min.js"></script>
	<script src="../Content/Homory/js/common.js"></script>
	<script src="../Content/Homory/js/notify.min.js"></script>
</head>
<body>
	<form id="formHome" runat="server">
        <telerik:RadScriptManager ID="scriptManager" runat="server"></telerik:RadScriptManager>
		<telerik:RadAjaxLoadingPanel ID="loading" runat="server">
			<i class="ui huge teal loading icon" style="margin-top: 50px;"></i>
			<div>&nbsp;</div>
			<div style="color: #564F8A; font-size: 16px;">正在加载 请稍候....</div>
		</telerik:RadAjaxLoadingPanel>
		<telerik:RadAjaxPanel ID="panel" runat="server" CssClass="ui left aligned stackable page grid" LoadingPanelID="loading">
			<div class="column">
				<table class="coreAuto">
					<tr class="coreTop">
						<td>
							<telerik:RadTreeView ID="tree" runat="server" EnableDragAndDrop="true" EnableDragAndDropBetweenNodes="false" DataTextField="Name" DataValueField="Id" DataFieldID="Id" DataFieldParentID="ParentId" OnNodeClick="tree_NodeClick">
								<NodeTemplate>
									<i class='<%# FormatTreeNode(Container.DataItem) %>'></i>&nbsp;<%# Eval("Name") %><%# CountChildren(Container.DataItem as Catalog) %>
								</NodeTemplate>
							</telerik:RadTreeView>
						</td>
						<td>&nbsp;</td>
						<td>
							<telerik:RadGrid ID="grid" runat="server" CssClass="coreCenter" AutoGenerateColumns="false" LocalizationPath="../Language" AllowSorting="True" PageSize="20" GridLines="None" OnNeedDataSource="grid_NeedDataSource" OnBatchEditCommand="grid_BatchEditCommand" OnItemCreated="grid_ItemCreated">
								<MasterTableView EditMode="Batch" DataKeyNames="Id" CommandItemDisplay="Top" HorizontalAlign="NotSet" ShowHeader="true" ShowHeadersWhenNoRecords="true" NoMasterRecordsText="">
									<BatchEditingSettings EditType="Row" OpenEditingEvent="DblClick" />
									<HeaderStyle HorizontalAlign="Center" />
									<Columns>
										<telerik:GridTemplateColumn HeaderText="序号" DataField="Ordinal" SortExpression="Ordinal" UniqueName="Ordinal">
											<ItemTemplate>
												<asp:Label runat="server" Text='<%# Eval("Ordinal") %>'></asp:Label>
											</ItemTemplate>
											<EditItemTemplate>
												<telerik:RadNumericTextBox ID="Ordinal" runat="server" EnabledStyle-HorizontalAlign="Center" Width="64" MinValue="1" MaxValue="99" AllowOutOfRangeAutoCorrect="true" Value='<%# Bind("Ordinal") %>'>
													<NumberFormat DecimalDigits="0" AllowRounding="true" />
												</telerik:RadNumericTextBox>
											</EditItemTemplate>
										</telerik:GridTemplateColumn>
										<telerik:GridTemplateColumn HeaderText="名称 *" DataField="Name" SortExpression="Name" UniqueName="Name">
											<ItemTemplate>
												<asp:Label runat="server" Text='<%# Eval("Name") %>'></asp:Label>
											</ItemTemplate>
											<EditItemTemplate>
												<telerik:RadTextBox ID="Name" runat="server" EnabledStyle-HorizontalAlign="Center" Width="64" MaxLength="16" Text='<%# Bind("Name") %>'>
												</telerik:RadTextBox>
											</EditItemTemplate>
										</telerik:GridTemplateColumn>
										<telerik:GridTemplateColumn HeaderText="状态" DataField="State" SortExpression="State" UniqueName="State">
											<ItemTemplate>
												<asp:Label runat="server" Text='<%# Eval("State") %>'></asp:Label>
											</ItemTemplate>
											<EditItemTemplate>
												<telerik:RadComboBox ID="State" runat="server" Width="64" EnableTextSelection="true" Text='<%# Bind("State") %>'>
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
						</td>
					</tr>
				</table>
			</div>
		</telerik:RadAjaxPanel>
	</form>
</body>
</html>
