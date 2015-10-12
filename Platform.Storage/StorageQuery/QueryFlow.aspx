<%@ Page Language="C#" AutoEventWireup="true" CodeFile="QueryFlow.aspx.cs" Inherits="QueryFlow" %>

<%@ Register Src="~/Menu/Menu.ascx" TagPrefix="homory" TagName="Menu" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <meta http-equiv="x-ua-compatible" content="ie=edge, chrome=1" />
    <meta name="viewport" content="width=device-width, initial-scale=1, minimum-scale=1, maximum-scale=1" />
    <title>物资管理 - 流通查询</title>
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
    <script>
        ajax_onRequestStart = function (sender, args) {
            if (args.get_eventTarget().indexOf("Button") >= 0) {
                args.set_enableAjax(false);
            }
        }
    </script>
</head>
<body>
    <form id="form" runat="server">
        <telerik:RadScriptManager runat="server"></telerik:RadScriptManager>
        <telerik:RadCodeBlock runat="server">
            <script>
            </script>
        </telerik:RadCodeBlock>
        <telerik:RadWindowManager runat="server" Modal="true" Behaviors="None" CenterIfModal="true" ShowContentDuringLoad="false" VisibleStatusbar="false" ReloadOnShow="true">
            <Windows>
            </Windows>
        </telerik:RadWindowManager>
        <homory:Menu runat="server" ID="menu" />
        <telerik:RadAjaxPanel ID="storage_panel" runat="server" CssClass="grid-container" EnableAJAX="true" ClientEvents-OnRequestStart="ajax_onRequestStart">
               <div class="grid-100 mobile-grid-100 grid-parent left">
                <h4 style="margin-left:20px;">库存查询</h4>
            </div>
            <hr>
            <div class="grid-100 grid-parent">
                <div class="grid-15 grid-parent" style="margin-top: 10px; border-right: 1px solid #cdcdcd; min-height: 500px;">
                    <div class="grid-100 left">
                        <telerik:RadTreeView ID="tree" runat="server" DataTextField="Name" DataValueField="Id" DataFieldID="Id" DataFieldParentID="ParentId" CheckBoxes="true" CheckChildNodes="true" OnNodeClick="tree_NodeClick" OnNodeCheck="tree_NodeCheck">
                        </telerik:RadTreeView>
                    </div>
                </div>
                <div class="grid-85 grid-parent">
                    <div class="grid-100 mobile-grid-100">
                        开始时间：<telerik:RadDatePicker ID="day_start" runat="server" LocalizationPath="~/Language" ShowPopupOnFocus="true">
                            <DatePopupButton Visible="false" />
                        </telerik:RadDatePicker>
                        结束时间：<telerik:RadDatePicker ID="day_end" runat="server" LocalizationPath="~/Language" ShowPopupOnFocus="true">
                            <DatePopupButton Visible="false" />
                        </telerik:RadDatePicker>
                        物品名称：<telerik:RadTextBox ID="name" runat="server"></telerik:RadTextBox>
                        <telerik:RadComboBox ID="combo" runat="server">
                            <Items>
                                <telerik:RadComboBoxItem Text="全部" Value="0" Selected="true" />
                                <telerik:RadComboBoxItem Text="易耗品" Value="1" />
                                <telerik:RadComboBoxItem Text="非易耗品" Value="2" />
                            </Items>
                        </telerik:RadComboBox>
                        <asp:ImageButton AlternateText="查询" ID="query" runat="server" OnClick="query_Click" />
                    </div>
                    <div class="grid-100 mobile-grid-100" style="margin-top: 10px; ">


                        <telerik:RadGrid ID="view" runat="server" OnNeedDataSource="view_NeedDataSource" AutoGenerateColumns="false">
                            <MasterTableView DataKeyNames="Id"  CommandItemDisplay="TopAndBottom">
                                <Columns>
                                    <telerik:GridBoundColumn DataField="分类" HeaderText="分类"></telerik:GridBoundColumn>
                                    <telerik:GridTemplateColumn DataField="物品名称" HeaderText="物资名称">
                                        <ItemTemplate>
                                            <asp:Label ID="label" runat="server" Text='<%# Eval("物品名称") %>'></asp:Label>
                                            <telerik:RadWindow ID="win" runat="server" Modal="true" Behaviors="Close" CenterIfModal="true" ShowContentDuringLoad="true" VisibleStatusbar="false" ReloadOnShow="true"></telerik:RadWindow>
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridTemplateColumn DataField="易耗品" HeaderText="易耗品">
                                        <ItemTemplate>
                                            <%# ((bool)Eval("易耗品")) ? "是" : "否" %>
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridBoundColumn DataField="期初库存" HeaderText="期初存量"></telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="期初余额" HeaderText="期初余额" DataFormatString="{0:F2}"></telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="入库数量" HeaderText="入库数量"></telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="入库金额" HeaderText="入库金额" DataFormatString="{0:F2}"></telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="借用数量" HeaderText="借用数量"></telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="借用金额" HeaderText="借用金额" DataFormatString="{0:F2}"></telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="耗废数量" HeaderText="耗废数量"></telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="耗废金额" HeaderText="耗废金额" DataFormatString="{0:F2}"></telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="期末库存" HeaderText="期末存量"></telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="期末余额" HeaderText="期末余额" DataFormatString="{0:F2}"></telerik:GridBoundColumn>
                                </Columns>
                                <HeaderStyle HorizontalAlign="Center" />
                                <CommandItemSettings ShowAddNewRecordButton="false" ShowRefreshButton="false" ShowExportToExcelButton="true" />
                                <ItemStyle HorizontalAlign="Center" />
                                <AlternatingItemStyle HorizontalAlign="Center" />
                                <NoRecordsTemplate></NoRecordsTemplate>
                            </MasterTableView>
                            <ExportSettings Excel-Format="ExcelML" FileName="领用查询" ExportOnlyData="true" UseItemStyles="false" IgnorePaging="true" OpenInNewWindow="true"></ExportSettings>
                        </telerik:RadGrid>
                    </div>
                </div>
            </div>
        </telerik:RadAjaxPanel>
    </form>
    <script src="../Assets/javascripts/jquery.js"></script>
    <script src="../Assets/javascripts/common.js"></script>
</body>
</html>
