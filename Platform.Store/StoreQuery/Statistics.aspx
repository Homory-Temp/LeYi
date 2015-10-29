<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Statistics.aspx.cs" Inherits="StoreQuery_Statistics" %>

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
        <homory:SideBarSingle runat="server" ID="SideBarSingle" Crumb="日常查询 - 汇总统计" />
        <telerik:RadAjaxPanel ID="ap" runat="server" CssClass="container-fluid" LoadingPanelID="loading">
            <div class="row">
                <div class="col-md-2" style="border-right: 1px solid #2B2B2B;">
                    <div class="row">
                        <div class="col-md-12">
                            <span class="btn btn-tumblr">物资类别：</span>
                            &nbsp;&nbsp;
                            <input type="button" class="btn btn-info" id="all" runat="server" value="清除选定" onserverclick="all_ServerClick" />
                            <input type="hidden" id="_all" runat="server" value="1" />
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12">
                            <telerik:RadTreeView ID="tree" runat="server" DataTextField="Name" DataValueField="Id" DataFieldID="Id" DataFieldParentID="ParentId" CheckBoxes="true" CheckChildNodes="true" OnNodeCheck="tree_NodeCheck">
                            </telerik:RadTreeView>
                        </div>
                    </div>
                </div>
                <div class="col-md-10" style="text-align: left;">
                    <div class="row">
                        <div class="col-md-12">
                            <span class="btn btn-info">期初：</span>
                            <telerik:RadMonthYearPicker ID="ps" runat="server" LocalizationPath="~/Language" ShowPopupOnFocus="true" Width="100" AutoPostBack="false">
                                <DatePopupButton runat="server" Visible="false" />
                            </telerik:RadMonthYearPicker>
                            &nbsp;&nbsp;
                            <span class="btn btn-info">期末：</span>
                            <telerik:RadMonthYearPicker ID="pe" runat="server" LocalizationPath="~/Language" ShowPopupOnFocus="true" Width="100" AutoPostBack="false">
                                <DatePopupButton runat="server" Visible="false" />
                            </telerik:RadMonthYearPicker>
                            &nbsp;&nbsp;&nbsp;&nbsp;
                            <telerik:RadTextBox ID="name" runat="server" Width="120" EmptyMessage="物资名称"></telerik:RadTextBox>
                            &nbsp;&nbsp;&nbsp;&nbsp;
                            <input type="button" class="btn btn-tumblr dictionary" id="query" runat="server" value="查询" onserverclick="query_ServerClick" />
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12">
                            <input type="hidden" id="___total" runat="server" />
                            <!-- Start Printing -->
                            <telerik:RadListView ID="view" runat="server" OnNeedDataSource="view_NeedDataSource" ItemPlaceholderID="holder" AllowPaging="true">
                                <LayoutTemplate>
                                    <div class="col-md-12">
                                        <table class="storeTable text-center">
                                            <tr>
                                                <th>物资类别</th>
                                                <th>物资名称</th>
                                                <th>期初数量</th>
                                                <th>期初金额</th>
                                                <th>入库数量</th>
                                                <th>入库金额</th>
                                                <th>出库数量</th>
                                                <th>出库金额</th>
                                                <th>退换数量</th>
                                                <th>退换金额</th>
                                                <th>期末数量</th>
                                                <th>期末金额</th>
                                            </tr>
                                            <asp:PlaceHolder ID="holder" runat="server"></asp:PlaceHolder>
                                            <tr>
                                                <td colspan="2">总计：</td>
                                                <td><%# ___total.Value.Split(new[] { '@' })[0] %></td>
                                                <td><%# ___total.Value.Split(new[] { '@' })[1] %></td>
                                                <td><%# ___total.Value.Split(new[] { '@' })[2] %></td>
                                                <td><%# ___total.Value.Split(new[] { '@' })[3] %></td>
                                                <td><%# ___total.Value.Split(new[] { '@' })[4] %></td>
                                                <td><%# ___total.Value.Split(new[] { '@' })[5] %></td>
                                                <td><%# ___total.Value.Split(new[] { '@' })[6] %></td>
                                                <td><%# ___total.Value.Split(new[] { '@' })[7] %></td>
                                                <td><%# ___total.Value.Split(new[] { '@' })[8] %></td>
                                                <td><%# ___total.Value.Split(new[] { '@' })[9] %></td>
                                            </tr>
                                        </table>
                                    </div>
                                </LayoutTemplate>
                                <ItemTemplate>
                                    <tr>
                                        <td><%# Eval("CatalogPath") %></td>
                                        <td><%# Eval("Name") %></td>
                                        <td><%# Eval("S").ToMoney() %></td>
                                        <td><%# Eval("SM").ToMoney() %></td>
                                        <td><%# Eval("I").ToMoney() %></td>
                                        <td><%# Eval("IM").ToMoney() %></td>
                                        <td><%# Eval("U").ToMoney() %></td>
                                        <td><%# Eval("UM").ToMoney() %></td>
                                        <td><%# Eval("R").ToMoney() %></td>
                                        <td><%# Eval("RM").ToMoney() %></td>
                                        <td><%# Eval("E").ToMoney() %></td>
                                        <td><%# Eval("EM").ToMoney() %></td>
                                    </tr>
                                </ItemTemplate>
                                <EmptyDataTemplate>
                                    <div class="col-md-12 text-center">
                                        <div class="btn btn-warning">暂无记录</div>
                                    </div>
                                </EmptyDataTemplate>
                            </telerik:RadListView>
                            <!-- End Printing -->
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12 text-center">
                            <input type="button" class="btn btn-tumblr" id="print" value="打印" onclick="printDepot();" />
                        </div>
                    </div>
                </div>
            </div>
        </telerik:RadAjaxPanel>
    </form>
</body>
</html>
