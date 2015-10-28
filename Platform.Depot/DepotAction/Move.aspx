<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Move.aspx.cs" Inherits="DepotAction_Move" %>

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
    <style>
        .depotm {
            margin-top: 4px;
        }
    </style>
</head>
<body>
    <form id="form" runat="server">
        <homory:SideBarSingle runat="server" ID="SideBarSingle" />
        <telerik:RadAjaxPanel ID="ap" runat="server" CssClass="container-fluid" LoadingPanelID="loading">
            <div class="row">
                <div class="col-md-2" style="border-right: 1px solid #2B2B2B;">
                    <div class="row">
                        <div class="col-md-12">
                            <span class="btn btn-tumblr">类别：</span>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12">
                            <telerik:RadTreeView ID="tree0" runat="server" OnNodeClick="tree0_NodeClick" ShowLineImages="false">
                                <Nodes>
                                    <telerik:RadTreeNode Value="0" Selected="true"></telerik:RadTreeNode>
                                </Nodes>
                            </telerik:RadTreeView>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12">
                            <telerik:RadTreeView ID="tree" runat="server" OnNodeClick="tree_NodeClick" DataTextField="Name" DataValueField="Id" DataFieldID="Id" DataFieldParentID="ParentId">
                                <NodeTemplate>
                                    <%# Eval("Name") %><%# Eval("Count").EmptyWhenZero() %>
                                </NodeTemplate>
                            </telerik:RadTreeView>
                        </div>
                    </div>
                </div>
                <div class="col-md-10" style="text-align: left;">
                    <div class="row">
                        <div class="col-md-3">
                            <span class="btn btn-tumblr">物资：</span>
                            <input id="all" runat="server" type="button" class="btn btn-info" value="全选" onserverclick="all_ServerClick" />
                        </div>
                        <div class="col-md-6 text-center">
                            <telerik:RadTextBox ID="toSearch" runat="server" Width="200" EmptyMessage="输入要检索的物资名称"></telerik:RadTextBox>
                            &nbsp;&nbsp;
                            <telerik:RadComboBox ID="combo" runat="server" MaxHeight="203" AutoPostBack="false" Width="120">
                                <Items>
                                    <telerik:RadComboBoxItem Text="分库状态" Value="0" Selected="true" />
                                    <telerik:RadComboBoxItem Text="已分库" Value="1" />
                                    <telerik:RadComboBoxItem Text="未分库" Value="2" />
                                </Items>
                            </telerik:RadComboBox>
                            &nbsp;&nbsp;
                            <input id="search" runat="server" type="button" class="btn btn-info" value="检索" onserverclick="search_ServerClick" />
                        </div>
                        <div class="col-md-3 text-right">&nbsp;</div>
                    </div>
                    <div class="row">&nbsp;</div>
                    <div class="row">
                        <telerik:RadListView ID="view" runat="server" OnNeedDataSource="view_NeedDataSource" ItemPlaceholderID="holder">
                            <ItemTemplate>
                                <span class="col-md-2 depotm"><span class="btn btn-info" title='<%# "规格：{0}".Formatted(Eval("Specification")) %>'><asp:CheckBox ID="check" runat="server" Text='<%# "&nbsp;{0}".Formatted(Eval("Name")) %>' /></span></span>
                            </ItemTemplate>
                        </telerik:RadListView>
                    </div>
                    <div class="row">&nbsp;</div>
                </div>
            </div>
        </telerik:RadAjaxPanel>
    </form>
</body>
</html>
