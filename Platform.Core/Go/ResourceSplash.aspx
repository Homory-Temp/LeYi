<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ResourceSplash.aspx.cs" Inherits="Go.GoResourceSplash" %>

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
    <script>

        (function (global, undefined) {
            function OnClientItemSelected(sender, args) {
                var pvwImage = $telerik.$("#pvwImage").get(0);
                var imageSrc = args.get_path();
                if (imageSrc.match(/\.(gif|jpg)$/gi)) {
                    pvwImage.src = imageSrc;
                    pvwImage.style.display = "inline-block";
                    pvwImage.alt = imageSrc.substring(imageSrc.lastIndexOf('/') + 1);
                }
                else {
                    pvwImage.style.display = "none";
                }
            }
            function OnClientItemDeleted(sender, args) {
                var pvwImage = $telerik.$("#pvwImage").get(0);
                pvwImage.style.display = "none";
            }
            global.OnClientItemSelected = OnClientItemSelected
            global.OnClientItemDeleted = OnClientItemDeleted
        })(window)
    </script>
    <!--[if lt IE 9]>
	    <script src="../Content/Homory/js/html5shiv.js"></script>
	    <script src="../Content/Homory/js/respond.min.js"></script>
    <![endif]-->
</head>
<body>
    <form id="formHome" runat="server">
        <telerik:RadScriptManager ID="sm" runat="server"></telerik:RadScriptManager>
        <telerik:RadAjaxLoadingPanel ID="loading" runat="server">
            <div>&nbsp;</div>
            <div class="btn btn-lg btn-warning" style="margin-top: 50px;">正在加载 请稍候....</div>
        </telerik:RadAjaxLoadingPanel>
        <telerik:RadAjaxPanel ID="panel" runat="server" CssClass="container" LoadingPanelID="loading">
            <div class="row">
                <div class="col-md-12 text-center">
                    <div class="btn btn-tumblr btn-lg">资源平台首页展示图图片设定</div>
                </div>
            </div>
            <div class="row">&nbsp;</div>
            <div class="row">
                <div class="col-md-6">
                    <telerik:RadFileExplorer runat="server" ID="exp" OnClientItemSelected="OnClientItemSelected" OnClientDelete="OnClientItemDeleted" LocalizationPath="~/Language" Width="100%" Height="240px" EnableCreateNewFolder="false" EnableOpenFile="false" ExplorerMode="Thumbnails" Font-Size="Large" VisibleControls="Toolbar,ListView">
                        <Configuration AllowMultipleSelection="false" EnableAsyncUpload="true" MaxUploadFileSize="2097152" />
                    </telerik:RadFileExplorer>
                </div>
                <div class="col-md-6">
                    <div style="width: 100%; height: 300px;">
                        <img id="pvwImage" src="#" alt="预览" class="img-responsive" style="display: none;" />
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-md-12 text-center">
                    <div class="btn btn-tumblr btn-lg">资源平台首页展示图链接设定</div>
                </div>
            </div>
            <div class="row">&nbsp;</div>
            <div class="row">
                <div class="col-md-12">
                    <telerik:RadTextBox ID="urls" runat="server" TextMode="MultiLine" Width="100%" Height="300px"></telerik:RadTextBox>
                </div>
                <div class="col-md-12">
                    &nbsp;
                </div>
                <div class="col-md-12 text-center">
                    <input id="save" runat="server" type="button" onserverclick="save_ServerClick" value="保存" class="btn btn-info" />
                </div>
            </div>
        </telerik:RadAjaxPanel>
    </form>
</body>
</html>
