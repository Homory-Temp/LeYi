<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Catalog.aspx.cs" Inherits="StoreSetting_Catalog" %>

<%@ Register Src="~/Control/SideBarSingle.ascx" TagPrefix="homory" TagName="SideBarSingle" %>

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
</head>
<body>
    <form id="form" runat="server">
        <homory:SideBarSingle runat="server" ID="SideBarSingle" Crumb="系统设置 - 物资类别" />
        <telerik:RadAjaxPanel ID="ap" runat="server" CssClass="container-fluid" LoadingPanelID="loading">
            <div class="row">
                <div class="col-md-2"></div>
                <div class="col-md-8">
                    <div style="float: left; border-right: 1px solid #2B2B2B;">
                        <div class="container-fluid">
                            <div class="row">
                                <div class="col-md-12">
                                    <span class="btn btn-tumblr">物资类别：</span>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-12">
                                    <telerik:RadTreeView ID="tree" runat="server"></telerik:RadTreeView>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div style="float: left;">
                        <div class="container-fluid">
                            <div class="row">
                                <div class="col-md-12">
                                    <telerik:RadTextBox ID="name" runat="server" Width="400"></telerik:RadTextBox>
                                    &nbsp;&nbsp;
                                    <input id="add" runat="server" type="button" class="btn btn-tumblr" value="保存" onserverclick="add_ServerClick" />
                                    &nbsp;&nbsp;
                                    <span id="sp" runat="server" class="text text-danger">（一级分类将作为“使用对象”用于日常操作和报表查询）</span>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div style="clear: both;"></div>
                </div>
                <div class="col-md-2"></div>
            </div>
        </telerik:RadAjaxPanel>
    </form>
</body>
</html>
