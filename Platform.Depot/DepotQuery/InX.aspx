<%@ Page Language="C#" AutoEventWireup="true" CodeFile="InX.aspx.cs" Inherits="DepotQuery_InX" %>

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
        <homory:SideBarSingle runat="server" ID="SideBarSingle" Crumb="日常查询 - 入库查询" />
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
                    <telerik:RadTextBox ID="toSearch" runat="server" EmptyMessage="物资名称"></telerik:RadTextBox>
                            &nbsp;&nbsp;&nbsp;&nbsp;
                    <telerik:RadComboBox ID="age" runat="server" MaxHeight="203" AutoPostBack="false" Width="120" AppendDataBoundItems="true" DataTextField="Name" DataValueField="Name">
                    </telerik:RadComboBox>
                            &nbsp;&nbsp;&nbsp;&nbsp;
                    <telerik:RadComboBox ID="place" runat="server" MaxHeight="203" AutoPostBack="false" Width="120" AppendDataBoundItems="true" DataTextField="Name" DataValueField="Name">
                    </telerik:RadComboBox>
                            &nbsp;&nbsp;&nbsp;&nbsp;
                    <telerik:RadComboBox ID="people" runat="server" MaxHeight="203" AutoPostBack="false" Width="120" DataTextField="Name" DataValueField="Id" AppendDataBoundItems="true">
                    </telerik:RadComboBox>
                            &nbsp;&nbsp;&nbsp;&nbsp;
                    <input type="button" class="btn btn-tumblr" id="query" runat="server" value="查询" onserverclick="query_ServerClick" />
                        </div>
                    </div>
                    <div class="row">&nbsp;</div>
                    <div class="row">
                        <input type="hidden" id="___total" runat="server" />
                        <!-- Start Printing -->
                        <telerik:RadListView ID="view" runat="server" OnNeedDataSource="view_NeedDataSource" ItemPlaceholderID="holder" AllowPaging="true">
                            <LayoutTemplate>
                                <div class="col-md-12">
                                    <table class="storeTable text-center">
                                        <tr>
                                            <th>购置单</th>
                                            <th>入库日期</th>
                                            <th>物资名称</th>
                                            <th>单位</th>
                                            <th>数量</th>
                                            <th>单价</th>
                                            <th>合计</th>
                                            <th style='<%# (Depot.Featured(Models.DepotType.幼儿园) ? "display: ;": "display: none;") %>'>年龄段</th>
                                            <th>存放地</th>
                                            <th>操作人</th>
                                            <th>备注</th>
                                            <th>操作</th>
                                        </tr>
                                        <asp:PlaceHolder ID="holder" runat="server"></asp:PlaceHolder>
                                        <tr>
                                            <td colspan="12">总计：<%# ___total.Value %></td>
                                        </tr>
                                    </table>
                                </div>
                            </LayoutTemplate>
                            <ItemTemplate>
                                <tr>
                                    <td>
                                        <asp:HyperLink runat="server" ForeColor="#3E5A70" Target="_blank" Text='<%# Eval("OrderName") %>' NavigateUrl='<%# "../DepotQuery/InPrint?DepotId={0}&OrderId={1}".Formatted(Depot.Id, Eval("OrderId")) %>'></asp:HyperLink></td>
                                    <td><%# Eval("Time").ToDay() %></td>
                                    <td><%# Eval("Name") %></td>
                                    <td><%# Eval("Unit") %></td>
                                    <td><%# Eval("Amount").ToAmount(Depot.Featured(Models.DepotType.小数数量库)) %></td>
                                    <td><%# decimal.Divide((decimal)Eval("Total"), (decimal)Eval("Amount")).ToMoney() %></td>
                                    <td><%# Eval("Total").ToMoney() %></td>
                                    <td style='<%# (Depot.Featured(Models.DepotType.幼儿园) ? "display: ;": "display: none;") %>'><%# Eval("Age") %></td>
                                    <td><%# Eval("Place") %></td>
                                    <td><%# Eval("Operator") %></td>
                                    <td><%# Eval("Note") %></td>
                                    <td>
                                        <input type="button" class="btn btn-tumblr" value="退货" id="redo" runat="server" match='<%# Eval("InId") %>' visible='<%# CanRedo((Guid)Eval("InId")) %>' onserverclick="redo_ServerClick" />
                                        <input type="button" class="btn btn-tumblr" value="编辑" id="edit" runat="server" match='<%# Eval("InId") %>' visible='<%# RightRoot || (!(bool)Eval("Done")) %>' onserverclick="edit_ServerClick" />
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
            <div class="row">
                <div class="col-md-12 text-center">
                    <input type="button" class="btn btn-tumblr" id="print" value="打印" onclick="printDepot();" />
                </div>
            </div>
                    <%--<div class="row">
                        <div class="col-md-3">&nbsp;</div>
                        <div class="col-md-6 text-center">
                            <telerik:RadDataPager ID="pager" runat="server" PagedControlID="view" BackColor="Transparent" BorderStyle="None" RenderMode="Auto" PageSize="10" OnPageIndexChanged="pager_PageIndexChanged">
                                <Fields>
                                    <telerik:RadDataPagerButtonField FieldType="FirstPrev"></telerik:RadDataPagerButtonField>
                                    <telerik:RadDataPagerButtonField FieldType="Numeric"></telerik:RadDataPagerButtonField>
                                    <telerik:RadDataPagerButtonField FieldType="NextLast"></telerik:RadDataPagerButtonField>
                                </Fields>
                            </telerik:RadDataPager>
                        </div>
                        <div class="col-md-3">&nbsp;</div>
                    </div>--%>
                </div>
            </div>
        </telerik:RadAjaxPanel>
    </form>
</body>
</html>
