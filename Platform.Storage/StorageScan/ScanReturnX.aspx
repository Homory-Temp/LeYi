<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ScanReturnX.aspx.cs" Inherits="ScanReturnX" %>

<%@ Register Src="~/Menu/Menu.ascx" TagPrefix="homory" TagName="Menu" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <meta http-equiv="x-ua-compatible" content="ie=edge, chrome=1" />
    <meta name="viewport" content="width=device-width, initial-scale=1, minimum-scale=1, maximum-scale=1" />
    <title>物资管理 - 物资归还</title>
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
</head>
<body>
    <form id="form" runat="server">
        <telerik:RadScriptManager ID="sm" runat="server"></telerik:RadScriptManager>
        <telerik:RadCodeBlock ID="cb" runat="server">
            <script>
                function scanDo() {
                    $find("<%= apxx.ClientID %>").ajaxRequest("Do");
                }
                function scanDoX() {
                    $find("<%= ap.ClientID %>").ajaxRequest("Do");
                }
            </script>
        </telerik:RadCodeBlock>
        <homory:Menu runat="server" ID="menu" />
        <telerik:RadAjaxPanel ID="ap" runat="server" CssClass="grid-container" OnAjaxRequest="ap_AjaxRequest">
            <div class="grid-100 mobile-grid-100 grid-parent">
                <div>
                    借用人：<asp:Label ID="responsibleX" runat="server"></asp:Label>
                    <input id="responsibleIdX" runat="server" type="hidden" />
                    <telerik:RadSearchBox ID="keeper_sourceX" runat="server" EmptyMessage="借用人筛选" MaxResultCount="10" LocalizationPath="~/Language" DataTextField="Name" DataValueField="Id" ShowSearchButton="false" OnDataSourceSelect="keeper_sourceX_DataSourceSelect" ShowLoadingIcon="false" OnSearch="keeper_sourceX_Search"></telerik:RadSearchBox>
                </div>
                <div>
                    <telerik:RadTextBox ID="code" runat="server" MaxLength="12" EmptyMessage="请扫描或输入条码"></telerik:RadTextBox>
                    <asp:ImageButton ID="add" runat="server" AlternateText="添加" OnClick="add_Click" />
                </div>
            </div>
            <div class="grid-100 mobile-grid-100 grid-parent">
                <telerik:RadListView ID="viewW" runat="server" OnNeedDataSource="viewW_NeedDataSource" DataKeyNames="Id">
                    <ItemTemplate>
                        <div class="grid-100 mobile-grid-100 grid-parent">
                            <input id="object_id" runat="server" type="hidden" />
                            <div>
                                名称：<%# Eval("Name") %>
                                    分类：<%# (Container.DataItem as Models.StorageObject).GeneratePath() %>
                            </div>
                            <div>
                                单位：<%# Eval("Unit") %>
                                    规格：<%# Eval("Specification") %>
                            </div>
                            <div>
                                库存：<%# Eval("InAmount") %>
                            </div>
                            <div>
                                数量：<telerik:RadNumericTextBox ID="amount" runat="server" Value="1" NumberFormat-DecimalDigits="2" MaxValue='<%# CountBack((Guid)Eval("Id")) %>' AllowOutOfRangeAutoCorrect="true"></telerik:RadNumericTextBox>
                                备注：<telerik:RadTextBox ID="note" runat="server"></telerik:RadTextBox>
                            </div>
                        </div>
                    </ItemTemplate>
                </telerik:RadListView>
                <telerik:RadListView ID="viewD" runat="server" OnNeedDataSource="viewD_NeedDataSource" DataKeyNames="Id">
                    <ItemTemplate>
                        <div class="grid-100 mobile-grid-100 grid-parent">
                            <input id="object_id" runat="server" type="hidden" />
                            <div>
                                名称：<%# (Container.DataItem as Models.StorageInSingle).StorageObject.Name %>
                                    分类：<%# (Container.DataItem as Models.StorageInSingle).StorageObject.GeneratePath() %>
                            </div>
                            <div>
                                单位：<%# (Container.DataItem as Models.StorageInSingle).StorageObject.Unit %>
                                    规格：<%# (Container.DataItem as Models.StorageInSingle).StorageObject.Specification %>
                            </div>
                            <div>
                            </div>
                            <div>
                                编号：<%# Eval("InOrdinal") %>
                                备注：<telerik:RadTextBox ID="note" runat="server"></telerik:RadTextBox>
                            </div>
                        </div>
                    </ItemTemplate>
                </telerik:RadListView>
            </div>
        </telerik:RadAjaxPanel>
        <telerik:RadAjaxPanel ID="apx" runat="server" CssClass="grid-container">
            <div class="grid-100 mobile-grid-100 grid-parent">
                <div>
                    <telerik:RadButton ID="in_confirm" runat="server" Checked="true" Visible="false" Text="确认归还信息填写正确" AutoPostBack="true" ButtonType="ToggleButton" ToggleType="CheckBox">
                    </telerik:RadButton>
                    <div>
                        <asp:ImageButton AlternateText="归还" ID="out" runat="server" OnClick="out_Click" />
                    </div>
                </div>
            </div>
        </telerik:RadAjaxPanel>
        <telerik:RadAjaxPanel ID="apxx" runat="server" CssClass="grid-container" OnAjaxRequest="apxx_AjaxRequest">
            <div class="grid-100 mobile-grid-100 grid-parent">
                <telerik:RadListView ID="viewX" runat="server" OnNeedDataSource="viewX_NeedDataSource" DataKeyNames="借用标识" OnItemDataBound="viewX_ItemDataBound">
                    <ItemTemplate>
                        <div class="grid-100 mobile-grid-100 grid-parent">
                            <div style="background-color: yellow">
                                借用日期：<%# Eval("日期") %>
                                物资名称：<%# Eval("物品名称") %>
                                物资编码：<%# GetAutoId(Container.DataItem as Models.查询_借用单) %>
                                借用数量：<%# Eval("数量") %>
                                待归还数量：<%# Eval("待归还数") %>
                                单价：<%# Eval("单价").Money() %>
                                合计：<%# Eval("合计").Money() %>
                                借用人：<%# Eval("借用人") %>
                                备注：<%# Eval("备注") %>
                            </div>
                            <div style="background-color: limegreen;">
                                <asp:Repeater ID="s" runat="server">
                                    <ItemTemplate>
                                        <div>
                                            编号：<%# Eval("Ordinal") %>
                                        </div>
                                        <div>
                                            编码：<%# GetAutoId(Container.DataItem as Models.StorageLendSingle) %>
                                        </div>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </div>
                        </div>
                    </ItemTemplate>
                </telerik:RadListView>
            </div>
        </telerik:RadAjaxPanel>
    </form>
    <script src="../Assets/javascripts/jquery.js"></script>
    <script src="../Assets/javascripts/common.js"></script>
</body>
</html>
