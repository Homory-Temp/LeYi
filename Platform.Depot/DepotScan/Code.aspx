﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Code.aspx.cs" Inherits="DepotAction_Code" %>

<%@ Register Src="~/Control/SideBarSingle.ascx" TagPrefix="homory" TagName="SideBarSingle" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=Edge,Chrome=1" />
    <meta name="viewport" content="width=device-width,initial-scale=1,minimum-scale=1,maximum-scale=1" />
    <script src="../Content/jQuery/jquery.min.js"></script>
    <link href="../assets/css/bootstrap.min.css" rel="stylesheet" />
    <link href="../assets/css/style-responsive.css" rel="stylesheet" />
    <link href="../assets/css/style.css" rel="stylesheet" />
    <link href="../Content/Core/css/common.css" rel="stylesheet" />
    <link href="../Content/Core/css/fix.css" rel="stylesheet" />
    <script src="../assets/js/bootstrap.min.js"></script>
    <script src="../Content/Homory/js/common.js"></script>
    <script src="../Content/Homory/js/notify.min.js"></script>
    <!--[if lt IE 9]>
	    <script src="../Content/Homory/js/html5shiv.js"></script>
	    <script src="../Content/Homory/js/respond.min.js"></script>
    <![endif]-->
    <style>
        .depot {
            margin-left: 10px;
            float: left;
            text-decoration: none;
            font-weight: normal;
        }
        .depotx label {
            text-decoration: none;
            font-weight: normal;
        }
    </style>
</head>
<body>
    <form id="form" runat="server">
        <homory:SideBarSingle runat="server" ID="SideBarSingle" Crumb="物资条码 - 条码打印" />
        <telerik:RadAjaxPanel ID="ap" runat="server" CssClass="container-fluid" LoadingPanelID="loading">
            <label id="____v" runat="server" style="display: none;"></label>
            <div class="row">
                <div class="col-md-2" style="border-right: 1px solid #2B2B2B;">
                    <div class="row">
                        <div class="col-md-12">
                            <span class="btn btn-tumblr">类别：</span>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12">
                            <telerik:RadTreeView ID="tree" runat="server" OnNodeClick="tree_NodeClick" DataTextField="Name" DataValueField="Id" DataFieldID="Id" DataFieldParentID="ParentId">
                                <NodeTemplate>
                                    <%# Eval("Name") %><%# Eval("Count").EmptyWhenZero() %>
                                </NodeTemplate>
                            </telerik:RadTreeView>
                        </div>
                    </div>
                </div>
                <div class="col-md-10" style="text-align: left;">
                    <div class="row">
                        <div class="col-md-12">
                            <span class="btn btn-tumblr">物资：</span>
                            <input id="all" runat="server" type="button" class="btn btn-info" value="全选" onserverclick="all_ServerClick" />
                            <input id="st" runat="server" type="button" class="btn btn-info" value="已选（0）" />
                            <input id="cl" runat="server" type="button" class="btn btn-info" value="清除选择" onserverclick="cl_ServerClick" />
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        </div>
                        <div class="col-md-12" style="margin-top: 6px;">
                            <telerik:RadTextBox ID="toSearch" runat="server" Width="120" EmptyMessage="物资名称"></telerik:RadTextBox>
                            &nbsp;&nbsp;
                            <telerik:RadTextBox ID="toSearchX" runat="server" Width="120" EmptyMessage="存放地点"></telerik:RadTextBox>
                            &nbsp;&nbsp;
                            <telerik:RadComboBox ID="depts" runat="server" Width="120" EmptyMessage="使用部门" AutoPostBack="false"></telerik:RadComboBox>
                            &nbsp;&nbsp;
                            <telerik:RadComboBox ID="ol" runat="server" Width="120" DropDownWidth="300" EmptyMessage="购置单" AutoPostBack="false" AppendDataBoundItems="true" DataTextField="Name" DataValueField="Id">
                                <Items>
                                    <telerik:RadComboBoxItem Text="" Value="*" />
                                </Items>
                            </telerik:RadComboBox>
                            &nbsp;&nbsp;
                            <input id="search" runat="server" type="button" class="btn btn-info" value="检索" onserverclick="search_ServerClick" />
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <telerik:RadTextBox ID="cName" runat="server" Width="150" EmptyMessage="生成任务名称"></telerik:RadTextBox>
                            &nbsp;&nbsp;
                            <input id="coding" runat="server" type="button" class="btn btn-info" value="生成条码" onserverclick="coding_ServerClick" />
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <input id="coded" runat="server" type="button" class="btn btn-info" value="条码列表" onserverclick="coded_ServerClick" />
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        </div>
                    </div>
                    <div class="row">&nbsp;</div>
                    <div class="row depotx">
                        <div class="col-md-12">
                            <telerik:RadListView ID="view" runat="server" OnNeedDataSource="view_NeedDataSource" ItemPlaceholderID="holder">
                                <LayoutTemplate>
                                    <table class="storeTablePrint">
                                        <tr>
                                            <th style="width: 30%;">名称</th>
                                            <th style="width: 70%;">条码</th>
                                        </tr>
                                        <asp:PlaceHolder ID="holder" runat="server"></asp:PlaceHolder>
                                    </table>
                                </LayoutTemplate>
                                <ItemTemplate>
                                    <tr>
                                        <td style="width: 30%;">
                                            <asp:CheckBox ID="check" runat="server" CssClass="depot" AutoPostBack="true" CC='<%# Eval("Code") %>' OBJ='<%# Eval("Id") %>' Text='<%# "&nbsp;{0}".Formatted(Eval("Name")) %>' OnCheckedChanged="check_CheckedChanged" />
                                        </td>
                                        <td style="width: 70%;">
                                            <asp:Panel ID="single" runat="server" Visible='<%# (bool)Eval("Single") %>'>
                                                <telerik:RadListView ID="viewx" runat="server" DataSource='<%# Ordinals((Guid)Eval("Id"), Container) %>'>
                                                    <ItemTemplate>
                                                        <div style="float: left;">
                                                            <asp:CheckBox ID="checkx" runat="server" CssClass="depot" AutoPostBack="true" OnCheckedChanged="checkx_CheckedChanged" SM="1" CC='<%# Eval("Code") %>' Checked='<%# CD(Eval("Code").ToString()) %>' OBJ='<%# Eval("ObjectId") %>' ORD='<%# Eval("Ordinal") %>' Text='<%# "&nbsp;{1}&nbsp;-&nbsp;{0}".Formatted(Eval("Ordinal"), Eval("Code")) %>' />
                                                        </div>
                                                    </ItemTemplate>
                                                </telerik:RadListView>
                                            </asp:Panel>
                                            <asp:Panel ID="multiple" runat="server" Visible='<%# !(bool)Eval("Single") %>'>
                                                <div style="float: left; width: 200px;">
                                                    <asp:CheckBox ID="checkx" runat="server" CssClass="depot" AutoPostBack="true" OnCheckedChanged="checkx_CheckedChanged" SM='<%# !(bool)Eval("Single") ? "1" : "0" %>' CC='<%# Eval("Code") %>' Checked='<%# CD(Eval("Code").ToString()) %>' OBJ='<%# Eval("Id") %>' Text='<%# "&nbsp;{0}".Formatted(Eval("Code")) %>' />
                                                </div>
                                            </asp:Panel>
                                        </td>
                                    </tr>
                                </ItemTemplate>
                            </telerik:RadListView>
                        </div>
                    </div>
                    <div class="row">&nbsp;</div>
                </div>
            </div>
        </telerik:RadAjaxPanel>
    </form>
</body>
</html>
