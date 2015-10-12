<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TargetEditPopup.aspx.cs" Inherits="TargetEditPopup" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <meta http-equiv="x-ua-compatible" content="ie=edge, chrome=1" />
    <meta name="viewport" content="width=device-width, initial-scale=1, minimum-scale=1, maximum-scale=1" />
    <title>编辑购置单</title>
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
        <telerik:RadCodeBlock ID="cb" runat="server">
            <script>
                function peek() {
                    var oWindow = null;
                    if (window.radWindow) oWindow = window.radWindow;
                    else if (window.frameElement.radWindow) oWindow = window.frameElement.radWindow;
                    return oWindow;
                }
                function ok() {
                    peek().BrowserWindow.rebind();
                    peek().close();
                }
                function cancel() {
                    peek().close();
                }
            </script>
        </telerik:RadCodeBlock>
        <telerik:RadAjaxPanel ID="ap" runat="server">
            <div class="grid-100 mobile-grid-100 grid-parent">
                <table class="table table-bordered" style="margin-top: 10px;" align="center">

                    <tr>
                        <td>购置单号：<asp:Label ID="number" runat="server"></asp:Label>
                        </td>
                        <td>发票编号：<telerik:RadTextBox ID="receipt" runat="server"></telerik:RadTextBox>
                        </td>
                        <td>购置时间：<telerik:RadDatePicker ID="day" runat="server" LocalizationPath="~/Language" ShowPopupOnFocus="true">
                            <DatePopupButton Visible="false" />
                        </telerik:RadDatePicker>
                        </td>
                    </tr>
                    <tr>
                        <td>采购来源：<telerik:RadComboBox ID="source" runat="server" AllowCustomText="true" LocalizationPath="~/Language" DataTextField="Name" DataValueField="Name" Filter="Contains" MarkFirstMatch="true" AppendDataBoundItems="true" ShowToggleImage="false">
                            <Items>
                                <telerik:RadComboBoxItem Text="" Value="" Selected="true" />
                            </Items>
                            <ItemTemplate>
                                <%# Eval("Name") %><span style="display: none;"><%# Eval("PinYin") %></span>
                            </ItemTemplate>
                        </telerik:RadComboBox>
                        </td>
                        <td>使用对象：<telerik:RadComboBox ID="target" runat="server" AllowCustomText="true" LocalizationPath="~/Language" DataTextField="Name" DataValueField="Name" Filter="Contains" MarkFirstMatch="true" AppendDataBoundItems="true" ShowToggleImage="false">
                            <Items>
                                <telerik:RadComboBoxItem Text="" Value="" Selected="true" />
                            </Items>
                            <ItemTemplate>
                                <%# Eval("Name") %><span style="display: none;"><%# Eval("PinYin") %></span>
                            </ItemTemplate>
                        </telerik:RadComboBox>
                        </td>
                        <td>应付金额：<telerik:RadNumericTextBox ID="toPay" runat="server" NumberFormat-DecimalDigits="2" DataType="System.Decimal" AllowOutOfRangeAutoCorrect="true"></telerik:RadNumericTextBox>
                        </td>
                    </tr>
                    <tr>

                        <td>实付金额：<telerik:RadNumericTextBox ID="paid" runat="server" NumberFormat-DecimalDigits="2" DataType="System.Decimal" AllowOutOfRangeAutoCorrect="true"></telerik:RadNumericTextBox>
                        </td>
                        <td>保管人：<asp:Label ID="keeper" runat="server"></asp:Label>
                            <asp:ImageButton ID="keeper_del" runat="server" AlternateText="×" OnClick="keeper_del_Click" Visible="false" />
                            <input id="keeperId" runat="server" type="hidden" />
                            <telerik:RadSearchBox ID="keeper_source" runat="server" EmptyMessage="保管人筛选" MaxResultCount="10" LocalizationPath="~/Language" DataTextField="Name" DataValueField="Id" ShowSearchButton="false" OnDataSourceSelect="keeper_source_DataSourceSelect" ShowLoadingIcon="false" OnSearch="keeper_source_Search"></telerik:RadSearchBox>
                        </td>
                        <td>经手人：<asp:Label ID="brokerage" runat="server"></asp:Label>
                            <asp:ImageButton ID="brokerage_del" runat="server" AlternateText="×" OnClick="brokerage_del_Click" Visible="false" />
                            <input id="brokerageId" runat="server" type="hidden" />
                            <telerik:RadSearchBox ID="brokerage_source" runat="server" EmptyMessage="经手人筛选" MaxResultCount="10" LocalizationPath="~/Language" DataTextField="Name" DataValueField="Id" ShowSearchButton="false" OnDataSourceSelect="brokerage_source_DataSourceSelect" ShowLoadingIcon="false" OnSearch="brokerage_source_Search"></telerik:RadSearchBox>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3">清单简述：<telerik:RadTextBox ID="content" Height="80" Width="80%" runat="server" TextMode="MultiLine"  style="width:80%;"  ></telerik:RadTextBox>
                        </td>
                    </tr>

                    <tr>
                        <td colspan="3">
                            <asp:ImageButton ID="ok" runat="server" AlternateText="保存" OnClick="ok_Click"></asp:ImageButton>
                            <asp:ImageButton ID="cancel" runat="server" AlternateText="取消" OnClick="cancel_Click"></asp:ImageButton></td>
                    </tr>
                </table>

            </div>
        </telerik:RadAjaxPanel>
    </form>
    <script src="../Assets/javascripts/jquery.js"></script>
    <script src="../Assets/javascripts/common.js"></script>
</body>
</html>
