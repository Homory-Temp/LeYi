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
                    <div class="row">
                        <div class="col-md-12">
                            <div class="panel-danger">
                                <div class="panel-heading">
                                    <div style="text-align: center; width: 100%;">物资管理</div>
                                </div>
                                <telerik:RadListView ID="view_action" runat="server" CssClass="panel-body" OnNeedDataSource="view_action_NeedDataSource">
                                    <ItemTemplate>
                                        <div class="col-md-4 text-center" style="cursor: pointer; margin-top: 50px;">
                                            <div class="row" style="display: none;" onclick="top.location.href = '<%# Eval("Url") %>';">
                                                <div class="col-md-12">
                                                    <img src="../Content/Images/Store.png" />
                                                </div>
                                            </div>
                                            <div class="row" onclick="top.location.href = '<%# Eval("Url") %>';">
                                                <div class="col-md-12">
                                                    <div class='btn btn-danger dictionaryX'><%# Eval("Name") %></div>
                                                </div>
                                            </div>
                                        </div>
                                    </ItemTemplate>
                                </telerik:RadListView>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-md-6">
                    <div class="row">
                        <div class="col-md-12">
                            <div class="panel-info">
                                <div class="panel-heading">
                                    <div style="text-align: center; width: 100%;">日常查询</div>
                                </div>
                                <telerik:RadListView ID="view_query" runat="server" CssClass="panel-body" OnNeedDataSource="view_query_NeedDataSource">
                                    <ItemTemplate>
                                        <div class="col-md-4 text-center" style="cursor: pointer; margin-top: 50px;">
                                            <div class="row" style="display: none;" onclick="top.location.href = '<%# Eval("Url") %>';">
                                                <div class="col-md-12">
                                                    <img src="../Content/Images/Store.png" />
                                                </div>
                                            </div>
                                            <div class="row" onclick="top.location.href = '<%# Eval("Url") %>';">
                                                <div class="col-md-12">
                                                    <div class='btn btn-info dictionaryX'><%# Eval("Name") %></div>
                                                </div>
                                            </div>
                                        </div>
                                    </ItemTemplate>
                                </telerik:RadListView>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="row">&nbsp;</div>
            <div class="row">
                <div class="col-md-6" id="scan" runat="server">
                    <div class="row">
                        <div class="col-md-12">
                            <div class="panel-primary">
                                <div class="panel-heading">
                                    <div style="text-align: center; width: 100%;">物资条码</div>
                                </div>
                                <telerik:RadListView ID="view_scan" runat="server" CssClass="panel-body" OnNeedDataSource="view_scan_NeedDataSource">
                                    <ItemTemplate>
                                        <div class="col-md-4 text-center" style="cursor: pointer; margin-top: 50px;">
                                            <div class="row" style="display: none;" onclick="top.location.href = '<%# Eval("Url") %>';">
                                                <div class="col-md-12">
                                                    <img src="../Content/Images/Store.png" />
                                                </div>
                                            </div>
                                            <div class="row" onclick="top.location.href = '<%# Eval("Url") %>';">
                                                <div class="col-md-12">
                                                    <div class='btn btn-primary dictionaryX'><%# Eval("Name") %></div>
                                                </div>
                                            </div>
                                        </div>
                                    </ItemTemplate>
                                </telerik:RadListView>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-md-6">
                    <div class="row">
                        <div class="col-md-12">
                            <div class="panel-warning">
                                <div class="panel-heading">
                                    <div style="text-align: center; width: 100%;">系统设置</div>
                                </div>
                                <telerik:RadListView ID="view_setting" runat="server" CssClass="panel-body" OnNeedDataSource="view_setting_NeedDataSource">
                                    <ItemTemplate>
                                        <div class="col-md-4 text-center" style="cursor: pointer; margin-top: 50px;">
                                            <div class="row" style="display: none;" onclick="top.location.href = '<%# Eval("Url") %>';">
                                                <div class="col-md-12">
                                                    <img src="../Content/Images/Store.png" />
                                                </div>
                                            </div>
                                            <div class="row" onclick="top.location.href = '<%# Eval("Url") %>';">
                                                <div class="col-md-12">
                                                    <div class='btn btn-warning dictionaryX'><%# Eval("Name") %></div>
                                                </div>
                                            </div>
                                        </div>
                                    </ItemTemplate>
                                </telerik:RadListView>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
