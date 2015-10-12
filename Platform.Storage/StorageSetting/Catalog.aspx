<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Catalog.aspx.cs" Inherits="Catalog" %>

<%@ Register Src="~/Menu/Menu.ascx" TagPrefix="homory" TagName="Menu" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <meta http-equiv="x-ua-compatible" content="ie=edge, chrome=1" />
    <meta name="viewport" content="width=device-width, initial-scale=1, minimum-scale=1, maximum-scale=1" />
    <title>物资管理 - 分类管理</title>
    <!--[if lt IE 9]>
        <script src="../Assets/javascripts/html5.js"></script>
    <![endif]-->
    <!--[if (gt IE 8) | (IEMobile)]><!-->
    <link rel="stylesheet" href="../Assets/stylesheets/unsemantic-grid-responsive.css" />
    <!--<![endif]-->
    <!--[if (lt IE 9) & (!IEMobile)]>
        <link rel="stylesheet" href="../Assets/stylesheets/ie.css" />
    <![endif]-->
    <link href="../Assets/stylesheets/common.css" rel="stylesheet" />
</head>
<body>
    <form id="form" runat="server">
        <telerik:RadScriptManager ID="sm" runat="server"></telerik:RadScriptManager>
        <telerik:RadCodeBlock ID="cb" runat="server">
            <script>
                function w_add(id) {
                    var w = window.radopen("../StorageSetting/CatalogAddPopup?Id=" + id + "&StorageId=" + "<%= StorageId %>", "w_add");
                    w.maximize();
                    return false;
                }
                function w_edit(id) {
                    var w = window.radopen("../StorageSetting/CatalogEditPopup?Id=" + id, "w_edit");
                    w.maximize();
                    return false;
                }
                function w_remove(id) {
                    var w = window.radopen("../StorageSetting/CatalogRemovePopup?Id=" + id, "w_remove");
                    w.maximize();
                    return false;
                }
                function rebind() {
                    $find("<%= ap.ClientID %>").ajaxRequest("Rebind");
                }
            </script>
        </telerik:RadCodeBlock>
        <telerik:RadWindowManager ID="wm" runat="server" Modal="true" VisibleTitlebar="false" Behaviors="None" CenterIfModal="true" ShowContentDuringLoad="true" VisibleStatusbar="false" ReloadOnShow="true">
            <Windows>
                <telerik:RadWindow ID="w_add" runat="server"></telerik:RadWindow>
                <telerik:RadWindow ID="w_edit" runat="server"></telerik:RadWindow>
                <telerik:RadWindow ID="w_remove" runat="server"></telerik:RadWindow>
            </Windows>
        </telerik:RadWindowManager>
        <homory:Menu runat="server" ID="menu" />
        <telerik:RadAjaxPanel ID="ap" runat="server" OnAjaxRequest="ap_AjaxRequest" CssClass="grid-container">

            <div class="grid-20 left" style="margin-top: 10px; border-right: 1px solid #cdcdcd; min-height: 500px;">
                <div>
                    <telerik:RadTreeView ID="tree" runat="server" OnNodeClick="tree_NodeClick" DataTextField="Name" DataValueField="Id" DataFieldID="Id" DataFieldParentID="ParentId">
                    </telerik:RadTreeView>
                </div>
            </div>

            <div class="grid-75  mobile-grid-100 grid-parent">
                <div class="am-cf am-padding">
                    <div class="am-fl am-cf">
                        <strong class="am-text-primary am-text-lg">分类管理</strong>&nbsp;&nbsp;/
                             <asp:ImageButton ID="add" runat="server" AlternateText="新增" OnClick="add_Click" class="btn btn-xs btn-info" />
                    </div>
                </div>

                <table class="table table-bordered" style="margin-left: 10px;" align="center">
                    <thead>
                        <tr>
                            <th width="60">顺序号</th>
                            <th>分类编号</th>
                            <th>名称</th>                           
                            <th width="200" align="center">操作</th>
                        </tr>
                    </thead>
                    <tbody>
                        <telerik:RadListView ID="list" runat="server" DataKeyNames="Id" OnNeedDataSource="list_NeedDataSource">
                            <ItemTemplate>
                                <tr>
                                    <td align="center"><%# Eval("Ordinal") %></td>
                                    <td align="left"><%# Eval("code") %></td>
                                    <td align="left"><%# Eval("Name") %></td>
                                    <td align="left">
                                        <asp:ImageButton ID="edit" runat="server" AlternateText="编辑" CommandArgument='<%# Eval("Id") %>' OnClick="edit_Click"    class="btn btn-xs btn-default"/> <asp:ImageButton ID="remove" runat="server" AlternateText="删除" CommandArgument='<%# Eval("Id") %>' OnClick="remove_Click"   class="btn btn-xs btn-default" />
                                    </td>
                                </tr>
                            </ItemTemplate>
                        </telerik:RadListView>
                    </tbody>
                </table>
            </div>

        </telerik:RadAjaxPanel>
    </form>
    <script src="../Assets/javascripts/jquery.js"></script>
    <script src="../Assets/javascripts/common.js"></script>
</body>
</html>
