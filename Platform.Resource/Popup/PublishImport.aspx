<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PublishImport.aspx.cs" Inherits="Popup.PopPublishImport" %>

<!DOCTYPE html>
<link href="../Style/common.css" rel="stylesheet" />

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>导入</title>
    <script src="../Content/jQuery/jquery.min.js"></script>
</head>
<body>
    <form id="form" runat="server">
        <div style="margin: 10px 0px;">
            <telerik:RadScriptManager runat="server" ID="sm"></telerik:RadScriptManager>
        </div>
        <telerik:RadAjaxLoadingPanel runat="server" ID="popup_publish_import_loading"></telerik:RadAjaxLoadingPanel>

        <telerik:RadAjaxPanel runat="server" ID="popup_publish_import_panel" LoadingPanelID="popup_publish_import_loading">
            <div style="margin: 10px 0px;">
                <label runat="server" id="popup_publish_import_label"></label>
            </div>
            <div style="margin: 10px 0px;">
                <label runat="server" id="popup_publish_import_sf_label"></label>
            </div>
            <div>
                <div style="margin: 10px 0px;">上传的文件前若出现<img alt="" src="../Image/img/Dot.png" />为格式或大小错误，请重新选择其他文件</div>
                <telerik:RadCodeBlock runat="server">
                    <script>
                        function ssssssss(sender, args) {
                            $("#<%= publish_import_commit.ClientID %>").css("display", "none");
                        }

                        function uuuuuuuu(sender, args) {
                            $("#<%= publish_import_commit.ClientID %>").css("display", "");
                        }

                        function rrrrrrrr(sender, args) {
                            $("#<%= publish_import_commit.ClientID %>").css("display", "none");
                        }
                    </script>
                </telerik:RadCodeBlock>
                <telerik:RadAsyncUpload RegisterWithScriptManager="True" runat="server" ID="publish_import_upload" OnClientFileSelected="ssssssss" OnClientFileUploadRemoved="rrrrrrrr" OnClientFileUploaded="uuuuuuuu" OnFileUploaded="publish_import_upload_OnFileUploaded" PostbackTriggers="publish_import_commit" HideFileInput="False" LocalizationPath="~/Language" TemporaryFolder="~/Temp" ChunkSize="1048576" AutoAddFileInputs="False" MaxFileInputsCount="1" InitialFileInputsCount="1" />
            </div>
            <div>
                <div style="margin: 10px 0px;">
                    <label>2、转换并导入（上传文件将自动转换格式并导入，转换过程中请勿关闭窗口）</label>
                </div>
            </div>
            <div style="width: 100%; text-align: center;">
                <a runat="server" id="publish_import_commit" onserverclick="publish_import_commit_OnServerClick" class="srx-ns-btn" style="margin: auto; padding: 4px; cursor: pointer; float: none; width: 150px; height: 32px; display: none;">转换并导入</a>
            </div>
        </telerik:RadAjaxPanel>

        <script>
            function GetRadWindow() {
                var oWindow = null;
                if (window.radWindow) oWindow = window.radWindow;
                else if (window.frameElement.radWindow) oWindow = window.frameElement.radWindow;
                return oWindow;
            }

            function RadClose() {
                GetRadWindow().close();
            }
        </script>
    </form>
</body>
</html>
