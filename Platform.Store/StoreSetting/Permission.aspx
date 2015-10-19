<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Permission.aspx.cs" Inherits="StoreSetting_Permission" %>

<%@ Register Src="~/Control/SideBarSingle.ascx" TagPrefix="homory" TagName="SideBarSingle" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=Edge,Chrome=1" />
    <meta name="viewport" content="width=device-width,initial-scale=1,minimum-scale=1,maximum-scale=1" />
    <script src="../Content/jQuery/jquery.min.js"></script>
    <link href="../assets/css/bootstrap.min.css" rel="stylesheet" />
    <link href="../assets/css/style-responsive.css" rel="stylesheet" />
    <link href="../assets/css/style.css" rel="stylesheet" />
    <link href="../Content/Core/css/common.css" rel="stylesheet" />
    <link href="../Content/Core/css/fix.css" rel="stylesheet" />
    <script src="../assets/js/bootstrap.min.js"></script>
    <script src="../Content/Homory/js/common.js"></script>
    <script src="../Content/Homory/js/notify.min.js"></script>
    <!--[if lt IE 9]>
	    <script src="../Content/Homory/js/html5shiv.js"></script>
	    <script src="../Content/Homory/js/respond.min.js"></script>
    <![endif]-->
</head>
<body>
    <form id="form" runat="server">
        <homory:SideBarSingle runat="server" ID="SideBarSingle" Crumb="系统设置 - 权限设置" />
        <telerik:RadAjaxPanel ID="ap" runat="server" CssClass="container-fluid" LoadingPanelID="loading">
            <div class="row">
                <div class="col-md-2" style="border-right: 1px solid #2B2B2B;">
                    <div class="row">
                        <div class="col-md-12">
                            <span class="btn btn-tumblr">角色列表：</span>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12">
                            <telerik:RadTreeView ID="tree0" runat="server" OnNodeClick="tree0_NodeClick" ShowLineImages="false">
                                <Nodes>
                                    <telerik:RadTreeNode Text="新增角色" Value="0" Selected="true"></telerik:RadTreeNode>
                                </Nodes>
                            </telerik:RadTreeView>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12">
                            <telerik:RadTreeView ID="tree" runat="server" OnNodeClick="tree_NodeClick" DataTextField="Name" DataValueField="Id">
                                <NodeTemplate>
                                    <%# Eval("Name") %><%# (Container.DataItem as Models.StoreRole).State == 0 ? "（内置）" : "" %>
                                </NodeTemplate>
                            </telerik:RadTreeView>
                        </div>
                    </div>
                </div>
                <div class="col-md-10" style="text-align: left;">
                    <div class="row">
                        <div class="col-md-12">
                            <telerik:RadNumericTextBox ID="ordinal" runat="server" EmptyMessage="序号" Width="100" NumberFormat-DecimalDigits="0"></telerik:RadNumericTextBox>
                            &nbsp;&nbsp;
                                            <telerik:RadTextBox ID="name" runat="server" EmptyMessage="角色名称" Width="300"></telerik:RadTextBox>
                            &nbsp;&nbsp;
                            <telerik:RadButton ID="r1" runat="server" Text="入库" AutoPostBack="false" ButtonType="ToggleButton" ToggleType="CheckBox" Value="1"></telerik:RadButton>
                            &nbsp;&nbsp;
                            <telerik:RadButton ID="r2" runat="server" Text="借领" AutoPostBack="false" ButtonType="ToggleButton" ToggleType="CheckBox" Value="2"></telerik:RadButton>
                            &nbsp;&nbsp;
                            <telerik:RadButton ID="r3" runat="server" Text="归还" AutoPostBack="false" ButtonType="ToggleButton" ToggleType="CheckBox" Value="3"></telerik:RadButton>
                            &nbsp;&nbsp;
                            <telerik:RadButton ID="r4" runat="server" Text="编辑" AutoPostBack="false" ButtonType="ToggleButton" ToggleType="CheckBox" Value="4"></telerik:RadButton>
                            &nbsp;&nbsp;
                            <telerik:RadButton ID="r5" runat="server" Text="报表" AutoPostBack="false" ButtonType="ToggleButton" ToggleType="CheckBox" Value="5"></telerik:RadButton>
                            &nbsp;&nbsp;
                            <telerik:RadButton ID="r6" runat="server" Text="设置" AutoPostBack="false" ButtonType="ToggleButton" ToggleType="CheckBox" Value="6"></telerik:RadButton>
                            &nbsp;&nbsp;
                                            <input id="add" runat="server" type="button" class="btn btn-tumblr" value="保存" onserverclick="add_ServerClick" />
                            &nbsp;&nbsp;
                                            <input id="delete" runat="server" type="button" class="btn btn-tumblr" value="删除" onserverclick="delete_ServerClick" />
                            &nbsp;&nbsp;
                                                        <span id="sp" runat="server" class="text-center text-danger">（内置角色不可删除，权限不可更改）&nbsp;&nbsp;</span>
                        </div>
                    </div>
                    <div class="row">&nbsp;</div>
                    <div class="row">
                        <div class="col-md-12">
                            <telerik:RadSearchBox ID="search" runat="server" EmptyMessage="用户姓名或拼音首字母" DataTextField="RealName" DataValueField="Id" MaxResultCount="10" LocalizationPath="~/Language" ShowLoadingIcon="false" ShowSearchButton="false" OnDataSourceSelect="search_DataSourceSelect" OnSearch="search_Search"></telerik:RadSearchBox>
                        </div>
                    </div>
                    <div class="row">&nbsp;</div>
                    <telerik:RadListView ID="view" runat="server" CssClass="row" OnNeedDataSource="view_NeedDataSource" AllowPaging="true">
                        <ItemTemplate>
                            <div class="col-md-4 viewPad text-center">
                                <span class="btn btn-info dictionaryX"><%# Eval("RealName") %></span>
                                <input type="button" class="btn btn-danger" value="删" id="remove" runat="server" visible='<%# !OnlyOne %>' match='<%# Eval("Id") %>' onserverclick="remove_ServerClick" />
                            </div>
                        </ItemTemplate>
                    </telerik:RadListView>
                    <div class="row">
                        <div class="col-md-4">&nbsp;</div>
                        <div class="col-md-4 text-center">
                            <telerik:RadDataPager ID="pager" runat="server" PagedControlID="view" BackColor="Transparent" BorderStyle="None" RenderMode="Auto" PageSize="16">
                                <Fields>
                                    <telerik:RadDataPagerButtonField FieldType="FirstPrev"></telerik:RadDataPagerButtonField>
                                    <telerik:RadDataPagerButtonField FieldType="Numeric"></telerik:RadDataPagerButtonField>
                                    <telerik:RadDataPagerButtonField FieldType="NextLast"></telerik:RadDataPagerButtonField>
                                </Fields>
                            </telerik:RadDataPager>
                        </div>
                        <div class="col-md-4">&nbsp;</div>
                    </div>
                </div>
            </div>
        </telerik:RadAjaxPanel>
    </form>
</body>
</html>
