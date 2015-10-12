<%@ Page Language="C#" AutoEventWireup="true" CodeFile="QueryTarget.aspx.cs" Inherits="QueryTarget" %>

<%@ Register Src="~/Menu/Menu.ascx" TagPrefix="homory" TagName="Menu" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <meta http-equiv="x-ua-compatible" content="ie=edge, chrome=1" />
    <meta name="viewport" content="width=device-width, initial-scale=1, minimum-scale=1, maximum-scale=1" />
    <title>物资管理 - 购置查询</title>
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

        function OpenClick(TargetId, StorageId)
        {
            var openUrl = "../StorageTarget/TargetIn?TargetId=" + TargetId + "&StorageId=" + StorageId;

            //var openUrl = "../StorageTarget/TargetIn";

            var iWidth = window.screen.availWidth; //弹出窗口的宽度;

            var iHeight = window.screen.availHeight; //弹出窗口的高度;

            var iTop = 0; //获得窗口的垂直位置;

            var iLeft = 0; //获得窗口的水平位置;

            window.open(openUrl, "“_blank", "height=" + iHeight + ", width=" + iWidth + ", top=" + iTop + ", left=" + iLeft);
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

            <div class="grid-100 mobile-grid-100 grid-parent">
                <div class="grid-100 mobile-grid-100">
                    开始时间：<telerik:RadDatePicker ID="day_start" runat="server" LocalizationPath="~/Language" ShowPopupOnFocus="true">
                        <DatePopupButton Visible="false" />
                    </telerik:RadDatePicker>
                    结束时间：<telerik:RadDatePicker ID="day_end" runat="server" LocalizationPath="~/Language" ShowPopupOnFocus="true">
                        <DatePopupButton Visible="false" />
                    </telerik:RadDatePicker>
                    购置单号：<telerik:RadTextBox ID="number" runat="server" Width="100px"></telerik:RadTextBox>
                    发票编号：<telerik:RadTextBox ID="receipt" runat="server" Width="100px"></telerik:RadTextBox>
                    人员：<telerik:RadTextBox ID="people" runat="server" Width="100px"></telerik:RadTextBox>
                   <asp:ImageButton AlternateText="查询" ID="query" runat="server" OnClick="query_Click" />
                </div>
             </div>
                <div class="grid-100 mobile-grid-100 grid-parent left">
                <h4 style="margin-left:20px;">购置单查询</h4>
            </div>
                <div class="grid-100 mobile-grid-100" style="margin-top:10px;">
                    <telerik:RadGrid ID="view" runat="server" OnNeedDataSource="view_NeedDataSource" AutoGenerateColumns="false" OnItemDataBound="view_ItemDataBound" LocalizationPath="~/Language">
                        <MasterTableView DataKeyNames="主键"  CommandItemDisplay="TopAndBottom">
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Center" />
                            <CommandItemSettings ShowAddNewRecordButton="false" ShowRefreshButton="false" ShowExportToExcelButton="true" />
                            <AlternatingItemStyle HorizontalAlign="Center" />
                            <Columns>
                               
                                <telerik:GridBoundColumn DataField="购置单号" HeaderText="购置单号" ItemStyle-Width="8%"></telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="发票编号" HeaderText="发票编号"  ItemStyle-Width="8%"></telerik:GridBoundColumn>
                                 <telerik:GridTemplateColumn DataField="采购日期" HeaderText="采购日期"  ItemStyle-Width="8%">
                                    <ItemTemplate>
                                        <%# ((int)Eval("采购日期")).TimeNode() %>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridBoundColumn DataField="采购来源" HeaderText="采购来源"  ItemStyle-Width="8%"></telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="使用对象" HeaderText="使用对象"  ItemStyle-Width="8%"></telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="应付金额" HeaderText="应付金额" DataFormatString="{0:F2}"  ItemStyle-Width="8%"></telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="实付金额" HeaderText="实付金额" DataFormatString="{0:F2}"  ItemStyle-Width="8%"></telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="经手人" HeaderText="经手人"  ItemStyle-Width="8%"></telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="保管人" HeaderText="保管人"  ItemStyle-Width="8%"></telerik:GridBoundColumn>
                                   <telerik:GridBoundColumn DataField="操作人" HeaderText="操作人"  ItemStyle-Width="8%"></telerik:GridBoundColumn>
                                 <telerik:GridBoundColumn DataField="清单简述" HeaderText="清单简述"  ItemStyle-Width="12%"></telerik:GridBoundColumn>
                            
                                <telerik:GridTemplateColumn  ItemStyle-Width="8%">
                                    <ItemTemplate>
                                        <div runat="server"  visible='<%# Eval("是否入库") %>'>
                                            <a href="javascript:void(0);" onclick='OpenClick("<%#Eval("主键") %>","<%=StorageId.ToString() %>")'>入库记录</a>
                               <%--<label onclick="OpenClick(<%#Eval("主键") %>,StorageId)">入库记录</label>--%>
                                        </div>
                                      <%--  <asp:HyperLink runat="server" Text="入库记录" Visible='<%# Eval("是否入库") %>' Target="_blank" NavigateUrl='<%# "~/StorageTarget/TargetIn?TargetId={0}&StorageId={1}".Formatted(Eval("主键"), StorageId) %>'></asp:HyperLink>--%>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                            </Columns>
                            <NoRecordsTemplate></NoRecordsTemplate>
                        </MasterTableView>
                        <ExportSettings Excel-Format="ExcelML" FileName="领用查询" ExportOnlyData="true" UseItemStyles="false" IgnorePaging="true" OpenInNewWindow="true"></ExportSettings>
                    </telerik:RadGrid>
                </div>
            </div>
        </telerik:RadAjaxPanel>
    </form>
    <script src="../Assets/javascripts/jquery.js"></script>
    <script src="../Assets/javascripts/common.js"></script>
</body>
</html>
