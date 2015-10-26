<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Redo.aspx.cs" Inherits="DepotQuery_Redo" %>

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
        <homory:SideBarSingle runat="server" ID="SideBarSingle" Crumb="日常查询 - 退货查询" />
        <telerik:RadAjaxPanel ID="ap" runat="server" CssClass="container-fluid" LoadingPanelID="loading">
            <div class="row">
                <telerik:RadListView ID="view" runat="server" OnNeedDataSource="view_NeedDataSource" ItemPlaceholderID="holder" AllowPaging="true">
                    <LayoutTemplate>
                        <div class="col-md-12">
                            <table class="storeTable text-center">
                                <tr>
                                    <th>购置单</th>
                                    <th>物资名称</th>
                                    <th>退货数量</th>
                                </tr>
                                <asp:PlaceHolder ID="holder" runat="server"></asp:PlaceHolder>
                            </table>
                        </div>
                    </LayoutTemplate>
                    <ItemTemplate>
                        <tr>
                            <td>
                                <asp:HyperLink runat="server" ForeColor="#3E5A70" Target="_blank" Text="购置单" NavigateUrl='<%# "../DepotQuery/InPrint?DepotId={0}&OrderId={1}".Formatted(Depot.Id, Eval("OrderId")) %>'></asp:HyperLink></td>
                            <td><%# Eval("Name") %></td>
                            <td><%# Eval("Amount").ToAmount(Depot.Featured(Models.DepotType.小数数量库)) %></td>
                        </tr>
                    </ItemTemplate>
                    <EmptyDataTemplate>
                        <div class="col-md-12 text-center">
                            <div class="btn btn-warning">暂无记录</div>
                        </div>
                    </EmptyDataTemplate>
                </telerik:RadListView>
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
