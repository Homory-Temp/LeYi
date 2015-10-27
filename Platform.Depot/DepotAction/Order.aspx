<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Order.aspx.cs" Inherits="DepotAction_Order" %>

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
        var t_day; var t_source; var t_usage;
    </script>
    <!--[if lt IE 9]>
	    <script src="../Content/Homory/js/html5shiv.js"></script>
	    <script src="../Content/Homory/js/respond.min.js"></script>
    <![endif]-->
</head>
<body>
    <form id="form" runat="server">
        <homory:SideBarSingle runat="server" ID="SideBarSingle" Crumb="物资管理 - 购置登记" />
        <telerik:RadCodeBlock runat="server">
            <script>
                function renewDay(sender, args) {
                    var s = args.get_renderDay().get_date();
                    t_day = s[0].toString() + (s[1].toString().length == 1 ? ('0' + s[1].toString()) : s[1].toString()) + (s[2].toString().length == 1 ? ('0' + s[2].toString()) : s[2].toString());
                    genNumber();
                }

                function renewUsage(sender, args) {
                    if (args.get_item() && args.get_item().get_text())
                        t_usage = args.get_item().get_text();
                    genNumber();
                }

                function renewSource(sender, args) {
                    if (args.get_item() && args.get_item().get_text())
                        t_source = args.get_item().get_text();
                    genNumber();
                }

                function renewSourceX(sender, args) {
                    if (sender.get_text())
                        t_source = sender.get_text();
                    genNumber();
                }

                function genNumber() {
                    var t_number = $find("<%= number.ClientID %>");
                    t_number.set_emptyMessage(t_day + (t_source ? '_' : '') + t_source + '_' + t_usage);
                }
            </script>
        </telerik:RadCodeBlock>
        <telerik:RadAjaxPanel ID="ap" runat="server" CssClass="container-fluid" LoadingPanelID="loading">
            <div class="row">
                <div class="col-md-2"></div>
                <div class="col-md-8">
                    <div class="row">
                        <div class="col-md-4 text-right">购置日期：</div>
                        <div class="col-md-8 text-left">
                            <telerik:RadDatePicker ID="day" runat="server" LocalizationPath="~/Language" ShowPopupOnFocus="true" Width="400" AutoPostBack="false" Calendar-ClientEvents-OnDateClick="renewDay">
                                <DatePopupButton runat="server" Visible="false" />
                            </telerik:RadDatePicker>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-4 text-right">购置来源：</div>
                        <div class="col-md-8 text-left">
                            <telerik:RadComboBox ID="source" runat="server" MaxHeight="203" LocalizationPath="~/Language" DataTextField="Name" DataValueField="Name" Filter="Contains" MarkFirstMatch="true" AppendDataBoundItems="true" ShowToggleImage="false" Width="400" AllowCustomText="true" OnClientSelectedIndexChanged="renewSource" OnClientDropDownClosed="renewSourceX">
                                <Items>
                                    <telerik:RadComboBoxItem Text="" Value="" Selected="true" />
                                </Items>
                                <ItemTemplate>
                                    <%# Eval("Name") %><span style="display: none;"><%# Eval("PinYin") %></span>
                                </ItemTemplate>
                            </telerik:RadComboBox>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-4 text-right">使用对象：</div>
                        <div class="col-md-8 text-left">
                            <telerik:RadComboBox ID="usage" runat="server" MaxHeight="203" LocalizationPath="~/Language" DataTextField="Name" DataValueField="Name" Filter="Contains" MarkFirstMatch="true" AppendDataBoundItems="true" ShowToggleImage="false" Width="400" AllowCustomText="true" OnClientSelectedIndexChanged="renewUsage">
                                <Items>
                                    <telerik:RadComboBoxItem Text="" Value="" Selected="true" />
                                </Items>
                                <ItemTemplate>
                                    <%# Eval("Name") %><span style="display: none;"><%# Eval("PinYin") %></span>
                                </ItemTemplate>
                            </telerik:RadComboBox>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-4 text-right">购置单号：</div>
                        <div class="col-md-8 text-left">
                            <telerik:RadTextBox ID="number" runat="server" Width="400"></telerik:RadTextBox>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-4 text-right">发票编号：</div>
                        <div class="col-md-8 text-left">
                            <telerik:RadTextBox ID="receipt" runat="server" Width="400"></telerik:RadTextBox>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-4 text-right">清单简述：</div>
                        <div class="col-md-8 text-left">
                            <telerik:RadTextBox ID="content" runat="server" Width="400"></telerik:RadTextBox>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-4 text-right">应付金额：</div>
                        <div class="col-md-8 text-left">
                            <telerik:RadNumericTextBox ID="toPay" runat="server" NumberFormat-DecimalDigits="2" DataType="System.Decimal" AllowOutOfRangeAutoCorrect="true" Width="400"></telerik:RadNumericTextBox>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-4 text-right">实付金额：</div>
                        <div class="col-md-8 text-left">
                            <telerik:RadNumericTextBox ID="paid" runat="server" NumberFormat-DecimalDigits="2" DataType="System.Decimal" AllowOutOfRangeAutoCorrect="true" Width="400"></telerik:RadNumericTextBox>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-4 text-right">保管人：</div>
                        <div class="col-md-8 text-left">
                            <telerik:RadComboBox ID="keep" runat="server" MaxHeight="203" LocalizationPath="~/Language" DataTextField="Name" DataValueField="Id" Filter="Contains" MarkFirstMatch="true" AppendDataBoundItems="true" ShowToggleImage="false" Width="400" AllowCustomText="true">
                                <Items>
                                    <telerik:RadComboBoxItem Text="" Value="" Selected="true" />
                                </Items>
                                <ItemTemplate>
                                    <%# Eval("Name") %><span style="display: none;"><%# Eval("PinYin") %></span>
                                </ItemTemplate>
                            </telerik:RadComboBox>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-4 text-right">经手人：</div>
                        <div class="col-md-8 text-left">
                            <telerik:RadComboBox ID="brokerage" runat="server" MaxHeight="203" LocalizationPath="~/Language" DataTextField="Name" DataValueField="Id" Filter="Contains" MarkFirstMatch="true" AppendDataBoundItems="true" ShowToggleImage="false" Width="400" AllowCustomText="true">
                                <Items>
                                    <telerik:RadComboBoxItem Text="" Value="" Selected="true" />
                                </Items>
                                <ItemTemplate>
                                    <%# Eval("Name") %><span style="display: none;"><%# Eval("PinYin") %></span>
                                </ItemTemplate>
                            </telerik:RadComboBox>
                        </div>
                    </div>
                    <div class="row">&nbsp;</div>
                    <div class="row">
                        <div class="col-md-4 text-right">&nbsp;</div>
                        <div class="col-md-8 text-left">
                            <input type="button" class="btn btn-tumblr" id="go" runat="server" value="保存" onserverclick="go_ServerClick" />
                            &nbsp;&nbsp;&nbsp;&nbsp;
                            <input type="button" class="btn btn-tumblr" id="goon" runat="server" value="保存并继续" onserverclick="goon_ServerClick" />
                            &nbsp;&nbsp;&nbsp;&nbsp;
                            <input type="button" class="btn btn-tumblr" id="in" runat="server" value="保存并入库" onserverclick="in_ServerClick" />
                            &nbsp;&nbsp;&nbsp;&nbsp;
                            <input type="button" class="btn btn-tumblr" id="cancel" runat="server" value="取消" onserverclick="cancel_ServerClick" />
                        </div>
                    </div>
                </div>
                <div class="col-md-2"></div>
            </div>
        </telerik:RadAjaxPanel>
    </form>
</body>
</html>
