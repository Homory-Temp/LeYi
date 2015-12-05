<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ObjectSingle.aspx.cs" Inherits="DepotAction_ObjectSingle" %>

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
        <homory:SideBarSingle runat="server" ID="SideBarSingle" Crumb="物资管理 - 资产查询" />
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
                        <div class="col-md-12 text-center">
                            物资名称：
                            <telerik:RadTextBox ID="toSearch" runat="server" Width="120"></telerik:RadTextBox>
                            &nbsp;&nbsp;&nbsp;&nbsp;
                            购置日期：
                                                <telerik:RadDatePicker ID="periodx" runat="server" LocalizationPath="~/Language" ShowPopupOnFocus="true" Width="100" AutoPostBack="false" MinDate="1900-01-01">
                                                    <DatePopupButton runat="server" Visible="false" />
                                                    <Calendar runat="server">
                                                        <FastNavigationSettings TodayButtonCaption="今日" OkButtonCaption="确认" CancelButtonCaption="取消"></FastNavigationSettings>
                                                    </Calendar>
                                                </telerik:RadDatePicker>
                            &nbsp;&nbsp;-&nbsp;&nbsp;
                    <telerik:RadDatePicker ID="period" runat="server" LocalizationPath="~/Language" ShowPopupOnFocus="true" Width="100" AutoPostBack="false" MinDate="1900-01-01">
                        <DatePopupButton runat="server" Visible="false" />
                                                    <Calendar runat="server">
                                                        <FastNavigationSettings TodayButtonCaption="今日" OkButtonCaption="确认" CancelButtonCaption="取消"></FastNavigationSettings>
                                                    </Calendar>
                    </telerik:RadDatePicker>
                            &nbsp;&nbsp;&nbsp;&nbsp;
                            卡片编号：
                            <telerik:RadTextBox ID="no" runat="server" Width="120"></telerik:RadTextBox>
                            &nbsp;&nbsp;&nbsp;&nbsp;
                            物资条码：
                            <telerik:RadTextBox ID="qr" runat="server" Width="120"></telerik:RadTextBox>
                            &nbsp;&nbsp;&nbsp;&nbsp;
                            存放地：
                            <telerik:RadTextBox ID="place" runat="server" Width="120"></telerik:RadTextBox>
                            &nbsp;&nbsp;&nbsp;&nbsp;
                            <input id="search" runat="server" type="button" class="btn btn-info" value="检索" onserverclick="search_ServerClick" />
                        </div>
                    </div>
                    <div class="row">&nbsp;</div>
                    <div class="row">
                        <div class="col-md-12" style="color: #2B2B2B;">
                            <telerik:RadListView ID="view" runat="server" OnNeedDataSource="view_NeedDataSource" ItemPlaceholderID="holder" AllowPaging="true" OnItemDataBound="view_ItemDataBound">
                                <LayoutTemplate>
                                    <table class="storeTablePrint">
                                        <tr>
                                            <th>物资名称</th>
                                            <th>卡片编号</th>
                                            <th>物资条码</th>
                                            <th>购置日期</th>
                                            <th>单价</th>
                                            <th>单位</th>
                                            <th>品牌</th>
                                            <th>规格</th>
                                            <th>存放地</th>
                                        </tr>
                                        <asp:PlaceHolder ID="holder" runat="server"></asp:PlaceHolder>
                                    </table>
                                </LayoutTemplate>
                                <ItemTemplate>
                                    <tr>
                                        <td><%# Eval("物资") %></td>
                                        <td><%# Eval("卡片编号") %></td>
                                        <td><%# Eval("条码") %></td>
                                        <td><%# Eval("购置日期").ToDay() %></td>
                                        <td><%# Eval("单价").ToMoney() %></td>
                                        <td><%# Eval("单位") %></td>
                                        <td><%# Eval("品牌") %></td>
                                        <td><%# Eval("规格") %></td>
                                        <td id="xp" runat="server" match='<%# Eval("条码") %>' matchp='<%# Eval("存放地") %>'>
                                            <asp:Label runat="server" Text='<%# Eval("存放地") %>' Style="cursor: pointer; color: #3e5a70;"></asp:Label>
                                            <telerik:RadToolTip ID="xt" runat="server" IsClientID="true" ManualClose="true" ManualCloseButtonText="" Position="MiddleLeft" Skin="Metro">
                                                <asp:GridView ID="xv" runat="server" AutoGenerateColumns="false">
                                                    <HeaderStyle Font-Size="Medium" />
                                                    <RowStyle Font-Size="Medium" />
                                                    <AlternatingRowStyle Font-Size="Medium" />
                                                    <EmptyDataRowStyle Font-Size="Medium" />
                                                    <Columns>
                                                        <asp:BoundField DataField="Time" HeaderStyle-Width="100" HeaderStyle-CssClass="coreAuto text-center" HeaderText="日期" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" DataFormatString="{0:yyyy-MM-dd}" />
                                                        <asp:BoundField DataField="Place" HeaderStyle-Width="200" HeaderStyle-CssClass="coreAuto text-center" HeaderText="存放地" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" />
                                                    </Columns>
                                                </asp:GridView>
                                            </telerik:RadToolTip>
                                        </td>
                                    </tr>
                                </ItemTemplate>
                            </telerik:RadListView>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-3">&nbsp;</div>
                        <div class="col-md-6 text-center">
                            <telerik:RadDataPager ID="pager" runat="server" PagedControlID="view" BackColor="Transparent" BorderStyle="None" RenderMode="Auto" PageSize="20">
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
