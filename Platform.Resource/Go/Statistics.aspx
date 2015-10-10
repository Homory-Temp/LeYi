<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Statistics.aspx.cs" Inherits="Go.GoCatalog" %>

<%@ Import Namespace="Homory.Model" %>

<%@ Register Src="~/Control/CommonCatalog.ascx" TagPrefix="homory" TagName="CommonCatalog" %>
<%@ Register Src="~/Control/CommonTop.ascx" TagPrefix="homory" TagName="CommonTop" %>
<%@ Register Src="~/Control/CommonBottom.ascx" TagPrefix="homory" TagName="CommonBottom" %>



<!DOCTYPE html>
<html>
<head runat="server">
    <title>资源平台 - 资源统计</title>
    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8">
    <meta http-equiv="Pragma" content="no-cache">
    <script src="../Script/jquery.min.js"></script>
    <link href="../Style/common.css" rel="stylesheet" />
    <link href="../Style/public.css" rel="stylesheet" />
    <link href="../Style/mhzy.css" rel="stylesheet" />
    <base target="_top" />
    <link href="../Style/login1.css" rel="stylesheet" />


</head>
<body>
    <form runat="server">
        <telerik:RadScriptManager ID="Rsm" runat="server">
            <Scripts>
                <asp:ScriptReference Assembly="Telerik.Web.UI" Name="Telerik.Web.UI.Common.Core.js" />
                <asp:ScriptReference Assembly="Telerik.Web.UI" Name="Telerik.Web.UI.Common.jQuery.js" />
                <asp:ScriptReference Assembly="Telerik.Web.UI" Name="Telerik.Web.UI.Common.jQueryInclude.js" />
            </Scripts>
        </telerik:RadScriptManager>
        <homory:CommonTop runat="server" ID="CommonTop" />
        <div class="srx-bg22">
            <div class="srx-wrap">
                <div class="portalMain w1000 clearfix">
                    <div class="Main w1000 clearfix eduSource">
                        <style>
                            .sChart {
                                float: left;
                                border: dashed 1px silver;
                            }
                        </style>
                        <asp:Repeater ID="repeater" runat="server" OnItemDataBound="repeater_ItemDataBound">
                            <ItemTemplate>
                                <telerik:RadHtmlChart runat="server" ID="RadarAreaChart" Width="498" Height="498" Transitions="true" Skin="Silk" CssClass="sChart">
                                    <PlotArea>
                                        <Series>
                                        </Series>
                                        <Appearance>
                                            <FillStyle BackgroundColor="Transparent"></FillStyle>
                                        </Appearance>
                                        <XAxis Color="Black" MajorTickType="Outside" MinorTickType="Outside"
                                            Reversed="false">
                                            <LabelsAppearance RotationAngle="0" Step="1" Skip="0">
                                            </LabelsAppearance>
                                            <MajorGridLines Color="#c8c8c8" Width="1"></MajorGridLines>
                                            <MinorGridLines Visible="false"></MinorGridLines>
                                            <Items>
                                            </Items>
                                        </XAxis>
                                        <YAxis Visible="true" MinValue="10" MaxValue="15">
                                            <MajorGridLines Color="#c8c8c8" Width="1" Visible="true"></MajorGridLines>
                                            <MinorGridLines Visible="true"></MinorGridLines>
                                        </YAxis>
                                    </PlotArea>
                                    <Appearance>
                                        <FillStyle BackgroundColor="Transparent"></FillStyle>
                                    </Appearance>
                                    <ChartTitle>
                                        <Appearance Align="Center" BackgroundColor="Transparent" Position="Top">
                                        </Appearance>
                                    </ChartTitle>
                                    <Legend>
                                        <Appearance BackgroundColor="Transparent" Position="Bottom">
                                        </Appearance>
                                    </Legend>
                                </telerik:RadHtmlChart>
                            </ItemTemplate>
                        </asp:Repeater>
                        <div style="clear: both;"></div>
                    </div>
                </div>
                <homory:CommonBottom runat="server" ID="CommonBottom" />
            </div>
        </div>
    </form>
</body>
</html>
