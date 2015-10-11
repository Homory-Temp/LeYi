<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Learned.aspx.cs" Inherits="Go.GoLearned" %>

<%@ Register Src="~/Control/SideBar.ascx" TagPrefix="homory" TagName="SideBar" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=Edge,Chrome=1" />
    <meta name="viewport" content="width=device-width,initial-scale=1,minimum-scale=1,maximum-scale=1" />
    <title>基础平台</title>
	<script src="../Content/jQuery/jquery.min.js"></script>
    <link href="../assets/css/bootstrap.min.css" rel="stylesheet" />
    <link href="../assets/css/style-responsive.css" rel="stylesheet" />
    <link href="../assets/css/style.css" rel="stylesheet" />
    <script src="../assets/js/bootstrap.min.js"></script>
    <link href="../Content/Semantic/css/semantic.min.css" rel="stylesheet" />
    <link href="../Content/Homory/css/common.css" rel="stylesheet" />
    <link href="../Content/Core/css/common.css" rel="stylesheet" />
    <script src="../Content/Semantic/javascript/semantic.min.js"></script>
    <script src="../Content/Homory/js/common.js"></script>
    <script src="../Content/Homory/js/notify.min.js"></script>
    <!--[if lt IE 9]>
	    <script src="../Content/Homory/js/html5shiv.js"></script>
	    <script src="../Content/Homory/js/respond.min.js"></script>
    <![endif]-->
</head>
<body>
    <form id="formHome" runat="server">
        <div>
            <homory:SideBar runat="server" ID="SideBar" />
        </div>
        <telerik:RadAjaxPanel ID="panel" runat="server" CssClass="ui left aligned stackable page grid" Style="margin: 0; padding: 0;" LoadingPanelID="loading">
            <div class="sixteen wide column">
                <telerik:RadComboBox ID="combo" runat="server" AutoPostBack="true" OnSelectedIndexChanged="combo_SelectedIndexChanged" DataTextField="Name" DataValueField="Id" Label="选择学校：" Width="220px" Filter="Contains" MarkFirstMatch="true" AllowCustomText="true" Height="202px">
                    <ItemTemplate>
                        <span><%# GenerateTreeName((Homory.Model.Department)Container.DataItem, Container.Index, 0) %></span>
                    </ItemTemplate>
                </telerik:RadComboBox>
            </div>
            <div class="sixteen wide column">
                <table class="coreAuto">
                    <tr class="coreTop">
                        <td>
                            <telerik:RadTreeView ID="tree" runat="server" EnableDragAndDrop="true" EnableDragAndDropBetweenNodes="false" DataTextField="Name" DataValueField="Id" DataFieldID="Id" DataFieldParentID="ParentId" OnNodeClick="tree_NodeClick">
                                <NodeTemplate>
                                    <i class='<%# FormatTreeNode(Container.DataItem) %>'></i>&nbsp;<%# GenerateTreeName((Homory.Model.Department)Container.DataItem, Container.Index, Container.Level) %>
                                </NodeTemplate>
                            </telerik:RadTreeView>
                        </td>
                        <td>&nbsp;</td>
                        <td class="coreFull">
                            <div class="ui basic segment">
                                <telerik:RadListView ID="view" runat="server" ItemPlaceholderID="holderS" DataKeyNames="Id" ClientDataKeyNames="Id" OnNeedDataSource="view_NeedDataSource">
                                    <LayoutTemplate>
                                        <div class="ui left aligned grid" style="margin-left: 60px;">
                                            <asp:PlaceHolder ID="holderS" runat="server"></asp:PlaceHolder>
                                            <div style="clear: both;"></div>
                                        </div>
                                    </LayoutTemplate>
                                    <ItemTemplate>
                                        <div class="rootPointer ui five wide column">
                                            <telerik:RadButton runat="server" CssClass='<%# HandleButton(Container.DataItem as Homory.Model.Catalog) %>' Text='<%# Iconed(Container.DataItem as Homory.Model.Catalog) %>' CommandArgument='<%# Eval("Id") %>' ButtonType="ToggleButton" OnClick="OnClick" AutoPostBack="True">
                                            </telerik:RadButton>
                                        </div>
                                    </ItemTemplate>
                                    <ClientSettings AllowItemsDragDrop="true"></ClientSettings>
                                </telerik:RadListView>
                            </div>
                        </td>
                    </tr>
                </table>
            </div>
        </telerik:RadAjaxPanel>
    </form>
</body>
</html>
