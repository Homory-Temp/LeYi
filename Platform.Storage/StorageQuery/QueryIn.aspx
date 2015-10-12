<%@ Page Language="C#" AutoEventWireup="true" CodeFile="QueryIn.aspx.cs" Inherits="QueryIn" %>

<%@ Register Src="~/Menu/Menu.ascx" TagPrefix="homory" TagName="Menu" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <meta http-equiv="x-ua-compatible" content="ie=edge, chrome=1" />
    <meta name="viewport" content="width=device-width, initial-scale=1, minimum-scale=1, maximum-scale=1" />
    <title>物资管理 - 入库查询</title>
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
                <h4 style="margin-left:20px;">入库查询</h4>
            </div>
            <hr>
            <div class="grid-100 mobile-grid-100 grid-parent">
                <div class="grid-15 mobile-grid-100 grid-parent" style="margin-top: 10px; border-right: 1px solid #cdcdcd; ">
                    <div class="grid-100 mobile-grid-100 left">
                        <div>分类：</div>
                        <div>
                            <telerik:RadTreeView ID="tree" runat="server" DataTextField="Name" DataValueField="Id" DataFieldID="Id" DataFieldParentID="ParentId" CheckBoxes="true" CheckChildNodes="true" OnNodeClick="tree_NodeClick" OnNodeCheck="tree_NodeCheck">
                            </telerik:RadTreeView>
                        </div>
                    </div>
                </div>
                <div class="grid-85 mobile-grid-100 grid-parent">
                    <div class="grid-100 mobile-grid-100">
                        开始时间：<telerik:RadDatePicker ID="day_start" runat="server" LocalizationPath="~/Language" ShowPopupOnFocus="true">
                            <DatePopupButton Visible="false" />
                        </telerik:RadDatePicker>
                        结束时间：<telerik:RadDatePicker ID="day_end" runat="server" LocalizationPath="~/Language" ShowPopupOnFocus="true">
                            <DatePopupButton Visible="false" />
                        </telerik:RadDatePicker>
                        物品名称：<telerik:RadTextBox ID="name" runat="server"></telerik:RadTextBox>
                        人员：<telerik:RadTextBox ID="people" runat="server"></telerik:RadTextBox>
                        <asp:ImageButton AlternateText="查询" ID="query" runat="server" OnClick="query_Click" />
                    </div>
                    <div class="grid-100 mobile-grid-100" style="margin-top: 10px; " >
                        <telerik:RadGrid ID="view" runat="server" OnNeedDataSource="view_NeedDataSource" AutoGenerateColumns="false">
                            <MasterTableView DataKeyNames="入库标识"  CommandItemDisplay="TopAndBottom">
                                <HeaderStyle HorizontalAlign="Center" />
                                <CommandItemSettings ShowAddNewRecordButton="false" ShowRefreshButton="false" ShowExportToExcelButton="true" />
                                <ItemStyle HorizontalAlign="Center" />
                                <AlternatingItemStyle HorizontalAlign="Center" />
                                <Columns>
                                   
                                    <telerik:GridTemplateColumn DataField="购置单号" HeaderText="购置单号">
                                        <ItemTemplate>
                                            <asp:HyperLink runat="server" Text='<%# Eval("购置单号") %>' NavigateUrl='<%# "~/StorageTarget/TargetIn?TargetId={0}&StorageId={1}".Formatted(Eval("购置标识"), StorageId) %>' Target="_blank"></asp:HyperLink>
                                        </ItemTemplate>
                                     </telerik:GridTemplateColumn>
                                    <telerik:GridBoundColumn DataField="发票编号" HeaderText="发票编号"></telerik:GridBoundColumn>
                                     <telerik:GridBoundColumn DataField="日期" HeaderText="入库日期"></telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="物品名称" HeaderText="物品名称"></telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="单位" HeaderText="单位" ></telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="数量" HeaderText="数量"></telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="单价" HeaderText="单价" DataFormatString="{0:F2}"></telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="合计" HeaderText="合计" DataFormatString="{0:F2}"></telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="年龄段" HeaderText="年龄段"></telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="存放地" HeaderText="存放地"></telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="责任人" HeaderText="责任人"></telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="操作人" HeaderText="操作人"></telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="备注" HeaderText="备注"></telerik:GridBoundColumn>
                                    <telerik:GridTemplateColumn DataField="入库标识" HeaderText="入库修改" UniqueName="Mod">
                                        <ItemTemplate>
                                            <asp:HyperLink runat="server" Text="入库修改" NavigateUrl='<%# "~/StorageHome/Adjust?InId={0}&StorageId={1}".Formatted(Eval("入库标识"), StorageId) %>' Target="_top"></asp:HyperLink>
                                        </ItemTemplate>
                                     </telerik:GridTemplateColumn>
                                </Columns>
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
