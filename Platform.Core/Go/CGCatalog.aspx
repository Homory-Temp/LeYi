﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CGCatalog.aspx.cs" Inherits="Go.GoCGCatalog" %>

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
    <link href="../Content/Core/css/treefix.css" rel="stylesheet" />
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
        <telerik:RadAjaxPanel ID="panel" runat="server" CssClass="container-fluid" LoadingPanelID="loading">
            <div class="row">
                <div class="col-md-12 text-center">
                    <telerik:RadComboBox ID="courseList" runat="server" Label="课程：" OnSelectedIndexChanged="courseList_SelectedIndexChanged" AutoPostBack="true" DataTextField="Name" DataValueField="Id"></telerik:RadComboBox>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <telerik:RadComboBox ID="gradeList" runat="server" Label="年级：" OnSelectedIndexChanged="gradeList_SelectedIndexChanged" AutoPostBack="true">
                        <Items>
                            <telerik:RadComboBoxItem Text="初一" Value="B25DE587-8C11-4B58-A5FE-08D1C78EFA2E" Selected="true" />
                            <telerik:RadComboBoxItem Text="初二" Value="08DDE587-8CFA-4C95-B63B-08D1C78F124D" />
                            <telerik:RadComboBoxItem Text="初三" Value="8444E587-8CB6-4D3E-AFBA-08D1C78F2F5B" />
                            <telerik:RadComboBoxItem Text="高一" Value="85AD57A8-B503-4B83-99B4-9040DADC1B22" />
                            <telerik:RadComboBoxItem Text="高二" Value="AC02A035-24F2-41F5-BA6B-178ED4348009" />
                            <telerik:RadComboBoxItem Text="高三" Value="0E667D0D-0BF5-4D18-9B4D-9096EDBEF969" />
                        </Items>
                    </telerik:RadComboBox>
                </div>
            </div>
            <div class="row">
                <div class="col-md-12">
                    <table class="coreAuto">
                        <tr class="coreTop">
                            <td>
                                <telerik:RadTreeView ID="tree" runat="server" EnableDragAndDrop="true" EnableDragAndDropBetweenNodes="false" DataTextField="Name" DataValueField="Id" DataFieldID="Id" DataFieldParentID="ParentId" OnNodeClick="tree_NodeClick">
                                    <NodeTemplate>
                                        <i class='<%# FormatTreeNode(Container.DataItem) %>'></i>&nbsp;<%# Eval("Name") %><%# CountChildren(Container.DataItem as Catalog) %>
                                    </NodeTemplate>
                                </telerik:RadTreeView>
                            </td>
                            <td>&nbsp;</td>
                            <td>
                                <telerik:RadGrid ID="grid" runat="server" CssClass="coreCenter" AutoGenerateColumns="false" LocalizationPath="../Language" AllowSorting="True" PageSize="20" GridLines="None" OnNeedDataSource="grid_NeedDataSource" OnBatchEditCommand="grid_BatchEditCommand" OnItemCreated="grid_ItemCreated">
                                    <MasterTableView EditMode="Batch" DataKeyNames="Id" CommandItemDisplay="Top" HorizontalAlign="NotSet" ShowHeader="true" ShowHeadersWhenNoRecords="true" NoMasterRecordsText="">
                                        <BatchEditingSettings EditType="Row" OpenEditingEvent="DblClick" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <Columns>
                                            <telerik:GridTemplateColumn HeaderText="序号" DataField="Ordinal" SortExpression="Ordinal" UniqueName="Ordinal">
                                                <ItemTemplate>
                                                    <asp:Label runat="server" Text='<%# Eval("Ordinal") %>'></asp:Label>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <telerik:RadNumericTextBox ID="Ordinal" runat="server" EnabledStyle-HorizontalAlign="Center" Width="64" MinValue="1" MaxValue="99" AllowOutOfRangeAutoCorrect="true" Value='<%# Bind("Ordinal") %>'>
                                                        <NumberFormat DecimalDigits="0" AllowRounding="true" />
                                                    </telerik:RadNumericTextBox>
                                                </EditItemTemplate>
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridTemplateColumn HeaderText="名称 *" DataField="Name" SortExpression="Name" UniqueName="Name">
                                                <ItemTemplate>
                                                    <asp:Label runat="server" Text='<%# Eval("Name") %>'></asp:Label>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <telerik:RadTextBox ID="Name" runat="server" EnabledStyle-HorizontalAlign="Center" Width="64" MaxLength="16" Text='<%# Bind("Name") %>'>
                                                    </telerik:RadTextBox>
                                                </EditItemTemplate>
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridTemplateColumn HeaderText="状态" DataField="State" SortExpression="State" UniqueName="State">
                                                <ItemTemplate>
                                                    <asp:Label runat="server" Text='<%# Eval("State") %>'></asp:Label>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <telerik:RadComboBox ID="State" runat="server" Width="64" EnableTextSelection="true" Text='<%# Bind("State") %>'>
                                                        <Items>
                                                            <telerik:RadComboBoxItem Text="" Value="-1" />
                                                            <telerik:RadComboBoxItem Text="启用" Value="1" />
                                                            <telerik:RadComboBoxItem Text="停用" Value="4" />
                                                            <telerik:RadComboBoxItem Text="删除" Value="5" />
                                                        </Items>
                                                    </telerik:RadComboBox>
                                                </EditItemTemplate>
                                            </telerik:GridTemplateColumn>
                                        </Columns>
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
