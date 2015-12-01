﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CheckDo.aspx.cs" Inherits="DepotScan_CheckDo" %>

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
        <homory:SideBarSingle runat="server" ID="SideBarSingle" Crumb="扫描盘库" />
        <telerik:RadAjaxPanel ID="ap" runat="server" CssClass="container-fluid" LoadingPanelID="loading">
            <div class="row">
                <div class="col-md-12 text-center">
                    <telerik:RadTextBox runat="server" ID="scan" Width="200" EmptyMessage="请扫描二维码"></telerik:RadTextBox>
                </div>
                <div class="col-md-12 text-center" style="margin-top: 10px;">
                    <input type="button" class="btn btn-lg btn-tumblr" id="scanFlow" runat="server" value="盘点" onserverclick="scanFlow_ServerClick" />
                </div>
            </div>
            <div class="row">
                <div class="col-md-12 text-center">
                    <div id="namex" runat="server" class="btn btn-info" style="width: 100%;">
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-md-12 text-center">
                    <div id="name" runat="server" class="btn btn-info" style="width: 100%;">
                    </div>
                </div>
            </div>
            <div class="row">
                <input type="hidden" runat="server" id="h" value="" />
                <telerik:RadListView ID="view" runat="server" OnNeedDataSource="view_NeedDataSource" ItemPlaceholderID="holder">
                    <LayoutTemplate>
                        <div class="col-md-12">
                            <table class="storeTablePrint text-center">
                                <tr>
                                    <th>物资名称</th>
                                    <th>物资条码</th>
                                    <th>存放地</th>
                                    <th>状态</th>
                                </tr>
                                <asp:PlaceHolder ID="holder" runat="server"></asp:PlaceHolder>
                            </table>
                        </div>
                    </LayoutTemplate>
                    <ItemTemplate>
                        <tr>
                            <td><%# Eval("Name") %>-<%# Eval("Ordinal") %></td>
                            <td><%# Eval("Code") %></td>
                            <td><%# Eval("Place") %></td>
                            <td style='<%# Codes.Count(o => o.In == true && o.Code == (string)Eval("Code")) > 0 ? "color: green;" : "color: red;" %>'><%# Codes.Count(o => o.In == true && o.Code == (string)Eval("Code")) > 0 ? "已盘" : "未盘" %></td>
                        </tr>
                    </ItemTemplate>
                    <EmptyDataTemplate>
                    </EmptyDataTemplate>
                </telerik:RadListView>
            </div>
        </telerik:RadAjaxPanel>
    </form>
</body>
</html>
