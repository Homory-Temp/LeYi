<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Move.aspx.cs" Inherits="Depot_Move" %>

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
            window.open('../DepotQuery/ObjectFixedMove?ObjectId=' + id + "&DepotId=" + idx, '_blank');
        }
    </script>
    <!--[if lt IE 9]>
	    <script src="../Content/Homory/js/html5shiv.js"></script>
	    <script src="../Content/Homory/js/respond.min.js"></script>
    <![endif]-->
</head>
<body>
    <form id="form" runat="server">
        <homory:SideBarSingle runat="server" ID="SideBarSingle" Crumb="分库提醒" />
        <telerik:RadAjaxPanel ID="ap" runat="server" CssClass="container-fluid" LoadingPanelID="loading">
            <div class="row">
                <div class="col-md-12" style="text-align: left;">
                    <div class="row">
                        <div class="col-md-3">
                            <span id="add" runat="server" class="btn btn-tumblr"></span>
                        </div>
                        <div class="col-md-6 text-center">
                            <telerik:RadTextBox ID="toSearch" runat="server" Width="200" EmptyMessage="输入要检索的物资名称"></telerik:RadTextBox>
                            &nbsp;&nbsp;&nbsp;&nbsp;
                            <input id="search" runat="server" type="button" class="btn btn-info" value="检索" onserverclick="search_ServerClick" />
                        </div>
                        <div class="col-md-3 text-right">&nbsp;</div>
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
                                                <th>总库数量</th>
                                                <%--<th>分库数量</th>--%>
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
                                            <td style="cursor: pointer; color: #3E5A70;" onclick="gox(this);" goid='<%# Eval("Id") %>' did='<%# Depot.Id %>'><%# (bool)Eval("Fixed") ? "[固] " : "" %><%# Eval("Name") %></td>
                                            <td><%# Eval("Unit") %></td>
                                            <td><%# Eval("Brand") %></td>
                                            <td><%# Eval("Specification") %></td>
                                            <td><%# CountTotal(Container.DataItem as Models.DepotObject).ToAmount(Depot.Featured(Models.DepotType.小数数量库)) %></td>
                                            <%--<td><%# CountDone(Container.DataItem as Models.DepotObject).ToAmount(Depot.Featured(Models.DepotType.小数数量库)) %></td>--%>
                                        </tr>
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
            <input id="line_no" runat="server" value="16" type="hidden" />
        </telerik:RadAjaxPanel>
    </form>
</body>
</html>
