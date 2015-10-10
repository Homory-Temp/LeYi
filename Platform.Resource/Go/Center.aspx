<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Center.aspx.cs" Inherits="Go.GoCenter" EnableEventValidation="false" %>

<%@ Import Namespace="System.Web.Configuration" %>

<%@ Register Src="~/Control/CommonTop.ascx" TagPrefix="homory" TagName="CommonTop" %>
<%@ Register Src="~/Control/CommonBottom.ascx" TagPrefix="homory" TagName="CommonBottom" %>
<%@ Register Src="~/Control/PersonalActionPersonal.ascx" TagPrefix="homory" TagName="PersonalActionPersonal" %>
<%@ Register Src="~/Control/PersonalActionvideo.ascx" TagPrefix="homory" TagName="PersonalActionvideo" %>
<%@ Register Src="~/Control/CenterRight.ascx" TagPrefix="homory" TagName="CenterRight" %>
<%@ Register Src="~/Control/CenterLeft.ascx" TagPrefix="homory" TagName="CenterLeft" %>




<!DOCTYPE html>

<html>
<head runat="server">
    <title>资源平台 - 个人中心</title>
    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8">
    <meta http-equiv="Pragma" content="no-cache">
    <script src="../Script/jquery.min.js"></script>
    <link rel="stylesheet" href="../Style/common.css">
    <link rel="stylesheet" href="../Style/common(1).css">
    <link rel="stylesheet" href="../Style/index1.css">
    <link rel="stylesheet" href="../Style/2.css" id="skinCss">
    <link href="../Style/public.css" rel="stylesheet" />
    <link href="../Style/mhzy.css" rel="stylesheet" />
    <link href="../Style/center.css" rel="stylesheet" />
    <base target="_top" />
</head>
<body class="srx-phome">
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
                <telerik:RadWindow ID="popup_publish" Title="资源发布" runat="server" AutoSize="False" Width="320" Height="330" ShowContentDuringLoad="false" ReloadOnShow="False" KeepInScreenBounds="true" VisibleStatusbar="false" Behaviors="Close" Modal="True" Localization-Close="关闭" EnableEmbeddedScripts="True" EnableEmbeddedBaseStylesheet="True" VisibleTitlebar="True">
                    <ContentTemplate>
                        <style>
                            .pub_v, .pub_v:hover{display:block;margin:0 auto;background:url("../image/up/pub_v.png") 0 0 no-repeat;width:173px;height:48px;line-height:43px;color:#fff;padding-left:15px;overflow:hidden;text-decoration:none;font-size:16px;}
                            .pub_a, .pub_a:hover{display:block;margin:0 auto;background:url("../image/up/pub_a.png") 0 0 no-repeat;width:173px;height:48px;line-height:43px;color:#fff;padding-left:15px;overflow:hidden;text-decoration:none;font-size:16px;}
                            .pub_c, .pub_c:hover{display:block;margin:0 auto;background:url("../image/up/pub_c.png") 0 0 no-repeat;width:173px;height:48px;line-height:43px;color:#fff;padding-left:15px;overflow:hidden;text-decoration:none;font-size:16px;}
                            .pub_p, .pub_p:hover{display:block;margin:0 auto;background:url("../image/up/pub_p.png") 0 0 no-repeat;width:173px;height:48px;line-height:43px;color:#fff;padding-left:15px;overflow:hidden;text-decoration:none;font-size:16px;}
                        </style>
                        <div style="width: 280px; text-align: center; margin: auto;">
                            <a class="pub_v" style="cursor: pointer; margin: 20px auto 10px 50px;" href="Publishing.aspx?Type=Media">发布视频</a>
                            <a class="pub_a" style="cursor: pointer; margin: 10px auto 10px 50px;" href="Publishing.aspx?Type=Article">发布文章</a>
                            <a class="pub_c" style="cursor: pointer; margin: 10px auto 10px 50px;" href="Publishing.aspx?Type=Courseware">发布课件</a>
                            <a class="pub_p" style="cursor: pointer; margin: 10px auto 20px 50px;" href="Publishing.aspx?Type=Paper">发布试卷</a>
                        </div>
                    </ContentTemplate>
                </telerik:RadWindow>
            </Windows>
        </telerik:RadWindowManager>
        <script>
            var window_publish;

            function popupPublish() {
                window_publish = window.radopen(null, "popup_publish");
                return false;
            }
            function closePublish() {
                window_publish.close();
                return false;
            }
        </script>
        <homory:CommonTop runat="server" ID="CommonTop" />

        <div class="srx-bg">
            <div class="srx-wrap">

                <%--左上方个人信息区--%>
                <div class="srx-main srx-main-bg">

                    <homory:CenterLeft runat="server" ID="CenterLeft" />
                    <div class="srx-right">
                        <div class="srx-r1">
                            <div class="msgFeed user_feed mt15">
                                <div style="background-color: #FFF; margin-top: 8px;">


                                    <div id="tabA" class="tabControl" style="width: 575px; height: 280px; float: left; background-color: #FFF">

                                        <div class="box doing">
                                            <div style="width: 575px; margin: auto;">
                                                <div class="tabs">

                                                    <span name="tabTit">
                                                        <div class="tab">资源互动</div>
                                                        <div class="tab">我的评论</div>
                                                        <div class="tab">我的评分</div>
                                                        <div class="tab">我的回复</div>
                                                    </span>
                                                    <div style="margin: 10px 0px;">
                                                    </div>
                                                </div>


                                                <div class="tabClear"></div>
                                                <div class="tabContents" style="border-top: 2px solid #EFEFEF;">

                                                    <div class="tabContent">
                                                        <div class="fd-list" id="feedList">
                                                            <homory:PersonalActionPersonal runat="server" ID="PersonalActionPersonal" />
                                                        </div>
                                                    </div>
                                                    <div class="tabContent">
                                                        <div class="fd-list" id="feedList">
                                                            <homory:PersonalActionPersonal runat="server" ID="PersonalActionPersonal1" PAPType="1" />
                                                        </div>
                                                    </div>
                                                    <div class="tabContent">
                                                        <div class="fd-list" id="feedList">
                                                            <homory:PersonalActionPersonal runat="server" ID="PersonalActionPersonal2" PAPType="2" />
                                                        </div>
                                                    </div>
                                                    <div class="tabContent">
                                                        <div class="fd-list" id="feedList">
                                                            <homory:PersonalActionPersonal runat="server" ID="PersonalActionPersonal3" PAPType="3" />
                                                        </div>
                                                    </div>
                                                </div>

                                            </div>
                                        </div>
                                    </div>
                                </div>

                                <script src="../Script/index_click.js"></script>




                            </div>

                        </div>

                        <homory:CenterRight runat="server" id="CenterRight" />
                    </div>
                </div>
                <homory:CommonBottom runat="server" ID="CommonBottom" />



            </div>
        </div>
    </form>
</body>
</html>













