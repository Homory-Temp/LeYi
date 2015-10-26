<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Code.aspx.cs" Inherits="DepotScan_Code" %>

<%@ Register Src="~/Control/SideBarSingle.ascx" TagPrefix="homory" TagName="SideBarSingle" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=Edge,Chrome=1" />
    <meta name="viewport" content="width=device-width,initial-scale=1,minimum-scale=1,maximum-scale=1" />
    <script src="../Content/jQuery/jquery.min.js"></script>
    <link href="../assets/css/bootstrap.min.css" rel="stylesheet" />
    <link href="../assets/css/style-responsive.css" rel="stylesheet" />
    <link href="../assets/css/style.css" rel="stylesheet" />
    <link href="../Content/Core/css/common.css" rel="stylesheet" />
    <link href="../Content/Core/css/fix.css" rel="stylesheet" />
    <script src="../assets/js/bootstrap.min.js"></script>
    <script src="../Content/Homory/js/common.js"></script>
    <script src="../Content/Homory/js/notify.min.js"></script>
    <!--[if lt IE 9]>
	    <script src="../Content/Homory/js/html5shiv.js"></script>
	    <script src="../Content/Homory/js/respond.min.js"></script>
    <![endif]-->
</head>
<body>
    <form id="form" runat="server">
        <homory:SideBarSingle runat="server" ID="SideBarSingle" Crumb="物资条码 - 条码打印" />
        <telerik:RadAjaxPanel ID="ap" runat="server" CssClass="container-fluid" LoadingPanelID="loading">
            <div class="row">
                <div class="col-md-2" style="border-right: 1px solid #2B2B2B;">
                    <div class="row">
                        <div class="col-md-12">
                            <span class="btn btn-tumblr">物资类别：</span>
                            &nbsp;&nbsp;
                            <input type="button" class="btn btn-info" id="all" runat="server" value="清除选定" onserverclick="all_ServerClick" />
                            <input type="hidden" id="_all" runat="server" value="1" />
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12">
                            <telerik:RadTreeView ID="tree" runat="server" DataTextField="Name" DataValueField="Id" DataFieldID="Id" DataFieldParentID="ParentId" CheckBoxes="true" CheckChildNodes="true" OnNodeCheck="tree_NodeCheck">
                            </telerik:RadTreeView>
                        </div>
                    </div>
                </div>
                <div class="col-md-10" style="text-align: left;">
                    <div class="row">
                        <telerik:RadListView ID="view" runat="server" OnNeedDataSource="view_NeedDataSource" ItemPlaceholderID="holder" AllowPaging="true">
                            <ItemTemplate>
                                <asp:Panel runat="server" Visible='<%# (Container.DataItem as CodeObject).Single %>'>
                                    <div style="float: left; margin: 5px; border: solid 1px #2B2B2B; color: black;">
                                        <div style="width: 410px; height: 46px; line-height: 46px; font-size: 21px; vertical-align: middle; font-weight: bold;">
                                            <img src="../Common/配置/StoreLogo.png" style="height: 40px;" />
                                            乐翼教育云物资 <%# ((Container.DataItem as CodeObject).Fixed ? "固定资产标签" : "准固定资产标签") %>
                                        </div>
                                        <div style="width: 240px; height: 180px; float: left; text-align: left; font-size: 16px; padding-left: 20px;">
                                            <div style="margin-top: 20px;">
                                                <label style="font-weight: normal;">物资名称</label>：<%# Eval("Name") %>
                                            </div>
                                            <div style="margin-top: 0;">
                                                <label style="font-weight: normal;">规格型号</label>：<%# "{0}".Formatted((Container.DataItem as CodeObject).Specification) %>
                                            </div>
                                            <div style="margin-top: 0;">
                                                <label style="font-weight: normal;">存放地　</label>
                                                ：<%# (Container.DataItem as CodeObject).Place %>
                                            </div>
                                        </div>
                                        <div style="width: 150px; height: 180px; float: left; text-align: center;">
                                            <telerik:RadBarcode runat="server" Type="QRCode" Text='<%# Eval("Code") %>' OutputType="EmbeddedPNG" Width="150">
                                                <QRCodeSettings Mode="Alphanumeric" ErrorCorrectionLevel="M" ECI="None" Version="0" AutoIncreaseVersion="true" DotSize="5" />
                                            </telerik:RadBarcode>
                                            <%# Eval("Code") %>
                                        </div>
                                    </div>
                                </asp:Panel>
                                <asp:Panel runat="server" Visible='<%# !(Container.DataItem as CodeObject).Single %>'>
                                    <div style="float: left; margin: 5px; border: solid 1px #2B2B2B; color: black;">
                                        <div style="width: 410px; height: 46px; line-height: 46px; font-size: 21px; vertical-align: middle; font-weight: bold;">
                                            <img src="../Common/配置/StoreLogo.png" style="height: 40px;" />
                                            乐翼教育云物资 物资标签
                                        </div>
                                        <div style="width: 240px; height: 180px; float: left; text-align: left; font-size: 16px; padding-left: 20px;">
                                            <div style="margin-top: 20px;">
                                                <label style="font-weight: normal;">物资名称</label>：<%# Eval("Name") %>
                                            </div>
                                            <div style="margin-top: 0;">
                                                <label style="font-weight: normal;">规格型号</label>：<%# "{0}".Formatted((Container.DataItem as CodeObject).Specification) %>
                                            </div>
                                            <div style="margin-top: 0;">
                                                <label style="font-weight: normal;">物资分类</label>：<%# (Container.DataItem as CodeObject).CatalogPath %>
                                            </div>
                                        </div>
                                        <div style="width: 150px; height: 180px; float: left; text-align: center;">
                                            <telerik:RadBarcode runat="server" Type="QRCode" Text='<%# Eval("Code") %>' OutputType="EmbeddedPNG" Width="150">
                                                <QRCodeSettings Mode="Alphanumeric" ErrorCorrectionLevel="M" ECI="None" Version="0" AutoIncreaseVersion="true" DotSize="5" />
                                            </telerik:RadBarcode>
                                            <%# Eval("Code") %>
                                        </div>
                                    </div>
                                </asp:Panel>
                            </ItemTemplate>
                        </telerik:RadListView>
                    </div>
                    <div class="row">
                        <div class="col-md-4">&nbsp;</div>
                        <div class="col-md-4 text-center">
                            <telerik:RadDataPager ID="pager" runat="server" PagedControlID="view" BackColor="Transparent" BorderStyle="None" RenderMode="Auto" PageSize="10" OnPageIndexChanged="pager_PageIndexChanged">
                                <Fields>
                                    <telerik:RadDataPagerButtonField FieldType="FirstPrev"></telerik:RadDataPagerButtonField>
                                    <telerik:RadDataPagerButtonField FieldType="Numeric"></telerik:RadDataPagerButtonField>
                                    <telerik:RadDataPagerButtonField FieldType="NextLast"></telerik:RadDataPagerButtonField>
                                </Fields>
                            </telerik:RadDataPager>
                        </div>
                        <div class="col-md-4">&nbsp;</div>
                    </div>
                </div>
            </div>
        </telerik:RadAjaxPanel>
    </form>
</body>
</html>
