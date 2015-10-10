<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SignOn.aspx.cs" Inherits="Go.GoSignOn" %>

<!DOCTYPE html>

<html lang="zh-hans">
<head runat="server">
    <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=Edge,Chrome=1" />
    <meta name="viewport" content="width=device-width,initial-scale=1,minimum-scale=1,maximum-scale=1" />
    <title>乐翼教育云平台</title>
    <script src="../Content/jQuery/jquery.min.js"></script>
    <link href="../assets/css/bootstrap.min.css" rel="stylesheet" />
    <link href="../assets/css/style-responsive.css" rel="stylesheet" />
    <link href="../assets/css/style.css" rel="stylesheet" />
    <script src="../assets/js/bootstrap.min.js"></script>
    <link href="../Content/Homory/css/common.css" rel="stylesheet" />
    <link href="../Content/Sso/css/sign.css" rel="stylesheet" />
    <script src="../Content/Homory/js/common.js"></script>
    <script src="../Content/Homory/js/notify.min.js"></script>
    <script src="../Content/Sso/js/signOn.js"></script>
    <!--[if lt IE 9]>
        <script src="../Content/Homory/js/html5shiv.js"></script>
	    <script src="../Content/Homory/js/respond.min.js"></script>
    <![endif]-->
</head>
<body style="background: url( '../Images/quc_index_bg.jpg')  no-repeat 50%">
    <form id="form" runat="server" style="width: 100%;">
        <telerik:RadScriptManager runat="server"></telerik:RadScriptManager>
        <div class="container">
            <div class="row">&nbsp;</div>
            <div class="row">
                <div class="col-md-7">
                    <img class="img-responsive" src="../Common/配置/SsoLogo.png" />
                </div>
                <div class="col-md-5">
                    <img class="img-responsive" src="../Common/配置/SsoTitle.png" style="float: right;" />
                </div>
            </div>
            <div class="row">
                <div class="col-md-4 col-xs-12" style="background: url('../Images/bdbg11.png') repeat-x; height: 442px">
                    <div class="panel panel-default" style="border: none; background-color: transparent; box-shadow: none;">
                        <div class="panel panel-info" style="background-color: transparent; margin-top: 30px;">
                            <div class="panel-heading">
                                <div class="panel-title" style="font-size: 20px;">用户登录</div>
                            </div>
                        </div>
                        <div class="panel" style="background-color: transparent; box-shadow: none;">
                            <div class="panel-body" style="border: none; padding-top: 0; background-color: transparent;">
                                <telerik:RadAjaxPanel ID="areaAction" runat="server">
                                    <div class="form-group">
                                        <div class="input-group input-group-lg" style="margin-top: 40px;">
                                            <span class="input-group-addon"><span class="glyphicon glyphicon-user"></span></span>
                                            <input id="userName" runat="server" type="text" value="" maxlength="256" class="form-control" />
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <div class="input-group input-group-lg" style="margin-top: 40px;">
                                            <span class="input-group-addon"><span class="glyphicon glyphicon-lock"></span></span>
                                            <input id="userPassword" runat="server" type="password" maxlength="64" value="" class="form-control" />
                                        </div>
                                    </div>
                                    <div class="form-group" style="text-align: right; float: right;">
                                        <div class="checkbox" style="text-align: right;">
                                            <label class="inline-popups" style="text-align: right;">
                                                <input id="autoPasswordCK" type="checkbox" class="ckx" onclick="signYN(this);" style="width: 18px; height: 18px;" />
                                                &nbsp;&nbsp;<label id="autoPasswordLabel">记住密码&nbsp;&nbsp;</label>
                                            </label>
                                        </div>
                                        <div class="clearfix"></div>
                                    </div>
                                    <div class="form-group text-center">
                                        <div style="width: 100%; height: 20px; line-height: 20px; background-color: transparent; clear: both;">&nbsp;&nbsp;</div>
                                        <asp:Button ID="buttonSign" runat="server" OnClientClick="doRemember();" OnClick="buttonSign_OnClick" CssClass="btn btn-info btn-block" Text="登录" Style="font-size: 18px;"></asp:Button>
                                        <asp:Button ID="buttonRegister" runat="server" OnClick="buttonRegister_OnClick" CssClass="btn btn-info btn-block" Text="注册" Style="font-size: 18px; display: none;"></asp:Button>
                                    </div>
                                </telerik:RadAjaxPanel>
                                <telerik:RadCodeBlock runat="server">
                                    <script>
                                        function GetUrlParms() {
                                            var args = new Object();
                                            var query = location.search.substring(1);
                                            var pairs = query.split("&");
                                            for (var i = 0; i < pairs.length; i++) {
                                                var pos = pairs[i].indexOf('=');
                                                if (pos == -1) continue;
                                                var argname = pairs[i].substring(0, pos);
                                                var value = pairs[i].substring(pos + 1);
                                                args[argname] = unescape(value);
                                            }
                                            return args;
                                        }
                                        var args = new Object();
                                        args = GetUrlParms();
                                        var u = args["Name"];
                                        var p = args["Password"];
                                        if (u) {
                                            if (p) {
                                                $("#userName").val(decodeURIComponent(u));
                                                $("#userPassword").val(decodeURIComponent(p));
                                                __doPostBack("<%= buttonSign.ClientID %>", "");
                                            }
                                        }
                                    </script>
                                </telerik:RadCodeBlock>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-md-8 col-xs-12" style="background: url('../Images/bdbg11.png') repeat-x; height: 442px">
                    <div style="margin: 0; padding: 25px; background-color: transparent; height: 442px;">
                        <div class="panel panel-default" style="border: none; background-color: transparent; box-shadow: none;">
                            <style>
                                .mAuto {
                                    margin: auto;
                                    background-color: transparent;
                                    width: 100%;
                                }

                                html .rigToolsWrapper {
                                    display: none;
                                }

                                html .rigActiveImage img {
                                    height: 392px;
                                    background-color: transparent;
                                }

                                html .rigItemBox {
                                    background-color: transparent;
                                }
                            </style>
                            <telerik:RadImageGallery ID="gallery" runat="server" BackColor="Transparent" ShowLoadingPanel="False" AllowPaging="false" CssClass="mAuto" Height="392px" ImagesFolderPath="~/Common/配置/SsoSplash" DisplayAreaMode="Image" LoopItems="true">
                                <ThumbnailsAreaSettings Mode="ImageSlider" ShowScrollbar="false" ShowScrollButtons="false" />
                                <ImageAreaSettings NavigationMode="Zone" NextImageButtonText="下一图" PrevImageButtonText="上一图" ShowDescriptionBox="false" ShowNextPrevImageButtons="true" />
                                <ClientSettings>
                                    <ClientEvents OnImageGalleryCreated="galleryLoaded" />
                                    <AnimationSettings SlideshowSlideDuration="6000">
                                        <NextImagesAnimation Type="HorizontalSlide" Easing="Linear" Speed="3000" />
                                        <PrevImagesAnimation Type="HorizontalSlide" Easing="Linear" Speed="3000" />
                                    </AnimationSettings>
                                </ClientSettings>
                                <ToolbarSettings ShowFullScreenButton="false" ShowSlideshowButton="false" EnterFullScreenButtonText="全屏" ExitFullScreenButtonText="退出全屏" PlayButtonText="播放" PauseButtonText="暂停" ShowItemsCounter="false" Position="None" />
                            </telerik:RadImageGallery>
                        </div>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-md-12" style="text-align: center; display: none;">

                    <telerik:RadAjaxPanel ID="areaFavourite" runat="server">
                        <asp:Label ID="FavouriteCount" runat="server" CssClass="btn btn-warning" Font-Size="16px" onclick="goFavFX();"></asp:Label>
                        <div id="signThumbCount" class="floating ui blue label width100">
                            <asp:Button ID="signThumbPost" runat="server" CssClass="signHidden" OnClick="signThumbPost_OnClick" />
                        </div>
                    </telerik:RadAjaxPanel>
                </div>
            </div>
            <div class="row">
                <div class="col-md-12">&nbsp;</div>
            </div>
            <div class="row">
                <div class="col-md-3 col-xs-12" style="background: url('../Images/bdbg12.png') repeat-x; height: 152px">
                    <div class="panel panel-default" style="border: none; padding: 25px 15px 25px 15px; background-color: transparent;">
                        <div class="panel panel-info" style="margin-bottom: 0; background-color: transparent;">
                            <div class="panel-heading" style="font-size: 16px;">
                                <asp:Label ID="ApplicationCount" runat="server" CssClass="panel-title"></asp:Label>个应用
                            </div>
                            <div class="panel-body" style="text-align: center;">平台一体化</div>
                        </div>
                    </div>
                </div>
                <div class="col-md-3 col-xs-12" style="background: url('../Images/bdbg12.png') repeat-x; height: 152px">
                    <div class="panel panel-default" style="border: none; padding: 25px 15px 25px 15px; background-color: transparent;">
                        <div class="panel panel-info" style="margin-bottom: 0; background-color: transparent;">
                            <div class="panel-heading" style="font-size: 16px;">
                                <asp:Label ID="ResourceCount" runat="server" CssClass="panel-title"></asp:Label>资源
                            </div>
                            <div class="panel-body" style="text-align: center;">资源多样化</div>
                        </div>
                    </div>
                </div>
                <div class="col-md-3 col-xs-12" style="background: url('../Images/bdbg12.png') repeat-x; height: 152px">
                    <div class="panel panel-default" style="border: none; padding: 25px 15px 25px 15px; background-color: transparent;">
                        <div class="panel panel-info" style="margin-bottom: 0; background-color: transparent;">
                            <div class="panel-heading" style="font-size: 16px;">
                                <asp:Label ID="UserCount" runat="server" CssClass="panel-title"></asp:Label>位用户
                            </div>
                            <div class="panel-body" style="text-align: center;">用户专业化</div>
                        </div>
                    </div>
                </div>
                <div class="col-md-3 col-xs-12" style="background: url('../Images/bdbg12.png') repeat-x; height: 152px">
                    <div class="panel panel-default" style="border: none; padding: 25px 15px 25px 15px; background-color: transparent;">
                        <div class="panel panel-info" style="margin-bottom: 0; background-color: transparent;">
                            <div class="panel-heading" style="font-size: 16px;">
                                <asp:Label ID="VisitCount" runat="server" CssClass="panel-title"></asp:Label>次登录
                            </div>
                            <div class="panel-body" style="text-align: center; background-color: transparent;">登录统一化</div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-md-12">&nbsp;</div>
            </div>
            <div class="row">
                <div class="col-md-12" style="text-align: center;">
                    <img alt="" style="width: 80%; height: auto; margin: auto;" src="../Common/配置/SsoCopyright.png" />
                </div>
            </div>
        </div>
    </form>
    <script>
        $("input[name='userName']").focus();
    </script>
</body>
</html>
