<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Flow.aspx.cs" Inherits="DepotScan_Flow" %>

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
        <homory:SideBarSingle runat="server" ID="SideBarSingle" Crumb="物资扫描 - 流通查询" />
        <telerik:RadAjaxPanel ID="ap" runat="server" CssClass="container-fluid" LoadingPanelID="loading">
            <div class="row">
                <div class="col-md-12 text-center">
                    <telerik:RadTextBox runat="server" ID="scan" Width="200" EmptyMessage="请扫描二维码"></telerik:RadTextBox>
                    &nbsp;&nbsp;&nbsp;&nbsp;
                    <input type="button" class="btn btn-tumblr" id="scanFlow" runat="server" value="查询" onserverclick="scanFlow_ServerClick" />
                </div>
            </div>
            <div class="row">&nbsp;</div>
            <div class="row">
                <div class="col-md-12">
                    <telerik:RadListView ID="view" runat="server" ItemPlaceholderID="holder" AllowPaging="true">
                        <LayoutTemplate>
                            <div class="col-md-12">
                                <table class="storeTablePrint text-center">
                                    <tr>
                                        <th>流通类型</th>
                                        <th>数量</th>
                                        <th>金额</th>
                                    </tr>
                                    <asp:PlaceHolder ID="holder" runat="server"></asp:PlaceHolder>
                                </table>
                            </div>
                        </LayoutTemplate>
                        <ItemTemplate>
                            <tr>
                                <td><%# Eval("Type") %></td>
                                <td><%# Eval("Amount").ToAmount(Depot.Featured(Models.DepotType.小数数量库)) %></td>
                                <td><%# Eval("Money").ToMoney() %></td>
                            </tr>
                        </ItemTemplate>
                        <EmptyDataTemplate>
                            <div class="col-md-12 text-center">
                                <div class="btn btn-warning">暂无记录</div>
                            </div>
                        </EmptyDataTemplate>
                    </telerik:RadListView>
                </div>
            </div>
        </telerik:RadAjaxPanel>
    </form>
</body>
</html>
