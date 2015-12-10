<%@ Control Language="C#" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI.Editor" TagPrefix="tools" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI.Widgets" TagPrefix="widgets" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI.Dialogs" TagPrefix="dialogs" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<script type="text/javascript">
	//<![CDATA[
	Type.registerNamespace("Telerik.Web.UI.Widgets");

	Telerik.Web.UI.Widgets.ImageProperties = function (element) {
		Telerik.Web.UI.Widgets.ImageProperties.initializeBase(this, [element]);
		this._clientParameters = null;
		this._allowedASCII = new Array(8, 16, 35, 36, 37, 39, 45, 46);
		this._constrainDimentions = true;
		this._ratio = 0;

		//this is used when we try to load an image before the dialog is initialized
		this._initialImage = null;
	}

	Telerik.Web.UI.Widgets.ImageProperties.prototype = {
		initialize: function () {
			Telerik.Web.UI.Widgets.ImageProperties.callBaseMethod(this, 'initialize');
			this._setupChildren();
			this.showThumbRow(false);
			this._originalImage = null;
			this._initialImage = null;
			this._clientInitPassed = false;
		},

		dispose: function () {
			this._disposeChildren();
			Telerik.Web.UI.Widgets.ImageProperties.callBaseMethod(this, 'dispose');
		},

		clientInit: function (clientParameters) {
			this._colors = clientParameters.Colors != null ? clientParameters.Colors : [];
			this._editor = clientParameters.editor;
			this._cssClasses = clientParameters.CssClasses != null ? clientParameters.CssClasses : [];
			this._clientInitPassed = true;

			this._initDropDowns();

			if (this._imageSrc) {
				this._imageSrc.set_editor(this._editor);
			}
			this._cleanImageAttributes();

			if (!clientParameters.get_value) {
				return;
			}
			var originalImage = null;

			if (this._initialImage != null) {
				originalImage = this._initialImage;
				this._initialImage = null;
			}
			else {
				try {
					//prevent"Can't execude code from a freed script"
					originalImage = clientParameters.get_value();
				} catch (ex) { };
			}

			if (originalImage) {
				this.loadImageProperties(originalImage);
			}
		},

		_initDropDowns: function () {
			//Set colors to the color picker
			this._colorPicker.set_items(this._colors);
			this._colorPicker.set_addCustomColorText(localization["AddCustomColor"]);

			//Set css class names to the css dropdown and selects the one of the selected image if existing
			//localization
			this._imageCssClassList.set_showText(true);
			this._imageCssClassList.set_clearclasstext(localization["ClearClass"]);
			this._imageCssClassList.set_text(localization["ApplyClass"]);
			this._imageCssClassList.set_value(localization["ApplyClass"]);
			this._imageCssClassList.set_items(this._cssClasses);
		},

		_cleanImageAttributes: function () {
			this._originalImage = null;
			this._imageWidth.value = "";
			this._imageHeight.value = "";
			if (this._imageSrc) this._imageSrc.set_value("");
			this._imageSrcValue = null;
			this._imageAlt.value = "";
			this._imageTitle.value = "";
			this._imageLongDecs.value = "";
			this._imageAlignment.updateValue("", null);
			this._marginLeftSpinBox.set_value("");
			this._marginTopSpinBox.set_value("");
			this._marginRightSpinBox.set_value("");
			this._marginBottomSpinBox.set_value("");
			this._borderWidthSpinBox.set_value("");
			this._colorPicker.set_color("");
			this._imageCssClassList.updateValue("");
		},

		loadImageProperties: function (originalImage) {
			if (!this._clientInitPassed) {
				//wait for clientInit before loading the image
				this._initialImage = originalImage;
				return;
			}

			this._originalImage = originalImage;
			var currentImage = this._originalImage.cloneNode(true);

			var widthValue = this._getOriginalWidth(this._originalImage);
			this._imageWidth.value = (widthValue.indexOf("px") != -1) ? parseInt(widthValue) : widthValue; //strp px from the width value
			var heightValue = this._getOriginalHeight(this._originalImage);
			this._imageHeight.value = (heightValue.indexOf("px") != -1) ? parseInt(heightValue) : heightValue; //strp px from the width value
			//NEW: Support for %
			this._ratio = parseInt(this._imageWidth.value) / parseInt(this._imageHeight.value); //this._ratio = this._originalImage.offsetWidth / this._originalImage.offsetHeight;

			this._customSize = { width: this._getCustomWidth(this._originalImage), height: this._getCustomHeight(this._originalImage) };

			var currentSrc = currentImage.getAttribute("src", 2);
			if (this._imageSrc) {
				this._imageSrc.set_value(currentSrc);
				this._imageSrcValue = null;
			}
			else {
				this._imageSrcValue = currentSrc;
			}

			this._imageAlt.value = this._getAttribute(currentImage, "alt");
			this._imageTitle.value = this._getAttribute(currentImage, "title");
			this._imageLongDecs.value = this._getAttribute(currentImage, "longDesc");

			//TODO: This should be moved to a separate method - perhaps even a shared method because similar code is used in an editor filter and in the nodeinspectormodule
			var floatJSProperty = ($telerik.isIE) ? "styleFloat" : "cssFloat";
			var styleFloat = (typeof (currentImage.style[floatJSProperty]) == "undefined") ? "" : currentImage.style[floatJSProperty];
			var verticalAlign = (typeof (currentImage.style["verticalAlign"]) == "undefined") ? "" : currentImage.style["verticalAlign"];
			var alignValue = "";

			if (verticalAlign == "" && styleFloat != "") {
				switch (styleFloat) {
					case "left":
						alignValue = "left";
						break;
					case "right":
						alignValue = "right";
						break;
				}
			}
			if (styleFloat == "") {
				switch (verticalAlign) {
					case "top":
						alignValue = "top";
						break;
					case "middle":
						alignValue = "absmiddle";
						break;
					case "text-bottom":
						alignValue = "bottom";
						break;
				}
			}

			this._imageAlignment.updateValue(alignValue, null);

			if (currentImage.style.marginTop)
				this._marginTopSpinBox.set_value(currentImage.style.marginTop.replace("px", ""));
			else
				this._marginTopSpinBox.set_value("");

			if (currentImage.style.marginRight)
				this._marginRightSpinBox.set_value(currentImage.style.marginRight.replace("px", ""));
			else
				this._marginRightSpinBox.set_value("");

			if (currentImage.style.marginBottom)
				this._marginBottomSpinBox.set_value(currentImage.style.marginBottom.replace("px", ""));
			else
				this._marginBottomSpinBox.set_value("");

			if (currentImage.style.marginLeft)
				this._marginLeftSpinBox.set_value(currentImage.style.marginLeft.replace("px", ""));
			else
				this._marginLeftSpinBox.set_value("");

			var borderValue = parseInt(currentImage.style.borderWidth);
			if (isNaN(borderValue)) borderValue = "";
			if (!borderValue) {
				var borderAttributeValue = currentImage.getAttribute("border");
				if (borderAttributeValue) {
					borderValue = borderAttributeValue;
					currentImage.style.borderWidth = borderAttributeValue + "px";
					currentImage.style.borderStyle = "solid";
				}
			}
			currentImage.removeAttribute("border");
			this._borderWidthSpinBox.set_value(borderValue);

			var borderColor = currentImage.style.borderColor.toUpperCase();
			this._colorPicker.set_color(borderColor);

			if (currentImage.className != null && currentImage.className != "") {
				this._imageCssClassList.updateValue(currentImage.className);
			}
		},

		showThumbRow: function (toShow) {
			if (this._thumbRow) {
				if (toShow == false) {
					this._thumbRow.style.display = "none";
				}
				else {
					this._thumbRow.style.display = "";
					if (this._chkThumb) this._chkThumb.checked = false;
					var chkNewWindow = $get("chkNewWindow");
					if (chkNewWindow) chkNewWindow.disabled = true;
				}
			}
		},

		isThumbLinkChecked: function () {
			return this._thumbRow && this._chkThumb && this._chkThumb.checked;
		},

		isThumbNewWindowChecked: function () {
			if (!this._thumbRow) return false;
			var chkNewWindow = $get("chkNewWindow");
			return chkNewWindow && chkNewWindow.checked;
		},

		_chkThumbClickHandler: function () {
			if (this._thumbRow && this._chkThumb) {
				var chkNewWindow = $get("chkNewWindow");
				chkNewWindow.disabled = !this._chkThumb.checked;
			}
		},

		_getOriginalWidth: function (currentImage) {
			if (!currentImage) return "";

			var width = this._getCustomWidth(currentImage) || currentImage.getAttribute("width") || currentImage.width || currentImage.offsetWidth || "";

			return width == null ? "" : width + "";
		},
		_getCustomWidth: function (currentImage) {
			if (!currentImage || !currentImage.style) return "";

			return currentImage.style.width || (!$telerik.isIE ? currentImage.getAttribute("width") : "") || "";
		},

		_getOriginalHeight: function (currentImage) {
			if (!currentImage) return "";

			var height = this._getCustomHeight(currentImage) || currentImage.getAttribute("height") || currentImage.height || currentImage.offsetHeight || "";

			return height == null ? "" : height + "";
		},
		_getCustomHeight: function (currentImage) {
			if (!currentImage || !currentImage.style) return "";

			return currentImage.style.height || (!$telerik.isIE ? currentImage.getAttribute("height") : "") || "";
		},

		_getImageOriginalSize: function (img) //NEW gets original image size
		{
			var tempImg = new Image();
			tempImg.src = img.getAttribute ? img.getAttribute("src") : img.src;
			var size = {
				width: tempImg.width,
				height: tempImg.height
			};
			return size;
		},

		_clearImgeDimensions: function (image) //NEW clear image's style size properties if they concur with the original image size.
		{
			var imageOriginalSize = this._getImageOriginalSize(image);

			var imageOriginalWidth = imageOriginalSize.width;
			var imageCurrentWidth = this._getOriginalWidth(image);
			if (imageCurrentWidth.indexOf("px") != -1) imageCurrentWidth = parseInt(imageCurrentWidth); //remove the px unit

			var imageOriginalHeight = imageOriginalSize.height;
			var imageCurrentHeight = this._getOriginalHeight(image);
			if (imageCurrentHeight.indexOf("px") != -1) imageCurrentHeight = parseInt(imageCurrentHeight); //remove the px unit

			if (!this._customSize.width && (imageOriginalWidth == imageCurrentWidth && (imageCurrentHeight == 0 || imageOriginalHeight == imageCurrentHeight)))
				image.style["width"] = "";
			if (!this._customSize.height && (imageOriginalHeight == imageCurrentHeight && (imageCurrentWidth == 0 || imageOriginalWidth == imageCurrentWidth)))
				image.style["height"] = "";
		},

		getModifiedImage: function () {
			if (this._originalImage == null) {
				return null;
			}

			var resultImage = this._originalImage.cloneNode(true);

			//Make sure the image src attribute is set before the widht/height, as setting the src causes IE to add width and height attributes expicitly
			var srcValue = this._imageSrcValue ? this._imageSrcValue : this._imageSrc.get_value();
			this._setAttribute(resultImage, "src", srcValue);

			this._setDimensionAttribute(resultImage, "width", this._imageWidth.value);
			this._setDimensionAttribute(resultImage, "height", this._imageHeight.value);
			this._clearImgeDimensions(resultImage); //check if the current dimensions coincide with the original image size and remove the css width properties

			this._setAttribute(resultImage, "alt", this._imageAlt.value);
			this._setAttribute(resultImage, "title", this._imageTitle.value);
			this._setAttribute(resultImage, "longDesc", this._imageLongDecs.value);

			//image align
			this._setImgAlignStyle(resultImage, this._imageAlignment.getAlign());

			var marginTop = parseInt(this._marginTopSpinBox.get_value());
			resultImage.style.marginTop = (!isNaN(marginTop)) ? marginTop + "px" : "";

			var marginRight = parseInt(this._marginRightSpinBox.get_value());
			resultImage.style.marginRight = (!isNaN(marginRight)) ? marginRight + "px" : "";

			var marginBottom = parseInt(this._marginBottomSpinBox.get_value());
			resultImage.style.marginBottom = (!isNaN(marginBottom)) ? marginBottom + "px" : "";

			var marginLeft = parseInt(this._marginLeftSpinBox.get_value());
			resultImage.style.marginLeft = (!isNaN(marginLeft)) ? marginLeft + "px" : "";

			var borderSize = parseInt(this._borderWidthSpinBox.get_value());
			if (!isNaN(borderSize) && borderSize >= 0) {
				resultImage.style.borderWidth = borderSize + "px";
				resultImage.style.borderStyle = "solid";
			}
			else {
				resultImage.style.borderWidth = "";
				resultImage.style.borderStyle = "";
			}
			resultImage.removeAttribute("border");

			if (this._colorPicker.get_color()) {
				resultImage.style.borderColor = this._colorPicker.get_color();
			}

			this._setClass(resultImage, this._imageCssClassList);
			return resultImage;
		},

		_setImgAlignStyle: function (img, align) {
			var floatJSProperty = ($telerik.isIE) ? "styleFloat" : "cssFloat";
			switch (align) {
				case "left":
					img.style[floatJSProperty] = "left";
					img.style["verticalAlign"] = "";
					break;
				case "right":
					img.style[floatJSProperty] = "right";
					img.style["verticalAlign"] = "";
					break;
				case "top":
					img.style[floatJSProperty] = "";
					img.style["verticalAlign"] = "top";
					break;
				case "bottom":
					img.style[floatJSProperty] = "";
					img.style["verticalAlign"] = "text-bottom";
					break;
				case "absmiddle":
					img.style[floatJSProperty] = "";
					img.style["verticalAlign"] = "middle";
					break;
				default:
					img.style[floatJSProperty] = "";
					img.style["verticalAlign"] = "";
					break;
			}
			img.removeAttribute("align");
		},

		_getAttribute: function (image, attributeName) {
			var attributeValue = "";
			if (image.getAttribute(attributeName)) {
				attributeValue = image.getAttribute(attributeName);
			}
			return attributeValue;
		},

		_setAttribute: function (image, attributeName, attributeValue) {
			if (attributeValue.trim()) {
				image.setAttribute(attributeName, attributeValue);
			}
			else {
				image.removeAttribute(attributeName, false);
			}
		},

		_setClass: function (element, cssClassHolder) {
			if (cssClassHolder.get_value() == "") {
				element.removeAttribute("className");
			}
			else {
				element.className = cssClassHolder.get_value();
			}
		},

		_setupChildren: function () {
			this._imageWidth = $get("ImageWidth");
			this._imageHeight = $get("ImageHeight");
			this._constrainButton = $get("ConstrainButton");
			this._colorPicker = $find("BorderColor");
			this._imageAlignment = $find("ImageAlignment");
			this._imageAlignment.setTagName("IMG");

			this._imageAlt = $get("ImageAlt");
			this._imageTitle = $get("ImageTitle");
			this._imageLongDecs = $get("ImageLongDesc");
			this._imageSrc = $find("ImageSrc");
			this._marginTopSpinBox = $find("marginTop");
			this._marginRightSpinBox = $find("marginRight");
			this._marginBottomSpinBox = $find("marginBottom");
			this._marginLeftSpinBox = $find("marginLeft");
			this._borderWidthSpinBox = $find("ImageBorderWidth");
			this._imageCssClassList = $find("ImageCssClass");
			this._insertButton = $get("IPInsertButton");
			if (this._insertButton) this._insertButton.title = localization["OK"];
			this._cancelButton = $get("IPCancelButton");
			if (this._cancelButton) this._cancelButton.title = localization["Cancel"];
			this._thumbRow = $get("thumbRow");
			if (this._thumbRow) {
				this._chkThumb = $get("chkThumb");
				if (this._chkThumb) {
					$addHandlers(this._chkThumb, { "click": this._chkThumbClickHandler }, this);
				}
			}

			this._initializeChildEvents();
		},

		_disposeChildren: function () {
			if (this._imageWidth) $clearHandlers(this._imageWidth);
			this._imageWidth = null;
			if (this._imageHeight) $clearHandlers(this._imageHeight);
			this._imageHeight = null;
			if (this._constrainButton) $clearHandlers(this._constrainButton);
			this._constrainButton = null;
			if (this._chkThumb) $clearHandlers(this._chkThumb);
			this._chkThumb = null;
			if (this._insertButton) $clearHandlers(this._insertButton);
			this._insertButton = null;
			if (this._cancelButton) $clearHandlers(this._cancelButton);
			this._cancelButton = null;
			this._thumbRow = null;
		},

		_initializeChildEvents: function () {
			$addHandlers(this._imageWidth, { "keyup": this._validateDimensionByWidth }, this);
			$addHandlers(this._imageWidth, { "keydown": this._validateNumber }, this);
			$addHandlers(this._imageHeight, { "keyup": this._validateDimensionByHeight }, this);
			$addHandlers(this._imageHeight, { "keydown": this._validateNumber }, this);
			$addHandlers(this._constrainButton, { "click": this._constrainClickHandler }, this);
			if (this._insertButton) $addHandlers(this._insertButton, { "click": this._insertClickHandler }, this);
			if (this._cancelButton) $addHandlers(this._cancelButton, { "click": this._cancelClickHandler }, this);
			if (this._imageSrc) {
				this._imageSrc.add_valueSelected(Function.createDelegate(this, this._imageSrcValueSelected));
			}
		},

		_imageSrcValueSelected: function (sender, args) {
			var editor = this._editor;
			var doc;
			if (editor && editor.get_document()) {
				doc = editor.get_document();
			}
			else {
				doc = document;
			}
			var img = doc.createElement("img");
			img.setAttribute("src", sender.get_value());
			this.loadImageProperties(img);

		},

		_setDimensionAttribute: function (image, attributeName, size) {
			image.removeAttribute(attributeName);
			var bRamoveStyleProperty = false;
			if (!size) {
				//remove style property if size is empty
				if (image.style.removeAttribute) image.style.removeAttribute(attributeName, false);
				else image.style[attributeName] = "";
			}
			else {
				//NEW: Provide support for % as well, not just px
				var unit = $telerik.parseUnit(size);
				image.style[attributeName] = unit.size + unit.type;
			}
		},

		_validateDimensionByWidth: function (e) {
			this._validateDimension(e, "width");
		},

		_validateDimensionByHeight: function (e) {
			this._validateDimension(e, "height");
		},

		_constrainClickHandler: function (e) {
			this._constrainDimentions = !this._constrainDimentions;

			if (this._constrainDimentions) {
				//Update the readings on the gauges
				//this._updateConstraintGauges(); there is no need to update the heiht on click of the this._constrainButton
				Sys.UI.DomElement.addCssClass(this._constrainButton.parentNode, "redConstrainToggle");
			}
			else {
				Sys.UI.DomElement.removeCssClass(this._constrainButton.parentNode, "redConstrainToggle");
			}

			//Cancel the postback that the button causes in FF
			return $telerik.cancelRawEvent(e);
		},

		_updateConstraintGauges: function (attributeName) {
			//If no attributeName is specified, the "width" is assumed
			var useWidth = (attributeName != "height");

			var dependantControl = null;
			var rulingControl = null;
			var ratio = 0;

			if (useWidth) {
				dependantControl = this._imageHeight;
				rulingControl = this._imageWidth;
				ratio = 1 / this._ratio;
			}
			else {
				dependantControl = this._imageWidth;
				rulingControl = this._imageHeight;
				ratio = this._ratio;
			}
			var rulingControlValue = rulingControl.value;
			var newValue = "";
			if (rulingControlValue) {
				//Support %, not just px
				var rulingUnit = $telerik.parseUnit(rulingControlValue);
				var size = rulingUnit.size;

				//Set the value
				newValue = Math.ceil(size * ratio);
				var rulingUnitType = rulingUnit.type;
				if (rulingUnitType != "px") newValue += rulingUnitType;
			}
			dependantControl.value = newValue;
		},

		_validateDimension: function (e, attributeName) {
			if (!this._validateNumber(e))
				return false;

			if (this._constrainDimentions)
				this._updateConstraintGauges(attributeName);

			return true;
		},

		_validateNumber: function (e) {
			if (window.event != null) e = window.event;

			if (((e.keyCode >= 48) && (e.keyCode <= 57)) ||
				((e.keyCode >= 96) && (e.keyCode <= 105)) ||
				(Array.contains(this._allowedASCII, e.keyCode))) {
				return true;
			}
			else {
				return $telerik.cancelRawEvent(e);
			}
		},

		_insertClickHandler: function (e) {
			var image = this.getModifiedImage();
			if (image) {
				var arg = new Telerik.Web.UI.EditorCommandEventArgs("SetImageProperies", null, image)
				Telerik.Web.UI.Dialogs.CommonDialogScript.get_windowReference().close(arg);
			}
			else $telerik.cancelRawEvent(e); //cancel event if there is no image to return
		},

		_cancelClickHandler: function (e) {
			Telerik.Web.UI.Dialogs.CommonDialogScript.get_windowReference().close();
		},

		get_imageWidth: function () {
			return this._imageWidth;
		},

		get_imageHeight: function () {
			return this._imageHeight;
		},

		get_ratio: function () {
			return this._ratio;
		},

		set_ratio: function (value) {
			this._ratio = value;
		}
	}

	Telerik.Web.UI.Widgets.ImageProperties.registerClass('Telerik.Web.UI.Widgets.ImageProperties', Telerik.Web.UI.RadWebControl, Telerik.Web.IParameterConsumer);
	//]]>
</script>
<div class="redWrapper">
	<div class="redSection redSectionTop">
		<div class="redRow">
			<span class="redLabel">
					<script type="text/javascript">
						document.write(localization["ImageSrc"]);
					</script>
			</span>
			<div class="redInlineBlock redMiddle">
				<tools:ImageDialogCaller ID="ImageSrc" runat="server" />
			</div>
		</div>

		<div class="redRow">
			<div class="redConstrainWrapper redConstrainToggle">
				<label class="redLabel" for="ImageWidth">
					<script type="text/javascript">document.write(localization["Width"]);</script>
				</label>
				<input type="text" id="ImageWidth" /> <span class="redDimensionPixels">px</span>

				<button id="ConstrainButton" class="redConstrainButton">&nbsp;</button>

				<label class="redLabel redSecondLabel" for="ImageHeight">
					<script type="text/javascript">document.write(localization["Height"]);</script>
				</label>
				<input type="text" id="ImageHeight" /> <span class="redDimensionPixels redRtlFix">px</span>
			</div>
		</div>

		<div class="redRow">
			<span class="redLabel">
				<script type="text/javascript">
						document.write(localization["ImageAlignment"]);
					</script>
			</span>
			<div class="redInlineBlock redMiddle">
				<tools:AlignmentSelector ID="ImageAlignment" runat="Server" />
			</div>
		</div>

		<div class="redRow">
			<span class="redLabel">
				<script type="text/javascript">
					document.write(localization["BorderColor"]);
				</script>
			</span>
			<div class="redInlineBlock redMiddle">
				<tools:ColorPicker ID="BorderColor" runat="Server" />
			</div>

			<span class="redLabel redSecondLabelAfterTool">
				<script type="text/javascript">
					document.write(localization["BorderWidth"]);
				</script>
			</span>
			<div class="redInlineBlock redMiddle">
				<tools:EditorSpinBox ID="ImageBorderWidth" runat="server" />
			</div>
			<span class="redDimensionPixels">px</span>
		</div>

		<div class="redRow">
			<span class="redLabel">
				<script type="text/javascript">
					document.write(localization["Top"]);
				</script>
			</span>
			<div class="redInlineBlock redMiddle">
				<tools:EditorSpinBox ID="marginTop" runat="server" />
			</div>
			 <span class="redDimensionPixels">px</span>
			<span class="redLabel redSecondLabel">
				<script type="text/javascript">
					document.write(localization["Bottom"]);
				</script>
			</span>
			<div class="redInlineBlock redMiddle">
				<tools:EditorSpinBox ID="marginBottom" runat="server" />
			</div>
			<span class="redDimensionPixels">px</span>
		</div>

		<div class="redRow">
			<span class="redLabel">
				<script type="text/javascript">
					document.write(localization["Right"]);
				</script>
			</span>
			<div class="redInlineBlock redMiddle">
				<tools:EditorSpinBox ID="marginRight" runat="server" />
			</div>
			<span class="redDimensionPixels">px</span>
			<span class="redLabel redSecondLabel">
				<script type="text/javascript">
					document.write(localization["Left"]);
				</script>
			</span>
			<div class="redInlineBlock redMiddle">
				<tools:EditorSpinBox ID="marginLeft" runat="server" />
			</div>
			<span class="redDimensionPixels">px</span>
		</div>

		<div class="redRow">
			<label class="redLabel" for="ImageAlt">
				<script type="text/javascript">
					document.write(localization["ImageAltText"]);
				</script>
			</label>
			<input type="text" id="ImageAlt" />
		</div>
		<div class="redRow">
			<label class="redLabel" for="ImageTitle">
				<script type="text/javascript">
					document.write(localization["ImageTitleText"]);
				</script>
			</label>
			<input type="text" id="ImageTitle" />
		</div>

		<div class="redRow">
			<label class="redLabel" for="ImageLongDesc">
				<script type="text/javascript">
					document.write(localization["LongDescription"]);
				</script>
			</label>
			<input type="text" id="ImageLongDesc" />
		</div>

		<div class="redRow">
			<label class="redLabel" for="">
				<script type="text/javascript">
					document.write(localization["CssClass"]);
				</script>
			</label>
			<div class="redInlineBlock redMiddle">
				<tools:ApplyClassDropDown ID="ImageCssClass" runat="Server" />
			</div>
		</div>

		<div class="redRow" id="thumbRow" runat="server" visible="false">
			<div class="redCheckRadioWrapper">
				<input type="checkbox" id="chkThumb" name="chkThumb" />
				<label class="redLabel" for="chkThumb">
						<script type="text/javascript">document.write(localization["LinkToOriginal"]);</script>
				</label>
			</div>

			<div class="redCheckRadioWrapper">
				<input type="checkbox" id="chkNewWindow" name="chkNewWindow" />
				<label class="redLabel" for="chkNewWindow">
					<script type="text/javascript">document.write(localization["OpenInNewWindow"]);</script>
				</label>
			</div>

		</div>

		<%--<div class="redRow"  runat="server" visible="false">
			
		</div>--%>
	</div>

	<div class="redActionButtonsWrapper redActionButtonsAbsoluteWrapper" id="controlButtonsRow" runat="server">
		<button type="button" id="IPInsertButton">
			<script type="text/javascript">
				setInnerHtml("IPInsertButton", localization["OK"]);
			</script>
		</button>
		<button type="button" id="IPCancelButton">
			<script type="text/javascript">
				setInnerHtml("IPCancelButton", localization["Cancel"]);
			</script>
		</button>
	</div>
</div>