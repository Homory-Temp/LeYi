<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AppIcon.aspx.cs" Inherits="Extended.ExtendedAppIcon" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=Edge,Chrome=1" />
    <meta name="viewport" content="width=device-width,initial-scale=1,minimum-scale=1,maximum-scale=1" />
    <title>选择应用Logo</title>
	<%-- ReSharper disable Html.PathError --%>
    <link href="../Content/Homory/css/common.css" rel="stylesheet" />
    <link href="../Content/Core/css/home.css" rel="stylesheet" />
    <link href="../Content/Core/css/common.css" rel="stylesheet" />
    <script src="../Content/jQuery/jquery.min.js"></script>
    <script src="../Content/Homory/js/common.js"></script>
    <script src="../Content/Homory/js/notify.min.js"></script>
    <script src="../Content/Core/js/home.js"></script>
	<%-- ReSharper restore Html.PathError --%>
</head>
<body style="margin: 0; padding: 0; overflow: hidden; text-align: center">
    <form id="formHome" runat="server">
        <telerik:RadScriptManager ID="scriptManager" runat="server"></telerik:RadScriptManager>
        <script type="text/javascript">
            function GetRadWindow() {
                var oWindow = null;
                if (window.radWindow) oWindow = window.radWindow;
                else if (window.frameElement.radWindow) oWindow = window.frameElement.radWindow;
                return oWindow;
            }

            function RadClose() {
                GetRadWindow().BrowserWindow.refreshGrid();
                GetRadWindow().close();
            }
        </script>
        <telerik:RadAjaxPanel ID="panelInner" runat="server">
            <telerik:RadAsyncUpload ID="uploadControl" runat="server" PostbackTriggers="uploadOk" Style="margin-top: 6px; margin-left: 6px;" AllowedFileExtensions="jpg,jpeg,png,gif" MultipleFileSelection="Disabled" Skin="MetroTouch" AutoAddFileInputs="false" Localization-Cancel="取消" Localization-Remove="移除" Localization-Select="选择" OnFileUploaded="upload_FileUploaded">
            </telerik:RadAsyncUpload>
            <telerik:RadButton ID="uploadOk" runat="server" Skin="MetroTouch" Text="保存"></telerik:RadButton>
        </telerik:RadAjaxPanel>
    </form>
</body>
</html>
