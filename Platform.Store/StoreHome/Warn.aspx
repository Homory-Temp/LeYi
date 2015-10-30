<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Warn.aspx.cs" Inherits="StoreHome_Warn" %>

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
        <homory:SideBarSingle runat="server" ID="SideBarSingle" Crumb="库存预警" />
        <telerik:RadAjaxPanel ID="ap" runat="server" CssClass="container" LoadingPanelID="loading">
            <div class="row">
                <div class="col-md-12 text-center">
                    <telerik:RadComboBox ID="usage" runat="server" AutoPostBack="true" Width="120" AppendDataBoundItems="true" DataTextField="Name" DataValueField="Id" OnSelectedIndexChanged="usage_SelectedIndexChanged">
                    </telerik:RadComboBox>
                </div>
            </div>
            <div class="row">
                &nbsp;
            </div>
            <div class="row">
                <div class="col-md-12">
                    <div class="row">
                        <div class="col-md-12">
                            <div class="panel-danger">
                                <div class="panel-heading">
                                    <div style="text-align: center; width: 100%;">低量预警</div>
                                </div>
                                <div class="panel-body">
                                    <telerik:RadListView ID="view_action" runat="server" CssClass="row" OnNeedDataSource="view_action_NeedDataSource" ItemPlaceholderID="holder">
                                        <LayoutTemplate>
                                            <div class="col-md-12">
                                                <table class="storeTable text-center">
                                                    <tr>
                                                        <th>物资类别</th>
                                                        <th>物资名称</th>
                                                        <th>单位</th>
                                                        <th>预警值</th>
                                                        <th>库存量</th>
                                                    </tr>
                                                    <asp:PlaceHolder ID="holder" runat="server"></asp:PlaceHolder>
                                                </table>
                                            </div>
                                        </LayoutTemplate>
                                        <ItemTemplate>
                                            <tr>
                                                <td class="col-mxd-3"><%# db.Value.GetCatalogPath((Guid)Eval("CatalogId")).Single() %></td>
                                                <td class="col-mxd-3"><%# Eval("Name") %></td>
                                                <td class="col-mxd-3"><%# Eval("Unit") %></td>
                                                <td class="col-mxd-3"><%# Eval("Low").ToAmount() %></td>
                                                <td class="col-mxd-3"><%# Eval("Amount").ToAmount() %></td>
                                            </tr>
                                        </ItemTemplate>
                                    </telerik:RadListView>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-md-12">
                    <div class="row">
                        <div class="col-md-12">
                            <div class="panel-info">
                                <div class="panel-heading">
                                    <div style="text-align: center; width: 100%;">超量预警</div>
                                </div>
                                <div class="panel-body">
                                    <telerik:RadListView ID="view_query" runat="server" CssClass="row" OnNeedDataSource="view_query_NeedDataSource" ItemPlaceholderID="holder">
                                        <LayoutTemplate>
                                            <div class="col-md-12">
                                                <table class="storeTable text-center">
                                                    <tr>
                                                        <th>物资类别</th>
                                                        <th>物资名称</th>
                                                        <th>单位</th>
                                                        <th>预警值</th>
                                                        <th>库存量</th>
                                                    </tr>
                                                    <asp:PlaceHolder ID="holder" runat="server"></asp:PlaceHolder>
                                                </table>
                                            </div>
                                        </LayoutTemplate>
                                        <ItemTemplate>
                                            <tr>
                                                <td class="col-mxd-3"><%# db.Value.GetCatalogPath((Guid)Eval("CatalogId")).Single() %></td>
                                                <td class="col-mxd-3"><%# Eval("Name") %></td>
                                                <td class="col-mxd-3"><%# Eval("Unit") %></td>
                                                <td class="col-mxd-3"><%# Eval("High").ToAmount() %></td>
                                                <td class="col-mxd-3"><%# Eval("Amount").ToAmount() %></td>
                                            </tr>
                                        </ItemTemplate>
                                    </telerik:RadListView>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="row">&nbsp;</div>
        </telerik:RadAjaxPanel>
    </form>
</body>
</html>
