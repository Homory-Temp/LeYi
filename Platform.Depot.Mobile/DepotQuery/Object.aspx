<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Object.aspx.cs" Inherits="DepotQuery_Object" %>

<%@ Register Src="~/Control/SideBar.ascx" TagPrefix="homory" TagName="SideBar" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=Edge,Chrome=1" />
    <meta name="viewport" content="width=device-width,initial-scale=1,minimum-scale=1,maximum-scale=1" />
    <script src="../Content/jQuery/jquery.min.js"></script>
    <link href="../assets/css/bootstrap.min.css" rel="stylesheet" />
    <link href="../assets/css/style-responsive.css" rel="stylesheet" />
    <link href="../assets/css/style.css" rel="stylesheet" />
    <link href="../Content/Core/css/common.css" rel="stylesheet" />
    <link href="../Content/Core/css/fix.css" rel="stylesheet" />
    <script src="../assets/js/bootstrap.min.js"></script>
    <script src="../Content/Homory/js/common.js"></script>
    <script src="../Content/Homory/js/notify.min.js"></script>
    <!--[if lt IE 9]>
	    <script src="../Content/Homory/js/html5shiv.js"></script>
	    <script src="../Content/Homory/js/respond.min.js"></script>
    <![endif]-->
    <style>
        .objInfo {
            color: black;
        }
    </style>
    <script>
        function showPic(obj) {
            var id = $(obj).attr("src");
            if (id)
                window.open(id, '_blank');
        }
    </script>
</head>
<body>
    <form id="form" runat="server">
        <telerik:RadScriptManager ID="sm" runat="server"></telerik:RadScriptManager>
        <telerik:RadAjaxPanel ID="ap" runat="server" CssClass="container" LoadingPanelID="loading">
            <div class="row">
                <div class="col-md-3 col-xs-12">
                    <span class="btn btn-tumblr">物资详情</span>
                </div>
                <div class="col-md-6 col-xs-12">
                    <span class="btn btn-danger" id="name" runat="server"></span>
                </div>
                <div class="col-md-3 col-xs-12 text-right">
                    <input type="button" class="btn btn-info" id="back" runat="server" value="返回" onserverclick="back_ServerClick" />
                </div>
            </div>
            <div class="row">
                <div class="col-md-12 text-center">
                    <hr style="color: #2B2B2B; margin-top: 4px;" />
                </div>
            </div>
            <div class="row">
                <div class="col-md-12">
                    <table class="storeTable">
                        <tr>
                            <td style="width: 15%;">
                                <span>在库：</span>
                            </td>
                            <td style="width: 35%;">
                                <span id="no" runat="server"></span>&nbsp;<span id="unit" runat="server"></span>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 15%;">
                                <span>总量：</span>
                            </td>
                            <td style="width: 35%;">
                                <span id="total" runat="server"></span>&nbsp;<span id="unitx" runat="server"></span>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 15%;">
                                <span>品牌：</span>
                            </td>
                            <td style="width: 35%;">
                                <span id="brand" runat="server"></span>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 15%;">
                                <span>规格：</span>
                            </td>
                            <td style="width: 35%;">
                                <span id="sp" runat="server"></span>
                            </td>
                        </tr>
                        <tr id="xRow" runat="server">
                            <td style="width: 15%;">
                                <span>年龄段</span>
                            </td>
                            <td colspan="3" style="width: 85%;">
                                <span id="age" runat="server"></span>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 15%;">
                                <span>备注：</span>
                            </td>
                            <td style="width: 85%;">
                                <span id="note" runat="server"></span>
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
            <div class="row">&nbsp;</div>
            <div class="row">
                <div class="col-md-12 text-center">
                    <input type="button" class="btn btn-info" id="do_up" runat="server" value="上传图片" onserverclick="do_up_ServerClick" />
                </div>
            </div>
            <div class="row">
                <div class="col-md-6" id="da" runat="server">
                    <img class="img-responsive" onclick="showPic(this);" style="width: 100%; cursor: pointer;" id="pa" runat="server" />
                </div>
                <div class="col-md-6" id="db" runat="server">
                    <img class="img-responsive" onclick="showPic(this);" style="width: 100%; cursor: pointer;" id="pb" runat="server" />
                </div>
                <div class="col-md-6" id="dc" runat="server">
                    <img class="img-responsive" onclick="showPic(this);" style="width: 100%; cursor: pointer;" id="pc" runat="server" />
                </div>
                <div class="col-md-6" id="dd" runat="server">
                    <img class="img-responsive" onclick="showPic(this);" style="width: 100%; cursor: pointer;" id="pd" runat="server" />
                </div>
            </div>
            <div class="row"><span class="btn btn-info dictionaryX">存放地</span></div>
            <div class="row">
                <telerik:RadGrid ID="grid" runat="server" CssClass="col-md-12 text-center" AutoGenerateColumns="false" LocalizationPath="../Language" AllowSorting="True" PageSize="20" GridLines="None" OnNeedDataSource="grid_NeedDataSource" OnBatchEditCommand="grid_BatchEditCommand">
                    <MasterTableView EditMode="Batch" DataKeyNames="Id,Fixed,Ordinal" CommandItemDisplay="Top" CommandItemSettings-ShowAddNewRecordButton="false" HorizontalAlign="NotSet" ShowHeader="true" ShowHeadersWhenNoRecords="true" NoMasterRecordsText="">
                        <BatchEditingSettings EditType="Row" OpenEditingEvent="DblClick" />
                        <HeaderStyle HorizontalAlign="Center" />
                        <Columns>
                            <telerik:GridTemplateColumn HeaderText="编号" DataField="Ordinal" SortExpression="Ordinal" UniqueName="Ordinal" ReadOnly="true" ItemStyle-Width="25%">
                                <ItemTemplate>
                                    <asp:Label runat="server" Text='<%# Eval("Ordinal") %>'></asp:Label>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridTemplateColumn HeaderText="条码" DataField="Ordinal" SortExpression="Ordinal" UniqueName="Ordinal" ReadOnly="true" ItemStyle-Width="25%">
                                <ItemTemplate>
                                    <asp:HyperLink runat="server" ForeColor="#3E5A70" Target="_blank" Text='<%# Eval("Code") %>' NavigateUrl='<%# "../DepotScan/Flow?DepotId={0}&Code={1}".Formatted(Depot.Id, Eval("Code")) %>'></asp:HyperLink>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridTemplateColumn HeaderText="存放地" DataField="Place" SortExpression="Place" UniqueName="Place" ItemStyle-Width="30%">
                                <ItemTemplate>
                                    <asp:Label runat="server" Text='<%# Eval("Place") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <telerik:RadTextBox ID="Place" runat="server" EnabledStyle-HorizontalAlign="Center" Text='<%# Bind("Place") %>'>
                                    </telerik:RadTextBox>
                                </EditItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridButtonColumn HeaderText="编辑" CommandName="Edit" ItemStyle-Width="20%" Text="编辑" ButtonType="LinkButton"></telerik:GridButtonColumn>
                        </Columns>
                    </MasterTableView>
                </telerik:RadGrid>
            </div>
            <div class="row">&nbsp;</div>
            <div class="row"><span class="btn btn-info dictionaryX">借出记录</span></div>
            <div class="row">
                <telerik:RadGrid ID="gridX" runat="server" CssClass="col-md-12 text-center" AutoGenerateColumns="false" LocalizationPath="../Language" AllowSorting="True" PageSize="20" GridLines="None" OnNeedDataSource="gridX_NeedDataSource">
                    <MasterTableView CommandItemDisplay="None" HorizontalAlign="NotSet" ShowHeader="true" ShowHeadersWhenNoRecords="true" NoMasterRecordsText="全部在库">
                        <HeaderStyle HorizontalAlign="Center" />
                        <Columns>
                            <telerik:GridTemplateColumn HeaderText="借用人" ItemStyle-Width="33%">
                                <ItemTemplate>
                                    <asp:Label runat="server" Text='<%# Eval("UserName") %>'></asp:Label>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridTemplateColumn HeaderText="借用数量" ItemStyle-Width="33%">
                                <ItemTemplate>
                                    <asp:Label runat="server" Text='<%# Eval("Amount").ToAmount(Depot.Featured(Models.DepotType.小数数量库)) %>'></asp:Label>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridTemplateColumn HeaderText="待还数量" ItemStyle-Width="33%">
                                <ItemTemplate>
                                    <asp:Label runat="server" Text='<%# ((decimal)Eval("Amount") - (decimal)Eval("ReturnedAmount")).ToAmount(Depot.Featured(Models.DepotType.小数数量库))%>'></asp:Label>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                        </Columns>
                    </MasterTableView>
                </telerik:RadGrid>
            </div>
            <div class="row">&nbsp;</div>
        </telerik:RadAjaxPanel>
    </form>
</body>
</html>
