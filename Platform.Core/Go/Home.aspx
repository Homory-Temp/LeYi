<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Home.aspx.cs" Inherits="Go.GoHome" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=Edge,Chrome=1" />
    <meta name="viewport" content="width=device-width,initial-scale=1,minimum-scale=1,maximum-scale=1" />
    <title>基础平台</title>
    <script src="../Content/jQuery/jquery.min.js"></script>
    <link href="../assets/css/bootstrap.min.css" rel="stylesheet" />
    <link href="../assets/css/style-responsive.css" rel="stylesheet" />
    <link href="../assets/css/style.css" rel="stylesheet" />
    <script src="../assets/js/bootstrap.min.js"></script>
    <link href="../Content/Homory/css/common.css" rel="stylesheet" />
    <link href="../Content/Core/css/home.css" rel="stylesheet" />
    <link href="../Content/Core/css/common.css" rel="stylesheet" />
    <script src="../Content/Homory/js/common.js"></script>
    <script src="../Content/Homory/js/notify.min.js"></script>
    <script src="../Content/Core/js/home.js"></script>
    <!--[if lt IE 9]>
	    <script src="../Content/Homory/js/html5shiv.js"></script>
	    <script src="../Content/Homory/js/respond.min.js"></script>
    <![endif]-->
</head>
<body>
    <form id="formHome" runat="server">
        <telerik:RadScriptManager ID="scriptManager" runat="server"></telerik:RadScriptManager>
        <div class="container">
            <div class="row">
                <div class="col-md-7">
                    <img class="img-responsive" src="../Common/配置/CoreLogo.png" width="400" />
                </div>
                <div class="col-md-5" style="text-align: right; margin-top: 25px;">
                    <asp:Label ID="u" runat="server" CssClass="btn btn-info"></asp:Label>&nbsp;&nbsp;
                    <asp:LinkButton ID="qb" runat="server" Text="退出" CssClass="btn btn-warning" OnClick="qb_click"></asp:LinkButton>
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
                <asp:Repeater ID="repeater" runat="server">
                    <ItemTemplate>
                        <div class="col-md-4 col-xs-12" style="text-align: center;">
                            <div>
                                    <div class="panel-body" style="text-align: center;">
                                        <div>
                                            <span class='<%# string.Format("glyphicon {0}", Eval("Icon")) %>'></span>
                                        </div>
                                        <div style="font-size: 25px; font-weight: bold;"><%# Eval("Name") %></div>
                                        <div class="padSubMenu"></div>
                                        <div class="sub header"><%# SubMenu(Container.DataItem as Homory.Model.Menu) %></div>
                                    </div>
                            </div>
                            <div class="glyphicon glyphicon-">

                            </div>
                            <%--<h2 class="ui center aligned icon header">
                                <div><i class=></i></div>
                            </h2>--%>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
            </div>
            <div class="row">&nbsp;</div>
            <div class="row">
                <div class="col-md-12" style="text-align: center;">
                    <img alt="" style="width: 80%; height: auto; margin: auto;" src="../Common/配置/CoreCopyright.png" />
                </div>
            </div>
        </div>
        <style>
            .rb_panel {
                right: 0;
                bottom: 0;
                position: absolute;
            }
        </style>
        <telerik:RadAjaxPanel ID="panel" runat="server" CssClass="rb_panel"></telerik:RadAjaxPanel>
    </form>
</body>
</html>
