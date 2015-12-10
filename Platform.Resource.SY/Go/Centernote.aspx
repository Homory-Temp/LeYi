<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Centernote.aspx.cs" Inherits="Go.GoCenternote" EnableEventValidation="false" %>

<%@ Import Namespace="System.Web.Configuration" %>
<%@ Import Namespace="Homory.Model" %>

<%@ Register Src="~/Control/CommonTop.ascx" TagPrefix="homory" TagName="CommonTop" %>
<%@ Register Src="~/Control/CommonBottom.ascx" TagPrefix="homory" TagName="CommonBottom" %>
<%@ Register Src="~/Control/PersonalActionvideo.ascx" TagPrefix="homory" TagName="PersonalActionvideo" %>
<%@ Register Src="~/Control/CenterLeft.ascx" TagPrefix="homory" TagName="CenterLeft" %>
<%@ Register Src="~/Control/CenterRight.ascx" TagPrefix="homory" TagName="CenterRight" %>




<!DOCTYPE html>

<html>
<head runat="server">
    <title>听课笔记- 个人中心-资源平台 </title>
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
                                                         <div class="tab">听课笔记</div>
                                                        



                                                    </span>
                                                    <div style="margin: 10px 0px;padding-left:200px;">
	                                                    开始时间：<telerik:RadDatePicker runat="server" ID="ts"></telerik:RadDatePicker>&nbsp;&nbsp;
														结束时间：<telerik:RadDatePicker runat="server" ID="te"></telerik:RadDatePicker>&nbsp;&nbsp;
                                                        <a id="filterGo" runat="server" onserverclick="filterGo_OnServerClick">搜索</a>
                                                    </div>
                                                </div>


                                                <div class="tabClear"></div>
                                                <div class="tabContents" style="border-top: 2px solid #EFEFEF;">

                                                    <div class="tabContent">
                                                        <table width="575">
                                                         <tr style="background:#F1F4F9;">
                                                                    <td width="120" height="30">课程名称</td>
                                                                    <td width="120">过程记录</td>
                                                                    <td width="120">重点摘要</td>
                                                                    <td  width="80">听课时间</td>
                                                                    <td></td>
                                                                    
                                                                </tr>
                                                             
                                                        <asp:Repeater runat="server" ID="result">
                                                            <HeaderTemplate>
                                                              
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <tr style="border-bottom:1px dashed #F1F4F9;">
                                                                    
                                                                    <td algin="left" height="25"> · 
                                                                        <a target="_blank" href='<%# string.Format("../Go/{0}?Id={1}", ((Homory.Model.MediaNote)Container.DataItem).Resource.Type== Homory.Model.ResourceType.视频 ? "ViewVideo" : "ViewPlain", Eval("ResourceId")) %>'><%# ((Homory.Model.MediaNote)Container.DataItem).Resource.Title.CutString(10,"...") %></a>
                                                                    </td>
                                                                    <td>
														                <label id="pa" runat="server" style="cursor: pointer;"><%# Eval("A").ToString().CutString(10, "...") %></label>
                                                                        <telerik:RadWindow runat="server" OffsetElementID='<%# Container.FindControl("pa").ClientID %>' OpenerElementID='<%# Container.FindControl("pa").ClientID %>' Width="400" Height="300" Title="过程记录">
                                                                            <ContentTemplate>
                                                                                <div style="overflow: auto;">
                                                                                    <%# Eval("A").ToString() %>
                                                                                </div>
                                                                            </ContentTemplate>
                                                                        </telerik:RadWindow>
                                                                    </td>
                                                                    <td>
                                                                        <label id="pb" runat="server" style="cursor: pointer;"><%# Eval("B").ToString().CutString(10, "...") %></label>
                                                                        <telerik:RadWindow runat="server" OffsetElementID='<%# Container.FindControl("pb").ClientID %>' OpenerElementID='<%# Container.FindControl("pb").ClientID %>' Width="400" Height="300" Title="重点摘要">
                                                                            <ContentTemplate>
                                                                                <div style="overflow: auto;">
                                                                                    <%# Eval("B").ToString() %>
                                                                                </div>
                                                                            </ContentTemplate>
                                                                        </telerik:RadWindow>
                                                                    </td>
                                                                    <td><em><%# ((DateTime)Eval("Time")).ToString("yyyy-MM-dd") %></em>
                                                                    </td>
                                                                    <td>
                                                                    </td>
                                                                </tr>
                                                            </ItemTemplate>
                                                            <FooterTemplate>
                                                                </table>
                                                            </FooterTemplate>
                                                        </asp:Repeater>
                                                    </div>
                                                    


                                                </div>

                                            </div>
                                        </div>
                                    </div>
                                </div>

                                <script src="../Script/index.js"></script>




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













