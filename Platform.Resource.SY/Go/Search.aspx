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
                        <div class="lg-search" style="width: 337px;">
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
                                                <telerik:RadMonthYearPicker ID="from" runat="server" LocalizationPath="~/Language" ShowPopupOnFocus="true" AutoPostBack="true" Width="80" DateInput-DisplayDateFormat="yyyy年MM月" DateInput-DateFormat="yyyy年MM月" OnSelectedDateChanged="from_SelectedDateChanged">
                                                    <DatePopupButton runat="server" Visible="false" />
                                                    <MonthYearNavigationSettings OkButtonCaption="确认" CancelButtonCaption="取消" TodayButtonCaption="今日" />
                                                </telerik:RadMonthYearPicker>
                                            </td>
                                            <th>&nbsp;-&nbsp;</th>
                                            <td>
                                                <telerik:RadMonthYearPicker ID="to" runat="server" LocalizationPath="~/Language" ShowPopupOnFocus="true" AutoPostBack="true" Width="80" DateInput-DisplayDateFormat="yyyy年MM月" DateInput-DateFormat="yyyy年MM月" OnSelectedDateChanged="from_SelectedDateChanged">
                                                    <DatePopupButton runat="server" Visible="false" />
                                                    <MonthYearNavigationSettings OkButtonCaption="确认" CancelButtonCaption="取消" TodayButtonCaption="今日" />
                                                </telerik:RadMonthYearPicker>
                                            </td>
                                            <th>&nbsp;&nbsp;&nbsp;&nbsp;</th>
                                            <th>适用年龄段：</th>
                                            <td>
                                                <telerik:RadButton ID="a1" runat="server" Text="通用" OnCheckedChanged="a_CheckedChanged" Value="A3757840-9DF7-4370-8151-FAD39B44EF6A" ToggleType="CheckBox" Width="60" Checked="true" Style="margin-right: 4px;"></telerik:RadButton>
                                                <telerik:RadButton ID="a2" runat="server" Text="大班" OnCheckedChanged="a_CheckedChanged" Value="625AE587-8C5A-454B-893C-08D2F6D187D5" ToggleType="CheckBox" Width="60" Checked="true" Style="margin-right: 4px;"></telerik:RadButton>
                                                <telerik:RadButton ID="a3" runat="server" Text="中班" OnCheckedChanged="a_CheckedChanged" Value="CF3AE587-8CB9-4D0A-B29A-08D2F6D187D9" ToggleType="CheckBox" Width="60" Checked="true" Style="margin-right: 4px;"></telerik:RadButton>
                                                <telerik:RadButton ID="a4" runat="server" Text="小班" OnCheckedChanged="a_CheckedChanged" Value="9FD9E587-8C09-4A55-9DB0-08D2F6D187DD" ToggleType="CheckBox" Width="60" Checked="true" Style="margin-right: 4px;"></telerik:RadButton>
                                                <telerik:RadButton ID="a5" runat="server" Text="托班" OnCheckedChanged="a_CheckedChanged" Value="850557E1-9EBD-4E0D-93DC-FE090A77D393" ToggleType="CheckBox" Width="60" Checked="true"></telerik:RadButton>
                                            </td>
                                            <th>&nbsp;&nbsp;&nbsp;&nbsp;</th>
                                            <th>发布人：</th>
                                            <td>
                                                <telerik:RadSearchBox ID="publisher" runat="server" ShowSearchButton="true" Width="100" DataTextField="Name" DataValueField="Id" OnSearch="publisher_Search" OnDataSourceSelect="publisher_DataSourceSelect"></telerik:RadSearchBox>
                                            </td>
                                        </tr>
                                    </table>
                                    <div>
                                    </div>
                                </div>
                                <div id="SourceLi" runat="server" class="fl xy_treebox mgt15">
                                    <div class="zTreeDemoBackground2 left">
                                        <telerik:RadTreeView runat="server" ID="tree" CheckBoxes="true" CheckChildNodes="true" OnNodeCheck="commentList_NodeCheck" ExpandNodeOnSingleClick="true" AutoPostBack="true" DataTextField="Name" DataValueField="Id" DataFieldID="Id" DataFieldParentID="ParentId"></telerik:RadTreeView>
                                    </div>
                                </div>
                                <div class="mgt15 fr" style="width: 700px;">
                                    <div class="xy_pxbar" style="height: 40px; line-height: 40px; vertical-align: middle;">
                                        <div style="margin: 10px auto;">
                                            <table>
                                                <tr>
                                                    <td>&nbsp;&nbsp;&nbsp;&nbsp;<telerik:RadButton Width="80" runat="server" ID="s1" OnClick="itemX_OnClick" Text="最新" Value='1' ToggleType="CheckBox" Checked="true"></telerik:RadButton>
                                                    </td>
                                                    <td>&nbsp;&nbsp;&nbsp;&nbsp;<telerik:RadButton Width="80" runat="server" ID="s2" OnClick="itemX_OnClick" Text="最热" Value='2' ToggleType="CheckBox"></telerik:RadButton>
                                                    </td>
                                                    <td>&nbsp;&nbsp;&nbsp;&nbsp;<telerik:RadButton Width="80" runat="server" ID="s3" OnClick="itemX_OnClick" Text="最优" Value='3' ToggleType="CheckBox"></telerik:RadButton>
                                                    </td>
                                                    <td style="width: 120px;">&nbsp;</td>
                                                    <td>&nbsp;&nbsp;&nbsp;&nbsp;<telerik:RadButton Width="80" runat="server" ID="sx" OnClick="sx_Click" Text="已审核" Value='s' ToggleType="CheckBox"></telerik:RadButton>
                                                    </td>
                                                    <td>&nbsp;&nbsp;&nbsp;&nbsp;<telerik:RadButton Width="80" runat="server" ID="ss" OnClick="ss_Click" Text="待审核" Value='x' ToggleType="CheckBox"></telerik:RadButton>
                                                        &nbsp;&nbsp;&nbsp;&nbsp;
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                    </div>
                                    <div class="xy_allzylist">
                                        <telerik:RadListView runat="server" ID="result" AllowPaging="true" PageSize="20" OnNeedDataSource="result_NeedDataSource" OnItemDataBound="result_ItemDataBound">
                                            <ItemTemplate>
                                                <dl>
                                                    <dt style="width: 90%;">
                                                        <img src='<%# string.Format("../Image/img/{0}.png", Eval("Thumbnail")) %>' width="64" height="64" />
                                                        <div>
                                                            <p>
                                                                <%#(int)Eval("Stick") == 0?string.Empty:"<span style='font-weight:bolder; color:#ca0e0e;font-size:14px;'>[置顶]</span>" %>&nbsp;<a limit="30" href='<%# string.Format("../Go/{0}?Id={1}", ((Homory.Model.ResourceType)Eval("Type"))== Homory.Model.ResourceType.视频 ? "ViewVideo" : "ViewPlain", Eval("Id")) %>' class="xy_zkyl"><%# Eval("Title") %> </a>
                                                            </p>

                                                            <p style="margin-top: 15px">
                                                                <table>
                                                                    <tr>
                                                                        <td style="width: 50%;">

                                                                            <a style="float: left;" href='<%# string.Format("../Go/Personal?Id={0}", Eval("UserId")) %>'><em><%# U(((Homory.Model.ResourceMap)Container.DataItem).UserId).RealName 

                                                                            %></em></a></span>&nbsp;&nbsp;<span>发布于：<em> <%# ((DateTime)Eval("ResourceTime")).ToString("yyyy-MM-dd") %></em></span>
                                                                        </td>
                                                                        <td style="width: 50%; padding-left: 80px">
                                                                            <telerik:RadRating Skin="Default" ItemCount="5" Orientation="Horizontal" Precision="Item" runat="server" CssClass="flList_ex" AutoPostBack="false" Value='<%# Eval("Grade") %>' Enabled="false"></telerik:RadRating>
                                                                            (<%# Eval("Rate") %>次评分)
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </p>
                                                        </div>

                                                    </dt>
                                                    <div style="clear: both;"></div>
                                                    <dd style="width: 90%; float: left;">
                                                        <ul>
                                                            <li>
                                                                <a id="ab" runat="server" class="xy_scbtn" visible='<%# (bool)Eval("Audit") %>' style="cursor: pointer;"><%# ((OpenType)Eval("OpenType")) == OpenType.不公开 ? "不公开" : (((State)Eval("State")) == State.停用 ? "未通过" : (((State)Eval("State")) == State.默认 ? "未审核" : "已通过")) %></a>
                                                                <telerik:RadToolTip ID="tip" runat="server" IsClientID="true" Skin="MetroTouch" AutoCloseDelay="0" Visible='<%# (IsOnline && AuditExists()) && ((OpenType)Eval("OpenType")) != OpenType.不公开 %>'>
                                                                    <table>
                                                                        <tr>
                                                                            <td colspan="5" style="text-align: center;">
                                                                                <telerik:RadTextBox ID="reason" runat="server" Skin="Metro" EmptyMessage="审核说明" Width="220px" Height="52" TextMode="MultiLine"></telerik:RadTextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td><a id="tb1" runat="server" class="xy_scbtn" style="cursor: pointer;" match='<%# Eval("Id") %>' onserverclick="tb1_ServerClick">通过</a></td>
                                                                            <td>&nbsp;</td>
                                                                            <td><a id="tb2" runat="server" class="xy_scbtn" style="cursor: pointer;" match='<%# Eval("Id") %>' onserverclick="tb2_ServerClick">不通过</a></td>
                                                                            <td>&nbsp;</td>
                                                                            <td><a id="tb3" runat="server" class="xy_scbtn" style="cursor: pointer;" match='<%# Eval("Id") %>' onserverclick="tb3_ServerClick">删除</a></td>
                                                                        </tr>
                                                                    </table>
                                                                </telerik:RadToolTip>
                                                            </li>
                                                            <li><a target="_blank" href='<%# string.Format("../Go/{0}?Id={1}", ((Homory.Model.ResourceType)Eval("Type"))== Homory.Model.ResourceType.视频 ? "ViewVideo" : "ViewPlain", Eval("Id")) %>' class="xy_xzbtn">浏览(<%# Eval("View") %>)</a></li>
                                                            <li><a target="_blank" href='<%# string.Format("../Go/{0}?Id={1}", ((Homory.Model.ResourceType)Eval("Type"))== Homory.Model.ResourceType.视频 ? "ViewVideo" : "ViewPlain", Eval("Id")) %>' class="xy_xzbtn">下载(<%# Eval("Download") %>)</a></li>
                                                            <li><a id="SetTop" class='<%#(int)Eval("Stick")== 0?"xy_ylbtn":"xy_blbtn" %>' style="cursor: pointer;" visible='<%# (IsOnline && AuditExists()) && ((Homory.Model.ResourceMap)Container.DataItem).AuditUsers.ToUpper().Contains(CurrentUser.Id.ToString().ToUpper()) %>' stick='<%#Eval("Stick") %>' runat="server" sourceid='<%#Eval("Id")%>' onserverclick="SetTop_Click"><%#(int)Eval("Stick") == 0? "置顶":"取消置顶" %></a></li>
                                                        </ul>
                                                    </dd>
                                                </dl>
                                            </ItemTemplate>
                                        </telerik:RadListView>
                                        <br />
                                        <telerik:RadDataPager runat="server" ID="pager" PageSize="20" PagedControlID="result">
                                            <Fields>
                                                <telerik:RadDataPagerSliderField HorizontalPosition="NoFloat" LabelTextFormat="第{0}页 共{1}页 每页20项" SliderDecreaseText="前翻" SliderIncreaseText="后翻" SliderDragText="拖动" SliderOrientation="Horizontal" />
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
