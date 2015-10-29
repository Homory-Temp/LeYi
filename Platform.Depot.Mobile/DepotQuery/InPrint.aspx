<%@ Page Language="C#" AutoEventWireup="true" CodeFile="InPrint.aspx.cs" Inherits="DepotQuery_InPrint" %>

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
        function printTarget() {
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
    <!--[if lt IE 9]>
	    <script src="../Content/Homory/js/html5shiv.js"></script>
	    <script src="../Content/Homory/js/respond.min.js"></script>
    <![endif]-->
</head>
<body>
    <form id="form" runat="server">
        <telerik:RadScriptManager ID="sm" runat="server"></telerik:RadScriptManager>
        <telerik:RadAjaxLoadingPanel ID="loading" runat="server" InitialDelayTime="1000">
            <div>&nbsp;</div>
            <div class="btn btn-lg btn-warning" style="margin-top: 50px;">正在加载 请稍候....</div>
        </telerik:RadAjaxLoadingPanel>
        <telerik:RadAjaxPanel ID="ap" runat="server" CssClass="container-fluid" LoadingPanelID="loading">
            <!-- Start Printing -->
            <div class="row" id="x4" runat="server" style="color: black;">
                <div class="col-md-12 text-center" style="font-size: 18px; font-weight: bold;">
                    <span>入库单</span>
                </div>
                <div class="col-md-4 text-left">
                    &nbsp;&nbsp;&nbsp;&nbsp;
                    <span style="font-weight: bold;">园区：</span><span id="campus" runat="server"></span>
                </div>
                <div class="col-md-4 text-center">
                    &nbsp;
                </div>
                <div class="col-md-4 text-right">
                    <span style="font-weight: bold;">日期：</span><span id="time" runat="server"></span>
                    &nbsp;&nbsp;&nbsp;&nbsp;
                </div>
                <div class="col-md-12">
                    <telerik:RadListView ID="view_record" runat="server" OnNeedDataSource="view_record_NeedDataSource" ItemPlaceholderID="recordHolder">
                        <LayoutTemplate>
                            <table class="storeTablePrint text-center">
                                <tr>
                                    <th>品名</th>
                                    <th>类别</th>
                                    <th>单位</th>
                                    <th>数量</th>
                                    <th>单价</th>
                                    <th>总价</th>
                                    <th>供应商</th>
                                    <th>规格</th>
                                    <th>备注</th>
                                </tr>
                                <asp:PlaceHolder ID="recordHolder" runat="server"></asp:PlaceHolder>
                                <tr>
                                    <td colspan="5">
                                        <telerik:RadCodeBlock runat="server">
                                            <span style="font-weight: bold;">合计：</span><span><%= total.Value %></span>
                                        </telerik:RadCodeBlock>
                                    </td>
                                    <td colspan="2" style="text-align: left;">
                                        <telerik:RadCodeBlock runat="server">
                                            &nbsp;&nbsp;&nbsp;&nbsp;
                                            <span style="font-weight: bold;">保管人：</span><span><%= keep.Value %></span>
                                        </telerik:RadCodeBlock>
                                    </td>
                                    <td colspan="2" style="text-align: left;">
                                        <telerik:RadCodeBlock runat="server">
                                            &nbsp;&nbsp;&nbsp;&nbsp;
                                            <span style="font-weight: bold;">经手人：</span><span><%= brokerage.Value %></span>
                                        </telerik:RadCodeBlock>
                                    </td>
                                </tr>
                            </table>
                        </LayoutTemplate>
                        <ItemTemplate>
                            <tr>
                                <td><%# Eval("Name") %></td>
                                <td><%# Eval("CatalogName") %></td>
                                <td><%# Eval("Unit") %></td>
                                <td><%# Eval("Amount").ToAmount(Depot.Featured(Models.DepotType.小数数量库)) %></td>
                                <td><%# Eval("PriceSet").ToMoney() %></td>
                                <td><%# Eval("Total").ToMoney() %></td>
                                <td><%# Eval("OrderSource") %></td>
                                <td><%# Eval("Specification") %></td>
                                <td><%# Eval("Note") %></td>
                            </tr>
                        </ItemTemplate>
                    </telerik:RadListView>
                    <input id="order" runat="server" type="hidden" />
                    <input id="total" runat="server" type="hidden" />
                    <input id="keep" runat="server" type="hidden" />
                    <input id="brokerage" runat="server" type="hidden" />
                </div>
            </div>
            <!-- End Printing -->
            <div class="row">&nbsp;</div>
            <div class="row">
                <div class="col-md-12 text-center">
                    <input type="button" class="btn btn-tumblr" value="打印" id="in" onclick="return printTarget();" />
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <input type="button" class="btn btn-tumblr" value="返回" id="go" runat="server" onserverclick="go_ServerClick" />
                </div>
            </div>
        </telerik:RadAjaxPanel>
    </form>
</body>
</html>
