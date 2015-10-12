﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="OutDoingS.aspx.cs" Inherits="OutDoingS" %>

<%@ Register Src="~/Menu/Menu.ascx" TagPrefix="homory" TagName="Menu" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <meta http-equiv="x-ua-compatible" content="ie=edge, chrome=1" />
    <meta name="viewport" content="width=device-width, initial-scale=1, minimum-scale=1, maximum-scale=1" />
    <title>物资管理 - 填报废单</title>
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
        <telerik:RadCodeBlock ID="cb" runat="server">
            <script>
                function responsible_selecting(sender, args) {
                    var id = $("#<%= responsibleId.ClientID %>").val();
                    var name = $find("<%= responsible.ClientID %>").get_value();
                    var type = "InResponsible";
                    var tag = "报废人";
                    var w = window.radopen("../StorageCommon/UserSelect?StorageId=" + "<%= StorageId.ToString() %>" + "&Id=" + id + "&Name=" + encodeURIComponent(name) + "&Type=" + type + "&Tag=" + encodeURIComponent(tag), "w_keeper");
                    w.maximize();
                    return false;
                }
                function user_selected(id, name, type) {
                    if (type == "InResponsible") {
                        if (id) {
                            $("#<%= responsibleId.ClientID %>").val(id);
                            $find("<%= responsible.ClientID %>").set_value(name);
                        }
                    }
            }
            </script>
        </telerik:RadCodeBlock>
        <telerik:RadWindowManager ID="wm" runat="server" Modal="true" Behaviors="None" CenterIfModal="true" ShowContentDuringLoad="true" VisibleStatusbar="false" ReloadOnShow="true">
            <Windows>
                <telerik:RadWindow ID="w_responsible" runat="server"></telerik:RadWindow>
            </Windows>
        </telerik:RadWindowManager>
        <homory:Menu runat="server" ID="menu" />
        <telerik:RadAjaxPanel ID="ap" runat="server" CssClass="grid-container">

            <div class="grid-100 mobile-grid-100 grid-parent left">
                <h3>物品</h3>
            </div>
            <div class="grid-100 mobile-grid-100 grid-parent">
                <table class="table table-bordered" style="margin-top: 10px;" align="center">
                    <thead>
                        <tr>
                            <th>名称</th>
                            <th>分类</th>
                            <th>单位</th>
                            <th>规格</th>
                            <th>固定资产编号</th>
                            <th>库存</th>

                        </tr>
                    </thead>
                    <tbody>
                        <tr>
                            <td align="left">
                                <input id="object_id" runat="server" type="hidden" />

                                <asp:Label ID="object_name" runat="server"></asp:Label>/         
                                <asp:Image AlternateText="固" ID="object_fixed" runat="server" />
                                <asp:Image AlternateText="易" ID="object_consumable" runat="server" />
                            </td>
                            <td align="left">
                                <asp:Label ID="object_catalog" runat="server"></asp:Label>
                            </td>
                            <td align="left">
                                <asp:Label ID="object_unit" runat="server"></asp:Label>
                            </td>
                            <td align="left">
                                <asp:Label ID="object_specification" runat="server"></asp:Label>
                            </td>

                            <td align="left" id="fixedArea" runat="server">
                                <asp:Label ID="object_fixed_serial" runat="server"></asp:Label>

                            </td>
                            <td align="left">
                                <asp:Label ID="object_inAmount" runat="server"></asp:Label>
                                <asp:Image AlternateText="低" ID="object_low" runat="server" />
                                <asp:Image AlternateText="超" ID="object_high" runat="server" />

                            </td>
                        </tr>
                    </tbody>
                </table>
            </div>
             <div class="grid-100 mobile-grid-100 grid-parent left">
                <h3>报废单</h3>
            </div>
            <div class="grid-100 mobile-grid-100 grid-parent">
                <div>
                    报废方式：<telerik:RadButton ID="act_r" runat="server" Checked="true" ButtonType="ToggleButton" ToggleType="CheckBox" Text="随机报废" OnCheckedChanged="act_r_CheckedChanged"></telerik:RadButton>
                    <telerik:RadButton ID="act_s" runat="server" Checked="false" ButtonType="ToggleButton" ToggleType="CheckBox" Text="特定报废" OnCheckedChanged="act_s_CheckedChanged"></telerik:RadButton>
                </div>
                <div id="r" runat="server" style="margin:5px 0px;">
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;数量：&nbsp;&nbsp;<telerik:RadNumericTextBox ID="in_amount" runat="server" NumberFormat-DecimalDigits="2" DataType="System.Decimal" AllowOutOfRangeAutoCorrect="true"></telerik:RadNumericTextBox>
                </div>
                <div id="s" runat="server" visible="false" style="margin:5px 0px;">
                    <div style="margin:5px 0px;">
                        选定编号：
                        <telerik:RadNumericTextBox ID="s_from" runat="server" NumberFormat-DecimalDigits="0" DataType="System.Int32" AllowOutOfRangeAutoCorrect="true"></telerik:RadNumericTextBox>
                        到
                        <telerik:RadNumericTextBox ID="s_to" runat="server" NumberFormat-DecimalDigits="0" DataType="System.Int32" AllowOutOfRangeAutoCorrect="true"></telerik:RadNumericTextBox>
                    </div>
                    <div style="margin:5px 0px;">
                        <telerik:RadButton ID="s_ok" runat="server" Text="选择" OnClick="s_ok_Click"></telerik:RadButton>
                        <telerik:RadButton ID="s_re" runat="server" Text="反选" OnClick="s_re_Click"></telerik:RadButton>
                        <telerik:RadButton ID="s_cl" runat="server" Text="清除" OnClick="s_cl_Click"></telerik:RadButton>
                    </div>
                    <div>
                        <telerik:RadListView ID="view" runat="server" OnNeedDataSource="view_NeedDataSource">
                            <ItemTemplate>
                                <telerik:RadButton ID="c" runat="server" ButtonType="ToggleButton" ToggleType="CheckBox" Text='<%# Container.DataItem %>'></telerik:RadButton>
                            </ItemTemplate>
                        </telerik:RadListView>
                    </div>
                </div>
                <div style="margin:5px 0px;">
                 &nbsp;&nbsp;报废人：&nbsp;<asp:Label ID="responsible" runat="server"></asp:Label>
                    <asp:ImageButton ID="keeper_del" runat="server" AlternateText="×" OnClick="keeper_del_Click" Visible="false" />
                    <input id="responsibleId" runat="server" type="hidden" />
                    <telerik:RadSearchBox ID="keeper_source" runat="server" EmptyMessage="报废人筛选" MaxResultCount="10" LocalizationPath="~/Language" DataTextField="Name" DataValueField="Id" ShowSearchButton="false" OnDataSourceSelect="keeper_source_DataSourceSelect" ShowLoadingIcon="false" OnSearch="keeper_source_Search"></telerik:RadSearchBox>
                </div>
                <div style="margin:5px 0px;">
                    报废类型：<telerik:RadComboBox   ID="out_type" runat="server" AllowCustomText="false" DataTextField="Name" DataValueField="Id"></telerik:RadComboBox>
                </div>
                <div style="margin:5px 0px;">
                    报废原因：<telerik:RadTextBox ID="out_reason" runat="server"></telerik:RadTextBox>
                </div>
                <div>
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;备注：&nbsp;&nbsp;<telerik:RadTextBox ID="in_note" runat="server"></telerik:RadTextBox>
                </div>
                <div style="margin:5px 0px;">
                    <telerik:RadButton ID="in_confirm" runat="server" Checked="false" Text="确认报废单信息填写正确" AutoPostBack="true" ButtonType="ToggleButton" ToggleType="CheckBox">
                    </telerik:RadButton>
                </div>
            </div>
            <div class="grid-100 mobile-grid-100 grid-parent" style="margin:5px 0px;">
                <asp:ImageButton AlternateText="报废" ID="in" runat="server" OnClick="in_Click"  class="btn btn-xm btn-default"/>
            </div>
        </telerik:RadAjaxPanel>
    </form>
    <script src="../Assets/javascripts/jquery.js"></script>
    <script src="../Assets/javascripts/common.js"></script>
</body>
</html>
