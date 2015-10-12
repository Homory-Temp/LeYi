<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Storage.aspx.cs" Inherits="Storage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <meta http-equiv="x-ua-compatible" content="ie=edge, chrome=1" />
    <meta name="viewport" content="width=device-width, initial-scale=1, minimum-scale=1, maximum-scale=1" />
    <link href="../Assets/stylesheets/amazeui.min.css" rel="stylesheet" />
    <link href="../Assets/stylesheets/admin.css" rel="stylesheet" />
    <link href="../Assets/stylesheets/bootstrap.min.css" rel="stylesheet" />
    <link href="../Assets/stylesheets/bootstrap-theme.min.css" rel="stylesheet" />

    <script src="../Assets/javascripts/jquery.min.js"></script>
    <script src="../Assets/javascripts/amazeui.min.js"></script>
    <script src="../Assets/javascripts/app.js"></script>
    <title>物资管理 - 仓库</title>
    <!--[if lt IE 9]>
        <script src="../Assets/javascripts/html5.js"></script>
    <![endif]-->
    <!--[if (gt IE 8) | (IEMobile)]><!-->
    <link rel="stylesheet" href="../Assets/stylesheets/unsemantic-grid-responsive.css" />
    <!--<![endif]-->
    <!--[if (lt IE 9) & (!IEMobile)]>
        <link rel="stylesheet" href="../Assets/stylesheets/ie.css" />
    <![endif]-->

</head>
<body>
    <form id="form" runat="server">
        <telerik:RadScriptManager ID="sm" runat="server"></telerik:RadScriptManager>
        <telerik:RadCodeBlock ID="cb" runat="server">
            <script>
                function w_add() {
                    var w = window.radopen("../Storage/StorageAddPopup", "w_add");
                    w.maximize();
                    return false;
                }
                function w_edit(id) {
                    var w = window.radopen("../Storage/StorageEditPopup?Id=" + id, "w_edit");
                    w.maximize();
                    return false;
                }
                function w_remove(id) {
                    var w = window.radopen("../Storage/StorageRemovePopup?Id=" + id, "w_remove");
                    w.maximize();
                    return false;
                }
                function rebind() {
                    $find("<%= list.ClientID %>").rebind();
                }
            </script>
        </telerik:RadCodeBlock>
        <telerik:RadWindowManager ID="wm" runat="server" Modal="true" Behaviors="None" CenterIfModal="true" VisibleTitlebar="false" ShowContentDuringLoad="true" VisibleStatusbar="false" ReloadOnShow="true">
            <Windows>
                <telerik:RadWindow ID="w_add" runat="server"></telerik:RadWindow>
                <telerik:RadWindow ID="w_edit" runat="server"></telerik:RadWindow>
                <telerik:RadWindow ID="w_remove" runat="server"></telerik:RadWindow>
            </Windows>
        </telerik:RadWindowManager>
        <header class="am-topbar admin-header">
            <style>
                #sa a:hover {
                    text-decoration: none;
                    color: white;
                }
            </style>
            <div id="sa" class="am-topbar-brand">
              <a href="../Storage/Storage.aspx"><img src="../StorageCode/Icon.png" /><strong>云物资管理平台</strong> </a>
            </div>
            <div class="am-collapse am-topbar-collapse" id="topbar-collapse">
                <ul
                    class="am-nav am-nav-pills am-topbar-nav am-topbar-right admin-header-list">

                    <li class="am-dropdown" data-am-dropdown=""><a class="am-dropdown-toggle"
                        href="javascript:;" data-am-dropdown-toggle="am-dropdown-content"><span
                            class="am-icon-users"></span>您好：
                        <label id="name" runat="server"></label>
                        <span class="am-icon-caret-down"></span>
                    </a>
                        <ul class="am-dropdown-content">
                            <%--                            <li><a href="#"><span class="am-icon-user"></span>
                                资料</a></li>
                            <li><a href="#"><span class="am-icon-cog"></span>
                                设置</a></li>--%>
                            <li><a id="off" runat="server" onserverclick="off_ServerClick"><span class="am-icon-power-off"></span>
                                退出</a></li>
                        </ul>
                    </li>
                    <li class="am-hide-sm-only"><a id="admin-fullscreen" href="javascript:;"><span
                        class="am-icon-arrows-alt"></span><span
                            class="admin-fullText">开启全屏</span></a></li>
                </ul>
            </div>
        </header>
        <telerik:RadAjaxPanel ID="ap" runat="server" CssClass="grid-container">
            <div class="am-cf admin-main">
                <!-- content start -->
                <div class="admin-content">
                    <div class="am-cf am-padding" style="border-bottom: 1px solid #E1E1E1;">
                        <div class="am-fl am-cf">
                            <strong class="am-text-primary am-text-lg">仓库管理</strong>
                            /
                      <asp:ImageButton ID="add" runat="server" AlternateText="新增" OnClick="add_Click" class="btn btn-xm btn-info" />
                        </div>
                    </div>

                    <div class="grid-container" style="min-height: 480px; margin: 0 auto;">


                        <telerik:RadListView ID="list" runat="server" DataKeyNames="Id" OnNeedDataSource="list_NeedDataSource">
                            <ItemTemplate>

                                <div class="grid-33 mobile-grid-80" style="margin-top: 30px; text-align: center;" id="storageDiv" runat="server" visible='<%# HasRight((Guid)Eval("Id")) %>'>
                                    <div class="grid-100">
                                        <a href='<%# GenerateUrl(Eval("Id")) %>' style="color: #ffffff; font-weight: BOLD;">
                                            <img src="../images/store.png"></a>
                                    </div>
                                    <div class="btn btn-xm btn-info" style="height: 45px; margin: 15px 0px; min-width: 150px; color: #ffffff;">
                                        <a href='<%# GenerateUrl(Eval("Id")) %>' style="color: #ffffff; font-weight: BOLD;">
                                            <!--<%# Eval("Ordinal") %>、-->
                                            <%# Eval("Name") %></a>
                                    </div>
                                    <div class="grid-100" style="height: 45px;">
                                        <asp:ImageButton ID="edit" runat="server" AlternateText="编辑" Visible='<%#GetVisible() %>' CommandArgument='<%# Eval("Id") %>' OnClick="edit_Click" class="btn btn-xm btn-default" />
                                        <asp:ImageButton ID="remove" runat="server" AlternateText="删除" Visible='<%#GetVisible() %>' CommandArgument='<%# Eval("Id") %>' OnClick="remove_Click" class="btn btn-xm btn-default" />
                                    </div>
                                </div>
                            </ItemTemplate>
                        </telerik:RadListView>
                    </div>
                </div>
            </div>
        </telerik:RadAjaxPanel>

        <!-- content end -->
        <footer>
            <hr>

            <p class="am-margin-center" style="height: 50px; margin: 0 auto; text-align: center;">
                © 2015 版权所有 乐翼教育云物资管理   
            </p>
        </footer>

    </form>
    <script src="../Assets/javascripts/jquery.js"></script>
    <script src="../Assets/javascripts/common.js"></script>
</body>
</html>
