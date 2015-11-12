<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ViewVideo.aspx.cs" Inherits="Go.GoViewVideo" %>

<%@ Import Namespace="Homory.Model" %>
<%@ Register Src="~/Control/CommonTop.ascx" TagPrefix="homory" TagName="CommonTop" %>
<%@ Register Src="~/Control/CommonBottom.ascx" TagPrefix="homory" TagName="CommonBottom" %>
<%@ Register Src="~/Control/XsfxPlayer.ascx" TagPrefix="homory" TagName="XsfxPlayer" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title><%= CurrentResource.Title %>-互动资源平台</title>
    <link href="../Style/common.css" rel="stylesheet" />
    <link href="../Style/common_002.css" rel="stylesheet" />
    <link href="../Style/detail.css" rel="stylesheet" />
    <link href="../Style/commentInputBox.css" rel="stylesheet" />
    <link href="../Style/1.css" rel="stylesheet" />
    <script src="../Script/jquery.min.js"></script>
    <base target="_top" />
    <script>
        function GetUrlParms() {
            var args = new Object();
            var query = location.search.substring(1);//获取查询串   
            var pairs = query.split("&");//在逗号处断开   
            for (var i = 0; i < pairs.length; i++) {
                var pos = pairs[i].indexOf('=');//查找name=value   
                if (pos == -1) continue;//如果没有找到就跳过   
                var argname = pairs[i].substring(0, pos);//提取name   
                var value = pairs[i].substring(pos + 1);//提取value   
                args[argname] = unescape(value);//存为属性   
            }
            return args;
        }
    </script>
    <style>
        ding:focus {
            outline: none;
            border: none;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <telerik:RadScriptManager ID="Rsm" runat="server">
            <Scripts>
                <asp:ScriptReference Assembly="Telerik.Web.UI" Name="Telerik.Web.UI.Common.Core.js" />
                <asp:ScriptReference Assembly="Telerik.Web.UI" Name="Telerik.Web.UI.Common.jQuery.js" />
                <asp:ScriptReference Assembly="Telerik.Web.UI" Name="Telerik.Web.UI.Common.jQueryInclude.js" />
            </Scripts>
        </telerik:RadScriptManager>
        <telerik:RadWindowManager runat="server" ID="Rwm" Skin="Metro" Localization-OK="确定">
            <Windows>
                <telerik:RadWindow ID="homory_note_view" runat="server" NavigateUrl="../Go/PlayVideo.aspx" Skin="Metro" ShowContentDuringLoad="False" AutoSize="true" ReloadOnShow="true" OnClientBeforeClose="xoff" KeepInScreenBounds="true" VisibleStatusbar="false" Behaviors="Move,Close" Modal="true" Localization-Close="关闭">
                </telerik:RadWindow>
                <telerik:RadWindow ID="popup_rate" Title="评分详情" runat="server" AutoSize="True" ShowContentDuringLoad="false" ReloadOnShow="True" KeepInScreenBounds="true" VisibleStatusbar="false" Behaviors="Close" Modal="True" Localization-Close="关闭" EnableEmbeddedScripts="True" EnableEmbeddedBaseStylesheet="True" VisibleTitlebar="True">
                </telerik:RadWindow>
                <telerik:RadWindow ID="statistics_RW" Title="评估统计" runat="server" Modal="true" AutoSize="false" VisibleStatusbar="false" Behaviors="Close" CenterIfModal="True" Localization-Close="关闭" Width="800" Height="500" DestroyOnClose="true" ReloadOnShow="true"></telerik:RadWindow>
            </Windows>
        </telerik:RadWindowManager>
        <script>
            function xoff() {
                CKStop();
            }
            function popup(id) {
                window.radopen("../Go/PlayVideo.aspx?" + id, "homory_note_view");
                return false;
            }
            function popupRate() {
                var args = new Object();
                args = GetUrlParms();
                window.radopen("../Go/ViewRate.aspx?" + args["Id"], "popup_rate");
                return false;
            }
            function statistics_onclick() {
                var id = document.getElementById("accessId_hidden").value;
                var w = window.radopen("../Popup/AssessStatistics.aspx?id=" + id, "statistics_RW");
                w.set_width(900);
                w.set_height(500);
            }
        </script>

        <homory:CommonTop runat="server" ID="CommonTop" />
        <telerik:RadAjaxPanel runat="server">
            <asp:Timer runat="server" ID="preview_timer" Interval="3000" Enabled="False" OnTick="preview_timer_Tick"></asp:Timer>
            <div class="srx-bg22">
                <div class="srx-wrap">
                    <div class="srx-main" id="mainBox">
                        <div class="srx-left" style="background-color: #FFF">
                            <telerik:RadCodeBlock runat="server">
                                <div class="title-bar clearfix">
                                    <h1 class="p-title fl"><%= CurrentResource.Title %></h1>
                                    <div class="fr" data-action-data="" data-pid="618101" data-aid="23778" data-inout="in" data-position-type="classe" data-otype="photo" data-oid="618101">
                                    </div>
                                </div>
                                <div class="photo-info">
                                    <span>作者：<a href='<%= string.Format("../Go/Personal?Id={0}", TargetUser.Id) %>'><%= CurrentResource.User.DisplayName %></a></span>&nbsp;&nbsp;
                                    <span id="catalog" runat="server">栏目：<%= CurrentResource.ResourceCatalog.Where(o=>o.State==State.启用 &&o.Catalog.Type== CatalogType.视频).Aggregate(string.Empty,Combine).CutString(null) %></span>
                                    <br />
                                    <asp:Panel runat="server" ID="cg">
                                        <span><%= CombineGrade() %></span>&nbsp;&nbsp;
                                    <span><%= CombineCourse() %></span>
                                        <br />
                                    </asp:Panel>
                                    <asp:Panel runat="server" ID="tag">
                                        <span>标签：<%= CombineTags() %></span>
                                        <br />
                                    </asp:Panel>
                                    <span>时间：<%= CurrentResource.Time.ToString("yyyy-MM-dd HH:mm") %></span><br />
                                    <span>容量：<%= MB() %></span>
                                </div>

                                <div id="ni" runat="server" style="font-size: 14px; font-weight: bold;">
                                    <br />
                                    <br />
                                    乐翼教育云资源平台正在为您转换视频格式，请稍候。。。
                                    <br />
                                    <br />
                                    视频转换成功后，会自动为您跳转至资源浏览页。您可以先<a target="_blank" href="../Go/Home">浏览其他云资源</a>。
                                    <br />
                                    <br />
                                </div>

                                <div id="notAllowed" runat="server" style="font-size: 14px; font-weight: bold;">
                                    <br />
                                    <br />
                                    您无权限查看该资源。
                                    <br />
                                    <br />
                                    请尝试登录后再访问此资源，也可以<a target="_blank" href="../Go/Home">浏览其他云资源</a>。
                                    <br />
                                    <br />
                                </div>

                                <div id="ri" runat="server" class="j-content clearfix">
                                    <%= CurrentResource.Content %>
                                    <br />
                                    <div style="background-color: black;">
                                        <homory:XsfxPlayer runat="server" ID="player" />

                                        <script>
                                            function startDo(sender, e) {
                                                var t = showtime();
                                                $find("<%= startDot.ClientID %>").set_value(t + '\'');
                                            }
                                            function endDo(sender, e) {
                                                var t = showtime();
                                                $find("<%= endDot.ClientID %>").set_value(t + '\'');
                                            }
                                        </script>

                                        <div style="width: 738px; margin-top: 8px; padding-left: 8px;">
                                            <telerik:RadButton ID="startBtn" AutoPostBack="false" runat="server" Skin="Black" Text="设定切片起点" OnClientClicked="startDo"></telerik:RadButton>
                                            <telerik:RadTextBox ID="startDot" runat="server" ReadOnly="true" ReadOnlyStyle-HorizontalAlign="Center" Skin="Black" Width="60" Style="margin-top: -8px; margin-right: -6px; margin-bottom: 0px; margin-left: -5px;"></telerik:RadTextBox>
                                            <telerik:RadButton ID="endBtn" AutoPostBack="false" runat="server" Skin="Black" Text="设定切片终点" OnClientClicked="endDo"></telerik:RadButton>
                                            <telerik:RadTextBox ID="endDot" runat="server" ReadOnly="true" ReadOnlyStyle-HorizontalAlign="Center" Skin="Black" Width="60" Style="margin-top: -8px; margin-right: -6px; margin-bottom: 0px; margin-left: -6px;"></telerik:RadTextBox>
                                            <telerik:RadButton ID="RadButton1" runat="server" Skin="Black" Text="点评内容：" AutoPostBack="false" Style="margin-left: 10px;"></telerik:RadButton>
                                            <telerik:RadTextBox ID="dotContent" runat="server" Skin="Black" Width="250" Style="margin-top: -8px; margin-right: 6px; margin-bottom: 0px; margin-left: -5px;"></telerik:RadTextBox>
                                            <telerik:RadButton ID="dotSend" runat="server" Skin="Black" Text="发表" OnClick="dotSend_Click"></telerik:RadButton>
                                        </div>
                                    </div>
                                </div>
                                <br />
                                <br />


                                <p id="pppp1" runat="server" style="font-size: 16px;">附件：</p>
                                <p id="pppp2" runat="server">

                                    <telerik:RadListView ID="publish_attachment_list" runat="server" OnNeedDataSource="publish_attachment_list_NeedDataSource">
                                        <ItemTemplate>
                                            <img src='<%# string.Format("../Image/img/{0}.jpg", (int)Eval("FileType")) %>' />
                                            <a href='<%# string.Format("{0}", Eval("Source")) %>'><%# Eval("Title") %></a>&nbsp;&nbsp;
                                        </ItemTemplate>
                                    </telerik:RadListView>
                                </p>


                                <div id="ri2" runat="server" class="photo-actions clearfix">
                                    <table>
                                        <tr>
                                            <td>
                                                <asp:Panel runat="server" ID="previous" Visible="False">
                                                    <span style="color: black;">上一个：<a runat="server" id="previousLink"></a></span>
                                                </asp:Panel>
                                            </td>
                                            <td>&nbsp;&nbsp;&nbsp;&nbsp;</td>
                                            <td>
                                                <asp:Panel runat="server" ID="next" Visible="False">
                                                    <span style="color: black;">下一个：<a runat="server" id="nextLink"></a></span>
                                                </asp:Panel>
                                            </td>
                                        </tr>
                                    </table>

                                </div>

                            </telerik:RadCodeBlock>



                            <div id="ri3" runat="server" class="xy_zypf mgt20" style="margin: 10px 0px; height: 100px;">
                                <table>
                                    <tr>
                                        <td>
                                            <strong>资源评分：</strong>
                                        </td>
                                        <td>
                                            <div class="grade_wrap">
                                                <div class="starbig" id="comment_score" style="cursor: pointer;">
                                                    <telerik:RadAjaxPanel runat="server">
                                                        <telerik:RadRating runat="server" ID="rating" Style="cursor: pointer;" AutoPostBack="True" Precision="Item" Skin="Default" OnRate="rating_OnRate"></telerik:RadRating>
                                                        <input type="hidden" id="gradeVal" value="8" />
                                                    </telerik:RadAjaxPanel>
                                                </div>
                                            </div>
                                        </td>
                                        <td width="180" style="font-size: 20px; cursor: pointer;" onclick="popupRate();">
                                            <telerik:RadAjaxPanel runat="server" ID="scorePanel" OnAjaxRequest="scorePanel_AjaxRequest">资源得分：<label id="score" runat="server"></label></telerik:RadAjaxPanel>
                                        </td>
                                    </tr>
                                </table>
                                <br />
                                <table>
                                    <tr>
                                        <td>
                                            <telerik:RadAjaxPanel runat="server" ID="downloadPanel">
                                                <table style="background: #a0d468; vertical-align: middle;  font-size: 15px;">
                                                    <tr>
                                                        <td style="width: 120px; text-align: center;">
                                                            <a target="_self" id="download" runat="server" onserverclick="download_OnServerClick" style="height: 50px; line-height: 50px; color: white;">高清下载</a>
                                                        </td>
                                                        <td style="width: 120px; text-align: center;">
                                                            <a target="_self" id="downloadX" runat="server" onserverclick="downloadX_OnServerClick" style="height: 50px; line-height: 50px; color: white;">高速下载</a>
                                                        </td>
                                                        <td style="width: 120px; text-align: center;">
                                                            <em id="downloadCount" runat="server" style="color: white;"></em>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </telerik:RadAjaxPanel>
                                        </td>
                                        <td>
                                            <telerik:RadAjaxPanel runat="server" ID="favouritePanel">
                                                <table style="vertical-align: middle;  font-size: 15px;" id="xxxxx" runat="server">
                                                    <tr>
                                                        <td style="width: 120px; text-align: center;">
                                                            <a target="_self" id="favourite" runat="server" onserverclick="favourite_OnServerClick" style="height: 50px; line-height: 50px; color: white;">收藏</a>
                                                        </td>
                                                        <td style="width: 100px; text-align: center;">
                                                            <em id="favouriteCount" runat="server" style="color: white;"></em>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </telerik:RadAjaxPanel>
                                        </td>
                                    </tr>
                                </table>
                            </div>



                            <br />
                            <telerik:RadAjaxPanel runat="server" ID="assessPanel">
                                <p style="font-size: 16px; margin-bottom: 6px; cursor: pointer;" onclick="statistics_onclick();">评估：（<span id="sss" runat="server"></span>）<span style="font-size: smaller; color: red;">&nbsp;&nbsp;&nbsp;&nbsp;评估细则和要点详见：无锡市中小学有效课堂教与学《规范条例》和《考评细则》</span></p>
                                <asp:TextBox id="accessId_hidden" runat="server"  style="display:none;"></asp:TextBox>
                                <table style="font-size: 15px;">
                                <telerik:RadListView runat="server" ID="assessTable" OnNeedDataSource="assessTable_OnNeedDataSource">
                                    <ItemTemplate>
                                        <tr><td>&nbsp;</td><td>&nbsp;</td></tr>
                                        <tr>
                                            <td style="width: 70%;">
                                                <%# Eval("Name") %><%# Eval("All") %>
                                            </td>
                                            <td style="width: 30%; text-align: right;">
                                                <telerik:RadSlider ID="ss" runat="server" IncreaseText="+" Width="300" Height="40" TrackPosition="BottomRight" DecreaseText="-" DragText="" ItemType="Tick" ThumbsInteractionMode="Free" LiveDrag="True" MaximumValue='<%# Eval("Score") %>' MinimumValue="0" Value='<%# Eval("Me") %>' SmallChange="1" LargeChange="5">
                                                </telerik:RadSlider>
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                </telerik:RadListView>
                                </table>
                                <div style="clear: both;"></div>
                                <div>
                                    <a id="rr" runat="server" class="button24 srx-ciptbox-submit" target="_self" onserverclick="rr_OnServerClick" style="width: 60px; margin-top: 10px;"><em>确认</em></a>
                                </div>
                            </telerik:RadAjaxPanel>
                            <br />
                            <br />

                            <p id="ri4" runat="server" style="font-size: 16px;">评论：</p>
                            <telerik:RadAjaxPanel runat="server" ID="commentPanel">

                                <div class="srx-comment-iptbox" id="srxCommentInputBox">
                                    <textarea id="comment" runat="server" rows="11" style="width: 730px;"></textarea>
                                    <div class="srx-ciptbox-toolbar">

                                        <a id="doComment" runat="server" class="button24 srx-ciptbox-submit" target="_self" onserverclick="doComment_OnServerClick" style="width: 60px;"><em>发表</em></a>


                                    </div>
                                </div>

                                <telerik:RadTreeView runat="server" ID="commentList" EnableEmbeddedBaseStylesheet="False" EnableEmbeddedSkins="False" DataFieldParentID="ParentId" DataTextField="Content" DataFieldID="Id" DataValueField="Id">
                                    <NodeTemplate>
                                        <div class="srx-comment-list-box" id="srxCommentListBox" style='<%# string.Format("margin-left: {0}px;", ((Homory.Model.ResourceComment)Container.DataItem).Level * 30) %>'>
                                            <div class="srx-comment-list" style="margin-top: 6px;">
                                                <dl class="srx-comment-item">
                                                    <dt>
                                                        <asp:Image runat="server" ID="icon" ImageUrl='<%# P(((Homory.Model.ResourceComment)Container.DataItem).User.Icon) %>' Width="35" Height="35" />
                                                    </dt>
                                                    <dd>
                                                        <div style="font-size: 14px;">
                                                            <a style="font-size: 14px;" href='<%# string.Format("../Go/Personal?Id={0}", ((Homory.Model.ResourceComment)Container.DataItem).User.Id) %>'><%# UC(((Homory.Model.ResourceComment)Container.DataItem).User.Id) + "&nbsp;" + ((Homory.Model.ResourceComment)Container.DataItem).User.DisplayName %></a>&nbsp;<%# ((DateTime)Eval("Time")).FormatTime() %>&nbsp;<%# ((Homory.Model.ResourceComment)Container.DataItem).Level ==0 ? "评论" : "回复" %>：
                                                        </div>
                                                        <asp:Panel runat="server" Visible='<%# ((bool?)Eval("Timed")).HasValue && ((bool?)Eval("Timed")).Value %>' CssClass="poviewvideo">
                                                            <img src="../Image/cut_video.gif" style="width: 26px;" onclick="popup('<%# ((ResourceComment)Container.DataItem).Id.ToString() %>');" />&nbsp;&nbsp;&nbsp;切片时间：<%# FormatPeriod((ResourceComment)Container.DataItem) %>
                                                        </asp:Panel>
                                                        <div class="srx-comment-content">
                                                            <label style="color: #333333; font-weight: bold; font-size: 14px;"><%# Eval("Content") %></label>&nbsp;&nbsp;&nbsp;&nbsp;
                                                            <a target="_self" id="goReply" runat="server" onserverclick="goReply_OnServerClick">
                                                                <img src="Image/c__c.gif" style="width: 26px;" onmouseover="this.src = 'Image/c.gif';" onmouseout="this.src = 'Image/c__c.gif';" /></a>&nbsp;&nbsp;&nbsp;
                                                            <a target="_self" id="goDelP" runat="server" alt='<%# Eval("Id").ToString() %>' name='<%# Eval("Id").ToString() %>' onserverclick="goDelP_ServerClick" visible='<%# IsOnline &&  ((Homory.Model.ResourceComment)Container.DataItem).User.Id == CurrentUser.Id %>'>
                                                                <img src="Image/b__b.gif" style="width: 26px;" onmouseover="this.src = 'Image/b.gif';" onmouseout="this.src = 'Image/b__b.gif';" /></a>
                                                        </div>
                                                        <div class="srx-comment-info">
                                                            <br />
                                                            <span runat="server" id="reply" visible="False">
                                                                <input id="replyId" type="hidden" runat="server" value='<%# Eval("Id").ToString() %>' /><textarea id="replyContent" runat="server" style="width: 90%; height: 40px;" /><br />
                                                                <a id="replyReply" runat="server" onserverclick="replyReply_OnServerClick" target="_self">
                                                                    <img src="Image/a__a.gif" onmouseover="this.src = 'Image/a.gif';" onmouseout="this.src = 'Image/a__a.gif';" style="width: 54px;" /></a></a>&nbsp;&nbsp;&nbsp;
                                                                <a id="noReply" runat="server" onserverclick="noReply_OnServerClick" target="_self">
                                                                    <img onmouseover="this.src = 'Image/d.gif';" onmouseout="this.src = 'Image/d__d.gif';" src="Image/d__d.gif" style="width: 54px;" /></a></a></span>
                                                        </div>
                                                    </dd>
                                                </dl>



                                            </div>




                                        </div>
                                    </NodeTemplate>
                                </telerik:RadTreeView>

                            </telerik:RadAjaxPanel>

                        </div>
                        <div id="ri5" runat="server" class="srx-right">
                            <telerik:RadCodeBlock runat="server">
                                <div class="photo-rbox rbox-user-zone clearfix">
                                    <a href='<%= string.Format("../Go/Personal?Id={0}", TargetUser.Id) %>' class="fl">
                                        <asp:Image runat="server" ID="icon" class="fl" Height="50" Width="50" />
                                    </a>
                                    <span class="rbox-uz-right fl">
                                        <h3>
                                            <div>
                                                <a href='<%= string.Format("../Go/Personal?Id={0}", TargetUser.Id) %>'>
                                                    <asp:Label runat="server" ID="name"></asp:Label></a>
                                            </div>
                                            <div>
                                                <a href='<%= string.Format("../Go/Personal?Id={0}", TargetUser.Id) %>'>
                                                    <asp:Label runat="server" ID="nameX"></asp:Label></a>
                                            </div>
                                        </h3>
                                        <a id="go" href='<%= string.Format("../Go/Personal?Id={0}", TargetUser.Id) %>'>进入教师空间</a>
                                    </span>
                                </div>
                            </telerik:RadCodeBlock>
                            <div style="clear: both; height: 10px"></div>
                            <!--Start-->

                            <div class="mb10">
                                <span style="font-size: 16px; color: #666; font-weight: bold" class="fl">听课笔记</span>
                                <a href="../Go/Centernote" class="fr mt5">+全部笔记</a>
                            </div>

                            <div class="cl"></div>

                            <telerik:RadAjaxPanel runat="server" ID="mnp">
                                <div class="tab1" id="tab1">
                                    <div class="menu1">
                                        <ul>
                                            <li>&nbsp;</li>
                                        </ul>
                                    </div>
                                    <div style="background: url('../Image/aa.jpg'); width: 183px; height: 182px; overflow: hidden; opacity: 0.8;">
                                        <textarea id="mn1" class="ding" runat="server" data-input-limit-uid="0" style="width: 182px; opacity: 1; margin-top: 28px; outline: none; resize: none; border: none; background-color: transparent; background: none; line-height: 21px; height: 146px; color: #5c2337; overflow: hidden;"></textarea>
                                    </div>
                                </div>
                                <br />
                                <div class="tab1" id="tab1">
                                    <%--                                    <div class="menu1">
                                        <ul>
                                            <li>&nbsp;</li>
                                        </ul>
                                    </div>--%>
                                    <div style="background: url('../Image/bb.jpg'); width: 183px; height: 182px; overflow: hidden; opacity: 0.8;">
                                        <textarea id="mn2" class="ding" runat="server" data-input-limit-uid="0" style="width: 182px; opacity: 1; margin-top: 28px; outline: none; resize: none; border: none; background-color: transparent; background: none; line-height: 21px; height: 146px; color: #5c2337; overflow: hidden;"></textarea>
                                    </div>
                                    <div class="menudiv1">
                                        <div id="con_one_1">
                                            <div class="c-prb-role clearfix">
                                                <div class="srx-comment-iptboxbj" id="srxCommentInputBox">
                                                    <div class="srx-ciptbox-toolbar">
                                                        <span class="srx-ciptbox-acts"></span>
                                                        <a id="mnbtn" target="_self" runat="server" onserverclick="mnbtn_ServerClick" class="button24 srx-ciptbox-submit" data-action="submit"><em>保存</em></a>
                                                        <span class="srx-ciptbox-counter" data-ui-role="counter"><em></em></span>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>


                                    </div>

                                </div>
                            </telerik:RadAjaxPanel>

                            <!--End-->
                            <div style="clear: both; height: 10px"></div>
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
                                                    <telerik:RadAjaxPanel runat="server">
                                                        <asp:Repeater runat="server" ID="latest">
                                                            <ItemTemplate>
                                                                <li style="clear: both;"><a style="float: left;" href='<%# string.Format("../Go/ViewVideo?Id={0}", Eval("Id")) %>'><%# Eval("Title").ToString().CutString(9) %></a><label style="float: right;"><%# ((System.DateTime)Eval("Time")).FormatTimeShort() %>&nbsp;&nbsp;</label></li>
                                                            </ItemTemplate>
                                                        </asp:Repeater>
                                                    </telerik:RadAjaxPanel>
                                                </ul>
                                            </div>
                                            <div class="tabContent">
                                                <ul class="mb10 mt10">
                                                    <telerik:RadAjaxPanel runat="server">
                                                        <asp:Repeater runat="server" ID="popular">
                                                            <ItemTemplate>
                                                                <li style="clear: both;"><a style="float: left;" href='<%# string.Format("../Go/ViewVideo?Id={0}", Eval("Id")) %>'><%# Eval("Title").ToString().CutString(9) %></a><label style="float: right;"><%# Eval("View") %>次&nbsp;&nbsp;</label></li>
                                                            </ItemTemplate>
                                                        </asp:Repeater>
                                                    </telerik:RadAjaxPanel>

                                                </ul>
                                            </div>
                                            <div class="tabContent">
                                                <ul class="mb10 mt10">
                                                    <telerik:RadAjaxPanel runat="server" ID="bestPanel" OnAjaxRequest="bestPanel_AjaxRequest">
                                                        <asp:Repeater runat="server" ID="best">
                                                            <ItemTemplate>
                                                                <li style="clear: both;"><a style="float: left;" href='<%# string.Format("../Go/ViewVideo?Id={0}", Eval("Id")) %>'><%# Eval("Title").ToString().CutString(9) %></a><label style="float: right;"><%# Eval("Grade") %>分&nbsp;&nbsp;</label></li>
                                                            </ItemTemplate>
                                                        </asp:Repeater>
                                                    </telerik:RadAjaxPanel>
                                                </ul>
                                            </div>

                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="r-visitors">
                                <div class="rv-title clearfix">
                                    <h3 class="fl">TA们刚刚看过</h3>

                                    <span class="rv-count fr">访问总数：<asp:Label runat="server" ID="viewCount"></asp:Label></span>

                                </div>
                                <div class="rv-box">
                                    <ul class="clearfix" style="margin-left: 15px">
                                        <asp:Repeater runat="server" ID="viewList">
                                            <ItemTemplate>
                                                <li style="margin: 4px;">
                                                    <a href='<%# string.Format("../Go/Personal?Id={0}", U(Eval("Id3")).Id) %>'>
                                                        <asp:Image runat="server" ID="icon" ImageUrl='<%# P(U(Eval("Id3")).Icon) %>' class="face face_40" Height="40" Width="40" /></a>
                                                    <span class="rv-time" style="margin-left: 4px;"><%# ((DateTime)Eval("Time")).FormatTimeShort() %></span>
                                                </li>
                                            </ItemTemplate>
                                        </asp:Repeater>



                                    </ul>
                                </div>
                            </div>


                        </div>
                    </div>
                    <script src="../Script/index.js"></script>
                    <style>
                        .poviewvideo {
                            cursor: pointer;
                            font-size: 14px;
                            margin-top: 6px;
                        }
                    </style>
                    <homory:CommonBottom runat="server" ID="CommonBottom" />
                </div>
            </div>
        </telerik:RadAjaxPanel>
    </form>
</body>
</html>
