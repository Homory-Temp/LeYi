<%@ Control Language="C#" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI.Widgets" TagPrefix="widgets" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI.Dialogs" TagPrefix="dialogs" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<script type="text/javascript">
	Type.registerNamespace("Telerik.Web.UI.Editor.DialogControls");

	Telerik.Web.UI.Editor.DialogControls.FileBrowser = function(element)
	{
		Telerik.Web.UI.Editor.DialogControls.FileBrowser.initializeBase(this, [element]);
	}

	Telerik.Web.UI.Editor.DialogControls.FileBrowser.prototype = {
		initialize: function()
		{
			this.set_insertButton($get("InsertButton"));
			this.set_cancelButton($get("CancelButton"));

			var previewer = this.get_previewerType();
			var previewerType = eval("Telerik.Web.UI.Widgets." + previewer);
			$create(previewerType, { "browser": this }, null, null, $get(previewer));
			this.set_filePreviewer($find(previewer));
			this.set_fileBrowser($find("RadFileExplorer1"));

			Telerik.Web.UI.Editor.DialogControls.FileBrowser.callBaseMethod(this, 'initialize');
		},

		dispose: function()
		{
			Telerik.Web.UI.Editor.DialogControls.FileBrowser.callBaseMethod(this, 'dispose');
			this._insertButton = null;
			this._cancelButton = null;
		}
	}

	Telerik.Web.UI.Editor.DialogControls.FileBrowser.registerClass('Telerik.Web.UI.Editor.DialogControls.FileBrowser', Telerik.Web.UI.Widgets.FileManager);
</script>
<div class="redWrapper redWrapperClean">
	<div class="redFBWrapper">
		<div class="redFEWrapper">
			<telerik:RadFileExplorer ID="RadFileExplorer1" Height="400px" Width="400px" TreePaneWidth="150px" runat="Server" EnableOpenFile="false" AllowPaging="true" PageSize="100" />
		</div>
		<div class="redDialogSeparator"></div>
		<div class="redFBDialogContent">
			<div class="redFBDialogContentContainer">
				<asp:PlaceHolder ID="PreviewerPlaceHolder" runat="server" />
			</div>
			<div class="redFBDialogContentButton redActionButtonsAbsoluteWrapper">
				<button type="button" id="InsertButton">
					<script type="text/javascript">setInnerHtml("InsertButton", localization["Insert"]);</script>
				</button>
				<button type="button" id="CancelButton">
					<script type="text/javascript">setInnerHtml("CancelButton", localization["Cancel"]);</script>
				</button>
			</div>
		</div>
	</div>
</div>