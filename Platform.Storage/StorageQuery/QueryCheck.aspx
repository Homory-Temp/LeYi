<%@ Page Language="C#" AutoEventWireup="true" CodeFile="QueryCheck.aspx.cs" Inherits="QueryCheck" %>

<%@ Register Src="~/Menu/Menu.ascx" TagPrefix="homory" TagName="Menu" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <meta http-equiv="x-ua-compatible" content="ie=edge, chrome=1" />
    <meta name="viewport" content="width=device-width, initial-scale=1, minimum-scale=1, maximum-scale=1" />
    <title>物资管理 - 盘库查询</title>
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
        <telerik:RadAjaxPanel ID="storage_panel" runat="server" CssClass="grid-container" EnableAJAX="true">
            <div class="grid-100 mobile-grid-100 grid-parent">
                <div class="grid-100 mobile-grid-100 grid-parent">
                    <div class="grid-100 mobile-grid-100">
                        开始时间：<telerik:RadDatePicker ID="day_start" runat="server" LocalizationPath="~/Language" ShowPopupOnFocus="true">
                            <DatePopupButton Visible="false" />
                        </telerik:RadDatePicker>
                        结束时间：<telerik:RadDatePicker ID="day_end" runat="server" LocalizationPath="~/Language" ShowPopupOnFocus="true">
                            <DatePopupButton Visible="false" />
                        </telerik:RadDatePicker>
                        盘库名称：<telerik:RadTextBox ID="name" runat="server"></telerik:RadTextBox>
                        <asp:ImageButton AlternateText="查询" ID="query" runat="server" OnClick="query_Click" />
                    </div>
                    <div class="grid-100 mobile-grid-100"  style="margin-top:10px;">
                        <telerik:RadGrid ID="view" runat="server" OnNeedDataSource="view_NeedDataSource" AutoGenerateColumns="false" LocalizationPath="~/Language" OnItemDataBound="view_ItemDataBound">
                            <MasterTableView DataKeyNames="Id">
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Center" />
                                <AlternatingItemStyle HorizontalAlign="Center" />
                                <Columns>
                                    <telerik:GridBoundColumn DataField="TimeNode" HeaderText="盘库日期"></telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="Name" HeaderText="盘库名称"></telerik:GridBoundColumn>
                                    <telerik:GridTemplateColumn UniqueName="Content">
                                    <ItemTemplate>
                                        <asp:HyperLink ID="c" runat="server" Text="盘库详情" Target="_blank"></asp:HyperLink>
                                        <telerik:RadWindow ID="tip" runat="server" Modal="true" Behaviors="Close" InitialBehaviors="Maximize" CenterIfModal="true" ShowContentDuringLoad="true" VisibleStatusbar="false" ReloadOnShow="true"  VisibleTitlebar="false"></telerik:RadWindow>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                </Columns>
                                <NoRecordsTemplate></NoRecordsTemplate>
                            </MasterTableView>
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
