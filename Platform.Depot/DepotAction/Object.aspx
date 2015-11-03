<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Object.aspx.cs" Inherits="DepotAction_Object" %>

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
        function gox(obj) {
            var id = $(obj).attr("goid");
            var idx = $(obj).attr("did");
            window.open('../DepotQuery/Object?ObjectId=' + id + "&DepotId=" + idx, '_blank');
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
                            <input id="add" runat="server" type="button" class="btn btn-info" value="新增" onserverclick="add_ServerClick" />
                        </div>
                        <div class="col-md-6 text-center">
                            <telerik:RadTextBox ID="toSearch" runat="server" Width="250" EmptyMessage="输入要检索的物资名称"></telerik:RadTextBox>
                            <input id="search" runat="server" type="button" class="btn btn-info" value="检索" onserverclick="search_ServerClick" />
                        </div>
                        <div class="col-md-3 text-right">
                            <input id="view_simple" runat="server" type="button" value="简洁模式" onserverclick="view_simple_ServerClick" />
                            <input id="view_photo" runat="server" type="button" value="图文模式" onserverclick="view_photo_ServerClick" />
                        </div>
                    </div>
                    <div class="row">&nbsp;</div>
                    <div class="row">
                        <div class="col-md-12" style="color: #2B2B2B;">
                            <telerik:RadListView ID="view" runat="server" OnNeedDataSource="view_NeedDataSource" ItemPlaceholderID="holder" AllowPaging="true">
                                <LayoutTemplate>
                                    <asp:Panel runat="server" Visible='<%# IsSimple %>'>
                                        <table class="storeTable">
                                            <tr>
                                                <th>序号</th>
                                                <th>名称</th>
                                                <th>单位</th>
                                                <th>品牌</th>
                                                <th>规格</th>
                                                <th>总数</th>
                                                <th>在库</th>
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
                                            <td style="cursor: pointer; color: #3E5A70;" onclick="gox(this);" goid='<%# Eval("Id") %>' did='<%# Depot.Id %>'><%# Eval("Name") %></td>
                                            <td><%# Eval("Unit") %></td>
                                            <td><%# Eval("Brand") %></td>
                                            <td><%# Eval("Specification") %></td>
                                            <td><%# CountTotal(Container.DataItem as Models.DepotObject) %></td>
                                            <td><%# Eval("Amount").ToAmount(Depot.Featured(Models.DepotType.小数数量库)) %></td>
                                            <td>
                                                <input type="button" class="btn btn-tumblr" value="入" id="in" runat="server" match='<%# Eval("Id") %>' onserverclick="in_ServerClick" visible='<%# !Depot.Featured(Models.DepotType.固定资产库) %>' />
                                                <input type="button" class="btn btn-tumblr" value="借" id="usex" runat="server" match='<%# Eval("Id") %>' visible='<%# (decimal)Eval("Amount") > 0 && !((bool)Eval("Consumable")) %>' onserverclick="usex_ServerClick" />
                                                <input type="button" class="btn btn-tumblr" value="领" id="use" runat="server" match='<%# Eval("Id") %>' visible='<%# (decimal)Eval("Amount") > 0 && !((bool)Eval("Single")) %>' onserverclick="use_ServerClick" />
                                                <input type="button" class="btn btn-tumblr" value="废" id="out" runat="server" match='<%# Eval("Id") %>' visible='<%# (decimal)Eval("Amount") > 0 %>' onserverclick="out_ServerClick" />
                                                <input type="button" class="btn btn-tumblr" value="编" id="edit" runat="server" match='<%# Eval("Id") %>' onserverclick="edit_ServerClick" />
                                                <input type="button" class="btn btn-tumblr" value="删" id="delete" runat="server" match='<%# Eval("Id") %>' onserverclick="delete_ServerClick" visible='<%# (Container.DataItem as Models.DepotObject).Amount == 0 %>' />
                                            </td>
                                        </tr>
                                    </asp:Panel>
                                    <asp:Panel runat="server" Visible='<%# !IsSimple %>'>
                                        <div class="col-md-3">
                                            <div class="row" style="margin: 30px 0; border: solid 1px silver;">
                                                <div class="col-md-12" style="width: 100%; height: 100%;">
                                                    <div style="height: 160px; text-align: center; cursor: pointer;" onclick="gox(this);" goid='<%# Eval("Id") %>' did='<%# Depot.Id %>'>
                                                        <img class="img-responsive" style="height: 158px; margin: auto;" src='<%# Eval("ImageA").None() ? "../Content/Images/Transparent.png" : Eval("ImageA") %>' />
                                                    </div>
                                                    <div style="height: 90px;">
                                                        <table style="margin: auto; width: 90%;">
                                                            <tr style="line-height: 57px; height: 57px; text-align: center;">
                                                                <td colspan="2" style="line-height: 55px; height: 55px; text-align: center; cursor: pointer;">
                                                                    <span class="btn btn-danger" onclick="gox(this);" goid='<%# Eval("Id") %>' did='<%# Depot.Id %>'><%# Eval("Name") %></span>
                                                                </td>
                                                            </tr>
                                                            <tr style="line-height: 28px; height: 28px; text-align: center;">
                                                                <td style="line-height: 28px; height: 28px; text-align: center;">总数：<%# CountTotal(Container.DataItem as Models.DepotObject) %>
                                                                </td>
                                                                <td style="line-height: 28px; height: 28px; text-align: center;">在库：<%# Eval("Amount").ToAmount(Depot.Featured(Models.DepotType.小数数量库)) %>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </div>
                                                    <div class="text-center" style="height: 55px;">
                                                        <input type="button" class="btn btn-tumblr" value="入" id="in2" runat="server" match='<%# Eval("Id") %>' onserverclick="in_ServerClick" visible='<%# !Depot.Featured(Models.DepotType.固定资产库) %>' />
                                                        <input type="button" class="btn btn-tumblr" value="借" id="use2x" runat="server" match='<%# Eval("Id") %>' visible='<%# (decimal)Eval("Amount") > 0 && !((bool)Eval("Consumable")) %>' onserverclick="usex_ServerClick" />
                                                        <input type="button" class="btn btn-tumblr" value="领" id="use2" runat="server" match='<%# Eval("Id") %>' visible='<%# (decimal)Eval("Amount") > 0 && !((bool)Eval("Single")) %>' onserverclick="use_ServerClick" />
                                                        <input type="button" class="btn btn-tumblr" value="废" id="out2" runat="server" match='<%# Eval("Id") %>' visible='<%# (decimal)Eval("Amount") > 0 %>' onserverclick="out_ServerClick" />
                                                        <input type="button" class="btn btn-tumblr" value="编" id="edit2" runat="server" match='<%# Eval("Id") %>' onserverclick="edit_ServerClick" />
                                                        <input type="button" class="btn btn-tumblr" value="删" id="delete2" runat="server" match='<%# Eval("Id") %>' visible='<%# (Container.DataItem as Models.DepotObject).Amount == 0 %>' onserverclick="delete_ServerClick" />
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
                        <div class="col-md-3">&nbsp;</div>
                        <div class="col-md-6 text-center">
                            <telerik:RadDataPager ID="pager" runat="server" PagedControlID="view" BackColor="Transparent" BorderStyle="None" RenderMode="Auto" PageSize="8">
                                <Fields>
                                    <telerik:RadDataPagerButtonField FieldType="FirstPrev"></telerik:RadDataPagerButtonField>
                                    <telerik:RadDataPagerButtonField FieldType="Numeric"></telerik:RadDataPagerButtonField>
                                    <telerik:RadDataPagerButtonField FieldType="NextLast"></telerik:RadDataPagerButtonField>
                                </Fields>
                            </telerik:RadDataPager>
                        </div>
                        <div class="col-md-3">&nbsp;</div>
                    </div>
                </div>
            </div>
        </telerik:RadAjaxPanel>
    </form>
</body>
</html>
