﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Object.aspx.cs" Inherits="DepotQuery_Object" %>

<%@ Register Src="~/Control/SideBar.ascx" TagPrefix="homory" TagName="SideBar" %>

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
    <style>
        .objInfo {
            color: black;
        }
    </style>
    <script>
        function showPic(obj) {
            var id = $(obj).attr("src");
            if (id)
                window.open(id, '_blank');
        }
    </script>
    <script>
        function sg(sender, e) {
            if (e.get_keyCode() == 13) {
                $("#search").click();
            }
        }
    </script>
</head>
<body>
    <form id="form" runat="server">
        <telerik:RadScriptManager ID="sm" runat="server"></telerik:RadScriptManager>
        <telerik:RadAjaxPanel ID="ap" runat="server" CssClass="container" LoadingPanelID="loading">
            <div class="row">
                <div class="col-md-2">
                    <span class="btn btn-tumblr">物资详情</span>
                </div>
                <div class="col-md-6 text-center">
                    <span class="btn btn-danger" id="name" runat="server"></span>
                </div>
                <div class="col-md-4 text-right">
                    <telerik:RadTextBox ID="toSearch" runat="server" Width="120" EmptyMessage="物资名称" ClientEvents-OnKeyPress="sg"></telerik:RadTextBox>
                            <input id="search" runat="server" type="button" class="btn btn-info" value="检索" onserverclick="search_ServerClick" />
                </div>
            </div>
            <div class="row">
                <div class="col-md-12 text-center">
                    <hr style="color: #2B2B2B; margin-top: 4px;" />
                </div>
            </div>
            <div class="row">
                <div class="col-md-12">
                    <table class="storeTable">
                        <tr>
                            <td style="width: 15%;">
                                <span class="btn btn-info dictionaryX">总库存：</span>
                            </td>
                            <td style="width: 35%;">
                                <span id="total" runat="server"></span>&nbsp;<span id="unitx" runat="server"></span>
                            </td>
                            <td style="width: 15%;">
                                <span class="btn btn-info dictionaryX">在库数：</span>
                            </td>
                            <td style="width: 35%;">
                                <span id="no" runat="server"></span>&nbsp;<span id="unit" runat="server"></span>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 15%;">
                                <span class="btn btn-info dictionaryX">类别：</span>
                            </td>
                            <td id="fk1" runat="server" style="width: 35%;">
                                <span id="cn" runat="server"></span>
                            </td>
                            <td id="fk2" runat="server" style="width: 15%;">
                                <span class="btn btn-info dictionaryX">年龄段</span>
                            </td>
                            <td id="fk3" runat="server" style="width: 85%;">
                                <span id="age" runat="server"></span>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 15%;">
                                <span class="btn btn-info dictionaryX">品牌：</span>
                            </td>
                            <td style="width: 35%;">
                                <span id="brand" runat="server"></span>
                            </td>
                            <td style="width: 15%;">
                                <span class="btn btn-info dictionaryX">规格型号：</span>
                            </td>
                            <td style="width: 35%;">
                                <span id="sp" runat="server"></span>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 15%;">
                                <span class="btn btn-info dictionaryX">备注：</span>
                            </td>
                            <td colspan="3" style="width: 85%; text-align: left; line-height: 20px;">
                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span id="note" runat="server"></span>
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
            <div class="row">&nbsp;</div>
            <div class="row">
                <div class="col-md-12">
                    <div class="row">
                        <div class="col-md-6" id="da" runat="server">
                            <img class="img-responsive" onclick="showPic(this);" style="width: 100%; cursor: pointer;" id="pa" runat="server" />
                        </div>
                        <div class="col-md-6" id="db" runat="server">
                            <img class="img-responsive" onclick="showPic(this);" style="width: 100%; cursor: pointer;" id="pb" runat="server" />
                        </div>
                    </div>
                </div>
                <div class="col-md-12">
                    <div class="row">
                        <div class="col-md-6" id="dc" runat="server">
                            <img class="img-responsive" onclick="showPic(this);" style="width: 100%; cursor: pointer;" id="pc" runat="server" />
                        </div>
                        <div class="col-md-6" id="dd" runat="server">
                            <img class="img-responsive" onclick="showPic(this);" style="width: 100%; cursor: pointer;" id="pd" runat="server" />
                        </div>
                    </div>
                </div>
            </div>
            <div class="row">&nbsp;</div>
            <div class="row"><div class="col-md-12"><span class="btn btn-info dictionaryX">存放地</span></div></div>
            <div class="row">
                <telerik:RadGrid ID="grid" runat="server" CssClass="col-md-12 text-center" AutoGenerateColumns="false" LocalizationPath="../Language" AllowSorting="True" PageSize="20" GridLines="None" OnNeedDataSource="grid_NeedDataSource" OnBatchEditCommand="grid_BatchEditCommand">
                    <MasterTableView EditMode="Batch" DataKeyNames="Id,Fixed,Ordinal" CommandItemDisplay="Top" CommandItemSettings-ShowAddNewRecordButton="false" HorizontalAlign="NotSet" ShowHeader="true" ShowHeadersWhenNoRecords="true">
                        <BatchEditingSettings EditType="Row" OpenEditingEvent="DblClick" />
                        <HeaderStyle HorizontalAlign="Center" Font-Bold="true" />
                        <Columns>
                            <telerik:GridTemplateColumn HeaderStyle-CssClass="xxx" HeaderText="编号" DataField="Ordinal" SortExpression="Ordinal" UniqueName="Ordinal" ReadOnly="true">
                                <ItemTemplate>
                                    <asp:Label runat="server" Text='<%# (int)Eval("Ordinal") == -1 ? (Container.ItemIndex + 1).ToString() : Eval("Ordinal") %>'></asp:Label>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridTemplateColumn HeaderStyle-BorderColor="Black" HeaderText="卡片编号" DataField="Number" SortExpression="Number" UniqueName="Number">
                                <ItemTemplate>
                                    <asp:Label runat="server" Text='<%# Eval("Number") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <telerik:RadTextBox ID="Number" runat="server" EnabledStyle-HorizontalAlign="Center" Text='<%# Bind("Number") %>'>
                                    </telerik:RadTextBox>
                                </EditItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridTemplateColumn HeaderStyle-BorderColor="Black" HeaderText="购置日期" DataField="Time" SortExpression="Time" UniqueName="Time">
                                <ItemTemplate>
                                    <asp:Label runat="server" Text='<%# Eval("Time").ToDay() %>'></asp:Label>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridTemplateColumn HeaderStyle-BorderColor="Black" HeaderText="条码" DataField="Ordinal" SortExpression="Ordinal" UniqueName="Ordinal" ReadOnly="true">
                                <ItemTemplate>
                                    <asp:HyperLink runat="server" ForeColor="#3E5A70" Target="_blank" Text='<%# Eval("Code") %>' NavigateUrl='<%# "../DepotScan/Flow?DepotId={0}&Code={1}".Formatted(Depot.Id, Eval("Code")) %>'></asp:HyperLink>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridTemplateColumn HeaderStyle-BorderColor="Black" HeaderText="存放地" DataField="Place" SortExpression="Place" UniqueName="Place">
                                <ItemTemplate>
                                    <asp:Label runat="server" Text='<%# Eval("Place") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <telerik:RadTextBox ID="Place" runat="server" EnabledStyle-HorizontalAlign="Center" Text='<%# Bind("Place") %>'>
                                    </telerik:RadTextBox>
                                </EditItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridTemplateColumn HeaderStyle-BorderColor="Black" HeaderText="使用状态" DataField="State" SortExpression="State" UniqueName="State" ReadOnly="true">
                                <ItemTemplate>
                                    <asp:Label runat="server" Text='<%# Eval("State") %>' ToolTip='<%# Eval("State").ToString() == "已报废" ? ((DateTime)Eval("StateTime")).ToString("yyyy-MM-dd") : "" %>'></asp:Label>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridTemplateColumn HeaderStyle-BorderColor="Black" HeaderText="固产分库编号" DataField="NumberX" SortExpression="NumberX" UniqueName="NumberX">
                                <ItemTemplate>
                                    <asp:HyperLink runat="server" Text='<%# Eval("NumberX") %>' NavigateUrl='<%# "../DepotQuery/ObjectSingle?DepotId={0}&Number={1}".Formatted(Depot.Id, Server.UrlEncode(Eval("NumberX").ToString())) %>'></asp:HyperLink>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <telerik:RadTextBox ID="NumberX" runat="server" EnabledStyle-HorizontalAlign="Center" Text='<%# Bind("NumberX") %>'>
                                    </telerik:RadTextBox>
                                </EditItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridTemplateColumn HeaderStyle-BorderColor="Black" HeaderText="数量" ReadOnly="True" DataField="Amount" SortExpression="Amount" UniqueName="Amount">
                                <ItemTemplate>
                                    <asp:Label runat="server" Text='<%# Eval("Amount").ToAmount() %>'></asp:Label>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridTemplateColumn HeaderStyle-BorderColor="Black" HeaderText="单价" ReadOnly="True" DataField="PriceX" SortExpression="PriceX" UniqueName="PriceX">
                                <ItemTemplate>
                                    <asp:Label runat="server" Text='<%# Eval("PriceX").ToMoney() %>'></asp:Label>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridTemplateColumn HeaderStyle-BorderColor="Black" HeaderText="总价" ReadOnly="True" DataField="TotalX" SortExpression="TotalX" UniqueName="TotalX">
                                <ItemTemplate>
                                    <asp:Label runat="server" Text='<%# Eval("TotalX").ToMoney() %>'></asp:Label>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                        </Columns>
                    </MasterTableView>
                </telerik:RadGrid>
            </div>
            <div class="row">&nbsp;</div>
            <div class="row"><div class="col-md-12"><span class="btn btn-info dictionaryX">借出记录</span></div></div>
            <div class="row">
                <telerik:RadGrid ID="gridX" runat="server" CssClass="col-md-12 text-center" AutoGenerateColumns="false" LocalizationPath="../Language" AllowSorting="True" PageSize="20" GridLines="None" OnNeedDataSource="gridX_NeedDataSource">
                    <MasterTableView CommandItemDisplay="None" HorizontalAlign="NotSet" ShowHeader="true" ShowHeadersWhenNoRecords="true">
                        <HeaderStyle HorizontalAlign="Center" Font-Bold="true" />
                        <Columns>
                            <telerik:GridTemplateColumn HeaderText="借用人" ItemStyle-Width="20%">
                                <ItemTemplate>
                                    <asp:Label runat="server" Text='<%# Eval("UserName") %>'></asp:Label>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridTemplateColumn HeaderStyle-BorderColor="Black" HeaderText="借用数量" ItemStyle-Width="20%">
                                <ItemTemplate>
                                    <asp:Label runat="server" Text='<%# Eval("Amount").ToAmount(Depot.Featured(Models.DepotType.小数数量库)) %>'></asp:Label>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridTemplateColumn HeaderStyle-BorderColor="Black" HeaderText="借用日期" ItemStyle-Width="20%">
                                <ItemTemplate>
                                    <asp:Label runat="server" Text='<%# Eval("Time").ToDay() %>'></asp:Label>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridTemplateColumn HeaderStyle-BorderColor="Black" HeaderText="待还数量" ItemStyle-Width="20%">
                                <ItemTemplate>
                                    <asp:Label runat="server" Text='<%# Eval("Type").ToString() == "1" ? "0" : ((decimal)Eval("Amount") - (decimal)Eval("ReturnedAmount")).ToAmount(Depot.Featured(Models.DepotType.小数数量库))%>'></asp:Label>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridTemplateColumn HeaderStyle-BorderColor="Black" HeaderText="待还日期" ItemStyle-Width="20%">
                                <ItemTemplate>
                                    <asp:Label runat="server" Text='<%# (int)Eval("Period") == 0 ? "不限期" : ((DateTime)Eval("Time")).AddDays((int)Eval("Period")).ToDay() %>'></asp:Label>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                        </Columns>
                    </MasterTableView>
                </telerik:RadGrid>
            </div>
            <div class="row">&nbsp;</div>
        </telerik:RadAjaxPanel>
        <style>
            html .RadGrid_Bootstrap {
                border: none;
            }
            html .RadGrid_Bootstrap .rgMasterTable {
                border-top: 1px solid black;
                border-left: 1px solid black;
                border-right: 1px solid black;
            }
            html .RadGrid_Bootstrap .rgRow > td, html .RadGrid_Bootstrap .rgAltRow > td {
                border-color: black;
            }
            html .RadGrid_Bootstrap .rgHeader, html .RadGrid_Bootstrap th.rgResizeCol, html .RadGrid_Bootstrap .rgHeaderWrapper, html .RadGrid_Bootstrap .rgMultiHeaderRow th.rgHeader {
                border-bottom: 1px solid black;
            }
            html .RadGrid_Bootstrap .rgCommandTable td {
                border-bottom: 1px solid black;
                border-left: none;
            }
            html .RadGrid_Bootstrap .rgNoRecords td {
                border-bottom: none;
            }
        </style>
    </form>
</body>
</html>
