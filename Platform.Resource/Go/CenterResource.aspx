<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CenterResource.aspx.cs" Inherits="Go.GoCenterResource" EnableEventValidation="false" %>

<%@ Import Namespace="System.Web.Configuration" %>

<%@ Register Src="~/Control/CommonTop.ascx" TagPrefix="homory" TagName="CommonTop" %>
<%@ Register Src="~/Control/CommonBottom.ascx" TagPrefix="homory" TagName="CommonBottom" %>
<%@ Register Src="~/Control/PersonalActionvideo.ascx" TagPrefix="homory" TagName="PersonalActionvideo" %>
<%@ Register Src="~/Control/CenterLeft.ascx" TagPrefix="homory" TagName="CenterLeft" %>
<%@ Register Src="~/Control/CenterRight.ascx" TagPrefix="homory" TagName="CenterRight" %>







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
                <telerik:RadWindow ID="popup_pushX" runat="server" OnClientClose="pushPopped" Title="呈送" ReloadOnShow="True" Width="600" Height="400" Top="60" Left="200" ShowContentDuringLoad="false" VisibleStatusbar="false" Behaviors="Move,Close" Modal="True" CenterIfModal="False" Localization-Close="关闭">
				</telerik:RadWindow>
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
            function popupPushX(url) {
                window.radopen(url, "popup_pushX");
                return false;
            }
        </script>
        <homory:CommonTop runat="server" ID="CommonTop" />

        <div class="srx-bg">
            <div class="srx-wrap">

                <%--左上方个人信息区--%>
                <div class="srx-main srx-main-bg">
					<telerik:RadAjaxPanel ID="LEFT" runat="server" OnAjaxRequest="LEFT_AjaxRequest">
                    <homory:CenterLeft runat="server" ID="CenterLeftControl" />
                    </telerik:RadAjaxPanel>
                    <div class="srx-right">
                        <div class="srx-r1">
                            <div class="msgFeed user_feed mt15">
                                <div style="background-color: #FFF; margin-top: 8px;">


                                    <div id="tabA" class="tabControl" style="width: 575px; height: 280px; float: left; background-color: #FFF">

                                        <div class="box doing">
                                            <div style="width: 575px; margin: auto;">
                                                <div class="tabs">
                                                    <div class="box-hd" style="float: left; width: 60px; font-size: 20px; color: #555; font-weight: normal;">&nbsp;</div>
                                                    <span name="tabTit">
                                                        <div class="tab">视频资源</div>
                                                        <div class="tab">文章资源</div>
                                                        <div class="tab">课件资源</div>
                                                        <div class="tab">试卷资源</div>



                                                    </span>
                                                    <div style="margin: 10px 0px;">
                                                        <input type="text" id="filter" runat="server" style="height: 25px;" />
                                                        <a id="filterGo" runat="server" onserverclick="filterGo_OnServerClick">搜索</a>
                                                    </div>
                                                </div>


                                                <div class="tabClear"></div>
                                                <div class="tabContents" style="border-top: 2px solid #EFEFEF;">

                                                    <div class="tabContent">
                                                        <telerik:RadAjaxPanel runat="server" ID="pp1">
                                                       <asp:Repeater runat="server" ID="Repeater3">
                                                            <HeaderTemplate>
                                                                <table width="575">
                                                                    <tr style="background:#F1F4F9;">
                                                                    <td width="260" height="30">标题</td>
                                                                    <td width="120">&nbsp;&nbsp;&nbsp;&nbsp;操作</td>
                                                                    <td width="120">&nbsp;&nbsp;&nbsp;&nbsp;发布时间</td>
                                                                    
                                                                </tr>
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <tr>
                                                                    <td height="25"><img src='<%# string.Format("../Image/img/{0}.jpg", Eval("Thumbnail")) %>' width="16" height="16" />&nbsp;&nbsp;&nbsp;&nbsp;<a href='<%# string.Format("../Go/ViewVideo?Id={0}", Eval("Id")) %>'><%# Eval("Title") %></a>
                                                                    </td>
                                                                    <td>&nbsp;&nbsp;&nbsp;&nbsp;<a  href='<%# string.Format("../Go/Editing?Id={0}", Eval("Id")) %>'>编辑</a>
                                                                    &nbsp;&nbsp;&nbsp;&nbsp;<a id="del1" runat="server"  data-id='<%# Eval("Id") %>' onserverclick="del1_ServerClick" target="_self">删除</a>
                                                                    </td>
                                                                    <td>&nbsp;&nbsp;&nbsp;&nbsp;
														     <em><%# ((DateTime)Eval("Time")).ToString("yyyy-MM-dd") %></em>
                                                                    </td>
                                                                </tr>
                                                            </ItemTemplate>
                                                            <FooterTemplate>
                                                                </table>
                                                            </FooterTemplate>
                                                        </asp:Repeater></telerik:RadAjaxPanel>
                                                    </div>
                                                    <div class="tabContent">
                                                        <telerik:RadAjaxPanel runat="server" ID="pp2">
                                                        <asp:Repeater runat="server" ID="result">
                                                            <HeaderTemplate>
                                                                <table width="575">
                                                                    <tr style="background:#F1F4F9;">
                                                                    <td width="260" height="30">标题</td>
                                                                    <td width="120">&nbsp;&nbsp;&nbsp;&nbsp;操作</td>
                                                                    <td width="120">&nbsp;&nbsp;&nbsp;&nbsp;发布时间</td>
                                                                    
                                                                </tr>
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <tr>
                                                                    <td height="25">
                                                                        <img src='<%# string.Format("../Image/img/{0}.jpg", Eval("Thumbnail")) %>' width="16" height="16" />
                                                                    &nbsp;&nbsp;&nbsp;&nbsp;<a href='<%# string.Format("../Go/ViewPlain?Id={0}", Eval("Id")) %>'><%# Eval("Title") %></a>
                                                                    </td>
                                                                    <td>&nbsp;&nbsp;&nbsp;&nbsp;<a  href='<%# string.Format("../Go/Editing?Id={0}", Eval("Id")) %>'>编辑</a>
                                                                    &nbsp;&nbsp;&nbsp;&nbsp;<a id="del2" runat="server" target="_self"  data-id='<%# Eval("Id") %>' onserverclick="del2_ServerClick">删除</a>
                                                                    </td>
                                                                    <td>&nbsp;&nbsp;&nbsp;&nbsp;
														     <em><%# ((DateTime)Eval("Time")).ToString("yyyy-MM-dd") %></em>
                                                                    </td>
                                                                </tr>
                                                            </ItemTemplate>
                                                            <FooterTemplate>
                                                                </table>
                                                            </FooterTemplate>
                                                        </asp:Repeater></telerik:RadAjaxPanel>
                                                    </div>
                                                    <div class="tabContent">
                                                        <telerik:RadAjaxPanel runat="server" ID="pp3">
                                                       <asp:Repeater runat="server" ID="Repeater1">
                                                            <HeaderTemplate>
                                                                <table width="575">
                                                                    <tr style="background:#F1F4F9;">
                                                                    <td width="260" height="30">标题</td>
                                                                    <td width="120">&nbsp;&nbsp;&nbsp;&nbsp;操作</td>
                                                                    <td width="120">&nbsp;&nbsp;&nbsp;&nbsp;发布时间</td>
                                                                    
                                                                </tr>
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <tr>
                                                                    <td height="25">
                                                                        <img src='<%# string.Format("../Image/img/{0}.jpg", Eval("Thumbnail")) %>' width="16" height="16" />
                                                                    &nbsp;&nbsp;&nbsp;&nbsp;<a href='<%# string.Format("../Go/ViewPlain?Id={0}", Eval("Id")) %>'><%# Eval("Title") %></a>
                                                                    </td>
                                                                    <td>&nbsp;&nbsp;&nbsp;&nbsp;<a  href='<%# string.Format("../Go/Editing?Id={0}", Eval("Id")) %>'>编辑</a>
                                                                    &nbsp;&nbsp;&nbsp;&nbsp;<a id="del3" runat="server" target="_self"  data-id='<%# Eval("Id") %>' onserverclick="del3_ServerClick">删除</a>
                                                                    </td>
                                                                    <td>&nbsp;&nbsp;&nbsp;&nbsp;
														     <em><%# ((DateTime)Eval("Time")).ToString("yyyy-MM-dd") %></em>
                                                                    </td>
                                                                </tr>
                                                            </ItemTemplate>
                                                            <FooterTemplate>
                                                                </table>
                                                            </FooterTemplate>
                                                        </asp:Repeater></telerik:RadAjaxPanel>
                                                    </div>

                                                    <div class="tabContent">
                                                        <telerik:RadAjaxPanel runat="server" ID="pp4">
                                                       <asp:Repeater runat="server" ID="Repeater2">
                                                            <HeaderTemplate>
                                                                <table width="575">
                                                                    <tr style="background:#F1F4F9;">
                                                                    <td width="260" height="30">标题</td>
                                                                    <td width="120">&nbsp;&nbsp;&nbsp;&nbsp;操作</td>
                                                                    <td width="120">&nbsp;&nbsp;&nbsp;&nbsp;发布时间</td>
                                                                    
                                                                </tr>
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <tr>
                                                                    <td height="25">
                                                                        <img src='<%# string.Format("../Image/img/{0}.jpg", Eval("Thumbnail")) %>' width="16" height="16" />
                                                                    &nbsp;&nbsp;&nbsp;&nbsp;<a href='<%# string.Format("../Go/ViewPlain?Id={0}", Eval("Id")) %>'><%# Eval("Title") %></a>
                                                                    </td>
                                                                    <td>&nbsp;&nbsp;&nbsp;&nbsp;<a  href='<%# string.Format("../Go/Editing?Id={0}", Eval("Id")) %>'>编辑</a>
                                                                    &nbsp;&nbsp;&nbsp;&nbsp;<a id="del4" data-id='<%# Eval("Id") %>' runat="server" target="_self" onserverclick="del4_ServerClick">删除</a>
                                                                    </td>
                                                                    <td>&nbsp;&nbsp;&nbsp;&nbsp;
														     <em><%# ((DateTime)Eval("Time")).ToString("yyyy-MM-dd") %></em>
                                                                    </td>
                                                                </tr>
                                                            </ItemTemplate>
                                                            <FooterTemplate>
                                                                </table>
                                                            </FooterTemplate>
                                                        </asp:Repeater></telerik:RadAjaxPanel>
                                                    </div>


                                                </div>

                                            </div>
                                        </div>
                                    </div>
                                </div>

                                <script src="../Script/index_click.js"></script>




                            </div>

                        </div>

                        <homory:CenterRight runat="server" ID="CenterRight" />
                    </div>
                </div>
                <homory:CommonBottom runat="server" ID="CommonBottom" />



            </div>
        </div>
    </form>
</body>
</html>













