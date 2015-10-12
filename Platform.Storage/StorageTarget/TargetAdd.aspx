<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TargetAdd.aspx.cs" Inherits="TargetAdd" %>

<%@ Register Src="~/Menu/Menu.ascx" TagPrefix="homory" TagName="Menu" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <meta http-equiv="x-ua-compatible" content="ie=edge, chrome=1" />
    <meta name="viewport" content="width=device-width, initial-scale=1, minimum-scale=1, maximum-scale=1" />
    <title>物资管理 - 购置登记</title>
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
</head>
<body>
    <form id="form" runat="server">
        <telerik:RadScriptManager ID="sm" runat="server"></telerik:RadScriptManager>
        <homory:Menu runat="server" ID="menu" />
        <div class="am-cf am-padding" style="border-bottom: 1px solid #E1E1E1;">
            <div class="am-fl am-cf">
                <strong class="am-text-primary am-text-lg">添加购置单</strong>
            </div>
        </div>
        <telerik:RadAjaxPanel ID="ap" runat="server" CssClass="grid-container">
            <div class="grid-100 mobile-grid-100 grid-parent">
                <div style="height: 45px; margin-top: 10px;">
                    购置单号：<telerik:RadTextBox ID="number" runat="server" Width="40%"></telerik:RadTextBox>
                </div>
                <div style="height: 45px;">
                    发票编号：<telerik:RadTextBox ID="receipt" runat="server" Width="40%"></telerik:RadTextBox>
                </div>
                <div style="height: 45px;">
                    清单简述：<telerik:RadTextBox ID="content" runat="server" TextMode="MultiLine" Width="40%"></telerik:RadTextBox>
                </div>
                <div style="height: 45px;">
                    采购来源：<telerik:RadComboBox ID="source" runat="server" LocalizationPath="~/Language" DataTextField="Name" DataValueField="Name" Filter="Contains" MarkFirstMatch="true" AppendDataBoundItems="true" ShowToggleImage="false" Width="40%" AllowCustomText="true">
                        <Items>
                            <telerik:RadComboBoxItem Text="" Value="" Selected="true" />
                        </Items>
                        <ItemTemplate>
                            <%# Eval("Name") %><span style="display: none;"><%# Eval("PinYin") %></span>
                        </ItemTemplate>
                    </telerik:RadComboBox>
                </div>
                <div style="height: 45px;">
                    使用对象：<telerik:RadComboBox ID="target" runat="server" LocalizationPath="~/Language" DataTextField="Name" DataValueField="Name" Filter="Contains" MarkFirstMatch="true" AppendDataBoundItems="true" ShowToggleImage="false" Width="40%" AllowCustomText="true">
                        <Items>
                            <telerik:RadComboBoxItem Text="" Value="" Selected="true" />
                        </Items>
                        <ItemTemplate>
                            <%# Eval("Name") %><span style="display: none;"><%# Eval("PinYin") %></span>
                        </ItemTemplate>
                    </telerik:RadComboBox>
                </div>
                <div style="height: 45px;">
                    应付金额：<telerik:RadNumericTextBox ID="toPay" runat="server" NumberFormat-DecimalDigits="2" DataType="System.Decimal" AllowOutOfRangeAutoCorrect="true" Width="40%"></telerik:RadNumericTextBox>
                </div>
                <div style="height: 45px;">
                    实付金额：<telerik:RadNumericTextBox ID="paid" runat="server" NumberFormat-DecimalDigits="2" DataType="System.Decimal" AllowOutOfRangeAutoCorrect="true" Width="40%"></telerik:RadNumericTextBox>
                </div>
                <div style="height: 45px;">
                    <table align="center">
                        <tr>
                            <td>&nbsp;&nbsp; 保管人：</td>
                            <td width="145px">
                                <asp:Label ID="keeper" runat="server" Text="未选择"></asp:Label>
                                <asp:ImageButton ID="keeper_del" runat="server" AlternateText="×" OnClick="keeper_del_Click" Visible="false" />
                                <input id="keeperId" runat="server" type="hidden" /></td>
                            <td>请选择：</td>
                            <td>
                                <telerik:RadSearchBox ID="keeper_source" runat="server" MaxResultCount="10" LocalizationPath="~/Language" DataTextField="Name" DataValueField="Id" ShowSearchButton="false" OnDataSourceSelect="keeper_source_DataSourceSelect" ShowLoadingIcon="false" OnSearch="keeper_source_Search" Width="270px"></telerik:RadSearchBox>
                            </td>
                        </tr>
                    </table>
                </div>
                <div style="height: 45px;">
                    <table align="center">
                        <tr>
                            <td>&nbsp;&nbsp; 经手人：</td>
                            <td width="145px">
                                <asp:Label ID="brokerage" runat="server" Text="未选择"></asp:Label>
                                <asp:ImageButton ID="brokerage_del" runat="server" AlternateText="×" OnClick="brokerage_del_Click" Visible="false" />
                                <input id="brokerageId" runat="server" type="hidden" /></td>
                            <td>请选择：</td>
                            <td>
                                <telerik:RadSearchBox ID="brokerage_source" runat="server" EmptyMessage="经手人筛选" MaxResultCount="10" LocalizationPath="~/Language" DataTextField="Name" DataValueField="Id" ShowSearchButton="false" OnDataSourceSelect="brokerage_source_DataSourceSelect" ShowLoadingIcon="false" OnSearch="brokerage_source_Search" Width="270px"></telerik:RadSearchBox>
                            </td>
                        </tr>
                    </table>
                </div>
                <div style="height: 45px;">
                    购置时间：<telerik:RadDatePicker ID="day" runat="server" LocalizationPath="~/Language" ShowPopupOnFocus="true" Width="40%">
                        <DatePopupButton Visible="false" />
                    </telerik:RadDatePicker>
                </div>
                <div>
                    <telerik:RadButton ID="confirm" runat="server" Checked="false" Text="确认上述信息填写正确" AutoPostBack="true" ButtonType="ToggleButton" ToggleType="CheckBox"></telerik:RadButton>
                </div>
            </div>
            <div class="grid-100 mobile-grid-100 grid-parent" style="margin-top: 10px;">
                <asp:ImageButton AlternateText="保存并继续" ID="goOn" runat="server" OnClick="goOn_Click" class="btn btn-xm btn-default"></asp:ImageButton>
                <asp:ImageButton AlternateText="保存" ID="ok" runat="server" OnClick="ok_Click" class="btn btn-xm btn-default"></asp:ImageButton>
                <asp:ImageButton AlternateText="保存并入库" ID="goOn_in" runat="server" OnClick="goOn_in_Click" class="btn btn-xm btn-default"></asp:ImageButton>
            </div>
        </telerik:RadAjaxPanel>
    </form>
    <script src="../Assets/javascripts/jquery.js"></script>
    <script src="../Assets/javascripts/common.js"></script>
    <style>
        html .RadSearchBox_Bootstrap .rsbInput {
            height: 32px;
            line-height: 32px;
        }

        html body .RadInput_Bootstrap .riRead, html body .RadInput_Read_Bootstrap, html body .RadInput_Bootstrap .riDisabled, html body .RadInput_Disabled_Bootstrap {
            border-color: #cdcdcd;
            color: #909090;
            background: #eee;
        }

        html .RadSearchBox {
            display: -moz-inline-stack;
            display: inline-block;
            *display: inline:;
            *zoom: 1:;
            width: 40%;
            text-align: left;
            line-height: 35px;
            white-space: nowrap;
            vertical-align: middle;
        }
    </style>
</body>
</html>
