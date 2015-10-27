<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Move.aspx.cs" Inherits="DepotAction_Move" %>

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
    <!--[if lt IE 9]>
	    <script src="../Content/Homory/js/html5shiv.js"></script>
	    <script src="../Content/Homory/js/respond.min.js"></script>
    <![endif]-->
</head>
<body>
    <form id="form" runat="server">
        <homory:SideBarSingle runat="server" ID="SideBarSingle" Crumb="物资管理 - 资产分库" />
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
                    <telerik:RadComboBox ID="source" runat="server" AutoPostBack="true" MaxHeight="203" Width="120" AppendDataBoundItems="true" DataTextField="Name" DataValueField="Name" OnSelectedIndexChanged="source_SelectedIndexChanged">
                    </telerik:RadComboBox>
                    &nbsp;&nbsp;&nbsp;&nbsp;
                    <telerik:RadComboBox ID="usage" runat="server" AutoPostBack="true" MaxHeight="203" Width="120" AppendDataBoundItems="true" DataTextField="Name" DataValueField="Name" OnSelectedIndexChanged="usage_SelectedIndexChanged">
                    </telerik:RadComboBox>
                    &nbsp;&nbsp;&nbsp;&nbsp;
                    <telerik:RadComboBox ID="people" runat="server" AutoPostBack="true" MaxHeight="203" Width="120" DataTextField="Name" DataValueField="Id" AppendDataBoundItems="true" OnSelectedIndexChanged="people_SelectedIndexChanged">
                    </telerik:RadComboBox>
                    &nbsp;&nbsp;&nbsp;&nbsp;
                    <telerik:RadComboBox ID="target" runat="server" AutoPostBack="true" MaxHeight="203" LocalizationPath="~/Language" DataTextField="Name" DataValueField="Id" Filter="Contains" MarkFirstMatch="true" ShowToggleImage="true" Width="400" AllowCustomText="true" OnSelectedIndexChanged="target_SelectedIndexChanged"></telerik:RadComboBox>
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
                                    <th>购置来源</th>
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
                            <td><%# Eval("OrderTime").ToDay() %></td>
                            <td><%# Eval("购置来源") %></td>
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
            <div class="row">
                <div class="col-md-2">
                    <div class="btn btn-tumblr dictionaryX">
                        目标库选择
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-md-12 text-center">
                    <div>
                        <telerik:RadComboBox ID="depots" runat="server" AutoPostBack="true" MaxHeight="203" LocalizationPath="~/Language" DataTextField="Name" DataValueField="Id" Filter="Contains" MarkFirstMatch="true" ShowToggleImage="true" Width="400" AllowCustomText="true" OnSelectedIndexChanged="depots_SelectedIndexChanged"></telerik:RadComboBox>
                    </div>
                </div>
            </div>
            <div class="row">&nbsp;</div>
            <div class="row">
                <div class="col-md-12 text-center">
                    <input type="button" class="btn btn-tumblr" id="do_move" runat="server" value="转移" onserverclick="do_move_ServerClick" />
                </div>
            </div>
        </telerik:RadAjaxPanel>
    </form>
</body>
</html>
