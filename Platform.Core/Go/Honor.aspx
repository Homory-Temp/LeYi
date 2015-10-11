<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Honor.aspx.cs" Inherits="Go.GoHonor" %>

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
                            <p style="text-align: left;">互动荣誉：</p>
                            <p>
                                <telerik:RadNumericTextBox ID="count1" CssClass="coreCenter" Width="120" Label="发布资源：" LabelWidth="60" runat="server" MinValue="0" MaxValue="99">
					                <NumberFormat DecimalDigits="0" GroupSeparator=""></NumberFormat>
				                </telerik:RadNumericTextBox>&nbsp;
                                <telerik:RadNumericTextBox ID="count2" CssClass="coreCenter" Width="120" Label="评分资源：" LabelWidth="60" runat="server" MinValue="0" MaxValue="99">
					                <NumberFormat DecimalDigits="0" GroupSeparator=""></NumberFormat>
				                </telerik:RadNumericTextBox>&nbsp;
                                <telerik:RadNumericTextBox ID="count3" CssClass="coreCenter" Width="120" Label="评论资源：" LabelWidth="60" runat="server" MinValue="0" MaxValue="99">
					                <NumberFormat DecimalDigits="0" GroupSeparator=""></NumberFormat>
				                </telerik:RadNumericTextBox>&nbsp;
                                <telerik:RadNumericTextBox ID="count4" CssClass="coreCenter" Width="120" Label="回复评论：" LabelWidth="60" runat="server" MinValue="0" MaxValue="99">
					                <NumberFormat DecimalDigits="0" GroupSeparator=""></NumberFormat>
				                </telerik:RadNumericTextBox>&nbsp;
                                <telerik:RadNumericTextBox ID="count5" CssClass="coreCenter" Width="120" Label="收藏资源：" LabelWidth="60" runat="server" MinValue="0" MaxValue="99">
					                <NumberFormat DecimalDigits="0" GroupSeparator=""></NumberFormat>
				                </telerik:RadNumericTextBox>
                            </p>
                            <p>
                                <telerik:RadButton ID="btnSave" runat="server" Text="保存" OnClick="btnSave_Click">
                                </telerik:RadButton>
                            </p>
                <div class="ui divider"></div>
                        <p style="text-align: left;">获奖荣誉：</p>
                        <p>
                            <telerik:RadGrid ID="grid" runat="server" AutoGenerateColumns="false" LocalizationPath="../Language" AllowSorting="True" PageSize="20" GridLines="None" OnNeedDataSource="grid_NeedDataSource" OnBatchEditCommand="grid_BatchEditCommand">
                                <MasterTableView EditMode="Batch" DataKeyNames="PrizeRange,PrizeLevel" CommandItemDisplay="Top" HorizontalAlign="NotSet" ShowHeader="true" ShowHeadersWhenNoRecords="true" NoMasterRecordsText="">
                                    <BatchEditingSettings EditType="Row" OpenEditingEvent="DblClick" />
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <CommandItemSettings ShowAddNewRecordButton="false" />
                                    <Columns>
                                        <telerik:GridTemplateColumn ReadOnly="true" HeaderText="获奖范围" DataField="PrizeRange" SortExpression="PrizeRange" UniqueName="PrizeRange">
                                            <ItemTemplate>
                                                <asp:Label runat="server" Text='<%# (Homory.Model.ResourcePrizeRange)Eval("PrizeRange") %>'></asp:Label>
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridTemplateColumn ReadOnly="true" HeaderText="获奖等级" DataField="PrizeLevel" SortExpression="PrizeLevel" UniqueName="PrizeLevel">
                                            <ItemTemplate>
                                                <asp:Label runat="server" Text='<%# (Homory.Model.ResourcePrizeLevel)Eval("PrizeLevel") %>'></asp:Label>
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridTemplateColumn HeaderText="荣誉分值 *" DataField="Credit" SortExpression="Credit" UniqueName="Credit">
                                            <ItemTemplate>
                                                <asp:Label ID="creditLabel" runat="server" Text='<%# Eval("Credit") %>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <telerik:RadNumericTextBox ID="Credit" runat="server" EnabledStyle-HorizontalAlign="Center" Width="64" MinValue="0" MaxValue="99" AllowOutOfRangeAutoCorrect="true" Value='<%# Bind("Credit") %>'>
                                                    <NumberFormat DecimalDigits="0" AllowRounding="true" />
                                                </telerik:RadNumericTextBox>
                                            </EditItemTemplate>
                                        </telerik:GridTemplateColumn>
                                    </Columns>
                                </MasterTableView>
                            </telerik:RadGrid>
                        </p>
            </div>
            </div>
        </telerik:RadAjaxPanel>
    </form>
</body>
</html>
