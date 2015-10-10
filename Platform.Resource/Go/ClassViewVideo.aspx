<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ClassViewVideo.aspx.cs" Inherits="Go.GoViewVideo" %>

<%@ Import Namespace="Homory.Model" %>
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
      <base target="_top" />
  <script src="../Script/jquery.min.js"></script>
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
</head>
<body style="background: url('../image/mainbg.png');">
    <form id="form1" runat="server">
        <telerik:RadScriptManager ID="Rsm" runat="server">
            <Scripts>
                <asp:ScriptReference Assembly="Telerik.Web.UI" Name="Telerik.Web.UI.Common.Core.js" />
                <asp:ScriptReference Assembly="Telerik.Web.UI" Name="Telerik.Web.UI.Common.jQuery.js" />
                <asp:ScriptReference Assembly="Telerik.Web.UI" Name="Telerik.Web.UI.Common.jQueryInclude.js" />
            </Scripts>
        </telerik:RadScriptManager>
        <telerik:RadAjaxPanel runat="server">
                                    <asp:Timer runat="server" ID="preview_timer" Interval="3000" Enabled="True" OnTick="preview_timer_Tick"></asp:Timer>
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
                                    <span>时间：<%= CurrentResource.Time.ToString("yyyy-MM-dd HH:mm") %></span>
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


                                <div id="ri" runat="server" class="j-content clearfix">
                                    <%= CurrentResource.Content %>
                                    <br />
                                    <div style="background-color: black;">
                                        <homory:XsfxPlayer runat="server" ID="player" />

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



                            </telerik:RadCodeBlock>

                        </div>
                        <script src="../Script/index.js"></script>
                        <style>
                            .poviewvideo {
                                cursor: pointer;
                            }
                        </style>
                    </div>
                </div>
        </telerik:RadAjaxPanel>
    </form>
</body>
</html>
