<%@ Page Language="C#" AutoEventWireup="true" CodeFile="QueryCheckContent.aspx.cs" Inherits="QueryCheckContent" %>

<%@ Register Src="~/Menu/Menu.ascx" TagPrefix="homory" TagName="Menu" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <meta http-equiv="x-ua-compatible" content="ie=edge, chrome=1" />
    <meta name="viewport" content="width=device-width, initial-scale=1, minimum-scale=1, maximum-scale=1" />
    <title>物资管理 - 盘库详情</title>
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
<body >
    <form id="form" runat="server">
        <telerik:RadScriptManager ID="sm" runat="server"></telerik:RadScriptManager>
        <telerik:RadCodeBlock ID="cb" runat="server">
            <script>
                function peek() {
                    var oWindow = null;
                    if (window.radWindow) oWindow = window.radWindow;
                    else if (window.frameElement.radWindow) oWindow = window.frameElement.radWindow;
                    return oWindow;
                }
                function cancel() {
                    peek().close();
                }
                peek().maximize();
            </script>
        </telerik:RadCodeBlock>
        <telerik:RadAjaxPanel ID="ap" runat="server" CssClass="grid-container" ClientEvents-OnRequestStart="ajax_onRequestStart">
            <div class="grid-100 mobile-grid-100 grid-parent" style="text-align:left;">盘库名称：
                <asp:Label ID="name_ex" runat="server"></asp:Label>
            </div>
            <div class="grid-100 mobile-grid-100 grid-parent" >
                <input id="kcuf" runat="server" type="hidden" />
                <input id="kcufX" runat="server" type="hidden" />
                <telerik:RadGrid ID="view" runat="server" OnNeedDataSource="view_NeedDataSource" AutoGenerateColumns="false" LocalizationPath="~/Language">
                    <MasterTableView DataKeyNames="单计标识" CommandItemDisplay="Top">
                        <HeaderStyle HorizontalAlign="Center" />
                        <CommandItemSettings ShowAddNewRecordButton="false" ShowRefreshButton="false" ShowExportToExcelButton="true" />
                        <ItemStyle HorizontalAlign="Center" />
                        <AlternatingItemStyle HorizontalAlign="Center" />
                        <Columns>
                            <telerik:GridTemplateColumn HeaderText="物资分类">
                                <ItemTemplate>
                                    <%# GP(Container.DataItem as Models.查询_盘库流) %>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridBoundColumn DataField="物品名称" HeaderText="物资名称"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="编号" HeaderText="固定资产编号"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="固定资产编号" HeaderText="物资编号"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="存放地" HeaderText="存放地"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="责任人" HeaderText="责任人"></telerik:GridBoundColumn>
                            <telerik:GridTemplateColumn HeaderText="已盘">
                                <ItemTemplate>
                                    <asp:Label runat="server" ForeColor='<%# CDCColor((Guid)Eval("单计标识")) %>' Text='<%# CDC((Guid)Eval("单计标识")) ? "√" : "" %>'></asp:Label>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridTemplateColumn HeaderText="未盘">
                                <ItemTemplate>
                                    <asp:Label runat="server" ForeColor='<%# CDCColor((Guid)Eval("单计标识")) %>' Text='<%# CDC((Guid)Eval("单计标识")) ? "" : "√" %>'></asp:Label>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                        </Columns>
                        <NoRecordsTemplate></NoRecordsTemplate>
                    </MasterTableView>
                    <ExportSettings Excel-Format="Xlsx" FileName="Check" ExportOnlyData="false" UseItemStyles="false" IgnorePaging="true" OpenInNewWindow="true"></ExportSettings>
                </telerik:RadGrid>
            </div>
            <div class="grid-100 mobile-grid-100 grid-parent">
                <asp:ImageButton AlternateText="关闭" ID="back" runat="server" OnClientClick="cancel(); return false;" />
            </div>
        </telerik:RadAjaxPanel>
    </form>
    <script src="../Assets/javascripts/jquery.js"></script>
    <script src="../Assets/javascripts/common.js"></script>

    <style>
        html     .RadGrid .rgMasterTable, .RadGrid .rgDetailTable, .RadGrid .rgEditForm table{
             height:auto;
        }
    </style>
</body>
</html>
