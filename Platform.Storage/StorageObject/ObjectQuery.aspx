<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ObjectQuery.aspx.cs" Inherits="ObjectQuery" %>

<%@ Register Src="~/Menu/Menu.ascx" TagPrefix="homory" TagName="Menu" %>
<%@ Register Src="~/StorageObject/ObjectImage.ascx" TagPrefix="homory" TagName="ObjectImage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <meta http-equiv="x-ua-compatible" content="ie=edge, chrome=1" />
    <meta name="viewport" content="width=device-width, initial-scale=1, minimum-scale=1, maximum-scale=1" />
    <title>物资管理 - 物资查询</title>
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
        <telerik:RadAjaxPanel ID="ap" runat="server" CssClass="grid-container">
            <div class="grid-25 mobile-grid-100 grid-parent left">
                <div>
                    <telerik:RadTreeView ID="tree" runat="server" OnNodeClick="tree_NodeClick" DataTextField="Name" DataValueField="Id" DataFieldID="Id" DataFieldParentID="ParentId">
                    </telerik:RadTreeView>
                </div>
            </div>
            <div class="grid-75 mobile-grid-100 grid-parent">
                <div class="grid-100 mobile-grid-100">
                    <telerik:RadSearchBox ID="search" runat="server" LocalizationPath="~/Language" EnableAutoComplete="false" OnSearch="search_Search"></telerik:RadSearchBox>
                </div>
                <telerik:RadListView ID="list" runat="server" DataKeyNames="Id" OnNeedDataSource="list_NeedDataSource" OnItemDataBound="list_ItemDataBound">
                    <ItemTemplate>
                        <div class="grid-25 mobile-grid-100">
                            <div>
                                顺序号：<%# Eval("Ordinal") %>
                                物资编号：<%# Eval("Code") %>
                                名称：<label id="lbl" runat="server"><%# Eval("Name") %></label>
                                <telerik:RadToolTip ID="tip" runat="server" IsClientID="true" Skin="MetroTouch">
                                    <homory:ObjectImage ID="ObjectImage1" runat="server" ImageJson='<%# Eval("Image") %>' />
                                </telerik:RadToolTip>
                            </div>
                            <div>
                                <asp:Image AlternateText="固" ID="fixed" Visible='<%# (bool)Eval("Fixed") %>' runat="server" />
                                <asp:Image AlternateText="易" ID="consumable" Visible='<%# (bool)Eval("Consumable") %>' runat="server" />
                            </div>
                            <div>
                                单位：<%# Eval("Unit") %>
                                规格：<%# Eval("Specification") %>
                            </div>
                            <div>
                                库存：<%# Eval("InAmount") %>
                                <asp:Image AlternateText="低" Visible='<%# (decimal)Eval("Low") > 0 && (decimal)Eval("Low") > (decimal)Eval("InAmount") %>' ID="low" runat="server" />
                                <asp:Image AlternateText="超" Visible='<%# (decimal)Eval("High") > 0 && (decimal)Eval("High") < (decimal)Eval("InAmount") %>' ID="high" runat="server" />
                            </div>
                            <div>
                                描述：<%# Eval("Note") %>
                            </div>
                        </div>
                    </ItemTemplate>
                </telerik:RadListView>
            </div>
        </telerik:RadAjaxPanel>
    </form>
    <script src="../Assets/javascripts/jquery.js"></script>
    <script src="../Assets/javascripts/common.js"></script>
</body>
</html>
