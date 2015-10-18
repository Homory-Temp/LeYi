<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Use.aspx.cs" Inherits="StoreAction_Use" %>

<%@ Register Src="~/Control/SideBarSingle.ascx" TagPrefix="homory" TagName="SideBarSingle" %>
<%@ Register Src="~/Control/ObjectUseHeader.ascx" TagPrefix="homory" TagName="ObjectUseHeader" %>
<%@ Register Src="~/Control/ObjectUseBody.ascx" TagPrefix="homory" TagName="ObjectUseBody" %>

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
        <homory:SideBarSingle runat="server" ID="SideBarSingle" Crumb="物资管理 - 物资借领" />
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
                    $find('<%= total.ClientID %>').set_value(g_total);
                }
            </script>
        </telerik:RadCodeBlock>
        <telerik:RadAjaxPanel ID="ap" runat="server" CssClass="container-fluid" LoadingPanelID="loading">
            <div class="row">
                <div class="col-md-2">
                    <div class="btn btn-tumblr dictionaryX">
                        借领对象选择
                    </div>
                </div>
                <div class="col-md-10 text-left">
                    <telerik:RadComboBox ID="usage" runat="server" AutoPostBack="true" Width="120" AppendDataBoundItems="true" DataTextField="Name" DataValueField="Id" OnSelectedIndexChanged="usage_SelectedIndexChanged">
                    </telerik:RadComboBox>
                    &nbsp;&nbsp;&nbsp;&nbsp;
                    <telerik:RadComboBox ID="people" runat="server" EmptyMessage="借领人" LocalizationPath="~/Language" DataTextField="RealName" DataValueField="Id" Filter="Contains" MarkFirstMatch="true" AppendDataBoundItems="true" ShowToggleImage="false" Width="240" AllowCustomText="true" AutoPostBack="true" OnSelectedIndexChanged="people_SelectedIndexChanged">
                        <Items>
                            <telerik:RadComboBoxItem Text="" Value="" Selected="true" />
                        </Items>
                        <ItemTemplate>
                            <%# Eval("RealName") %><span style="display: none;"><%# Eval("PinYin") %></span>
                        </ItemTemplate>
                    </telerik:RadComboBox>
                </div>
            </div>
            <div class="row">&nbsp;</div>
            <div class="row" id="x1" runat="server">
                <div class="col-md-2">
                    <div class="btn btn-tumblr dictionaryX">
                        借领物资选择
                    </div>
                </div>
            </div>
            <div class="row" id="x2" runat="server">
                <div class="col-md-12">
                    <telerik:RadListView ID="view_obj" runat="server" OnNeedDataSource="view_obj_NeedDataSource" ItemPlaceholderID="inHolder" OnItemDataBound="view_obj_ItemDataBound">
                        <LayoutTemplate>
                            <table class="storeTable text-center">
                                <homory:ObjectUseHeader runat="server" ID="ObjectInHeader" />
                                <asp:PlaceHolder ID="inHolder" runat="server"></asp:PlaceHolder>
                            </table>
                        </LayoutTemplate>
                        <ItemTemplate>
                            <homory:ObjectUseBody runat="server" ID="ObjectInBody" ItemIndex='<%# Container.DataItemIndex %>' />
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
                    <input type="button" class="btn btn-tumblr" id="do_use" runat="server" value="借领" onserverclick="do_use_ServerClick" />
                </div>
            </div>
        </telerik:RadAjaxPanel>
    </form>
</body>
</html>
