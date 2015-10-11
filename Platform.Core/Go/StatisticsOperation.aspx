<%@ Page Language="C#" AutoEventWireup="true" CodeFile="StatisticsOperation.aspx.cs" Inherits="Go.GoStatisticsOperation" %>

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
        <telerik:RadAjaxPanel ID="panel" runat="server" CssClass="container-fluid" LoadingPanelID="loading">
            <div class="row">
                <div class="col-md-12">
                    <telerik:RadComboBox ID="combo" runat="server" Skin="MetroTouch" Label="统计年份：" Width="80" OnSelectedIndexChanged="combo_OnSelectedIndexChanged" AutoPostBack="True"></telerik:RadComboBox>
                </div>
                <div id="fullChart" runat="server" class="scol-md-12">
                    <telerik:RadChart ID="c" runat="server" Skin="Black" DefaultType="StackedBar" AutoLayout="True" PlotArea-EmptySeriesMessage-TextBlock-Text="无统计数据" Height="600">
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
                </div>
                <div class="sixteen wide column">
                    <telerik:RadChart ID="c2" runat="server" DefaultType="Pie" Skin="Black" AutoLayout="True" PlotArea-EmptySeriesMessage-TextBlock-Text="无统计数据" Width="800" Height="600" Style="margin: auto;">
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
                </div>
                <div class="sixteen wide column">
                    <telerik:RadGrid ID="grid" runat="server" CssClass="coreAuto coreFull coreCenter" AllowPaging="true" AutoGenerateColumns="False" LocalizationPath="../Language/" AllowSorting="True" PageSize="20" AllowFilteringByColumn="False" Culture="zh-CN" ShowGroupPanel="True" OnInit="grid_OnInit" OnNeedDataSource="grid_NeedDataSource">
                        <ClientSettings AllowDragToGroup="True">
                        </ClientSettings>
                        <MasterTableView CssClass="coreAuto coreFull coreCenter" CommandItemDisplay="Top" HorizontalAlign="NotSet" ShowHeader="true" ShowHeadersWhenNoRecords="true" NoMasterRecordsText="">
                            <HeaderStyle HorizontalAlign="Center" />
                            <GroupHeaderItemStyle HorizontalAlign="Left"></GroupHeaderItemStyle>
                            <CommandItemSettings ShowAddNewRecordButton="false" />
                            <Columns>
                                <telerik:GridBoundColumn FilterControlWidth="30" DataField="Name" HeaderText="学校" SortExpression="Name" UniqueName="Name" FilterControlAltText="Filter Name column">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn FilterControlWidth="30" DataField="姓名" HeaderText="姓名" SortExpression="姓名" UniqueName="姓名" FilterControlAltText="Filter 姓名 column">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn FilterControlWidth="30" DataField="Type" HeaderText="操作" SortExpression="Type" UniqueName="Type" FilterControlAltText="Filter Type column">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn FilterControlWidth="30" DataFormatString="{0:yyyy-MM-dd HH:mm:ss}" DataField="Time" HeaderText="时间" SortExpression="Time" UniqueName="Time" DataType="System.DateTime" FilterControlAltText="Filter Time column">
                                </telerik:GridBoundColumn>
                            </Columns>
                            <PagerStyle Mode="NextPrevAndNumeric" PageSizes="10,20,50,100" Position="Bottom" PageSizeControlType="RadComboBox" AlwaysVisible="true" PagerTextFormat="{4} 第{0}页，共{1}页；第{2}-{3}项，共{5}项" />
                        </MasterTableView>
                        <ClientSettings>
                            <Selecting AllowRowSelect="true" UseClientSelectColumnOnly="true" />
                        </ClientSettings>
                    </telerik:RadGrid>
                </div>
            </div>
        </telerik:RadAjaxPanel>
    </form>
</body>
</html>
