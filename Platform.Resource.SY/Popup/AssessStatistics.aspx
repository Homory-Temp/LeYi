<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AssessStatistics.aspx.cs" Inherits="Popup_AssessStatistics" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <telerik:RadScriptManager runat="server">
        </telerik:RadScriptManager>
        <telerik:RadAjaxPanel runat="server">
            <telerik:RadHtmlChart runat="server" ID="chart" Width="430" Height="430" Transitions="true" Skin="Silk">
                <PlotArea>
                    <Series></Series>
                    <Appearance>
                        <FillStyle BackgroundColor="Transparent" />
                    </Appearance>
                    <XAxis Color="Black" MajorTickType="Outside" MinorTickType="Outside" Reversed="false">
                        <LabelsAppearance RotationAngle="0" Step="1" Skip="0"></LabelsAppearance>
                        <MajorGridLines Color="#c8c8c8" Width="1" />
                        <MinorGridLines Visible="false" />
                    </XAxis>
                    <YAxis Visible="true">
                        <MajorGridLines Color="#c8c8c8" Width="1" />
                        <MinorGridLines Visible="false" />
                    </YAxis>
                </PlotArea>
                <Appearance>
                    <FillStyle BackgroundColor="Transparent" />
                </Appearance>
                <ChartTitle Text="">
                    <Appearance Visible="false"></Appearance>
                </ChartTitle>
                <Legend>
                    <Appearance BackgroundColor="Transparent" Position="Bottom"></Appearance>
                </Legend>
            </telerik:RadHtmlChart>
            <br />
            <telerik:RadGrid ID="grid" runat="server" AutoGenerateColumns="true" Skin="MetroTouch" Height="400" Font-Size="13px">
                <MasterTableView Font-Size="13px" HorizontalAlign="Center">
                    <NoRecordsTemplate>暂无评估</NoRecordsTemplate>
                </MasterTableView>
            </telerik:RadGrid>
        </telerik:RadAjaxPanel>
    </div>
    </form>
</body>
</html>
