<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ResourceManage.aspx.cs" Inherits="Go.GoResourceManage" %>

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
		<telerik:RadAjaxPanel ID="panel" runat="server" CssClass="container-fluid" LoadingPanelID="loading">
            <div class="row">
                <div class="col-md-6">
                    <telerik:RadComboBox ID="combo" runat="server" AutoPostBack="true" OnSelectedIndexChanged="combo_SelectedIndexChanged" DataTextField="Name" DataValueField="Id" Label="选择学校：" Width="220px" Filter="Contains" MarkFirstMatch="true" AllowCustomText="true" Height="202px">
                    </telerik:RadComboBox>
                </div>
                <div class="col-md-6 text-right">
                    <div class="btn btn-tumblr" onclick="window.open('../Go/ResourceSplash', '_blank');">资源平台首页展示图设定</div>
                </div>
            </div>
            <div class="row">&nbsp;</div>
			<div class="row">
                <div class="col-md-12">
				    <telerik:RadGrid ID="grid" runat="server" AutoGenerateColumns="false" LocalizationPath="../Language" AllowSorting="True" AllowPaging="true" AllowFilteringByColumn="true" PageSize="100" GridLines="None" OnNeedDataSource="grid_NeedDataSource" OnBatchEditCommand="grid_BatchEditCommand" OnItemCreated="grid_ItemCreated">
					    <MasterTableView EditMode="Batch" DataKeyNames="Id" CommandItemDisplay="Top" HorizontalAlign="NotSet" ShowHeader="true" ShowHeadersWhenNoRecords="true" NoMasterRecordsText="">
						    <BatchEditingSettings EditType="Row" OpenEditingEvent="DblClick" />
						    <HeaderStyle HorizontalAlign="Center" />
                            <FilterItemStyle HorizontalAlign="Left" />
						    <Columns>
							    <telerik:GridTemplateColumn ItemStyle-HorizontalAlign="Center" ReadOnly="true" ItemStyle-Width="150" AllowFiltering="false" HeaderStyle-Width="150" HeaderText="发布时间" DataField="Time" SortExpression="Time" UniqueName="Time">
								    <ItemTemplate>
									    <asp:Label runat="server" Text='<%# ((DateTime)Eval("Time")).ToString("yyyy-MM-dd HH:mm:ss") %>'></asp:Label>
								    </ItemTemplate>
							    </telerik:GridTemplateColumn>
							    <telerik:GridTemplateColumn ItemStyle-HorizontalAlign="Center" ReadOnly="true" ItemStyle-Width="100" HeaderStyle-Width="100" HeaderText="作者" DataField="UserId" SortExpression="UserId" UniqueName="Author" AllowFiltering="false">
								    <ItemTemplate>
									    <asp:Label runat="server" Text='<%# U((Guid)Eval("UserId")) %>'></asp:Label>
								    </ItemTemplate>
							    </telerik:GridTemplateColumn>
							    <telerik:GridTemplateColumn ItemStyle-HorizontalAlign="Left" HeaderText="标题" FilterControlWidth="400" DataField="Title" SortExpression="Title" UniqueName="Title">
								    <ItemTemplate>
									    <asp:HyperLink Target="_blank" NavigateUrl='<%# Application["Resource"] + "Go/ViewPlain?Id=" + Eval("Id") %>' runat="server" Text='<%# Eval("Title") %>'></asp:HyperLink>
								    </ItemTemplate>
								    <EditItemTemplate>
									    <telerik:RadTextBox ID="Name" runat="server" EnabledStyle-HorizontalAlign="Center" Width="200" MaxLength="256" Text='<%# Bind("Title") %>'>
									    </telerik:RadTextBox>
								    </EditItemTemplate>
							    </telerik:GridTemplateColumn>
							    <telerik:GridTemplateColumn ItemStyle-HorizontalAlign="Center" HeaderText="状态" DataField="State" AllowFiltering="false" SortExpression="State" UniqueName="State" ItemStyle-Width="100" HeaderStyle-Width="100">
								    <ItemTemplate>
									    <asp:Label ID="stateLabel" runat="server" Text='<%# Eval("State") %>'></asp:Label>
								    </ItemTemplate>
								    <EditItemTemplate>
									    <telerik:RadComboBox ID="State" runat="server" Width="64" EnableTextSelection="true" Text='<%# Eval("State") %>'>
										    <Items>
											    <telerik:RadComboBoxItem Text="" Value="-1" />
											    <telerik:RadComboBoxItem Text="启用" Value="1" />
											    <telerik:RadComboBoxItem Text="删除" Value="5" />
										    </Items>
									    </telerik:RadComboBox>
								    </EditItemTemplate>
							    </telerik:GridTemplateColumn>
						    </Columns>
                            <PagerStyle Mode="NextPrevAndNumeric" PageSizes="10,20,50,100" Position="Bottom" PageSizeControlType="RadComboBox" AlwaysVisible="true" PagerTextFormat="{4} 第{0}页，共{1}页；第{2}-{3}项，共{5}项" />
					    </MasterTableView>
				    </telerik:RadGrid>
                </div>
			</div>
		</telerik:RadAjaxPanel>
	</form>
</body>
</html>
