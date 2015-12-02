<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Out.aspx.cs" Inherits="DepotAction_Out" %>

<%@ Register Src="~/Control/SideBarSingle.ascx" TagPrefix="homory" TagName="SideBarSingle" %>
<%@ Register Src="~/Control/ObjectOut.ascx" TagPrefix="homory" TagName="ObjectOut" %>

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
        <homory:SideBarSingle runat="server" ID="SideBarSingle" Crumb="物资管理 - 物资报废" />
        <telerik:RadAjaxPanel ID="ap" runat="server" CssClass="container-fluid" LoadingPanelID="loading">
            <div class="row">
                <div class="col-md-2">
                    <div class="btn btn-tumblr dictionaryX">
                        报废信息选择
                    </div>
                </div>
                <div class="col-md-10 text-left">
                    <telerik:RadComboBox ID="people" runat="server" MaxHeight="203" EmptyMessage="报废申请人" LocalizationPath="~/Language" DataTextField="Name" DataValueField="Id" Filter="Contains" MarkFirstMatch="true" AppendDataBoundItems="true" ShowToggleImage="false" Width="240" AllowCustomText="true" AutoPostBack="true" OnSelectedIndexChanged="people_SelectedIndexChanged">
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
                        报废物资选择
                    </div>
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
                                    <th>单位</th>
                                    <th>品牌</th>
                                    <th>规格</th>
                                    <th>库存</th>
                                    <th>数量/编号</th>
                                    <th>报废原因</th>
                                </tr>
                                <asp:PlaceHolder ID="useHolder" runat="server"></asp:PlaceHolder>
                            </table>
                        </LayoutTemplate>
                        <ItemTemplate>
                            <homory:ObjectOut runat="server" ID="ObjectOut" ItemIndex='<%# Container.DataItemIndex %>' />
                        </ItemTemplate>
                    </telerik:RadListView>
                </div>
                <div class="col-md-4">
                    <input type="button" class="btn btn-tumblr" id="plus" runat="server" value="+" title="增加" onserverclick="plus_ServerClick" />
                    <input type="button" class="btn btn-tumblr" id="back" runat="server" value="<<" title="返回" onserverclick="back_ServerClick" />
                    <input type="hidden" id="counter" runat="server" value="0" />
                    <input type="hidden" id="x" runat="server" value="" />
                </div>
                <div class="col-md-4">&nbsp;</div>
                <div class="col-md-4 text-left">
                    &nbsp;
                </div>
                <div class="col-md-12 text-center">
                    <input type="button" class="btn btn-tumblr" id="do_out" runat="server" value="报废" onserverclick="do_out_ServerClick" />
                </div>
            </div>
        </telerik:RadAjaxPanel>
    </form>
</body>
</html>
