<%@ Page Language="C#" AutoEventWireup="true" CodeFile="QueryTaught.aspx.cs" Inherits="Go.GoQueryTaught" %>

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
        <telerik:RadAjaxLoadingPanel ID="loading" runat="server">
            <i class="ui huge teal loading icon" style="margin-top: 50px;"></i>
            <div>&nbsp;</div>
            <div style="color: #564F8A; font-size: 16px;">正在加载 请稍候....</div>
        </telerik:RadAjaxLoadingPanel>
        <div style="clear: both;"></div>
        <telerik:RadAjaxPanel ID="panel" runat="server" CssClass="coreAuto coreFull coreCenter" Style="margin: 0; padding: 0;" LoadingPanelID="loading">
            <telerik:RadGrid ID="grid" runat="server" CssClass="coreAuto coreFull coreCenter" AllowPaging="true" AutoGenerateColumns="False" LocalizationPath="../Language/" AllowSorting="True" PageSize="20" AllowFilteringByColumn="True" Culture="zh-CN" ShowGroupPanel="True" OnInit="grid_OnInit" OnItemCommand="grid_OnItemCommand" OnNeedDataSource="grid_NeedDataSource">
                <ClientSettings AllowDragToGroup="True">
                </ClientSettings>
                <MasterTableView CssClass="coreAuto coreFull coreCenter" CommandItemDisplay="Top" HorizontalAlign="NotSet" ShowHeader="true" ShowHeadersWhenNoRecords="true" NoMasterRecordsText="">
                    <HeaderStyle HorizontalAlign="Center" />
                    <GroupHeaderItemStyle HorizontalAlign="Left"></GroupHeaderItemStyle>
                    <CommandItemSettings ExportToExcelText="导出" ShowAddNewRecordButton="False" ShowExportToExcelButton="False"></CommandItemSettings>
                    <Columns>
                        <telerik:GridBoundColumn FilterControlWidth="30" DataField="学校" HeaderText="学校" SortExpression="学校" UniqueName="学校" FilterControlAltText="Filter 学校 column">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn FilterControlWidth="30" DataField="届" HeaderText="届" SortExpression="届" UniqueName="届" FilterControlAltText="Filter 届 column">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn FilterControlWidth="30" DataField="班级" HeaderText="班级" SortExpression="班级" UniqueName="班级" FilterControlAltText="Filter 班级 column">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn FilterControlWidth="30" DataField="课程名称" HeaderText="课程" SortExpression="课程名称" UniqueName="课程名称" FilterControlAltText="Filter 课程名称 column">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn FilterControlWidth="30" DataField="教师" HeaderText="教师" SortExpression="教师" UniqueName="教师" FilterControlAltText="Filter 教师 column">
                        </telerik:GridBoundColumn>
                    </Columns>
                    <PagerStyle Mode="NextPrevAndNumeric" PageSizes="10,20,50,100" Position="Bottom" PageSizeControlType="RadComboBox" AlwaysVisible="true" PagerTextFormat="{4} 第{0}页，共{1}页；第{2}-{3}项，共{5}项" />
                </MasterTableView>
                <ClientSettings>
                    <Selecting AllowRowSelect="true" UseClientSelectColumnOnly="true" />
                </ClientSettings>
            </telerik:RadGrid>
            <div class="ui divider"></div>
            <telerik:RadChart ID="c" runat="server" DefaultType="Pie" Skin="Black" AutoLayout="True" PlotArea-EmptySeriesMessage-TextBlock-Text="无统计数据" Width="1000" Height="600" style="margin: auto;">
                <Legend>
                    <Appearance Position-AlignedPosition="Left">
                    </Appearance>
                </Legend>
                <PlotArea>
                    <EmptySeriesMessage Visible="True">
                        <Appearance Visible="True">
                        </Appearance>
                        <TextBlock Text="无统计数据">
                        </TextBlock>
                    </EmptySeriesMessage>
                </PlotArea>
                <ChartTitle>
                    <TextBlock Text="">
                    </TextBlock>
                </ChartTitle>
            </telerik:RadChart>
        </telerik:RadAjaxPanel>
    </form>
</body>
</html>
