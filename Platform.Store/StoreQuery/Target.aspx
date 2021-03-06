﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Target.aspx.cs" Inherits="StoreQuery_Target" %>

<%@ Register Src="~/Control/SideBarSingle.ascx" TagPrefix="homory" TagName="SideBarSingle" %>
<%@ Register Src="~/Control/TargetHeader.ascx" TagPrefix="homory" TagName="TargetHeader" %>
<%@ Register Src="~/Control/TargetBody.ascx" TagPrefix="homory" TagName="TargetBody" %>

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
    <script>
        function printDepot() {
            bdhtml = window.document.body.innerHTML;
            sprnstr = "<!-- Start Printing -->";
            eprnstr = "<!-- End Printing -->";
            prnhtml = bdhtml.substring(bdhtml.indexOf(sprnstr) + 23);
            prnhtml = prnhtml.substring(0, prnhtml.indexOf(eprnstr));
            prnhtml = "<body>" + prnhtml + "</body>";
            window.document.body.innerHTML = prnhtml;
            window.print();
            window.document.body.innerHTML = bdhtml;
            return false;
        }
    </script>
</head>
<body>
    <form id="form" runat="server">
        <homory:SideBarSingle runat="server" ID="SideBarSingle" Crumb="日常查询 - 购置单查询" />
        <telerik:RadAjaxPanel ID="ap" runat="server" CssClass="container-fluid" LoadingPanelID="loading">
            <div class="row">
                <div class="col-md-12 text-center">
                    <telerik:RadDatePicker ID="periodx" runat="server" LocalizationPath="~/Language" ShowPopupOnFocus="true" Width="120" AutoPostBack="false">
                        <DatePopupButton runat="server" Visible="false" />
                    </telerik:RadDatePicker>
                    &nbsp;&nbsp;-&nbsp;&nbsp;
                            <telerik:RadDatePicker ID="period" runat="server" LocalizationPath="~/Language" ShowPopupOnFocus="true" Width="120" AutoPostBack="false">
                                <DatePopupButton runat="server" Visible="false" />
                            </telerik:RadDatePicker>
                            &nbsp;&nbsp;&nbsp;&nbsp;
                    <telerik:RadComboBox ID="combo" runat="server" AutoPostBack="false" Width="120">
                        <Items>
                            <telerik:RadComboBoxItem Text="办结状态" Value="2" Selected="true" />
                            <telerik:RadComboBoxItem Text="待办" Value="0" />
                            <telerik:RadComboBoxItem Text="已办" Value="1" />
                        </Items>
                    </telerik:RadComboBox>
                    &nbsp;&nbsp;&nbsp;&nbsp;
                    <telerik:RadComboBox ID="source" runat="server" AutoPostBack="false" Width="120" AppendDataBoundItems="true">
                    </telerik:RadComboBox>
                    &nbsp;&nbsp;&nbsp;&nbsp;
                    <telerik:RadComboBox ID="usage" runat="server" AutoPostBack="false" Width="120" AppendDataBoundItems="true" DataTextField="Name" DataValueField="Id">
                    </telerik:RadComboBox>
                    &nbsp;&nbsp;&nbsp;&nbsp;
                    <telerik:RadComboBox ID="people" runat="server" AutoPostBack="false" Width="120" DataTextField="RealName" DataValueField="Id" AppendDataBoundItems="true">
                    </telerik:RadComboBox>
                    &nbsp;&nbsp;&nbsp;&nbsp;
                    <input type="button" class="btn btn-tumblr" id="query" runat="server" value="查询" onserverclick="query_ServerClick" />
                </div>
            </div>
            <div class="row">&nbsp;</div>
            <div class="row">
                <input type="hidden" id="___total" runat="server" />
                <!-- Start Printing -->
                <telerik:RadListView ID="view" runat="server" OnNeedDataSource="view_NeedDataSource" ItemPlaceholderID="holder" AllowPaging="true">
                    <LayoutTemplate>
                        <div class="col-md-12">
                            <table class="storeTable text-center">
                                <tr>
                                    <homory:TargetHeader runat="server" ID="TargetHeader" />
                                    <th>操作</th>
                                </tr>
                                <asp:PlaceHolder ID="holder" runat="server"></asp:PlaceHolder>
                                <%--<tr>
                                    <td colspan="12">总计：<%# ___total.Value %></td>
                                </tr>--%>
                            </table>
                        </div>
                    </LayoutTemplate>
                    <ItemTemplate>
                        <tr>
                            <homory:TargetBody runat="server" ID="TargetBody" />
                            <td>
                                <input type="button" class="btn btn-tumblr" value="入库" id="in" runat="server" match='<%# Eval("Id") %>' visible='<%# !(bool)Eval("In") %>' onserverclick="in_ServerClick" />
                                <input type="button" class="btn btn-tumblr" value="编辑" id="edit" runat="server" match='<%# Eval("Id") %>' onserverclick="edit_ServerClick" />
                                <input type="button" class="btn btn-tumblr" value="删除" id="delx" runat="server" match='<%# Eval("Id") %>' visible='<%# CanDelete(Container.DataItem as Models.Store_Target) %>' onserverclick="delx_ServerClick" />
                                <input type="button" class="btn btn-tumblr" value="办结" id="done" runat="server" match='<%# Eval("Id") %>' visible='<%# !(bool)Eval("In") %>' onserverclick="done_ServerClick" />
                                <input type="button" class="btn btn-tumblr" value="补办" id="redo" runat="server" match='<%# Eval("Id") %>' visible='<%# (bool)Eval("In") && RightAdvanced %>' onserverclick="redo_ServerClick" />
                                <input type="button" class="btn btn-tumblr" value="打印" id="print" runat="server" match='<%# Eval("Id") %>' visible='<%# (bool)Eval("In") %>' onserverclick="print_ServerClick" />
                            </td>
                        </tr>
                    </ItemTemplate>
                    <EmptyDataTemplate>
                        <div class="col-md-12 text-center"><div class="btn btn-warning">暂无记录</div></div>
                    </EmptyDataTemplate>
                </telerik:RadListView>
                <!-- End Printing -->
            </div>
            <div class="row">
                <div class="col-md-12 text-center">
                    <input type="button" class="btn btn-tumblr" id="print" value="打印" onclick="printDepot();" />
                </div>
            </div>
            <div class="row">
                <div class="col-md-4">&nbsp;</div>
                <div class="col-md-4 text-center">
                    <telerik:RadDataPager ID="pager" runat="server" PagedControlID="view" BackColor="Transparent" BorderStyle="None" RenderMode="Auto" PageSize="10">
                        <Fields>
                            <telerik:RadDataPagerButtonField FieldType="FirstPrev"></telerik:RadDataPagerButtonField>
                            <telerik:RadDataPagerButtonField FieldType="Numeric"></telerik:RadDataPagerButtonField>
                            <telerik:RadDataPagerButtonField FieldType="NextLast"></telerik:RadDataPagerButtonField>
                        </Fields>
                    </telerik:RadDataPager>
                </div>
                <div class="col-md-4">&nbsp;</div>
            </div>
        </telerik:RadAjaxPanel>
    </form>
</body>
</html>
