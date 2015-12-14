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
    <script>
        function MouseOver(id) {
            var sourceId = $("#" + id);
            var stick = sourceId.attr("Stick");
            if (stick == "0") {
                var url = "Image/upr.png";
                sourceId.attr("src", url);
            }
            else {
                url = "Image/downr.png";
                sourceId.attr("src", url);
            }
        }

        function MouseOut(id) {
            var sourceId = $("#" + id);
            var stick = sourceId.attr("Stick");
            if (stick == "0") {
                url = "Image/uph.png";
                sourceId.attr("src", url);
            }
            else {
                url = "Image/downh.png";
                sourceId.attr("src", url);
            }
        }
    </script>
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
                            <a class="srx-ns-btn" runat="server" id="search_go" onserverclick="search_go_OnServerClick" style="border-right: solid 1px silver;">检索</a>
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
                                    <span>资源检索</span>
                                </div>
                                <div class="xy_ltit mgtb10">
                                    &nbsp;
                                <a class="xy_zksq" style="background-image: none;">共<dfn><label id="total" runat="server">0</label></dfn>份资源</a>
                                </div>
                                <div class="xy_sort">
                                    <table style="margin: 10px 20px; clear: both; width: 90%;">
                                        <tr>
                                            <th>日期：</th>
                                            <td>
                                                <telerik:RadMonthYearPicker ID="from" runat="server" LocalizationPath="~/Language" ShowPopupOnFocus="true" AutoPostBack="true" Width="80" DateInput-DisplayDateFormat="yyyy年MM月" DateInput-DateFormat="yyyy年MM月">
                                                    <DatePopupButton runat="server" Visible="false" />
                                                    <MonthYearNavigationSettings OkButtonCaption="确认" CancelButtonCaption="取消" TodayButtonCaption="今日" />
                                                </telerik:RadMonthYearPicker>
                                            </td>
                                            <th>&nbsp;-&nbsp;</th>
                                            <td>
                                                <telerik:RadMonthYearPicker ID="to" runat="server" LocalizationPath="~/Language" ShowPopupOnFocus="true" AutoPostBack="true" Width="80" DateInput-DisplayDateFormat="yyyy年MM月" DateInput-DateFormat="yyyy年MM月">
                                                    <DatePopupButton runat="server" Visible="false" />
                                                    <MonthYearNavigationSettings OkButtonCaption="确认" CancelButtonCaption="取消" TodayButtonCaption="今日" />
                                                </telerik:RadMonthYearPicker>
                                            </td>
                                            <th>&nbsp;&nbsp;&nbsp;&nbsp;</th>
                                            <th>适用年龄段：</th>
                                            <td>
                                                <telerik:RadButton ID="a1" runat="server" Text="通用" Value="A3757840-9DF7-4370-8151-FAD39B44EF6A" ToggleType="CheckBox" Width="60" Checked="true" Style="margin-right: 4px;"></telerik:RadButton>
                                                <telerik:RadButton ID="a2" runat="server" Text="大班" Value="625AE587-8C5A-454B-893C-08D2F6D187D5" ToggleType="CheckBox" Width="60" Checked="true" Style="margin-right: 4px;"></telerik:RadButton>
                                                <telerik:RadButton ID="a3" runat="server" Text="中班" Value="CF3AE587-8CB9-4D0A-B29A-08D2F6D187D9" ToggleType="CheckBox" Width="60" Checked="true" Style="margin-right: 4px;"></telerik:RadButton>
                                                <telerik:RadButton ID="a4" runat="server" Text="小班" Value="9FD9E587-8C09-4A55-9DB0-08D2F6D187DD" ToggleType="CheckBox" Width="60" Checked="true" Style="margin-right: 4px;"></telerik:RadButton>
                                                <telerik:RadButton ID="a5" runat="server" Text="托班" Value="850557E1-9EBD-4E0D-93DC-FE090A77D393" ToggleType="CheckBox" Width="60" Checked="true"></telerik:RadButton>
                                            </td>
                                            <th>&nbsp;&nbsp;&nbsp;&nbsp;</th>
                                            <th>发布人：</th>
                                            <td>
                                                <telerik:RadSearchBox ID="publisher" runat="server" ShowSearchButton="false" Width="80"></telerik:RadSearchBox>
                                            </td>
                                        </tr>
                                    </table>
                                    <div>
                                    </div>
                                </div>
                                    <div id="SourceLi" runat="server"  class="fl xy_treebox mgt15" >
                                        <div class="zTreeDemoBackground2 left">
                                            <telerik:RadTreeView runat="server" ID="tree" CheckBoxes="true" CheckChildNodes="true" OnNodeCheck="commentList_NodeCheck" ExpandNodeOnSingleClick="true" AutoPostBack="true" DataTextField="Name" DataValueField="Id" DataFieldID="Id" DataFieldParentID="ParentId"></telerik:RadTreeView>
                                        </div>
                                    </div>
                                    <div  class="mgt15 fr" style="width:770px;" >       
                                    <div class="xy_pxbar" style="height: 40px; line-height: 40px;">
                                        <p>
                                            <telerik:RadButton Width="80" runat="server" ID="s1" OnClick="itemX_OnClick" Text="最新" Value='1' ToggleType="CheckBox" Checked="true" Style="margin-left: 10px; margin-right: 10px;"></telerik:RadButton>
                                            <telerik:RadButton Width="80" runat="server" ID="s2" OnClick="itemX_OnClick" Text="最热" Value='2' ToggleType="CheckBox" Style="margin-left: 10px; margin-right: 10px;"></telerik:RadButton>
                                            <telerik:RadButton Width="80" runat="server" ID="s3" OnClick="itemX_OnClick" Text="最优" Value='3' ToggleType="CheckBox" Style="margin-left: 10px; margin-right: 10px;"></telerik:RadButton>
                                        </p>
                                    </div>
                                    <div class="xy_allzylist">
                                        <telerik:RadListView runat="server" ID="result" AllowPaging="true" PageSize="20" OnNeedDataSource="result_NeedDataSource" OnItemDataBound="result_ItemDataBound">
                                            <ItemTemplate>
                                                <dl>
                                                    <dt>
                                                        <img src='<%# string.Format("../Image/img/{0}.png", Eval("Thumbnail")) %>' width="64" height="64" />
                                                        <div>
                                                            <p><%#(int)Eval("Stick") == 0?string.Empty:"<span style='font-weight:bolder; color:#ca0e0e;font-size:18px;'>[置顶]</span>" %>&nbsp;<a limit="30" href='<%# string.Format("../Go/{0}?Id={1}", ((Homory.Model.ResourceType)Eval("Type"))== Homory.Model.ResourceType.视频 ? "ViewVideo" : "ViewPlain", Eval("Id")) %>' class="xy_zkyl"><%# Eval("Title") %> </a></p>

                                                            <p class="sd wjdx">
                                                                <span><%# UC(Eval("UserId")) %>&nbsp;<a href='<%# string.Format("../Go/Personal?Id={0}", Eval("UserId")) %>'><em><%# U(((Homory.Model.ResourceMap)Container.DataItem).UserId).RealName %></em></a></span><span>发布时间：<em> <%# ((DateTime)Eval("Time")).ToString("yyyy-MM-dd HH:mm") %></em></span>
                                                            </p>
                                                        </div>
                                                    </dt>
                                                    <dd>
                                                        <ul>
                                                            <li>
                                                                <p id="grade" class="grade">
                                                                    <telerik:RadRating Skin="Default" ItemCount="5" Orientation="Horizontal" Precision="Item" runat="server" CssClass="flList_ex" AutoPostBack="false" Value='<%# Eval("Grade") %>' Enabled="false"></telerik:RadRating>
                                                                    (<%# Eval("Rate") %>次评分)
                                                                </p>
                                                            </li>
                                                            <li><a class="xy_scbtn">审核</a></li>
                                                            <li><a target="_blank" href='<%# string.Format("../Go/{0}?Id={1}", ((Homory.Model.ResourceType)Eval("Type"))== Homory.Model.ResourceType.视频 ? "ViewVideo" : "ViewPlain", Eval("Id")) %>' class="xy_xzbtn">浏览(<%# Eval("View") %>)</a></li>
                                                            <li><a target="_blank" href='<%# string.Format("../Go/{0}?Id={1}", ((Homory.Model.ResourceType)Eval("Type"))== Homory.Model.ResourceType.视频 ? "ViewVideo" : "ViewPlain", Eval("Id")) %>' class="xy_xzbtn">下载(<%# Eval("Download") %>)</a></li>
                                                            <li><asp:ImageButton ID="SetTop" class="SetTopClass" Visible='<%# ((Homory.Model.ResourceMap)Container.DataItem).AuditUsers.ToUpper().Contains(CurrentUser.Id.ToString().ToUpper()) %>' Stick='<%#Eval("Stick") %>' style="margin-top:16px;" ImageUrl='<%#(int)Eval("Stick")== 0?"Image/uph.png":"Image/downh.png"%>' ToolTip='<%#(int)Eval("Stick") == 0? "置顶":"取消置顶" %>'  runat="server"  SourceId='<%#Eval("Id")%>' OnClick="SetTop_Click" /></li>
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
    <style>
        .xy_sort span {
            width: auto;
            margin: 0;
        }
    </style>
</body>
</html>
