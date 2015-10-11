<%@ Page Language="C#" AutoEventWireup="true" CodeFile="QueryStudent.aspx.cs" Inherits="Go.GoQueryStudent" %>

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
        <script>
            function mngRequestStarted(ajaxManager, eventArgs) {
                if (eventArgs.get_eventTarget().indexOf("ExportToExcel") >= 0)
                    eventArgs.set_enableAjax(false);
            }
        </script>
        <telerik:RadAjaxPanel ID="panel" runat="server" CssClass="container-fluid" LoadingPanelID="loading" ClientEvents-OnRequestStart="mngRequestStarted">
            <div class="row">
                <div class="col-md-12">
                    <telerik:RadGrid ID="grid" runat="server" CssClass="coreAuto coreFull coreCenter" AllowPaging="true" AutoGenerateColumns="False" LocalizationPath="../Language/" AllowSorting="True" PageSize="20" AllowFilteringByColumn="True" Culture="zh-CN" ShowGroupPanel="True" OnInit="grid_OnInit" OnItemCommand="grid_OnItemCommand" OnNeedDataSource="grid_NeedDataSource">
                        <ClientSettings AllowDragToGroup="True">
                        </ClientSettings>
                        <MasterTableView CssClass="coreAuto coreFull coreCenter" CommandItemDisplay="Top" HorizontalAlign="NotSet" ShowHeader="true" ShowHeadersWhenNoRecords="true" NoMasterRecordsText="">
                            <HeaderStyle HorizontalAlign="Center" />
                            <GroupHeaderItemStyle HorizontalAlign="Left"></GroupHeaderItemStyle>
                            <CommandItemSettings ExportToExcelText="导出" ShowAddNewRecordButton="False" ShowExportToExcelButton="True"></CommandItemSettings>
                            <Columns>
                                <telerik:GridBoundColumn FilterControlWidth="30" DataField="姓名" HeaderText="姓名" SortExpression="姓名" UniqueName="姓名" FilterControlAltText="Filter 姓名 column">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn FilterControlWidth="30" DataField="状态" ReadOnly="True" HeaderText="状态" SortExpression="状态" UniqueName="状态" FilterControlAltText="Filter 状态 column">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn FilterControlWidth="30" DataField="学校" HeaderText="学校" SortExpression="学校" UniqueName="学校" FilterControlAltText="Filter 学校 column">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn FilterControlWidth="30" DataField="届" HeaderText="学届" SortExpression="届" UniqueName="届" FilterControlAltText="Filter 届 column">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn FilterControlWidth="30" DataField="班级" HeaderText="班级" SortExpression="班级" UniqueName="班级" FilterControlAltText="Filter 班级 column">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn FilterControlWidth="30" DataField="学号" HeaderText="学号" SortExpression="学号" UniqueName="学号" DataType="System.Int32" FilterControlAltText="Filter 学号 column">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn FilterControlWidth="30" DataField="学籍号" HeaderText="学籍号" SortExpression="学籍号" UniqueName="学籍号" FilterControlAltText="Filter 学籍号 column">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn FilterControlWidth="30" DataField="身份证号" HeaderText="身份证号" SortExpression="身份证号" UniqueName="身份证号" FilterControlAltText="Filter 身份证号 column">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn FilterControlWidth="30" DataField="性别" ReadOnly="True" HeaderText="性别" SortExpression="性别" UniqueName="性别" FilterControlAltText="Filter 性别 column">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn FilterControlWidth="30" DataField="籍贯" HeaderText="籍贯" SortExpression="籍贯" UniqueName="籍贯" FilterControlAltText="Filter 籍贯 column">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn FilterControlWidth="30" DataField="出生日期" DataFormatString="{0:yyyy-MM-dd}" HeaderText="出生日期" SortExpression="出生日期" UniqueName="出生日期" DataType="System.DateTime" FilterControlAltText="Filter 出生日期 column">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn FilterControlWidth="30" DataField="现居住地" HeaderText="现居住地" SortExpression="现居住地" UniqueName="现居住地" FilterControlAltText="Filter 现居住地 column">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn FilterControlWidth="30" DataField="民族" HeaderText="民族" SortExpression="民族" UniqueName="民族" FilterControlAltText="Filter 民族 column">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn FilterControlWidth="30" DataField="联系人" HeaderText="联系人" SortExpression="联系人" UniqueName="联系人" FilterControlAltText="Filter 联系人 column">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn FilterControlWidth="30" DataField="联系号码" HeaderText="联系号码" SortExpression="联系号码" UniqueName="联系号码" FilterControlAltText="Filter 联系号码 column">
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
            <div class="row">&nbsp;</div>
            <div class="row">
                <div class="col-md-12" style="text-align: center; overflow: scroll;">
                    <telerik:RadChart ID="c" runat="server" Skin="Black" AutoLayout="True" PlotArea-EmptySeriesMessage-TextBlock-Text="无统计数据" Height="400" Style="margin: auto; width: 100%;">
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
            </div>
        </telerik:RadAjaxPanel>
    </form>
</body>
</html>
