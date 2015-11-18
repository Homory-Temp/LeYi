<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ImportX.aspx.cs" Inherits="DepotAction_Import" %>

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
        <homory:SideBarSingle runat="server" ID="SideBarSingle" Crumb="物资管理 - 资产导入" />
        <telerik:RadAjaxPanel ID="panel" runat="server" CssClass="container left">
            <div class="row">
                <div class="col-md-12">
                    <div>
                        <div class="btn btn-primary">1</div>
                        请选择财政局固定资产Excel文件
                    </div>
                    <div style="margin-left: 50px;">
                        <telerik:RadAsyncUpload RegisterWithScriptManager="True" runat="server" ID="im_up" Skin="Metro" OnFileUploaded="im_up_FileUploaded" HideFileInput="False" LocalizationPath="~/Language" TemporaryFolder="~/Common/物资/临时" TargetFolder="~/Common/物资/临时" PostbackTriggers="im_do" ChunkSize="1048576" AutoAddFileInputs="False" MaxFileInputsCount="1" InitialFileInputsCount="1" />
                        <input type="hidden" id="file" runat="server" />
                    </div>
                    <div>&nbsp;</div>
                    <div>&nbsp;</div>
                    <div>
                        <div class="btn btn-primary">2</div>
                        <telerik:RadButton ID="im_do" runat="server" Text="预览要导入的固定资产数据" OnClick="im_do_Click"></telerik:RadButton>
                    </div>
                    <div style="margin-left: 50px; margin-top: 20px;">
                        <telerik:RadGrid ID="grid" runat="server" AutoGenerateColumns="true" Font-Size="12px" AllowPaging="true" PageSize="20" OnNeedDataSource="grid_NeedDataSource" LocalizationPath="../Language">
                        </telerik:RadGrid>
                    </div>
                </div>
            </div>
        </telerik:RadAjaxPanel>
        <div>&nbsp;</div>
        <div>&nbsp;</div>
        <telerik:RadAjaxPanel ID="panelX" runat="server" CssClass="container left" LoadingPanelID="loading">
            <div class="row">
                <div class="col-md-12">
                    <div>
                        <div class="btn btn-primary">3</div>
                        <telerik:RadButton ID="im_ok" runat="server" Text="开始导入固定资产数据" OnClick="im_ok_Click"></telerik:RadButton>
                    </div>
                </div>
            </div>
        </telerik:RadAjaxPanel>
        <div>&nbsp;</div>
        <div>&nbsp;</div>
        <div>&nbsp;</div>
        <div>&nbsp;</div>
    </form>
</body>
</html>
