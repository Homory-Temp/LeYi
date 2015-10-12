<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ReturnDoingS.aspx.cs" Inherits="ReturnDoingS" %>

<%@ Register Src="~/Menu/Menu.ascx" TagPrefix="homory" TagName="Menu" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <meta http-equiv="x-ua-compatible" content="ie=edge, chrome=1" />
    <meta name="viewport" content="width=device-width, initial-scale=1, minimum-scale=1, maximum-scale=1" />
    <title>物资管理 - 填归还单</title>
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
                <telerik:RadWizard ID="steps" runat="server" LocalizationPath="~/Language" ActiveStepIndex="1" DisplayCancelButton="false" DisplayNavigationButtons="false" DisplayProgressBar="false" OnActiveStepChanged="steps_ActiveStepChanged">
                    <WizardSteps>
                        <telerik:RadWizardStep StepType="Start" Title="归还准备"></telerik:RadWizardStep>
                        <telerik:RadWizardStep StepType="Step" Title="填归还单"></telerik:RadWizardStep>
                        <telerik:RadWizardStep StepType="Finish" Title="归还完成"></telerik:RadWizardStep>
                    </WizardSteps>
                </telerik:RadWizard>
            </div>
            <div class="grid-100 mobile-grid-100 grid-parent header">物品</div>
            <div class="grid-100 mobile-grid-100 grid-parent">
                <input id="object_id" runat="server" type="hidden" />
                <div>
                    名称：<asp:Label ID="object_name" runat="server"></asp:Label>
                    分类：<asp:Label ID="object_catalog" runat="server"></asp:Label>
                </div>
                <div>
                    单位：<asp:Label ID="object_unit" runat="server"></asp:Label>
                    规格：<asp:Label ID="object_specification" runat="server"></asp:Label>
                </div>
                <div>
                    <asp:Image AlternateText="固" ID="object_fixed" runat="server" />
                    <asp:Image AlternateText="易" ID="object_consumable" runat="server" />
                </div>
                <div id="fixedArea" runat="server">
                    固定资产编号：<asp:Label ID="object_fixed_serial" runat="server"></asp:Label>
                </div>
                <div>
                    库存：<asp:Label ID="object_inAmount" runat="server"></asp:Label>
                    <asp:Image AlternateText="低" ID="object_low" runat="server" />
                    <asp:Image AlternateText="超" ID="object_high" runat="server" />
                </div>
            </div>
            <div class="grid-100 mobile-grid-100 grid-parent header">借用记录</div>
            <div class="grid-100 mobile-grid-100 grid-parent">
                <telerik:RadListView ID="borrow" runat="server" OnNeedDataSource="borrow_NeedDataSource">
                    <ItemTemplate>
                        <div>
                            借用人：<%# db.Value.UserName(Eval("BorrowUserId")) %>
                        </div>
                        <div>
                            借用数量：<%# Eval("Amount") %>
                            借用金额：<%# Eval("TotalMoney").Money() %>
                        </div>
                        <div>
                            已归还数量：<%# Eval("ReturnedAmount") %>
                            已归还金额：<%# (decimal.Multiply(decimal.Divide((decimal)Eval("ReturnedAmount"), (decimal)Eval("Amount")), (decimal)Eval("TotalMoney"))).Money() %>
                        </div>
                        <div>
                            借用日期：<%# ((int)Eval("TimeNode")).TimeNode() %>
                        </div>
                        <div>
                            备注：<%# Eval("Note") %>
                        </div>
                    </ItemTemplate>
                </telerik:RadListView>
            </div>
            <div class="grid-100 mobile-grid-100 grid-parent header">归还单</div>
            <div class="grid-100 mobile-grid-100 grid-parent">
                <div>
                    归还方式：<telerik:RadButton ID="act_r" runat="server" Checked="true" ButtonType="ToggleButton" ToggleType="CheckBox" Text="随机归还" OnCheckedChanged="act_r_CheckedChanged"></telerik:RadButton>
                    <telerik:RadButton ID="act_s" runat="server" Checked="false" ButtonType="ToggleButton" ToggleType="CheckBox" Text="特定归还" OnCheckedChanged="act_s_CheckedChanged"></telerik:RadButton>
                </div>
                <div id="r" runat="server">
                    数量：<telerik:RadNumericTextBox ID="in_amount" runat="server" NumberFormat-DecimalDigits="2" DataType="System.Decimal" AllowOutOfRangeAutoCorrect="true"></telerik:RadNumericTextBox>
                </div>
                <div id="s" runat="server" visible="false">
                    <div>
                        选定编号：
                        <telerik:RadNumericTextBox ID="s_from" runat="server" NumberFormat-DecimalDigits="0" DataType="System.Int32" AllowOutOfRangeAutoCorrect="true"></telerik:RadNumericTextBox>
                        到
                        <telerik:RadNumericTextBox ID="s_to" runat="server" NumberFormat-DecimalDigits="0" DataType="System.Int32" AllowOutOfRangeAutoCorrect="true"></telerik:RadNumericTextBox>
                    </div>
                    <div>
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
                <div>
                    备注：<telerik:RadTextBox ID="in_note" runat="server"></telerik:RadTextBox>
                </div>
                <div>
                    <telerik:RadButton ID="in_confirm" runat="server" Checked="false" Text="确认归还单信息填写正确" AutoPostBack="true" ButtonType="ToggleButton" ToggleType="CheckBox">
                    </telerik:RadButton>
                </div>
            </div>
            <div class="grid-100 mobile-grid-100 grid-parent">
                <asp:ImageButton AlternateText="归还" ID="in" runat="server" OnClick="in_Click" />
            </div>
        </telerik:RadAjaxPanel>
    </form>
    <script src="../Assets/javascripts/jquery.js"></script>
    <script src="../Assets/javascripts/common.js"></script>
</body>
</html>
