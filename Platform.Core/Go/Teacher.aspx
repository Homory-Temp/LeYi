<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Teacher.aspx.cs" Inherits="Go.GoTeacher" %>

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
        <script>
            function mngRequestStarted(ajaxManager, eventArgs) {
                if (eventArgs.get_eventTarget().indexOf("ExportToExcel") >= 0)
                    eventArgs.set_enableAjax(false);
            }
        </script>
        <telerik:RadAjaxPanel ID="panel" runat="server" CssClass="ui left aligned stackable page grid" Style="margin: 0; padding: 0;" LoadingPanelID="loading" ClientEvents-OnRequestStart="mngRequestStarted">
            <div class="five wide column">
                <telerik:RadComboBox ID="combo" runat="server" AutoPostBack="true" OnSelectedIndexChanged="combo_SelectedIndexChanged" DataTextField="Name" DataValueField="Id" Label="选择学校：" Width="220px" Filter="Contains" MarkFirstMatch="true" AllowCustomText="true" Height="202px">
                    <ItemTemplate>
                        <span><%# GenerateTreeName((Homory.Model.Department)Container.DataItem, Container.Index, 0) %></span>
                    </ItemTemplate>
                </telerik:RadComboBox>
            </div>
            <div class="six wide center aligned column">
                <telerik:RadSearchBox ID="peek" runat="server" OnSearch="peek_Search" EmptyMessage="查找教师...." EnableAutoComplete="false">
                </telerik:RadSearchBox>
            </div>
            <div class="five wide right aligned column">
                &nbsp;
            </div>
            <div class="sixteen wide column">
                <table>
                    <tr class="coreTop">
                        <td rowspan="2">
                            <telerik:RadTreeView ID="tree" runat="server" CssClass="coreLeft" DataTextField="Name" DataValueField="Id" DataFieldID="Id" DataFieldParentID="ParentId" OnNodeClick="tree_NodeClick">
                                <NodeTemplate>
                                    <span><%# ForceTreeName(Container.DataItem as Homory.Model.Department) %></span>
                                </NodeTemplate>
                            </telerik:RadTreeView>
                        </td>
                        <td>
                            <telerik:RadListView ID="view" runat="server" ItemPlaceholderID="holder" OnNeedDataSource="view_NeedDataSource">
                                <LayoutTemplate>
                                    <asp:Panel ID="innerPanel" runat="server" CssClass="ui left middle aligned stackable grid" Style="border: solid 1px #828282; padding: 20px; margin: 0px;" Visible='<%# ActionTeachers.Count > 0 %>'>
                                        <div class="sixteen wide column" style="margin: 0; padding: 0;">
                                            <asp:PlaceHolder ID="holder" runat="server"></asp:PlaceHolder>
                                        </div>
                                        <div class="sixteen wide column" style="margin: 10px 0 0 0; padding: 0;">
                                            <asp:Button ID="btnM" runat="server" CssClass="ui teal mini button" Text="主职设定" OnClick="btnM_Click"></asp:Button>
                                            <asp:Button ID="btnP" runat="server" CssClass="ui teal mini button" Text="兼职设定" OnClick="btnP_Click"></asp:Button>
                                            <asp:Button ID="btnB" runat="server" CssClass="ui teal mini button" Text="借调设定" OnClick="btnB_Click" OnPreRender="btnB_Load"></asp:Button>
                                            <asp:Button ID="btnV" runat="server" CssClass="ui teal mini button" Text="可访设定" OnClick="btnV_Click" OnPreRender="btnV_Load"></asp:Button>
                                            <asp:Button ID="btnR" runat="server" CssClass="ui teal mini button" Text="密码重置" OnClick="btnR_Click"></asp:Button>
                                        </div>
                                    </asp:Panel>
                                </LayoutTemplate>
                                <ItemTemplate>
                                    <telerik:RadButton ID="actionButton" runat="server" Text='<%# Eval("RealName") %>' Value='<%# Eval("Id") %>' OnClick="actionButton_Click"></telerik:RadButton>
                                </ItemTemplate>
                            </telerik:RadListView>
                        </td>
                    </tr>
                    <tr class="coreTop">
                        <td>
                            <div id="MainPanel" runat="server">
                                <h6 class="ui teal header left floated"><i class="ui teal circle icon"></i>主职</h6>
                                <div style="clear: both;"></div>
                                <telerik:RadGrid ID="grid" runat="server" AllowPaging="true" BackImageUrl="../../../Images/Common/BG.gif" AutoGenerateColumns="false" LocalizationPath="../Language" AllowSorting="True" PageSize="25" GridLines="None" OnNeedDataSource="grid_NeedDataSource" OnBatchEditCommand="grid_BatchEditCommand" OnItemCreated="grid_ItemCreated">
                                    <MasterTableView EditMode="Batch" DataKeyNames="Id" CommandItemDisplay="Top" HorizontalAlign="NotSet" ShowHeader="true" ShowHeadersWhenNoRecords="true" NoMasterRecordsText="">
                                        <BatchEditingSettings EditType="Row" OpenEditingEvent="DblClick" />
                                        <CommandItemSettings ShowExportToExcelButton="true" ExportToExcelText="导出" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <Columns>
                                            <telerik:GridTemplateColumn UniqueName="ActionSelectionColumn" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="48" ItemStyle-Width="48">
                                                <HeaderTemplate>
                                                    <asp:CheckBox ID="actSelX" runat="server" Checked='<%# ContainsAllActSel() %>' OnCheckedChanged="actSel_CheckedChangedX" AutoPostBack="true" />
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="actSel" runat="server" ItemID='<%# Eval("Id").ToString() %>' Checked='<%# ActionTeachers.Contains((Guid)Eval("Id")) %>' OnCheckedChanged="actSel_CheckedChanged" AutoPostBack="true" />
                                                </ItemTemplate>
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridTemplateColumn ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="48" ItemStyle-Width="48" HeaderText="序号 *" DataField="PriorOrdinal" SortExpression="PriorOrdinal" UniqueName="PriorOrdinal">
                                                <ItemTemplate>
                                                    <asp:Label ID="Label1" runat="server" Text='<%# Eval("PriorOrdinal").ToString().PadLeft(2).Replace(" ", "0") %>'></asp:Label>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <telerik:RadNumericTextBox ID="PriorOrdinal" runat="server" Width="40" EnabledStyle-HorizontalAlign="Center" MinValue="1" MaxValue="99" AllowOutOfRangeAutoCorrect="true" Value='<%# Bind("PriorOrdinal") %>'>
                                                        <NumberFormat DecimalDigits="0" AllowRounding="true" />
                                                    </telerik:RadNumericTextBox>
                                                </EditItemTemplate>
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridTemplateColumn ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="80" ItemStyle-Width="80" HeaderText="登录账号 *" DataField="Account" SortExpression="Account" UniqueName="Account">
                                                <ItemTemplate>
                                                    <asp:Label ID="Label2" runat="server" Text='<%# Eval("Account") %>'></asp:Label>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <telerik:RadTextBox ID="Account" runat="server" Width="72" EnabledStyle-HorizontalAlign="Center" MaxLength="16" Text='<%# Bind("Account") %>'>
                                                    </telerik:RadTextBox>
                                                </EditItemTemplate>
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridTemplateColumn ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="72" ItemStyle-Width="72" HeaderText="姓名 *" DataField="RealName" SortExpression="RealName" UniqueName="RealName">
                                                <ItemTemplate>
                                                    <asp:Label ID="Label3" runat="server" Text='<%# Eval("RealName") %>'></asp:Label>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <telerik:RadTextBox ID="RealName" runat="server" Width="64" EnabledStyle-HorizontalAlign="Center" MaxLength="16" Text='<%# Bind("RealName") %>'>
                                                    </telerik:RadTextBox>
                                                </EditItemTemplate>
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridTemplateColumn ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="60" ItemStyle-Width="60" HeaderText="状态" DataField="State" SortExpression="State" UniqueName="State">
                                                <ItemTemplate>
                                                    <asp:Label ID="Label4" runat="server" Text='<%# Eval("State") %>'></asp:Label>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <telerik:RadComboBox ID="State" runat="server" Width="52" EnableTextSelection="true" Text='<%# Bind("State") %>'>
                                                        <Items>
                                                            <telerik:RadComboBoxItem Text="" Value="-1" />
                                                            <telerik:RadComboBoxItem Text="启用" Value="1" />
                                                            <telerik:RadComboBoxItem Text="停用" Value="4" />
                                                            <telerik:RadComboBoxItem Text="删除" Value="5" />
                                                        </Items>
                                                    </telerik:RadComboBox>
                                                </EditItemTemplate>
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridTemplateColumn ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="60" ItemStyle-Width="60" HeaderText="同步" DataField="Sync" SortExpression="Sync" UniqueName="Sync">
                                                <ItemTemplate>
                                                    <asp:Label ID="Label7" runat="server" Text='<%# (((bool)Eval("Sync")) ? "是" : "否") %>'></asp:Label>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <telerik:RadComboBox ID="Sync" runat="server" Width="52" EnableTextSelection="true" Text='<%# Bind("Sync") %>'>
                                                        <Items>
                                                            <telerik:RadComboBoxItem Text="" Value="-1" />
                                                            <telerik:RadComboBoxItem Text="是" Value="True" />
                                                            <telerik:RadComboBoxItem Text="否" Value="False" />
                                                        </Items>
                                                    </telerik:RadComboBox>
                                                </EditItemTemplate>
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridTemplateColumn ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="80" ItemStyle-Width="80" HeaderText="手机号码 *" DataField="Phone" SortExpression="Phone" UniqueName="Phone">
                                                <ItemTemplate>
                                                    <asp:Label ID="Label8" runat="server" Text='<%# Eval("Phone") %>'></asp:Label>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <telerik:RadTextBox ID="Phone" runat="server" Width="72" EnabledStyle-HorizontalAlign="Center" MaxLength="11" Text='<%# Bind("Phone") %>'>
                                                    </telerik:RadTextBox>
                                                </EditItemTemplate>
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridTemplateColumn ItemStyle-HorizontalAlign="Left" HeaderStyle-Width="100" ItemStyle-Width="100" HeaderText="电子邮件" DataField="Email" SortExpression="Email" UniqueName="Email">
                                                <ItemTemplate>
                                                    <asp:Label ID="Label9" runat="server" Text='<%# Eval("Email") %>' ToolTip='<%# Eval("Email") %>'></asp:Label>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <telerik:RadTextBox ID="Email" runat="server" Width="92" EnabledStyle-HorizontalAlign="Center" MaxLength="32" Text='<%# Bind("Email") %>'>
                                                    </telerik:RadTextBox>
                                                </EditItemTemplate>
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridTemplateColumn ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="128" ItemStyle-Width="128" HeaderText="身份证号" DataField="IDCard" SortExpression="IDCard" UniqueName="IDCard">
                                                <ItemTemplate>
                                                    <asp:Label ID="Label10" runat="server" Text='<%# Eval("IDCard") %>' Font-Italic='<%# Eval("IDCard") != null && Eval("IDCard").ToString().Length != 18 %>'></asp:Label>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <telerik:RadTextBox ID="IDCard" runat="server" EnabledStyle-HorizontalAlign="Center" Width="120" MaxLength="18" Text='<%# Bind("IDCard") %>'>
                                                    </telerik:RadTextBox>
                                                </EditItemTemplate>
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridTemplateColumn ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="60" ItemStyle-Width="60" HeaderText="性别" DataField="Gender" SortExpression="Gender" UniqueName="Gender">
                                                <ItemTemplate>
                                                    <asp:Label ID="Label17" runat="server" Text='<%# ((bool?)Eval("Gender")).HasValue ? (((bool?)Eval("Gender")).Value ? "男" : "女") : string.Empty %>'></asp:Label>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <telerik:RadComboBox ID="Gender" runat="server" Width="52" EnableTextSelection="true" Text='<%# Bind("Gender") %>'>
                                                        <Items>
                                                            <telerik:RadComboBoxItem Text="" Value="-1" />
                                                            <telerik:RadComboBoxItem Text="男" Value="True" />
                                                            <telerik:RadComboBoxItem Text="女" Value="False" />
                                                        </Items>
                                                    </telerik:RadComboBox>
                                                </EditItemTemplate>
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridTemplateColumn ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="88" ItemStyle-Width="88" HeaderText="出生日期" DataField="Birthday" SortExpression="Birthday" UniqueName="Birthday">
                                                <ItemTemplate>
                                                    <asp:Label ID="Label18" runat="server" Text='<%# ((DateTime?)Eval("Birthday")).HasValue ? ((DateTime)Eval("Birthday")).ToString("yyyy-MM-dd") : string.Empty %>'></asp:Label>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <telerik:RadDatePicker ID="Birthday" runat="server" Width="80" DateInput-DateFormat="yyyy-MM-dd" LocalizationPath="../Language" SelectedDate='<%# Bind("Birthday") %>'></telerik:RadDatePicker>
                                                </EditItemTemplate>
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridTemplateColumn ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="48" ItemStyle-Width="48" HeaderText="民族" DataField="Nationality" SortExpression="Nationality" UniqueName="Nationality">
                                                <ItemTemplate>
                                                    <asp:Label ID="Label19" runat="server" Text='<%# Eval("Nationality") %>' ToolTip='<%# Eval("Nationality") %>'></asp:Label>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <telerik:RadTextBox ID="Nationality" runat="server" Width="40" EnabledStyle-HorizontalAlign="Center" MaxLength="16" Text='<%# Bind("Nationality") %>'>
                                                    </telerik:RadTextBox>
                                                </EditItemTemplate>
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridTemplateColumn ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="68" ItemStyle-Width="68" HeaderText="籍贯" DataField="Birthplace" SortExpression="Birthplace" UniqueName="Birthplace">
                                                <ItemTemplate>
                                                    <asp:Label ID="Label20" runat="server" Text='<%# Eval("Birthplace") %>' ToolTip='<%# Eval("Birthplace") %>'></asp:Label>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <telerik:RadTextBox ID="Birthplace" runat="server" Width="60" EnabledStyle-HorizontalAlign="Center" MaxLength="16" Text='<%# Bind("Birthplace") %>'>
                                                    </telerik:RadTextBox>
                                                </EditItemTemplate>
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridTemplateColumn ItemStyle-HorizontalAlign="Left" HeaderStyle-Width="128" ItemStyle-Width="128" HeaderText="现居住地" DataField="Address" SortExpression="Address" UniqueName="Address">
                                                <ItemTemplate>
                                                    <asp:Label ID="Label21" runat="server" Text='<%# Eval("Address") %>' ToolTip='<%# Eval("Address") %>'></asp:Label>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <telerik:RadTextBox ID="Address" runat="server" Width="120" EnabledStyle-HorizontalAlign="Center" MaxLength="128" Text='<%# Bind("Address") %>'>
                                                    </telerik:RadTextBox>
                                                </EditItemTemplate>
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridTemplateColumn ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="60" ItemStyle-Width="60" HeaderText="在编" DataField="PerStaff" SortExpression="PerStaff" UniqueName="PerStaff">
                                                <ItemTemplate>
                                                    <asp:Label ID="Label22" runat="server" Text='<%# ((bool?)Eval("PerStaff")).HasValue ? (((bool?)Eval("PerStaff")).Value ? "是" : "否") : string.Empty %>'></asp:Label>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <telerik:RadComboBox ID="PerStaff" runat="server" Width="52" EnableTextSelection="true" Text='<%# Bind("PerStaff") %>'>
                                                        <Items>
                                                            <telerik:RadComboBoxItem Text="" Value="-1" />
                                                            <telerik:RadComboBoxItem Text="是" Value="True" />
                                                            <telerik:RadComboBoxItem Text="否" Value="False" />
                                                        </Items>
                                                    </telerik:RadComboBox>
                                                </EditItemTemplate>
                                            </telerik:GridTemplateColumn>
                                        </Columns>
                                        <PagerStyle Mode="NextPrevAndNumeric" PageSizes="10,20,50,100" Position="Bottom" PageSizeControlType="RadComboBox" AlwaysVisible="true" PagerTextFormat="{4} 第{0}页，共{1}页；第{2}-{3}项，共{5}项" />
                                    </MasterTableView>
                                    <ExportSettings Excel-Format="ExcelML" FileName="Teachers" Excel-FileExtension="xls" IgnorePaging="true" ExportOnlyData="true" OpenInNewWindow="true"></ExportSettings>
                                </telerik:RadGrid>
                                <div class="ui divider"></div>
                                <h6 class="ui purple header left floated"><i class="ui purple circle icon"></i>兼职</h6>
                                <div style="clear: both;"></div>
                                <telerik:RadGrid ID="gridX" runat="server" AllowPaging="true" AutoGenerateColumns="false" LocalizationPath="../Language" AllowSorting="True" PageSize="10" GridLines="None" OnNeedDataSource="gridX_NeedDataSource" OnBatchEditCommand="gridX_BatchEditCommand" OnItemCreated="grid_ItemCreated">
                                    <MasterTableView EditMode="Batch" DataKeyNames="Id" CommandItemDisplay="Top" HorizontalAlign="NotSet" ShowHeader="true" ShowHeadersWhenNoRecords="true" NoMasterRecordsText="">
                                        <BatchEditingSettings EditType="Row" OpenEditingEvent="DblClick" />
                                        <CommandItemSettings ShowAddNewRecordButton="False"></CommandItemSettings>
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <Columns>
                                            <telerik:GridTemplateColumn ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="48" ItemStyle-Width="48" HeaderText="序号 *" DataField="MinorOrdinal" SortExpression="MinorOrdinal" UniqueName="MinorOrdinal">
                                                <ItemTemplate>
                                                    <asp:Label ID="Label12" runat="server" Text='<%# Eval("MinorOrdinal").ToString().PadLeft(2).Replace(" ", "0") %>'></asp:Label>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <telerik:RadNumericTextBox ID="MinorOrdinal" runat="server" EnabledStyle-HorizontalAlign="Center" Width="40" MinValue="1" MaxValue="99" AllowOutOfRangeAutoCorrect="true" Value='<%# Bind("MinorOrdinal") %>'>
                                                        <NumberFormat DecimalDigits="0" AllowRounding="true" />
                                                    </telerik:RadNumericTextBox>
                                                </EditItemTemplate>
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridTemplateColumn ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="80" ItemStyle-Width="80" HeaderText="登录账号 *" ReadOnly="True" DataField="Account" SortExpression="Account" UniqueName="Account">
                                                <ItemTemplate>
                                                    <asp:Label ID="Label2" runat="server" Text='<%# Eval("Account") %>'></asp:Label>
                                                </ItemTemplate>
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridTemplateColumn ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="100" ItemStyle-Width="100" HeaderText="姓名 *" ReadOnly="True" DataField="RealName" SortExpression="RealName" UniqueName="RealName">
                                                <ItemTemplate>
                                                    <asp:Label ID="Label13" runat="server" Text='<%# Eval("RealName") %>'></asp:Label>
                                                </ItemTemplate>
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridTemplateColumn ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="80" ItemStyle-Width="80" HeaderText="手机号码 *" ReadOnly="True" DataField="Phone" SortExpression="Phone" UniqueName="Phone">
                                                <ItemTemplate>
                                                    <asp:Label ID="Label15" runat="server" Text='<%# Eval("Phone") %>'></asp:Label>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <telerik:RadTextBox ID="Phone" runat="server" EnabledStyle-HorizontalAlign="Center" Width="72" MaxLength="11" Text='<%# Bind("Phone") %>'>
                                                    </telerik:RadTextBox>
                                                </EditItemTemplate>
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridTemplateColumn ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="60" ItemStyle-Width="60" HeaderText="状态" DataField="State" SortExpression="State" UniqueName="State">
                                                <ItemTemplate>
                                                    <asp:Label ID="Label14" runat="server" Text='<%# Eval("State") %>'></asp:Label>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <telerik:RadComboBox ID="State" runat="server" Width="52" EnableTextSelection="true" Text='<%# Bind("State") %>'>
                                                        <Items>
                                                            <telerik:RadComboBoxItem Text="" Value="-1" />
                                                            <telerik:RadComboBoxItem Text="启用" Value="1" />
                                                            <telerik:RadComboBoxItem Text="停用" Value="4" />
                                                            <telerik:RadComboBoxItem Text="删除" Value="5" />
                                                        </Items>
                                                    </telerik:RadComboBox>
                                                </EditItemTemplate>
                                            </telerik:GridTemplateColumn>
                                        </Columns>
                                        <PagerStyle Mode="NextPrevAndNumeric" PageSizes="10,20,50,100" Position="Bottom" PageSizeControlType="RadComboBox" AlwaysVisible="true" PagerTextFormat="{4} 第{0}页，共{1}页；第{2}-{3}项，共{5}项" />
                                    </MasterTableView>
                                </telerik:RadGrid>
                                <div class="ui divider"></div>
                                <h6 class="ui red header left floated"><i class="ui red circle icon"></i>借调</h6>
                                <div style="clear: both;"></div>
                                <telerik:RadGrid ID="grid_b" runat="server" AllowPaging="true" AutoGenerateColumns="false" LocalizationPath="../Language" AllowSorting="True" PageSize="10" GridLines="None" OnNeedDataSource="grid_b_NeedDataSource" OnBatchEditCommand="grid_b_BatchEditCommand" OnItemCreated="grid_ItemCreated">
                                    <MasterTableView EditMode="Batch" DataKeyNames="Id,DepartmentId,Type" CommandItemDisplay="Top" HorizontalAlign="NotSet" ShowHeader="true" ShowHeadersWhenNoRecords="true" NoMasterRecordsText="">
                                        <BatchEditingSettings EditType="Row" OpenEditingEvent="DblClick" />
                                        <CommandItemSettings ShowAddNewRecordButton="False"></CommandItemSettings>
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <Columns>
                                            <telerik:GridTemplateColumn ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="80" ItemStyle-Width="80" HeaderText="登录账号 *" ReadOnly="True" DataField="Account" SortExpression="Account" UniqueName="Account">
                                                <ItemTemplate>
                                                    <asp:Label ID="Label2" runat="server" Text='<%# Eval("Account") %>'></asp:Label>
                                                </ItemTemplate>
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridTemplateColumn ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="100" ItemStyle-Width="100" HeaderText="姓名 *" ReadOnly="True" DataField="RealName" SortExpression="RealName" UniqueName="RealName">
                                                <ItemTemplate>
                                                    <asp:Label ID="Label13" runat="server" Text='<%# Eval("RealName") %>'></asp:Label>
                                                </ItemTemplate>
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridTemplateColumn ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="80" ItemStyle-Width="80" HeaderText="手机号码 *" ReadOnly="True" DataField="Phone" SortExpression="Phone" UniqueName="Phone">
                                                <ItemTemplate>
                                                    <asp:Label ID="Label15" runat="server" Text='<%# Eval("Phone") %>'></asp:Label>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <telerik:RadTextBox ID="Phone" runat="server" EnabledStyle-HorizontalAlign="Center" Width="72" MaxLength="11" Text='<%# Bind("Phone") %>'>
                                                    </telerik:RadTextBox>
                                                </EditItemTemplate>
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridTemplateColumn ReadOnly="true" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="80" ItemStyle-Width="80" HeaderText="借调状态" DataField="Type" SortExpression="Type" UniqueName="Type">
                                                <ItemTemplate>
                                                    <asp:Label ID="Label14" runat="server" Text='<%# ((Homory.Model.DepartmentUserType)Eval("Type")) == Homory.Model.DepartmentUserType.借调前部门主职教师 ? "借出" : "借入" %>'></asp:Label>
                                                </ItemTemplate>
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridTemplateColumn ItemStyle-HorizontalAlign="Left" HeaderStyle-Width="300" ItemStyle-Width="300" HeaderText="借调信息" ReadOnly="True" DataField="Type" SortExpression="Type" UniqueName="Info">
                                                <ItemTemplate>
                                                    <asp:Label ID="Label16" runat="server" Text='<%# GetInfo(Container.DataItem as Homory.Model.ViewTeacher) %>'></asp:Label>
                                                </ItemTemplate>
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridTemplateColumn ItemStyle-HorizontalAlign="Center" HeaderText="撤销借调" HeaderStyle-Width="80" ItemStyle-Width="80" UniqueName="QuitBorrow">
                                                <ItemTemplate>
                                                    <asp:Label runat="server" Text="保留"></asp:Label>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                     <telerik:RadComboBox ID="State" runat="server" Width="52" EnableTextSelection="true" Text='<%# Bind("State") %>'>
                                                        <Items>
                                                            <telerik:RadComboBoxItem Text="保留" Value="1" />
                                                            <telerik:RadComboBoxItem Text="撤销" Value="5" />
                                                        </Items>
                                                    </telerik:RadComboBox>
                                                </EditItemTemplate>
                                            </telerik:GridTemplateColumn>
                                        </Columns>
                                        <PagerStyle Mode="NextPrevAndNumeric" PageSizes="10,20,50,100" Position="Bottom" PageSizeControlType="RadComboBox" AlwaysVisible="true" PagerTextFormat="{4} 第{0}页，共{1}页；第{2}-{3}项，共{5}项" />
                                    </MasterTableView>
                                </telerik:RadGrid>
                                <div class="ui divider"></div>
                                <h6 class="ui black header left floated"><i class="ui black circle icon"></i>可访</h6>
                                <div style="clear: both;"></div>
                                <telerik:RadGrid ID="grid_view" runat="server" AllowPaging="true" AutoGenerateColumns="false" LocalizationPath="../Language" AllowSorting="True" PageSize="10" GridLines="None" OnNeedDataSource="grid_view_NeedDataSource" OnBatchEditCommand="grid_view_BatchEditCommand" OnItemCreated="grid_ItemCreated">
                                    <MasterTableView EditMode="Batch" DataKeyNames="Id" CommandItemDisplay="Top" HorizontalAlign="NotSet" ShowHeader="true" ShowHeadersWhenNoRecords="true" NoMasterRecordsText="">
                                        <BatchEditingSettings EditType="Row" OpenEditingEvent="DblClick" />
                                        <CommandItemSettings ShowAddNewRecordButton="False"></CommandItemSettings>
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <Columns>
                                            <telerik:GridTemplateColumn ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="80" ItemStyle-Width="80" HeaderText="登录账号 *" ReadOnly="True" DataField="Account" SortExpression="Account" UniqueName="Account">
                                                <ItemTemplate>
                                                    <asp:Label ID="Label2" runat="server" Text='<%# Eval("Account") %>'></asp:Label>
                                                </ItemTemplate>
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridTemplateColumn ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="100" ItemStyle-Width="100" HeaderText="姓名 *" ReadOnly="True" DataField="RealName" SortExpression="RealName" UniqueName="RealName">
                                                <ItemTemplate>
                                                    <asp:Label ID="Label13" runat="server" Text='<%# Eval("RealName") %>'></asp:Label>
                                                </ItemTemplate>
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridTemplateColumn ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="80" ItemStyle-Width="80" HeaderText="手机号码 *" ReadOnly="True" DataField="Phone" SortExpression="Phone" UniqueName="Phone">
                                                <ItemTemplate>
                                                    <asp:Label ID="Label15" runat="server" Text='<%# Eval("Phone") %>'></asp:Label>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <telerik:RadTextBox ID="Phone" runat="server" EnabledStyle-HorizontalAlign="Center" Width="72" MaxLength="11" Text='<%# Bind("Phone") %>'>
                                                    </telerik:RadTextBox>
                                                </EditItemTemplate>
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridTemplateColumn ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="60" ItemStyle-Width="60" HeaderText="状态" DataField="State" SortExpression="State" UniqueName="State">
                                                <ItemTemplate>
                                                    <asp:Label ID="Label14" runat="server" Text='<%# Eval("State") %>'></asp:Label>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <telerik:RadComboBox ID="State" runat="server" Width="52" EnableTextSelection="true" Text='<%# Bind("State") %>'>
                                                        <Items>
                                                            <telerik:RadComboBoxItem Text="" Value="-1" />
                                                            <telerik:RadComboBoxItem Text="启用" Value="1" />
                                                            <telerik:RadComboBoxItem Text="停用" Value="4" />
                                                            <telerik:RadComboBoxItem Text="删除" Value="5" />
                                                        </Items>
                                                    </telerik:RadComboBox>
                                                </EditItemTemplate>
                                            </telerik:GridTemplateColumn>
                                        </Columns>
                                        <PagerStyle Mode="NextPrevAndNumeric" PageSizes="10,20,50,100" Position="Bottom" PageSizeControlType="RadComboBox" AlwaysVisible="true" PagerTextFormat="{4} 第{0}页，共{1}页；第{2}-{3}项，共{5}项" />
                                    </MasterTableView>
                                </telerik:RadGrid>
                            </div>
                        </td>
                    </tr>
                </table>
            </div>
        </telerik:RadAjaxPanel>
    </form>
</body>
</html>
