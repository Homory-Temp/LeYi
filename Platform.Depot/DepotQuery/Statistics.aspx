<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Statistics.aspx.cs" Inherits="DepotQuery_Statistics" %>

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
        <homory:SideBarSingle runat="server" ID="SideBarSingle" Crumb="日常查询 - 汇总统计" />
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
                        <div class="col-md-12">
                            <span class="btn btn-info">期初：</span>
                            <telerik:RadMonthYearPicker ID="ps" runat="server" LocalizationPath="~/Language" ShowPopupOnFocus="true" Width="100" AutoPostBack="false">
                                <DatePopupButton runat="server" Visible="false" />
                            </telerik:RadMonthYearPicker>
                            &nbsp;&nbsp;
                            <span class="btn btn-info">期末：</span>
                            <telerik:RadMonthYearPicker ID="pe" runat="server" LocalizationPath="~/Language" ShowPopupOnFocus="true" Width="100" AutoPostBack="false">
                                <DatePopupButton runat="server" Visible="false" />
                            </telerik:RadMonthYearPicker>
                            &nbsp;&nbsp;&nbsp;&nbsp;
                            <telerik:RadTextBox ID="name" runat="server" Width="120" EmptyMessage="物资名称"></telerik:RadTextBox>
                            &nbsp;&nbsp;&nbsp;&nbsp;
                            <input type="button" class="btn btn-tumblr dictionary" id="query" runat="server" value="查询" onserverclick="query_ServerClick" />
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12">
                        <telerik:RadGrid ID="grid" runat="server" AutoGenerateColumns="false" OnNeedDataSource="grid_NeedDataSource" AllowPaging="true" PageSize="15">
                            <MasterTableView>
                                <Columns>
                                    <telerik:GridBoundColumn DataField="CatalogPath" HeaderText="物资类别"></telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="Name" HeaderText="物资名称"></telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="S" HeaderText="期初数量" DataFormatString="{0:F2}"></telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="SM" HeaderText="期初金额" DataFormatString="{0:F2}"></telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="I" HeaderText="入库数量" DataFormatString="{0:F2}"></telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="IM" HeaderText="入库金额" DataFormatString="{0:F2}"></telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="U" HeaderText="出库数量" DataFormatString="{0:F2}"></telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="UM" HeaderText="出库金额" DataFormatString="{0:F2}"></telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="R" HeaderText="退换数量" DataFormatString="{0:F2}"></telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="RM" HeaderText="退换金额" DataFormatString="{0:F2}"></telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="O" HeaderText="报废数量" DataFormatString="{0:F2}"></telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="OM" HeaderText="报废金额" DataFormatString="{0:F2}"></telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="E" HeaderText="期末数量" DataFormatString="{0:F2}"></telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="EM" HeaderText="期末金额" DataFormatString="{0:F2}"></telerik:GridBoundColumn>
                                </Columns>
                                <NoRecordsTemplate>
                                    <div class="row">
                                        <div class="col-md-12 text-center"><div class="btn btn-warning">暂无记录</div></div>
                                    </div>
                                </NoRecordsTemplate>
                            </MasterTableView>
                        </telerik:RadGrid>
                        </div>
                    </div>
                </div>
            </div>
        </telerik:RadAjaxPanel>
    </form>
</body>
</html>
