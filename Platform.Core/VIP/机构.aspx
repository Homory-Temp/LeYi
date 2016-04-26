<%@ Page Language="C#" AutoEventWireup="true" CodeFile="机构.aspx.cs" Inherits="VIP_机构" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>机构</title>
    <script src="../Content/jQuery/jquery.min.js"></script>
    <script>
        var o_items = [];
    </script>
    <style>
        div {
            font-family: "Segoe UI",Arial,Helvetica,sans-serif;
            font-size: 1em;
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
                <asp:ScriptReference Assembly="Telerik.Web.UI" Name="Telerik.Web.UI.ComboBox.RadComboBoxScripts.js" />
            </Scripts>
        </telerik:RadScriptManager>
        <telerik:RadAjaxPanel ID="ap" runat="server">
            <div style="width: 100%;">
                <div style="width: 55%; float: left;">
                    <telerik:RadTreeList ID="tree" runat="server" Skin="Metro" RenderMode="Lightweight" AutoGenerateColumns="false" AllowMultiItemSelection="true" ClientDataKeyNames="机构ID,机构名称,金和ID" OnNeedDataSource="tree_NeedDataSource" DataKeyNames="机构ID" ParentDataKeyNames="父级ID" HideExpandCollapseButtonIfNoChildren="true" OnPreRender="tree_PreRender" OnItemDataBound="tree_ItemDataBound">
                        <Columns>
                            <telerik:TreeListBoundColumn Display="false" DataField="机构ID" HeaderText="机构ID" UniqueName="机构ID"></telerik:TreeListBoundColumn>
                            <telerik:TreeListBoundColumn Display="false" DataField="金和ID" HeaderText="金和ID" UniqueName="金和ID"></telerik:TreeListBoundColumn>
                            <telerik:TreeListSelectColumn HeaderText="选择" UniqueName="选择" HeaderStyle-Width="60" ItemStyle-Width="60" ItemStyle-HorizontalAlign="Center"></telerik:TreeListSelectColumn>
                            <telerik:TreeListBoundColumn DataField="机构名称" HeaderText="机构名称" UniqueName="机构名称"></telerik:TreeListBoundColumn>
                            <telerik:TreeListBoundColumn DataField="机构拼音" HeaderText="机构拼音" UniqueName="机构拼音"></telerik:TreeListBoundColumn>
                        </Columns>
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="Left" />
                        <ClientSettings>
                            <Selecting AllowItemSelection="true" UseSelectColumnOnly="true" />
                            <ClientEvents OnItemSelected="ois" OnItemDeselected="oids" />
                        </ClientSettings>
                    </telerik:RadTreeList>
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
                    function ois(sender, args) {
                        var item = args.get_item();
                        var id = item.get_dataKeyValue("机构ID") + "@" + item.get_dataKeyValue("金和ID");
                        var name = item.get_dataKeyValue("机构名称");
                        while (item.get_parentItem()) {
                            item = item.get_parentItem();
                            name = item.get_dataKeyValue("机构名称") + "-" + name;
                        }
                        var o_i = new Object();
                        o_i.id = id;
                        o_i.name = name;
                        o_items[o_items.length] = o_i;
                        doPreview();
                    }

                    function oids(sender, args) {
                        var item = args.get_item();
                        var id = item.get_dataKeyValue("机构ID") + "@" + item.get_dataKeyValue("金和ID");
                        var name = item.get_dataKeyValue("机构名称");
                        while (item.get_parentItem()) {
                            item = item.get_parentItem();
                            name = item.get_dataKeyValue("机构名称") + "-" + name;
                        }
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
        </telerik:RadAjaxPanel>
    </form>
</body>
</html>
