<%@ Page Language="C#" AutoEventWireup="true" CodeFile="QueryTeacher.aspx.cs" Inherits="Go.GoQueryTeacher" %>

<%@ Register Src="~/Control/SideBar.ascx" TagPrefix="homory" TagName="SideBar" %>

<%@ Register assembly="Telerik.Web.UI" namespace="Telerik.Charting" tagprefix="telerik" %>

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
        <script>
            function mngRequestStarted(ajaxManager, eventArgs) 
            {
                if (eventArgs.get_eventTarget().indexOf("ExportToExcel") >= 0)
                    eventArgs.set_enableAjax(false);
            } 
        </script>
        <telerik:RadAjaxPanel ID="panel" runat="server" CssClass="ui left aligned page grid" style="margin:0;padding:0;" LoadingPanelID="loading" ClientEvents-OnRequestStart="mngRequestStarted">
            <div class="sixteen wide column">
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
                            <telerik:GridBoundColumn FilterControlWidth="30" DataField="账号" HeaderText="账号" SortExpression="账号" UniqueName="账号" FilterControlAltText="Filter 账号 column">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn FilterControlWidth="30" DataField="状态" HeaderText="状态" SortExpression="状态" UniqueName="状态" FilterControlAltText="Filter 状态 column">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn FilterControlWidth="30" DataField="同步" HeaderText="同步" SortExpression="同步" UniqueName="同步" FilterControlAltText="Filter 同步 column">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn FilterControlWidth="30" DataField="在编" HeaderText="在编" SortExpression="在编" UniqueName="在编" FilterControlAltText="Filter 在编 column">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn FilterControlWidth="30" DataField="学校" HeaderText="学校" SortExpression="学校" UniqueName="学校" FilterControlAltText="Filter 学校 column">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn FilterControlWidth="30" DataField="部门" HeaderText="部门" SortExpression="部门" UniqueName="部门" FilterControlAltText="Filter 部门 column">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn FilterControlWidth="30" DataField="主兼职" HeaderText="主兼职" SortExpression="主兼职" UniqueName="主兼职" FilterControlAltText="Filter 主兼职 column">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn FilterControlWidth="30" DataField="手机号码" HeaderText="手机号码" SortExpression="手机号码" UniqueName="手机号码" FilterControlAltText="Filter 手机号码 column">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn FilterControlWidth="30" DataField="电子邮件" HeaderText="电子邮件" SortExpression="电子邮件" UniqueName="电子邮件" FilterControlAltText="Filter 电子邮件 column">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn FilterControlWidth="30" DataField="身份证号" HeaderText="身份证号" SortExpression="身份证号" UniqueName="身份证号" FilterControlAltText="Filter 身份证号 column">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn FilterControlWidth="30" DataField="性别" HeaderText="性别" SortExpression="性别" UniqueName="性别" FilterControlAltText="Filter 性别 column">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn FilterControlWidth="30" DataField="籍贯" HeaderText="籍贯" SortExpression="籍贯" UniqueName="籍贯" FilterControlAltText="Filter 籍贯 column">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn FilterControlWidth="30" DataFormatString="{0:yyyy-MM-dd}" DataField="出生日期" HeaderText="出生日期" SortExpression="出生日期" UniqueName="出生日期" DataType="System.DateTime" FilterControlAltText="Filter 出生日期 column">
                            </telerik:GridBoundColumn>
                            <%--<telerik:GridBoundColumn FilterControlWidth="30" DataField="现居住地" HeaderText="现居住地" SortExpression="现居住地" UniqueName="现居住地" FilterControlAltText="Filter 现居住地 column">
                            </telerik:GridBoundColumn>--%>
                            <telerik:GridBoundColumn FilterControlWidth="30" DataField="民族" HeaderText="民族" SortExpression="民族" UniqueName="民族" FilterControlAltText="Filter 民族 column">
                            </telerik:GridBoundColumn>
                        </Columns>
                        <PagerStyle Mode="NextPrevAndNumeric" PageSizes="10,20,50,100" Position="Bottom" PageSizeControlType="RadComboBox" AlwaysVisible="true" PagerTextFormat="{4} 第{0}页，共{1}页；第{2}-{3}项，共{5}项" />
                    </MasterTableView>
                    <ClientSettings>
                        <Selecting AllowRowSelect="true" UseClientSelectColumnOnly="true" />
                    </ClientSettings>
                </telerik:RadGrid>
            </div>
            <div class="sixteen wide column">
                <telerik:RadChart ID="c" runat="server" Skin="Black" AutoLayout="True" PlotArea-EmptySeriesMessage-TextBlock-Text="无统计数据" Height="600">
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
        </telerik:RadAjaxPanel>
    </form>
</body>
</html>
