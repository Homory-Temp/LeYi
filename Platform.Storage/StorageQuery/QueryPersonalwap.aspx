<%@ Page Language="C#" AutoEventWireup="true" CodeFile="QueryPersonal.aspx.cs" Inherits="QueryPersonal" %>

<%@ Register Src="~/Menu/MenuMobile.ascx" TagPrefix="homory" TagName="Menu" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <meta http-equiv="x-ua-compatible" content="ie=edge, chrome=1" />
    <meta name="viewport" content="width=device-width, initial-scale=1, minimum-scale=1, maximum-scale=1" />
    <title>物资管理 - 个人查询</title>
    <link href="../Assets/stylesheets/amazeui.min.css" rel="stylesheet">
    <link href="../Assets/stylesheets/admin.css" rel="stylesheet">
    <link href="../Assets/stylesheets/bootstrap.min.css" rel="stylesheet">
    <link href="../Assets/stylesheets/bootstrap-theme.min.css" rel="stylesheet">
    <!--[if lt IE 9]>
        <script src="../Assets/javascripts/html5.js"></script>
    <![endif]-->
    <!--[if (gt IE 8) | (IEMobile)]><!-->
    <link rel="stylesheet" href="../Assets/stylesheets/unsemantic-grid-responsive.css" />
    <!--<![endif]-->

    <link href="../Assets/stylesheets/common.css" rel="stylesheet" />
</head>
<body>
    <form id="form" runat="server">
        <telerik:RadScriptManager ID="sm" runat="server"></telerik:RadScriptManager>
        <telerik:RadCodeBlock ID="cb" runat="server">
            <script>
                function scanDo() {
                    $find("<%= apxx.ClientID %>").ajaxRequest("Do");
                }
                function scanDoX() {
                    $find("<%= ap.ClientID %>").ajaxRequest("Do");
                }
            </script>
        </telerik:RadCodeBlock>
        <homory:Menu runat="server" ID="menu" />

        <telerik:RadAjaxPanel ID="ap" runat="server" CssClass="grid-container">
            <div class="grid-100 mobile-grid-100 grid-parent left" style="text-align: center; margin: 80px 0px 10px 0px;">
                            <h3 id="name" runat="server"></h3>
            </div>
            <div class="grid-100 mobile-grid-100 grid-parent" style="max-width: 640px;">

                <table align="center" style="display: none;">
                    <tr>
                        <td>借用人：</td>
                        <td width="100">
                            <asp:Label ID="responsibleX" runat="server" Text="未选择"></asp:Label>

                            <input id="responsibleIdX" runat="server" type="hidden" /></td>
                        <td>请选择：</td>
                        <td>
                            <telerik:RadSearchBox ID="keeper_sourceX" runat="server" EmptyMessage="借用人筛选" MaxResultCount="10" LocalizationPath="~/Language" DataTextField="Name" DataValueField="Id" ShowSearchButton="false" OnDataSourceSelect="keeper_sourceX_DataSourceSelect" ShowLoadingIcon="false" OnSearch="keeper_sourceX_Search"></telerik:RadSearchBox>
                        </td>
                        <td width="100">&nbsp;</td>
                    </tr>
                </table>





            </div>
        </telerik:RadAjaxPanel>
        <div class="grid-100 mobile-grid-100 grid-parent" style="max-width: 640px;">
            <telerik:RadAjaxPanel ID="apxx" runat="server" CssClass="grid-container" OnAjaxRequest="apxx_AjaxRequest">
                <div class="grid-100 mobile-grid-100 grid-parent">

                    <table class="table table-bordered" style="margin-top: 10px; max-width: 640px;" align="center">
                        <thead>
                            <tr>
                                <th width="10%">借用日期</th>

                                <th width="10%">物资名称</th>
                                <th width="10%">物资编码</th>
                                <th width="10%">借用数量</th>
                                <th width="10%">待归还数量</th>
                                <th width="10%">借用人</th>



                            </tr>
                        </thead>

                        <tbody>

                            <telerik:RadListView ID="viewX" runat="server" OnNeedDataSource="viewX_NeedDataSource" DataKeyNames="借用标识" OnItemDataBound="viewX_ItemDataBound">
                                <ItemTemplate>
                                    <tr>
                                        <td align="left"><%# Eval("日期") %></td>

                                        <td align="left" onclick='OpenClick("<%#Eval("物品标识") %>");'><%# Eval("物品名称") %></td>
                                        <td align="left"><%# GetAutoId(Container.DataItem as Models.查询_借用单) %></td>
                                        <td align="left"><%# Eval("数量") %></td>
                                        <td align="left"><%# Eval("待归还数") %></td>
                                        <%--                            <td align="left"><%# Eval("单价").Money() %></td>
                            <td align="left"><%# Eval("合计").Money() %></td>--%>
                                        <td align="left"><%# Eval("借用人") %></td>


                                    </tr>
                                </ItemTemplate>
                            </telerik:RadListView>
                        </tbody>
                    </table>
                </div>
            </telerik:RadAjaxPanel>
        </div>
    </form>
    <style>
        html .RadSearchBox {
            display: -moz-inline-stack;
            display: inline-block;
            *display: inline:;
            *zoom: 1:;
            width: 90%;
            text-align: left;
            line-height: 30px;
            height: 30px;
            white-space: nowrap;
            vertical-align: middle;
        }
    </style>
    <script src="../Assets/javascripts/jquery.js"></script>
    <script src="../Assets/javascripts/common.js"></script>
</body>
</html>
