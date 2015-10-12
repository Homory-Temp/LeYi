<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TargetIn.aspx.cs" Inherits="TargetIn" %>

<%@ Register Src="~/Menu/Menu.ascx" TagPrefix="homory" TagName="Menu" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <meta http-equiv="x-ua-compatible" content="ie=edge, chrome=1" />
    <meta name="viewport" content="width=device-width, initial-scale=1, minimum-scale=1, maximum-scale=1" />
    <title>物资管理 - 入库记录</title>
    <link href="../Assets/stylesheets/amazeui.min.css" rel="stylesheet">
    <link href="../Assets/stylesheets/admin.css" rel="stylesheet">
    <link href="../Assets/stylesheets/bootstrap.min.css" rel="stylesheet">
    <link href="../Assets/stylesheets/bootstrap-theme.min.css" rel="stylesheet">
    <!--[if lt IE 9]>
        <script src="../Assets/javascripts/html5.js"></script>
    <![endif]-->
    <!--[if (gt IE 8) | (IEMobile)]><!-->
    <link rel="stylesheet" href="../Assets/stylesheets/unsemantic-grid-responsive.css" />
    <!--<![endif]-->
    <!--[if (lt IE 9) & (!IEMobile)]>
        <link rel="stylesheet" href="../Assets/stylesheets/ie.css" />
    <![endif]-->
    <link href="../Assets/stylesheets/common.css" rel="stylesheet" />
    <script>
        function printpreview() {
            bdhtml = window.document.body.innerHTML;
            sprnstr = "<!-- Start Printing -->";
            eprnstr = "<!-- End Printing -->";
            prnhtml = bdhtml.substring(bdhtml.indexOf(sprnstr) + 23);

            prnhtml = prnhtml.substring(0, prnhtml.indexOf(eprnstr));

            prnhtml = "<body>" + prnhtml + "</body>";
            window.document.body.innerHTML = prnhtml;
            window.print();
            window.document.body.innerHTML = bdhtml;
        }
    </script>
</head>
<body>
    <form id="form" runat="server">
        <telerik:RadScriptManager ID="sm" runat="server"></telerik:RadScriptManager>
        <telerik:RadAjaxPanel ID="ap" runat="server" CssClass="grid-container">
            <!-- Start Printing -->


            <div style="text-align: center; height: 30px; width: 1000px;">
                <h3>入库单</h3>
            </div>
            <div class="grid-90 left mobile-grid-100 grid-parent"  style="padding-left:20px;"> 


                <table style="margin-top: 10px; width: 100%;" align="center">

                    <tbody>
                        <tr>
                            <td align="left">园区： 
                                <asp:Label ID="c" runat="server"></asp:Label>
                            </td>
                            <td align="left">用餐对象： 
                                <asp:Label ID="target_target" runat="server"></asp:Label>
                            </td>
                            <td>日期：   
                                <asp:Label ID="target_day" runat="server"></asp:Label>
                            </td>
                        </tr>
                    </tbody>
                </table>
            </div>

            <div class="grid-90 mobile-grid-100 grid-parent" style="padding-left:20px;">
                <table class="table table-bordered">
                    <tr>
                        <td>品名</td>
                        <td>类别</td>
                        <td>单位</td>
                        <td>数量</td>
                        <td>单价</td>
                        <td>优惠</td>
                        <td>总价</td>
                        <td width="100">供应商</td>
                        <%-- <td>退（换）货</td>--%>
                        <td>规格</td>
                        <td>备注</td>
                    </tr>
                    <telerik:RadListView ID="view" runat="server" OnNeedDataSource="view_NeedDataSource">
                        <ItemTemplate>
                            <tr>
                                <td><%#Eval("物品名称") %></td>
                                <td>
                                    <%# db.Value.StorageObjectGetOne((Container.DataItem as Models.查询_入库单).物品标识).GeneratePath() %>
                                </td>
                                <td>
                                    <%# db.Value.StorageObjectGetOne((Container.DataItem as Models.查询_入库单).物品标识).Unit %>
                                </td>
                                <td><%#Eval("数量") %></td>
                                <td><%#Eval("单价").Money() %></td>
                                <td><%#Eval("优惠价").Money() %></td>
                                <td><%#Eval("合计") .Money()%></td>
                                <td>
                                    <%# db.Value.StorageTargetGetOne((Container.DataItem as Models.查询_入库单).购置标识).OrderSource %>
                                </td>
                                <%--<td><%#Eval("合计") %></td>--%>
                                <td>
                                    <%# db.Value.StorageObjectGetOne((Container.DataItem as Models.查询_入库单).物品标识).Specification %>
                                </td>
                                <td><%#Eval("备注") %></td>
                            </tr>
                        </ItemTemplate>
                    </telerik:RadListView>
                    <tr>
                        <td align="left" colspan="6">合计：<asp:Label ID="t" runat="server"></asp:Label>
                        </td>
                        <td align="left" colspan="2">保管人：   
                            <asp:Label ID="target_keeper" runat="server"></asp:Label>
                        </td>
                        <td colspan="2">经手人：
                            <asp:Label ID="target_brokerage" runat="server"></asp:Label>
                        </td>

                    </tr>
                </table>








            </div>
            <!-- End Printing -->

        </telerik:RadAjaxPanel>
        <asp:ImageButton AlternateText="打印" runat="server" OnClientClick="printpreview(); return false;" />
    </form>
    <script src="../Assets/javascripts/jquery.js"></script>
    <script src="../Assets/javascripts/common.js"></script>
</body>
</html>
