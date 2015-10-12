<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ScanUse.aspx.cs" Inherits="ScanUse" %>

<%@ Register Src="~/Menu/Menu.ascx" TagPrefix="homory" TagName="Menu" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <meta http-equiv="x-ua-compatible" content="ie=edge, chrome=1" />
    <meta name="viewport" content="width=device-width, initial-scale=1, minimum-scale=1, maximum-scale=1" />
    <title>物资管理 - 领用借用</title>
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
    <style>
        html .table {
            width: 80%;
            margin-bottom: 20px;
            max-width: 90%;
        }
    </style>
</head>
<body>
    <form id="form" runat="server" defaultbutton="add">
        <telerik:RadScriptManager ID="sm" runat="server"></telerik:RadScriptManager>
        <telerik:RadCodeBlock ID="cb" runat="server">
            <script>
                function scanDo() {
                    $find("<%= ap.ClientID %>").ajaxRequest("Do");
                }
            </script>
        </telerik:RadCodeBlock>
        <homory:Menu runat="server" ID="menu" />
        <telerik:RadAjaxPanel ID="ap" runat="server" CssClass="grid-container" OnAjaxRequest="ap_AjaxRequest">
            <div class="grid-100 mobile-grid-100 grid-parent">
                <table align="center">
                    <tr>
                        <td>领用/借用人：</td>
                        <td width="100">
                            <asp:Label ID="responsible" runat="server" Text="未选择"></asp:Label>
                            <asp:ImageButton ID="keeper_del" runat="server" AlternateText="×" OnClick="keeper_del_Click" Visible="false" />
                            <input id="responsibleId" runat="server" type="hidden" /></td>
                        <td>请选择：</td>
                        <td>
                            <telerik:RadSearchBox ID="keeper_source" runat="server" MaxResultCount="10" LocalizationPath="~/Language" DataTextField="Name" DataValueField="Id" ShowSearchButton="false" OnDataSourceSelect="keeper_source_DataSourceSelect" ShowLoadingIcon="false" OnSearch="keeper_source_Search" Width="180"></telerik:RadSearchBox>
                        </td>
                           <td width="100">&nbsp;</td>
                    </tr>
                </table>

            </div>
            <div class="grid-100 mobile-grid-100 grid-parent" style="margin-top: 10px;">
                <table align="center">
                    <tr>
                        <td>&nbsp;请扫描条码：</td>
                        <td>
                            <telerik:RadTextBox ID="code" runat="server" MaxLength="12" EmptyMessage="请扫描或输入条码"  Width="300"></telerik:RadTextBox></td>
                        <td>
                            <asp:ImageButton ID="add" runat="server" AlternateText="查询" OnClick="add_Click" />
                        </td>
                    </tr>
                </table>
            </div>
            <div class="grid-100 mobile-grid-100 grid-parent">
                <telerik:RadListView ID="viewW" runat="server" OnNeedDataSource="viewW_NeedDataSource" DataKeyNames="Id">
                    <ItemTemplate>

                        <input id="object_id" runat="server" type="hidden" />
                        <table class="table table-bordered" style="margin-left: 10px; margin-top: 10px;" align="center">
                            <thead>
                                <tr>
                                    <th width="20%">名称</th>
                                    <th width="30%">分类</th>
                                    <th width="10%">单位</th>
                                    <th width="10%">规格</th>
                                    <th width="10%">库存</th>
                                    <th width="5%">数量</th>
                                    <th width="20%">备注</th>

                                </tr>
                            </thead>
                            <tbody>
                                <tr>
                                    <td align="left">
                                        <%# Eval("Name") %></td>
                                    <td align="left">
                                        <%# (Container.DataItem as Models.StorageObject).GeneratePath() %>
                                    </td>
                                    <td>
                                        <%# Eval("Unit") %></td>
                                    <td>
                                        <%# Eval("Specification") %>
                                    </td>
                                    <td>
                                        <%# Eval("InAmount") %>
                                    </td>
                                    <td>
                                        <telerik:RadNumericTextBox ID="amount" runat="server" Value='<%# WAmount((Guid)Eval("Id")) %>' NumberFormat-DecimalDigits="2" MaxValue='<%# (decimal)Eval("InAmount") %>' AllowOutOfRangeAutoCorrect="true"></telerik:RadNumericTextBox></td>
                                    <td>
                                        <telerik:RadTextBox ID="note" runat="server"></telerik:RadTextBox>
                                    </td>
                                </tr>
                            </tbody>
                        </table>

                    </ItemTemplate>
                </telerik:RadListView>
                <telerik:RadListView ID="viewD" runat="server" OnNeedDataSource="viewD_NeedDataSource" DataKeyNames="Id">
                    <ItemTemplate>
                        <div class="grid-100 mobile-grid-100 grid-parent">
                            <input id="object_id" runat="server" type="hidden" />
                            <table class="table table-bordered" style="margin-left: 10px; margin-top: 10px;" align="center">
                                <thead>
                                    <tr>
                                        <th width="20%">名称</th>
                                        <th width="30%">分类</th>
                                        <th width="10%">单位</th>
                                        <th width="10%">规格</th>
                                        <th width="10%">编号</th>

                                        <th width="5%">数量</th>
                                        <th width="10%">存放地</th>
                                        <th width="20%">备注</th>

                                    </tr>
                                </thead>
                                <tbody>
                                    <tr>
                                        <td align="left">
                                            <%# (Container.DataItem as Models.StorageInSingle).StorageObject.Name %></td>
                                        <td align="left">
                                            <%# (Container.DataItem as Models.StorageInSingle).StorageObject.GeneratePath() %>
                                        </td>
                                        <td align="left">
                                            <%# (Container.DataItem as Models.StorageInSingle).StorageObject.Unit %></td>
                                        <td align="left">
                                            <%# (Container.DataItem as Models.StorageInSingle).StorageObject.Specification %>
                                        </td>
                                        <td align="left">
                                            <%# Eval("InOrdinal") %></td>
                                        <td>
                                            <telerik:RadNumericTextBox ID="amount" runat="server" Value='<%# ((bool)Eval("In")) ? 1 : 0 %>' NumberFormat-DecimalDigits="2" MaxValue='<%# ((bool)Eval("In")) ? 1 : 0 %>' AllowOutOfRangeAutoCorrect="true" Width="30px"></telerik:RadNumericTextBox>
                                        </td>
                                        <td>
                                            <telerik:RadComboBox ID="place" runat="server" AutoPostBack="false" AllowCustomText="true" LocalizationPath="~/Language" DataTextField="Name" DataValueField="Name" Filter="Contains" MarkFirstMatch="true" AppendDataBoundItems="true" ShowToggleImage="false">
                                                <Items>
                                                    <telerik:RadComboBoxItem Text="" Value="" Selected="true" Width="30px" />
                                                </Items>
                                                <ItemTemplate>
                                                    <%# Eval("Name") %><span style="display: none;"><%# Eval("PinYin") %></span>
                                                </ItemTemplate>
                                            </telerik:RadComboBox>
                                        </td>
                                        <td>
                                            <telerik:RadTextBox ID="note" runat="server" Text='<%# ((bool)Eval("In")) ? "" : "已出库" %>' Width="60px"></telerik:RadTextBox>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </div>
                    </ItemTemplate>
                </telerik:RadListView>
            </div>
        </telerik:RadAjaxPanel>
        <telerik:RadAjaxPanel ID="apx" runat="server" CssClass="grid-container">
            <div class="grid-100 mobile-grid-100 grid-parent">
                <div>
                    <telerik:RadButton ID="in_confirm" runat="server" Checked="true" Visible="false" Text="确认领用/借用信息填写正确" AutoPostBack="true" ButtonType="ToggleButton" ToggleType="CheckBox">
                    </telerik:RadButton>
                    <div style="margin-top: 10px;">
                        <asp:ImageButton AlternateText="领用借用" ID="out" runat="server" OnClick="out_Click" class="btn btn-xm btn-default" />
                    </div>
                </div>
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
