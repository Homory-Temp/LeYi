<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Class.aspx.cs" Inherits="Go.GoClass" %>

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
        <telerik:RadCodeBlock runat="server">
            <div class="container-fluid">
                <div class="row">&nbsp;</div>
                <div class="row">
                    <div class="col-md-12">
                        <div class="btn-info btn btn-lg">组织管理 - 班级管理</div>
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
                    <telerik:RadComboBox ID="combo" runat="server" AutoPostBack="true" OnSelectedIndexChanged="combo_SelectedIndexChanged" DataTextField="Name" DataValueField="Id" Label="选择学校：" Width="220px" Filter="Contains" MarkFirstMatch="true" AllowCustomText="true" Height="202px">
                        <ItemTemplate>
                            <%# GenerateTreeName((Homory.Model.Department)Container.DataItem, Container.Index, 0) %>
                        </ItemTemplate>
                    </telerik:RadComboBox>
                </div>
                <div class="col-md-12">
                    <table class="coreAuto">
                        <tr class="coreTop">
                            <td>
                                <telerik:RadTreeView ID="tree" runat="server" EnableDragAndDrop="true" EnableDragAndDropBetweenNodes="false" DataTextField="Name" DataValueField="Id" DataFieldID="Id" DataFieldParentID="ParentId" OnNodeClick="tree_NodeClick" OnNodeDrop="tree_NodeDrop">
                                    <NodeTemplate>
                                        <%# GenerateTreeName((Homory.Model.Department)Container.DataItem, Container.Index, Container.Level) %>
                                    </NodeTemplate>
                                </telerik:RadTreeView>
                            </td>
                            <td>&nbsp;&nbsp;&nbsp;&nbsp;</td>
                            <td class="coreFull">
                                <telerik:RadGrid ID="grid" runat="server" CssClass="coreCenter coreFull" AllowPaging="false" AutoGenerateColumns="False" LocalizationPath="../Language" AllowSorting="True" PageSize="10" GridLines="None" OnNeedDataSource="grid_NeedDataSource">
                                    <MasterTableView DataKeyNames="Id" CssClass="coreFull" CommandItemDisplay="None" HorizontalAlign="NotSet" ShowHeader="true" ShowHeadersWhenNoRecords="true" NoMasterRecordsText="">
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <Columns>
                                            <telerik:GridTemplateColumn HeaderText="学届" DataField="Ordinal" SortExpression="Ordinal" UniqueName="Ordinal">
                                                <ItemTemplate>
                                                    <asp:Label runat="server" Text='<%# Eval("Ordinal") + "届" %>'></asp:Label>
                                                </ItemTemplate>
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridTemplateColumn HeaderText="名称" DataField="Name" SortExpression="Name" UniqueName="Name">
                                                <ItemTemplate>
                                                    <asp:Label runat="server" Text='<%# GenerateGridName((Homory.Model.Department)Container.DataItem) %>'></asp:Label>
                                                </ItemTemplate>
                                            </telerik:GridTemplateColumn>
                                        </Columns>
                                    </MasterTableView>
                                </telerik:RadGrid>
                                <telerik:RadGrid ID="gridX" runat="server" CssClass="coreCenter" AllowPaging="true" AutoGenerateColumns="false" LocalizationPath="../Language" AllowSorting="True" PageSize="20" GridLines="None" OnNeedDataSource="gridX_NeedDataSource" OnBatchEditCommand="gridX_BatchEditCommand" OnItemCreated="grid_ItemCreated">
                                    <MasterTableView EditMode="Batch" DataKeyNames="Id" CommandItemDisplay="Top" HorizontalAlign="NotSet" ShowHeader="true" ShowHeadersWhenNoRecords="true" NoMasterRecordsText="">
                                        <BatchEditingSettings EditType="Row" OpenEditingEvent="DblClick" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <Columns>
                                            <telerik:GridTemplateColumn HeaderText="班号 *" DataField="Ordinal" SortExpression="Ordinal" UniqueName="Ordinal">
                                                <ItemTemplate>
                                                    <asp:Label runat="server" Text='<%# Eval("Ordinal") %>'></asp:Label>
                                                </ItemTemplate>
                                                <InsertItemTemplate>
                                                    <telerik:RadNumericTextBox ID="Ordinal" runat="server" EnabledStyle-HorizontalAlign="Center" Width="64" MinValue="1" MaxValue="99" AllowOutOfRangeAutoCorrect="true" Value='<%# Bind("Ordinal") %>'>
                                                        <NumberFormat DecimalDigits="0" AllowRounding="true" />
                                                    </telerik:RadNumericTextBox>
                                                </InsertItemTemplate>
                                                <EditItemTemplate></EditItemTemplate>
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridTemplateColumn HeaderText="别名" DataField="DisplayName" SortExpression="DisplayName" UniqueName="DisplayName">
                                                <ItemTemplate>
                                                    <asp:Label runat="server" Text='<%# Eval("DisplayName") %>'></asp:Label>
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
                                        <PagerStyle Mode="NextPrevAndNumeric" Position="Bottom" PageSizeControlType="RadComboBox" AlwaysVisible="true" PagerTextFormat="{4} 第{0}页，共{1}页；第{2}-{3}项，共{5}项" />
                                    </MasterTableView>
                                </telerik:RadGrid>
                                <asp:Panel ID="gridXX" runat="server" CssClass="container-fluid" Style="border: solid 1px #828282; padding: 20px; margin: 20px; text-align: center; vertical-align: middle;">
                                    <div class="row">
                                        <div class="col-md-6" style="text-align: left;">
                                            <h6 class="btn btn-warning">班主任：</h6>
                                            &nbsp;&nbsp;&nbsp;&nbsp;
										    <telerik:RadButton ID="charging" runat="server" CssClass="btn btn-primary" ForeColor="White" Width="80" Height="32" ButtonType="ToggleButton" AutoPostBack="True" OnClick="charging_OnClick" Style="margin-top: 10px;">
                                            </telerik:RadButton>
                                        </div>
                                        <div class="col-md-6" style="text-align: left;">
                                            <telerik:RadSearchBox ID="peek" runat="server" OnSearch="peek_Search" EmptyMessage="查找...." EnableAutoComplete="false" Style="margin-top: 4px;">
                                                </telerik:RadSearchBox>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-12">&nbsp;</div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-12" style="text-align: left;">
                                            <h6 class="btn btn-warning">待选区：</h6>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-12">&nbsp;</div>
                                    </div>
                                    <div class="row">
                                        <telerik:RadListView ID="view" runat="server" DataKeyNames="Id" ClientDataKeyNames="Id" OnNeedDataSource="view_OnNeedDataSource">
                                            <ItemTemplate>
                                                <div class="rootPointer col-md-2 col-xs-4">
                                                    <telerik:RadButton ID="charger" runat="server" Width="80" Height="32" ForeColor="White" CssClass='<%# Eval("Id").ToString() == charging.CommandArgument ? "btn btn-primary" : "btn btn-info" %>' ButtonType="ToggleButton" CommandArgument='<%# Eval("Id") %>' AutoPostBack="True" Text='<%# Eval("RealName").ToString().Length == 2 ? Eval("RealName").ToString()[0] + "　" + Eval("RealName").ToString()[1] : Eval("RealName").ToString() %>' OnClick="charger_OnClick">
                                                    </telerik:RadButton>
                                                </div>
                                            </ItemTemplate>
                                            <ClientSettings AllowItemsDragDrop="true"></ClientSettings>
                                        </telerik:RadListView>
                                    </div>
                                </asp:Panel>
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
            <div class="row">&nbsp;</div>
        </telerik:RadAjaxPanel>
    </form>
</body>
</html>
