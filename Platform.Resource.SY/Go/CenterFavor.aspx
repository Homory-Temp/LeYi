<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CenterFavor.aspx.cs" Inherits="Go.GoCenter" EnableEventValidation="false" %>

<%@ Import Namespace="System.Web.Configuration" %>

<%@ Register Src="~/Control/CommonTop.ascx" TagPrefix="homory" TagName="CommonTop" %>
<%@ Register Src="~/Control/CommonBottom.ascx" TagPrefix="homory" TagName="CommonBottom" %>
<%@ Register Src="~/Control/PersonalActionvideo.ascx" TagPrefix="homory" TagName="PersonalActionvideo" %>
<%@ Register Src="~/Control/CenterLeft.ascx" TagPrefix="homory" TagName="CenterLeft" %>
<%@ Register Src="~/Control/CenterRight.ascx" TagPrefix="homory" TagName="CenterRight" %>




<!DOCTYPE html>

<html>
<head runat="server">
    <title>我的收藏- 个人中心-资源平台 </title>
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

                                    <telerik:RadAjaxPanel runat="server">
                                    <div id="tabA" class="tabControl" style="width: 575px; height: 280px; float: left; background-color: #FFF">

                                        <div class="box doing">
                                            <div style="width: 575px; margin: auto;">
                                                <div class="tabs">
                                               
                                                    <span name="tabTit">
                                                         <div class="tab">我的收藏</div>
                                                        



                                                    </span>
                                                    <div style="margin: 10px 0px;padding-left:200px;">
                                                        <input type="text" id="filter" runat="server" style="height: 23px;width:300px;" />
                                                        <a id="filterGo" runat="server" onserverclick="filterGo_OnServerClick">搜索</a>
                                                    </div>
                                                </div>


                                                <div class="tabClear"></div>
                                                <div class="tabContents" style="border-top: 2px solid #EFEFEF;">

                                                    <div class="tabContent">
                                                        <table width="575">
                                                        <asp:Repeater runat="server" ID="result">
                                                            <HeaderTemplate>
                                                              
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <tr style="border-bottom:1px dashed #F1F4F9;">
                                                                    
                                                                    <td>
														     <asp:Image runat="server" ImageUrl='<%# string.Format("../Image/img/{0}.jpg", Eval("Thumbnail")) %>' width="16" height="16" />
                                                                    </td>

                                                                    <td algin="left" height="25"> · <a href='<%# string.Format("../Go/{0}?Id={1}", ((Homory.Model.ResourceType)Eval("Type"))== Homory.Model.ResourceType.视频 ? "ViewVideo" : "ViewPlain", Eval("Id")) %>'><%# Eval("Title") %></a>
                                                                    </td>
                                                                    <td>
                                                                    </td>
                                                                    <td><em><%# ((DateTime)Eval("Time")).ToString("yyyy-MM-dd") %></em>
                                                                    </td>
                                                                    <td> <a id="del" runat="server" data-id='<%# Eval("Id") %>' onserverclick="del_ServerClick">删除</a>
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
                                        </telerik:RadAjaxPanel>
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













