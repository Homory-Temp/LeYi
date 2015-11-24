<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Home.aspx.cs" Inherits="StoreHome_Home" %>

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
        <homory:SideBarSingle runat="server" ID="SideBarSingle" Crumb="快速导航" NoCrumb="true" />
        <div class="container">
            <div class="row">
                <div class="col-md-6">
                    <div class="panel panel-info">
                        <div class="panel-heading panel">
                            <div class="panel-title text-center">
                                物资管理
                            </div>
                        </div>
                        <div class="panel-body">
                            <telerik:RadListView ID="view_action" runat="server" CssClass="panel-body" OnNeedDataSource="view_action_NeedDataSource">
                                    <ItemTemplate>
                                        <a class="btn btn-info dictionaryX" href='<%# Eval("Url") %>'><%# Eval("Name") %></a>
                                    </ItemTemplate>
                                </telerik:RadListView>
                        </div>
                    </div>
                </div>
                <div class="col-md-6">
                    <div class="panel panel-info">
                        <div class="panel-heading panel">
                            <div class="panel-title text-center">
                                日常查询
                            </div>
                        </div>
                        <div class="panel-body">
                            <telerik:RadListView ID="view_query" runat="server" CssClass="panel-body" OnNeedDataSource="view_query_NeedDataSource">
                                    <ItemTemplate>
                                        <a class="btn btn-info dictionaryX" href='<%# Eval("Url") %>'><%# Eval("Name") %></a>
                                    </ItemTemplate>
                                </telerik:RadListView>
                        </div>
                    </div>
                </div>
            </div>
            <div class="row">&nbsp;</div>
            <div class="row">
                <div class="col-md-6" id="scan" runat="server">
                    <div class="panel panel-info">
                        <div class="panel-heading panel">
                            <div class="panel-title text-center">
                                物资条码
                            </div>
                        </div>
                        <div class="panel-body">
                            <telerik:RadListView ID="view_scan" runat="server" CssClass="panel-body" OnNeedDataSource="view_scan_NeedDataSource">
                                    <ItemTemplate>
                                        <a class="btn btn-info dictionaryX" href='<%# Eval("Url") %>'><%# Eval("Name") %></a>
                                    </ItemTemplate>
                                </telerik:RadListView>
                        </div>
                    </div>
                </div>
                <div class="col-md-6">
                    <div class="panel panel-info">
                        <div class="panel-heading panel">
                            <div class="panel-title text-center">
                                系统设置
                            </div>
                        </div>
                        <div class="panel-body">
                            <telerik:RadListView ID="view_setting" runat="server" CssClass="panel-body" OnNeedDataSource="view_setting_NeedDataSource">
                                    <ItemTemplate>
                                        <a class="btn btn-info dictionaryX" href='<%# Eval("Url") %>'><%# Eval("Name") %></a>
                                    </ItemTemplate>
                                </telerik:RadListView>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
