<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Campus.aspx.cs" Inherits="Go.GoCampus" %>

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
    <link href="../Content/Homory/css/common.css" rel="stylesheet" />
    <link href="../Content/Core/css/common.css" rel="stylesheet" />
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
        <telerik:RadCodeBlock runat="server">
            <div class="container-fluid">
                <div class="row">&nbsp;</div>
                <div class="row">
                    <div class="col-md-12">
                        <div class="btn-info btn btn-lg">组织管理 - 学校管理</div>
                    </div>
                </div>
            </div>
        </telerik:RadCodeBlock>
		<telerik:RadAjaxLoadingPanel ID="loading" runat="server">
			<div>&nbsp;</div>
			<div class="btn btn-lg btn-warning" style="margin-top: 50px;">正在加载 请稍候....</div>
		</telerik:RadAjaxLoadingPanel>
		<telerik:RadAjaxPanel ID="panel" runat="server" CssClass="container-fluid" LoadingPanelID="loading">
            <div class="row">&nbsp;</div>
			<div class="row">
                <div class="col-md-12">
				    <telerik:RadGrid ID="grid" runat="server" AutoGenerateColumns="false" LocalizationPath="../Language" AllowSorting="True" PageSize="20" GridLines="None" OnNeedDataSource="grid_NeedDataSource" OnBatchEditCommand="grid_BatchEditCommand" OnItemCreated="grid_ItemCreated">
					    <MasterTableView EditMode="Batch" DataKeyNames="Id" CommandItemDisplay="Top" HorizontalAlign="NotSet" ShowHeader="true" ShowHeadersWhenNoRecords="true" NoMasterRecordsText="">
						    <BatchEditingSettings EditType="Row" OpenEditingEvent="DblClick" />
						    <HeaderStyle HorizontalAlign="Center" />
						    <Columns>
							    <telerik:GridTemplateColumn ItemStyle-Width="80" HeaderStyle-Width="80" HeaderText="序号" DataField="Ordinal" SortExpression="Ordinal" UniqueName="Ordinal">
								    <ItemTemplate>
									    <asp:Label runat="server" Text='<%# Eval("Ordinal") %>'></asp:Label>
								    </ItemTemplate>
								    <EditItemTemplate>
									    <telerik:RadNumericTextBox ID="Ordinal" runat="server" DataType="System.Int32" EnabledStyle-HorizontalAlign="Center" Width="64" MinValue="1" MaxValue="999999" AllowOutOfRangeAutoCorrect="true" Value='<%# Bind("Ordinal") %>'>
										    <NumberFormat DecimalDigits="0" AllowRounding="true" />
									    </telerik:RadNumericTextBox>
								    </EditItemTemplate>
							    </telerik:GridTemplateColumn>
							    <telerik:GridTemplateColumn ItemStyle-HorizontalAlign="Left" ItemStyle-Width="250" HeaderStyle-Width="250" HeaderText="机构名称 *" DataField="Name" SortExpression="Name" UniqueName="Name">
								    <ItemTemplate>
									    <asp:Label runat="server" Text='<%# Eval("Name") %>'></asp:Label>
								    </ItemTemplate>
								    <EditItemTemplate>
									    <telerik:RadTextBox ID="Name" runat="server" EnabledStyle-HorizontalAlign="Center" Width="64" MaxLength="16" Text='<%# Bind("Name") %>'>
									    </telerik:RadTextBox>
								    </EditItemTemplate>
							    </telerik:GridTemplateColumn>
							    <telerik:GridTemplateColumn HeaderText="机构代码" DataField="Code" SortExpression="Code" UniqueName="Code">
								    <ItemTemplate>
									    <asp:Label runat="server" Text='<%# Eval("Code") %>'></asp:Label>
								    </ItemTemplate>
								    <EditItemTemplate>
									    <telerik:RadTextBox ID="Code" runat="server" EnabledStyle-HorizontalAlign="Center" Width="64" MaxLength="32" Text='<%# Bind("Code") %>'>
									    </telerik:RadTextBox>
								    </EditItemTemplate>
							    </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn HeaderText="学校类别" DataField="ClassType" SortExpression="ClassType" UniqueName="ClassType">
								    <ItemTemplate>
									    <asp:Label ID="classTypeLabel" runat="server" Text='<%# Eval("ClassType") %>'></asp:Label>
								    </ItemTemplate>
								    <EditItemTemplate>
									    <telerik:RadComboBox ID="ClassType" runat="server" Width="90" EnableTextSelection="true" Text='<%# Eval("ClassType") %>'>
										    <Items>
											    <telerik:RadComboBoxItem Text="" Value="0" />
                                                <telerik:RadComboBoxItem Text="九年一贯制" Value="1" />
                                                <telerik:RadComboBoxItem Text="幼儿园" Value="2" />
                                                <telerik:RadComboBoxItem Text="小学" Value="3" />
                                                <telerik:RadComboBoxItem Text="初中" Value="4" />
                                                <telerik:RadComboBoxItem Text="高中" Value="6" />
                                                <telerik:RadComboBoxItem Text="其他" Value="5" />
										    </Items>
									    </telerik:RadComboBox>
								    </EditItemTemplate>
							    </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn HeaderText="学校办别" DataField="BuildType" SortExpression="BuildType" UniqueName="BuildType">
								    <ItemTemplate>
									    <asp:Label ID="buildTypeLabel" runat="server" Text='<%# Eval("BuildType") %>'></asp:Label>
								    </ItemTemplate>
								    <EditItemTemplate>
									    <telerik:RadComboBox ID="BuildType" runat="server" Width="140" EnableTextSelection="true" Text='<%# Eval("BuildType") %>'>
										    <Items>
											    <telerik:RadComboBoxItem Text="" Value="0" />
											    <telerik:RadComboBoxItem Text="教育部门社会集体办" Value="1" />
											    <telerik:RadComboBoxItem Text="民办" Value="2" />
										    </Items>
									    </telerik:RadComboBox>
								    </EditItemTemplate>
							    </telerik:GridTemplateColumn>
							    <telerik:GridTemplateColumn HeaderText="状态" DataField="State" SortExpression="State" UniqueName="State">
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
			</div>
            <div class="row">&nbsp;</div>
		</telerik:RadAjaxPanel>
	</form>
</body>
</html>
