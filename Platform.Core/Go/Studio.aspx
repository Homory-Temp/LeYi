<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Studio.aspx.cs" Inherits="Go.GoStudio" %>

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
                <telerik:RadWindow ID="windowIcon" runat="server" NavigateUrl="~/Extended/StudioIcon.aspx" Width="560" Height="130" CssClass="coreCenter coreMiddle" ReloadOnShow="true" VisibleStatusbar="false" Behaviors="Close" Modal="true" Title="选择头像" Localization-Close="关闭">
                </telerik:RadWindow>
                <telerik:RadWindow ID="windowLeader" runat="server" NavigateUrl="~/Extended/StudioLeader.aspx" Width="800" Height="400" CssClass="coreCenter coreMiddle" ReloadOnShow="true" VisibleStatusbar="false" Behaviors="Close" Modal="true" Title="选择引领" Localization-Close="关闭">
                </telerik:RadWindow>
                <telerik:RadWindow ID="windowMember" runat="server" OnClientClose="ccRefresh" NavigateUrl="~/Extended/StudioMember.aspx" Width="800" Height="400" CssClass="coreCenter coreMiddle" ReloadOnShow="true" VisibleStatusbar="false" Behaviors="Close" Modal="true" Title="选择成员" Localization-Close="关闭">
                </telerik:RadWindow>
                <telerik:RadWindow ID="windowCatalog" runat="server" NavigateUrl="~/Extended/StudioCatalog.aspx" Width="800" Height="400" CssClass="coreCenter coreMiddle" ReloadOnShow="true" VisibleStatusbar="false" Behaviors="Close" Modal="true" Title="栏目管理" Localization-Close="关闭">
                </telerik:RadWindow>
            </Windows>
        </telerik:RadWindowManager>
        <%-- ReSharper disable UseOfImplicitGlobalInFunctionScope --%>
        <%-- ReSharper disable UnusedParameter --%>
        <script type="text/javascript">
            function PopIcon(id) {
                window.radopen("../Extended/StudioIcon.aspx?" + id, "windowIcon");
                return false;
            }

            function PopLeader(id) {
                window.radopen("../Extended/StudioLeader.aspx?" + id, "windowLeader");
                return false;
            }

            function PopMember(id) {
                window.radopen("../Extended/StudioMember.aspx?" + id, "windowMember");
                return false;
            }

            function PopCatalog(id) {
                window.radopen("../Extended/StudioCatalog.aspx?" + id, "windowCatalog");
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
        <telerik:RadAjaxPanel ID="panel" runat="server" CssClass="container-fluid" LoadingPanelID="loading">
            <div class="row">
                <div class="col-md-12">
                    <telerik:RadGrid ID="grid" runat="server" AutoGenerateColumns="false" LocalizationPath="../Language" AllowSorting="True" PageSize="20" GridLines="None" OnNeedDataSource="grid_NeedDataSource" OnBatchEditCommand="grid_BatchEditCommand" OnItemCreated="grid_ItemCreated">
                        <MasterTableView EditMode="Batch" DataKeyNames="Id" CommandItemDisplay="Top" HorizontalAlign="NotSet" ShowHeader="true" ShowHeadersWhenNoRecords="true" NoMasterRecordsText="">
                            <BatchEditingSettings EditType="Row" OpenEditingEvent="DblClick" />
                            <HeaderStyle HorizontalAlign="Center" />
                            <Columns>
                                <telerik:GridTemplateColumn HeaderStyle-Width="60" ItemStyle-Width="60" HeaderText="序号" DataField="Ordinal" SortExpression="Ordinal" UniqueName="Ordinal">
                                    <ItemTemplate>
                                        <asp:Label runat="server" Text='<%# Eval("Ordinal") %>'></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <telerik:RadNumericTextBox ID="Ordinal" runat="server" EnabledStyle-HorizontalAlign="Center" Width="40" MinValue="1" MaxValue="999999" AllowOutOfRangeAutoCorrect="true" Value='<%# Bind("Ordinal") %>'>
                                            <NumberFormat DecimalDigits="0" AllowRounding="true" />
                                        </telerik:RadNumericTextBox>
                                    </EditItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn HeaderText="名称 *" DataField="Name" SortExpression="Name" UniqueName="Name">
                                    <ItemTemplate>
                                        <asp:Label runat="server" Text='<%# Eval("Name") %>'></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <telerik:RadTextBox ID="Name" runat="server" EnabledStyle-HorizontalAlign="Center" Width="64" MaxLength="16" Text='<%# Bind("Name") %>'>
                                        </telerik:RadTextBox>
                                    </EditItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn HeaderStyle-Width="160" ItemStyle-HorizontalAlign="Left" HeaderText="简介" ItemStyle-Width="160" DataField="Introduction" SortExpression="Introduction" UniqueName="Introduction">
                                    <ItemTemplate>
                                        <asp:Label runat="server" Text='<%# Eval("Introduction") %>'></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <telerik:RadTextBox ID="Introduction" runat="server" EnabledStyle-HorizontalAlign="Center" Width="160" MaxLength="512" Text='<%# Bind("Introduction") %>'>
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
                                <telerik:GridTemplateColumn ReadOnly="true" ItemStyle-HorizontalAlign="Center" HeaderText="头像" DataField="Icon" AllowSorting="false" UniqueName="Icon">
                                    <ItemTemplate>
                                        <asp:Image ID="icon" runat="server" onclick=<%# string.Format("PopIcon('{0}');", Eval("Id")) %> Width="60" Height="60" CssClass='<%# string.Format("{0}{1}", "ui image rootPointer", ((Homory.Model.State)Eval("State")) < Homory.Model.State.审核 ? "" : " disabled") %>' ImageUrl='<%# FormatIcon((string)Eval("Icon")) + "?"  + Guid.NewGuid().ToString("N")%>'></asp:Image>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn ItemStyle-HorizontalAlign="Center" ReadOnly="true" HeaderText="引领" AllowSorting="false" UniqueName="Members">
                                    <ItemTemplate>
                                        <asp:Label ID="leader" runat="server" CssClass="rootPointer" Text='<%# LoadLeader((Guid)Eval("Id")) %>' onclick=<%# string.Format("PopLeader('{0}');", Eval("Id")) %>></asp:Label>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn HeaderStyle-Width="160" ItemStyle-Width="160" ItemStyle-HorizontalAlign="Left" ReadOnly="true" HeaderText="成员" AllowSorting="false" UniqueName="Members">
                                    <ItemTemplate>
                                        <asp:Label ID="member" runat="server" CssClass="rootPointer" Text='<%# LoadMember((Guid)Eval("Id")) %>' onclick=<%# string.Format("PopMember('{0}');", Eval("Id")) %>></asp:Label>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn ReadOnly="true" AllowSorting="false" UniqueName="CatalogManage">
                                    <ItemTemplate>
                                        <asp:Label ID="catalog" runat="server" CssClass="rootPointer" Text="栏目管理" onclick=<%# string.Format("PopCatalog('{0}');", Eval("Id")) %>></asp:Label>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                            </Columns>
                        </MasterTableView>
                    </telerik:RadGrid>
                </div>
            </div>
        </telerik:RadAjaxPanel>
    </form>
</body>
</html>
