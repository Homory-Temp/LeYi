<%@ Page Language="C#" AutoEventWireup="true" CodeFile="In.aspx.cs" Inherits="StoreAction_In" %>

<%@ Register Src="~/Control/SideBarSingle.ascx" TagPrefix="homory" TagName="SideBarSingle" %>
<%@ Register Src="~/Control/TargetHeader.ascx" TagPrefix="homory" TagName="TargetHeader" %>
<%@ Register Src="~/Control/TargetBody.ascx" TagPrefix="homory" TagName="TargetBody" %>
<%@ Register Src="~/Control/ObjectInHeader.ascx" TagPrefix="homory" TagName="ObjectInHeader" %>
<%@ Register Src="~/Control/ObjectInBody.ascx" TagPrefix="homory" TagName="ObjectInBody" %>
<%@ Register Src="~/Control/RecordInHeader.ascx" TagPrefix="homory" TagName="RecordInHeader" %>
<%@ Register Src="~/Control/RecordInBody.ascx" TagPrefix="homory" TagName="RecordInBody" %>

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
                function accAdd(arg1, arg2) {
                    var r1, r2, m;
                    try { r1 = arg1.toString().split(".")[1].length } catch (e) { r1 = 0 }
                    try { r2 = arg2.toString().split(".")[1].length } catch (e) { r2 = 0 }
                    m = Math.pow(10, Math.max(r1, r2));
                    return (arg1 * m + arg2 * m) / m;
                }
                function accMul(arg1, arg2) {
                    var m = 0, s1 = arg1.toString(), s2 = arg2.toString();
                    try { m += s1.split(".")[1].length } catch (e) { }
                    try { m += s2.split(".")[1].length } catch (e) { }
                    return Number(s1.replace(".", "")) * Number(s2.replace(".", "")) / Math.pow(10, m)
                }
                function calc(sender, args) {
                    var g_in_price;
                    var id = sender.get_id().replace("amount", "").replace("perPrice", "");
                    var g_in_amount = $find(id + "amount").get_value();
                    var g_in_price = $find(id + "perPrice").get_value();
                    $find(id + "money").set_value(accMul(g_in_amount, g_in_price));
                }
                function calcTotal(sender, args) {
                    var g_total = 0.00;
                    var ccs = $("input[tocalc='calc']");
                    for (var j = 0; j < ccs.length; j++) {
                        var v = $find(ccs[j].id).get_value();
                        if (v) {
                            g_total = accAdd(g_total, v);
                        }
                    }
                    $find('<%= total.ClientID %>').set_value(g_total + adjust + 0.0000000000001);
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
                    <telerik:RadComboBox ID="source" runat="server" AutoPostBack="true" Width="120" AppendDataBoundItems="true" OnSelectedIndexChanged="source_SelectedIndexChanged">
                    </telerik:RadComboBox>
                    &nbsp;&nbsp;&nbsp;&nbsp;
                    <telerik:RadComboBox ID="usage" runat="server" AutoPostBack="true" Width="120" AppendDataBoundItems="true" DataTextField="Name" DataValueField="Id" OnSelectedIndexChanged="usage_SelectedIndexChanged">
                    </telerik:RadComboBox>
                    &nbsp;&nbsp;&nbsp;&nbsp;
                    <telerik:RadComboBox ID="people" runat="server" AutoPostBack="true" Width="120" DataTextField="RealName" DataValueField="Id" AppendDataBoundItems="true" OnSelectedIndexChanged="people_SelectedIndexChanged">
                    </telerik:RadComboBox>
                    &nbsp;&nbsp;&nbsp;&nbsp;
                    <telerik:RadComboBox ID="target" runat="server" AutoPostBack="true" LocalizationPath="~/Language" DataTextField="购置单号" EmptyMessage="购置单" DataValueField="Id" Filter="Contains" MarkFirstMatch="true" ShowToggleImage="true" Width="400" AllowCustomText="true" OnSelectedIndexChanged="target_SelectedIndexChanged"></telerik:RadComboBox>
                </div>
            </div>
            <div class="row">
                <telerik:RadListView ID="view_target" runat="server" OnNeedDataSource="view_target_NeedDataSource" ItemPlaceholderID="holder">
                    <LayoutTemplate>
                        <div class="col-md-12">
                            <table class="storeTable text-center">
                                <tr>
                                    <homory:TargetHeader runat="server" ID="TargetHeader" />
                                </tr>
                                <asp:PlaceHolder ID="holder" runat="server"></asp:PlaceHolder>
                            </table>
                        </div>
                    </LayoutTemplate>
                    <ItemTemplate>
                        <tr>
                            <homory:TargetBody runat="server" ID="TargetBody" />
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
                <div class="col-md-8">
                    &nbsp;
                </div>
                <div class="col-md-2 text-right">
                    <input type="button" class="btn btn-tumblr" id="addObj" runat="server" value="新增物资" title="新增物资" onserverclick="addObj_ServerClick" />
                </div>
            </div>
            <div class="row" id="x2" runat="server">
                <div class="col-md-12">
                    <telerik:RadListView ID="view_obj" runat="server" OnNeedDataSource="view_obj_NeedDataSource" ItemPlaceholderID="inHolder" OnItemDataBound="view_obj_ItemDataBound">
                        <LayoutTemplate>
                            <table class="storeTable text-center">
                                <homory:ObjectInHeader runat="server" ID="ObjectInHeader" />
                                <asp:PlaceHolder ID="inHolder" runat="server"></asp:PlaceHolder>
                            </table>
                        </LayoutTemplate>
                        <ItemTemplate>
                            <homory:ObjectInBody runat="server" ID="ObjectInBody" ItemIndex='<%# Container.DataItemIndex %>' TargetId='<%# target.SelectedValue.GlobalId() %>' />
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
                                    <homory:RecordInHeader runat="server" ID="ObjectInHeader" />
                                </tr>
                                <asp:PlaceHolder ID="recordHolder" runat="server"></asp:PlaceHolder>
                            </table>
                        </LayoutTemplate>
                        <ItemTemplate>
                            <tr>
                                <homory:RecordInBody runat="server" ID="ObjectInBody" ItemIndex='<%# Container.DataItemIndex %>' />
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
