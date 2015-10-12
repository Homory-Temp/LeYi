<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Consume.aspx.cs" Inherits="Consume" %>

<%@ Register Src="~/Menu/Menu.ascx" TagPrefix="homory" TagName="Menu" %>
<%@ Register Src="~/StorageObject/ObjectImage.ascx" TagPrefix="homory" TagName="ObjectImage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <meta http-equiv="x-ua-compatible" content="ie=edge, chrome=1" />
    <meta name="viewport" content="width=device-width, initial-scale=1, minimum-scale=1, maximum-scale=1" />
    <title>物资管理 - 领用准备</title>
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
    <script>
    </script>
</head>
<body>
    <form id="form" runat="server">
        <telerik:RadScriptManager ID="sm" runat="server"></telerik:RadScriptManager>
        <homory:Menu runat="server" ID="menu" />
        <telerik:RadAjaxPanel ID="ap" runat="server" CssClass="grid-container">

            <div class="grid-100 mobile-grid-100 grid-parent">
                <div class="grid-100 mobile-grid-100 grid-parent">
                    <div>
                        物品：<telerik:RadSearchBox ID="obj_search" runat="server" DataTextField="Name" DataValueField="Id" ShowSearchButton="false" OnDataSourceSelect="obj_search_DataSourceSelect" ShowLoadingIcon="false" OnSearch="obj_search_Search"></telerik:RadSearchBox>
                        <asp:ImageButton AlternateText="新增" ID="new_obj" runat="server" OnClick="new_obj_Click" />
                    </div>
                    <table class="table table-bordered" style="margin-top: 10px;" align="center">
                        <thead>
                            <tr>
                                <th >名称</th>
                                <th>分类</th>
                                <th>单位</th>
                                <th>规格</th>

                                <th>库存</th>
                                <th>图片</th>
                            </tr>
                        </thead>
                        <tbody>

                            <telerik:RadListView ID="obj_view" runat="server" OnNeedDataSource="obj_view_NeedDataSource">
                                <ItemTemplate>
                                    <tr>
                                        <td align="left">

                                            <telerik:RadButton ID="obj_set" AutoPostBack="true" ButtonType="ToggleButton" ToggleType="Radio" runat="server" Checked='<%# obj_id.Value == Eval("Id").ToString() %>' Text='<%# Eval("Name") %>' Value='<%# Eval("Id") %>' OnClick="obj_set_Click"></telerik:RadButton> / 
                                                    <asp:Image AlternateText="固" ID="fixed" Visible='<%# (bool)Eval("Fixed") %>' runat="server" />
                                                    <asp:Image AlternateText="易" ID="consumable" Visible='<%# (bool)Eval("Consumable") %>' runat="server" />
                                            <td align="left"><%# (Container.DataItem as Models.StorageObject).GeneratePath() %>
                                                
                                                </td>
                                                <td align="left"><%# Eval("Unit") %>   </td>
                                                <td align="left">
                                                    <%# Eval("Specification") %>
                                                </td>
                                                <td align="left"><%# Eval("InAmount") %>
                                                    <asp:Image AlternateText="低" Visible='<%# (decimal)Eval("Low") > 0 && (decimal)Eval("Low") > (decimal)Eval("InAmount") %>' ID="low" runat="server" />
                                                    <asp:Image AlternateText="超" Visible='<%# (decimal)Eval("High") > 0 && (decimal)Eval("High") < (decimal)Eval("InAmount") %>' ID="high" runat="server" />
                                                </td>
                                                <td align="left">
                                                    <homory:ObjectImage ID="ObjectImage" runat="server" ImageJson='<%# Eval("Image") %>' />
                                                </td>
                                            </tr>
                                </ItemTemplate>
                            </telerik:RadListView>
                            <input id="obj_id" runat="server" type="hidden" />
                        </tbody>
                    </table>
                </div>

            </div>
            <div class="grid-100 mobile-grid-100 grid-parent">
                <asp:ImageButton AlternateText="领用" ID="in" runat="server" Visible="false" OnClick="in_Click"  class="btn btn-xm btn-default"/>
            </div>
        </telerik:RadAjaxPanel>
    </form>
    <script src="../Assets/javascripts/jquery.js"></script>
    <script src="../Assets/javascripts/common.js"></script>
</body>
</html>
