<%@ Page Language="C#" AutoEventWireup="true" CodeFile="StatisticsResource.aspx.cs" Inherits="Go.GoStatisticsResource" %>

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
        <telerik:RadAjaxPanel ID="panel" runat="server" CssClass="ui center aligned page grid" Style="margin: 0; padding: 0;" LoadingPanelID="loading">
            <div class="sixteen wide column">
                <telerik:RadComboBox ID="combo" runat="server" Skin="MetroTouch" Label="统计年份：" Width="80" OnSelectedIndexChanged="combo_OnSelectedIndexChanged" AutoPostBack="True"></telerik:RadComboBox>
                &nbsp;&nbsp;&nbsp;&nbsp;
                <telerik:RadComboBox ID="comboX" runat="server" Skin="MetroTouch" Label="统计月份：" Width="80" OnSelectedIndexChanged="combo_OnSelectedIndexChanged" AutoPostBack="True">
                    <Items>
                        <telerik:RadComboBoxItem runat="server" Text="全部" Selected="True" Value="0" />
                        <telerik:RadComboBoxItem runat="server" Text="01月" Value="1" />
                        <telerik:RadComboBoxItem runat="server" Text="02月" Value="2" />
                        <telerik:RadComboBoxItem runat="server" Text="03月" Value="3" />
                        <telerik:RadComboBoxItem runat="server" Text="04月" Value="4" />
                        <telerik:RadComboBoxItem runat="server" Text="05月" Value="5" />
                        <telerik:RadComboBoxItem runat="server" Text="06月" Value="6" />
                        <telerik:RadComboBoxItem runat="server" Text="07月" Value="7" />
                        <telerik:RadComboBoxItem runat="server" Text="08月" Value="8" />
                        <telerik:RadComboBoxItem runat="server" Text="09月" Value="9" />
                        <telerik:RadComboBoxItem runat="server" Text="10月" Value="10" />
                        <telerik:RadComboBoxItem runat="server" Text="11月" Value="11" />
                        <telerik:RadComboBoxItem runat="server" Text="12月" Value="12" />
                    </Items>
                </telerik:RadComboBox>
            </div>
            <div class="sixteen wide column">
                <telerik:RadChart ID="c" runat="server" Skin="Black" DefaultType="Bar" AutoLayout="True" PlotArea-EmptySeriesMessage-TextBlock-Text="无统计数据" Height="600">
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
                <telerik:RadGrid ID="grid" runat="server" CssClass="coreAuto coreFull coreCenter" AllowPaging="true" AutoGenerateColumns="False" LocalizationPath="../Language/" AllowSorting="True" PageSize="20" AllowFilteringByColumn="False" Culture="zh-CN" ShowGroupPanel="True" OnNeedDataSource="grid_OnNeedDataSource">
                    <ClientSettings AllowDragToGroup="True">
                    </ClientSettings>
                    <MasterTableView CssClass="coreAuto coreFull coreCenter" CommandItemDisplay="Top" HorizontalAlign="NotSet" ShowHeader="true" ShowHeadersWhenNoRecords="true" NoMasterRecordsText="">
                        <HeaderStyle HorizontalAlign="Center" />
                        <GroupHeaderItemStyle HorizontalAlign="Left"></GroupHeaderItemStyle>
                        <CommandItemSettings ShowAddNewRecordButton="false" />
                        <Columns>
                            <telerik:GridBoundColumn HeaderText="学校" DataField="学校" SortExpression="学校" UniqueName="学校" ItemStyle-HorizontalAlign="Left">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn HeaderText="教师" DataField="教师" SortExpression="教师" UniqueName="教师">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn HeaderText="发布视频" DataField="发布视频" SortExpression="发布视频" UniqueName="发布视频">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn HeaderText="发布文章" DataField="发布文章" SortExpression="发布文章" UniqueName="发布文章">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn HeaderText="发布课件" DataField="发布课件" SortExpression="发布课件" UniqueName="发布课件">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn HeaderText="发布试卷" DataField="发布试卷" SortExpression="发布试卷" UniqueName="发布试卷">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn HeaderText="浏览资源" DataField="浏览资源" SortExpression="浏览资源" UniqueName="浏览资源">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn HeaderText="收藏资源" DataField="收藏资源" SortExpression="收藏资源" UniqueName="收藏资源">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn HeaderText="下载资源" DataField="下载资源" SortExpression="下载资源" UniqueName="下载资源">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn HeaderText="评论资源" DataField="评论资源" SortExpression="评论资源" UniqueName="评论资源">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn HeaderText="回复评论" DataField="回复评论" SortExpression="回复评论" UniqueName="回复评论">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn HeaderText="评定资源" DataField="评定资源" SortExpression="评定资源" UniqueName="评定资源">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn HeaderText="获得积分" DataField="获得积分" SortExpression="获得积分" UniqueName="获得积分">
                            </telerik:GridBoundColumn>
                        </Columns>
                        <PagerStyle Mode="NextPrevAndNumeric" PageSizes="10,20,50,100" Position="Bottom" PageSizeControlType="RadComboBox" AlwaysVisible="true" PagerTextFormat="{4} 第{0}页，共{1}页；第{2}-{3}项，共{5}项" />
                    </MasterTableView>
                    <ClientSettings>
                        <Selecting AllowRowSelect="true" UseClientSelectColumnOnly="true" />
                    </ClientSettings>
                </telerik:RadGrid>
            </div>
        </telerik:RadAjaxPanel>
    </form>
</body>
</html>
