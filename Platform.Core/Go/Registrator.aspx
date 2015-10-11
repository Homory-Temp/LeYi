<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Registrator.aspx.cs" Inherits="Go.GoRegistrator" %>

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
                <div class="col-md-12">
                    <telerik:RadSearchBox ID="peek" runat="server" OnSearch="peek_Search" EmptyMessage="查找用户...." EnableAutoComplete="false">
                    </telerik:RadSearchBox>
                    <div class="coreTop">&nbsp;</div>
                    <telerik:RadGrid ID="grid" runat="server" AllowPaging="true" AutoGenerateColumns="false" LocalizationPath="../Language" AllowSorting="True" PageSize="10" GridLines="None" OnNeedDataSource="grid_NeedDataSource" OnBatchEditCommand="grid_BatchEditCommand" OnItemCreated="grid_ItemCreated">
                        <MasterTableView EditMode="Batch" DataKeyNames="Id" CommandItemDisplay="Top" HorizontalAlign="NotSet" ShowHeader="true" ShowHeadersWhenNoRecords="true" NoMasterRecordsText="">
                            <BatchEditingSettings EditType="Row" OpenEditingEvent="DblClick" />
                            <HeaderStyle HorizontalAlign="Center" />
                            <CommandItemSettings ShowAddNewRecordButton="false" />
                            <Columns>
                                <telerik:GridTemplateColumn ItemStyle-HorizontalAlign="Center" ReadOnly="true" HeaderText="头像" DataField="Icon" AllowSorting="false" UniqueName="Icon">
                                    <ItemTemplate>
                                        <asp:Image runat="server" Width="60" Height="60" CssClass='<%# string.Format("{0}{1}", "ui image", ((Homory.Model.State)Eval("State")) < Homory.Model.State.审核 ? "" : " disabled") %>' ImageUrl='<%# Eval("Icon") %>'></asp:Image>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn ReadOnly="true" HeaderText="账号" DataField="Account" SortExpression="Account" UniqueName="Account">
                                    <ItemTemplate>
                                        <asp:Label runat="server" Text='<%# Eval("Account") %>'></asp:Label>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn ReadOnly="true" HeaderText="昵称" DataField="DisplayName" SortExpression="DisplayName" UniqueName="DisplayName">
                                    <ItemTemplate>
                                        <asp:Label runat="server" Text='<%# Eval("DisplayName") %>'></asp:Label>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn ReadOnly="true" HeaderText="真实姓名" DataField="RealName" SortExpression="RealName" UniqueName="RealName">
                                    <ItemTemplate>
                                        <asp:Label runat="server" Text='<%# Eval("RealName") %>'></asp:Label>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn HeaderText="状态" DataField="State" SortExpression="State" UniqueName="State">
                                    <ItemTemplate>
                                        <asp:Label runat="server" Text='<%# Eval("State") %>'></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <telerik:RadComboBox ID="State" runat="server" Width="64" EnableTextSelection="true" Text='<%# Bind("State") %>'>
                                            <Items>
                                                <telerik:RadComboBoxItem Text="启用" Value="1" />
                                                <telerik:RadComboBoxItem Text="停用" Value="4" />
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
