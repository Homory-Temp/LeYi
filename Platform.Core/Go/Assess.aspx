<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Assess.aspx.cs" Inherits="Go.GoAssess" %>

<%@ Import Namespace="System.Globalization" %>

<%@ Register Src="~/Control/SideBar.ascx" TagPrefix="homory" TagName="SideBar" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=Edge,Chrome=1" />
    <meta name="viewport" content="width=device-width,initial-scale=1,minimum-scale=1,maximum-scale=1" />
    <title>基础平台</title>
    <script src="../Content/jQuery/jquery.min.js"></script>
    <link href="../assets/css/bootstrap.min.css" rel="stylesheet" />
    <link href="../assets/css/style-responsive.css" rel="stylesheet" />
    <link href="../assets/css/style.css" rel="stylesheet" />
    <script src="../assets/js/bootstrap.min.js"></script>
    <link href="../Content/Semantic/css/semantic.min.css" rel="stylesheet" />
    <link href="../Content/Homory/css/common.css" rel="stylesheet" />
    <link href="../Content/Core/css/common.css" rel="stylesheet" />
    <script src="../Content/Semantic/javascript/semantic.min.js"></script>
    <script src="../Content/Homory/js/common.js"></script>
    <script src="../Content/Homory/js/notify.min.js"></script>
    <!--[if lt IE 9]>
	    <script src="../Content/Homory/js/html5shiv.js"></script>
	    <script src="../Content/Homory/js/respond.min.js"></script>
    <![endif]-->
</head>
<body>
    <form id="formHome" runat="server">
        <div>
            <homory:SideBar runat="server" ID="SideBar" />
        </div>
        <telerik:RadAjaxPanel ID="panel" runat="server" CssClass="container-fluid" LoadingPanelID="loading">
            <div class="row">
                <div class="col-md-12">
                    <div>
                        <telerik:RadComboBox ID="grade" runat="server" Skin="MetroTouch" DataTextField="Name" DataValueField="Id" Label="年级：" Width="120" AutoPostBack="true" OnSelectedIndexChanged="combo_SelectedIndexChanged"></telerik:RadComboBox>
                        &nbsp;&nbsp;&nbsp;&nbsp;
                    <telerik:RadComboBox ID="course" runat="server" Skin="MetroTouch" DataTextField="Name" DataValueField="Id" Label="课程：" Width="120" AutoPostBack="true" OnSelectedIndexChanged="combo_SelectedIndexChanged"></telerik:RadComboBox>
                    </div>
                    <div class="ui divider"></div>
                    <div>
                        <telerik:RadGrid ID="grid" runat="server" CssClass="coreCenter" AutoGenerateColumns="false" LocalizationPath="../Language" AllowSorting="True" PageSize="20" GridLines="None" OnNeedDataSource="grid_NeedDataSource" OnBatchEditCommand="grid_BatchEditCommand" OnItemCreated="grid_ItemCreated" OnItemCommand="grid_ItemCommand">
                            <MasterTableView EditMode="Batch" DataKeyNames="Id" CommandItemDisplay="Top" HorizontalAlign="NotSet" ShowHeader="true" ShowHeadersWhenNoRecords="true" NoMasterRecordsText="">
                                <BatchEditingSettings EditType="Row" OpenEditingEvent="DblClick" />
                                <HeaderStyle HorizontalAlign="Center" />
                                <Columns>
                                    <telerik:GridTemplateColumn HeaderText="序号" DataField="Ordinal" SortExpression="Ordinal" UniqueName="Ordinal">
                                        <ItemTemplate>
                                            <asp:Label runat="server" Text='<%# Eval("Ordinal") %>'></asp:Label>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <telerik:RadNumericTextBox ID="Ordinal" runat="server" EnabledStyle-HorizontalAlign="Center" Width="64" MinValue="1" MaxValue="99" AllowOutOfRangeAutoCorrect="true" Value='<%# Bind("Ordinal") %>'>
                                                <NumberFormat DecimalDigits="0" AllowRounding="true" />
                                            </telerik:RadNumericTextBox>
                                        </EditItemTemplate>
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridTemplateColumn ItemStyle-Width="150" HeaderStyle-Width="150" ItemStyle-HorizontalAlign="Left" HeaderText="评估方案名称 *" DataField="Title" SortExpression="Title" UniqueName="Title">
                                        <ItemTemplate>
                                            <asp:Label runat="server" Text='<%# Eval("Title") %>'></asp:Label>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <telerik:RadTextBox ID="Title" runat="server" EnabledStyle-HorizontalAlign="Center" Width="130" MaxLength="16" Text='<%# Bind("Title") %>'>
                                            </telerik:RadTextBox>
                                        </EditItemTemplate>
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridBoundColumn HeaderText="时间" ReadOnly="true" UniqueName="Time" DataField="Time" DataFormatString="{0:yyyy-MM-dd}"></telerik:GridBoundColumn>
                                    <telerik:GridTemplateColumn HeaderText="状态" DataField="State" SortExpression="State" UniqueName="State">
                                        <ItemTemplate>
                                            <asp:Label runat="server" Text='<%# Eval("State") %>'></asp:Label>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <telerik:RadComboBox ID="State" runat="server" Width="64" EnableTextSelection="true" Text='<%# Bind("State") %>'>
                                                <Items>
                                                    <telerik:RadComboBoxItem Text="" Value="-1" />
                                                    <telerik:RadComboBoxItem Text="启用" Value="1" />
                                                    <telerik:RadComboBoxItem Text="停用" Value="4" />
                                                    <telerik:RadComboBoxItem Text="删除" Value="5" />
                                                </Items>
                                            </telerik:RadComboBox>
                                        </EditItemTemplate>
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridButtonColumn Text="评估项目设定" CommandName="Select">
                                    </telerik:GridButtonColumn>
                                </Columns>
                            </MasterTableView>
                        </telerik:RadGrid>
                    </div>
                    <asp:Panel ID="items" runat="server" Visible="false">
                        <div class="ui divider"></div>
                        <div>
                            <input id="v" runat="server" type="hidden" />
                            <asp:Repeater ID="repeater" runat="server">
                                <HeaderTemplate>
                                    <table class="coreAuto">
                                        <tr>
                                            <td>&nbsp;</td>
                                            <td>&nbsp;</td>
                                            <td class="coreLeft">
                                                <h6 class="ui teal header">评估项目</h6>
                                            </td>
                                            <td>&nbsp;</td>
                                            <td class="coreLeft">
                                                <h6 class="ui purple header">分值</h6>
                                            </td>
                                        </tr>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <tr>
                                        <td>
                                            <div class="ui black circular small label"><%# (Container.ItemIndex + 1).ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') %></div>
                                        </td>
                                        <td>&nbsp;</td>
                                        <td>
                                            <telerik:RadTextBox ID="in_name" runat="server" Width="400"></telerik:RadTextBox>
                                        </td>
                                        <td>&nbsp;</td>
                                        <td>
                                            <telerik:RadNumericTextBox ID="in_value" runat="server" EnabledStyle-HorizontalAlign="Center" Width="64" MinValue="1" MaxValue="99" AllowOutOfRangeAutoCorrect="true">
                                                <NumberFormat DecimalDigits="0" AllowRounding="true" />
                                            </telerik:RadNumericTextBox>
                                        </td>
                                    </tr>
                                </ItemTemplate>
                                <FooterTemplate>
                                    </table>
                                </FooterTemplate>
                            </asp:Repeater>
                        </div>
                        <p>
                            <telerik:RadButton ID="buttonOk" runat="server" Skin="MetroTouch" Text="保存" OnClick="buttonOk_Click"></telerik:RadButton>
                        </p>
                    </asp:Panel>
                </div>
            </div>
        </telerik:RadAjaxPanel>
    </form>
</body>
</html>
