<%@ Page Language="C#" AutoEventWireup="true" CodeFile="用户.aspx.cs" Inherits="VIP_用户" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>用户</title>
    <script src="../Content/jQuery/jquery.min.js"></script>
    <script>
        var o_items = [];
    </script>
    <style>
        body {
            font-family: "Segoe UI",Arial,Helvetica,sans-serif;
            font-size: 14px;
        }

        table tr th, table tr td {
            border: solid 1px #2B2B2B;
        }

        table {
            border-collapse: collapse;
            width: 100%;
        }

        .vs {
            margin-top: 10px;
        }
    </style>
</head>
<body>
    <script>
        o_items = [];
    </script>
    <form id="form" runat="server">
        <telerik:RadScriptManager ID="sm" runat="server" LoadScriptsBeforeUI="true" EnablePartialRendering="true">
            <Scripts>
                <asp:ScriptReference Assembly="Telerik.Web.UI" Name="Telerik.Web.UI.Common.Core.js" />
                <asp:ScriptReference Assembly="Telerik.Web.UI" Name="Telerik.Web.UI.Common.jQuery.js" />
                <asp:ScriptReference Assembly="Telerik.Web.UI" Name="Telerik.Web.UI.Common.jQueryPlugins.js" />
            </Scripts>
        </telerik:RadScriptManager>

        <div style="width: 100%;">
            <div style="width: 55%; float: left;">
                <telerik:RadAjaxPanel ID="ap" runat="server">
                    <telerik:RadComboBox ID="box" runat="server" Width="500" AutoPostBack="true" OnSelectedIndexChanged="box_SelectedIndexChanged" AllowCustomText="true" MarkFirstMatch="true" Filter="Contains" DataTextField="Name" DataValueField="Id" Skin="MetroTouch"></telerik:RadComboBox>
                    <div class="vs">
                        <telerik:RadListView ID="view" runat="server" OnNeedDataSource="view_NeedDataSource" Skin="Metro" RenderMode="Lightweight" ItemPlaceholderID="holder">
                            <LayoutTemplate>
                                <table>
                                    <tr>
                                        <th>用户单位部门</th>
                                        <th>用户姓名</th>
                                        <th>选择</th>
                                        <th>用户手机</th>
                                        <th>用户证件</th>
                                    </tr>
                                    <asp:PlaceHolder ID="holder" runat="server"></asp:PlaceHolder>
                                </table>
                            </LayoutTemplate>
                            <ItemTemplate>
                                <tr>
                                    <td><%# Eval("用户部门") %></td>
                                    <td><%# Eval("用户姓名") %></td>
                                    <td>
                                        <input type="checkbox" match='<%# string.Format("{0}@{1}", Eval("用户ID"), Eval("金和ID")) %>' onchange="uis(this, 0, '<%# Eval("用户ID") %>', '<%# Eval("金和ID") %>', '<%# string.Format("{0}-{1}", Eval("用户单位"), Eval("用户姓名")) %>');" /></td>
                                    <td><%# Eval("用户手机") %></td>
                                    <td><%# Eval("用户证件") %></td>
                                </tr>
                            </ItemTemplate>
                            <AlternatingItemTemplate>
                                <tr style="background-color: #EBEBEB;">
                                    <td><%# Eval("用户部门") %></td>
                                    <td><%# Eval("用户姓名") %></td>
                                    <td>
                                        <input type="checkbox" match='<%# string.Format("{0}@{1}", Eval("用户ID"), Eval("金和ID")) %>' onchange="uis(this, 1,'<%# Eval("用户ID") %>', '<%# Eval("金和ID") %>', '<%# string.Format("{0}-{1}", Eval("用户单位"), Eval("用户姓名")) %>');" /></td>
                                    <td><%# Eval("用户手机") %></td>
                                    <td><%# Eval("用户证件") %></td>
                                </tr>
                            </AlternatingItemTemplate>
                        </telerik:RadListView>
                    </div>
                </telerik:RadAjaxPanel>
            </div>
            <div style="width: 5%; float: left; height: 100%;">&nbsp;</div>
            <div style="width: 40%; float: left;">
                <div id="preview" style="width: 100%;"></div>
            </div>
            <div style="clear: both;"></div>
            <input id="v" name="v" runat="server" type="hidden" />
            <div style="position: fixed; top: 10px; right: 10px;">
                <input id="sub" runat="server" type="button" style="width: 80px; height: 80px; cursor: pointer;" onserverclick="sub_ServerClick" value="继续" />
            </div>
        </div>
        <telerik:RadCodeBlock ID="cb" runat="server">
            <script>
                function uis(sender, alt, idx, sync, name) {
                    var id = idx + "@" + sync;
                    if (sender.checked) {
                        $(sender).parent().parent().css("background-color", "#ffd800");
                        ois(id, name);
                    } else {
                        $(sender).parent().parent().css("background-color", alt ? "#EBEBEB" : "#FFFFFF");
                        oids(id, name);
                    }
                }

                function matchUser() {
                    $("input[type=checkbox").each(function () {
                        var m = $(this).attr("match");
                        for (var i = 0; i < o_items.length; i++) {
                            if (o_items[i].id == m) {
                                this.checked = "checked";
                                $(this).parent().parent().css("background-color", "#ffd800");
                            }
                        }
                    });
                }

                function ois(id, name) {
                    var o_i = new Object();
                    o_i.id = id;
                    o_i.name = name;
                    o_items[o_items.length] = o_i;
                    doPreview();
                }

                function oids(id, name) {
                    for (var i = 0; i < o_items.length; i++) {
                        if (o_items[i].id == id) {
                            o_items[i] = null;
                        }
                    }
                    doPreview();
                }

                function doPreview() {
                    $("#preview").html("");
                    var x_items = [];
                    for (var i = 0; i < o_items.length; i++) {
                        if (o_items[i]) {
                            x_items[x_items.length] = o_items[i];
                            $("#preview").append(o_items[i].name);
                            $("#preview").append("<br />");
                        }
                    }
                    o_items = x_items;
                    $("input[name=v]").val(JSON.stringify(o_items));
                }
            </script>
        </telerik:RadCodeBlock>
    </form>
</body>
</html>
