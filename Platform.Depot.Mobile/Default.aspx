<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>物资管理</title>
    <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=Edge,Chrome=1" />
    <meta name="viewport" content="width=device-width,initial-scale=1,minimum-scale=1,maximum-scale=1" />
    <script src="Content/jQuery/jquery.min.js"></script>
    <link href="assets/css/bootstrap.min.css" rel="stylesheet" />
    <link href="assets/css/style-responsive.css" rel="stylesheet" />
    <link href="assets/css/style.css" rel="stylesheet" />
    <link href="Content/Core/css/common.css" rel="stylesheet" />
    <link href="Content/Core/css/fix.css" rel="stylesheet" />
    <script src="assets/js/bootstrap.min.js"></script>
    <script src="Content/Homory/js/common.js"></script>
    <script src="Content/Homory/js/notify.min.js"></script>
    <!--[if lt IE 9]>
	    <script src="Content/Homory/js/html5shiv.js"></script>
	    <script src="Content/Homory/js/respond.min.js"></script>
    <![endif]-->
</head>
<body style="background: url( 'Images/quc_index_bg.jpg')  no-repeat 50%">
    <form id="form" runat="server" style="width: 100%;">
        <telerik:RadScriptManager runat="server"></telerik:RadScriptManager>
        <telerik:RadAjaxLoadingPanel ID="loading" runat="server" InitialDelayTime="1000">
            <div>&nbsp;</div>
            <div class="btn btn-lg btn-warning" style="margin-top: 50px;">正在加载 请稍候....</div>
        </telerik:RadAjaxLoadingPanel>
        <telerik:RadAjaxPanel ID="ap" runat="server" LoadingPanelID="loading">
            <div class="container">
                <div class="row">
                    <div class="col-md-12">
                        <img class="img-responsive" src="Common/配置/SsoLogo.png" />
                    </div>
                </div>
                <div class="row">&nbsp;</div>
                <div class="row">&nbsp;</div>
                <div class="row">
                    <div class="col-md-12" style="background: url('Images/bdbg11.png') repeat-x; height: 442px">
                        <div class="panel panel-default" style="border: none; background-color: transparent; box-shadow: none;">
                            <div class="panel panel-info" style="background-color: transparent; margin-top: 30px;">
                                <div class="panel-heading">
                                    <div class="panel-title" style="font-size: 20px;">用户登录</div>
                                </div>
                            </div>
                            <div class="panel" style="background-color: transparent; box-shadow: none;">
                                <div class="panel-body" style="border: none; padding-top: 0; background-color: transparent;">
                                    <input id="___id" runat="server" type="hidden" value="" />
                                    <div class="form-group">
                                        <div style="width: 100%; height: 20px; line-height: 20px; background-color: transparent; clear: both;">&nbsp;&nbsp;</div>
                                        <telerik:RadListView ID="list" runat="server" OnNeedDataSource="list_NeedDataSource">
                                            <ItemTemplate>
                                                <input type="button" class='<%# "btn btn-{0} dictionary".Formatted(Eval("Id").ToString().ToLower() == ___id.Value.ToLower() ? "warning" : "info") %>' id="do_in" runat="server" uid='<%# Eval("Id") %>' value='<%# Eval("Name") %>' onserverclick="do_in_ServerClick" />
                                            </ItemTemplate>
                                        </telerik:RadListView>
                                    </div>
                                    <div class="form-group text-center">
                                        <div style="width: 100%; height: 20px; line-height: 20px; background-color: transparent; clear: both;">&nbsp;&nbsp;</div>
                                        <asp:Button ID="buttonSign" runat="server" OnClick="buttonSign_Click" CssClass="btn btn-info btn-block" Text="登录" Style="font-size: 18px;"></asp:Button>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </telerik:RadAjaxPanel>
    </form>
</body>
</html>
