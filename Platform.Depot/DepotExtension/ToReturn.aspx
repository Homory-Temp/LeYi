﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ToReturn.aspx.cs" Inherits="DepotExtension_ToReturn" %>

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
        <telerik:RadScriptManager ID="sm" runat="server"></telerik:RadScriptManager>
        <telerik:RadAjaxPanel ID="ap" runat="server" CssClass="container-fluid" LoadingPanelID="loading">
            <div class="row" id="x1" runat="server">
                <div class="col-md-2">
                    <div class="btn btn-tumblr dictionaryX">
                        借用记录
                    </div>
                </div>
            </div>
            <div class="row" id="x2" runat="server">
                <div class="col-md-12">
                    <telerik:RadListView ID="view_obj" runat="server" OnNeedDataSource="view_obj_NeedDataSource" ItemPlaceholderID="useHolder">
                        <LayoutTemplate>
                            <table class="storeTable text-center">
                                <tr>
                                    <th>借用日期</th>
                                    <th>物资名称</th>
                                    <th>借用数量</th>
                                    <th>待归还数</th>
                                    <th>单价</th>
                                    <th>合计</th>
                                    <th style="display: none;">归还数</th>
                                    <th style="display: none;">报废数</th>
                                    <th style="display: none;">备注</th>
                                </tr>
                                <asp:PlaceHolder ID="useHolder" runat="server"></asp:PlaceHolder>
                            </table>
                        </LayoutTemplate>
                        <ItemTemplate>
                            <tr>
                                <td style="display: none;">
                                    <asp:Label runat="server" ID="id" Text='<%# Eval("Id") %>'></asp:Label>
                                </td>
                                <td>
                                    <%# Eval("Time").ToDay() %>
                                </td>
                                <td>
                                    <%# Eval("Name") %>
                                </td>
                                <td>
                                    <%# Eval("Amount").ToAmount() %>
                                </td>
                                <td>
                                    <%# ((decimal)Eval("Amount") - (decimal)Eval("ReturnedAmount")).ToAmount() %>
                                </td>
                                <td>
                                    <%# Eval("PriceSet").ToMoney() %>
                                </td>
                                <td>
                                    <%# Eval("Money").ToMoney() %>
                                </td>
                                <td style="display: none;">
                                    <telerik:RadNumericTextBox ID="amount" runat="server" Width="120" MaxValue='<%# (double)((decimal)Eval("Amount") - (decimal)Eval("ReturnedAmount")) %>' NumberFormat-DecimalDigits="0" DataType="System.Decimal" AllowOutOfRangeAutoCorrect="true"></telerik:RadNumericTextBox>
                                </td>
                                <td style="display: none;">
                                    <telerik:RadNumericTextBox ID="outAmount" runat="server" Width="120" NumberFormat-DecimalDigits="0" DataType="System.Decimal" AllowOutOfRangeAutoCorrect="true"></telerik:RadNumericTextBox>
                                </td>
                                <td style="display: none;">
                                    <telerik:RadTextBox ID="note" runat="server" Width="100"></telerik:RadTextBox>
                                </td>
                            </tr>
                        </ItemTemplate>
                        <EmptyDataTemplate>
                                <div class="col-md-12 text-center">
                                    <div class="btn btn-warning">无借用记录</div>
                                </div>
                        </EmptyDataTemplate>
                    </telerik:RadListView>
                </div>
                <div class="col-md-4">
                    <input type="hidden" id="counter" runat="server" value="0" />
                    <input type="hidden" id="x" runat="server" value="" />
                </div>
                <div class="col-md-4">&nbsp;</div>
                <div class="col-md-4 text-left">
                    &nbsp;
                </div>
                <div class="col-md-12 text-center">
                    &nbsp;
                </div>
            </div>
        </telerik:RadAjaxPanel>
    </form>
</body>
</html>
