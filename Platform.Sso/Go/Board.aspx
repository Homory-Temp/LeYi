<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Board.aspx.cs" Inherits="Go.GoBoard" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=Edge,Chrome=1" />
    <meta name="viewport" content="width=device-width,initial-scale=1,minimum-scale=1,maximum-scale=1" />
    <title>面板</title>
    <script src="../Content/jQuery/jquery.min.js"></script>
    <link href="../assets/css/bootstrap.min.css" rel="stylesheet" />
    <link href="../assets/css/style-responsive.css" rel="stylesheet" />
    <link href="../assets/css/style.css" rel="stylesheet" />
    <script src="../assets/js/bootstrap.min.js"></script>
    <link href="../Content/Homory/css/common.css" rel="stylesheet" />
    <link href="../Content/Sso/css/sign.css" rel="stylesheet" />
    <link href="../Content/Sso/css/board.css" rel="stylesheet" />
    <script src="../Content/Homory/js/common.js"></script>
    <script src="../Content/Homory/js/notify.min.js"></script>
    <script src="../Content/Sso/js/board.js"></script>
    <!--[if lt IE 9]>
	    <script src="../Content/Homory/js/html5shiv.js"></script>
	    <script src="../Content/Homory/js/respond.min.js"></script>
    <![endif]-->
</head>
<body>
    <form id="form" runat="server">
        <telerik:RadScriptManager runat="server"></telerik:RadScriptManager>
        <div class="container">
            <div class="row">
                <div class="col-md-2 col-xs-2">
                    <asp:Image ID="icon" runat="server" CssClass="ui image" Width="60" Height="60" />
                </div>
                <div class="col-md-8 col-xs-10" style="text-align: center; margin-top: 2px;">
                    <a>
                        <div id="headInfo" runat="server" class="btn btn-info btn-lg"></div>
                    </a>
                </div>
                <div class="col-md-2 col-xs-12" style="text-align: right; margin-top: 12px;">
                    <a href="../Go/SignOff" id="quit">
                        <div class="btn btn-warning">退出</div>
                    </a>
                </div>
            </div>
            <div class="row">
                <div class="col-md-12">
                    <ul class="nav nav-list">
                        <li class="divider" style="border-top: dashed 1px rgba(0,0,0,0.1);">&nbsp;</li>
                    </ul>
                </div>
            </div>
            <div class="row">&nbsp;</div>
            <div class="row">
                <asp:Repeater ID="items" runat="server">
                    <ItemTemplate>
                        <div class="col-md-4 col-xs-12">
                            <div class="panel panel-default" style="text-align: center; background-color: transparent;">
                                <div style="font-size: 16px; background-color: #000; filter:alpha(opacity=40); -moz-opacity:0.4; -khtml-opacity: 0.4; opacity: 0.4; height:40px; line-height:40px;">
                                    <div class="panel-title" style="color: white;">
                                        <%# Eval("Name") %>
                                    </div>
                                </div>
                                <div class="panel-body item signLink" style="text-align: center; background-color: transparent;" data-url='<%# Eval("Home").ToString().Contains("{0}") ? (string.Format("{0}&OnlineId={1}", string.Format(Eval("Home").ToString(), Server.UrlEncode(InitUser.Account), Server.UrlEncode(Homory.Model.HomoryCryptor.Decrypt(InitUser.Password, InitUser.CryptoKey, InitUser.CryptoSalt))), Session[Homory.Model.HomoryConstant.SessionOnlineId])) : (string.Format("{0}?OnlineId={1}", Eval("Home"), Session[Homory.Model.HomoryConstant.SessionOnlineId])) %>'>
                                    <img style="margin: auto;" onerror="this.src = '../Common/默认/应用.png';" src='<%# Eval("Icon") %>' alt="" />
                                </div>
                            </div>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
            </div>
            <div class="row">
                <div class="col-md-12 col-xs-12" style="text-align: right;">
                    <img class="ui image" src="../Common/配置/SsoLogo.png" width="350" height="70" />
                </div>
            </div>
        </div>
    </form>
</body>
</html>
