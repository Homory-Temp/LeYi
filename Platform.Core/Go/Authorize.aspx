<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Authorize.aspx.cs" Inherits="Go.GoAuthorize" %>

<%@ Import Namespace="Homory.Model" %>

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
    <link href="../Content/Homory/css/common.css" rel="stylesheet" />
    <link href="../Content/Core/css/common.css" rel="stylesheet" />
    <script src="../Content/Homory/js/common.js"></script>
    <script src="../Content/Homory/js/notify.min.js"></script>
    <!--[if lt IE 9]>
	    <script src="../Content/Homory/js/html5shiv.js"></script>
	    <script src="../Content/Homory/js/respond.min.js"></script>
    <![endif]-->
    <style>
        html .RadSearchBox, html .RadSearchBox .rsbInner, html .RadSearchBox .rsbInput {
            text-align: center;
            vertical-align: middle;
            width: 100px;
            height: 18px;
            line-height: 18px;
            font-size: 12px;
            margin-top: -1px;
        }

            html .RadSearchBox .rsbButtonSearch {
                width: 18px;
                height: 18px;
                margin: 0;
                cursor: pointer;
            }
    </style>
</head>
<body>
    <form id="formHome" runat="server">
        <div>
            <homory:SideBar runat="server" ID="SideBar" />
        </div>
        <telerik:RadAjaxPanel ID="panel" runat="server" CssClass="container-fluid" LoadingPanelID="loading">
            <div class="row">
                <div class="col-md-6">
                    <h6 class="ui teal header"><i class="ui teal circle icon"></i>教师</h6>
                    <telerik:RadComboBox ID="combo" runat="server" AutoPostBack="true" OnSelectedIndexChanged="combo_SelectedIndexChanged" DataTextField="Name" DataValueField="Id" Label="选择学校：" Width="220px" Filter="Contains" MarkFirstMatch="true" AllowCustomText="true" Height="202px">
                        <ItemTemplate>
                            <%# Eval("Name") %><%--<%# CountChildren(Container.DataItem as Department) %>--%>
                        </ItemTemplate>
                    </telerik:RadComboBox>
                    &nbsp;&nbsp;
                    <telerik:RadSearchBox ID="peek" runat="server" OnSearch="peek_Search" EmptyMessage="查找...." EnableAutoComplete="false">
                    </telerik:RadSearchBox>
                </div>
                <div class="col-md-6">
                    <h6 class="ui purple header"><i class="ui purple circle icon"></i>角色</h6>
                </div>
            </div>
            <div class="row">
                <div class="col-md-6">
                    <table>
                        <tr class="coreTop">
                            <td>
                                <telerik:RadTreeView ID="tree" runat="server" Width="250" DataTextField="Name" DataValueField="Id" DataFieldID="Id" DataFieldParentID="ParentId" OnNodeClick="tree_NodeClick" EnableDragAndDrop="False">
                                    <NodeTemplate>
                                        <%# ForceTreeName(Container.DataItem as Homory.Model.Department) %>
                                    </NodeTemplate>
                                </telerik:RadTreeView>
                            </td>
                            <td>
                                <telerik:RadListView ID="view" runat="server" DataKeyNames="Id" ClientDataKeyNames="Id" OnNeedDataSource="view_OnNeedDataSource" OnItemDrop="view_OnItemDrop">
                                    <ItemTemplate>
                                        <%-- ReSharper disable UnknownCssClass --%>
                                        <div id='<%# Eval("Id") %>' class="rlvI RadTreeView_Default rlvDrag rootPointer ui basic segment left floated" onmousedown="Telerik.Web.UI.RadListView.HandleDrag(event, '<%# Container.OwnerListView.ClientID %>', <%# Container.DisplayIndex%>);">
                                            <%-- ReSharper restore UnknownCssClass --%>
                                            <i class="ui teal circle icon"></i>&nbsp;<%# Eval("RealName") %>
                                        </div>
                                    </ItemTemplate>
                                    <ClientSettings AllowItemsDragDrop="true"></ClientSettings>
                                </telerik:RadListView>
                            </td>
                        </tr>
                    </table>
                </div>
                <div class="col-md-6">
                    <table>
                        <tr class="coreTop">
                            <td>
                                <telerik:RadListView ID="viewX" runat="server" DataKeyNames="Id" ClientDataKeyNames="Id" OnNeedDataSource="viewX_OnNeedDataSource" OnItemDrop="viewX_OnItemDrop">
                                    <ItemTemplate>
                                        <%-- ReSharper disable UnknownCssClass --%>
                                        <div id='<%# Eval("Id") %>' class="rlvI RadTreeView_Default rlvDrag rootPointer ui basic segment left floated" onmousedown="Telerik.Web.UI.RadListView.HandleDrag(event, '<%# Container.OwnerListView.ClientID %>', <%# Container.DisplayIndex%>);">
                                            <%-- ReSharper restore UnknownCssClass --%>
                                            <i class="ui purple circle icon"></i>&nbsp;<%# Eval("Name") %>
                                        </div>
                                    </ItemTemplate>
                                    <ClientSettings AllowItemsDragDrop="true"></ClientSettings>
                                </telerik:RadListView>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <div class="ui divider"></div>
                                <h6 class="ui red header"><i class="ui red circle icon"></i>授权列表</h6>
                                <telerik:RadGrid ID="grid" runat="server" AllowPaging="True" AutoGenerateColumns="false" LocalizationPath="../Language" AllowSorting="True" GridLines="None" OnNeedDataSource="grid_OnNeedDataSource" OnDeleteCommand="grid_OnDeleteCommand">
                                    <MasterTableView DataKeyNames="UserId,RoleId" CommandItemDisplay="None" HorizontalAlign="NotSet" ShowHeader="true" ShowHeadersWhenNoRecords="true" NoMasterRecordsText="">
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                        <AlternatingItemStyle HorizontalAlign="Center"></AlternatingItemStyle>
                                        <Columns>
                                            <telerik:GridBoundColumn HeaderText="教师" DataField="UserName" SortExpression="UserName" UniqueName="UserName">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn HeaderText="角色" DataField="RoleName" SortExpression="RoleName" UniqueName="RoleName">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridButtonColumn HeaderText="" CommandName="Delete" ButtonType="ImageButton" />
                                        </Columns>
                                        <PagerStyle Mode="NextPrevAndNumeric" PageSizes="10,20,50,100" Position="Bottom" PageSizeControlType="RadComboBox" AlwaysVisible="true" PagerTextFormat="{4} 第{0}页，共{1}页；第{2}-{3}项，共{5}项" />
                                    </MasterTableView>
                                </telerik:RadGrid>
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
        </telerik:RadAjaxPanel>
    </form>
</body>
</html>
