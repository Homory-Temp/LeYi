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
                <div class="col-md-12">
                    <input type="button" class="btn btn-tumblr" value="物资详情" />
                    <hr style="color: #2B2B2B; margin-top: 4px;" />
                </div>
            </div>
            <div class="row">
                <div class="col-md-12 text-center">
                    <span class="btn btn-danger" id="name" runat="server"></span>
                </div>
            </div>
            <div class="row">&nbsp;</div>
            <div class="row">
                <div class="col-md-4 text-center">
                    <div class="btn btn-info">
                        库存：<span id="no" runat="server"></span>&nbsp;<span id="unit" runat="server"></span>
                    </div>
                </div>
                <div class="col-md-4 text-center">
                    <div class="btn btn-info">
                        编号：<span id="sn" runat="server"></span>
                    </div>
                </div>
                <div class="col-md-4 text-center">
                    <div class="btn btn-info">
                        规格：<span id="sp" runat="server"></span>
                    </div>
                </div>
            </div>
            <div class="row">&nbsp;</div>
            <div class="row">
                <div class="col-md-3">
                    <img class="img-responsive" onclick="showPic(this);" style="width: 100%; cursor: pointer;" id="pa" runat="server" />
                </div>
                <div class="col-md-3">
                    <img class="img-responsive" onclick="showPic(this);" style="width: 100%; cursor: pointer;" id="pb" runat="server" />
                </div>
                <div class="col-md-3">
                    <img class="img-responsive" onclick="showPic(this);" style="width: 100%; cursor: pointer;" id="pc" runat="server" />
                </div>
                <div class="col-md-3">
                    <img class="img-responsive" onclick="showPic(this);" style="width: 100%; cursor: pointer;" id="pd" runat="server" />
                </div>
            </div>
            <div class="row">&nbsp;</div>
            <div class="row">
                <telerik:RadGrid ID="grid" runat="server" CssClass="col-md-12 text-center" AutoGenerateColumns="false" LocalizationPath="../Language" AllowSorting="True" PageSize="20" GridLines="None" OnNeedDataSource="grid_NeedDataSource" OnBatchEditCommand="grid_BatchEditCommand">
                    <MasterTableView EditMode="Batch" DataKeyNames="Id,Fixed,Ordinal" CommandItemDisplay="Top" CommandItemSettings-ShowAddNewRecordButton="false" HorizontalAlign="NotSet" ShowHeader="true" ShowHeadersWhenNoRecords="true" NoMasterRecordsText="">
                        <BatchEditingSettings EditType="Row" OpenEditingEvent="DblClick" />
                        <HeaderStyle HorizontalAlign="Center" />
                        <Columns>
                            <telerik:GridTemplateColumn HeaderText="编号" DataField="Ordinal" SortExpression="Ordinal" UniqueName="Ordinal" ReadOnly="true">
                                <ItemTemplate>
                                    <asp:Label runat="server" Text='<%# Eval("Ordinal") %>'></asp:Label>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridTemplateColumn HeaderText="存放地" DataField="Place" SortExpression="Place" UniqueName="Place">
                                <ItemTemplate>
                                    <asp:Label runat="server" Text='<%# Eval("Place") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <telerik:RadTextBox ID="Place" runat="server" EnabledStyle-HorizontalAlign="Center" Text='<%# Bind("Place") %>'>
                                    </telerik:RadTextBox>
                                </EditItemTemplate>
                            </telerik:GridTemplateColumn>
                        </Columns>
                    </MasterTableView>
                </telerik:RadGrid>
            </div>
        </telerik:RadAjaxPanel>
    </form>
</body>
</html>
