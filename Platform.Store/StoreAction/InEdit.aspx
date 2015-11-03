<%@ Page Language="C#" AutoEventWireup="true" CodeFile="InEdit.aspx.cs" Inherits="StoreAction_InEdit" %>

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
</head>
<body>
    <form id="form" runat="server">
        <homory:SideBarSingle runat="server" ID="SideBarSingle" Crumb="物资管理 - 入库编辑" />
        <telerik:RadCodeBlock runat="server">
            <script>
                function accMul(arg1, arg2) {
                    var m = 0, s1 = arg1.toString(), s2 = arg2.toString();
                    try { m += s1.split(".")[1].length } catch (e) { }
                    try { m += s2.split(".")[1].length } catch (e) { }
                    return Number(s1.replace(".", "")) * Number(s2.replace(".", "")) / Math.pow(10, m)
                }
                function calc(sender, args) {
                    var a = $find('<%= amount.ClientID %>').get_value();
                    var p = $find('<%= perPrce.ClientID %>').get_value();
                    $find('<%= money.ClientID %>').set_value(accMul(a, p));
                }
            </script>
        </telerik:RadCodeBlock>
        <telerik:RadAjaxPanel ID="ap" runat="server" CssClass="container-fluid" LoadingPanelID="loading">
            <div class="row">
                <div class="col-md-2"></div>
                <div class="col-md-8">
                    <div class="row">
                        <div class="col-md-4 text-right">入库编辑说明：</div>
                        <div class="col-md-8 text-left">
                            入库记录请勿随意变更
                            <br />
                            入库数量或入库合计填0则清除本次入库记录
                            <br />
                            若需变更入库记录所属购置单则数量填0后重新入库
                            <br />
                            有流通记录的数据无法修改
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-4 text-right">入库日期：</div>
                        <div class="col-md-8 text-left">
                            <telerik:RadDatePicker ID="day" runat="server" LocalizationPath="~/Language" ShowPopupOnFocus="true" Width="400" AutoPostBack="false">
                                <DatePopupButton runat="server" Visible="false" />
                            </telerik:RadDatePicker>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-4 text-right">入库数量：</div>
                        <div class="col-md-8 text-left">
                            <telerik:RadNumericTextBox ID="amount" runat="server" NumberFormat-DecimalDigits="2" DataType="System.Decimal" AllowOutOfRangeAutoCorrect="true" Width="400" ClientEvents-OnValueChanged="calc"></telerik:RadNumericTextBox>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-4 text-right">入库单价：</div>
                        <div class="col-md-8 text-left">
                            <telerik:RadNumericTextBox ID="perPrce" runat="server" NumberFormat-DecimalDigits="2" DataType="System.Decimal" AllowOutOfRangeAutoCorrect="true" Width="400" ClientEvents-OnValueChanged="calc"></telerik:RadNumericTextBox>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-4 text-right">入库合计：</div>
                        <div class="col-md-8 text-left">
                            <telerik:RadNumericTextBox ID="money" runat="server" NumberFormat-DecimalDigits="2" DataType="System.Decimal" AllowOutOfRangeAutoCorrect="true" Width="400" NumberFormat-AllowRounding="false"></telerik:RadNumericTextBox>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-4 text-right">入库存放地：</div>
                        <div class="col-md-8 text-left">
                            <telerik:RadTextBox ID="place" runat="server" Width="400"></telerik:RadTextBox>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-4 text-right">入库备注：</div>
                        <div class="col-md-8 text-left">
                            <telerik:RadTextBox ID="note" runat="server" Width="400"></telerik:RadTextBox>
                        </div>
                    </div>
                    <div class="row">&nbsp;</div>
                    <div class="row">
                        <div class="col-md-4 text-right">&nbsp;</div>
                        <div class="col-md-8 text-left">
                            <input type="button" class="btn btn-tumblr" id="go" runat="server" value="保存" onserverclick="go_ServerClick" />
                            &nbsp;&nbsp;&nbsp;&nbsp;
                            <input type="button" class="btn btn-tumblr" id="cancel" runat="server" value="取消" onserverclick="cancel_ServerClick" />
                        </div>
                    </div>
                </div>
                <div class="col-md-2"></div>
            </div>
        </telerik:RadAjaxPanel>
    </form>
</body>
</html>
