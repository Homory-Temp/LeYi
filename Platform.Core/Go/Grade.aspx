<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Grade.aspx.cs" Inherits="Go.GoGrade" %>

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
    <link href="../Content/Homory/css/common.css" rel="stylesheet" />
    <link href="../Content/Core/css/common.css" rel="stylesheet" />
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
        <telerik:RadCodeBlock runat="server">
            <div class="container-fluid">
                <div class="row">&nbsp;</div>
                <div class="row">
                    <div class="col-md-12">
                        <div class="btn-info btn btn-lg">组织管理 - 年级管理</div>
                    </div>
                </div>
            </div>
        </telerik:RadCodeBlock>
		<telerik:RadAjaxLoadingPanel ID="loading" runat="server">
			<div>&nbsp;</div>
			<div class="btn btn-lg btn-warning" style="margin-top: 50px;">正在加载 请稍候....</div>
		</telerik:RadAjaxLoadingPanel>
		<telerik:RadAjaxPanel ID="panel" runat="server" CssClass="container-fluid" LoadingPanelID="loading">
            <div class="row">&nbsp;</div>
            <div class="row">
                <div class="col-md-12">
                    <table style="text-align: left;">
                        <tr>
                            <td>
                                <telerik:RadNumericTextBox ID="years" CssClass="coreCenter" Width="120" Label="本学年：" LabelWidth="50" runat="server" MinValue="2014" MaxValue="2114">
					                <NumberFormat DecimalDigits="0" GroupSeparator=""></NumberFormat>
				                </telerik:RadNumericTextBox>
                                <telerik:RadNumericTextBox ID="toYear" CssClass="coreCenter" ReadOnly="true" Width="80" Label="-" LabelWidth="10" runat="server" MinValue="2014" MaxValue="2114">
					                <NumberFormat DecimalDigits="0" GroupSeparator=""></NumberFormat>
				                </telerik:RadNumericTextBox>
                            </td>
                            <td>&nbsp;&nbsp;</td>
                            <td>
                                <telerik:RadButton ID="btnSaveX" runat="server" Text="设定" OnClick="btnSaveX_Click">
                                </telerik:RadButton>
                            </td>
                            <td>&nbsp;&nbsp;</td>
                            <td><span style="color: red; font-size: 14px;">（于8月1日学期变更时更改，设定学年会同时进行毕业班处理，请勿随意更改）</span></td>
                        </tr>
                    </table>
                </div>
                <div class="col-md-6 col-xs-12">
                    <h6 class="btn btn-warning btn-padded">九年一贯</h6>
                    <telerik:RadGrid ID="g4" runat="server" CssClass="coreCenter" AutoGenerateColumns="True" LocalizationPath="../Language" GridLines="None" OnNeedDataSource="g4_NeedDataSource">
					    <MasterTableView>
						    <HeaderStyle HorizontalAlign="Center" />
					    </MasterTableView>
				    </telerik:RadGrid>
                    <h6 class="btn btn-warning btn-padded">小学</h6>
                    <telerik:RadGrid ID="g3" runat="server" CssClass="coreCenter" AutoGenerateColumns="True" LocalizationPath="../Language" GridLines="None" OnNeedDataSource="g3_NeedDataSource">
					    <MasterTableView>
						    <HeaderStyle HorizontalAlign="Center" />
					    </MasterTableView>
				    </telerik:RadGrid>
                </div>
                <div class="col-md-6 col-xs-12">
                    <h6 class="btn btn-warning btn-padded">幼儿园</h6>
                    <telerik:RadGrid ID="g1" runat="server" CssClass="coreCenter" AutoGenerateColumns="True" LocalizationPath="../Language" GridLines="None" OnNeedDataSource="g1_NeedDataSource">
					    <MasterTableView>
						    <HeaderStyle HorizontalAlign="Center" />
					    </MasterTableView>
				    </telerik:RadGrid>
                    <h6 class="btn btn-warning btn-padded">初中</h6>
                    <telerik:RadGrid ID="g2" runat="server" CssClass="coreCenter" AutoGenerateColumns="True" LocalizationPath="../Language" GridLines="None" OnNeedDataSource="g2_NeedDataSource">
					    <MasterTableView>
						    <HeaderStyle HorizontalAlign="Center" />
					    </MasterTableView>
				    </telerik:RadGrid>
                    <h6 class="btn btn-warning btn-padded">高中</h6>
                    <telerik:RadGrid ID="g5" runat="server" CssClass="coreCenter" AutoGenerateColumns="True" LocalizationPath="../Language" GridLines="None" OnNeedDataSource="g5_NeedDataSource">
					    <MasterTableView>
						    <HeaderStyle HorizontalAlign="Center" />
					    </MasterTableView>
				    </telerik:RadGrid>
                </div>
            </div>
            <div class="row">&nbsp;</div>
		</telerik:RadAjaxPanel>
        <style>
            .btn-padded {
                margin-bottom: 6px;
            }
        </style>
	</form>
</body>
</html>
