<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Dictionary.aspx.cs" Inherits="StoreSetting_Dictionary" %>

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
        <homory:SideBarSingle runat="server" ID="SideBarSingle" Crumb="系统设置 - 基础数据" />
        <telerik:RadAjaxPanel ID="ap" runat="server" CssClass="container-fluid" LoadingPanelID="loading">
            <div class="row">
                <div class="col-md-2"></div>
                <div class="col-md-8">
                    <div class="row">
                        <div class="col-md-12">
                            <span class="btn btn-tumblr">基础数据类型：</span>
                            <span>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span>
                            <input id="d1" runat="server" type="button" class="btn btn-warning dictionary" value="单位" onserverclick="d_ServerClick" />
                            <span>&nbsp;&nbsp;&nbsp;&nbsp;</span>
                            <input id="d2" runat="server" type="button" class="btn btn-info dictionary" value="规格" onserverclick="d_ServerClick" />
                            <span>&nbsp;&nbsp;&nbsp;&nbsp;</span>
                            <input id="d3" runat="server" type="button" class="btn btn-info dictionary" value="采购来源" onserverclick="d_ServerClick" />
                            <span>&nbsp;&nbsp;&nbsp;&nbsp;</span>
                            <input id="d4" runat="server" type="button" class="btn btn-info dictionary" value="使用对象" onserverclick="d_ServerClick" />
                            <span id="ds4" runat="server">&nbsp;&nbsp;&nbsp;&nbsp;</span>
                            <input id="d5" runat="server" type="button" class="btn btn-info dictionary" value="年龄段" onserverclick="d_ServerClick" />
                            <input id="dh" runat="server" type="hidden" value="1" />
                        </div>
                    </div>
                    <div class="row">&nbsp;</div>
                    <div class="row">
                        <div class="col-md-12">
                            <telerik:RadTextBox ID="name" runat="server" Width="400"></telerik:RadTextBox>
                            &nbsp;&nbsp;
                            <input id="add" runat="server" type="button" class="btn btn-tumblr" value="保存" onserverclick="add_ServerClick" />
                        </div>
                    </div>
                    <div class="row">&nbsp;</div>
                    <div class="row">
                        <telerik:RadListView ID="view" runat="server" OnNeedDataSource="view_NeedDataSource" AllowPaging="true">
                            <ItemTemplate>
                                <div class="col-md-3 viewPad text-center">
                                    <span class="btn btn-info dictionaryX"><%# Eval("Name") %></span>
                                    <input type="button" class="btn btn-danger" value="删" id="remove" runat="server" match='<%# Eval("Name") %>' onserverclick="remove_ServerClick" />
                                </div>
                            </ItemTemplate>
                        </telerik:RadListView>
                    </div>
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
                </div>
                <div class="col-md-2"></div>
            </div>
        </telerik:RadAjaxPanel>
    </form>
</body>
</html>
