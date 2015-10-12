<%@ Page Language="C#" AutoEventWireup="true" CodeFile="InDoing.aspx.cs" Inherits="InDoing" %>

<%@ Register Src="~/Menu/Menu.ascx" TagPrefix="homory" TagName="Menu" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <meta http-equiv="x-ua-compatible" content="ie=edge, chrome=1" />
    <meta name="viewport" content="width=device-width, initial-scale=1, minimum-scale=1, maximum-scale=1" />
    <title>物资管理 - 填入库单</title>
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
                function reCalc(sender, args) {
                    var amount = $find("<%= in_amount.ClientID %>").get_value();
                    var perPrice = $find("<%= in_perPrice.ClientID %>").get_value();
                    $find("<%= in_price.ClientID %>").set_value(amount * perPrice);
                }
                function reCalc(sender, args) {
                    var amount = $find("<%= in_amount.ClientID %>").get_value();
                    if (!amount) amount = 0;
                    var perPrice = $find("<%= in_perPrice.ClientID %>").get_value();
                    if (!perPrice) perPrice = 0;
                    var addPrice = $find("<%= in_additionalPrice.ClientID %>").get_value();
                    if (!addPrice) addPrice = 0;
                    $("#<%= in_price.ClientID %>").text((amount * perPrice).toFixed(2));
                    $("#<%= in_calced.ClientID %>").text((amount * perPrice + addPrice).toFixed(2));
                }   </script>
        </telerik:RadCodeBlock>
        <homory:Menu runat="server" ID="menu" />
        <telerik:RadAjaxPanel ID="ap" runat="server" CssClass="grid-container">
            <%--            <div class="grid-100 mobile-grid-100 grid-parent">
                <telerik:RadWizard ID="steps" runat="server" LocalizationPath="~/Language" ActiveStepIndex="1" DisplayCancelButton="false" DisplayNavigationButtons="false" DisplayProgressBar="false" OnActiveStepChanged="steps_ActiveStepChanged">
                    <WizardSteps>
                        <telerik:RadWizardStep StepType="Start" Title="入库准备"></telerik:RadWizardStep>
                        <telerik:RadWizardStep StepType="Step" Title="填入库单"></telerik:RadWizardStep>
                        <telerik:RadWizardStep StepType="Finish" Title="入库完成"></telerik:RadWizardStep>
                    </WizardSteps>
                </telerik:RadWizard>
            </div>--%>
            <div class="grid-100 mobile-grid-100 grid-parent left">
                <h3>入库单</h3>
            </div>
            <div class="grid-100 mobile-grid-100 grid-parent">
                <table class="table1" style="margin-top: 10px; width: 100%;" align="center">
                    <tbody>
                        <tr>
                            <td align="right">年龄段：</td>
                            <td>
                                <telerik:RadComboBox ID="in_age" runat="server" LocalizationPath="~/Language" DataTextField="Name" DataValueField="Name" Filter="Contains" MarkFirstMatch="true" AppendDataBoundItems="true" ShowToggleImage="false" AllowCustomText="true">
                                    <Items>
                                        <telerik:RadComboBoxItem Text="" Value="" Selected="true" />
                                    </Items>
                                    <ItemTemplate>
                                        <%# Eval("Name") %><span style="display: none;"><%# Eval("PinYin") %></span>
                                    </ItemTemplate>
                                </telerik:RadComboBox>
                            </td>
                            <td>存放地：</td>
                            <td>
                                <telerik:RadComboBox ID="in_place" runat="server" AllowCustomText="true" LocalizationPath="~/Language" DataTextField="Name" DataValueField="Name" Filter="Contains" MarkFirstMatch="true" AppendDataBoundItems="true" ShowToggleImage="false">
                                    <Items>
                                        <telerik:RadComboBoxItem Text="" Value="" Selected="true" />
                                    </Items>
                                    <ItemTemplate>
                                        <%# Eval("Name") %><span style="display: none;"><%# Eval("PinYin") %></span>
                                    </ItemTemplate>
                                </telerik:RadComboBox>
                            </td>
                            <td>责任人：</td>

                            <td>
                                <asp:Label ID="responsible" runat="server" Text="未指定"></asp:Label>
                                <asp:ImageButton ID="keeper_del" runat="server" AlternateText="×" OnClick="keeper_del_Click" Visible="false" />
                                <input id="responsibleId" runat="server" type="hidden" />
                                <telerik:RadSearchBox ID="keeper_source" runat="server" EmptyMessage="责任人筛选" MaxResultCount="10" LocalizationPath="~/Language" DataTextField="Name" DataValueField="Id" ShowSearchButton="false" OnDataSourceSelect="keeper_source_DataSourceSelect" ShowLoadingIcon="false" OnSearch="keeper_source_Search"></telerik:RadSearchBox>
                            </td>

                        </tr>
                        <tr>
                            <td align="right">数量：</td>
                            <td>
                                <telerik:RadNumericTextBox ID="in_amount" runat="server" NumberFormat-DecimalDigits="2" DataType="System.Decimal" AllowOutOfRangeAutoCorrect="true" ClientEvents-OnValueChanged="reCalc"></telerik:RadNumericTextBox>

                            </td>
                            <td>单价：</td>
                            <td>
                                <telerik:RadNumericTextBox ID="in_perPrice" runat="server" NumberFormat-DecimalDigits="2" DataType="System.Decimal" AllowOutOfRangeAutoCorrect="true" ClientEvents-OnValueChanged="reCalc"></telerik:RadNumericTextBox></td>

                            <td>总价：</td>
                            <td>
                                <asp:Label ID="in_price" runat="server"></asp:Label></td>


                        </tr>
                        <tr>
                            <td align="right">优惠价：</td>

                            <td>
                                <telerik:RadNumericTextBox ID="in_additionalPrice" runat="server" NumberFormat-DecimalDigits="2" DataType="System.Decimal" AllowOutOfRangeAutoCorrect="true" ClientEvents-OnValueChanged="reCalc"></telerik:RadNumericTextBox></td>

                            <td>合计：</td>
                            <td>
                                <asp:Label ID="in_calced" runat="server"></asp:Label></td>
                        </tr>
                        <tr>

                            <td align="right">备注：</td>
                            <td colspan="3" align="left">
                                <telerik:RadTextBox ID="in_note" runat="server" Width="70%"></telerik:RadTextBox></td>
                        </tr>

                        <tr>
                            <td colspan="6">
                                <div class="grid-100 mobile-grid-100 grid-parent">
                                    <asp:ImageButton AlternateText="入库" ID="in" runat="server" OnClick="in_Click" class="btn btn-xm btn-default" />

                                </div>
                        </tr>
                </table>
            </div>
            <div class="grid-100 mobile-grid-100 grid-parent left">
                <h3>物品</h3>
            </div>
            <div class="grid-100 mobile-grid-100 grid-parent">
                <table class="table table-bordered" style="margin-top: 10px;" align="center">
                    <thead>
                        <tr>
                            <th style="text-align: center; width: 25%;">名称</th>
                            <th style="text-align: center; width: 15%;">分类</th>
                            <th style="text-align: center; width: 15%;">单位</th>
                            <th style="text-align: center; width: 15%;">规格</th>
                            <th style="text-align: center; width: 15%;">物资编号</th>
                            <th style="text-align: center; width: 15%;">库存</th>

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
                <h3>购置单</h3>
            </div>
            <div class="grid-100 mobile-grid-100 grid-parent">
                <table class="table table-bordered" style="margin-top: 10px;" align="center">
                    <thead>
                        <tr>
                            <th style="text-align: center; width: 8%;">购置单号</th>
                            <th style="text-align: center; width: 8%;">发票编号</th>
                            <th style="text-align: center; width: 8%;">购置时间</th>

                            <th style="text-align: center; width: 8%;">采购来源</th>

                            <th style="text-align: center; width: 8%;">使用对象</th>

                            <th style="text-align: center; width: 8%;">应付金额</th>
                            <th style="text-align: center; width: 8%;">实付金额</th>

                            <th style="text-align: center; width: 8%;">保管人</th>
                            <th style="text-align: center; width: 8%;">经手人</th>
                            <th style="text-align: center; width: 20%;">清单简述</th>

                        </tr>
                    </thead>
                    <tbody>
                        <tr>
                            <td align="left">
                                <input id="target_id" runat="server" type="hidden" />
                                <asp:Label ID="target_number" runat="server"></asp:Label>
                            </td>
                            <td align="left">
                                <asp:Label ID="target_receipt" runat="server"></asp:Label>
                            </td>
                            <td align="left">
                                <asp:Label ID="target_day" runat="server"></asp:Label>

                            </td>
                            <td align="left">
                                <asp:Label ID="target_source" runat="server"></asp:Label>
                            </td>
                          
                            <td align="left">
                                <asp:Label ID="target_target" runat="server"></asp:Label>
                            </td>
                            <td align="left">
                                <asp:Label ID="target_toPay" runat="server"></asp:Label>
                            </td>
                            <td align="left">
                                <asp:Label ID="target_paid" runat="server"></asp:Label>
                            </td>
                            <td align="left">
                                <asp:Label ID="target_keeper" runat="server"></asp:Label>
                            </td>
                            <td align="left">
                                <asp:Label ID="target_brokerage" runat="server"></asp:Label>
                            </td>
                              <td align="left" id="target_content" runat="server"></td>

                        </tr>
                    </tbody>
                </table>
            </div>
            <div class="grid-100 mobile-grid-100 grid-parent left">
                <h3>入库记录</h3>
            </div>
            <div class="grid-100 mobile-grid-100 grid-parent">
                <table class="table table-bordered" style="margin-top: 10px;" align="center">
                    <thead>
                        <tr>
                            <th>名称</th>
                            <th>分类</th>
                            <th>单位</th>
                            <th>规格</th>
                            <th>年龄段</th>

                            <th>存放地</th>
                            <th>数量</th>

                            <th>单价</th>
                            <th>优惠价</th>
                            <th>总价</th>
                            <th>日期</th>
                            <th>备注</th>
                        </tr>
                    </thead>
                    <tbody>
                        <telerik:RadListView ID="ins" runat="server" OnNeedDataSource="ins_NeedDataSource">
                            <EmptyDataTemplate>
                                <div>暂无入库记录</div>
                            </EmptyDataTemplate>
                            <ItemTemplate>

                                <tr>
                                    <td align="left">

                                        <%# (Container.DataItem as Models.StorageIn).StorageObject.Name %>
                                    </td>
                                    <td align="left">
                                        <%# (Container.DataItem as Models.StorageIn).StorageObject.GeneratePath() %>
                                    </td>
                                    <td align="left">
                                        <%# (Container.DataItem as Models.StorageIn).StorageObject.Unit %>
                                    </td>
                                    <td align="left">
                                        <%# (Container.DataItem as Models.StorageIn).StorageObject.Specification %>
                                    </td>
                                    <td align="left">
                                        <%# Eval("Age") %>
                                    </td>
                                    <td align="left">
                                        <%# Eval("Place") %>
                                    </td>
                                    <td align="left"><%# Eval("Amount") %></td>
                                    <td align="left">
                                        <%# Eval("PerPrice").Money() %></td>
                                    <td align="left">
                                        <%# Eval("AdditionalFee").Money() %></td>
                                    <td align="left">
                                        <%# Eval("TotalMoney").Money() %></td>
                                    <td align="left">

                                        <%# ((int)Eval("TimeNode")).TimeNode() %></td>
                                    <td align="left"><%# Eval("Note") %>
                                    </td>
                                </tr>

                            </ItemTemplate>
                        </telerik:RadListView>
                    </tbody>
                </table>
            </div>
        </telerik:RadAjaxPanel>
    </form>
    <script src="../Assets/javascripts/jquery.js"></script>
    <script src="../Assets/javascripts/common.js"></script>
    <style>
        html .RadSearchBox_Bootstrap .rsbInput {
            height: 32px;
            line-height: 32px;
        }
    </style>
</body>
</html>
