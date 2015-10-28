<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Use.aspx.cs" Inherits="DepotScan_Use" %>

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
        <homory:SideBarSingle runat="server" ID="SideBarSingle" Crumb="物资扫描 - 扫码出库" />
        <telerik:RadAjaxPanel ID="ap" runat="server" CssClass="container-fluid" LoadingPanelID="loading">
            <div class="row">
                <div class="col-md-2">
                    <div class="btn btn-tumblr dictionaryX">
                        出库信息选择
                    </div>
                </div>
                <div class="col-md-10 text-left">
                    <%--<telerik:RadComboBox ID="age" runat="server" AutoPostBack="true" MaxHeight="203" Width="120" AppendDataBoundItems="true" DataTextField="Name" DataValueField="Name" OnSelectedIndexChanged="usage_SelectedIndexChanged">
                    </telerik:RadComboBox>
                    &nbsp;&nbsp;&nbsp;&nbsp;--%>
                    <telerik:RadComboBox ID="people" runat="server" EmptyMessage="借领人" MaxHeight="203" LocalizationPath="~/Language" DataTextField="Name" DataValueField="Id" Filter="Contains" MarkFirstMatch="true" AppendDataBoundItems="true" ShowToggleImage="false" Width="240" AllowCustomText="true" AutoPostBack="true" OnSelectedIndexChanged="people_SelectedIndexChanged">
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
                        出库物资选择
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
                    <telerik:RadListView ID="view_obj" runat="server" OnNeedDataSource="view_obj_NeedDataSource" ItemPlaceholderID="useHolder" OnItemDataBound="view_obj_ItemDataBound">
                        <LayoutTemplate>
                            <table class="storeTable text-center">
                                <tr>
                                    <th>物资类别</th>
                                    <th>物资名称</th>
                                    <th>出库类型</th>
                                    <th>单位</th>
                                    <th>规格</th>
                                    <th>库存</th>
                                    <th>数量/编号</th>
                                    <th style="display: none;">年龄段</th>
                                    <th>存放地</th>
                                    <th>备注</th>
                                </tr>
                                <asp:PlaceHolder ID="useHolder" runat="server"></asp:PlaceHolder>
                            </table>
                        </LayoutTemplate>
                        <ItemTemplate>
                            <homory:ObjectUse runat="server" ID="ObjectUse" ItemIndex='<%# Container.DataItemIndex %>' />
                        </ItemTemplate>
                    </telerik:RadListView>
                </div>
                <div class="col-md-4">
                    <input type="button" class="btn btn-tumblr" id="plus" runat="server" value="+" title="增加" onserverclick="plus_ServerClick" />
                    <input type="hidden" id="counter" runat="server" value="0" />
                    <input type="hidden" id="x" runat="server" value="" />
                </div>
                <div class="col-md-4">&nbsp;</div>
                <div class="col-md-4 text-left">
                    &nbsp;
                </div>
                <div class="col-md-12 text-center">
                    <input type="button" class="btn btn-tumblr" id="do_use" runat="server" value="出库" onserverclick="do_use_ServerClick" />
                </div>
            </div>
            <div class="row" id="x3" runat="server" visible="false">
                <div class="col-md-12 text-center">
                    <div class="btn btn-danger">该用户存在指定期限内未归还的物品，请先归还。</div>
                </div>
            </div>
        </telerik:RadAjaxPanel>
    </form>
</body>
</html>
