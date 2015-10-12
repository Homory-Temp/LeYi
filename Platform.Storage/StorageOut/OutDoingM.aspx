<%@ Page Language="C#" AutoEventWireup="true" CodeFile="OutDoingM.aspx.cs" Inherits="OutDoingM" %>

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
        <homory:Menu runat="server" ID="menu" />
        <telerik:RadAjaxPanel ID="ap" runat="server" CssClass="grid-container">
            <div class="grid-100 mobile-grid-100 grid-parent">
                <telerik:RadWizard ID="steps" runat="server" LocalizationPath="~/Language" ActiveStepIndex="1" DisplayCancelButton="false" DisplayNavigationButtons="false" DisplayProgressBar="false" OnActiveStepChanged="steps_ActiveStepChanged">
                    <WizardSteps>
                        <telerik:RadWizardStep StepType="Start" Title="报废准备"></telerik:RadWizardStep>
                        <telerik:RadWizardStep StepType="Step" Title="填报废单"></telerik:RadWizardStep>
                        <telerik:RadWizardStep StepType="Finish" Title="报废完成"></telerik:RadWizardStep>
                    </WizardSteps>
                </telerik:RadWizard>
            </div>
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
            <div class="grid-100 mobile-grid-100 grid-parent ">
                <table width="800" style="margin-top: 10px; border: 0px;" align="center">
                    <tbody>
                        <tr>
                            <td width="100" align="right" height="45px">数量：</td>
                            <td width="200" align="left">
                                <telerik:RadNumericTextBox ID="in_amount" runat="server" NumberFormat-DecimalDigits="2" DataType="System.Decimal" AllowOutOfRangeAutoCorrect="true" Width="200"></telerik:RadNumericTextBox>
                            </td>
                            <td width="100" align="right">报废人：</td>
                            <td width="200"  align="left">
                                <asp:Label ID="responsible" runat="server"></asp:Label>
                    <asp:ImageButton ID="keeper_del" runat="server" AlternateText="×" OnClick="keeper_del_Click" Visible="false" />
                    <input id="responsibleId" runat="server" type="hidden" />
                    <telerik:RadSearchBox ID="keeper_source" runat="server" EmptyMessage="报废人筛选" MaxResultCount="10" LocalizationPath="~/Language" DataTextField="Name" DataValueField="Id" ShowSearchButton="false" OnDataSourceSelect="keeper_source_DataSourceSelect" ShowLoadingIcon="false" OnSearch="keeper_source_Search"></telerik:RadSearchBox>
                            </td>
                            <tr>
                                <td width="100" align="right"  height="45px">报废类型：</td>
                                <td width="200"  align="left">
                                    <telerik:RadComboBox ID="out_type" runat="server" AllowCustomText="false" DataTextField="Name" DataValueField="Id"  Width="200"></telerik:RadComboBox>
                                </td>
                                <td  align="right"  width="100" >报废原因：</td>
                                <td width="200"  align="left">
                                    <telerik:RadTextBox ID="out_reason" runat="server"></telerik:RadTextBox>
                                </td>
                            </tr>
                            <tr>
                                <td  width="100" align="right"  height="45px">备注：</td>
                                <td width="600" colspan="3"  align="left" ><telerik:RadTextBox ID="in_note" runat="server"  width="585" ></telerik:RadTextBox>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="5"  height="45px">
                                    <telerik:RadButton ID="in_confirm" runat="server" Checked="false" Text="确认报废单信息填写正确" AutoPostBack="true" ButtonType="ToggleButton" ToggleType="CheckBox">
                                    </telerik:RadButton>
                                </td>
                            </tr>
                </table>
            </div>
            <div class="grid-100 mobile-grid-100 grid-parent">
                <asp:ImageButton AlternateText="报废" ID="in" runat="server" OnClick="in_Click" class="btn btn-xm btn-default" />
            </div>
        </telerik:RadAjaxPanel>
    </form>
    <script src="../Assets/javascripts/jquery.js"></script>
    <script src="../Assets/javascripts/common.js"></script>
</body>
</html>
