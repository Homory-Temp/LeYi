<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ViewPlain.aspx.cs" Inherits="Go.GoViewPlain" %>

<%@ Import Namespace="Homory.Model" %>
<%@ Register Src="~/Control/CommonTop.ascx" TagPrefix="homory" TagName="CommonTop" %>
<%@ Register Src="~/Control/CommonBottom.ascx" TagPrefix="homory" TagName="CommonBottom" %>

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

        function GetUrlParmsEx(name) {
            var args = new Object();
            args = GetUrlParms();
            return args[name];
        }

        function isIE() {
            if (window.ActiveXObject || "ActiveXObject" in window)
                return true;
            else
                return false;
        }
        function isIE6() {
            return isIE() && !window.XMLHttpRequest;
        }
        function isIE7() {
            return isIE() && window.XMLHttpRequest && !document.documentMode;
        }
        function isIE8() {
            return isIE() && !-[1, ] && document.documentMode;
        }
        function fixIE(url) {
            if (isIE() && (isIE6() || isIE7() || isIE8()))
                top.location.href = url;
        }

        if (isIE())
            fixIE('./ViewPlainFix?Id=' + GetUrlParmsEx('Id'));
    </script>
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
        <telerik:RadWindowManager runat="server" ID="Rwm" Skin="Metro">
            <Windows>
                <telerik:RadWindow ID="popup_rate" runat="server" Title="评分详情" AutoSize="True" ShowContentDuringLoad="false" ReloadOnShow="True" KeepInScreenBounds="true" VisibleStatusbar="false" Behaviors="Close" Modal="True" Localization-Close="关闭" EnableEmbeddedScripts="True" EnableEmbeddedBaseStylesheet="True" VisibleTitlebar="True">
                </telerik:RadWindow>
            </Windows>
        </telerik:RadWindowManager>
        <script>
            function popupRate() {
                var args = new Object();
                args = GetUrlParms();
                window.radopen("../Go/ViewRate.aspx?" + args["Id"], "popup_rate");
                return false;
            }
        </script>
        <homory:CommonTop runat="server" ID="CommonTop" />
        <telerik:RadAjaxPanel runat="server">
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
                                    <span id="catalog" runat="server">栏目：<%= CurrentResource.ResourceCatalog.Where(o=>o.State==State.启用 &&o.Catalog.Type== CatalogType.文章).Aggregate(string.Empty,Combine).CutString(null) %></span>
                                    <br />
                                    <span>适用年龄段：<%= CombineAge() %></span>
                                    <br />
                                    <asp:Panel runat="server" ID="tag">
                                        <span>标签：<%= CombineTags() %></span>
                                        <br />
                                    </asp:Panel>
                                    <span>时间：<%= CurrentResource.Time.ToString("yyyy-MM-dd HH:mm") %></span>
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
                                <div id="vp1" runat="server" class="j-content clearfix">
                                    <%= CurrentResource.Content %>
                                    <br />
                                    <iframe runat="server" src="../Document/web/PdfViewer.aspx" width="738px" height="800px" id="publish_preview_pdf" style="margin-top: 10px;"></iframe>

                                </div>
                                <br />
                                <br />
                                <p id="pppp1" runat="server" style="font-size: 16px;">附件：</p>
                                <p id="pppp2" runat="server">

                                    <telerik:RadListView ID="publish_attachment_list" runat="server" OnNeedDataSource="publish_attachment_list_OnNeedDataSource">
                                        <ItemTemplate>
                                            <img src='<%# string.Format("../Image/img/{0}.jpg", (int)Eval("FileType")) %>' />
                                            <a href='<%# string.Format("{0}", Eval("Source")) %>'><%# Eval("Title") %></a>&nbsp;&nbsp;
                                        </ItemTemplate>
                                    </telerik:RadListView>
                                </p>

                                <div id="vp2" runat="server" class="photo-actions clearfix">
                                    <table>
                                        <tr>
                                            <td>
                                                <asp:Panel runat="server" ID="previous" Visible="False">
                                                    <span style="color: black;">上一篇：<a runat="server" id="previousLink"></a></span>
                                                </asp:Panel>
                                            </td>
                                            <td>&nbsp;&nbsp;&nbsp;&nbsp;</td>
                                            <td>
                                                <asp:Panel runat="server" ID="next" Visible="False">
                                                    <span style="color: black;">下一篇：<a runat="server" id="nextLink"></a></span>
                                                </asp:Panel>
                                            </td>
                                        </tr>
                                    </table>

                                </div>

                            </telerik:RadCodeBlock>



                            <div id="vp3" runat="server" class="xy_zypf mgt20" style="margin: 10px 0px;">
                                <strong>资源评分：</strong>
                                <div class="grade_wrap">
                                    <div class="starbig" id="comment_score" style="cursor: pointer;">
                                        <telerik:RadAjaxPanel runat="server">
                                            <telerik:RadRating runat="server" ID="rating" Style="cursor: pointer;" AutoPostBack="True" Precision="Item" Skin="Default" OnRate="rating_OnRate"></telerik:RadRating>
                                            <input type="hidden" id="gradeVal" value="8" />
                                        </telerik:RadAjaxPanel>
                                    </div>

                                </div>

                                <table style="margin-top: -20px;">


                                    <tr>

                                        <td width="180" style="font-size: 20px; cursor: pointer;" onclick="popupRate();">
                                            <telerik:RadAjaxPanel runat="server" ID="scorePanel" OnAjaxRequest="scorePanel_AjaxRequest">资源得分：<label id="score" runat="server"></label></telerik:RadAjaxPanel>
                                        </td>

                                        <td width="130">
                                            <telerik:RadAjaxPanel runat="server" ID="downloadPanel">
                                                <a target="_self" id="download" runat="server" onserverclick="download_OnServerClick" class="xzbigbtn"></br>下载</br>
                                  <em id="downloadCount" runat="server"></em></a>
                                            </telerik:RadAjaxPanel>
                                        </td>
                                        <td align="right" width="130">
                                            <telerik:RadAjaxPanel runat="server" ID="favouritePanel">
                                                <a target="_self" id="favourite" runat="server" onserverclick="favourite_OnServerClick"></br>收藏</br>
                                  <em id="favouriteCount" runat="server"></em></a>
                                            </telerik:RadAjaxPanel>
                                        </td>
                                    </tr>

                                </table>
                            </div>

                            <p id="vp4" runat="server" style="font-size: 16px;">评论：</p>
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
                                                        <asp:Image runat="server" ID="icon" ImageUrl='<%# ((Homory.Model.ResourceComment)Container.DataItem).User.Icon %>' Width="35" Height="35" />
                                                    </dt>
                                                    <dd>
                                                        <div style="font-size: 14px;">
                                                            <a style="font-size: 14px;" href='<%# string.Format("../Go/Personal?Id={0}", ((Homory.Model.ResourceComment)Container.DataItem).User.Id) %>'><%# UC(((Homory.Model.ResourceComment)Container.DataItem).User.Id) + "&nbsp;" + ((Homory.Model.ResourceComment)Container.DataItem).User.DisplayName %></a>&nbsp;<%# ((DateTime)Eval("Time")).FormatTime() %>&nbsp;<%# ((Homory.Model.ResourceComment)Container.DataItem).Level ==0 ? "评论" : "回复" %>：
                                                        </div>
                                                        <div class="srx-comment-content">
                                                            <label style="color: #333333; font-weight: bold; font-size: 14px;"><%# Eval("Content") %></label>&nbsp;&nbsp;&nbsp;&nbsp;
                                                            <a target="_self" id="goReply" runat="server" onserverclick="goReply_OnServerClick">
                                                                <img src="Image/c.gif" style="width: 26px;" /></a>&nbsp;&nbsp;&nbsp;
                                                            <a target="_self" id="goDelP" runat="server" alt='<%# Eval("Id").ToString() %>' name='<%# Eval("Id").ToString() %>' onserverclick="goDelP_ServerClick" visible='<%# IsOnline && (Detect() || ((Homory.Model.ResourceComment)Container.DataItem).User.Id == CurrentUser.Id) %>'>
                                                                <img src="Image/b.gif" style="width: 26px;" /></a>
                                                        </div>
                                                        <div class="srx-comment-info" style="margin-top: 4px;">
                                                            <br />
                                                            <span runat="server" id="reply" visible="False">
                                                                <input id="replyId" type="hidden" runat="server" value='<%# Eval("Id").ToString() %>' /><textarea id="replyContent" runat="server" style="width: 90%; height: 40px;" /><br />
                                                                <a id="replyReply" runat="server" onserverclick="replyReply_OnServerClick" target="_self">
                                                                    <img src="Image/a.gif" style="width: 26px;" /></a></a>&nbsp;&nbsp;&nbsp;<a id="noReply" runat="server" onserverclick="noReply_OnServerClick" target="_self"><img src="Image/d.gif" style="width: 26px;" /></a></a></span>
                                                        </div>
                                                    </dd>
                                                </dl>



                                            </div>




                                        </div>
                                    </NodeTemplate>
                                </telerik:RadTreeView>

                            </telerik:RadAjaxPanel>

                        </div>
                        <div class="srx-right">
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
                                                                <li style="clear: both;"><a style="float: left;" href='<%# string.Format("../Go/ViewPlain?Id={0}", Eval("Id")) %>'><%# Eval("Title").ToString().CutString(9) %></a><label style="float: right;"><%# ((System.DateTime)Eval("Time")).FormatTimeShort() %>&nbsp;&nbsp;</label></li>
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
                                                                <li style="clear: both;"><a style="float: left;" href='<%# string.Format("../Go/ViewPlain?Id={0}", Eval("Id")) %>'><%# Eval("Title").ToString().CutString(9) %></a><label style="float: right;"><%# Eval("View") %>次&nbsp;&nbsp;</label></li>
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
                                                                <li style="clear: both;"><a style="float: left;" href='<%# string.Format("../Go/ViewPlain?Id={0}", Eval("Id")) %>'><%# Eval("Title").ToString().CutString(9) %></a><label style="float: right;"><%# Eval("Grade") %>分&nbsp;&nbsp;</label></li>
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
                                                <li style="margin: 4px;" title='<%# U(Eval("Id3")).DisplayName %>'>
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
                    <homory:CommonBottom runat="server" ID="CommonBottom" />
                </div>
            </div>
        </telerik:RadAjaxPanel>
    </form>
</body>
</html>
