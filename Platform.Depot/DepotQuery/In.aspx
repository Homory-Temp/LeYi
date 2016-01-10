<%@ Page Language="C#" AutoEventWireup="true" CodeFile="In.aspx.cs" Inherits="DepotQuery_In" %>

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
        <homory:SideBarSingle runat="server" ID="SideBarSingle" Crumb="日常查询 - 购置单查询" />
        <telerik:RadWindowManager runat="server" ID="Rwm" Skin="Metro">
            <Windows>
                <telerik:RadWindow ID="popup_win" runat="server" Title="请购流程" ReloadOnShow="True" Width="800" Height="450" VisibleStatusbar="false" Behaviors="Move,Close" Modal="True" CenterIfModal="True" Localization-Close="关闭">
                </telerik:RadWindow>
            </Windows>
        </telerik:RadWindowManager>
        <script>
            function pop(url) {
                window.radopen(url, "popup_win");
                return false;
            }
        </script>
        <telerik:RadAjaxPanel ID="ap" runat="server" CssClass="container-fluid" LoadingPanelID="loading">
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
                    <telerik:RadComboBox ID="combo" runat="server" MaxHeight="203" AutoPostBack="false" Width="120">
                        <Items>
                            <telerik:RadComboBoxItem Text="办结状态" Value="2" Selected="true" />
                            <telerik:RadComboBoxItem Text="待办" Value="0" />
                            <telerik:RadComboBoxItem Text="已办" Value="1" />
                        </Items>
                    </telerik:RadComboBox>
                    &nbsp;&nbsp;&nbsp;&nbsp;
                    <telerik:RadComboBox ID="source" runat="server" MaxHeight="203" AutoPostBack="false" Width="120" AppendDataBoundItems="true" DataTextField="Name" DataValueField="Name" Filter="Contains" AllowCustomText="true">
                        <ItemTemplate>
                            <%# Eval("Name") %><span style="display: none;"><%# Eval("PinYin") %></span>
                        </ItemTemplate>
                    </telerik:RadComboBox>
                    &nbsp;&nbsp;&nbsp;&nbsp;
                    <telerik:RadComboBox ID="usage" runat="server" MaxHeight="203" AutoPostBack="false" Width="120" AppendDataBoundItems="true" DataTextField="Name" DataValueField="Name" Filter="Contains" AllowCustomText="true">
                        <ItemTemplate>
                            <%# Eval("Name") %><span style="display: none;"><%# Eval("PinYin") %></span>
                        </ItemTemplate>
                    </telerik:RadComboBox>
                    &nbsp;&nbsp;&nbsp;&nbsp;
                    <telerik:RadComboBox ID="people" runat="server" MaxHeight="203" AutoPostBack="false" Width="120" DataTextField="Name" DataValueField="Id" AppendDataBoundItems="true" Filter="Contains" AllowCustomText="true">
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
                <%--<input type="hidden" id="___total" runat="server" />--%>
                <!-- Start Printing -->
                <telerik:RadListView ID="view" runat="server" OnNeedDataSource="view_NeedDataSource" ItemPlaceholderID="holder" AllowPaging="true">
                    <LayoutTemplate>
                        <div class="col-md-12">
                            <table class="storeTable text-center">
                                <tr>
                                    <th>购置单号</th>
                                    <th>发票编号</th>
                                    <th>购置日期</th>
                                    <th>购置来源</th>
                                    <th>使用对象</th>
                                    <th>应付金额</th>
                                    <th>实付金额</th>
                                    <th>保管人</th>
                                    <th>经手人</th>
                                    <th>清单简述</th>
                                    <th>操作人</th>
                                    <th>操作</th>
                                </tr>
                                <asp:PlaceHolder ID="holder" runat="server"></asp:PlaceHolder>
                                <%--<tr>
                                    <td colspan="12">总计：<%# ___total.Value %></td>
                                </tr>--%>
                            </table>
                        </div>
                    </LayoutTemplate>
                    <ItemTemplate>
                        <tr>
                            <td><asp:HyperLink runat="server" ForeColor="#3E5A70" Target="_top" Text='<%# Eval("购置单号") %>' NavigateUrl='<%# "../DepotQuery/InPrint?DepotId={0}&OrderId={1}".Formatted(Depot.Id, Eval("Id")) %>'></asp:HyperLink></td>
                            <td><%# Eval("发票编号") %></td>
                            <td><%# Eval("RecordTime").ToDay() %></td>
                            <td><%# Eval("购置来源") %></td>
                            <td><%# Eval("使用对象") %></td>
                            <td><%# Eval("应付金额").ToMoney() %></td>
                            <td><%# Eval("实付金额").ToMoney() %></td>
                            <td><%# Eval("保管人") %></td>
                            <td><%# Eval("经手人") %></td>
                            <td style="cursor: pointer;">
                                <span id="target_note" runat="server">清单简述</span>
                                <telerik:RadToolTip ID="tooltip" runat="server" TargetControlID="target_note" Skin="Metro">
                                    <%# Eval("清单简述") %>
                                </telerik:RadToolTip>
                            </td>
                            <td><%# Eval("操作人") %></td>
                            <td>
                                <input type="button" class="btn btn-tumblr" value="入库" id="in" runat="server" match='<%# Eval("Id") %>' visible='<%# !(bool)Eval("Done") %>' onserverclick="in_ServerClick" />
                                <input type="button" class="btn btn-tumblr" value="编辑" id="edit" runat="server" match='<%# Eval("Id") %>' onserverclick="edit_ServerClick" />
                                <input type="button" class="btn btn-tumblr" value="退货" id="back" runat="server" match='<%# Eval("Id") %>' visible='<%# !(bool)Eval("Done") %>' onserverclick="back_ServerClick" />
                                <input type="button" class="btn btn-tumblr" value="办结" id="done" runat="server" match='<%# Eval("Id") %>' visible='<%# !(bool)Eval("Done") %>' onserverclick="done_ServerClick" />
                                <input type="button" class="btn btn-tumblr" value="补办" id="redo" runat="server" match='<%# Eval("Id") %>' visible='<%# (bool)Eval("Done") && RightRoot %>' onserverclick="redo_ServerClick" />
                                <input type="button" class="btn btn-tumblr" value="打印" id="print" runat="server" match='<%# Eval("Id") %>' visible='<%# (bool)Eval("Done") %>' onserverclick="print_ServerClick" />
                                <input type="button" class="btn btn-tumblr" value="删除" id="del" runat="server" match='<%# Eval("Id") %>' onserverclick="del_ServerClick" visible='<%# CanDel((Guid)Eval("Id")) %>' />
                                <input type="button" class="btn btn-tumblr" value="流程" id="fs" runat="server" match='<%# Eval("Id") %>' onserverclick="fs_ServerClick" />
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
        </telerik:RadAjaxPanel>
    </form>
</body>
</html>
