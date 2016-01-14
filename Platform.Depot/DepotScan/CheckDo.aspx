<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CheckDo.aspx.cs" Inherits="DepotScan_CheckDo" %>

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
        function sg(sender, e) {
            if (e.get_keyCode() == 13) {
                $("#scanFlow").click();
            }
        }
    </script>
</head>
<body>
    <form id="form" runat="server">
        <homory:SideBarSingle runat="server" ID="SideBarSingle" Crumb="物资扫描 - 盘库" />
        <telerik:RadAjaxPanel ID="ap" runat="server" CssClass="container-fluid" LoadingPanelID="loading">
            <div class="row">
                <div class="col-md-12 text-center">
                    <input type="hidden" id="____vx" runat="server" />
                    <telerik:RadTextBox runat="server" ID="scan" Width="200" EmptyMessage="" ClientEvents-OnKeyPress="sg"></telerik:RadTextBox>
                </div>
                <div class="col-md-12 text-center" style="margin-top: 10px;">
                    <input type="button" class="btn btn-lg btn-tumblr" id="scanFlow" runat="server" value="盘点" onserverclick="scanFlow_ServerClick" />
                    <input type="button" class="btn btn-lg btn-tumblr" id="scanGo" runat="server" value="查询" onserverclick="scanGo_ServerClick" />
                </div>
            </div>
            <div class="row">
                <div class="col-md-12 text-center">
                    <div id="namex" runat="server" class="btn btn-info" style="width: 100%;">
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-md-12 text-center">
                    <div id="name" runat="server" class="btn btn-info" style="width: 100%;">
                    </div>
                </div>
            </div>
            <div class="row">
                <input type="hidden" runat="server" id="h" value="" />
                <telerik:RadListView ID="view" runat="server" OnNeedDataSource="view_NeedDataSource" ItemPlaceholderID="holder" PageSize="10" AllowPaging="true">
                    <LayoutTemplate>
                        <div class="col-md-12">
                            <table class="storeTablePrint text-center">
                                <tr>
                                    <th>物资名称</th>
                                    <th>物资条码</th>
                                    <th>存放地</th>
                                    <th>状态</th>
                                </tr>
                                <asp:PlaceHolder ID="holder" runat="server"></asp:PlaceHolder>
                            </table>
                        </div>
                    </LayoutTemplate>
                    <ItemTemplate>
                        <tr>
                            <td><%# Eval("Name") %>-<%# Eval("Ordinal") %></td>
                            <td><%# Eval("Code") %></td>
                            <td><%# Eval("Place") %></td>
                            <td><%# "已盘" %></td>
                        </tr>
                    </ItemTemplate>
                    <EmptyDataTemplate>
                    </EmptyDataTemplate>
                </telerik:RadListView>
            </div>
            <%--<div class="row">
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
            </div>--%>
        </telerik:RadAjaxPanel>
    </form>
</body>
</html>
