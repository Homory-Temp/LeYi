<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Import.aspx.cs" Inherits="DepotAction_Import" %>

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
        <homory:SideBarSingle runat="server" ID="SideBarSingle" Crumb="离线盘库导入" />
        <telerik:RadAjaxPanel ID="panel" runat="server" CssClass="container-fluid text-center" LoadingPanelID="loading">
            <div class="row">
                <div class="col-md-12 text-center">
                    <input type="hidden" runat="server" id="h" value="" />
                    <div>
                        请选择离线盘库文件
                    </div>
                    <div style="margin-left: 50px;">
                        <telerik:RadAsyncUpload RegisterWithScriptManager="True" RenderMode="Mobile" runat="server" ID="im_up" Skin="MetroTouch" OnFileUploaded="im_up_FileUploaded" HideFileInput="False" LocalizationPath="~/Language" TemporaryFolder="~/Common/物资/临时" TargetFolder="~/Common/物资/临时" PostbackTriggers="im_ok" ChunkSize="1048576" AutoAddFileInputs="False" MaxFileInputsCount="1" InitialFileInputsCount="1" />
                        <input type="hidden" id="file" runat="server" />
                    </div>
                </div>
            </div>
            <div class="row">&nbsp;</div>
            <div class="row" style="display: none;">
                <div class="col-md-12 text-center">
                        <telerik:RadButton ID="im_do" CssClass="btn btn-primary" runat="server" Text="预览要导入的盘库数据" OnClick="im_do_Click"></telerik:RadButton>
                    <div style="margin: 20px 50px;">
                        <div id="r" runat="server"></div>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-md-12">
                    <div>
                        <telerik:RadButton ID="im_ok" CssClass="btn btn-primary" runat="server" Text="开始导入离线盘库数据" OnClick="im_ok_Click"></telerik:RadButton>
                    </div>
                </div>
            </div>
        </telerik:RadAjaxPanel>
    </form>
</body>
</html>
