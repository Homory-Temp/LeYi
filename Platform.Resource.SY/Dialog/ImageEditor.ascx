<%@ Control Language="C#" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI.Editor" TagPrefix="tools" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI.Widgets" TagPrefix="widgets" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI.Dialogs" TagPrefix="dialogs" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<script type="text/javascript">
	var $ = $telerik.$;
	var $T = Telerik.Web.UI;

	Type.registerNamespace("Telerik.Web.UI.Widgets");

	Telerik.Web.UI.Widgets.ImageEditor = function (element)
	{
		Telerik.Web.UI.Widgets.ImageEditor.initializeBase(this, [element]);
		this._clientParameters = null;
		this._ajaxPanel = null;
		this._imageEditor = null;
		this._originalImageLocation = null;
		this._saveDataControl = null;

		this._suffix = "_thumb";
		this._name = "";
		this._src = "";
	}

	Telerik.Web.UI.Widgets.ImageEditor.prototype =
{
	initialize: function ()
	{
		Telerik.Web.UI.Widgets.ImageEditor.callBaseMethod(this, "initialize");
		this.setupChildren();
		//Intialize ajax panel handlers
		this._attachAjaxPanelHandlers();
	},

	setupChildren: function ()
	{
		this._imageEditor = $find("RadImageEditor1");
		this._ajaxPanel = $find("RadAjaxPanel1");
		this._originalImageLocation = $get("OriginalImageLocation");
		this._saveDataControl = $get("SaveData");
		this._newImageNameControl = $get("NewImageName");
		this._overrideExistingControl = $get("OverwriteExistingCheckBox");

		//Save button
		this._saveButton = $get("saveThumbnail");
		this._saveButton.title = localization["Save"];
		$addHandlers(this._saveButton, { "click": this._saveButtonClickHandler }, this);
		this._cancelButton = $get("CancelButton");
		this._cancelButton.title = localization["Cancel"];
	},

	_attachAjaxPanelHandlers: function ()
	{
		Sys.WebForms.PageRequestManager.getInstance().add_endRequest(Function.createDelegate(this, this._onAjaxResponseEnd));
	},

	_onAjaxResponseEnd: function ()
	{
		this.set_imageEditor($find("RadImageEditor1"));
		this._attachImageEditorHandlers();
	},

	_attachImageEditorHandlers: function()
	{
		var imageEditor = this.get_imageEditor();
		if (imageEditor && imageEditor.useCanvas())
		{
			imageEditor.add_saved(Function.createDelegate(this, this._onClientImageSaved));
		}
	},

	_onClientImageSaved: function (sender, args)
	{
		if (args.get_isSaved())
		{
			//this code is generated on the server if CanvasMode is not used
			Telerik.Web.UI.Dialogs.CommonDialogScript.get_windowReference().close({
				_newImageSrc: args.get_fileName(),
				get_newImageSrc: function ()
				{
					return this._newImageSrc;
				}
			});
		}
	},

	clientInit: function (clientParameters)
	{
		this._clientParameters = clientParameters;
		this._name = clientParameters.name;
		if (clientParameters.suffix != null)
		{
			this.set_suffix(clientParameters.suffix);
		}
		//Reset SaveData field value
		this._saveDataControl.value = "";
		this.set_src(clientParameters.imageSrc);
		this._resetNewImageName();
	},

	_resetNewImageName: function ()
	{
		var src = this.get_src();

		//Set the proposed thumbname in the name input
		var newName = this._name ? this._name : src.substring(src.lastIndexOf("/") + 1);
		var dotPos = newName.lastIndexOf(".");
		if (dotPos != -1)
		{
			$get("NewImageExt").innerHTML = newName.substring(dotPos);
			newName = newName.substring(0, dotPos);
		}
		newName += this.get_suffix();
		this.set_newImageName(newName);
	},

	_saveButtonClickHandler: function (e)
	{
		var separator = ":";

		var name = this.get_newImageName();// +$get("NewImageExt").innerHTML;
		if (!name || name.indexOf(separator) != -1)
		{
			alert(localization["MessageCannotWriteToFolder"]);
			return;
		}

		var imgEditor = this.get_imageEditor();
		if (imgEditor && imgEditor.useCanvas())
		{
			imgEditor.saveImageOnServer(name, this._overrideExistingControl.checked);
		}
		else
		{
			var sb = new Sys.StringBuilder("");
			sb.append(this._overrideExistingControl.checked ? "true" : "false");
			sb.append(separator);
			sb.append(name);

			this._saveDataControl.value = sb.toString();
			this._ajaxPanel.ajaxRequest();
		}
	},

	dispose: function ()
	{
		this.clearHandlers();
		this._imageEditor = null;
		this._ajaxPanel = null;
		this._originalImageLocation = null;
		this._saveDataControl = null;
		this._saveButton = null;
		this._cancelButton = null;
		Telerik.Web.UI.Widgets.ImageEditor.callBaseMethod(this, "dispose");
	},

	clearHandlers: function ()
	{
		//Save button - could have remained from earlier phase, prior to AJAX
		if (this._saveButton) $clearHandlers(this._saveButton);
	},

	//================================== Properties ================================================//
	get_src: function ()
	{
		//Get src from the hidden field
		return this._src;
	},

	set_src: function (src)
	{
		//Set path in the hidden field and if not available, use src
		this._src = src;
		var field = this._originalImageLocation;
		var oldValue = field.value;
		field.setAttribute("value", src);
		
		//Set the src of the image in RadImageEditor. This makes the next ajax request invisible for the user.
		this.set_previewImageSrc(src);
		//Send an ajax request the ImageUrl property of RadImageEditor gets initialized on the server.
		this._ajaxPanel.ajaxRequest();
	},


	set_previewImageSrc: function (value)
	{
		var previewImage = this.get_editableImage();
		if (previewImage)
		{
			previewImage.src = value;
		}
	},

	get_editableImage: function ()
	{
		var editableImage = this.get_imageEditor().getEditableImage();
		var image = editableImage.getImage();
		if (typeof (image) == "undefined") image = null;
		return image;
	},

	get_imageEditor: function ()
	{
		if (!this._imageEditor || typeof (this._imageEditor.getEditableImage().getImage()) == "undefined")
		{
			this._imageEditor = $find("RadImageEditor1");
		}
		return this._imageEditor;
	},

	set_imageEditor: function (value)
	{
		this._imageEditor = value;
	},

	get_newImageName: function ()
	{
		return this._newImageNameControl.value;
	},

	set_newImageName: function (value)
	{
		this._newImageNameControl.value = value;
	},

	get_suffix: function ()
	{
		var suffix = this._suffix;
		return suffix ? suffix : "";
	},

	set_suffix: function (suffix)
	{
		this._suffix = suffix;
	}
}

	Telerik.Web.UI.Widgets.ImageEditor.registerClass("Telerik.Web.UI.Widgets.ImageEditor", Telerik.Web.UI.RadWebControl, Telerik.Web.IParameterConsumer);
</script>
<style type="text/css">
	/* Hide RadImageEdior borders*/
	div.RadImageEditor
	{
		border: 0;
		padding: 0;
	}

	div.rieContentArea 
	{
		border: 0;
	}

	div.rieStatusBar
	{
		padding-left: 5px;
	}
	*html #RadAjaxPanel1Panel
	{
		overflow:hidden;
	}
	*+html #RadAjaxPanel1Panel
	{
		overflow:hidden;
	}
</style>


<div>
	<input type="hidden" id="OriginalImageLocation" name="OriginalImageLocation" />
	<input type="hidden" id="SaveData" name="SaveData" />
	<telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server">
		<telerik:RadImageEditor ID="RadImageEditor1" runat="server" Height="450" Width="816" EnableResize="false" >
			<Tools>
				<telerik:ImageEditorToolGroup>
					<telerik:ImageEditorTool CommandName="Print" />
					<telerik:ImageEditorToolSeparator />
					<telerik:ImageEditorToolStrip CommandName="Undo" />
					<telerik:ImageEditorToolStrip CommandName="Redo" />
					<telerik:ImageEditorTool CommandName="Reset" />
					<telerik:ImageEditorToolSeparator />
					<telerik:ImageEditorTool CommandName="Crop" IsToggleButton="true"/>
					<telerik:ImageEditorTool CommandName="Resize" IsToggleButton="true"/>
					<telerik:ImageEditorTool CommandName="Zoom" IsToggleButton="true"/>
					<telerik:ImageEditorTool CommandName="ZoomIn"/>
					<telerik:ImageEditorTool CommandName="ZoomOut"/>
					<telerik:ImageEditorTool CommandName="Opacity" IsToggleButton="true"/>
					<telerik:ImageEditorTool CommandName="RotateRight" />
					<telerik:ImageEditorTool CommandName="RotateLeft" />
					<telerik:ImageEditorTool CommandName="FlipVertical" />
					<telerik:ImageEditorTool CommandName="FlipHorizontal" />
					<telerik:ImageEditorTool CommandName="AddText" IsToggleButton="true" />
				</telerik:ImageEditorToolGroup>
			</Tools>
		</telerik:RadImageEditor>
	</telerik:RadAjaxPanel>
</div>
<table cellpadding="0" cellspacing="0" style="width: 100%;padding: 10px 0 5px 0;">
	<tr>
		<td class="redBtnAlignment">
			<label for="NewImageName">
				<script type="text/javascript">
					document.write(localization["SaveAs"]);
				</script>
			</label>
		</td>
		<td>
			<input type="text" id="NewImageName" name="NewImageName" style="margin-left: 4px; width: 260px;" /><span id="NewImageExt">&nbsp;</span>
		</td>
		<td>&nbsp;</td>
	</tr>
	<tr>
		<td>&nbsp;</td>
		<td colspan="2">
			<input checked="checked" type="checkbox" id="OverwriteExistingCheckBox" style="margin-left: "/>
			<label for="OverwriteExistingCheckBox">
				<script type="text/javascript">
					document.write(localization["OverwriteExisting"]);
				</script>
			</label>
		</td>
		<td class="redBtnAlignment">
			<button type="button" id="saveThumbnail" style="width: 100px;">
				<script type="text/javascript">
					setInnerHtml("saveThumbnail", localization["Save"]);
				</script>
			</button>
			<button type="button" id="CancelButton" onclick="Telerik.Web.UI.Dialogs.CommonDialogScript.get_windowReference().close();" style="width: 100px;">
				<script type="text/javascript">
					setInnerHtml("CancelButton", localization["Cancel"]);
				</script>
			</button>
		</td>
	</tr>
</table>
