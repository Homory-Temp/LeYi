<%@ Page Language="C#" AutoEventWireup="true" CodeFile="QueryPersonal.aspx.cs" Inherits="QueryPersonal" %>

<%@ Register Src="~/Menu/Menu.ascx" TagPrefix="homory" TagName="Menu" %>

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
    <!--[if (lt IE 9) & (!IEMobile)]>
        <link rel="stylesheet" href="../Assets/stylesheets/ie.css" />
    <![endif]-->
    <link href="../Assets/stylesheets/common.css" rel="stylesheet" />
    <telerik:RadCodeBlock runat="server">
        <script type="text/javascript">

            function OpenClick(id) {

                var openUrl = "../StorageObject/ObjectWindow?Id=" + id + "&PlaceVisible=true";

                var iWidth = 800; //弹出窗口的宽度;

                var iHeight = 700; //弹出窗口的高度;

                var iTop = (window.screen.availHeight - 30 - iHeight) / 2; //获得窗口的垂直位置;

                var iLeft = (window.screen.availWidth - 10 - iWidth) / 2; //获得窗口的水平位置;

                window.open(openUrl, "", "height=" + iHeight + ", width=" + iWidth + ", top=" + iTop + ", left=" + iLeft);

            }

        </script>
    </telerik:RadCodeBlock>
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
        <telerik:RadAjaxPanel ID="ap" runat="server" CssClass="grid-container" Visible="false">
            <div class="grid-100 mobile-grid-100 grid-parent">
                <div style="margin-top: 20px;">
                    借用人：<asp:Label ID="responsibleX" runat="server"></asp:Label>
                    <input id="responsibleIdX" runat="server" type="hidden" />
                    <telerik:RadSearchBox ID="keeper_sourceX" runat="server" EmptyMessage="借用人筛选" MaxResultCount="10" LocalizationPath="~/Language" DataTextField="Name" DataValueField="Id" ShowSearchButton="false" OnDataSourceSelect="keeper_sourceX_DataSourceSelect" ShowLoadingIcon="false" OnSearch="keeper_sourceX_Search"></telerik:RadSearchBox>
                </div>
            </div>
        </telerik:RadAjaxPanel>
        <telerik:RadAjaxPanel ID="apxx" runat="server" CssClass="grid-container" OnAjaxRequest="apxx_AjaxRequest">
            <div class="grid-100 mobile-grid-100 grid-parent">
                        <div class="grid-100 mobile-grid-100 grid-parent left" style="text-align:center">
                            <h3 id="name" runat="server"></h3>
                        </div>
                <telerik:RadListView ID="viewX" runat="server" OnNeedDataSource="viewX_NeedDataSource" DataKeyNames="借用标识" OnItemDataBound="viewX_ItemDataBound" ItemPlaceholderID="holder">


                    <LayoutTemplate>

                        <div class="grid-100 mobile-grid-100 grid-parent">
                            <table class="table table-bordered" style="margin-left: 10px; margin-top: 10px;" align="center">
                                <thead>
                                    <tr>
                                        <th width="10%">借用日期</th>
                                        <th width="10%">仓库</th>
                                        <th width="10%">物资名称</th>
                                        <th width="10%">物资编码</th>
                                        <th width="10%">借用数量</th>
                                        <th width="10%">待归还数量</th>
              <%--                          <th width="5%">单价</th>
                                        <th width="5%">合计</th>--%>
                                        <th width="5%">借用人</th>
                                        <th width="10%">备注</th>
                                        <th width="10%">编号/编码</th>


                                    </tr>
                                </thead>

                                <tbody>
                                    <asp:PlaceHolder ID="holder" runat="server"></asp:PlaceHolder>
                                </tbody>
                                </table>
                    </LayoutTemplate>
                    <ItemTemplate>

                        <tr>
                            <td align="left"><%# Eval("日期") %></td>
                            <td align="left"><%# db.Value.StorageGet((Guid)Eval("仓库标识")).Name %></td>
                            <td align="left"  onclick='OpenClick("<%#Eval("物品标识") %>");'><%# Eval("物品名称") %></td>
                            <td align="left"><%# GetAutoId(Container.DataItem as Models.查询_借用单) %></td>
                            <td align="left"><%# Eval("数量") %></td>
                            <td align="left"><%# Eval("待归还数") %></td>
<%--                            <td align="left"><%# Eval("单价").Money() %></td>
                            <td align="left"><%# Eval("合计").Money() %></td>--%>
                            <td align="left"><%# Eval("借用人") %></td>
                            <td align="left"><%# Eval("备注") %></td>
                            <td align="left">

                                <asp:Repeater ID="s" runat="server">
                                    <ItemTemplate>
                                        <%# Eval("Ordinal") %>/<%# GetAutoId(Container.DataItem as Models.StorageLendSingle) %>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </td>
                        </tr>

                    </ItemTemplate>
                  </telerik:RadListView>
            </div>
        </telerik:RadAjaxPanel>
    </form>
    <style>
        html .RadSearchBox {
            display: -moz-inline-stack;
            display: inline-block;
            *display: inline:;
            *zoom: 1:;
            width: 30%;
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
