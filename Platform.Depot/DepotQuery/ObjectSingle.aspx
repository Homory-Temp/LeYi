<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ObjectSingle.aspx.cs" Inherits="DepotQuery_ObjectSingle" %>

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
    <script>
        function gox(obj) {
            var id = $(obj).attr("goid");
            var idx = $(obj).attr("did");
            window.open('../DepotQuery/Object?ObjectId=' + id + "&DepotId=" + idx, '_blank');
        }
    </script>
    <!--[if lt IE 9]>
	    <script src="../Content/Homory/js/html5shiv.js"></script>
	    <script src="../Content/Homory/js/respond.min.js"></script>
    <![endif]-->
</head>
<body>
    <form id="form" runat="server">
        <homory:SideBarSingle runat="server" ID="SideBarSingle" Crumb="物资管理 - 资产查询" />
        <telerik:RadAjaxPanel ID="ap" runat="server" CssClass="container-fluid" LoadingPanelID="loading">
            <div class="row">
                <div class="col-md-12" style="text-align: left;">
                    <div class="row">
                        <div class="col-md-12 text-center">
                            物资名称：
                            <telerik:RadTextBox ID="toSearch" runat="server" Width="120"></telerik:RadTextBox>
                            &nbsp;&nbsp;&nbsp;&nbsp;
                            卡片编号：
                            <telerik:RadTextBox ID="no" runat="server" Width="120"></telerik:RadTextBox>
                            <input id="search" runat="server" type="button" class="btn btn-info" value="检索" onserverclick="search_ServerClick" />
                        </div>
                    </div>
                    <div class="row">&nbsp;</div>
                    <div class="row">
                        <div class="col-md-12" style="color: #2B2B2B;">
                            <telerik:RadListView ID="view" runat="server" OnNeedDataSource="view_NeedDataSource" ItemPlaceholderID="holder" AllowPaging="true">
                                <LayoutTemplate>
                                    <table class="storeTablePrint">
                                        <tr>
                                            <th>物资名称</th>
                                            <th>卡片编号</th>
                                            <th>数量</th>
                                            <th>单价</th>
                                            <th>单位</th>
                                            <th>总额</th>
                                            <th>品牌</th>
                                            <th>规格</th>
                                            <th>存放地</th>
                                        </tr>
                                        <asp:PlaceHolder ID="holder" runat="server"></asp:PlaceHolder>
                                    </table>
                                </LayoutTemplate>
                                <ItemTemplate>
                                    <tr>
                                        <td onclick="gox(this);" goid='<%# Eval("ObjectId") %>' did='<%# Depot.Id %>' style="color: rgb(62, 90, 112); cursor: pointer;"><%# Eval("Name") %></td>
                                        <td><%# Eval("Note") %></td>
                                        <td><%# Eval("Amount").ToAmount() %></td>
                                        <td><%# Eval("PriceSet").ToMoney() %></td>
                                        <td><%# Eval("Total").ToMoney() %></td>
                                        <td><%# Eval("Unit") %></td>
                                        <td><%# Eval("Brand") %></td>
                                        <td><%# Eval("Specification") %></td>
                                        <td><%# Eval("Place") %></td>
                                    </tr>
                                </ItemTemplate>
                            </telerik:RadListView>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-3">&nbsp;</div>
                        <div class="col-md-6 text-center">
                            <telerik:RadDataPager ID="pager" runat="server" PagedControlID="view" BackColor="Transparent" BorderStyle="None" RenderMode="Auto" PageSize="20">
                                <Fields>
                                    <telerik:RadDataPagerButtonField FieldType="FirstPrev"></telerik:RadDataPagerButtonField>
                                    <telerik:RadDataPagerButtonField FieldType="Numeric"></telerik:RadDataPagerButtonField>
                                    <telerik:RadDataPagerButtonField FieldType="NextLast"></telerik:RadDataPagerButtonField>
                                </Fields>
                            </telerik:RadDataPager>
                        </div>
                        <div class="col-md-3">&nbsp;</div>
                    </div>
                </div>
            </div>
            <input id="line_no" runat="server" value="16" type="hidden" />
        </telerik:RadAjaxPanel>
    </form>
</body>
</html>
