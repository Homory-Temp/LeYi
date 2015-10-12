<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Object.aspx.cs" Inherits="Object" %>

<%@ Register Src="~/Menu/Menu.ascx" TagPrefix="homory" TagName="Menu" %>
<%@ Register Src="~/StorageObject/ObjectImage.ascx" TagPrefix="homory" TagName="ObjectImage" %>
<%@ Register Src="~/StorageObject/ObjectImageOne.ascx" TagPrefix="homory" TagName="ObjectImageOne" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <meta http-equiv="x-ua-compatible" content="ie=edge, chrome=1" />
    <meta name="viewport" content="width=device-width, initial-scale=1, minimum-scale=1, maximum-scale=1" />
    <title>物资管理 - 物资管理</title>
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
                    var w = window.radopen("../StorageObject/ObjectAddPopup?Id=" + id + "&StorageId=" + "<%= StorageId %>", "w_add");
                    w.maximize();
                    return false;
                }
                function w_edit(id) {
                    var w = window.radopen("../StorageObject/ObjectEditPopup?Id=" + id + "&StorageId=" + "<%= StorageId %>", "w_edit");
                    w.maximize();
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

                    var openUrl = "../StorageObject/ObjectWindow.aspx?Id=" + id + "&PlaceVisible=true";

                    var iWidth = 800; //弹出窗口的宽度;

                    var iHeight = 700; //弹出窗口的高度;

                    var iTop = (window.screen.availHeight - 30 - iHeight) / 2; //获得窗口的垂直位置;

                    var iLeft = (window.screen.availWidth - 10 - iWidth) / 2; //获得窗口的水平位置;

                    window.open(openUrl, "", "height=" + iHeight + ", width=" + iWidth + ", top=" + iTop + ", left=" + iLeft);

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
        <telerik:RadAjaxPanel ID="ap" runat="server" CssClass="grid-container" OnAjaxRequest="ap_AjaxRequest">
            <div class="grid-15 left" style="margin-top: 10px; border-right: 1px solid #cdcdcd; min-height: 500px;">
                <div>
                    <telerik:RadTreeView ID="tree" runat="server" OnNodeClick="tree_NodeClick" DataTextField="Name" DataValueField="Id" DataFieldID="Id" DataFieldParentID="ParentId" EnableDragAndDropBetweenNodes="true" EnableDragAndDrop="true" OnNodeDrop="tree_NodeDrop">
                    </telerik:RadTreeView>
                </div>
            </div>
            <div class="grid-85 mobile-grid-100 grid-parent">
                <div class="grid-100 mobile-grid-100 left">
                    <div class="am-cf am-padding">
                        <div class="am-fl am-cf">
                            <strong class="am-text-primary am-text-lg">物资管理</strong>&nbsp;&nbsp;/
                             <asp:ImageButton ID="add" runat="server" AlternateText="新增" OnClick="add_Click" class="btn btn-xs btn-info" />
                            <telerik:RadSearchBox ID="search" runat="server" LocalizationPath="~/Language" EnableAutoComplete="false" OnSearch="search_Search" Width="300px"></telerik:RadSearchBox>
                            <span style="color: #FF0000;">
                                <asp:Label ID="target_content" runat="server" Visible="false"></asp:Label></span>
                        </div>
                    </div>
                </div>
                <div class="grid-100 mobile-grid-100 left">
                </div>
                <div class="grid-100 mobile-grid-100 right">
                    <asp:ImageButton ID="view_type_grid" runat="server" AlternateText="网格模式" OnClick="view_type_grid_Click" class="btn btn-xm btn-default" />
                    <asp:ImageButton ID="view_type_list" runat="server" AlternateText="列表模式" OnClick="view_type_list_Click" class="btn btn-xm btn-default" />
                </div>

                <telerik:RadListView ID="list" runat="server" DataKeyNames="Id" ItemPlaceholderID="holderr" OnNeedDataSource="list_NeedDataSource" OnItemDataBound="list_ItemDataBound">
                    <LayoutTemplate>
                        <div class="grid-100 mobile-grid-100 left" style="border: 1px solid #cdcdcd; margin: 10px; min-height: 500px;">
                            <asp:PlaceHolder ID="holderr" runat="server"></asp:PlaceHolder>
                        </div>

                    </LayoutTemplate>
                    <ItemTemplate>

                        <div class="grid-20 mobile-grid-100" style="border: 1px solid #F0F0F0; min-height: 340px; width: 230px; margin-right: 15px; margin-top: 10px;">


                            <table width="220" border="0" cellspacing="0" cellpadding="0">
                                <tr>
                                    <td colspan="4" height="5"></td>
                                </tr>
                                <tr>
                                    <td colspan="4" id="lbl" runat="server" width="100%">
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
                                    <td width="" align="left" style="color: #808080;">库存：</td>
                                    <td align="left" style="color: #808080;"><%# Eval("InAmount") %></td>
                                    <td width="" align="left" style="color: #808080;">单位：</td>
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
                                    <td align="left" style="color: #808080;" colspan="3"><div style="width:165px;overflow:hidden;height:20px;"><%# Eval("Specification") %></div></td>

                                   
                                </tr>

                                <tr>
                                    <td colspan="4" align="centers">
                                        <asp:ImageButton ID="in" runat="server" AlternateText="入库" Visible='<%# Right_In %>' ImageUrl="~/images/入1.png" CommandArgument='<%# Eval("Id") %>' OnClick="in_Click" />
                                        <asp:ImageButton ID="consume" runat="server" ImageUrl="~/images/领1.png" Visible='<%# (bool)Eval("Consumable") && (Container.DataItem as Models.StorageObject).InAmount > 0 %>' AlternateText="领用" CommandArgument='<%# Eval("Id") %>' OnClick="consume_Click" />
                                        <asp:ImageButton ID="lend" runat="server" ImageUrl="~/images/借1.png" Visible='<%# !(bool)Eval("Consumable") && (Container.DataItem as Models.StorageObject).InAmount > 0 %>' AlternateText="借用" CommandArgument='<%# Eval("Id") %>' OnClick="lend_Click" />

                                        <asp:ImageButton ID="out" runat="server" ImageUrl="~/images/废1.png" Visible='<%# (Container.DataItem as Models.StorageObject).InAmount > 0 %>' AlternateText="报废" CommandArgument='<%# Eval("Id") %>' OnClick="out_Click" />
                                        <asp:ImageButton ID="flow" runat="server" ImageUrl="~/images/流1.png" Visible='<%# (Container.DataItem as Models.StorageObject).StorageFlow.Count > 0 %>' AlternateText="流通" CommandArgument='<%# Eval("Id") %>' OnClick="flow_Click" />
                                        <asp:ImageButton ID="edit" runat="server" AlternateText="编" ImageUrl="~/images/编1.png" CommandArgument='<%# Eval("Id") %>' OnClick="edit_Click" Visible='<%# Right_Set %>' />
                                        <asp:ImageButton ID="remove" runat="server" AlternateText="删" ImageUrl="~/images/删1.png" CommandArgument='<%# Eval("Id") %>' OnClick="remove_Click" />
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="4" height="5"></td>
                                </tr>

                            </table>



                        </div>

                    </ItemTemplate>
                </telerik:RadListView>

                <telerik:RadListView ID="listX" runat="server" Visible="false" DataKeyNames="Id" ItemPlaceholderID="holder" OnNeedDataSource="listX_NeedDataSource" OnItemDataBound="listX_ItemDataBound">
                    <LayoutTemplate>

                        <table class="table table-bordered" style="margin-left: 10px; margin-top: 10px;" align="center">
                            <thead>
                                <tr>
                                    <th width="60">顺序号</th>
                                    <th>物资编号</th>
                                    <th width="350">名称</th>
                                    <th width="60">单位</th>
                                    <th width="60">规格</th>
                                    <th width="80">库存</th>
                                    <td align="center" width="250"><b>操作</b></td>
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

                                <telerik:RadToolTip runat="server" ID="tip2" Skin="MetroTouch" IsClientID="true">
                                    <homory:ObjectImageOne ID="ObjectImage" runat="server" ImageJson='<%# Eval("Image") %>' />
                                </telerik:RadToolTip>
                                                                
                            </td>
                            <td><%# Eval("Unit") %></td>
                            <td align="left"><%# Eval("Specification") %></td>
                            <td align="left"><%# Eval("InAmount") %></td>
                            <td align="right" width="120">
                                <table>
                                    <tr>
                                        <td width="60">
                                            <asp:ImageButton ID="in" ImageUrl="~/images/入.png" runat="server" AlternateText="入库" CommandArgument='<%# Eval("Id") %>' OnClick="in_Click" />
                                        </td>
                                        <td align="left" width="60">
                                            <asp:ImageButton ID="consume" ImageUrl="~/images/领.png" runat="server" Visible='<%# (bool)Eval("Consumable") && (Container.DataItem as Models.StorageObject).InAmount > 0 %>' AlternateText="领用" CommandArgument='<%# Eval("Id") %>' OnClick="consume_Click" />

                                            <asp:ImageButton ID="lend" runat="server" ImageUrl="~/images/借.png" Visible='<%# !(bool)Eval("Consumable") && (Container.DataItem as Models.StorageObject).InAmount > 0 %>' AlternateText="借用" CommandArgument='<%# Eval("Id") %>' OnClick="lend_Click" />
                                        </td>
                                        <td align="left" width="60">

                                            <asp:ImageButton ID="out" runat="server" ImageUrl="~/images/废.png" Visible='<%# (Container.DataItem as Models.StorageObject).InAmount > 0 %>' AlternateText="报废" CommandArgument='<%# Eval("Id") %>' OnClick="out_Click" />
                                        </td>
                                        <td align="center" width="60">

                                            <asp:ImageButton ID="edit" runat="server" AlternateText="编" class="btn btn-xs btn-default" CommandArgument='<%# Eval("Id") %>' OnClick="edit_Click" />
                                        </td>
                                        <td align="center" width="60">

                                            <asp:ImageButton ID="remove" runat="server" class="btn btn-xs btn-default" AlternateText="删" CommandArgument='<%# Eval("Id") %>' OnClick="remove_Click" />
                                        </td>
                                    </tr>
                                </table>
                            </td>

                        </tr>
                    </ItemTemplate>
                </telerik:RadListView>
            </div>
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
