<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Personal.aspx.cs" Inherits="Go.GoPersonal" EnableEventValidation="false" %>

<%@ Import Namespace="Homory.Model" %>
<%@ Import Namespace="System.Web.Configuration" %>

<%@ Register Src="~/Control/CommonTop.ascx" TagPrefix="homory" TagName="CommonTop" %>
<%@ Register Src="~/Control/CommonBottom.ascx" TagPrefix="homory" TagName="CommonBottom" %>
<%@ Register Src="~/Control/PersonalActionPersonal.ascx" TagPrefix="homory" TagName="PersonalActionPersonal" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <title>资源平台 - 个人首页</title>
    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8">
    <meta http-equiv="Pragma" content="no-cache">
    <script src="../Script/jquery.min.js"></script>
        <base target="_top" />

    <link rel="stylesheet" href="../Style/common.css">
    <link rel="stylesheet" href="../Style/common(1).css">
    <link rel="stylesheet" href="../Style/common(2).css">
    <link rel="stylesheet" href="../Style/index.css">
    <link rel="stylesheet" href="../Style/2.css" id="skinCss">
    <link href="../Style/center.css" rel="stylesheet" />
    <style>
        .hGrid tr td {
            padding: 2px;
            font-size: 14px;
        }

        .flList_ex ul li {
            width: auto;
        }
    </style>
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
        <div class="srx-bg1">
            <div class="srx-wrap">
                <div class="srx-main">
                    <div class="user-box">
                        <div class="user-frame-bg">
                            <div class="user-frame-bg1"></div>
                            <div class="user-frame">
                                <div class="user-avatar">
                                    <asp:Image runat="server" ID="icon" Width="180" Height="180" />
                                </div>
                                <telerik:RadAjaxPanel runat="server" ID="countPanel" OnAjaxRequest="countPanel_AjaxRequest">
                                    <ul>
                                        <li>
                                            <a href="../Go/CenterAttend">
                                                <asp:Label runat="server" ID="count1"></asp:Label></a>
                                            <em>关注</em>
                                        </li>
                                        <li>
                                            <a href="../Go/CenterAttend">
                                                <asp:Label runat="server" ID="count2"></asp:Label>
                                            </a>
                                            <em>被关注</em>
                                        </li>
                                        <li>
                                            <a href="../Go/CenterFavor">
                                                <asp:Label runat="server" ID="count3"></asp:Label></a>
                                            <em>收藏</em>
                                        </li>
                                    </ul>
                                </telerik:RadAjaxPanel>
                            </div>
                        </div>
                        <div class="user-desc">
                            <h3>
                                <asp:Label runat="server" ID="name"></asp:Label>
                                <a class="icon16 icon16-v-person"></a>
                            </h3>
                            <p class="user-des-line">
                                <span id="saying" runat="server" style="width: 500px;"></span>
                            </p>
                            <div class="profile-top-bar">
                                <telerik:RadAjaxPanel runat="server">
                                    <telerik:RadButton ID="h_fav" runat="server" Width="100" Skin="Metro" BackColor="#227DC5" Height="30" ForeColor="White" Checked="true" OnClick="h_fav_Click">
                                    </telerik:RadButton>
                                    &nbsp;
                                </telerik:RadAjaxPanel>
                            </div>
                        </div>
                        <div class="user-v">
                            <telerik:RadAjaxPanel runat="server">
                                <table style="margin-top: auto; margin-right: auto; margin-bottom: auto; margin-left: auto; font-size: 16px; text-align: center;">
                                    <tr>
                                        <td style="padding-top: 10px; padding-right: 10px; padding-bottom: 0px; padding-left: 10px;">
                                            <a>
                                                <asp:Label runat="server" ID="Label1"></asp:Label></a>
                                        </td>
                                        <td>&nbsp;&nbsp;&nbsp;&nbsp;</td>
                                        <td style="padding-top: 10px; padding-right: 10px; padding-bottom: 0px; padding-left: 10px;">
                                            <a>
                                                <asp:Label runat="server" ID="Label2"></asp:Label>
                                            </a>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="padding-top: 10px; padding-right: 10px; padding-bottom: 10px; padding-left: 10px;">
                                            <em>视频</em>
                                        </td>
                                        <td>&nbsp;&nbsp;&nbsp;&nbsp;</td>
                                        <td style="padding-top: 10px; padding-right: 10px; padding-bottom: 10px; padding-left: 10px;">
                                            <em>文章</em>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="padding-top: 10px; padding-right: 10px; padding-bottom: 0px; padding-left: 10px;">
                                            <a>
                                                <asp:Label runat="server" ID="Label3"></asp:Label></a>
                                        </td>
                                        <td>&nbsp;&nbsp;&nbsp;&nbsp;</td>
                                        <td style="padding-top: 10px; padding-right: 10px; padding-bottom: 0px; padding-left: 10px;">
                                            <a>
                                                <asp:Label runat="server" ID="Label4"></asp:Label></a>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="padding-top: 10px; padding-right: 10px; padding-bottom: 10px; padding-left: 10px;">
                                            <em>课件</em>
                                        </td>
                                        <td>&nbsp;&nbsp;&nbsp;&nbsp;</td>
                                        <td style="padding-top: 10px; padding-right: 10px; padding-bottom: 10px; padding-left: 10px;">
                                            <em>试卷</em>
                                        </td>
                                    </tr>
                                </table>
                            </telerik:RadAjaxPanel>
                        </div>
                    </div>
                    <div class="content-box clearfix">
                        <div class="feed-box clearfix mt10">
                            <div class="fd-list" id="feedList">
                                <div class="msgFeed user_feed mt15">
                                    <div style="background-color: #FFF; margin-top: 8px;">
                                        <div id="tabA" class="tabControl" style="width: 650px; height: auto; min-height: 280px; float: left; background-color: #FFF">
                                            <div class="box doing">
                                                <div style="width: 600px; margin: auto;">
                                                    <div class="tabs">
                                                        <span name="tabTit">
                                                            <div class="tab" style="width: 20%; height: 30px; line-height: 30px;">资源互动</div>
                                                            <div class="tab" style="width: 20%; height: 30px; line-height: 30px;">视频资源</div>
                                                            <div class="tab" style="width: 20%; height: 30px; line-height: 30px;">文章资源</div>
                                                            <div class="tab" style="width: 20%; height: 30px; line-height: 30px;">课件资源</div>
                                                            <div class="tab" style="width: 20%; height: 30px; line-height: 30px;">试卷资源</div>
                                                        </span>

                                                    </div>
													

                                                    <div class="tabClear"></div>

                                                    <div class="tabContents" style="border-top: 2px solid #EFEFEF;">
	                                                    <br/>

                                                        <div class="tabContent">
                                                            <div class="fd-list" id="feedList" style="border: none;">
                                                                    <homory:PersonalActionPersonal runat="server" ID="PersonalActionPersonal" />
                                                            </div>
                                                        </div>
                                                        <div class="tabContent">
                                                            <telerik:RadAjaxPanel runat="server">
                                                                <telerik:RadListView runat="server" ID="result1" AllowPaging="true" PageSize="20" OnNeedDataSource="result1_NeedDataSource" ItemPlaceholderID="holder1">
                                                                    <LayoutTemplate>
                                                                        <table class="hGrid" style="width: 600px;">
                                                                            <asp:PlaceHolder ID="holder1" runat="server"></asp:PlaceHolder>
                                                                        </table>
                                                                    </LayoutTemplate>
                                                                    <ItemTemplate>
                                                                        <tr>
                                                                            <td rowspan="2">
                                                                                <img src='<%# string.Format("../Image/img/{0}.png", Eval("Thumbnail")) %>' width="48" height="48" />
                                                                            </td>
                                                                            <td colspan="5">
                                                                                <a href='<%# string.Format("../Go/ViewVideo?Id={0}", Eval("Id")) %>' class="xy_zkyl"><%# Eval("Title") %> </a>
                                                                                <span style="float: right;"><em><%# ((DateTime)Eval("Time")).FormatTime() %></em></span>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>浏览：<%# Eval("View") %>
                                                                            </td>
                                                                            <td>收藏：<%# Eval("Favourite") %>
                                                                            </td>
                                                                            <td>评论：<%# Eval("Comment") %>
                                                                            </td>
                                                                            <td>下载：<%# Eval("Download") %>
                                                                            </td>
                                                                            <td><telerik:RadRating Skin="Default" ItemCount="5" Orientation="Horizontal" Precision="Item" runat="server" CssClass="flList_ex" AutoPostBack="false" Value='<%# Eval("Grade") %>' Enabled="false"></telerik:RadRating>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td colspan="6">
                                                                                <div style="width: 100%; border-bottom: dashed 1px #efefef;height: 1px; line-height: 1px;">&nbsp;</div>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td colspan="6">
                                                                                <div style="width: 100%; height: 30px; line-height: 30px;">&nbsp;</div>
                                                                            </td>
                                                                        </tr>
                                                                    </ItemTemplate>
                                                                </telerik:RadListView>
                                                                <br />
                                                                <telerik:RadDataPager runat="server" ID="pager1" PageSize="20" PagedControlID="result1">
                                                                    <Fields>
                                                                        <telerik:RadDataPagerSliderField HorizontalPosition="NoFloat" LabelTextFormat="第{0}页 共{1}页" SliderDecreaseText="前翻" SliderIncreaseText="后翻" SliderDragText="拖动" SliderOrientation="Horizontal" />
                                                                    </Fields>
                                                                </telerik:RadDataPager>
                                                            </telerik:RadAjaxPanel>
                                                        </div>
                                                        <div class="tabContent">
                                                            <telerik:RadAjaxPanel runat="server">
                                                                <telerik:RadListView runat="server" ID="result2" AllowPaging="true" PageSize="20" OnNeedDataSource="result2_NeedDataSource" ItemPlaceholderID="holder2">
                                                                    <LayoutTemplate>
                                                                        <table class="hGrid" style="width: 600px;">
                                                                            <asp:PlaceHolder ID="holder2" runat="server"></asp:PlaceHolder>
                                                                        </table>
                                                                    </LayoutTemplate>
                                                                    <ItemTemplate>
                                                                        <tr>
                                                                            <td rowspan="2">
                                                                                <img src='<%# string.Format("../Image/img/{0}.png", Eval("Thumbnail")) %>' width="48" height="48" />
                                                                            </td>
                                                                            <td colspan="5">
                                                                                <a href='<%# string.Format("../Go/ViewPlain?Id={0}", Eval("Id")) %>' class="xy_zkyl"><%# Eval("Title") %> </a>
                                                                                <span style="float: right;"><em><%# ((DateTime)Eval("Time")).FormatTime() %></em></span>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>浏览：<%# Eval("View") %>
                                                                            </td>
                                                                            <td>收藏：<%# Eval("Favourite") %>
                                                                            </td>
                                                                            <td>评论：<%# Eval("Comment") %>
                                                                            </td>
                                                                            <td>下载：<%# Eval("Download") %>
                                                                            </td>
                                                                            <td><telerik:RadRating Skin="Default" ItemCount="5" Orientation="Horizontal" Precision="Item" runat="server" CssClass="flList_ex" AutoPostBack="false" Value='<%# Eval("Grade") %>' Enabled="false"></telerik:RadRating>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td colspan="6">
                                                                                <div style="width: 100%; border-bottom: dashed 1px #efefef;height: 1px; line-height: 1px;">&nbsp;</div>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td colspan="6">
                                                                                <div style="width: 100%; height: 30px; line-height: 30px;">&nbsp;</div>
                                                                            </td>
                                                                        </tr>
                                                                    </ItemTemplate>
                                                                </telerik:RadListView>
                                                                <br />
                                                                <telerik:RadDataPager runat="server" ID="pager2" PageSize="20" PagedControlID="result2">
                                                                    <Fields>
                                                                        <telerik:RadDataPagerSliderField HorizontalPosition="NoFloat" LabelTextFormat="第{0}页 共{1}页" SliderDecreaseText="前翻" SliderIncreaseText="后翻" SliderDragText="拖动" SliderOrientation="Horizontal" />
                                                                    </Fields>
                                                                </telerik:RadDataPager>
                                                            </telerik:RadAjaxPanel>
                                                        </div>
                                                        <div class="tabContent">
                                                            <telerik:RadAjaxPanel runat="server">
                                                                <telerik:RadListView runat="server" ID="result3" AllowPaging="true" PageSize="20" OnNeedDataSource="result3_NeedDataSource" ItemPlaceholderID="holder3">
                                                                    <LayoutTemplate>
                                                                        <table class="hGrid" style="width: 600px;">
                                                                            <asp:PlaceHolder ID="holder3" runat="server"></asp:PlaceHolder>
                                                                        </table>
                                                                    </LayoutTemplate>
                                                                    <ItemTemplate>
                                                                        <tr>
                                                                            <td rowspan="2">
                                                                                <img src='<%# string.Format("../Image/img/{0}.png", Eval("Thumbnail")) %>' width="48" height="48" />
                                                                            </td>
                                                                            <td colspan="5">
                                                                                <a href='<%# string.Format("../Go/ViewPlain?Id={0}", Eval("Id")) %>' class="xy_zkyl"><%# Eval("Title") %> </a>
                                                                                <span style="float: right;"><em><%# ((DateTime)Eval("Time")).FormatTime() %></em></span>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>浏览：<%# Eval("View") %>
                                                                            </td>
                                                                            <td>收藏：<%# Eval("Favourite") %>
                                                                            </td>
                                                                            <td>评论：<%# Eval("Comment") %>
                                                                            </td>
                                                                            <td>下载：<%# Eval("Download") %>
                                                                            </td>
                                                                            <td><telerik:RadRating Skin="Default" ItemCount="5" Orientation="Horizontal" Precision="Item" runat="server" CssClass="flList_ex" AutoPostBack="false" Value='<%# Eval("Grade") %>' Enabled="false"></telerik:RadRating>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td colspan="6">
                                                                                <div style="width: 100%; border-bottom: dashed 1px #efefef;height: 1px; line-height: 1px;">&nbsp;</div>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td colspan="6">
                                                                                <div style="width: 100%; height: 30px; line-height: 30px;">&nbsp;</div>
                                                                            </td>
                                                                        </tr>
                                                                    </ItemTemplate>
                                                                </telerik:RadListView>
                                                                <br />
                                                                <telerik:RadDataPager runat="server" ID="pager3" PageSize="20" PagedControlID="result3">
                                                                    <Fields>
                                                                        <telerik:RadDataPagerSliderField HorizontalPosition="NoFloat" LabelTextFormat="第{0}页 共{1}页" SliderDecreaseText="前翻" SliderIncreaseText="后翻" SliderDragText="拖动" SliderOrientation="Horizontal" />
                                                                    </Fields>
                                                                </telerik:RadDataPager>
                                                            </telerik:RadAjaxPanel>
                                                        </div>
                                                        <div class="tabContent">
                                                            <telerik:RadAjaxPanel runat="server">
                                                                <telerik:RadListView runat="server" ID="result4" AllowPaging="true" PageSize="20" OnNeedDataSource="result4_NeedDataSource" ItemPlaceholderID="holder4">
                                                                    <LayoutTemplate>
                                                                        <table class="hGrid" style="width: 600px;">
                                                                            <asp:PlaceHolder ID="holder4" runat="server"></asp:PlaceHolder>
                                                                        </table>
                                                                    </LayoutTemplate>
                                                                    <ItemTemplate>
                                                                        <tr>
                                                                            <td rowspan="2">
                                                                                <img src='<%# string.Format("../Image/img/{0}.png", Eval("Thumbnail")) %>' width="48" height="48" />
                                                                            </td>
                                                                            <td colspan="5">
                                                                                <a href='<%# string.Format("../Go/ViewPlain?Id={0}", Eval("Id")) %>' class="xy_zkyl"><%# Eval("Title") %> </a>
                                                                                <span style="float: right;"><em><%# ((DateTime)Eval("Time")).FormatTime() %></em></span>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>浏览：<%# Eval("View") %>
                                                                            </td>
                                                                            <td>收藏：<%# Eval("Favourite") %>
                                                                            </td>
                                                                            <td>评论：<%# Eval("Comment") %>
                                                                            </td>
                                                                            <td>下载：<%# Eval("Download") %>
                                                                            </td>
                                                                            <td><telerik:RadRating Skin="Default" ItemCount="5" Orientation="Horizontal" Precision="Item" runat="server" CssClass="flList_ex" AutoPostBack="false" Value='<%# Eval("Grade") %>' Enabled="false"></telerik:RadRating>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td colspan="6">
                                                                                <div style="width: 100%; border-bottom: dashed 1px #efefef;height: 1px; line-height: 1px;">&nbsp;</div>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td colspan="6">
                                                                                <div style="width: 100%; height: 30px; line-height: 30px;">&nbsp;</div>
                                                                            </td>
                                                                        </tr>
                                                                    </ItemTemplate>
                                                                </telerik:RadListView>

                                                                <br />
                                                                <telerik:RadDataPager runat="server" ID="pager4" PageSize="20" PagedControlID="result4">
                                                                    <Fields>
                                                                        <telerik:RadDataPagerSliderField HorizontalPosition="NoFloat" LabelTextFormat="第{0}页 共{1}页" SliderDecreaseText="前翻" SliderIncreaseText="后翻" SliderDragText="拖动" SliderOrientation="Horizontal" />
                                                                    </Fields>
                                                                </telerik:RadDataPager>
                                                            </telerik:RadAjaxPanel>
                                                        </div>
                                                    </div>

                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <script src="../Script/index.js"></script>
                                </div>
                            </div>
                        </div>
                        <div class="right-box clearfix" id="rightBox">

                            <div class="right-item mb15">
                            </div>

                            <div style="clear: both; height: 10px"></div>
                            <div id="tabH" class="tabControl" style="width: 250px; height: 290px; background-color: #FFF">

                                <div class="box doing">
                                    <div style="width: 240px; margin: auto; height: 30px;">
                                        <div class="tabs" style="margin-left: 8px">

                                            <div class="tab" style="margin-top: -5px; margin-left: -1px;">最新</div>
                                            <div class="tab" style="margin-top: -5px; margin-left: -1px;">最热</div>
                                            <div class="tab" style="margin-top: -5px; margin-left: -1px;">最优</div>
                                        </div>

                                        <div class="tabClear"></div>
                                        <div class="tabContents">
                                            <div class="tabContent">
                                                <ul class="mb10 mt10">
                                                    <asp:Repeater runat="server" ID="latest">
                                                        <ItemTemplate>
                                                            <li style="clear: both;"><a style="float: left; margin-left: 20px; color: #227dc5;" href='<%# string.Format("../Go/{1}?Id={0}", Eval("Id"), ((Homory.Model.ResourceType)Eval("Type")) == Homory.Model.ResourceType.视频 ? "ViewVideo" : "ViewPlain") %>'><%# Eval("Title").ToString().CutString(9) %></a><label style="float: right; margin-right: 50px; color: #555;"><%# ((System.DateTime)Eval("Time")).FormatTimeShort() %>&nbsp;&nbsp;</label></li>
                                                        </ItemTemplate>
                                                    </asp:Repeater>
                                                </ul>
                                            </div>
                                            <div class="tabContent">
                                                <ul class="mb10 mt10">
                                                    <asp:Repeater runat="server" ID="popular">
                                                        <ItemTemplate>
                                                            <li style="clear: both;"><a style="float: left; margin-left: 20px; color: #227dc5;" href='<%# string.Format("../Go/{1}?Id={0}", Eval("Id"), ((Homory.Model.ResourceType)Eval("Type")) == Homory.Model.ResourceType.视频 ? "ViewVideo" : "ViewPlain") %>'><%# Eval("Title").ToString().CutString(9) %></a><label style="float: right; margin-right: 50px; color: #555;"><%# Eval("View") %>次&nbsp;&nbsp;</label></li>
                                                        </ItemTemplate>
                                                    </asp:Repeater>
                                                </ul>
                                            </div>
                                            <div class="tabContent">
                                                <ul class="mb10 mt10">
                                                    <asp:Repeater runat="server" ID="best">
                                                        <ItemTemplate>
                                                            <li style="clear: both;"><a style="float: left; margin-left: 20px; color: #227dc5;" href='<%# string.Format("../Go/{1}?Id={0}", Eval("Id"), ((Homory.Model.ResourceType)Eval("Type")) == Homory.Model.ResourceType.视频 ? "ViewVideo" : "ViewPlain") %>'><%# Eval("Title").ToString().CutString(9) %></a><label style="float: right; margin-right: 50px; color: #555;"><%# Eval("Grade") %>分&nbsp;&nbsp;</label></li>
                                                        </ItemTemplate>
                                                    </asp:Repeater>
                                                </ul>
                                            </div>

                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="r-visitors">
                                <div class="rv-title clearfix" style="width: 220px;">
                                    <h3 class="fl">最近访客</h3>

                                    <span class="rv-count fr">访问总数：<asp:Label runat="server" ID="viewCount"></asp:Label></span>

                                </div>
                                <div class="rv-box">
                                    <ul class="clearfix" style="margin-left: 15px">
                                        <asp:Repeater runat="server" ID="viewList">
                                            <ItemTemplate>
                                                <li style="margin: 4px; float: left;">
                                                    <a href='<%# string.Format("../Go/Personal?Id={0}", U(Eval("Id2")).Id) %>'>
                                                        <asp:Image runat="server" ID="icon" ImageUrl='<%# U(Eval("Id2")).Icon %>' class="face face_40" Height="40" Width="40" /></a><br />
                                                    <span class="rv-time" style="margin-left: 4px;"><%# ((DateTime)Eval("Time")).FormatTimeShort() %></span>
                                                </li>
                                            </ItemTemplate>
                                        </asp:Repeater>



                                    </ul>
                                </div>
                            </div>

                        </div>
                    </div>
                </div>
                <script src="../Script/index_click.js"></script>
                <homory:CommonBottom runat="server" ID="CommonBottom" />
            </div>
        </div>
    </form>
</body>
</html>
