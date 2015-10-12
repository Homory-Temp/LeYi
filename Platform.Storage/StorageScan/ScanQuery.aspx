<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ScanQuery.aspx.cs" Inherits="ScanQuery" %>

<%@ Register Src="~/Menu/MenuMobile.ascx" TagPrefix="homory" TagName="Menu" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <meta http-equiv="x-ua-compatible" content="ie=edge, chrome=1" />
    <meta name="viewport" content="width=device-width, initial-scale=1, minimum-scale=1, maximum-scale=1" />
    <title>物资管理 - 流通查询</title>
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
    <!--[if (lt IE 9) & (!IEMobile)]>
        <link rel="stylesheet" href="../Assets/stylesheets/ie.css" />
    <![endif]-->
    <link href="../Assets/stylesheets/common.css" rel="stylesheet" />
</head>
<body>
    <form id="form" runat="server">
        <telerik:RadScriptManager ID="sm" runat="server"></telerik:RadScriptManager>
        <homory:Menu runat="server" ID="menu" />

        <telerik:RadAjaxPanel ID="ap" runat="server" CssClass="grid-container">
            <div class="grid-100 mobile-grid-100 grid-parent" style="margin-top: 80px;">
                <telerik:RadTextBox ID="code" runat="server" MaxLength="12" Style="ime-mode: disabled; display: none;"></telerik:RadTextBox>
                <%--开始时间：<telerik:RadDatePicker ID="day_start" runat="server" LocalizationPath="~/Language" ShowPopupOnFocus="true">
                    <DatePopupButton Visible="false" />
                </telerik:RadDatePicker>
                结束时间：<telerik:RadDatePicker ID="day_end" runat="server" LocalizationPath="~/Language" ShowPopupOnFocus="true">
                    <DatePopupButton Visible="false" />
                </telerik:RadDatePicker>--%>
             <%--   <asp:ImageButton ID="query" runat="server" AlternateText="查询" OnClick="query_Click" />--%>
            </div>
     
           
            <div class="grid-100 mobile-grid-100 grid-parent">
                <table class="table table-bordered">
                     <tr>
                        <td colspan="3"> 物资名称：<asp:Label ID="name" runat="server"></asp:Label></td>
                        <td colspan="3">当前库存：<asp:Label ID="number" runat="server"></asp:Label></td>
                    </tr>
                    <tr>
                        <td>日期</td>
                        <td>人员</td>
                        <td>操作</td>
                        <td>数量</td>
                        <td>单位</td>
                        <td>库存</td>
                    </tr>
                    <telerik:RadListView ID="view" runat="server" OnNeedDataSource="view_NeedDataSource">
                        <ItemTemplate>
                            <tr>
                                <td><%# Eval("日期") %>
                                </td>
                                 <td><%#Eval("人员") %></td>
                                <td><%#Eval("类型") %></td>

                                <td><%#Eval("数量") %></td>

                                <td><%#Eval("单位") %></td>

                                <td><%# Eval("库存") %></td>

                            </tr>
                        </ItemTemplate>
                    </telerik:RadListView>
                </table>
            </div>
        </telerik:RadAjaxPanel>
    </form>
    <script src="../Assets/javascripts/jquery.js"></script>
    <script src="../Assets/javascripts/common.js"></script>
</body>
</html>
