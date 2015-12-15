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













