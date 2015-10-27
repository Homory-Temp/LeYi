<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Return.aspx.cs" Inherits="DepotScan_Return" %>

<%@ Register Src="~/Control/SideBarSingle.ascx" TagPrefix="homory" TagName="SideBarSingle" %>
<%@ Register Src="~/Control/ObjectUse.ascx" TagPrefix="homory" TagName="ObjectUse" %>

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
        <homory:SideBarSingle runat="server" ID="SideBarSingle" Crumb="物资扫描 - 扫描归还" />
        <telerik:RadAjaxPanel ID="ap" runat="server" CssClass="container-fluid" LoadingPanelID="loading">
            <div class="row">
                <div class="col-md-2">
                    <div class="btn btn-tumblr dictionaryX">
                        借用对象选择
                    </div>
                </div>
                <div class="col-md-10 text-left">
                    <telerik:RadComboBox ID="people" runat="server"  MaxHeight="203" EmptyMessage="借领人" LocalizationPath="~/Language" DataTextField="Name" DataValueField="Id" Filter="Contains" MarkFirstMatch="true" AppendDataBoundItems="true" ShowToggleImage="false" Width="240" AllowCustomText="true" AutoPostBack="true" OnSelectedIndexChanged="people_SelectedIndexChanged">
                        <Items>
                            <telerik:RadComboBoxItem Text="" Value="" Selected="true" />
                        </Items>
                        <ItemTemplate>
                            <%# Eval("Name") %><span style="display: none;"><%# Eval("PinYin") %></span>
                        </ItemTemplate>
                    </telerik:RadComboBox>
                    &nbsp;&nbsp;&nbsp;&nbsp;
                    <telerik:RadDatePicker ID="time" runat="server" LocalizationPath="~/Language" ShowPopupOnFocus="true" Width="100" AutoPostBack="true">
                        <Calendar runat="server">
                            <FastNavigationSettings TodayButtonCaption="今日" OkButtonCaption="确定" CancelButtonCaption="取消"></FastNavigationSettings>
                        </Calendar>
                        <DatePopupButton runat="server" Visible="false" />
                    </telerik:RadDatePicker>
                </div>
            </div>
            <div class="row">&nbsp;</div>
            <div class="row" id="x1" runat="server">
                <div class="col-md-2">
                    <div class="btn btn-tumblr dictionaryX">
                        归还物资选择
                    </div>
                </div>
                <div class="col-md-8 text-center">
                    <telerik:RadTextBox runat="server" ID="scan" Width="200" EmptyMessage="请扫描二维码"></telerik:RadTextBox>
                    &nbsp;&nbsp;&nbsp;&nbsp;
                    <input type="button" class="btn btn-tumblr" id="scanAdd" runat="server" value="添加" title="添加物资" onserverclick="scanAdd_ServerClick" />
                </div>
                <div class="col-md-2">
                    &nbsp;
                </div>
            </div>
            <div class="row" id="x2" runat="server">
                <div class="col-md-12">
                    <telerik:RadListView ID="view_obj" runat="server" OnNeedDataSource="view_obj_NeedDataSource" ItemPlaceholderID="useHolder">
                        <LayoutTemplate>
                            <table class="storeTable text-center">
                                <tr>
                                    <th>借用日期</th>
                                    <th>物资名称</th>
                                    <th>借用数量</th>
                                    <th>待归还数</th>
                                    <th>单价</th>
                                    <th>合计</th>
                                    <th>归还数</th>
                                    <th>备注</th>
                                </tr>
                                <asp:PlaceHolder ID="useHolder" runat="server"></asp:PlaceHolder>
                            </table>
                        </LayoutTemplate>
                        <ItemTemplate>
                            <tr>
                                <td style="display: none;">
                                    <asp:Label runat="server" ID="id" Text='<%# Eval("Id") %>'></asp:Label>
                                    <asp:Label runat="server" ID="code" Text='<%# Gen(Container.DataItem as Models.DepotUseXRecord) %>'></asp:Label>
                                </td>
                                <td>
                                    <%# Eval("Time").ToDay() %>
                                </td>
                                <td>
                                    <%# Eval("Name") %>
                                </td>
                                <td>
                                    <%# Eval("Amount").ToAmount(Depot.Featured(Models.DepotType.固定资产库)) %>
                                </td>
                                <td>
                                    <%# ((decimal)Eval("Amount") - (decimal)Eval("ReturnedAmount")).ToAmount(Depot.Featured(Models.DepotType.固定资产库)) %>
                                </td>
                                <td>
                                    <%# Eval("PriceSet").ToMoney() %>
                                </td>
                                <td>
                                    <%# Eval("Money").ToMoney() %>
                                </td>
                                <td>
                                    <telerik:RadNumericTextBox ID="amount" runat="server" Width="120" MaxValue='<%# (double)((decimal)Eval("Amount") - (decimal)Eval("ReturnedAmount")) %>' NumberFormat-DecimalDigits='<%# Depot.Featured(Models.DepotType.小数数量库) ? 2 : 0 %>' DataType="System.Decimal" AllowOutOfRangeAutoCorrect="true"></telerik:RadNumericTextBox>
                                </td>
                                <td>
                                    <telerik:RadTextBox ID="note" runat="server" Width="100"></telerik:RadTextBox>
                                </td>
                            </tr>
                        </ItemTemplate>
                        <EmptyDataTemplate>
                                <div class="col-md-12 text-center">
                                    <div class="btn btn-warning">无借用记录</div>
                                </div>
                        </EmptyDataTemplate>
                    </telerik:RadListView>
                </div>
                <div class="col-md-4">
                    <input type="hidden" id="counter" runat="server" value="0" />
                    <input type="hidden" id="x" runat="server" value="" />
                </div>
                <div class="col-md-4">&nbsp;</div>
                <div class="col-md-4 text-left">
                    &nbsp;
                </div>
                <div class="col-md-12 text-center">
                    <input type="button" class="btn btn-tumblr" id="do_return" runat="server" value="归还" onserverclick="do_return_ServerClick" />
                </div>
            </div>
        </telerik:RadAjaxPanel>
    </form>
</body>
</html>
