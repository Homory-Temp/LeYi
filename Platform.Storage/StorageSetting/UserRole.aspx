<%@ Page Language="C#" AutoEventWireup="true" CodeFile="UserRole.aspx.cs" Inherits="UserRole" %>

<%@ Register Src="~/Menu/Menu.ascx" TagPrefix="homory" TagName="Menu" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <meta http-equiv="x-ua-compatible" content="ie=edge, chrome=1" />
    <meta name="viewport" content="width=device-width, initial-scale=1, minimum-scale=1, maximum-scale=1" />
    <title>物资管理 - 用户角色</title>
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
            <div class="grid-100 mobile-grid-100 grid-parent">
                <telerik:RadListView ID="list" runat="server" DataKeyNames="Id" OnNeedDataSource="list_NeedDataSource" OnItemDataBound="list_ItemDataBound"  ItemPlaceholderID="holder">


                       <LayoutTemplate>

                    <table style="margin-left: 10px; margin-top: 10px; width: 80%;" align="center">
                        <thead>
                         <tr>
                                <td colspan="4" align="left" height="20"></td>
                            </tr>
                            <tr>
                                <td colspan="4" align="left"><strong class="am-text-primary am-text-lg">用户角色</strong></td>
                            </tr>

                              <tr>
                                <td colspan="4" align="left" height="20"></td>
                            </tr>
                            <tr>
                                <th width="20%" style="background:#EFEFEF;height:40px;text-align:center;">角色</th>
                                <th width="60%" style="background:#EFEFEF;height:40px;text-align:left;">人员</th>
                                <th width="30%" style="background:#EFEFEF;height:40px;text-align:left;">操作</th>
 


                            </tr>
                        </thead>
                        <tbody >
                            <asp:PlaceHolder ID="holder" runat="server"></asp:PlaceHolder>
                        </tbody>
                    </table>

                </LayoutTemplate>
                    <ItemTemplate>
                       <tr style="border-bottom:1px dashed #CDCDCD;height:40px;text-align:center;">
                        <td align="left"><%# Eval("Name") %>
                        </td>
                        <td align="left">
                                <asp:Repeater ID="r" runat="server">
                                    <ItemTemplate>
                                        <%# Eval("RealName") %>
                                        <asp:ImageButton ID="del" runat="server" AlternateText="删" CommandArgument='<%# Eval("Id") %>' OnClick="del_Click" OnLoad="del_Load" />
                                    </ItemTemplate>
                                </asp:Repeater>
                            </td>
                        <td align="left">                           
                                <telerik:RadSearchBox ID="people" runat="server" EmptyMessage="筛选并添加" MaxResultCount="10" LocalizationPath="~/Language" DataTextField="Name" DataValueField="Id" ShowSearchButton="false" OnDataSourceSelect="people_DataSourceSelect" ShowLoadingIcon="false" OnSearch="people_Search" OnLoad="people_Load"></telerik:RadSearchBox>
                            </td>
                        </tr>
                    </ItemTemplate>
                </telerik:RadListView>
            </div>
        </telerik:RadAjaxPanel>
    </form>
    <script src="../Assets/javascripts/jquery.js"></script>
    <script src="../Assets/javascripts/common.js"></script>
</body>
</html>
