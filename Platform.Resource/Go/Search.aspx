<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Search.aspx.cs" Inherits="Go.GoSearch" %>

<%@ Import Namespace="Homory.Model" %>

<%@ Register Src="~/Control/CommonBottom.ascx" TagPrefix="uc1" TagName="CommonBottom" %>
<!DOCTYPE html>
<html>
<head runat="server">
    <title>资源平台 - 检索</title>
    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8">
    <meta http-equiv="Pragma" content="no-cache">
    <script src="../Script/jquery.min.js"></script>
    <link href="../Style/common.css" rel="stylesheet" />
    <link href="../Style/login.css" rel="stylesheet" />
    <link href="../Style/plaza.css" rel="stylesheet" />
    <link href="../Style/public.css" rel="stylesheet" />
    <link href="../Style/mhzy.css" rel="stylesheet" />
    <link href="../Style/zTreeStyle2.css" rel="stylesheet" />
    <base target="_top" />
    <style>
        
        .flList_ex ul li {
            width: auto;
        }

    </style>
</head>
<body>
    <form runat="server">
        <telerik:RadScriptManager ID="Rsm" runat="server">
            <Scripts>
                <asp:ScriptReference Assembly="Telerik.Web.UI" Name="Telerik.Web.UI.Common.Core.js" />
                <asp:ScriptReference Assembly="Telerik.Web.UI" Name="Telerik.Web.UI.Common.jQuery.js" />
                <asp:ScriptReference Assembly="Telerik.Web.UI" Name="Telerik.Web.UI.Common.jQueryInclude.js" />
            </Scripts>
        </telerik:RadScriptManager>
        <div class="srx-bg22">
            <telerik:RadAjaxPanel runat="server" ID="panel">
                <div class="login" style="margin-top: 0px;">
                    <div class="lg-top">
                        <h1 class="logo"><a class="fixpng" href="../Go/Home" title="互动教育资源管理平台">互动教育资源管理平台</a></h1>
                        <div class="lg-search" style="width: 396px;">
                            <input runat="server" type="text" class="srx-ns-input" id="search_content" value="" data-prevcolor="" style="color: rgb(170, 170, 170);" />
                            <a class="srx-ns-btn" runat="server" id="search_go_inner" onserverclick="search_go_OnServerClick">检索校内</a>
                            <a class="srx-ns-btn" runat="server" id="search_go" onserverclick="search_go_OnServerClick" style="border-right: solid 1px silver;">检索全部</a>
                            <input id="hhhh" runat="server" type="hidden" value="1" />
                        </div>

                    </div>
                </div>
                <div class="srx-wrap11">
                    <div class="portalMain w1000 clearfix">
                        <div class="Main w1000 clearfix eduSource">
                            <div class="Main w960 clearfix">
                                <div class="xy_crumbs mgtb10">
                                    <a href="../Go/Home" class="h_icon"><em></em></a>
                                    <a href="../Go/Catalog">资源目录</a>
                                    <span>检索结果</span>

                                </div>
                                <div class="xy_ltit mgtb10">
                                    &nbsp;
                                <a class="xy_zksq" style="background-image: none;">共<dfn><label id="total" runat="server">0</label></dfn>份资源</a>
                                </div>
                                <div class="xy_sort">
                                    <ul>
                                        <li><span>学段：</span><p>
                                            <asp:Repeater runat="server" ID="period">
                                                <ItemTemplate>
                                                    <telerik:RadButton Width="80" runat="server" ID="period_item" OnClick="item0_OnClick" Text='<%# Container.DataItem.ToString().Replace("年级_", "") %>' Value='<%# (int)Container.DataItem %>' ToggleType="CheckBox" Checked="true" Style="margin-left: 10px; margin-right: 10px;">
                                                    </telerik:RadButton>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                        </p>
                                        </li>
                                        <li><span>年级：</span><p>
                                            <asp:Repeater runat="server" ID="grade">
                                                <ItemTemplate>
                                                    <telerik:RadButton Width="80" runat="server" ID="grade_item" OnClick="item2_OnClick" Text='<%# Eval("Name") %>' Value='<%# Eval("Id") %>' ToggleType="CheckBox" Checked="true" Style="margin-left: 10px; margin-right: 10px;"></telerik:RadButton>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                        </p>
                                        </li>
                                        <li><span>课程：</span><p>
                                            <asp:Repeater runat="server" ID="course">
                                                <ItemTemplate>
                                                    <telerik:RadButton Width="80" runat="server" ID="course_item" OnClick="item1_OnClick" Text='<%# Eval("Name") %>' Value='<%# Eval("Id") %>' ToggleType="CheckBox" Checked="true" Style="margin-left: 10px; margin-right: 10px;">
                                                    </telerik:RadButton>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                        </p>
                                        </li>
                                        <li><span>栏目：</span><p>
                                            <asp:Repeater runat="server" ID="catalog">
                                                <ItemTemplate>
                                                    <telerik:RadButton Width="80" runat="server" ID="catalog_item" OnClick="item3_OnClick" Text='<%# Eval("Name") %>' Value='<%# Eval("Id") %>' ToggleType="CheckBox" Checked="true" Style="margin-left: 10px; margin-right: 10px;"></telerik:RadButton>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                        </p>
                                        </li>
                                        <li style="border: 0px;"><span>类型：</span><p>
                                            <telerik:RadButton Width="80" runat="server" ID="t1" OnClick="item_OnClick" Text="视频" Value='1' ToggleType="CheckBox" Checked="true" Style="margin-left: 10px; margin-right: 10px;"></telerik:RadButton>
                                            <telerik:RadButton Width="80" runat="server" ID="t2" OnClick="item_OnClick" Text="文章" Value='2' ToggleType="CheckBox" Checked="true" Style="margin-left: 10px; margin-right: 10px;"></telerik:RadButton>
                                            <telerik:RadButton Width="80" runat="server" ID="t3" OnClick="item_OnClick" Text="课件" Value='3' ToggleType="CheckBox" Checked="true" Style="margin-left: 10px; margin-right: 10px;"></telerik:RadButton>
                                            <telerik:RadButton Width="80" runat="server" ID="t4" OnClick="item_OnClick" Text="试卷" Value='4' ToggleType="CheckBox" Checked="true" Style="margin-left: 10px; margin-right: 10px;"></telerik:RadButton>
                                        </p>
                                        </li>
                                    </ul>
                                </div>
                                <div class="mgt30">
                                    <div class="xy_pxbar mgt15" style="height: 40px; line-height: 40px;">
                                        <p>
                                            <telerik:RadButton Width="80" runat="server" ID="s1" OnClick="itemX_OnClick" Text="最新" Value='1' ToggleType="CheckBox" Checked="true" Style="margin-left: 10px; margin-right: 10px;"></telerik:RadButton>
                                            <telerik:RadButton Width="80" runat="server" ID="s2" OnClick="itemX_OnClick" Text="最热" Value='2' ToggleType="CheckBox" Style="margin-left: 10px; margin-right: 10px;"></telerik:RadButton>
                                            <telerik:RadButton Width="80" runat="server" ID="s3" OnClick="itemX_OnClick" Text="最优" Value='3' ToggleType="CheckBox" Style="margin-left: 10px; margin-right: 10px;"></telerik:RadButton>
                                            <telerik:RadButton Width="80" runat="server" ID="ss" OnClick="itemS_OnClick" Text="学习助手" ToggleType="CheckBox" Style="margin-left: 450px; margin-right: 10px;"></telerik:RadButton>
                                            <asp:Image ImageUrl="~/Go/css/s.png" runat="server" Width="28" Height="28" />
                                        </p>
                                    </div>
                                    <div class="xy_allzylist">
                                        <telerik:RadListView runat="server" ID="result" AllowPaging="true" PageSize="20" OnNeedDataSource="result_NeedDataSource">
                                            <ItemTemplate>
                                                <dl>
                                                    <dt>
                                                        <img src='<%# string.Format("../Image/img/{0}.png", Eval("Thumbnail")) %>' width="64" height="64" />
                                                        <div>
                                                            <p><a limit="30" href='<%# string.Format("../Go/{0}?Id={1}", ((Homory.Model.ResourceType)Eval("Type"))== Homory.Model.ResourceType.视频 ? "ViewVideo" : "ViewPlain", Eval("Id")) %>' class="xy_zkyl"><%# Eval("Title") %> </a></p>
                                                            <p class="sd wjdx">
                                                                <span><%# UC(Eval("UserId")) %>&nbsp;<a href='<%# string.Format("../Go/Personal?Id={0}", Eval("UserId")) %>'><em><%# ((Homory.Model.Resource)Container.DataItem).User.DisplayName %></em></a></span>
                                                                <span>发布时间：<em> <%# ((DateTime)Eval("Time")).ToString("yyyy-MM-dd HH:mm") %></em></span>
                                                            </p>
                                                        </div>
                                                    </dt>
                                                    <dd>
                                                        <ul>
                                                            <li>
                                                                <p id="grade" class="grade">
                                                                    <telerik:RadRating Skin="Default" ItemCount="5" Orientation="Horizontal" Precision="Item" runat="server" CssClass="flList_ex" AutoPostBack="false" Value='<%# Eval("Grade") %>' Enabled="false"></telerik:RadRating>(<%# Eval("Rate") %>次评分)
                                                                    
                                                                </p>
                                                            </li>
                                                            <li><a target="_blank" href='<%# string.Format("../Go/{0}?Id={1}", ((Homory.Model.ResourceType)Eval("Type"))== Homory.Model.ResourceType.视频 ? "ViewVideo" : "ViewPlain", Eval("Id")) %>' class="xy_scbtn">浏览(<%# Eval("View") %>)</a></li>
                                                            <li><a target="_blank" href='<%# string.Format("../Go/{0}?Id={1}", ((Homory.Model.ResourceType)Eval("Type"))== Homory.Model.ResourceType.视频 ? "ViewVideo" : "ViewPlain", Eval("Id")) %>' class="xy_xzbtn">下载(<%# Eval("Download") %>)</a></li>
                                                        </ul>
                                                    </dd>

                                                </dl>
                                            </ItemTemplate>
                                        </telerik:RadListView>
                                        <br />
                                        <telerik:RadDataPager runat="server" ID="pager" PageSize="20" PagedControlID="result">
                                            <Fields>
                                                <telerik:RadDataPagerSliderField HorizontalPosition="NoFloat" LabelTextFormat="第{0}页 共{1}页" SliderDecreaseText="前翻" SliderIncreaseText="后翻" SliderDragText="拖动" SliderOrientation="Horizontal" />
                                             </Fields>
                                        </telerik:RadDataPager>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <uc1:CommonBottom runat="server" ID="CommonBottom" />
                </div>
            </telerik:RadAjaxPanel>
        </div>
    </form>
</body>
</html>
