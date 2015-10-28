<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Period.aspx.cs" Inherits="DepotSetting_Period" %>

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
    <!--[if lt IE 9]>
	    <script src="../Content/Homory/js/html5shiv.js"></script>
	    <script src="../Content/Homory/js/respond.min.js"></script>
    <![endif]-->
</head>
<body>
    <form id="form" runat="server">
        <homory:SideBarSingle runat="server" ID="SideBarSingle" Crumb="系统设置 - 借还时限" />
        <telerik:RadAjaxPanel ID="ap" runat="server" CssClass="container-fluid" LoadingPanelID="loading">
            <div class="row">
                <div class="col-md-2">
                    <span class="btn btn-tumblr dictionaryX">时限（按月计数）</span>
                </div>
                <div class="col-md-8 text-center" style="margin-top: 1px;">
                    <telerik:RadNumericTextBox ID="month" runat="server" NumberFormat-DecimalDigits="0" MinValue="0" Width="183" EnabledStyle-HorizontalAlign="Center" DataType="System.Int32" AllowOutOfRangeAutoCorrect="true"></telerik:RadNumericTextBox>
                    &nbsp;&nbsp;
                    <input type="button" class="btn btn-tumblr" id="save" runat="server" value="保存" title="新增物资" onserverclick="save_ServerClick" />
                </div>
                <div class="col-md-2">&nbsp;</div>
            </div>
            <div>&nbsp;</div>
            <div class="row">
                <div class="col-md-2">
                    <span class="btn btn-tumblr dictionaryX">取消时限用户</span>
                </div>
                <div class="col-md-8 text-center" style="margin-top: 1px;">
                    <telerik:RadSearchBox ID="search" runat="server" EmptyMessage="用户姓名或拼音首字母" DataTextField="Name" Width="250" DataValueField="Id" MaxResultCount="10" LocalizationPath="~/Language" ShowLoadingIcon="false" ShowSearchButton="false" OnDataSourceSelect="search_DataSourceSelect" OnSearch="search_Search"></telerik:RadSearchBox>
                </div>
                <div class="col-md-2">&nbsp;</div>
            </div>
            <div class="row">&nbsp;</div>
            <telerik:RadListView ID="view" runat="server" CssClass="row" OnNeedDataSource="view_NeedDataSource" AllowPaging="true">
                <ItemTemplate>
                    <div class="col-md-4 viewPad text-center">
                        <span class="btn btn-info dictionaryX"><%# Eval("Name") %></span>
                        <input type="button" class="btn btn-danger" value="删" id="remove" runat="server" match='<%# Eval("Id") %>' onserverclick="remove_ServerClick" />
                    </div>
                </ItemTemplate>
            </telerik:RadListView>
            <div class="row">
                <div class="col-md-4">&nbsp;</div>
                <div class="col-md-4 text-center">
                    <telerik:RadDataPager ID="pager" runat="server" PagedControlID="view" BackColor="Transparent" BorderStyle="None" RenderMode="Auto" PageSize="16">
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
