<%@ Page Language="C#" AutoEventWireup="true" CodeFile="UseX.aspx.cs" Inherits="DepotQuery_UseX" %>

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
    <script>
        function sg(sender, e) {
            if (e.get_keyCode() == 13) {
                $("#query").click();
            }
        }
    </script>
</head>
<body>
    <form id="form" runat="server">
        <homory:SideBarSingle runat="server" ID="SideBarSingle" Crumb="日常查询 - 出库查询" />
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
                    <telerik:RadComboBox ID="useTypeX" runat="server" AutoPostBack="false" MaxHeight="203" Width="120" AppendDataBoundItems="false">
                        <Items>
                            <telerik:RadComboBoxItem Text="借还状态" Value="*" />
                            <telerik:RadComboBoxItem Text="待归还" Value="1" Selected="true" />
                            <telerik:RadComboBoxItem Text="已归还" Value="2" />
                            <telerik:RadComboBoxItem Text="领用" Value="3" />
                        </Items>
                    </telerik:RadComboBox>
                            &nbsp;&nbsp;&nbsp;&nbsp;
                            <telerik:RadTextBox ID="name" runat="server" Width="120" EmptyMessage="物资名称" ClientEvents-OnKeyPress="sg"></telerik:RadTextBox>
                            &nbsp;&nbsp;&nbsp;&nbsp;
                    <telerik:RadComboBox ID="age" runat="server" AutoPostBack="false" MaxHeight="203" Width="120" AppendDataBoundItems="true" DataTextField="Name" DataValueField="Name" Filter="Contains" AllowCustomText="true">
                        <ItemTemplate>
                            <%# Eval("Name") %><span style="display: none;"><%# Eval("PinYin") %></span>
                        </ItemTemplate>
                    </telerik:RadComboBox>
                            &nbsp;&nbsp;&nbsp;&nbsp;
                    <telerik:RadComboBox ID="peopleX" runat="server" AutoPostBack="false" MaxHeight="203" Width="120" DataTextField="Name" DataValueField="Id" AppendDataBoundItems="true" Filter="Contains" AllowCustomText="true">
                        <ItemTemplate>
                            <%# Eval("Name") %><span style="display: none;"><%# Eval("PinYin") %></span>
                        </ItemTemplate>
                    </telerik:RadComboBox>
                            &nbsp;&nbsp;&nbsp;&nbsp;
                    <telerik:RadComboBox ID="people" runat="server" AutoPostBack="false" MaxHeight="203" Width="120" DataTextField="Name" DataValueField="Id" AppendDataBoundItems="true" Filter="Contains" AllowCustomText="true">
                        <ItemTemplate>
                            <%# Eval("Name") %><span style="display: none;"><%# Eval("PinYin") %></span>
                        </ItemTemplate>
                    </telerik:RadComboBox>
                            &nbsp;&nbsp;&nbsp;&nbsp;
                    <input type="button" class="btn btn-tumblr" id="query" runat="server" value="查询" onserverclick="query_ServerClick" />
                        </div>
                    </div>
                    <div class="row">&nbsp;</div>
                    <div class="row">
                        <%--<input type="hidden" id="___total" runat="server" value="@@@@@@" />--%>
                        <!-- Start Printing -->
                        <telerik:RadListView ID="view" runat="server" OnNeedDataSource="view_NeedDataSource" ItemPlaceholderID="holder" AllowPaging="true">
                            <LayoutTemplate>
                                <div class="col-md-12">
                                    <table class="storeTable text-center">
                                        <tr>
                                            <th>出库单</th>
                                            <th>借领日期</th>
                                            <th>待还日期</th>
                                            <th>品名</th>
                                            <th>类别</th>
                                            <th>单位</th>
                                            <th>类型</th>
                                            <th>出库数量</th>
                                            <th>待还数量</th>
                                            <th>单价(元)</th>
                                            <th>总价(元)</th>
                                            <th style='<%# (Depot.Featured(Models.DepotType.幼儿园) ? "display: ;": "display: none;") %>'>适用年龄段</th>
                                            <th>借领人</th>
                                            <th>操作人</th>
                                        </tr>
                                        <asp:PlaceHolder ID="holder" runat="server"></asp:PlaceHolder>
                                        <%--<tr>
                                            <td colspan="13">总数：<%# ___total.Value.Split(new[] { "@@@" }, StringSplitOptions.None)[0] %>&nbsp;&nbsp;&nbsp;&nbsp;总计：<%# ___total.Value.Split(new[] { "@@@" }, StringSplitOptions.None)[1] %></td>
                                        </tr>--%>
                                    </table>
                                </div>
                            </LayoutTemplate>
                            <ItemTemplate>
                                <tr>
                                    <td>
                                        <asp:HyperLink runat="server" ForeColor="#3E5A70" Target="_top" Text="出库单" NavigateUrl='<%# "../DepotQuery/UsePrint?DepotId={0}&UseId={1}".Formatted(Depot.Id, Eval("UseId")) %>'></asp:HyperLink></td>
                                    </td>
                                    <td><%# Eval("Time").ToDay() %></td>
                                    <td><%# (int)Eval("Period") == 0 ? "不限期" : ((DateTime)Eval("Time")).AddDays((int)Eval("Period")).ToDay() %></td>
                                    <td><%# Eval("Name") %></td>
                                    <td><%# Eval("CatalogName") %></td>
                                    <td><%# Eval("Unit") %></td>
                                    <td><%# ((Models.UseType)Eval("Type")).ToString() %></td>
                                    <td><%# Eval("Amount").ToAmount(Depot.Featured(Models.DepotType.小数数量库)) %></td>
                                    <td><%# ((Models.UseType)Eval("Type")) == Models.UseType.借用 ? ((decimal)Eval("Amount") - (decimal)Eval("ReturnedAmount")).ToAmount(Depot.Featured(Models.DepotType.小数数量库)) : "0" %></td>
                                    <td><%# decimal.Divide((decimal)Eval("Money"), (decimal)Eval("Amount")).ToMoney() %></td>
                                    <td><%# Eval("Money").ToMoney() %></td>
                                    <td style='<%# (Depot.Featured(Models.DepotType.幼儿园) ? "display: ;": "display: none;") %>'><%# Eval("Age") %></td>
                                    <td><%# Eval("UserName") %></td>
                                    <td><%# Eval("OperatorName") %></td>
                                </tr>
                            </ItemTemplate>
                            <EmptyDataTemplate>
                                <div class="col-md-12 text-center">
                                    <div class="btn btn-warning">暂无记录</div>
                                </div>
                            </EmptyDataTemplate>
                        </telerik:RadListView>
                        <telerik:RadListView ID="viewx" runat="server" OnNeedDataSource="viewx_NeedDataSource" ItemPlaceholderID="holder" AllowPaging="true">
                            <LayoutTemplate>
                                <div class="col-md-12">
                                    <table class="storeTable text-center">
                                        <tr>
                                            <th>出库单</th>
                                            <th>借领日期</th>
                                            <th>归还日期</th>
                                            <th>品名</th>
                                            <th>类别</th>
                                            <th>单位</th>
                                            <th>类型</th>
                                            <th>出库数量</th>
                                            <th>归还数量</th>
                                            <th>单价(元)</th>
                                            <th>总价(元)</th>
                                            <th style='<%# (Depot.Featured(Models.DepotType.幼儿园) ? "display: ;": "display: none;") %>'>适用年龄段</th>
                                            <th>归还人</th>
                                            <th>操作人</th>
                                        </tr>
                                        <asp:PlaceHolder ID="holder" runat="server"></asp:PlaceHolder>
                                        <%--<tr>
                                            <td colspan="9">总数：<%# ___total.Value.Split(new[] { "@@@" }, StringSplitOptions.None)[0] %>&nbsp;&nbsp;&nbsp;&nbsp;总计：<%# ___total.Value.Split(new[] { "@@@" }, StringSplitOptions.None)[1] %></td>
                                        </tr>--%>
                                    </table>
                                </div>
                            </LayoutTemplate>
                            <ItemTemplate>
                                <tr>
                                    <td>
                                        <asp:HyperLink runat="server" ForeColor="#3E5A70" Target="_top" Text="出库单" NavigateUrl='<%# "../DepotQuery/UsePrint?DepotId={0}&UseId={1}".Formatted(Depot.Id, Eval("UseId")) %>'></asp:HyperLink></td>
                                    </td>
                                    <td><%# GT((Guid)Eval("UseId")).ToDay() %></td>
                                    <td><%# Eval("Time").ToDay() %></td>
                                    <td><%# Eval("Name") %></td>
                                    <td><%# GC((Guid)Eval("CatalogId"), (int)Eval("Level")) %></td>
                                    <td><%# Eval("Unit") %></td>
                                    <td>归还</td>
                                    <td><%# Eval("Amount").ToAmount() %></td>
                                    <td><%# Eval("ReturnAmount").ToAmount(Depot.Featured(Models.DepotType.小数数量库)) %></td>
                                    <td><%# decimal.Divide((decimal)Eval("Money"), (decimal)Eval("Amount")).ToMoney() %></td>
                                    <td><%# (decimal.Divide((decimal)Eval("Money"), (decimal)Eval("Amount")) * (decimal)Eval("ReturnAmount")).ToMoney() %></td>
                                    <td style='<%# (Depot.Featured(Models.DepotType.幼儿园) ? "display: ;": "display: none;") %>'><%# Eval("Age") %></td>
                                    <td><%# Eval("UserName") %></td>
                                    <td><%# Eval("OperatorName") %></td>
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
                    <div class="row">
                        <div class="col-md-3">&nbsp;</div>
                        <div class="col-md-6 text-center">
                            <telerik:RadDataPager ID="pager" runat="server" PagedControlID="view" BackColor="Transparent" BorderStyle="None" RenderMode="Auto" PageSize="10" OnPageIndexChanged="pager_PageIndexChanged">
                                <Fields>
                                    <telerik:RadDataPagerButtonField FieldType="FirstPrev"></telerik:RadDataPagerButtonField>
                                    <telerik:RadDataPagerButtonField FieldType="Numeric"></telerik:RadDataPagerButtonField>
                                    <telerik:RadDataPagerButtonField FieldType="NextLast"></telerik:RadDataPagerButtonField>
                                </Fields>
                            </telerik:RadDataPager>
                            <telerik:RadDataPager ID="pagerx" runat="server" PagedControlID="viewx" BackColor="Transparent" BorderStyle="None" RenderMode="Auto" PageSize="10" OnPageIndexChanged="pagerx_PageIndexChanged">
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
