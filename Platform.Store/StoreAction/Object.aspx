﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Object.aspx.cs" Inherits="StoreAction_Object" %>

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
    <script>
        function sg(sender, e) {
            if (e.get_keyCode() == 13) {
                $("#search").click();
            }
        }
    </script>
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
                        <div class="col-md-3">
                            <span class="btn btn-tumblr">物资：</span>
                            <input id="add" runat="server" type="button" class="btn btn-info" value="新增" onserverclick="add_ServerClick" />
                        </div>
                        <div class="col-md-6">
                            <telerik:RadTextBox ID="toSearch" runat="server" Width="200" EmptyMessage="输入要检索的物资名称" ClientEvents-OnKeyPress="sg"></telerik:RadTextBox>
                            &nbsp;&nbsp;&nbsp;&nbsp;
                             <input id="search" runat="server" type="button" class="btn btn-info" value="检索" onserverclick="search_ServerClick" />
                        </div>
                        <div class="col-md-3 text-right">
                            <input id="view_simple" runat="server" type="button" value="简洁模式" onserverclick="view_simple_ServerClick" />
                            <input id="view_photo" runat="server" type="button" value="图文模式" onserverclick="view_photo_ServerClick" />
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12" style="color: #2B2B2B;">
                            <telerik:RadListView ID="view" runat="server" OnNeedDataSource="view_NeedDataSource" ItemPlaceholderID="holder" AllowPaging="true">
                                <LayoutTemplate>
                                    <asp:Panel runat="server" Visible='<%# IsSimple %>'>
                                        <table class="storeTable">
                                            <tr>
                                                <th>序号</th>
                                                <th>物资编号</th>
                                                <th>名称</th>
                                                <th>单位</th>
                                                <th>规格</th>
                                                <th>库存</th>
                                                <th>操作</th>
                                            </tr>
                                    </asp:Panel>
                                    <asp:PlaceHolder ID="holder" runat="server"></asp:PlaceHolder>
                                    <asp:Panel runat="server" Visible='<%# IsSimple %>'>
                                        </table>
                                    </asp:Panel>
                                </LayoutTemplate>
                                <ItemTemplate>
                                    <asp:Panel runat="server" Visible='<%# IsSimple %>'>
                                        <tr>
                                            <td><%# Eval("Ordinal") %></td>
                                            <td><%# Eval("Code") %></td>
                                            <td><%# Eval("Name") %></td>
                                            <td><%# Eval("Unit") %></td>
                                            <td><%# Eval("Specification") %></td>
                                            <td><%# Eval("Amount").ToAmount() %></td>
                                            <td>
                                                <input type="button" class="btn btn-tumblr" value="入" id="in" runat="server" match='<%# Eval("Id") %>' onserverclick="in_ServerClick" />
                                                <input type="button" class="btn btn-tumblr" value="领" id="use" runat="server" match='<%# Eval("Id") %>' visible='<%# (decimal)Eval("Amount") > 0 %>' onserverclick="use_ServerClick" />
                                                <input type="button" class="btn btn-tumblr" value="编" id="edit" runat="server" match='<%# Eval("Id") %>' onserverclick="edit_ServerClick" />
                                                <input type="button" class="btn btn-tumblr" value="删" id="delete" runat="server" match='<%# Eval("Id") %>' onserverclick="delete_ServerClick" visible='<%# (Container.DataItem as Models.StoreObject).StoreIn.Count == 0 %>' />
                                            </td>
                                        </tr>
                                    </asp:Panel>
                                    <asp:Panel runat="server" Visible='<%# !IsSimple %>'>
                                        <div class="col-md-3">
                                            <div class="row" style="margin: 30px; border: solid 1px silver;">
                                                <div class="col-md-12" style="width: 100%; height: 100%;">
                                                    <div style="height: 160px; text-align: center; cursor: pointer;">
                                                        <img class="img-responsive" style="height: 158px; margin: auto;" src='<%# Eval("Image").Null() ? "../Content/Images/Transparent.png" : Eval("Image").ToString().Split(new char[] { '*' })[0] %>' />
                                                    </div>
                                                    <div style="height: 145px;">
                                                        <table style="margin: auto; width: 90%;">
                                                            <tr style="line-height: 28px; height: 28px; text-align: center;">
                                                                <td style="line-height: 55px; height: 55px; text-align: center; cursor: pointer;">
                                                                    <span class="btn btn-danger"><%# Eval("Name") %></span>
                                                                </td>
                                                            </tr>
                                                            <tr style="line-height: 28px; height: 28px; text-align: left;">
                                                                <td style="line-height: 28px; height: 28px; text-align: left;">库存：<%# Eval("Amount").ToAmount() %>&nbsp;<%# Eval("Unit") %>
                                                                </td>
                                                            </tr>
                                                            <tr style="line-height: 28px; height: 28px; text-align: left;">
                                                                <td style="line-height: 28px; height: 28px; text-align: left;">编号：<%# Eval("Code") %>
                                                                </td>
                                                            </tr>
                                                            <tr style="line-height: 28px; height: 28px; text-align: left;">
                                                                <td style="line-height: 28px; height: 28px; text-align: left;">规格：<%# Eval("Specification") %>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </div>
                                                    <div style="height: 55px;">
                                                        <input type="button" class="btn btn-tumblr" value="入" id="in2" runat="server" match='<%# Eval("Id") %>' onserverclick="in_ServerClick" />
                                                        <input type="button" class="btn btn-tumblr" value="领" id="use2" runat="server" match='<%# Eval("Id") %>' visible='<%# (decimal)Eval("Amount") > 0 %>' onserverclick="use_ServerClick" />
                                                        <input type="button" class="btn btn-tumblr" value="编" id="edit2" runat="server" match='<%# Eval("Id") %>' onserverclick="edit_ServerClick" />
                                                        <input type="button" class="btn btn-tumblr" value="删" id="delete2" runat="server" match='<%# Eval("Id") %>' visible='<%# (Container.DataItem as Models.StoreObject).StoreIn.Count == 0 %>' onserverclick="delete_ServerClick" />
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </asp:Panel>
                                </ItemTemplate>
                            </telerik:RadListView>
                        </div>
                    </div>
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
