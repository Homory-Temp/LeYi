<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Note.aspx.cs" Inherits="Go.GoNote" %>

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
        <telerik:RadWindowManager ID="wm" runat="server">
            <Windows>
                <telerik:RadWindow ID="windowContent" runat="server" NavigateUrl="~/Extended/NoteContent.aspx" Width="800" Height="500" CssClass="coreCenter coreMiddle" ReloadOnShow="true" VisibleStatusbar="false" Behaviors="Close" Modal="true" Title="编辑正文" Localization-Close="关闭">
                </telerik:RadWindow>
            </Windows>
        </telerik:RadWindowManager>
	    <%-- ReSharper disable UseOfImplicitGlobalInFunctionScope --%>
	    <%-- ReSharper disable UnusedParameter --%>
        <script type="text/javascript">
            function PopContent(id) {
                window.radopen("../Extended/NoteContent.aspx?" + id, "windowContent");
                return false;
            }

            function refreshGrid() {
                var g = $find("grid");
                g.MasterTableView.rebind();
            }

            function ccRefresh(sender, e) {
                refreshGrid();
            }
        </script>
	    <%-- ReSharper restore UnusedParameter --%>
	    <%-- ReSharper restore UseOfImplicitGlobalInFunctionScope --%>
        <div>
            <homory:SideBar runat="server" ID="SideBar" />
        </div>
        <telerik:RadAjaxPanel ID="panel" runat="server" CssClass="ui center aligned page grid" style="margin:0;padding:0;" LoadingPanelID="loading">
            <div class="column">
                <telerik:RadGrid ID="grid" runat="server" AutoGenerateColumns="false" LocalizationPath="../Language" AllowSorting="True" PageSize="20" GridLines="None" OnNeedDataSource="grid_NeedDataSource" OnBatchEditCommand="grid_BatchEditCommand" OnItemCreated="grid_ItemCreated">
                    <MasterTableView EditMode="Batch" DataKeyNames="Id" CommandItemDisplay="Top" HorizontalAlign="NotSet" ShowHeader="true" ShowHeadersWhenNoRecords="true" NoMasterRecordsText="">
                        <BatchEditingSettings EditType="Row" OpenEditingEvent="DblClick" />
                        <HeaderStyle HorizontalAlign="Center" />
                        <Columns>
                            <telerik:GridBoundColumn HeaderText="发布时间" ReadOnly="true" UniqueName="Time" DataField="Time" DataFormatString="{0:yyyy-MM-dd}"></telerik:GridBoundColumn>
                            <telerik:GridTemplateColumn ItemStyle-Width="220" HeaderStyle-Width="220" ItemStyle-HorizontalAlign="Left" HeaderText="标题" DataField="Title" SortExpression="Title" UniqueName="Title">
                                <ItemTemplate>
                                    <asp:Label runat="server" Text='<%# Eval("Title") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <telerik:RadTextBox ID="Title" runat="server" EnabledStyle-HorizontalAlign="Center" Width="200" MaxLength="16" Text='<%# Bind("Title") %>'>
                                    </telerik:RadTextBox>
                                </EditItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridTemplateColumn HeaderText="状态" DataField="State" SortExpression="State" UniqueName="State">
                                <ItemTemplate>
                                    <asp:Label ID="stateLabel" runat="server" Text='<%# Eval("State") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <telerik:RadComboBox ID="State" runat="server" Width="64" EnableTextSelection="true" Text='<%# Eval("State") %>'>
                                        <Items>
                                            <telerik:RadComboBoxItem Text="" Value="-1" />
                                            <telerik:RadComboBoxItem Text="启用" Value="1" />
                                            <telerik:RadComboBoxItem Text="停用" Value="4" />
                                            <telerik:RadComboBoxItem Text="删除" Value="5" />
                                        </Items>
                                    </telerik:RadComboBox>
                                </EditItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridTemplateColumn ReadOnly="true" AllowSorting="false" UniqueName="CatalogManage">
                                <ItemTemplate>
                                   <asp:Label ID="catalog" runat="server" CssClass="rootPointer" Text="编辑正文" onclick=<%# string.Format("PopContent('{0}');", Eval("Id")) %>></asp:Label>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                        </Columns>
                    </MasterTableView>
                </telerik:RadGrid>
            </div>
        </telerik:RadAjaxPanel>
    </form>
</body>
</html>
