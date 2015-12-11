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













