﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Taught.aspx.cs" Inherits="Go.GoTaught" %>

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
    <link href="../Content/Core/css/treefix.css" rel="stylesheet" />
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
                    <div class="btn btn-primary"><i class="ui teal circle icon"></i>教师</div><br /><br />
                    <div>
                    <telerik:RadComboBox ID="combo" runat="server" AutoPostBack="true" OnSelectedIndexChanged="combo_SelectedIndexChanged" DataTextField="Name" DataValueField="Id" Label="选择学校：" Width="220px" Filter="Contains" MarkFirstMatch="true" AllowCustomText="true" Height="202px">
                        <ItemTemplate>
                            <%# Eval("Name") %><%--<%# CountChildren(Container.DataItem as Department) %>--%>
                        </ItemTemplate>
                    </telerik:RadComboBox>
                    &nbsp;&nbsp;
                    <telerik:RadSearchBox ID="peek" runat="server" OnSearch="peek_Search" EmptyMessage="查找...." EnableAutoComplete="false">
                    </telerik:RadSearchBox>
                    </div>
                </div>
                <div class="col-md-6">
                    <div class="btn btn-danger"><i class="ui purple circle icon"></i>班级课程</div><br /><br />
                    <div>
                    <telerik:RadComboBox ID="comboX" runat="server" AutoPostBack="true" OnSelectedIndexChanged="comboX_SelectedIndexChanged" DataTextField="Name" DataValueField="Id" Label="选择学校：" Width="220px" Filter="Contains" MarkFirstMatch="true" AllowCustomText="true" Height="202px">
                        <ItemTemplate>
                            <%# Eval("Name") %><%--<%# CountChildren(Container.DataItem as Department) %>--%>
                        </ItemTemplate>
                    </telerik:RadComboBox>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-md-6">
                    <table>
                        <tr class="coreTop">
                            <td>
                                <telerik:RadTreeView ID="tree" runat="server" Width="250" DataTextField="Name" DataValueField="Id" DataFieldID="Id" DataFieldParentID="ParentId" OnNodeClick="tree_NodeClick" EnableDragAndDrop="False">
                                    <NodeTemplate>
                                        <%# Eval("Name").ToString() %>
                                    </NodeTemplate>
                                </telerik:RadTreeView>
                            </td>
                            <td>&nbsp;</td>
                            <td>
                                <telerik:RadListView ID="view" runat="server" DataKeyNames="Id" ClientDataKeyNames="Id" OnNeedDataSource="view_OnNeedDataSource" OnItemDrop="view_OnItemDrop">
                                    <ItemTemplate>
                                        <%-- ReSharper disable UnknownCssClass --%>
                                        <div id='<%# Eval("Id") %>' class="rlvI RadTreeView_Default rlvDrag rootPointer ui basic segment left floated" onmousedown="Telerik.Web.UI.RadListView.HandleDrag(event, '<%# Container.OwnerListView.ClientID %>', <%# Container.DisplayIndex%>);">
                                            <%-- ReSharper restore UnknownCssClass --%>
                                            <span class='<%# GetIcon(Container.DataItem as Homory.Model.ViewTeacher) %>'>&nbsp;&nbsp;&nbsp;&nbsp;</span>&nbsp;<%# Eval("RealName") %>
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
                                <telerik:RadTreeView ID="treeX" runat="server" Width="250" DataTextField="Name" DataValueField="Id" DataFieldID="Id" DataFieldParentID="ParentId" OnNodeClick="treeX_NodeClick" EnableDragAndDrop="False">
                                    <NodeTemplate>
                                        <%# GenerateTreeName((Homory.Model.Department)Container.DataItem, Container.Index, Container.Level) %>
                                    </NodeTemplate>
                                </telerik:RadTreeView>
                            </td>
                            <td>&nbsp;</td>
                            <td>
                                <telerik:RadListView ID="viewX" runat="server" DataKeyNames="DepartmentId,CourseId" ClientDataKeyNames="DepartmentId,CourseId" OnNeedDataSource="viewX_OnNeedDataSource" OnItemDrop="viewX_OnItemDrop">
                                    <ItemTemplate>
                                        <%-- ReSharper disable UnknownCssClass --%>
                                        <div id='<%# Eval("CourseId") %>' style="line-height: 30px; vertical-align: middle;" class="rlvI RadTreeView_Default rlvDrag rootPointer ui basic segment" onmousedown="Telerik.Web.UI.RadListView.HandleDrag(event, '<%# Container.OwnerListView.ClientID %>', <%# Container.DisplayIndex%>);">
                                            <%-- ReSharper restore UnknownCssClass --%>
                                            <span class='<%# GetTeacher(Guid.Parse(treeX.SelectedNode.Value), (Guid)Eval("CourseId")) == null ? "badge-default" : "badge-danger" %>'>&nbsp;&nbsp;&nbsp;&nbsp;</span>&nbsp;<%# Eval("CourseName") %>&nbsp;<%# GetTeacher(Guid.Parse(treeX.SelectedNode.Value), (Guid)Eval("CourseId")) %>&nbsp;<asp:ImageButton ID="toDel" runat="server" Visible='<%# HasTeacher(Guid.Parse(treeX.SelectedNode.Value), (Guid)Eval("CourseId")) %>' ImageUrl="~/Images/Common/Delete.png" CommandArgument='<%# Eval("CourseId") %>' OnClick="toDel_Click" />
                                        </div>
                                    </ItemTemplate>
                                    <ClientSettings AllowItemsDragDrop="true"></ClientSettings>
                                </telerik:RadListView>
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
        </telerik:RadAjaxPanel>
    </form>
</body>
</html>
