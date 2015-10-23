<%@ Page Language="C#" AutoEventWireup="true" CodeFile="UseEdit.aspx.cs" Inherits="StoreAction_UseEdit" %>

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
        <homory:SideBarSingle runat="server" ID="SideBarSingle" Crumb="物资管理 - 出库编辑" />
        <telerik:RadAjaxPanel ID="ap" runat="server" CssClass="container-fluid" LoadingPanelID="loading">
            <div class="row">
                <div class="col-md-2"></div>
                <div class="col-md-8">
                    <div class="row">
                        <div class="col-md-4 text-right">出库编辑说明：</div>
                        <div class="col-md-8 text-left">
                            出库金额请勿随意变更
                            <br />
                            若需变更出库明细请访问出库查询页面
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-4 text-right">出库日期：</div>
                        <div class="col-md-8 text-left">
                            <telerik:RadDatePicker ID="day" runat="server" LocalizationPath="~/Language" ShowPopupOnFocus="true" Width="400" AutoPostBack="false">
                                <DatePopupButton runat="server" Visible="false" />
                            </telerik:RadDatePicker>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-4 text-right">出库金额：</div>
                        <div class="col-md-8 text-left">
                            <telerik:RadNumericTextBox ID="money" runat="server" NumberFormat-DecimalDigits="2" DataType="System.Decimal" AllowOutOfRangeAutoCorrect="true" Width="400" NumberFormat-AllowRounding="false"></telerik:RadNumericTextBox>
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
