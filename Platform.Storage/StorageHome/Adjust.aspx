<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Adjust.aspx.cs" Inherits="Home" %>

<%@ Register Src="~/Menu/Menu.ascx" TagPrefix="homory" TagName="Menu" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <meta http-equiv="x-ua-compatible" content="ie=edge, chrome=1" />
    <meta name="viewport" content="width=device-width, initial-scale=1, minimum-scale=1, maximum-scale=1" />
    <title>物资管理 - 入库修改</title>
    <link href="../Assets/stylesheets/amazeui.min.css" rel="stylesheet" />
    <link href="../Assets/stylesheets/admin.css" rel="stylesheet" />
    <link href="../Assets/stylesheets/bootstrap.min.css" rel="stylesheet" />
    <link href="../Assets/stylesheets/bootstrap-theme.min.css" rel="stylesheet" />
    <script src="../Assets/javascripts/jquery.min.js"></script>
    <script src="../Assets/javascripts/amazeui.min.js"></script>
    <script src="../Assets/javascripts/app.js"></script>
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
    </script>

</head>
<body>
    <form id="form" runat="server">
        <div class="am-cf am-padding" style="border-bottom: 1px solid #E1E1E1;">
            <div class="am-fl am-cf">
                <strong class="am-text-primary am-text-lg">入库修改</strong>
            </div>
        </div>
        <telerik:RadScriptManager ID="sm" runat="server"></telerik:RadScriptManager>
        <telerik:RadAjaxLoadingPanel ID="lp" runat="server" Skin="MetroTouch"></telerik:RadAjaxLoadingPanel>
        <telerik:RadAjaxPanel ID="ap" runat="server" CssClass="grid-container" LoadingPanelID="lp">
            <div class="grid-100 mobile-grid-100 grid-parent">
                <table style="width: 100%; text-align: center;">
                    <tr>
                        <td style="text-align: right;">
                            <table style="width: 100%; text-align: center;">
                                <tr>
                                    <td>
                                        <div class="grid-100" style="height: 65px; margin-top: 50px;">
                                            原入库数量：<asp:Label ID="o_num" runat="server"></asp:Label>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <div class="grid-100" style="height: 65px;">
                                            原入库单价：<asp:Label ID="o_price" runat="server"></asp:Label>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <div class="grid-100" style="height: 65px;">
                                            原优惠总价：<asp:Label ID="o_off" runat="server"></asp:Label>
                                        </div>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td style="text-align: left;">
                            <table style="width: 100%; text-align: center;">
                                <tr>
                                    <td>
                                        <div class="grid-100" style="height: 65px; margin-top: 50px;">
                                            新入库数量：<telerik:RadNumericTextBox ID="x_num" runat="server" NumberFormat-DecimalDigits="2" DataType="System.Decimal" AllowOutOfRangeAutoCorrect="true" Width="40%"></telerik:RadNumericTextBox>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <div class="grid-100" style="height: 65px;">
                                            新入库单价：<telerik:RadNumericTextBox ID="x_price" runat="server" NumberFormat-DecimalDigits="2" DataType="System.Decimal" AllowOutOfRangeAutoCorrect="true" Width="40%"></telerik:RadNumericTextBox>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <div class="grid-100" style="height: 65px;">
                                            新优惠总价：<telerik:RadNumericTextBox ID="x_off" runat="server" NumberFormat-DecimalDigits="2" DataType="System.Decimal" AllowOutOfRangeAutoCorrect="true" Width="40%"></telerik:RadNumericTextBox>
                                        </div>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" style="width: 100%; text-align: center;">
                            <div class="grid-100" style="height: 65px; margin-top: 30px;">
                                <asp:ImageButton ID="ok" runat="server" AlternateText="修改" OnClick="ok_Click" class="btn btn-xm btn-default"></asp:ImageButton>
                                <asp:ImageButton ID="cancel" runat="server" AlternateText="取消" OnClick="cancel_Click" class="btn btn-xm btn-default"></asp:ImageButton>
                            </div>
                        </td>
                    </tr>
                </table>
            </div>
        </telerik:RadAjaxPanel>
    </form>
    <script src="../Assets/javascripts/jquery.js"></script>
    <script src="../Assets/javascripts/common.js"></script>
    <style type="text/css">
        th {
            text-align: center;
        }

        td {
            text-align: center;
        }
    </style>
</body>
</html>
