<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PublishAttachment.aspx.cs" Inherits="Popup.PopupPublishAttachment" %>

<link href="../Style/common.css" rel="stylesheet" />

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
	<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
	<title>附件</title>
	<style>
		.relPos {
			position: relative;
		}
	</style>
</head>
<body>
	<form id="form" runat="server">
		<telerik:RadScriptManager runat="server" ID="sm"></telerik:RadScriptManager>
		<telerik:RadAjaxPanel runat="server" ID="popup_publish_attachment_panel">
			<div style="margin:10px 0px;">上传文件不得超过100MB，格式仅限图片、Office文档、文本文档、PDF文档、压缩包、Flash文件和音视频文件（上传过程中请勿关闭窗口）</div>
			<div style="margin:10px 0px;">上传的文件前若出现<img alt="" src="../Image/img/Dot.png" />为格式或大小错误，请重新选择其他文件</div>
			<telerik:RadAsyncUpload RegisterWithScriptManager="True" runat="server" ID="publish_attachment_upload" OnFileUploaded="publish_attachment_upload_OnFileUploaded" PostbackTriggers="publish_attachment_commit" HideFileInput="False" LocalizationPath="~/Language" ChunkSize="1048576" MaxFileSize="1048576000" AutoAddFileInputs="False" CssClass="relPos" AllowedFileExtensions="jpg,jpeg,png,gif,bmp,rar,zip,7z,doc,docx,ppt,pptx,xls,xlsx,txt,rtf,pdf,swf,flv,mp4,mpg,mpeg,vob,avi,rm,rmvb,mp3,wav" InitialFileInputsCount="1" />
            <telerik:RadTextBox ID="remarkTextbox" runat="server" EmptyMessage="备注" Width="163px"></telerik:RadTextBox>
			<div style="width: 100%; text-align: center;"><a runat="server" id="publish_attachment_commit" onserverclick="publish_attachment_commit_OnServerClick" class="srx-ns-btn" style="margin: auto; padding: 4px; cursor: pointer; float: none; width: 150px; height: 32px;">保存</a></div>
		</telerik:RadAjaxPanel>
		<script>
			function GetRadWindow() {
				var oWindow = null;
				if (window.radWindow) oWindow = window.radWindow;
				else if (window.frameElement.radWindow) oWindow = window.frameElement.radWindow;
				return oWindow;
			}

			function RadCloseRebind() {
			    GetRadWindow().BrowserWindow.refreshAttachments();
			    GetRadWindow().close();
			}

			function RadClose() {
				GetRadWindow().close();
			}
		</script>
	</form>
</body>
</html>
