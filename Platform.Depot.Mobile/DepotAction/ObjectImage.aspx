<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ObjectImage.aspx.cs" Inherits="DepotAction_ObjectImage" %>

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
        <homory:SideBarSingle runat="server" ID="SideBarSingle" Crumb="物资管理 - 上传图片" />
        <telerik:RadCodeBlock ID="cb" runat="server">
            <script>
                function img_up(sender, e) {
                    $find("<%= ap.ClientID %>").ajaxRequest("Upload");
                }
            </script>
        </telerik:RadCodeBlock>
        <telerik:RadAjaxPanel ID="ap" runat="server" CssClass="container-fluid" LoadingPanelID="loading">
            <div class="row">
                <div class="col-md-6 text-left">
                    <telerik:RadAsyncUpload ID="upload" runat="server" LocalizationPath="~/Language" TemporaryFolder="~/Common/物资/临时" TargetFolder="~/Common/物资/图片" AllowedFileExtensions="bmp,gif,jpg,jpeg,png" MaxFileSize="10240000" MultipleFileSelection="Disabled" ManualUpload="false" MaxFileInputsCount="4" HideFileInput="true" OnClientFileUploaded="img_up" OnFileUploaded="upload_FileUploaded"></telerik:RadAsyncUpload>
                    <telerik:RadButton ID="clear" runat="server" Visible="false" Text="清除" Width="43" OnClick="clear_Click"></telerik:RadButton>
                </div>
                <div class="col-md-6 text-right">
                    <input type="button" class="btn btn-tumblr" id="go" runat="server" value="保存" onserverclick="go_ServerClick" />
                    <input type="button" class="btn btn-tumblr" id="cancel" runat="server" value="取消" onserverclick="cancel_ServerClick" />
                </div>
                <div class="col-md-12 text-left">
                    <div class="row" id="imgRow" runat="server" visible="false">
                        <div class="col-md-2 text-center">
                            <img id="p0" runat="server" src="../Content/Images/Transparent.png" class="img-responsive image-thumb-result storeObjThumb" onclick="window.open(this.src, '_blank');" />
                        </div>
                        <div class="col-md-2 text-center">
                            <img id="p1" runat="server" src="../Content/Images/Transparent.png" class="img-responsive image-thumb-result storeObjThumb" onclick="window.open(this.src, '_blank');" />
                        </div>
                        <div class="col-md-2 text-center">
                            <img id="p2" runat="server" src="../Content/Images/Transparent.png" class="img-responsive image-thumb-result storeObjThumb" onclick="window.open(this.src, '_blank');" />
                        </div>
                        <div class="col-md-2 text-center">
                            <img id="p3" runat="server" src="../Content/Images/Transparent.png" class="img-responsive image-thumb-result storeObjThumb" onclick="window.open(this.src, '_blank');" />
                        </div>
                    </div>
                </div>
            </div>
        </telerik:RadAjaxPanel>
    </form>
</body>
</html>
