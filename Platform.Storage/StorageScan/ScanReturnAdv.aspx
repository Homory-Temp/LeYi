<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ScanReturnAdv.aspx.cs" Inherits="ScanReturnAdv" %>

<%@ Register Src="~/Menu/Menu.ascx" TagPrefix="homory" TagName="Menu" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <meta http-equiv="x-ua-compatible" content="ie=edge, chrome=1" />
    <meta name="viewport" content="width=device-width, initial-scale=1, minimum-scale=1, maximum-scale=1" />
    <title>物资管理 - 物资归还</title>
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
</head>
<body>
    <form id="form" runat="server">
        <telerik:RadScriptManager ID="sm" runat="server"></telerik:RadScriptManager>
        <telerik:RadCodeBlock ID="cb" runat="server">
            <script>
                function scanDoX() {
                    $find("<%= ap.ClientID %>").ajaxRequest("Do");
                }
                function amount_changed(sender, args) {
                    var v = sender.get_value();
                    var id = $("#" + sender.get_id()).attr("ob");
                    $find(id).set_maxValue(v);
                    __doPostBack(id, "");
                }
            </script>
        </telerik:RadCodeBlock>
        <homory:Menu runat="server" ID="menu" />
        <telerik:RadAjaxPanel ID="ap" runat="server" CssClass="grid-container">
            <div class="grid-100 mobile-grid-100 grid-parent">
                <div>
                   
                </div>
                <div>
                    借用人：<asp:Label ID="responsibleX" runat="server"></asp:Label>
                    <input id="responsibleIdX" runat="server" type="hidden" />
                    <telerik:RadSearchBox ID="keeper_sourceX" runat="server" EmptyMessage="借用人筛选" MaxResultCount="10" LocalizationPath="~/Language" DataTextField="Name" DataValueField="Id" ShowSearchButton="false" OnDataSourceSelect="keeper_sourceX_DataSourceSelect" ShowLoadingIcon="false" OnSearch="keeper_sourceX_Search"></telerik:RadSearchBox>
                     <asp:ImageButton ID="han" runat="server" AlternateText="快捷模式" OnClick="han_Click"   class="btn btn-xs btn-default"/>
                </div>
            </div>
            <div class="grid-100 mobile-grid-100 grid-parent">
                <telerik:RadListView ID="viewW" runat="server" OnNeedDataSource="viewW_NeedDataSource" DataKeyNames="借用标识" OnItemDataBound="viewW_ItemDataBound" ItemPlaceholderID="holderrr">



                    <ItemTemplate>
                        <table class="table table-bordered" style="margin-left: 10px; margin-top: 10px;" align="center">
                            <thead>
                                <tr>
                                    <th>借用日期</th>
                                    <th>物资名称</th>
                                    <th>借用数量</th>
                                    <th>待归还数量</th>
                                    <th>单价</th>
                                    <th>合计</th>
                                    <th>借用人</th>
                                    <th>编号</th>
                                    <th>归还数量</th>
                                    <th>报废数量</th>
                                    <th>备注</th>
                                </tr>
                            </thead>
                            <tbody>

                                <tr>
                                    <td align="left"><%# Eval("日期") %></td>
                                    <td align="left">
                                        <%# Eval("物品名称") %></td>
                                    <td align="left">
                                        <%# Eval("数量") %></td>
                                    <td align="left">
                                        <%# Eval("待归还数") %></td>
                                    <td align="left">
                                        <%# Eval("单价").Money() %></td>
                                    <td align="left">
                                        <%# Eval("合计").Money() %></td>
                                    <td align="left">
                                        <%# Eval("借用人") %></td>
                                    <td align="left">
                                        <telerik:RadNumericTextBox ID="amount" runat="server" ClientEvents-OnValueChanged="amount_changed" Value="0" NumberFormat-DecimalDigits="2" MaxValue='<%# (decimal)Eval("待归还数") %>' AllowOutOfRangeAutoCorrect="true"></telerik:RadNumericTextBox></td>
                                    <td align="left">
                                        <telerik:RadNumericTextBox ID="amountX" runat="server" Value="0" NumberFormat-DecimalDigits="2" MaxValue="0" AllowOutOfRangeAutoCorrect="true"></telerik:RadNumericTextBox></td>
                                    <td align="left">
                                        <telerik:RadTextBox ID="note" runat="server"></telerik:RadTextBox></td>
                                </tr>

                            </tbody>
                        </table>
                    </ItemTemplate>
                </telerik:RadListView>
                <telerik:RadListView ID="viewD" runat="server" OnNeedDataSource="viewD_NeedDataSource" DataKeyNames="单借标识" OnItemDataBound="viewD_ItemDataBound" ItemPlaceholderID="holder">

                    <ItemTemplate>
                        <table class="table table-bordered" style="margin-left: 10px; margin-top: 10px;" align="center">
                            <thead>
                                <tr>
                                    <th>借用日期</th>
                                    <th>物资名称</th>
                                    <th>借用数量</th>
                                    <th>待归还数量</th>
                                    <th>单价</th>
                                    <th>合计</th>
                                    <th>借用人</th>
                                    <th>编号</th>
                                    <th>归还数量</th>
                                    <th>报废数量</th>
                                    <th>备注</th>
                                </tr>
                            </thead>
                            <tbody>

                                <tr>
                                    <td align="left">
                                        <%# Eval("时间节点") %></td>
                                    <td align="left">
                                        <%# db.Value.StorageObjectGetOne((Guid)Eval("物品标识")).Name %></td>
                                    <td align="left">1</td>
                                    <td align="left">1</td>
                                    <td align="left">
                                        <%# decimal.Divide((decimal)Eval("TotalMoney"), (decimal)Eval("TotalAmount")).Money() %></td>
                                    <td align="left">
                                        <%# decimal.Divide((decimal)Eval("TotalMoney"), (decimal)Eval("TotalAmount")).Money() %></td>
                                    <td align="left">
                                        <%# db.Value.UserName((Guid)Eval("人员标识")) %></td>
                                    <td align="left">
                                        <%# Eval("编号") %>
                                    </td>
                                    <td align="left">
                                        <telerik:RadNumericTextBox ID="amount" runat="server" Value="0" ClientEvents-OnValueChanged="amount_changed" NumberFormat-DecimalDigits="2" MaxValue="1" AllowOutOfRangeAutoCorrect="true"></telerik:RadNumericTextBox></td>
                                    <td align="left">
                                        <telerik:RadNumericTextBox ID="amountX" runat="server" Value="0" NumberFormat-DecimalDigits="2" MaxValue="0" AllowOutOfRangeAutoCorrect="true"></telerik:RadNumericTextBox></td>
                                    <td align="left">
                                        <telerik:RadTextBox ID="note" runat="server"></telerik:RadTextBox>
                                    </td>
                                </tr>
                        </table>
                    </ItemTemplate>
                </telerik:RadListView>
            </div>
            <div class="grid-100 mobile-grid-100 grid-parent">
                <div>
                    <telerik:RadButton ID="in_confirm" runat="server" Checked="true" Visible="false" Text="确认归还信息填写正确" AutoPostBack="true" ButtonType="ToggleButton" ToggleType="CheckBox">
                    </telerik:RadButton>
                    <div style="margin-top: 10px;">
                        <asp:ImageButton AlternateText="归还" ID="out" runat="server" OnClick="out_Click" class="btn btn-xs btn-default" />
                    </div>
                </div>
            </div>
        </telerik:RadAjaxPanel>
    </form>
    <style>
        html .RadSearchBox {
            display: -moz-inline-stack;
            display: inline-block;
            *display: inline:;
            *zoom: 1:;
            width: 300px;
            text-align: left;
            line-height: 30px;
            height: 30px;
            white-space: nowrap;
            vertical-align: middle;
        }
    </style>
    <script src="../Assets/javascripts/jquery.js"></script>
    <script src="../Assets/javascripts/common.js"></script>
</body>
</html>
