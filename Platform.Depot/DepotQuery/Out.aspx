<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Out.aspx.cs" Inherits="DepotQuery_Out" %>

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
    <script>
        function printDepot() {
            bdhtml = window.document.body.innerHTML;
            sprnstr = "<!-- Start Printing -->";
            eprnstr = "<!-- End Printing -->";
            prnhtml = bdhtml.substring(bdhtml.indexOf(sprnstr) + 23);
            prnhtml = prnhtml.substring(0, prnhtml.indexOf(eprnstr));
            prnhtml = "<body>" + prnhtml + "</body>";
            window.document.body.innerHTML = prnhtml;
            window.print();
            window.document.body.innerHTML = bdhtml;
            return false;
        }
    </script>
</head>
<body>
    <form id="form" runat="server">
        <homory:SideBarSingle runat="server" ID="SideBarSingle" Crumb="日常查询 - 报废查询" />
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
                        <div class="col-md-12 text-center">
                    <telerik:RadDatePicker ID="periodx" runat="server" LocalizationPath="~/Language" ShowPopupOnFocus="true" Width="120" AutoPostBack="false">
                        <DatePopupButton runat="server" Visible="false" />
                    </telerik:RadDatePicker>
                    &nbsp;&nbsp;-&nbsp;&nbsp;
                            <telerik:RadDatePicker ID="period" runat="server" LocalizationPath="~/Language" ShowPopupOnFocus="true" Width="120" AutoPostBack="false">
                                <DatePopupButton runat="server" Visible="false" />
                            </telerik:RadDatePicker>
                            &nbsp;&nbsp;&nbsp;&nbsp;
                    <telerik:RadComboBox ID="outType" runat="server" AutoPostBack="false" MaxHeight="203" Width="120" AppendDataBoundItems="false" DataTextField="Name" DataValueField="Id">
                        <Items>
                            <telerik:RadComboBoxItem Text="审批状态" Value="*" Selected="true" />
                            <telerik:RadComboBoxItem Text="已审" Value="1" />
                            <telerik:RadComboBoxItem Text="未审" Value="2" />
                        </Items>
                    </telerik:RadComboBox>
                            &nbsp;&nbsp;&nbsp;&nbsp;
                    <telerik:RadComboBox ID="peopleX" runat="server" MaxHeight="203" AutoPostBack="false" Width="200" DataTextField="Name" DataValueField="Id" AppendDataBoundItems="true">
                    </telerik:RadComboBox>
                            &nbsp;&nbsp;&nbsp;&nbsp;
                    <input type="button" class="btn btn-tumblr" id="query" runat="server" value="查询" onserverclick="query_ServerClick" />
                        </div>
                    </div>
                    <div class="row">&nbsp;</div>
                    <div class="row">
                        <!-- Start Printing -->
                        <telerik:RadListView ID="view" runat="server" OnNeedDataSource="view_NeedDataSource" ItemPlaceholderID="holder" AllowPaging="true">
                            <LayoutTemplate>
                                <div class="col-md-12">
                                    <table class="storeTable text-center">
                                        <tr>
                                            <th>报废时间</th>
                                            <th>报废申请人</th>
                                            <th>物资名称</th>
                                            <th>报废原因</th>
                                            <th>申请数量</th>
                                            <th>审批通过数量</th>
                                            <th>审批状态</th>
                                            <th style='<%# (RightRoot ? "display: ;": "display: none;") %>'>操作</th>
                                        </tr>
                                        <asp:PlaceHolder ID="holder" runat="server"></asp:PlaceHolder>
                                    </table>
                                </div>
                            </LayoutTemplate>
                            <ItemTemplate>
                                <tr>
                                    <td><%# Eval("Time").ToDay() %></td>
                                    <td><%# Eval("UserName") %></td>
                                    <td><%# Eval("Name") %></td>
                                    <td><%# Eval("Reason") %></td>
                                    <td><%# Eval("ToAmount").ToAmount(Depot.Featured(Models.DepotType.小数数量库)) %></td>
                                    <td><%# Eval("Amount").ToAmount(Depot.Featured(Models.DepotType.小数数量库)) %></td>
                                    <td><%# Eval("State").ToString() == "1" ? "已审" : "未审" %></td>
                                    <td style='<%# (RightRoot ? "display: ;": "display: none;") %>'>
                                        <telerik:RadNumericTextBox ID="amount" runat="server" EmptyMessage="报废数" Visible='<%# Eval("State").ToString() == "2" %>' MinValue="0" MaxValue='<%# (double)((decimal)Eval("ToAmount")) %>' Width="120" NumberFormat-DecimalDigits="0" DataType="System.Decimal" AllowOutOfRangeAutoCorrect="true"></telerik:RadNumericTextBox>
                                        <input type="button" class="btn btn-tumblr" value="确认" id="go_out" visible='<%# Eval("State").ToString() == "2" %>' match='<%# Eval("Id") %>' runat="server" onserverclick="go_out_ServerClick" />
                                    </td>
                                </tr>
                            </ItemTemplate>
                            <EmptyDataTemplate>
                                <div class="col-md-12 text-center">
                                    <div class="btn btn-warning">暂无记录</div>
                                </div>
                            </EmptyDataTemplate>
                        </telerik:RadListView>
                        <!-- End Printing -->
                    </div>
            <%--<div class="row">
                <div class="col-md-12 text-center">
                    <input type="button" class="btn btn-tumblr" id="print" value="打印" onclick="printDepot();" />
                </div>
            </div>--%>
                    <div class="row">
                        <div class="col-md-3">&nbsp;</div>
                        <div class="col-md-6 text-center">
                            <telerik:RadDataPager ID="pager" runat="server" PagedControlID="view" BackColor="Transparent" BorderStyle="None" RenderMode="Auto" PageSize="10">
                                <Fields>
                                    <telerik:RadDataPagerButtonField FieldType="FirstPrev"></telerik:RadDataPagerButtonField>
                                    <telerik:RadDataPagerButtonField FieldType="Numeric"></telerik:RadDataPagerButtonField>
                                    <telerik:RadDataPagerButtonField FieldType="NextLast"></telerik:RadDataPagerButtonField>
                                </Fields>
                            </telerik:RadDataPager>
                        </div>
                        <div class="col-md-3">&nbsp;</div>
                    </div>
                </div>
            </div>
        </telerik:RadAjaxPanel>
    </form>
</body>
</html>
