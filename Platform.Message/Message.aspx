<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Message.aspx.cs" Inherits="Message" %>

<%@ Import Namespace="System.Linq" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.1//EN" "http://www.w3.org/tr/xhtml11/dtd/xhtml11.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" style="height: 100%;">
<head runat="server">
    <title>梁溪教育网络寻呼</title>
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <link href="Content/css/bootstrap.min.css" rel="stylesheet" />
    <link href="Content/css/style-responsive.css" rel="stylesheet" />
    <link href="Content/css/style.css" rel="stylesheet" />
    <script src="Content/js/bootstrap.min.js"></script>
    <link href="Content/common.css" rel="stylesheet" />
    <script>
        var exp = true;

        var tids = "";

        function refresh() {
            window.location.reload();
        }

        var ac = 0;

        function countUser() {
            $("div[x=2]").text("已选（" + $("div[class='btn btn-tumblr btn-xs pull-left']").length + "）");
            tids = "";
            $("div[class='btn btn-tumblr btn-xs pull-left']").each(function (i, o) {
                if ($(o).attr("tid"))
                    tids += $(o).attr("tid") + "|";
            });
            $("#send_ids").val(tids);
        }

        function checkDepartment(obj) {
            $(obj).parent().parent().parent().parent().find("div[x=1]").each(function (i, o) {
                o.click();
            });
            countUser();
        }

        function checkUser(obj) {
            if ($(obj).attr('class') == $(obj).attr('toclass'))
                $(obj).attr('class', 'btn btn-tumblr btn-xs pull-left');
            else
                $(obj).attr('class', $(obj).attr('toclass'));
            countUser();
        }

        function list_members(obj) {
            var l = "";
            $("#members").html(l);
            $(".t div[class='" + $(obj).attr('class').replace(' pull-right', '') + " btn-xs pull-left']").each(function (i, o) {
                l += o.innerText + "、&nbsp;&nbsp;";
            });
            if (l.length >= 13) {
                $("#members").html(l.substr(0, l.length - 13));
            }
            openCount();
        }

        function sent() {
            $("#content").val('');
            $("div[class='btn btn-tumblr btn-xs pull-left']").each(function (i, o) {
                o.click();
            });
            window.radalert('寻呼发送成功', 200, 90);
        }

        function searchUser() {
            var t = $("#toSearch").val();
            $(".searchArea").animate({
                scrollTop: 0
            }, 0);
            $(".searchArea div[x=1]").each(function (i, o) {
                if ($(o).text() == t) {
                    var scroll_offset = $(o).offset();
                    $(".searchArea").animate({
                        scrollTop: scroll_offset.top - 43
                    }, 0);
                    $(o).animate({
                        opacity: '0.2'
                    }, 1000);
                    $(o).animate({
                        opacity: '1'
                    }, 1000);
                    $(o).animate({
                        opacity: '0.2'
                    }, 1000);
                    $(o).animate({
                        opacity: '1'
                    }, 1000);
                    $(o).animate({
                        opacity: '0.2'
                    }, 1000);
                    $(o).animate({
                        opacity: '1'
                    }, 1000);
                }
            });
        }
    </script>
</head>
<body style="height: 100%; margin: 0;">
    <form id="form" runat="server" style="height: 100%; margin: 0;">
        <telerik:RadScriptManager ID="sm" runat="server">
            <Scripts>
                <asp:ScriptReference Assembly="Telerik.Web.UI" Name="Telerik.Web.UI.Common.Core.js" />
                <asp:ScriptReference Assembly="Telerik.Web.UI" Name="Telerik.Web.UI.Common.jQuery.js" />
                <asp:ScriptReference Assembly="Telerik.Web.UI" Name="Telerik.Web.UI.Common.jQueryInclude.js" />
            </Scripts>
        </telerik:RadScriptManager>
        <telerik:RadWindowManager ID="wm" runat="server">
            <Localization Close="关闭" OK="确定" />
            <Windows>
                <telerik:RadWindow ID="users" runat="server" Title="人员列表" VisibleStatusbar="false" Modal="True" InitialBehaviors="Close" AutoSize="false" Behaviors="Close" Width="600" Height="296">
                    <ContentTemplate>
                        <div id="members" style="line-height: 30px; word-break: normal; word-wrap: normal;"></div>
                    </ContentTemplate>
                </telerik:RadWindow>
                <telerik:RadWindow ID="attachments" runat="server" NavigateUrl="Upload.aspx" Title="附件上传" VisibleStatusbar="false" Modal="True" InitialBehaviors="Close" AutoSize="false" Behaviors="Close" Width="600" Height="450">
                </telerik:RadWindow>
            </Windows>
        </telerik:RadWindowManager>
        <telerik:RadCodeBlock ID="cb" runat="server">
            <script>
                function rea(ac) {
                    $("#acv").val("附件（" + ac + "）");
                }

                function toggle(obj) {
                    exp = !exp;
                    $(obj).val(exp ? "全部收缩" : "全部展开");
                    var treeView = $find("<%= tree.ClientID %>");
                    var nodes = treeView.get_allNodes();
                    for (var i = 0; i < nodes.length; i++) {
                        if (nodes[i].get_nodes() != null) {
                            if (exp)
                                nodes[i].expand();
                            else
                                nodes[i].collapse();
                        }
                    }
                }

                function openCount() {
                    $find("<%= users.ClientID %>").show();
                }

                function openUpload() {
                    $find("<%= attachments.ClientID %>").show();
                }

                function call(msg) {
                    var call_ids = $("#send_ids").val();
                    var call_msg = msg;
                    var call_val = $("#content").val();
                    if (!call_ids) {
                        window.radalert('请选择寻呼接收人员', 200, 90);
                        return;
                    }
                    if (!call_val) {
                        window.radalert('请填写寻呼内容', 200, 90);
                        return;
                    }
                    var push = call_ids + "@*@*@*@*@*@" + call_msg + "@*@*@*@*@*@" + call_val;
                    $find("<%= xp.ClientID %>").ajaxRequest(push);
                            }

            </script>
        </telerik:RadCodeBlock>
        <telerik:RadAjaxPanel ID="ap" runat="server" Style="height: 100%; margin: 0;">
            <telerik:RadSplitter ID="sp" runat="server" CssClass="bg" BorderStyle="None" Orientation="Horizontal" ResizeWithBrowserWindow="true" ResizeWithParentPane="true" LiveResize="true" Width="100%" Height="100%">
                <telerik:RadPane ID="tp" runat="server" Scrolling="Y" CssClass="bg" PersistScrollPosition="True" Height="43" BorderStyle="None">
                    <div class="container-fluid" style="border-bottom: solid 1px #666666;">
                        <div class="row rowx">
                            <div class="col-md-12">
                                <input class="btn btn-info" type="button" value="全部收缩" onclick="toggle(this);" />&nbsp;&nbsp;&nbsp;&nbsp;
                                <input id="toSearch" class="input" type="text" />&nbsp;&nbsp;
                                <input class="btn btn-info" type="button" value="查询" onclick="searchUser();" />&nbsp;&nbsp;&nbsp;&nbsp;
                                <input class="btn btn-info" type="button" value="刷新" onclick="refresh();" />
                                <div id="e" x="2" runat="server" class="btn btn-tumblr pull-right" onclick="list_members(this);">已选（0）</div>
                                <div id="d" runat="server" class="btn btn-warning pull-right" onclick="list_members(this);">在线</div>
                                <div id="c" runat="server" class="btn btn-success pull-right" onclick="list_members(this);">兼职</div>
                                <div id="b" runat="server" class="btn btn-danger pull-right" onclick="list_members(this);">在线</div>
                                <div id="a" runat="server" class="btn btn-info pull-right" onclick="list_members(this);">主职</div>
                            </div>
                        </div>
                    </div>
                </telerik:RadPane>
                <telerik:RadPane ID="mp" runat="server" Scrolling="Y" CssClass="bg searchArea" BorderStyle="None">
                    <telerik:RadTreeView ID="tree" runat="server" CssClass="t" Font-Size="15px" DataFieldID="Id" DataFieldParentID="ParentId" DataTextField="Name" DataValueField="Id">
                        <NodeTemplate>
                            <div id='<%# "b" + ((Guid)Eval("Id")).ToString("N") %>' class="btn btn-tumblr btn-rounded-lg btn-xs" onclick="checkDepartment(this);">
                                <%# Eval("Name") %>
                            </div>
                            <div>
                                <telerik:RadListView ID="view" runat="server" DataSource='<%# LoadUsers(Container.DataItem as M_寻呼) %>'>
                                    <ItemTemplate>
                                        <div x="1" tid='<%# Eval("TargetId") %>' title='<%# "主职单位：" + Eval("PriorName") + "&#10;手机号码：" + Eval("Phone") %>' class='<%# LoadColor(Container.DataItem as M_寻呼) %>' toclass='<%# LoadColor(Container.DataItem as M_寻呼) %>' onclick="checkUser(this);"><%# Eval("Name") %></div>
                                    </ItemTemplate>
                                </telerik:RadListView>
                            </div>
                        </NodeTemplate>
                    </telerik:RadTreeView>
                </telerik:RadPane>
                <telerik:RadPane ID="bp" runat="server" CssClass="bg" BorderStyle="None" Height="117">
                    <div class="container-fluid" style="border-top: solid 1px #666666;">
                        <div class="row rowx">
                            <div class="col-md-12">
                                <input class="btn btn-info" type="button" value="呼叫" onclick="call(0);" />&nbsp;&nbsp;&nbsp;&nbsp;
                                <input class="btn btn-info" type="button" value="呼叫+短信" onclick="call(1);" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                <input id="acv" class="btn btn-info" type="button" value="附件（0）" onclick="openUpload();" />
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-12">
                                <textarea id="content" rows="3" cols="1" class="input" style="width: 100%;"></textarea>
                            </div>
                        </div>
                    </div>
                    <div class="contain-fluid">
                        <input id="send_ids" type="hidden" />
                    </div>
                </telerik:RadPane>
            </telerik:RadSplitter>
        </telerik:RadAjaxPanel>
        <telerik:RadAjaxPanel ID="xp" runat="server" Style="height: 0; width: 0;" OnAjaxRequest="post_request"></telerik:RadAjaxPanel>
    </form>
</body>
</html>
