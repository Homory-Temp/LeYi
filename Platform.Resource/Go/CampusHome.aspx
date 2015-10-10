<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CampusHome.aspx.cs" Inherits="Go.GoCampusHome" %>

<%@ Register Src="~/Control/HomeTop.ascx" TagPrefix="homory" TagName="HomeTop" %>
<%@ Register Src="~/Control/CommonBottom.ascx" TagPrefix="homory" TagName="CommonBottom" %>
<%@ Register Src="~/Control/HomeSplash.ascx" TagPrefix="homory" TagName="HomeSplash" %>
<%@ Register Src="~/Control/HomeTopic.ascx" TagPrefix="homory" TagName="HomeTopic" %>
<%@ Register Src="~/Control/HomeCatalog.ascx" TagPrefix="homory" TagName="HomeCatalog" %>
<%@ Register Src="~/Control/HomeNote.ascx" TagPrefix="homory" TagName="HomeNote" %>
<%@ Register Src="~/Control/HomeArticle.ascx" TagPrefix="homory" TagName="HomeArticle" %>
<%@ Register Src="~/Control/HomeHonor.ascx" TagPrefix="homory" TagName="HomeHonor" %>
<%@ Register Src="~/Control/HomeStudio.ascx" TagPrefix="homory" TagName="HomeStudio" %>
<%@ Register Src="~/Control/HomeGroup.ascx" TagPrefix="homory" TagName="HomeGroup" %>
<%@ Register Src="~/Control/HomeVideo.ascx" TagPrefix="homory" TagName="HomeVideo" %>
<%@ Register Src="~/Control/HomeCourseware.ascx" TagPrefix="homory" TagName="HomeCourseware" %>
<%@ Register Src="~/Control/PersonalAction.ascx" TagPrefix="homory" TagName="PersonalAction" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <title>资源平台 - 首页</title>
    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8">
    <meta http-equiv="Pragma" content="no-cache">
    <link href="../Style/common.css" rel="stylesheet" />
    <link href="../Style/login.css" rel="stylesheet" />
    <link href="../Style/plaza.css" rel="stylesheet" />
    <script src="../Script/JQ.js"></script>
    <script src="../Script/jquery.tab.js"></script>
    <script src="../Script/logger.js"></script>
    <script src="../Script/zzsc.js"></script>
    <script src="../Script/jquery.min1.js"></script>
    <script src="../Script/jquery.min.js"></script>
    <script type="text/javascript" src="js/JQ.js"></script>
    <script type="text/javascript" src="js/zzsc.js"></script>
    <script src="js/jquery.min.js"></script>
    <script src="js/logger.js"></script>
    <script src="js/bds_s_v2.js"></script>
    <base target="_top" />
</head>
<body class="srx-plogin" style="margin: 0; padding: 0;">
    <form runat="server">
        <telerik:RadScriptManager ID="Rsm" runat="server">
            <Scripts>
                <asp:ScriptReference Assembly="Telerik.Web.UI" Name="Telerik.Web.UI.Common.Core.js" />
                <asp:ScriptReference Assembly="Telerik.Web.UI" Name="Telerik.Web.UI.Common.jQuery.js" />
                <asp:ScriptReference Assembly="Telerik.Web.UI" Name="Telerik.Web.UI.Common.jQueryInclude.js" />
            </Scripts>
        </telerik:RadScriptManager>
        <telerik:RadWindowManager runat="server" ID="Rwm" Skin="Metro">
            <Windows>
                <telerik:RadWindow ID="homory_note_view" runat="server" NavigateUrl="../Popup/HomeNotePopup.aspx" Skin="Metro" ShowContentDuringLoad="false" Width="500" Height="300" ReloadOnShow="true" KeepInScreenBounds="true" VisibleStatusbar="false" Behaviors="Move,Close" Modal="true" Localization-Close="关闭">
                </telerik:RadWindow>
                <telerik:RadWindow ID="homory_selector" runat="server" NavigateUrl="../Popup/CampusSelector.aspx" Skin="Metro" ShowContentDuringLoad="false" Width="900" Height="600" ReloadOnShow="true" KeepInScreenBounds="true" VisibleStatusbar="false" Behaviors="Move,Close" Modal="true" Localization-Close="关闭">
                </telerik:RadWindow>
            </Windows>
        </telerik:RadWindowManager>
        <script>
            function popup(id) {
                window.open("../Go/PlayVideo.aspx?" + id);
                //window.radopen("../Go/PlayVideo.aspx?" + id, "homory_note_view");
                return false;
            }
        </script>

        <div class="lg-main-box" id="mainBox">
            <div class="login">
                <homory:HomeTop runat="server" ID="HomeTop" />
                <div class="login-lr">
                    <div class="login-l">
                        <homory:HomeSplash runat="server" ID="HomeSplash" />
                        <homory:HomeCatalog runat="server" ID="HomeCatalog" />
                        <div class="c-plaza" id="cPlaza" style="background-color: #FFF">
                            <div class="c-p-content clearfix">
                                <homory:HomeVideo runat="server" ID="HomeVideo" />
                            </div>
                        </div>
                        <homory:HomeCourseware runat="server" ID="HomeCourseware" />
                        <script src="../Script/index.js"></script>
                    </div>
                    <div class="login-r">
                        <homory:HomeNote runat="server" ID="HomeNote" Count="5" MaxTitleLength="12" />

	                    <div style="margin-top: 0; margin-left: 0; margin-right: 0; margin-bottom: 6px; padding: 0;">
		                    <a href="../Go/Search.aspx?Assistant=1"><img src="../Image/help.jpg" style="margin: 0; padding: 0;" /></a>
	                    </div>

                        <homory:PersonalAction runat="server" ID="PersonalAction" />

                        <homory:HomeArticle runat="server" ID="HomeArticle" Count="5" MaxTitleLength="12" />

                        <telerik:RadAjaxPanel runat="server">
                            <div class="box class-feed" style="width: 262px; height: 80px;">
                                <div class="box-hd">资源标签<a id="reTag" target="_self" style="float:right;cursor:pointer;font-size:13px;" runat="server" onserverclick="reTag_ServerClick">换一换</a></div>
                                <div class="box-bd" style="height: 60px; overflow: hidden;">
                                        <telerik:RadTagCloud runat="server" ID="tags" AutoPostBack="false" Font-Size="16px" Font-Underline="true" ForeColor="#227DC5" OnClientItemClicking="tagGo">
                                        </telerik:RadTagCloud>
                                </div>
                            </div>
                        </telerik:RadAjaxPanel>
                        <script>
                            function tagGo(sender, args) {
                                var homory_peek = "../Go/Search?Content=" + encodeURIComponent(args.get_item().get_text());
                                window.open(homory_peek);
                                args.set_cancel(true);
                            }
                        </script>

                        <homory:HomeTopic runat="server" ID="HomeTopic" />
                        <homory:HomeHonor runat="server" ID="HomeHonor" />
                        <homory:HomeGroup runat="server" ID="HomeGroup" />
                        <homory:HomeStudio runat="server" ID="HomeStudio" />
                    </div>
                </div>
            </div>
        </div>
        <homory:CommonBottom runat="server" ID="CommonBottom" />
    </form>
</body>
</html>
