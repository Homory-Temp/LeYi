<%@ Page Language="C#" AutoEventWireup="true" CodeFile="LendDoneM.aspx.cs" Inherits="LendDoneM" %>

<%@ Register Src="~/Menu/Menu.ascx" TagPrefix="homory" TagName="Menu" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <meta http-equiv="x-ua-compatible" content="ie=edge, chrome=1" />
    <meta name="viewport" content="width=device-width, initial-scale=1, minimum-scale=1, maximum-scale=1" />
    <title>物资管理 - 借用完成</title>
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
                <h3>借用记录</h3>
            </div>
            <div class="grid-100 mobile-grid-100 grid-parent">
                <table class="table table-bordered" style="margin-top: 10px;" align="center">
                    <thead>
                        <tr>
                            <th>借用人</th>
                            <th>借用数量</th>
                            <th>借用合计</th>
                          
                            <th>日期</th>
                            <th>备注</th>

                        </tr>
                    </thead>
                    <tbody>
                        <tr>
                            <td align="left"><asp:Label ID="consumer" runat="server"></asp:Label>
                            </td>
                            <td align="left"><asp:Label ID="consume_totalAmount" runat="server"></asp:Label>
                            </td>
                            <td align="left"><asp:Label ID="consume_totalMoney" runat="server"></asp:Label>
                            </td>
                           
                            <td align="left"><asp:Label ID="day" runat="server"></asp:Label>
                            </td>
                            <td align="left"><asp:Label ID="note" runat="server"></asp:Label>
                            </td>
                        </tr>
                    </tbody>
                </table>
            <div class="grid-100 mobile-grid-100 grid-parent">
                <asp:ImageButton AlternateText="借用同类物品" ID="in_obj" runat="server" OnClick="in_obj_Click" />
                <asp:ImageButton AlternateText="借用他类物品" ID="in_other" runat="server" OnClick="in_other_Click" />
                <asp:ImageButton AlternateText="返回首页" ID="in_back" runat="server" OnClick="in_back_Click" />
            </div>
        </telerik:RadAjaxPanel>
    </form>
    <script src="../Assets/javascripts/jquery.js"></script>
    <script src="../Assets/javascripts/common.js"></script>
</body>
</html>
