<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Object.aspx.cs" Inherits="StoreAction_Object" %>

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
        <homory:SideBarSingle runat="server" ID="SideBarSingle" Crumb="物资管理 - 物资管理" />
        <telerik:RadAjaxPanel ID="ap" runat="server" CssClass="container-fluid" LoadingPanelID="loading">
            <div class="row">
                <div class="col-md-2" style="border-right: 1px solid #2B2B2B;">
                    <div class="row">
                        <div class="col-md-12">
                            <span class="btn btn-tumblr">类别：</span>
                            <input id="manage" runat="server" type="button" class="btn btn-info" value="管理" onserverclick="manage_ServerClick" />
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
                                    <%# Eval("Name") %><%# db.Value.CountObjects((Guid)Eval("Id"), StoreId).Single().Value.EmptyWhenZero() %>
                                </NodeTemplate>
                            </telerik:RadTreeView>
                        </div>
                    </div>
                </div>
                <div class="col-md-10" style="text-align: left;">
                    <div class="row">
                        <div class="col-md-4">
                            <span class="btn btn-tumblr">物资：</span>
                            <input id="add" runat="server" type="button" class="btn btn-info" value="新增" onserverclick="add_ServerClick" />
                        </div>
                        <div class="col-md-4">&nbsp;</div>
                        <div class="col-md-4 text-right">
                            <input id="view_simple" runat="server" type="button" value="简洁模式" onserverclick="view_simple_ServerClick" />
                            <input id="view_photo" runat="server" type="button" value="图文模式" onserverclick="view_photo_ServerClick" />
                        </div>
                    </div>
                    <div class="row">
                        <telerik:RadListView ID="view" runat="server" OnNeedDataSource="view_NeedDataSource" ItemPlaceholderID="holder">
                            <LayoutTemplate>
                                <div></div>
                            </LayoutTemplate>
                            <ItemTemplate>

                            </ItemTemplate>
                        </telerik:RadListView>
                    </div>
                </div>
            </div>
        </telerik:RadAjaxPanel>
    </form>
</body>
</html>
