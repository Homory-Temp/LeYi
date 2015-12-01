﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="InOrderX.aspx.cs" Inherits="DepotQuery_InPrint" %>

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
            <telerik:RadListView ID="view" runat="server" OnNeedDataSource="view_NeedDataSource" ItemPlaceholderID="holder" AllowPaging="false">
                <LayoutTemplate>
                    <div class="col-md-12">
                        <table class="storeTablePrint text-center">
                            <tr>
                                <th colspan="12" style="font-size: 18px; font-weight: bold; padding: 10px; border: none;"><span>请购内容</span></th>
                            </tr>
                            <tr>
                                <th>物资名称</th>
                                <th>规格</th>
                                <th>请购数量</th>
                                <th>单价</th>
                                <th>要求</th>
                                <th>用途</th>
                            </tr>
                            <asp:PlaceHolder ID="holder" runat="server"></asp:PlaceHolder>
                        </table>
                    </div>
                </LayoutTemplate>
                <ItemTemplate>
                    <tr>
                        <td><%# Eval("物品名称") %></td>
                        <td><%# Eval("规格") %></td>
                        <td><%# Eval("请购数量").ToAmount() %></td>
                        <td><%# Eval("单价").ToMoney() %></td>
                        <td><%# Eval("要求") %></td>
                        <td><%# Eval("用途") %></td>
                    </tr>
                </ItemTemplate>
                <EmptyDataTemplate>
                    <div class="col-md-12 text-center">
                        <div class="btn btn-warning">暂无记录</div>
                    </div>
                </EmptyDataTemplate>
            </telerik:RadListView>
        </telerik:RadAjaxPanel>
        <div class="container-fluid"><div class="row">&nbsp;</div></div>
        <div class="container-fluid"><div class="row">&nbsp;</div></div>
        <telerik:RadAjaxPanel ID="apx" runat="server" CssClass="container-fluid" LoadingPanelID="loading">
            <telerik:RadListView ID="viewx" runat="server" OnNeedDataSource="viewx_NeedDataSource" ItemPlaceholderID="holder" AllowPaging="false">
                <LayoutTemplate>
                    <div class="col-md-12">
                        <table class="storeTablePrint text-center">
                            <tr>
                                <th colspan="12" style="font-size: 18px; font-weight: bold; padding: 10px; border: none;"><span>请购流程</span></th>
                            </tr>
                            <tr>
                                <th>办理步骤</th>
                                <th>办理人</th>
                                <th>状态</th>
                                <th>意见</th>
                                <th>时间</th>
                            </tr>
                            <asp:PlaceHolder ID="holder" runat="server"></asp:PlaceHolder>
                        </table>
                    </div>
                </LayoutTemplate>
                <ItemTemplate>
                    <tr>
                        <td><%# Eval("步骤") %></td>
                        <td><%# Eval("办理人") %></td>
                        <td><%# Eval("状态") %></td>
                        <td><%# Eval("意见") %></td>
                        <td><%# Eval("时间") %></td>
                    </tr>
                </ItemTemplate>
                <EmptyDataTemplate>
                    <div class="col-md-12 text-center">
                        <div class="btn btn-warning">暂无记录</div>
                    </div>
                </EmptyDataTemplate>
            </telerik:RadListView>
        </telerik:RadAjaxPanel>
    </form>
</body>
</html>
