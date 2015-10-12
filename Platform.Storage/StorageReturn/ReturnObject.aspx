<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ReturnObject.aspx.cs" Inherits="Return" %>

<%@ Register Src="~/Menu/Menu.ascx" TagPrefix="homory" TagName="Menu" %>
<%@ Register Src="~/StorageObject/ObjectImage.ascx" TagPrefix="homory" TagName="ObjectImage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <meta http-equiv="x-ua-compatible" content="ie=edge, chrome=1" />
    <meta name="viewport" content="width=device-width, initial-scale=1, minimum-scale=1, maximum-scale=1" />
    <title>物资管理 - 归还准备</title>
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
                <telerik:RadWizard ID="steps" runat="server" LocalizationPath="~/Language" ActiveStepIndex="0" DisplayCancelButton="false" DisplayNavigationButtons="false" DisplayProgressBar="false" OnActiveStepChanged="steps_ActiveStepChanged">
                    <WizardSteps>
                        <telerik:RadWizardStep StepType="Start" Title="归还准备"></telerik:RadWizardStep>
                        <telerik:RadWizardStep StepType="Step" Title="填归还单"></telerik:RadWizardStep>
                        <telerik:RadWizardStep StepType="Finish" Title="归还完成"></telerik:RadWizardStep>
                    </WizardSteps>
                </telerik:RadWizard>
            </div>
            <div class="grid-100 mobile-grid-100 grid-parent">
                <div class="grid-50 mobile-grid-50 grid-parent">
                    <div>物品</div>
                    <div>
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
                </div>
                <div class="grid-50 mobile-grid-50 grid-parent">
                    <div>
                        借用人：<telerik:RadSearchBox ID="tar_search" runat="server" DataTextField="RealName" DataValueField="Id" ShowSearchButton="false" OnDataSourceSelect="tar_search_DataSourceSelect" ShowLoadingIcon="false" OnSearch="tar_search_Search"></telerik:RadSearchBox>
                    </div>
                    <div>
                        <telerik:RadListView ID="tar_view" runat="server" OnNeedDataSource="tar_view_NeedDataSource">
                            <ItemTemplate>
                                <div>
                                    <telerik:RadButton ID="tar_set" AutoPostBack="true" ButtonType="ToggleButton" ToggleType="Radio" runat="server" Checked='<%# tar_id.Value == Eval("Id").ToString() %>' Text='<%# Eval("RealName") %>' Value='<%# Eval("Id") %>' OnClick="tar_set_Click"></telerik:RadButton>
                                </div>
                                <div>
                                    手机号码：<asp:Label runat="server" Text='<%# (Container.DataItem as Models.User).Teacher.Phone %>'></asp:Label>
                                </div>
                            </ItemTemplate>
                        </telerik:RadListView>
                        <input id="tar_id" runat="server" type="hidden" />
                    </div>
                </div>
            </div>
            <div class="grid-100 mobile-grid-100 grid-parent">
                <asp:ImageButton AlternateText="归还" ID="in" runat="server" Visible="false" OnClick="in_Click" />
            </div>
        </telerik:RadAjaxPanel>
    </form>
    <script src="../Assets/javascripts/jquery.js"></script>
    <script src="../Assets/javascripts/common.js"></script>
</body>
</html>
