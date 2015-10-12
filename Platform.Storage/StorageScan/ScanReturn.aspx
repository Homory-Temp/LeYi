<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ScanReturn.aspx.cs" Inherits="ScanReturn" %>

<%@ Register Src="~/Menu/Menu.ascx" TagPrefix="homory" TagName="Menu" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <meta http-equiv="x-ua-compatible" content="ie=edge, chrome=1" />
    <meta name="viewport" content="width=device-width, initial-scale=1, minimum-scale=1, maximum-scale=1" />
    <title>物资管理 - 物品归还</title>
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
        <telerik:RadCodeBlock ID="cb" runat="server">
            <script>
                function scanDoX() {
                    $find("<%= ap.ClientID %>").ajaxRequest("Do");
                }
            </script>
        </telerik:RadCodeBlock>
        <homory:Menu runat="server" ID="menu" />
        <telerik:RadAjaxPanel ID="ap" runat="server" CssClass="grid-container" OnAjaxRequest="ap_AjaxRequest">
            <div class="grid-100 mobile-grid-100 grid-parent">
                <table style="margin-left: 10px; margin-top: 10px;" align="center">

                    <tr>
                        <td width="100" align="right">请扫描条码：
                        </td>
                        <td width="300">
                            <telerik:RadTextBox ID="code" runat="server" MaxLength="12" Width="300"></telerik:RadTextBox>
                        </td>
                        <td>
                            <asp:ImageButton ID="add" runat="server" AlternateText="查询" OnClick="add_Click" />
                        </td>

                    </tr>
                    <tr>
                        <td colspan="2">（本页面仅提供（准）固定资产归还，其他请使用高级模式。）</td>
                        <td>
                            <asp:ImageButton ID="adv" runat="server" AlternateText="高级模式" OnClick="adv_Click" class="btn btn-xs btn-default" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3" height="10"></td>
                    </tr>





                </table>

            </div>
            <div class="grid-100 mobile-grid-100 grid-parent">
                <telerik:RadListView ID="viewD" runat="server" OnNeedDataSource="viewD_NeedDataSource" DataKeyNames="单借标识">
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
                                        <th width="10%">借用人</th>
                                        <th width="10%">编号</th>
                                        <th width="5%">数量</th>
                                        <th width="20%">备注</th>

                                    </tr>
                                </thead>
                                <tbody>
                                    <tr>
                                        <tr>
                                            <td align="left"><%# db.Value.StorageObjectGetOne((Container.DataItem as Models.查询_借还流).物品标识).Name %></td>
                                            <td><%# db.Value.StorageObjectGetOne((Container.DataItem as Models.查询_借还流).物品标识).GeneratePath() %>
                                            </td>
                                            <td><%# db.Value.StorageObjectGetOne((Container.DataItem as Models.查询_借还流).物品标识).Unit %>
                                            </td>
                                            <td><%# db.Value.StorageObjectGetOne((Container.DataItem as Models.查询_借还流).物品标识).Specification %>
                                            </td>
                                            <td><%# db.Value.UserName((Container.DataItem as Models.查询_借还流).人员标识) %></td>
                                            <td><%# Eval("编号") %>
                                            </td>
                                            <td>
                                                <telerik:RadNumericTextBox ID="amount" runat="server" Value="1" NumberFormat-DecimalDigits="2" MaxValue='<%# 1 %>' AllowOutOfRangeAutoCorrect="true"></telerik:RadNumericTextBox>
                                            </td>
                                            <td>
                                                <telerik:RadTextBox ID="note" runat="server"></telerik:RadTextBox>
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
                    <telerik:RadButton ID="in_confirm" runat="server" Checked="true" Visible="false" Text="确认归还信息填写正确" AutoPostBack="true" ButtonType="ToggleButton" ToggleType="CheckBox">
                    </telerik:RadButton>
                    <div style="margin-top: 10px;">
                        <asp:ImageButton AlternateText="归还" ID="out" runat="server" OnClick="out_Click" class="btn btn-xm btn-default" />
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
            width: 300px;
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
