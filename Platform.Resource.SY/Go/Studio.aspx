<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Studio.aspx.cs" Inherits="Go.GoStudio" %>

<%@ Register Src="~/Control/HomeTop.ascx" TagPrefix="homory" TagName="HomeTop" %>
<%@ Register Src="~/Control/CommonBottom.ascx" TagPrefix="homory" TagName="CommonBottom" %>
<%@ Register Src="~/Control/CommonTop.ascx" TagPrefix="homory" TagName="CommonTop" %>
<%@ Import Namespace="Homory.Model" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8">
    <meta http-equiv="Pragma" content="no-cache">
    <title>资源平台 - 名师工作室</title>
    <link rel="stylesheet" href="css/common.css">
    <link rel="stylesheet" href="css/common(1).css">
    <link rel="stylesheet" href="css/detail.css">
    <link rel="stylesheet" href="css/base.min.css">
    <link rel="stylesheet" href="css/plaza2.css">
    <script src="js/jquery.min.js"></script>
    <link rel="stylesheet" href="css/1.css" id="skinCss">
    <base target="_top" />
</head>
<body class="srx-pclass">
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
                <div class="c-plaza" id="cPlaza">
                    <div class="c-p-content clearfix">
                        <div class="c-pc-left fl">
                            <div class="mstop">
                            </div>
                            <div class="webcinema cl">
                                <ul class="webcnm_list">
                                    <asp:Repeater runat="server" ID="studioList">
                                        <ItemTemplate>
                                            <li class="c-p-box1"><span class="payTag"></span><span class="titleMask"><a href='<%# string.Format("../Go/ViewStudio?Id={0}", Eval("Id")) %>'><%# Eval("Name") %></a></span> <a href='<%# string.Format("../Go/ViewStudio?Id={0}", Eval("Id")) %>'>
                                                <div class="loadmask" style="position: absolute; width: 150px; height: 150px; display: none;"></div>
                                                <asp:Image Width="150" Height="150" Style="display: inline;" runat="server" ImageUrl='<%# P(Eval("Icon")) %>' />
                                            </a></li>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </ul>
                            </div>
                        </div>
                        <div class="c-p-right fl">
                            <div class="c-pr-box">
                                <div class="r_topic mt20">
                                    <div class="c-prb-title mb5 ml10">
                                        <div class="box-hd">评论排行 </div>
                                    </div>
                                    <ul class="hot_topic">
                                        <asp:Repeater ID="studioHonor" runat="server">
                                            <ItemTemplate>
                                                <li><span class="icon_order1">
                                                    <img src='<%# string.Format("../Image/honor/{0}.jpg", Container.ItemIndex) %>' width="20" height="34"></span> <span class="topic_title"><a class="fl" href='<%# string.Format("../Go/ViewPlain?Id={0}", Eval("Id")) %>'><%# Eval("Title") %></a> </span><span class="topic_author">评论数：<a><%# Eval("View") %></a></span> </li>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </ul>
                                </div>
                                <div class="cl"></div>
                                <div id="tabA" class="tabControl" style="width: 200px; height: 290px; float: left; background-color: #FFF">

                                    <div class="box doing">
                                        <div style="width: 200px; margin: auto; height: 30px;">
                                            <div class="tabs" style="margin-left: 8px">

                                                <div class="tab" style="margin-top: -5px; margin-left: -1px;">最新</div>
                                                <div class="tab" style="margin-top: -5px; margin-left: -1px;">最热</div>
                                                <div class="tab" style="margin-top: -5px; margin-left: -1px;">最优</div>
                                            </div>

                                            <div class="tabClear"></div>
                                            <div class="tabContents">
                                                <div class="tabContent">
                                                    <ul class="mb10 mt10">
                                                        <asp:Repeater ID="latest" runat="server">
                                                            <ItemTemplate>
                                                                <li>
                                                                    <a href='<%# string.Format("../Go/ViewPlain?Id={0}", Eval("Id")) %>'><%# Eval("Title").ToString().CutString(10) %></a>
                                                                </li>
                                                            </ItemTemplate>
                                                        </asp:Repeater>
                                                    </ul>
                                                </div>
                                                <div class="tabContent">
                                                    <ul class="mb10 mt10">
                                                        <asp:Repeater ID="popular" runat="server">
                                                            <ItemTemplate>
                                                                <li>
                                                                    <a href='<%# string.Format("../Go/ViewPlain?Id={0}", Eval("Id")) %>'><%# Eval("Title").ToString().CutString(10) %></a>
                                                                </li>
                                                            </ItemTemplate>
                                                        </asp:Repeater>
                                                    </ul>
                                                </div>
                                                <div class="tabContent">
                                                    <ul class="mb10 mt10">
                                                        <asp:Repeater ID="best" runat="server">
                                                            <ItemTemplate>
                                                                <li>
                                                                    <a href='<%# string.Format("../Go/ViewPlain?Id={0}", Eval("Id")) %>'><%# Eval("Title").ToString().CutString(10) %></a>
                                                                </li>
                                                            </ItemTemplate>
                                                        </asp:Repeater>
                                                    </ul>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                        </div>
                    </div>
                </div>
                <homory:CommonBottom runat="server" ID="CommonBottom" />
            </div>
        </div>
        <script src="../Script/index.js"></script>
        <script src="js/h.js" type="text/javascript"></script>
    </form>
</body>
</html>
