<%@ Page Language="C#" AutoEventWireup="true" CodeFile="机构.aspx.cs" Inherits="VIP_机构" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>机构</title>
    <script>
        function ois(sender, args) {
            var id = args.get_item().get_dataKeyValue("机构ID");
            var name = args.get_item().get_dataKeyValue("机构名称");
            var pid = args.get_item().get_dataKeyValue("父级ID");
        }

        function oids(sender, args) {
        }
   </script>
</head>
<body>
    <form id="form" runat="server">
        <telerik:RadScriptManager ID="sm" runat="server"></telerik:RadScriptManager>
        <div style="width: 100%;">
            <div style="width: 48%; float: left;">
                <telerik:RadAjaxPanel ID="apl" runat="server">
                    <telerik:RadTreeList ID="tree" runat="server" Skin="Metro" RenderMode="Lightweight" AutoGenerateColumns="false" AllowMultiItemSelection="true" ClientDataKeyNames="机构ID,机构名称,父级ID" OnNeedDataSource="tree_NeedDataSource" DataKeyNames="机构ID" ParentDataKeyNames="父级ID" HideExpandCollapseButtonIfNoChildren="true" OnPreRender="tree_PreRender" OnItemDataBound="tree_ItemDataBound">
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
                </telerik:RadAjaxPanel>
            </div>
            <div style="width: 4%; float: left; height: 100%;">&nbsp;</div>
            <div style="width: 48%; float: left; position: static; top: 10px;">
                <telerik:RadAjaxPanel ID="apr" runat="server">
                    <telerik:RadButton ID="move" runat="server" Text="加入" Skin="Metro" OnClick="move_Click"></telerik:RadButton>
                </telerik:RadAjaxPanel>
            </div>
            <div style="clear: both;"></div>
        </div>
    </form>
</body>
</html>
