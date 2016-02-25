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
                <div class="col-md-12 text-center">
                    <table class="storeTablePrint text-center">
                        <tr>
                            <th colspan="12" style="font-size: 18px; font-weight: bold; padding: 10px;"><span>入库单</span></th>
                        </tr>
                        <tr>
                            <td><span style="font-weight: bold;">园区：</span><span id="campus" runat="server"></span></td>
                            <td colspan="2"><span style="font-weight: bold;">购置单号：</span><span id="orderNo" runat="server"></span></td>
                            <td colspan="3"><span style="font-weight: bold;">发票编号：</span><span id="re" runat="server"></span></td>
                            <td colspan="2"><span style="font-weight: bold;">入库日期：</span><span id="time" runat="server"></span></td>
                            <td colspan="2"><span style="font-weight: bold;">购置来源：</span><span id="os" runat="server"></span></td>
                            <td colspan="2"><span style="font-weight: bold;">使用对象：</span><span id="ot" runat="server"></span></td>
                        </tr>
                        <telerik:RadListView ID="view_record" runat="server" OnNeedDataSource="view_record_NeedDataSource" ItemPlaceholderID="recordHolder">
                            <LayoutTemplate>
                                <tr>
                                    <th>品名</th>
                                    <th>类别</th>
                                    <th>单位</th>
                                    <th>数量</th>
                                    <th>单价(元)</th>
                                    <th>总价(元)</th>
                                    <th>品牌</th>
                                    <th>规格型号</th>
                                    <th>存放地点</th>
                                    <th>适用年龄段</th>
                                    <th>入库人</th>
                                    <th>备注</th>
                                </tr>
                                <asp:PlaceHolder ID="recordHolder" runat="server"></asp:PlaceHolder>
                                <tr>
                                    <td colspan="4" style="text-align: left;">
                                        <telerik:RadCodeBlock runat="server">
                                            &nbsp;&nbsp;&nbsp;&nbsp;
                                            <span style="font-weight: bold;">物资合计：</span>共&nbsp;<span><%= total.Value.Split(new[] { "@@@" }, StringSplitOptions.None)[2] %></span>&nbsp;种&nbsp;<span><%= total.Value.Split(new[] { "@@@" }, StringSplitOptions.None)[0] %></span>&nbsp;件
                                        </telerik:RadCodeBlock>
                                    </td>
                                    <td colspan="4" style="text-align: left;">
                                        <telerik:RadCodeBlock runat="server">
                                            &nbsp;&nbsp;&nbsp;&nbsp;
                                            <span style="font-weight: bold;">金额合计：</span>￥<span><%= total.Value.Split(new[] { "@@@" }, StringSplitOptions.None)[1] %></span>
                                        </telerik:RadCodeBlock>
                                    </td>
                                    <td colspan="2" style="text-align: left;">
                                        <telerik:RadCodeBlock runat="server">
                                            &nbsp;&nbsp;&nbsp;&nbsp;
                                            <span style="font-weight: bold;">经手人：</span><span><%= brokerage.Value %></span>
                                        </telerik:RadCodeBlock>
                                    </td>
                                    <td colspan="2" style="text-align: left;">
                                        <telerik:RadCodeBlock runat="server">
                                            &nbsp;&nbsp;&nbsp;&nbsp;
                                            <span style="font-weight: bold;">保管人：</span><span><%= keep.Value %></span>
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
                                    <td><%# Eval("Brand") %></td>
                                    <td><%# Eval("Specification") %></td>
                                    <td><%# Eval("Place") %></td>
                                    <td><%# Eval("Age") %></td>
                                    <td><%# OpName((Guid)Eval("OperatorId")) %></td>
                                    <td><%# Eval("Note") %></td>
                                </tr>
                            </ItemTemplate>
                        </telerik:RadListView>
                        <tr>
                            <td>
                                <input id="order" runat="server" type="hidden" /></td>
                            <td>
                                <input id="total" runat="server" type="hidden" /></td>
                            <td>
                                <input id="keep" runat="server" type="hidden" /></td>
                            <td>
                                <input id="brokerage" runat="server" type="hidden" /></td>
                        </tr>
                    </table>
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
    <style>
        html body {
            padding-top: 0px;
        }
    </style>
</body>
</html>
