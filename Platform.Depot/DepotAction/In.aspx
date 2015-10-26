﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="In.aspx.cs" Inherits="DepotAction_In" %>

<%@ Register Src="~/Control/SideBarSingle.ascx" TagPrefix="homory" TagName="SideBarSingle" %>
<%@ Register Src="~/Control/ObjectIn.ascx" TagPrefix="homory" TagName="ObjectIn" %>

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
        var adjust = 0;
    </script>
    <!--[if lt IE 9]>
	    <script src="../Content/Homory/js/html5shiv.js"></script>
	    <script src="../Content/Homory/js/respond.min.js"></script>
    <![endif]-->
</head>
<body>
    <form id="form" runat="server">
        <homory:SideBarSingle runat="server" ID="SideBarSingle" Crumb="物资管理 - 物资入库" />
        <telerik:RadCodeBlock runat="server">
            <script>
                function calc(sender, args) {
                    var g_in_price;
                    var id = sender.get_id().replace("amount", "").replace("perPrice", "");
                    var g_in_amount = $find(id + "amount").get_value();
                    var g_in_price = $find(id + "perPrice").get_value();
                    $find(id + "money").set_value(g_in_amount * g_in_price);
                }
                function calcTotal(sender, args) {
                    var g_total = 0.00;
                    var ccs = $("input[tocalc='calc']");
                    for (var j = 0; j < ccs.length; j++) {
                        var v = $find(ccs[j].id).get_value();
                        if (v)
                            g_total += v;
                    }
                    $find('<%= total.ClientID %>').set_value(g_total + adjust);
                }
                function set_adj(value) {
                    adjust = value;
                    var co = $find('<%= total.ClientID %>');
                    if (!co.get_value()) {
                        co.set_value(adjust);
                    }
                }
            </script>
        </telerik:RadCodeBlock>
        <telerik:RadAjaxPanel ID="ap" runat="server" CssClass="container-fluid" LoadingPanelID="loading">
            <div class="row">
                <div class="col-md-2">
                    <div class="btn btn-tumblr dictionaryX">
                        购置单选择
                    </div>
                </div>
                <div class="col-md-10 text-left">
                    <telerik:RadMonthYearPicker ID="period" runat="server" LocalizationPath="~/Language" ShowPopupOnFocus="true" Width="100" AutoPostBack="true" OnSelectedDateChanged="period_SelectedDateChanged">
                        <DatePopupButton runat="server" Visible="false" />
                    </telerik:RadMonthYearPicker>
                    &nbsp;&nbsp;&nbsp;&nbsp;
                    <telerik:RadComboBox ID="source" runat="server" AutoPostBack="true" Width="120" AppendDataBoundItems="true" DataTextField="Name" DataValueField="Name" OnSelectedIndexChanged="source_SelectedIndexChanged">
                    </telerik:RadComboBox>
                    &nbsp;&nbsp;&nbsp;&nbsp;
                    <telerik:RadComboBox ID="usage" runat="server" AutoPostBack="true" Width="120" AppendDataBoundItems="true" DataTextField="Name" DataValueField="Name" OnSelectedIndexChanged="usage_SelectedIndexChanged">
                    </telerik:RadComboBox>
                    &nbsp;&nbsp;&nbsp;&nbsp;
                    <telerik:RadComboBox ID="people" runat="server" AutoPostBack="true" Width="120" DataTextField="Name" DataValueField="Id" AppendDataBoundItems="true" OnSelectedIndexChanged="people_SelectedIndexChanged">
                    </telerik:RadComboBox>
                    &nbsp;&nbsp;&nbsp;&nbsp;
                    <telerik:RadComboBox ID="target" runat="server" AutoPostBack="true" LocalizationPath="~/Language" DataTextField="Name" DataValueField="Id" Filter="Contains" MarkFirstMatch="true" ShowToggleImage="true" Width="400" AllowCustomText="true" OnSelectedIndexChanged="target_SelectedIndexChanged"></telerik:RadComboBox>
                </div>
            </div>
            <div class="row">
                <telerik:RadListView ID="view_target" runat="server" OnNeedDataSource="view_target_NeedDataSource" ItemPlaceholderID="holder">
                    <LayoutTemplate>
                        <div class="col-md-12">
                            <table class="storeTable text-center">
                                <tr>
                                    <th>购置单号</th>
                                    <th>发票编号</th>
                                    <th>购置时间</th>
                                    <th>采购来源</th>
                                    <th>使用对象</th>
                                    <th>应付金额</th>
                                    <th>实付金额</th>
                                    <th>保管人</th>
                                    <th>经手人</th>
                                    <th>清单简述</th>
                                    <th>操作人</th>
                                </tr>
                                <asp:PlaceHolder ID="holder" runat="server"></asp:PlaceHolder>
                            </table>
                        </div>
                    </LayoutTemplate>
                    <ItemTemplate>
                        <tr>
                            <td><%# Eval("购置单号") %></td>
                            <td><%# Eval("发票编号") %></td>
                            <td><%# Eval("TimeNode").ToDay() %></td>
                            <td><%# Eval("采购来源") %></td>
                            <td><%# Eval("使用对象") %></td>
                            <td><%# Eval("应付金额").ToMoney() %></td>
                            <td><%# Eval("实付金额").ToMoney() %></td>
                            <td><%# Eval("保管人") %></td>
                            <td><%# Eval("经手人") %></td>
                            <td style="cursor: pointer;">
                                <span id="target_note" runat="server">清单简述</span>
                                <telerik:RadToolTip ID="tooltip" runat="server" TargetControlID="target_note" Skin="Metro">
                                    <%# Eval("清单简述") %>
                                </telerik:RadToolTip>
                            </td>
                            <td><%# Eval("操作人") %></td>
                        </tr>
                    </ItemTemplate>
                    <EmptyDataTemplate>
                    </EmptyDataTemplate>
                </telerik:RadListView>
            </div>
            <div class="row">&nbsp;</div>
            <div class="row" id="x1" runat="server">
                <div class="col-md-2">
                    <div class="btn btn-tumblr dictionaryX">
                        入库物资选择
                    </div>
                </div>
            </div>
            <div class="row" id="x2" runat="server">
                <div class="col-md-12">
                    <telerik:RadListView ID="view_obj" runat="server" OnNeedDataSource="view_obj_NeedDataSource" ItemPlaceholderID="inHolder" OnItemDataBound="view_obj_ItemDataBound">
                        <LayoutTemplate>
                            <table class="storeTable text-center">
                                <tr>
                                    <th>物资类别</th>
                                    <th>物资名称</th>
                                    <th>单位</th>
                                    <th>规格</th>
                                    <th>库存</th>
                                    <th>入库日期</th>
                                    <th>数量</th>
                                    <th>单价</th>
                                    <th style="display: none;">优惠价</th>
                                    <th>合计</th>
                                    <th>年龄段</th>
                                    <th>存放地</th>
                                    <th>备注</th>
                                </tr>
                                <asp:PlaceHolder ID="inHolder" runat="server"></asp:PlaceHolder>
                            </table>
                        </LayoutTemplate>
                        <ItemTemplate>
                            <homory:ObjectIn runat="server" ID="ObjectIn" ItemIndex='<%# Container.DataItemIndex %>' TargetId='<%# target.SelectedValue.GlobalId() %>' />
                        </ItemTemplate>
                    </telerik:RadListView>
                </div>
                <div class="col-md-4">
                    <input type="button" class="btn btn-tumblr" id="plus" runat="server" value="+" onserverclick="plus_ServerClick" />
                    <input type="hidden" id="counter" runat="server" value="0" />
                    <input type="hidden" id="x" runat="server" value="" />
                </div>
                <div class="col-md-4">&nbsp;</div>
                <div class="col-md-4 text-left">
                    <div class="btn btn-info">总额：</div>
                    <telerik:RadNumericTextBox ID="total" runat="server" Width="120" NumberFormat-DecimalDigits="2" DataType="System.Decimal" AllowOutOfRangeAutoCorrect="true" NumberFormat-AllowRounding="false"></telerik:RadNumericTextBox>
                </div>
                <div class="col-md-12 text-center">
                    <input type="button" class="btn btn-tumblr" id="do_in" runat="server" value="入库" onserverclick="do_in_ServerClick" />
                    &nbsp;&nbsp;
                    <input type="button" class="btn btn-tumblr" id="Button2" runat="server" value="入库并办结购置单" onserverclick="done_in_ServerClick" />
                </div>
            </div>
            <div class="row">&nbsp;</div>
            <div class="row" id="x3" runat="server">
                <div class="col-md-2">
                    <div class="btn btn-tumblr dictionaryX">
                        本购置单入库记录
                    </div>
                </div>
            </div>
            <div class="row" id="x4" runat="server">
                <div class="col-md-12">
                    <telerik:RadListView ID="view_record" runat="server" OnNeedDataSource="view_record_NeedDataSource" ItemPlaceholderID="recordHolder">
                        <LayoutTemplate>
                            <table class="storeTable text-center">
                                <tr>
                                    <th>物资类别</th>
                                    <th>物资名称</th>
                                    <th>单位</th>
                                    <th>规格</th>
                                    <th>入库日期</th>
                                    <th>数量</th>
                                    <th>单价</th>
                                    <th style="display: none;">优惠价</th>
                                    <th>合计</th>
                                    <th>存放地</th>
                                    <th>备注</th>
                                </tr>
                                <asp:PlaceHolder ID="recordHolder" runat="server"></asp:PlaceHolder>
                            </table>
                        </LayoutTemplate>
                        <ItemTemplate>
                            <tr>
                                <td><%# Eval("类别") %></td>
                                <td><%# Eval("物资名称") %></td>
                                <td><%# Eval("单位") %></td>
                                <td><%# Eval("规格") %></td>
                                <td><%# Eval("入库日期").ToDay() %></td>
                                <td><%# Eval("数量").ToAmount() %></td>
                                <td><%# Eval("单价").ToMoney() %></td>
                                <td style="display: none;"><%# Eval("优惠价").ToMoney() %></td>
                                <td><%# Eval("合计").ToMoney() %></td>
                                <td><%# Eval("存放地") %></td>
                                <td><%# Eval("备注") %></td>
                            </tr>
                        </ItemTemplate>
                        <EmptyDataTemplate>
                        </EmptyDataTemplate>
                    </telerik:RadListView>
                </div>
            </div>
        </telerik:RadAjaxPanel>
    </form>
</body>
</html>
