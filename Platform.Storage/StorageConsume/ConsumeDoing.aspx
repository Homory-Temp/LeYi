<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ConsumeDoing.aspx.cs" Inherits="ConsumeDoing" %>

<%@ Register Src="~/Menu/Menu.ascx" TagPrefix="homory" TagName="Menu" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <meta http-equiv="x-ua-compatible" content="ie=edge, chrome=1" />
    <meta name="viewport" content="width=device-width, initial-scale=1, minimum-scale=1, maximum-scale=1" />
    <title>物资管理 - 填领用单</title>
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

            <div class="grid-100 mobile-grid-100 grid-parent left">
                <h3>物品</h3>
            </div>
            <div class="grid-100 mobile-grid-100 grid-parent">
                <table class="table table-bordered" style="margin-top: 10px;" align="center">
                    <thead>
                        <tr>
                            <th  style="text-align: center; width: 20%;">名称</th>
                            <th  style="text-align: center; width: 20%;">分类</th>
                            <th style="text-align: center; width: 20%;">单位</th>
                            <th style="text-align: center; width: 20%;">规格</th>

                            <th style="text-align: center; width: 20%;">库存</th>

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
                <h3>领用单</h3>
            </div>
            <div class="grid-100 mobile-grid-100 grid-parent">
                <table class="table1" style="margin-top: 10px;width:100%;" align="center">
                    <tbody>
                        <tr>
                            <td>领用人：</td>
                            <td>
                                <asp:Label ID="responsible" runat="server"></asp:Label>
                    <asp:ImageButton ID="keeper_del" runat="server" AlternateText="×" OnClick="keeper_del_Click" Visible="false" />
                    <input id="responsibleId" runat="server" type="hidden" />
                    <telerik:RadSearchBox ID="keeper_source" runat="server" EmptyMessage="领用人筛选" MaxResultCount="10" LocalizationPath="~/Language" DataTextField="Name" DataValueField="Id" ShowSearchButton="false" OnDataSourceSelect="keeper_source_DataSourceSelect" ShowLoadingIcon="false" OnSearch="keeper_source_Search"></telerik:RadSearchBox>
                            </td>
                            <td>数量：</td>
                            <td>
                                <telerik:RadNumericTextBox ID="in_amount" runat="server" NumberFormat-DecimalDigits="2" DataType="System.Decimal" AllowOutOfRangeAutoCorrect="true"></telerik:RadNumericTextBox>
                            </td>
                            <td>备注：</td>

                            <td>
                                <telerik:RadTextBox ID="in_note" runat="server"></telerik:RadTextBox></td>

                        </tr>
                        <tr><td colspan="6">		<telerik:RadButton ID="in_confirm" runat="server" Checked="false" Text="确认领用单信息填写正确" AutoPostBack="true" ButtonType="ToggleButton" ToggleType="CheckBox">
                    </telerik:RadButton>
                    <input id="Hidden1" runat="server" type="hidden" /></td></tr>
                    </tbody>
                </table>
            </div>
            <div class="grid-100 mobile-grid-100 grid-parent">
	
                <asp:ImageButton AlternateText="领用" ID="in" runat="server" OnClick="in_Click" class="btn btn-xm btn-default" />
            </div>
        </telerik:RadAjaxPanel>
    </form>
    <script src="../Assets/javascripts/jquery.js"></script>
    <script src="../Assets/javascripts/common.js"></script>
    <style>
        html .RadSearchBox_Bootstrap .rsbInput {
            height: 32px;
            line-height: 32px;
            border-color: #000;
        }
        html .RadSearchBox_Bootstrap .rsbInput {
        z-index:-99;
        }
        .RadSearchBox .rsbInner{
              z-index:-99;
        }
        html .RadSearchBox_Bootstrap .rsbInner {
        
    padding: 0;
    border-radius: 4px;
    border-color: #000;
    color: #555;
    background-color: #fff;
    box-shadow: inset 0px 1px 1px rgba(0,0,0,0.075);

        }
        html body .RadInput_Bootstrap .riTextBox, html body .RadInputMgr_Bootstrap {
              padding: 0;
    border-radius: 4px;
    border-color: #000;
    color: #555;
    background-color: #fff;
    box-shadow: inset 0px 1px 1px rgba(0,0,0,0.075);

        }
    </style>
</body>
</html>
