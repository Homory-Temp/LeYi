<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ObjectMobile.aspx.cs" Inherits="Object" %>

<%@ Register Src="~/Menu/MenuMobile.ascx" TagPrefix="homory" TagName="Menu" %>
<%@ Register Src="~/StorageObject/ObjectImage.ascx" TagPrefix="homory" TagName="ObjectImage" %>
<%@ Register Src="~/StorageObject/ObjectImageOne.ascx" TagPrefix="homory" TagName="ObjectImageOne" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <meta http-equiv="x-ua-compatible" content="ie=edge, chrome=1" />
    <meta name="viewport" content="width=device-width, initial-scale=1, minimum-scale=1, maximum-scale=1" />
    <title>物资管理 - 物资管理</title>
    <script src="../Assets/javascripts/jquery.js"></script>
    <link href="../Assets/stylesheets/amazeui.min.css" rel="stylesheet">
    <link href="../Assets/stylesheets/admin.css" rel="stylesheet">
    <link href="../Assets/stylesheets/bootstrap.min.css" rel="stylesheet">
    <link href="../Assets/stylesheets/bootstrap-theme.min.css" rel="stylesheet">

    <script src="../Assets/javascripts/jquery.min.js"></script>
    <script src="../Assets/javascripts/amazeui.min.js"></script>
    <script src="../Assets/javascripts/app.js"></script>
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
<body style="max-width: 640px;">
    <form id="form" runat="server">
        <telerik:RadScriptManager ID="sm" runat="server"></telerik:RadScriptManager>
        <telerik:RadCodeBlock ID="cb" runat="server">
            <script>
                function w_add(id) {
                    var w = window.radopen("../StorageObject/ObjectAddPopup?Id=" + id + "&StorageId=" + "<%= StorageId %>", "w_add");
                    w.maximize();
                    return false;
                }
                function w_edit(id) {
                     window.open("../StorageObject/ObjectEditPopupMobile?Id=" + id + "&StorageId=" + "<%= StorageId %>");
                    return false;
                }
                function w_remove(id) {
                    var w = window.radopen("../StorageObject/ObjectRemovePopup?Id=" + id, "w_remove");
                    //w.maximize();
                    return false;
                }
                function rebind() {
                    $find("<%= ap.ClientID %>").ajaxRequest("Rebind");
                }

                function OpenClick(id) {
                    window.open("../StorageObject/ObjectEditPopupMobile?Id=" + id + "&StorageId=" + "<%= StorageId %>");
                    return false;
                    //var openUrl = "../StorageObject/ObjectWindow.aspx?Id=" + id + "&PlaceVisible=true";

                    //var iWidth = 800; //弹出窗口的宽度;

                    //var iHeight = 700; //弹出窗口的高度;

                    //var iTop = (window.screen.availHeight - 30 - iHeight) / 2; //获得窗口的垂直位置;

                    //var iLeft = (window.screen.availWidth - 10 - iWidth) / 2; //获得窗口的水平位置;

                    //window.open(openUrl, "", "height=" + iHeight + ", width=" + iWidth + ", top=" + iTop + ", left=" + iLeft);

                }
            </script>
        </telerik:RadCodeBlock>
        <telerik:RadWindowManager ID="wm" runat="server" Modal="true" Behaviors="None" CenterIfModal="true" ShowContentDuringLoad="true" VisibleStatusbar="false" ReloadOnShow="true" VisibleTitlebar="false">
            <Windows>
                <telerik:RadWindow ID="w_add" runat="server"></telerik:RadWindow>
                <telerik:RadWindow ID="w_edit" runat="server"></telerik:RadWindow>
                <telerik:RadWindow ID="w_remove" runat="server" Width="300" Height="200"></telerik:RadWindow>
            </Windows>
        </telerik:RadWindowManager>
        <homory:Menu runat="server" ID="menu" />
        <telerik:RadAjaxPanel ID="ap" runat="server">
<%--            <div class="grid-100 left" style="margin-top: 10px; display: none;">
                <div>
                    <telerik:RadTreeView ID="tree" runat="server" OnNodeClick="tree_NodeClick" DataTextField="Name" DataValueField="Id" DataFieldID="Id" DataFieldParentID="ParentId">
                    </telerik:RadTreeView>
                </div>
            </div>--%>

            <div class="grid-100 mobile-grid-100 left" style="margin-top: 70px;">

                <table width="600" align="center">
                    <tr>
                        <td width="200" style="color:#0E90D2;font-size:18px;font-weight:BOLD;">物资管理
                        </td>
                        <td width="400">
                            <telerik:RadTextBox ID="code" runat="server" MaxLength="12" EmptyMessage="请扫描或输入条码" width="300px"></telerik:RadTextBox></td>
                        <td>
                <asp:ImageButton ID="add" runat="server" AlternateText="查询" OnClick="add_Click" />
                        </td>
                    </tr>
                    <%--<tr>
                        <td colspan="2">
                            <span style="color: #FF0000;">
                                <asp:Label ID="target_content" runat="server" Visible="false"></asp:Label></span></td>
                    </tr>--%>
                </table>
            </div>

        <%--    <div class="grid-100 mobile-grid-100 right" style="max-width:640px;">
                <asp:ImageButton ID="view_type_grid" runat="server" AlternateText="网格模式" OnClick="view_type_grid_Click" ImageUrl="~/images/wg.jpg" title="网格模式" />
                <asp:ImageButton ID="view_type_list" runat="server" AlternateText="列表模式" OnClick="view_type_list_Click" ImageUrl="~/images/tb.jpg" title="列表模式" />
            </div>--%>

            <telerik:RadListView ID="list" runat="server" DataKeyNames="Id" ItemPlaceholderID="holderr" OnNeedDataSource="list_NeedDataSource" OnItemDataBound="list_ItemDataBound">
                <LayoutTemplate>
                    <div style="max-width:640px;">
                        <asp:PlaceHolder ID="holderr" runat="server"></asp:PlaceHolder>
                    </div>

                </LayoutTemplate>
                <ItemTemplate>

                    <div style="border: 1px solid #cdcdcd; min-height: 280px; width: 45%; margin: 10px; float: left;">
                        <table width="100%" border="0" cellspacing="0" cellpadding="0" align="center" style="margin: 10px;">
                            <tr>
                                <td colspan="4" height="5"></td>
                            </tr>
                            <tr>
                                <td colspan="4" id="lbl" runat="server" width="100%" align="center">
                                    <homory:ObjectImageOne ID="ObjectImage1" runat="server" ImageJson='<%# Eval("Image") %>' ImageWidth="200" ImageHeight="180" />
                                </td>
                            </tr>
                            <tr>
                                <td colspan="4" height="5"></td>
                            </tr>
                            <tr>
                                <td colspan="4" align="center"><b>
                                    <label id="label" style="color: #0E90D2;" onclick='OpenClick("<%#Eval("Id") %>")'><%# Eval("Name") %></label></b>
                                    <telerik:RadWindow ID="tip" runat="server" Modal="true" Behaviors="Close" CenterIfModal="true" ShowContentDuringLoad="true" VisibleTitlebar="false" VisibleStatusbar="false" ReloadOnShow="true"></telerik:RadWindow>
                                </td>
                            </tr>
                            <tr>
                                <td width="80" align="left" style="color: #808080;">库存：</td>
                                <td align="left" style="color: #808080;"><%# Eval("InAmount") %></td>
                                <td width="80" align="left" style="color: #808080;">单位：</td>
                                <td align="left" style="color: #808080;"><%# Eval("Unit") %>
                                </td>
                            </tr>

                            <tr>
                                <td colspan="4" height="5"></td>
                            </tr>

                            <tr>
                                <td width="" align="left" style="color: #808080;">编号：</td>
                                <td align="left" style="color: #808080;" colspan="3"><%# Eval("Code") %></td>

                                </td>
                            </tr>
                            <tr>
                                <td colspan="4" height="5"></td>
                            </tr>

                            <tr>
                                <td width="" align="left" style="color: #808080;">规格：</td>
                                <td align="left" style="color: #808080;" colspan="3">
                                    <div style="width: 165px; overflow: hidden; height: 20px;"><%# Eval("Specification") %></div>
                                </td>


                            </tr>
                            <tr>
                                <td colspan="2">
                                    <asp:ImageButton ID="edit" runat="server" AlternateText="编" class="btn btn-xs btn-default" CommandArgument='<%# Eval("Id") %>' OnClick="edit_Click" />
                                </td>
                            </tr>


                        </table>



                    </div>

                </ItemTemplate>
            </telerik:RadListView>

           <%-- <telerik:RadListView ID="listX" runat="server" Visible="false" DataKeyNames="Id" ItemPlaceholderID="holder" OnNeedDataSource="listX_NeedDataSource" OnItemDataBound="listX_ItemDataBound">
                <LayoutTemplate>

                    <table class="table table-bordered" style="margin-top: 10px; max-width: 640px;" align="center">
                        <thead>
                            <tr>
                                <th>序</th>
                                <th>物资编号</th>
                                <th>名称</th>

                                <th>规格</th>
                                <th>库存</th>

                            </tr>
                        </thead>
                        <tbody>
                            <asp:PlaceHolder ID="holder" runat="server"></asp:PlaceHolder>
                        </tbody>
                    </table>

                </LayoutTemplate>
                <ItemTemplate>
                    <tr>
                        <td align="left"><%# Eval("Ordinal") %></td>
                        <td align="left"><%# Eval("Code") %></td>
                        <td align="left">

                            <label id="labelx" runat="server" onclick='OpenClick("<%#Eval("Id") %>")'><%# Eval("Name") %></label>

                            <%--<telerik:RadWindow ID="tip" runat="server" Modal="true" Behaviors="Close" CenterIfModal="true" ShowContentDuringLoad="true" VisibleTitlebar="false" VisibleStatusbar="false" ReloadOnShow="true">
                                    <ContentTemplate>
                                        
                                    </ContentTemplate>
                                </telerik:RadWindow>--%>

                   <%--         <telerik:RadToolTip runat="server" ID="tip2" Skin="MetroTouch" IsClientID="true">
                                <homory:ObjectImageOne ID="ObjectImage" runat="server" ImageJson='<%# Eval("Image") %>' />
                            </telerik:RadToolTip>

                        </td>

                        <td align="left"><%# Eval("Specification") %></td>
                        <td align="left"><%# Eval("InAmount") %></td>

                    </tr>
                </ItemTemplate>
            </telerik:RadListView>--%>

        </telerik:RadAjaxPanel>
    </form>
    <style>
        html .zemine ul li {
            width: 33%;
            float: left;
            list-style: none;
        }
    </style>
    <script src="../Assets/javascripts/jquery.js"></script>
    <script src="../Assets/javascripts/common.js"></script>
</body>
</html>
