<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Student.aspx.cs" Inherits="Go.GoStudent" %>

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
        <telerik:RadAjaxLoadingPanel ID="loading" runat="server">
            <i class="ui huge teal loading icon" style="margin-top: 50px;"></i>
            <div>&nbsp;</div>
            <div style="color: #564F8A; font-size: 16px;">正在加载 请稍候....</div>
        </telerik:RadAjaxLoadingPanel>
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
                <telerik:RadSearchBox ID="peek" runat="server" OnSearch="peek_Search" EmptyMessage="查找学生...." EnableAutoComplete="false">
                </telerik:RadSearchBox>
            </div>
            <div class="five wide right aligned column">
                <telerik:RadButton ID="btnImport" runat="server" Text="导入" OnClick="btnImport_Click">
                </telerik:RadButton>
                <telerik:RadButton ID="btnDown" runat="server" Target="_blank" Text="模版下载" ButtonType="LinkButton" NavigateUrl="~/学生数据-完整.xls" AutoPostBack="false"></telerik:RadButton>
            </div>
            <div class="sixteen wide column">
                <table>
                    <tr>
                        <td rowspan="2">
                            <telerik:RadTreeView ID="tree" runat="server" CssClass="coreLeft" DataTextField="Name" DataValueField="Id" DataFieldID="Id" DataFieldParentID="ParentId" OnNodeClick="tree_NodeClick">
                                <NodeTemplate>
                                    <span id='<%# string.Format("homory_{0}", Container.Value) %>'><%# GenerateTreeName((Homory.Model.Department)Container.DataItem, Container.Index, Container.Level) %></span>
                                </NodeTemplate>
                            </telerik:RadTreeView>
                        </td>
                        <td>
                            <telerik:RadListView ID="view" runat="server" ItemPlaceholderID="holder" OnNeedDataSource="view_NeedDataSource">
                                <LayoutTemplate>
                                    <asp:Panel ID="innerPanel" runat="server" CssClass="ui left middle aligned stackable grid" Style="border: solid 1px #828282; padding: 20px; margin: 0px;" Visible='<%# ActionStudents.Count > 0 %>'>
                                        <div class="sixteen wide column" style="margin: 0; padding: 0;">
                                            <asp:PlaceHolder ID="holder" runat="server"></asp:PlaceHolder>
                                        </div>
                                        <div class="sixteen wide column" style="margin: 10px 0 0 0; padding: 0;">
                                            <asp:Button ID="btnMove" runat="server" CssClass="ui teal mini button" Text="调动" OnClick="btnMove_Click"></asp:Button>
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
                            <telerik:RadGrid ID="grid" runat="server" AllowPaging="true" AutoGenerateColumns="false" LocalizationPath="../Language" AllowSorting="True" PageSize="60" GridLines="None" OnNeedDataSource="grid_NeedDataSource" OnBatchEditCommand="grid_BatchEditCommand" OnItemCreated="grid_ItemCreated">
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
                                                <asp:CheckBox ID="actSel" runat="server" ItemID='<%# Eval("Id").ToString() %>' Checked='<%# ActionStudents.Contains((Guid)Eval("Id")) %>' OnCheckedChanged="actSel_CheckedChanged" AutoPostBack="true" />
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridTemplateColumn ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="48" ItemStyle-Width="48" HeaderText="学号 *" DataField="Ordinal" SortExpression="Ordinal" UniqueName="Ordinal">
                                            <ItemTemplate>
                                                <asp:Label ID="Label1" runat="server" Text='<%# Eval("Ordinal").ToString().PadLeft(2).Replace(" ", "0") %>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <telerik:RadNumericTextBox ID="Ordinal" runat="server" Width="40" EnabledStyle-HorizontalAlign="Center" MinValue="1" MaxValue="99" AllowOutOfRangeAutoCorrect="true" Value='<%# Bind("Ordinal") %>'>
                                                    <NumberFormat DecimalDigits="0" AllowRounding="true" />
                                                </telerik:RadNumericTextBox>
                                            </EditItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridTemplateColumn ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="52" ItemStyle-Width="52" HeaderText="姓名 *" DataField="RealName" SortExpression="RealName" UniqueName="RealName">
                                            <ItemTemplate>
                                                <asp:Label ID="Label2" runat="server" Text='<%# Eval("RealName") %>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <telerik:RadTextBox ID="RealName" runat="server" Width="44" EnabledStyle-HorizontalAlign="Center" MaxLength="16" Text='<%# Bind("RealName") %>'>
                                                </telerik:RadTextBox>
                                            </EditItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridTemplateColumn ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="60" ItemStyle-Width="60" HeaderText="状态" DataField="State" SortExpression="State" UniqueName="State">
                                            <ItemTemplate>
                                                <asp:Label ID="Label3" runat="server" Text='<%# Eval("State") %>'></asp:Label>
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
                                        <telerik:GridTemplateColumn ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="128" ItemStyle-Width="128" HeaderText="身份证号 *" DataField="IDCard" SortExpression="IDCard" UniqueName="IDCard">
                                            <ItemTemplate>
                                                <asp:Label ID="Label6" runat="server" Text='<%# Eval("IDCard") %>' Font-Italic='<%# Eval("IDCard") != null && Eval("IDCard").ToString().Length != 18 %>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <telerik:RadTextBox ID="IDCard" runat="server" EnabledStyle-HorizontalAlign="Center" Width="120" MaxLength="18" Text='<%# Bind("IDCard") %>'>
                                                </telerik:RadTextBox>
                                            </EditItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridTemplateColumn ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="80" ItemStyle-Width="80" HeaderText="学籍号" DataField="UniqueId" SortExpression="UniqueId" UniqueName="UniqueId">
                                            <ItemTemplate>
                                                <asp:Label ID="Label4" runat="server" Text='<%# Eval("UniqueId") %>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <telerik:RadTextBox ID="UniqueId" runat="server" Width="72" EnabledStyle-HorizontalAlign="Center" MaxLength="11" Text='<%# Bind("UniqueId") %>'>
                                                </telerik:RadTextBox>
                                            </EditItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridTemplateColumn ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="60" ItemStyle-Width="60" HeaderText="性别" DataField="Gender" SortExpression="Gender" UniqueName="Gender">
                                            <ItemTemplate>
                                                <asp:Label ID="Label7" runat="server" Text='<%# ((bool?)Eval("Gender")).HasValue ? (((bool?)Eval("Gender")).Value ? "男" : "女") : string.Empty %>'></asp:Label>
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
                                                <asp:Label ID="Label8" runat="server" Text='<%# ((DateTime?)Eval("Birthday")).HasValue ? ((DateTime)Eval("Birthday")).ToString("yyyy-MM-dd") : string.Empty %>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <telerik:RadDatePicker ID="Birthday" runat="server" Width="80" DateInput-DateFormat="yyyy-MM-dd" LocalizationPath="../Language" SelectedDate='<%# Bind("Birthday") %>'></telerik:RadDatePicker>
                                            </EditItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridTemplateColumn ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="48" ItemStyle-Width="48" HeaderText="民族" DataField="Nationality" SortExpression="Nationality" UniqueName="Nationality">
                                            <ItemTemplate>
                                                <asp:Label ID="Label9" runat="server" Text='<%# Eval("Nationality") %>' ToolTip='<%# Eval("Nationality") %>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <telerik:RadTextBox ID="Nationality" runat="server" Width="40" EnabledStyle-HorizontalAlign="Center" MaxLength="16" Text='<%# Bind("Nationality") %>'>
                                                </telerik:RadTextBox>
                                            </EditItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridTemplateColumn ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="68" ItemStyle-Width="68" HeaderText="籍贯" DataField="Birthplace" SortExpression="Birthplace" UniqueName="Birthplace">
                                            <ItemTemplate>
                                                <asp:Label ID="Label10" runat="server" Text='<%# Eval("Birthplace") %>' ToolTip='<%# Eval("Birthplace") %>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <telerik:RadTextBox ID="Birthplace" runat="server" Width="60" EnabledStyle-HorizontalAlign="Center" MaxLength="16" Text='<%# Bind("Birthplace") %>'>
                                                </telerik:RadTextBox>
                                            </EditItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridTemplateColumn ItemStyle-HorizontalAlign="Left" HeaderStyle-Width="128" ItemStyle-Width="128" HeaderText="现居住地" DataField="Address" SortExpression="Address" UniqueName="Address">
                                            <ItemTemplate>
                                                <asp:Label ID="Label11" runat="server" Text='<%# Eval("Address") %>' ToolTip='<%# Eval("Address") %>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <telerik:RadTextBox ID="Address" runat="server" Width="120" EnabledStyle-HorizontalAlign="Center" MaxLength="128" Text='<%# Bind("Address") %>'>
                                                </telerik:RadTextBox>
                                            </EditItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridTemplateColumn ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="52" ItemStyle-Width="52" HeaderText="监护人" DataField="Charger" SortExpression="Charger" UniqueName="Charger">
                                            <ItemTemplate>
                                                <asp:Label ID="Label12" runat="server" Text='<%# Eval("Charger") %>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <telerik:RadTextBox ID="Charger" runat="server" Width="44" EnabledStyle-HorizontalAlign="Center" MaxLength="16" Text='<%# Bind("Charger") %>'>
                                                </telerik:RadTextBox>
                                            </EditItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridTemplateColumn ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="80" ItemStyle-Width="80" HeaderText="联系号码" DataField="ChargerContact" SortExpression="ChargerContact" UniqueName="ChargerContact">
                                            <ItemTemplate>
                                                <asp:Label ID="Label14" runat="server" Text='<%# Eval("ChargerContact") %>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <telerik:RadTextBox ID="ChargerContact" runat="server" Width="72" EnabledStyle-HorizontalAlign="Center" MaxLength="32" Text='<%# Bind("ChargerContact") %>'>
                                                </telerik:RadTextBox>
                                            </EditItemTemplate>
                                        </telerik:GridTemplateColumn>
                                    </Columns>
                                    <PagerStyle Mode="NextPrevAndNumeric" PageSizes="10,20,60,100" Position="Bottom" PageSizeControlType="RadComboBox" AlwaysVisible="true" PagerTextFormat="{4} 第{0}页，共{1}页；第{2}-{3}项，共{5}项" />
                                </MasterTableView>
                                <ExportSettings Excel-Format="ExcelML" FileName="Students" Excel-FileExtension="xls" IgnorePaging="true" ExportOnlyData="true" OpenInNewWindow="true"></ExportSettings>
                            </telerik:RadGrid>
                        </td>
                    </tr>
                </table>
            </div>
        </telerik:RadAjaxPanel>
    </form>
</body>
</html>
