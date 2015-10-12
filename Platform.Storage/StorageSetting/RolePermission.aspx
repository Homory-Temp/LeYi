<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RolePermission.aspx.cs" Inherits="RolePermission" %>

<%@ Register Src="~/Menu/Menu.ascx" TagPrefix="homory" TagName="Menu" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <meta http-equiv="x-ua-compatible" content="ie=edge, chrome=1" />
    <meta name="viewport" content="width=device-width, initial-scale=1, minimum-scale=1, maximum-scale=1" />
    <title>物资管理 - 角色权限</title>
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
                function w_add() {
                    var w = window.radopen("../StorageSetting/RolePermissionAddPopup?StorageId=" + "<%= StorageId %>", "w_add");
                    w.maximize();
                    return false;
                }
                function w_edit(id) {
                    var w = window.radopen("../StorageSetting/RolePermissionEditPopup?Id=" + id + "&StorageId=" + "<%= StorageId %>", "w_edit");
                    w.maximize();
                    return false;
                }
                function w_remove(id) {
                    var w = window.radopen("../StorageSetting/RolePermissionRemovePopup?Id=" + id + "&StorageId=" + "<%= StorageId %>", "w_remove");
                    w.maximize();
                    return false;
                }
                function rebind() {
                    $find("<%= list.ClientID %>").rebind();
                }
            </script>
        </telerik:RadCodeBlock>
        <telerik:RadWindowManager ID="wm" runat="server" Modal="true" Behaviors="None" CenterIfModal="true" ShowContentDuringLoad="true" VisibleStatusbar="false" ReloadOnShow="true">
            <Windows>
                <telerik:RadWindow ID="w_add" runat="server"></telerik:RadWindow>
                <telerik:RadWindow ID="w_edit" runat="server"></telerik:RadWindow>
                <telerik:RadWindow ID="w_remove" runat="server"></telerik:RadWindow>
            </Windows>
        </telerik:RadWindowManager>
        <homory:Menu runat="server" ID="menu" />
        <telerik:RadAjaxPanel ID="ap" runat="server" CssClass="grid-container">
            <div class="grid-100 mobile-grid-100 grid-parent">

                <table style="margin-left: 10px; margin-top: 10px; width: 80%;" align="center">
                    <thead>
                        <tr>
                            <td colspan="4" align="left" height="20"></td>
                        </tr>
                        <tr>
                            <td colspan="4" align="left"><strong class="am-text-primary am-text-lg">权限管理</strong>/  
                                <asp:ImageButton ID="add" runat="server" AlternateText="新增" OnClick="add_Click" class="btn btn-xs btn-info" /></td>
                        </tr>

                        <tr>
                            <td colspan="4" align="left" height="20"></td>
                        </tr>
                        <tr>
                            <th width="10%" style="background: #EFEFEF; height: 40px; text-align: center;">序号</th>
                            <th width="20%" style="background: #EFEFEF; height: 40px; text-align: center;">角色名</th>
                            <th width="70%" style="background: #EFEFEF; height: 40px; text-align: left;">权限</th>




                        </tr>


                        <telerik:RadListView ID="list" runat="server" DataKeyNames="Id" OnNeedDataSource="list_NeedDataSource">
                            <ItemTemplate>
                                <tr>
                                    <td align="center">
                                        <%# Eval("Ordinal") %>
                                    </td>
                                    <td align="center"><%# Eval("Name") %></td>
                                    <td align="left">
                                        <asp:Panel ID="p0" runat="server" Visible='<%# ((Models.State)Eval("State")) == Models.State.内置 %>'>

                                            <asp:Label ID="l1" runat="server" Text="购置入库"></asp:Label>
                                            <asp:Label ID="l2" runat="server" Text="领用借用"></asp:Label>
                                            <asp:Label ID="l3" runat="server" Text="物资报废"></asp:Label>
                                            <asp:Label ID="l4" runat="server" Text="查询打印"></asp:Label>
                                            <asp:Label ID="l5" runat="server" Text="权限设定"></asp:Label>
                                            <asp:Label ID="l6" runat="server" Text="报废审核"></asp:Label>
                                            <asp:ImageButton ID="edit0" runat="server" AlternateText="编辑" CommandArgument='<%# Eval("Id") %>' OnClick="edit_Click" />
                                        </asp:Panel>
                                        <asp:Panel ID="p1" runat="server" Visible='<%# ((Models.State)Eval("State")) == Models.State.启用 %>'>
                                            <telerik:RadButton ID="r1" runat="server" Text="购置入库" Value='<%# "+" + Eval("Id").ToString() %>' Checked='<%# (Container.DataItem as Models.StorageRole).StorageRoleRight.Count(o => o.Right == "+") > 0 %>' AutoPostBack="true" ButtonType="ToggleButton" ToggleType="CheckBox" OnClick="r_Click"></telerik:RadButton>
                                            <telerik:RadButton ID="r2" runat="server" Text="领用借用" Value='<%# "*" + Eval("Id").ToString() %>' Checked='<%# (Container.DataItem as Models.StorageRole).StorageRoleRight.Count(o => o.Right == "*") > 0 %>' AutoPostBack="true" ButtonType="ToggleButton" ToggleType="CheckBox" OnClick="r_Click"></telerik:RadButton>
                                            <telerik:RadButton ID="r3" runat="server" Text="物资报废" Value='<%# "-" + Eval("Id").ToString() %>' Checked='<%# (Container.DataItem as Models.StorageRole).StorageRoleRight.Count(o => o.Right == "-") > 0 %>' AutoPostBack="true" ButtonType="ToggleButton" ToggleType="CheckBox" OnClick="r_Click"></telerik:RadButton>
                                            <telerik:RadButton ID="r4" runat="server" Text="查询打印" Value='<%# "?" + Eval("Id").ToString() %>' Checked='<%# (Container.DataItem as Models.StorageRole).StorageRoleRight.Count(o => o.Right == "?") > 0 %>' AutoPostBack="true" ButtonType="ToggleButton" ToggleType="CheckBox" OnClick="r_Click"></telerik:RadButton>
                                            <telerik:RadButton ID="r5" runat="server" Text="权限设定" Value='<%# "!" + Eval("Id").ToString() %>' Checked='<%# (Container.DataItem as Models.StorageRole).StorageRoleRight.Count(o => o.Right == "!") > 0 %>' AutoPostBack="true" ButtonType="ToggleButton" ToggleType="CheckBox" OnClick="r_Click"></telerik:RadButton>
                                            <telerik:RadButton ID="r6" runat="server" Text="报废审核" Value='<%# "=" + Eval("Id").ToString() %>' Checked='<%# (Container.DataItem as Models.StorageRole).StorageRoleRight.Count(o => o.Right == "=") > 0 %>' AutoPostBack="true" ButtonType="ToggleButton" ToggleType="CheckBox" OnClick="r_Click"></telerik:RadButton>
                                            <asp:ImageButton ID="edit1" runat="server" AlternateText="编辑" CommandArgument='<%# Eval("Id") %>' OnClick="edit_Click" />
                                            <asp:ImageButton ID="remove1" runat="server" AlternateText="删除" CommandArgument='<%# Eval("Id") %>' OnClick="remove_Click" />
                                        </asp:Panel>
                                    </td>
                                </tr>
                            </ItemTemplate>
                        </telerik:RadListView>
                    </thead>
                </table>
            </div>
        </telerik:RadAjaxPanel>
    </form>
    <script src="../Assets/javascripts/jquery.js"></script>
    <script src="../Assets/javascripts/common.js"></script>
</body>
</html>
