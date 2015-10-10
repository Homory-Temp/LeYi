<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ViewStudioX.aspx.cs" Inherits="Go.GoViewStudioX" %>

<%@ Import Namespace="Homory.Model" %>
<%@ Register Src="~/Control/CommonTop.ascx" TagPrefix="homory" TagName="CommonTop" %>
<%@ Register Src="~/Control/CommonBottom.ascx" TagPrefix="homory" TagName="CommonBottom" %>


<!DOCTYPE html>

<html>
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8">
    <meta http-equiv="Pragma" content="no-cache">
    <title>互动资源平台 名师工作室</title>
    <link rel="stylesheet" href="css/common.css">
    <link rel="stylesheet" href="css/common(1).css">
    <link rel="stylesheet" href="css/detail.css">
    <link rel="stylesheet" href="css/plaza2.css">
    <link rel="stylesheet" href="css/1.css" id="skinCss">
    <base target="_top" />
</head>
<body class="srx-pclass">
    <form id="form1" runat="server">
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
                            <asp:Repeater runat="server" ID="catalogs" OnItemDataBound="catalogs_OnItemDataBound">
                                <ItemTemplate>
                                    <div class="center_right">
                                        <div class="c-p-title">
                                            <div class="box-hd"><%# Eval("Name") %></div>
                                            <a style="display: none;">+more</a>
                                        </div>
                                        <ul class="aList">
                                            <asp:Repeater runat="server" ID="resources">
                                                <ItemTemplate>
                                                    <li>
                                                        <img src='<%# string.Format("../Image/img/{0}.jpg", Eval("Thumbnail")) %>' style="float: left; width: 20px; height: 20px; margin-top: 6px; margin-right: 20px;" />
                                                        <strong style="width: 550px;"><a href='<%# string.Format("../Go/{0}?Id={1}", ((Homory.Model.ResourceType)Eval("Type"))== Homory.Model.ResourceType.视频 ? "ViewVideo" : "ViewPlain", Eval("Id")) %>'><%# Eval("Title") %></a></strong>
                                                        <span style="float:right;"><%# Eval("Time") %></span>
                                                        <div style="clear:both;"></div>
                                                    </li>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                        </ul>
                                        <br /><br />
                                    </div>
                                    
                                </ItemTemplate>
                            </asp:Repeater>
                        </div>
                        <div class="c-p-right fl">
                            <div class="c-pr-box">
                                <div class="c-prb-role clearfix">
                                    <div class="c-prb-title ">
                                        <div class="box-hd">团队简介 </div>
                                    </div>
                                    <div class="l_schPrincipal">
                                        <p id="introduction" runat="server">
                                        </p>


                                    </div>
                                </div>
                                <div class="c-prb-role clearfix">
                                    <div class="c-prb-title ">
                                        <div class="box-hd">团队引领 </div>
                                    </div>
                                    <ul id="role_1" data-count="7">
                                        <asp:Repeater runat="server" ID="leader">
                                            <ItemTemplate>
                                                <li class="none  first-child" style="display: list-item; height: 50px;">
                                                    <div class="c-prb-face">
                                                        <a style="border: none;" href='<%# string.Format("../Go/Personal?Id={0}", Eval("Id")) %>'>
                                                            <asp:Image runat="server" ID="icon" ImageUrl='<%# P(Eval("Icon")) %>' Width="40" Height="40" /></a>
                                                    </div>
                                                    <div class="ml50">
                                                        <div class="c-prb-nickname"><a href='<%# string.Format("../Go/Personal?Id={0}", Eval("Id")) %>'><%# Eval("DisplayName") %></a> </div>
                                                        <div style="display: none;"><a href="javascript:;" class="unflw" data-id="24760" data-type="person" data-action="follow"><strong>×</strong>删除</a> </div>
                                                    </div>
                                                </li>
                                                <a style="display: none;">删除</a>
                                                <a style="display: none;">设为管理员</a>
                                                <a style="display: none;">关注</a>
                                                <a style="display: none;">取消关注</a>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </ul>
                                </div>
                                <div class="c-prb-role clearfix">
                                    <div class="c-prb-title mb10">
                                        <div class="box-hd">团队成员 </div>
                                    </div>
                                    <ul id="role_0" data-count="7">
                                        <asp:Repeater runat="server" ID="members">
                                            <ItemTemplate>
                                                <li class="none  first-child" style="display: list-item; height: 50px;">
                                                    <div class="c-prb-face">
                                                        <a style="border: none;" href='<%# string.Format("../Go/Personal?Id={0}", Eval("Id")) %>'>
                                                            <asp:Image runat="server" ID="icon" ImageUrl='<%# P(Eval("Icon")) %>' Width="40" Height="40" /></a>
                                                    </div>
                                                    <div class="ml50">
                                                        <div class="c-prb-nickname"><a href='<%# string.Format("../Go/Personal?Id={0}", Eval("Id")) %>'><%# Eval("DisplayName") %></a> </div>
                                                        <div style="display: none;"><a href="javascript:;" class="unflw" data-id="24760" data-type="person" data-action="follow"><strong>×</strong>删除</a> </div>
                                                    </div>
                                                </li>
                                                <a style="display: none;">删除</a>
                                                <a style="display: none;">设为管理员</a>
                                                <a style="display: none;">关注</a>
                                                <a style="display: none;">取消关注</a>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </ul>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <homory:CommonBottom runat="server" ID="CommonBottom" />
            </div>
        </div>
        <script src="js/h.js" type="text/javascript"></script>
    </form>
</body>
</html>
