<%@ Control Language="C#" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI.Widgets" TagPrefix="widgets" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI.Dialogs" TagPrefix="dialogs" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI.Editor.DialogControls" TagPrefix="dc" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI.Editor" TagPrefix="tools" %>

<script type="text/javascript">
	Type.registerNamespace("Telerik.Web.UI.Widgets");

	Telerik.Web.UI.Widgets.InsertSelectDialog = function(element)
	{
		Telerik.Web.UI.Widgets.InsertSelectDialog.initializeBase(this, [element]);
		this._columnWidthBox = null;
		this._columnHeightBox = null;
		this._styleBuilder = null;
		this._cssClassSelector = null;
		this._cssText = "";
		this._idHolder = null;
		this._sizeHolder = null;
		this._nameHolder = null;
		this._multipleHolder = null;
		this._editor = null;
		this._selectToModify = null;
		this._spinBoxOptions = null;
		this._currentIndex = 0;
		this._insertSelectTextBox = null;
		this._insertSelectValueBox = null;
		this._radListBox = null;
		this._insertButton = null;
		this._cancelButton = null;
	}

	Telerik.Web.UI.Widgets.InsertSelectDialog.prototype = {
		initialize: function()
		{
			Telerik.Web.UI.Widgets.InsertSelectDialog.callBaseMethod(this, "initialize");
			this._initializeChildren();
		},

		_initializeChildren: function()
		{
			this._spinBoxDelegate = Function.createDelegate(this, this._spinBoxValueChanged);
			this._columnWidthBox = $find("SpinBoxWidth");
			this._columnHeightBox = $find("SpinBoxHeight");
			this._cssClassSelector = $find("ClassSelector");
			this._styleBuilder = $find("StyleBuilder");
			this._idHolder = $get("SelectId");
			this._sizeHolder = $get("SelectSize");
			this._nameHolder = $get("SelectName");
			this._multipleHolder = $get("SelectMultiple");
			this._spinBoxOptions = this._initializeSpinBox("SpinBoxOptions");
			this._insertSelectTextBox = $get("insertSelectText");
			this._insertSelectValueBox = $get("insertSelectValue");
			this._radListBox = $find("radListBoxOptions");
			this._insertButton = $get("InsertButton");
			this._cancelButton = $get("CancelButton");
			this._radListBox.get_items().clear();
			this._initializeChildEvents();
		},

		_initializeChildEvents: function()
		{
			this._styleBuilder.add_valueSelected(Function.createDelegate(this, this._styleBuilderClicked));
			$addHandlers(this._insertSelectTextBox, { "blur": this._insertSelectTextBoxBlurHandler }, this);
			$addHandlers(this._insertSelectValueBox, { "blur": this._insertSelectValueBoxBlurHandler }, this);
			this._radListBox.add_selectedIndexChanged(Function.createDelegate(this, this._radListBoxIndexChangedHandler));
			this._cssClassSelector.add_valueSelected(this._cssValueSelected);
			$addHandlers(this._insertButton, { "click": this._insertButtonClickHandler }, this);
			$addHandlers(this._cancelButton, { "click": this._cancelButtonClickHandler }, this);
		},

		clientInit: function(clientParameters)
		{
			this.setSelect(clientParameters.selectToModify);
			this._editor = clientParameters.editor;
			this._cssClasses = clientParameters.CssClasses;
			this._cssClassSelector.set_showText(true);
			this._cssClassSelector.set_clearclasstext(localization["ClearClass"]);
			this._cssClassSelector.set_text(localization["ApplyClass"]);
			this._spinBoxOptions.set_tooltipIncrease(localization['AddOption']);
			this._spinBoxOptions.set_tooltipDecrease(localization['RemoveOption']);
			this.setCssClasses(this._cssClasses);
			this.get_textBoxText().value = "";
			this.get_textBoxValue().value = "";
			this._columnWidthBox.set_value("");
			this._columnHeightBox.set_value("");
			this.loadSelectOptionsIntoRadListBox(this.getSelect());
			this.loadSelectPropertiesValues(this.getSelect());
			this._checkButtonAvailability();
			this._checkOptionsTextboxesAvailability();
		},

		_initializeSpinBox: function(boxId)
		{
			var spinBox = $find(boxId);
			spinBox.add_valueSelected(this._spinBoxDelegate);
			spinBox.set_value(9999);
			return spinBox;
		},

		_spinBoxValueChanged: function(sender, args)
		{
			var oldValue = args.get_oldValue();
			var newValue = args.get_newValue();
			var diff = newValue - oldValue;
			//difference should be either +1 or -1
			if (diff != -1 && diff != 1)
			{
				return;
			}
			return (diff == 1) ? this._addOptionClickHandler() : this._removeOptionClickHandler();
		},

		_insertButtonClickHandler: function()
		{
			this.updateSelectPropertiesValues(this.getSelect());

			Telerik.Web.UI.Dialogs.CommonDialogScript.get_windowReference().close(
			{
				selectToInsert: this.getSelect()
			});
		},

		_cancelButtonClickHandler: function()
		{
			Telerik.Web.UI.Dialogs.CommonDialogScript.get_windowReference().close();
		},

		_radListBoxIndexChangedHandler: function(sender, args)
		{
			var selected = sender.get_selectedItem();
			var text = selected.get_text();
			var value = selected.get_value();
			this.get_textBoxText().value = text;
			this.get_textBoxValue().value = value;
			this._checkOptionsTextboxesAvailability();
		},

		_insertSelectTextBoxBlurHandler: function()
		{
			var selectedItem = this.get_listBox().get_selectedItem();
			if (!selectedItem)
			{
				return;
			}

			var option = this.getOptionItem(selectedItem.get_text(), selectedItem.get_value());
			if (!option)
			{
				return;
			}
			var newText = this.get_textBoxText().value;

			option.text = newText;
			selectedItem.set_text(newText);
		},

		_insertSelectValueBoxBlurHandler: function()
		{
			var selectedItem = this.get_listBox().get_selectedItem();
			if (!selectedItem)
			{
				return;
			}

			var option = this.getOptionItem(selectedItem.get_text(), selectedItem.get_value());
			if (!option)
			{
				return;
			}

			var newValue = this.get_textBoxValue().value;
			option.value = newValue;
			selectedItem.set_value(newValue);
		},

		_addOptionClickHandler: function()
		{
			var lb = this.get_listBox();
			var item = new Telerik.Web.UI.RadListBoxItem();
			var index = this.get_index();
			var text = "Option" + index;
			var value = "Value" + index;
			item.set_text(text);
			item.set_value(value);

			var selectedItem;
			var selectedItemIndex;

			if (lb.get_items().get_count() > 0)
			{
				selectedItem = lb.get_selectedItem();
				selectedItemIndex = selectedItem ? selectedItem.get_index() : 0;
			}
			else
			{
				selectedItemIndex = 0;
			}

			lb.trackChanges();
			lb.get_items().insert(selectedItemIndex + 1, item);
			item.select();
			lb.commitChanges();

			this.addSelectElement(text, value, selectedItemIndex);

			this._checkButtonAvailability();
			this._checkOptionsTextboxesAvailability();
		},

		addSelectElement: function(text, value, index)
		{
			var select = this.getSelect();
			var indexOfPreviousElement = index == select.length - 1 ? null : index + 1;
			var previousElement = select[indexOfPreviousElement];
			var newOption = document.createElement("OPTION");
			newOption.text = text;
			newOption.value = value;
			if (previousElement)
			{
				select.add(newOption, previousElement);
			}
			else
			{
				select.add(newOption);
			}
			this.setSelect(select);
		},

		_removeOptionClickHandler: function()
		{
			var listBox = this.get_listBox();
			var item = listBox.get_selectedItem();
			if (!item)
			{
				return;
			}
			var selectedItemIndex = item.get_index();
			listBox.trackChanges();
			var text = item.get_text();
			var value = item.get_value();

			this.removeSelectItem(text, value);
			listBox.get_items().remove(item);

			if (selectedItemIndex == listBox.get_items().get_count() || selectedItemIndex > 0)
			{
				selectedItemIndex--;
			}

			var itemToSelect = listBox.getItem(selectedItemIndex);
			if (itemToSelect)
			{
				itemToSelect.select();
			}
			else
			{
				this.get_textBoxText().value = "";
				this.get_textBoxValue().value = "";
			}
			listBox.commitChanges();

			this._checkButtonAvailability();
			this._checkOptionsTextboxesAvailability();
		},

		_styleBuilderClicked: function(oTool, args)
		{
			var editor = this.get_editor();
			var callbackFunction = Function.createDelegate(this, function(sender, args)
			{
				var select = args.get_value();
				this.setCssText(select.style.cssText);
			});

			var parameterSelect = this.getSelect().cloneNode(true);
			var argument = new Telerik.Web.UI.EditorCommandEventArgs("StyleBuilder", null, parameterSelect);

			Telerik.Web.UI.Editor.CommandList._getDialogArguments(argument, "*", editor, "StyleBuilder");
			argument.fontNames = editor.get_fontNames();
			editor.showDialog("StyleBuilder", argument, callbackFunction);
		},

		_cssValueSelected: function(oTool, args)
		{
			if (!oTool) return;

			var commandName = oTool.get_name();

			if ("ApplyClass" == commandName)
			{
				var attribValue = oTool.get_selectedItem();
				oTool.updateValue(attribValue);
			}
		},

		_checkButtonAvailability: function()
		{
			this._spinBoxOptions.set_enabledIncrease(true);
			this._spinBoxOptions.set_enabledDecrease(this.getSelect().childNodes.length > 0);
		},

		_checkOptionsTextboxesAvailability: function()
		{
			var selectedItem = this.get_listBox().get_selectedItem();
			if (selectedItem)
			{
				this.get_textBoxText().removeAttribute("disabled");
				this.get_textBoxValue().removeAttribute("disabled");
			}
			else
			{
				this.get_textBoxText().setAttribute("disabled", "disabled");
				this.get_textBoxValue().setAttribute("disabled", "disabled");
			}
		},

		getCssClasses: function()
		{
			return this._cssClassSelector.get_value();
		},

		setCssClasses: function(classes)
		{
			if (classes)
			{
				this._cssClassSelector.set_items(classes);
			}
		},

		getStyleBuilder: function()
		{
			return this._styleBuilder;
		},

		getCssClassSelector: function()
		{
			return this._cssClassSelector;
		},

		getWidth: function()
		{
			return this._columnWidthBox.get_value();
		},

		setWidth: function(value)
		{
			this._columnWidthBox.set_value(value);
		},

		clearWidth: function()
		{
			this._columnWidthBox.set_value("");
		},

		getHeight: function()
		{
			return this._columnHeightBox.get_value();
		},

		setHeight: function(value)
		{
			this._columnHeightBox.set_value(value);
		},

		clearHeight: function()
		{
			this._columnHeightBox.set_value("");
		},

		getId: function()
		{
			if (this._idHolder && this._idHolder.value)
			{
				return this._idHolder.value;
			}
			else
			{
				return "";
			}
		},

		setId: function(id)
		{
			if (id && id != "")
			{
				this._idHolder.value = id;
			}
			else
			{
				this._idHolder.value = "";
			}
		},

		getName: function()
		{
			if (this._nameHolder && this._nameHolder.value)
			{
				return this._nameHolder.value;
			}
			else
			{
				return "";
			}
		},

		setName: function(name)
		{
			if (name && name != "")
			{
				this._nameHolder.value = name;
			}
		},

		getMultipleIsChecked: function()
		{
			return this._multipleHolder.checked;
		},

		setMultipleChecked: function(checked)
		{
			this._multipleHolder.checked = checked;
		},

		getSize: function()
		{
			if (this._sizeHolder && this._sizeHolder.value)
			{
				return this._sizeHolder.value;
			}
			else
			{
				return "";
			}
		},

		setSize: function(size)
		{
			var parsedSize = this.parseIntOrReturnNull(size);
			if (parsedSize)
			{
				this._sizeHolder.value = parsedSize;
			}
			else
			{
				this._sizeHolder.value = "";
			}
		},

		parseIntOrReturnNull: function(strNumber)
		{
			var returnValue = null;
			if (strNumber != null && strNumber.toString().length > 0 && !isNaN(strNumber))
			{
				returnValue = parseInt(strNumber);
			}
			return returnValue;
		},

		getSelect: function()
		{
			if (this._selectToModify)
			{
				return this._selectToModify;
			}
			else
			{
				return document.createElement("SELECT");
			}
		},

		setSelect: function(select)
		{
			if (select)
			{
				this._selectToModify = select;
			}
			else
			{
				this._selectToModify = document.createElement("SELECT");
			}
		},

		setCssText: function(cssText)
		{
			this._cssText = cssText;
		},

		getCssText: function()
		{
			return this._cssText;
		},

		loadSelectOptionsIntoRadListBox: function(select)
		{
			var list = this.get_listBox();
			var items = list.get_items();

			list.trackChanges();
			items.clear();
			for (var i = 0; i < select.length; i++)
			{
				var item = new Telerik.Web.UI.RadListBoxItem();
				item.set_text(select.options[i].text);
				item.set_value(select.options[i].value);
				items.add(item);
			}
			list.commitChanges();
		},

		get_textBoxText: function()
		{
			return this._insertSelectTextBox;
		},

		get_textBoxValue: function()
		{
			return this._insertSelectValueBox;
		},

		get_listBox: function()
		{
			return this._radListBox;
		},

		getOptionItem: function(text, value)
		{
			var select = this.getSelect();

			for (var i = 0; i < select.length; i++)
			{
				if (select[i].value == value && select[i].text == text)
				{
					return select[i];
				}
			}
		},

		removeSelectItem: function(text, value)
		{
			var itemToRemove = this.getOptionItem(text, value);

			if (itemToRemove)
			{
				this.getSelect().removeChild(itemToRemove);
			}
		},

		get_index: function()
		{
			if (!this._currentIndex)
			{
				this._currentIndex = 0;
			}

			this._currentIndex++;
			return this._currentIndex;
		},

		get_editor: function()
		{
			return this._editor;
		},

		loadSelectPropertiesValues: function(select)
		{
			this.setCssText(select.style.cssText);
			this.setWidth(select.style.width);
			this.setHeight(select.style.height);

			var oId = select.getAttribute("id");
			this.setId(oId);

			var oSize = select.getAttribute("size");
			if (!oSize || isNaN(oSize))
			{
				select.removeAttribute("size", false);
				this.setSize("");
			}
			else
			{
				this.setSize(oSize);
			}

			var oName = select.getAttribute("name");
			this._nameHolder.value = oName || "";

			if (select.multiple)
			{
				this.setMultipleChecked(true);
			}
			else
			{
				this.setMultipleChecked(false);
			}

			var cssClassVal = select.className;
			if (cssClassVal == null)
			{
				cssClassVal = "";
			}
			this._cssClassSelector.updateValue(cssClassVal);
			if (cssClassVal == "")
				this._cssClassSelector.set_selectedIndex(0);
		},

		updateSelectPropertiesValues: function(select)
		{
			if (!select)
			{
				return;
			}

			select.style.cssText = this.getCssText();

			if (select.style.cssText == '')
			{
				select.removeAttribute('style', false);
			}

			var theWidthValue = this.getWidth();
			if (this._isValueValid(theWidthValue))
			{
				select.style.width = theWidthValue ? this._convertIntToPixel(theWidthValue) : "";
			}

			var theHeightValue = this.getHeight();
			if (this._isValueValid(theHeightValue))
			{
				select.style.height = theHeightValue ? this._convertIntToPixel(theHeightValue) : "";
			}

			if (this.getId() != '')
			{
				select.setAttribute("id", this.getId());
			}
			else
			{
				select.removeAttribute("id");
			}

			if (this.getName() != '')
			{
				select.setAttribute("name", this.getName());
			}
			else
			{
				select.removeAttribute("name");
			}

			if (!isNaN(this.getSize()) && this.getSize() != '' && this.getSize() != '0')
			{
				select.setAttribute("size", this.getSize());
			}
			else
			{
				select.removeAttribute("size");
			}

			if (this.getMultipleIsChecked())
			{
				select.setAttribute("multiple", "multiple");
			}
			else
			{
				select.removeAttribute("multiple");
			}

			if (this.getCssClasses() != '')
			{
				select.className = this.getCssClasses();
			}
			else
			{
				select.removeAttribute("class");
			}

			this.setSelect(select);
		},

		_isValueValid: function(value)
		{
			if (value == "")
				return true;
			var valueInt = parseFloat(value, 10);
			if (!isNaN(valueInt))
			{
				//NEW support for all css units
				var isValidPercent = (valueInt + '%' == value);
				var isValidPixel = (valueInt + 'px' == value.toLowerCase());
				var isValidEm = (valueInt + 'em' == value.toLowerCase());
				var isValidEx = (valueInt + 'ex' == value.toLowerCase());
				var isValidIn = (valueInt + 'in' == value.toLowerCase());
				var isValidCm = (valueInt + 'cm' == value.toLowerCase());
				var isValidMm = (valueInt + 'mm' == value.toLowerCase());
				var isValidPt = (valueInt + 'pt' == value.toLowerCase());
				var isValidPc = (valueInt + 'pc' == value.toLowerCase());
				var isValidNumber = (valueInt.toString() == value);

				if (isValidPercent || isValidPixel || isValidEm || isValidEx || isValidIn || isValidCm || isValidMm || isValidPt || isValidPc || isValidNumber)
				{
					return true;
				}
			}
			return false;
		},

		_convertIntToPixel: function(oVal)
		{
			var oNew = "" + oVal;

			if (oNew.indexOf("%") != -1)
			{
				return oNew;
			}
			else
			{
				var arMatch = oNew.match(/(em|ex|px|in|cm|mm|pt|pc)$/); //NEW support for all css units
				oNew = parseFloat(oNew);
				if (!isNaN(oNew))
				{
					oNew = (arMatch) ? oNew + arMatch[0] : oNew + "px"; //NEW support for all css units
					return oNew;
				}
			}
			return oVal;
		},

		/////////////////////////////////////////////////////////////////////////

		dispose: function()
		{
			this._radListBox.get_items().clear();
			this._columnWidthBox.value = "";
			this._columnHeightBox.value = "";
			this._idHolder.value = "";
			this._sizeHolder.value = "";
			this._nameHolder.value = "";
			this._insertSelectTextBox.value = "";
			this._insertSelectValueBox.value = "";
			this._columnWidthBox = null;
			this._columnHeightBox = null;
			this._styleBuilder = null;
			this._cssClassSelector = null;
			this._cssText = "";
			this._idHolder = null;
			this._sizeHolder = null;
			this._nameHolder = null;
			this._multipleHolder = null;
			this._editor = null;
			this._selectToModify = null;
			this._currentIndex = 0;
			this._insertButton = null;
			this._cancelButton = null;

			Telerik.Web.UI.Widgets.InsertSelectDialog.callBaseMethod(this, "dispose");
		}
	}

	Telerik.Web.UI.Widgets.InsertSelectDialog.registerClass('Telerik.Web.UI.Widgets.InsertSelectDialog', Telerik.Web.UI.RadWebControl, Telerik.Web.IParameterConsumer);


</script>

<style type="text/css">
	.reInsertSelectCssClass {
		height: 245px;
		width: 210px;
		margin: 4px 2px 4px 2px;
		padding: 4px;
	}
	
	.RadForm_BlackMetroTouch .reInsertSelectCssClass,
	.RadForm_MetroTouch .reInsertSelectCssClass {
		width: 230px;
		height: 315px;
	}

		.reInsertSelectCssClass > table > tbody > tr > .reLabelCell {
			text-align: right;
			padding-right: 2px;
		}

	.reInsertSelectItemsWrapper {
		width: 265px;
		height: 245px;
		margin: 4px 4px 4px 6px;
		padding: 5px 1px 3px 1px;
	}
	
	.RadForm_BlackMetroTouch .reInsertSelectItemsWrapper,
	.RadForm_MetroTouch .reInsertSelectItemsWrapper {
		height: 315px;
	}

	.insertSelectItems {
		vertical-align: top;
		margin-right: 5px;
	}

	.insertSelectOkCancelButtons {
		display: block;
		margin: 0 auto;
		float: right;
	}

	.insertSelectButtonCell {
		padding: 3px 8px 3px 0px;
		display: inline-block;
		*display: inline;
	}

	#SpinBoxHeight, #SpinBoxWidth {
		float: left;
	}

	.insertSelectCell {
		width: 111px !important;
	}

	.reInsertSelectButtonsWrapper {
		margin-bottom: 2px;
	}

	.itemsWrapper {
		float: left;
		margin-left: 5px;
	}

	.reInsertSelectClearer {
		clear: both;
	}

	.reInsertSelectTextboxWrapper {
		float: left;
		margin-bottom: 5px;
        width: 115px;
	}

	.reInsertSelectDialogTable > tbody > tr > td {
		vertical-align: top;
	}
</style>



<table class="reInsertSelectDialogTable" cellpadding="0" cellspacing="0">
	<tr>
		<td>
			<fieldset class="reInsertSelectItemsWrapper">
				<legend>
					<script type="text/javascript">document.write(localization["ManageOptions"]);</script>
				</legend>
				<div class="reInsertSelectButtonsWrapper">
					<tools:EditorSpinBox id="SpinBoxOptions" VisibleInput="false" runat="server"></tools:EditorSpinBox>
				</div>

				<div class="reInsertSelectClearer"></div>

				<div class="itemsWrapper">
					<div class="reLabelCell insertSelectItems">
						<label class="reDialogLabel">
							<script type="text/javascript">document.write(localization["Options"]);</script>:
						</label>
					</div>
					<div class="insertSelectItems">
						<telerik:RadListBox ID="radListBoxOptions" runat="server" SelectionMode="Single" Width="132" Height="155">
						</telerik:RadListBox>
					</div>
				</div>
				<div class="reInsertSelectTextboxWrapper">
					<div class="reLabelCell">
						<label class="reDialogLabel">
							<script type="text/javascript">document.write(localization["Text"]);</script>:
						</label>
					</div>
					<div>
						<input id="insertSelectText" class="insertSelectCell" type="text" value="" />
					</div>
				</div>

				<div class="reInsertSelectTextboxWrapper">
					<div class="reLabelCell">
						<label class="reDialogLabel">
							<script type="text/javascript">document.write(localization["Value"]);</script>:
						</label>
					</div>
					<div>
						<input id="insertSelectValue" class="insertSelectCell" type="text" value="" />
					</div>
				</div>

			</fieldset>
		</td>
		<td>
			<fieldset class="reInsertSelectCssClass">
				<legend>
					<script type="text/javascript">document.write(localization["Properties"]);</script>
				</legend>
				<table border="0" cellpadding="1" cellspacing="0">
					<tr>
						<td class="reLabelCell">
							<label class="reDialogLabel">
								<script type="text/javascript">document.write(localization["Height"]);</script>:
							</label>
						</td>
						<td class="reControlCell">
							<tools:EditorSpinBox ID="SpinBoxHeight" runat="server"></tools:EditorSpinBox>
							<span>
								<script type="text/javascript">document.write(localization["PixelsOrPercents"]);</script>
							</span>
						</td>
					</tr>
					<tr>
						<td class="reLabelCell">
							<label class="reDialogLabel">
								<script type="text/javascript">document.write(localization["Width"]);</script>:
							</label>
						</td>
						<td class="reControlCell">
							<tools:EditorSpinBox ID="SpinBoxWidth" runat="server"></tools:EditorSpinBox>
							<span>
								<script type="text/javascript">document.write(localization["PixelsOrPercents"]);</script>
							</span>
						</td>
					</tr>
					<tr>
						<td class="reLabelCell">
							<label class="reDialogLabel">
								<script type="text/javascript">document.write(localization["StyleBuilder"]);</script>:
							</label>
						</td>
						<td class="reControlCell">
							<tools:StandardButton runat="server" ID="StyleBuilder" ToolName="StyleBuilder" />
						</td>
					</tr>
					<tr>
						<td class="reLabelCell">
							<label class="reDialogLabel">
								<script type="text/javascript">document.write(localization["CssClass"])</script>:
							</label>
						</td>
						<td class="reControlCell">
							<tools:ApplyClassDropDown ID="ClassSelector" runat="server" Width="100px" />
						</td>
					</tr>
					<tr>
						<td class="reLabelCell">
							<label class="reDialogLabel">
								<script type="text/javascript">document.write(localization["Id"]);</script>:
							</label>
						</td>
						<td class="reControlCell">
							<input type="text" id="SelectId" class="insertSelectCell" value="" />
						</td>
					</tr>
					<tr>
						<td class="reLabelCell">
							<label class="reDialogLabel">
								<script type="text/javascript">document.write(localization["Size"]);</script>:
							</label>
						</td>
						<td class="reControlCell">
							<input type="text" id="SelectSize" class="insertSelectCell" value="" />
						</td>
					</tr>
					<tr>
						<td class="reLabelCell">
							<label class="reDialogLabel">
								<script type="text/javascript">document.write(localization["Name"]);</script>:
							</label>
						</td>
						<td class="reControlCell">
							<input type="text" id="SelectName" class="insertSelectCell" value="" />
						</td>
					</tr>
					<tr>
						<td class="reLabelCell">
							<label class="reDialogLabel">
								<script type="text/javascript">document.write(localization["Multiple"]);</script>:
							</label>
						</td>
						<td class="reControlCell">
							<input type="checkbox" id="SelectMultiple" />
						</td>
					</tr>
				</table>
			</fieldset>
		</td>
	</tr>
</table>
<div class="insertSelectOkCancelButtons">
	<div class="insertSelectButtonCell">
		<button type="button" id="InsertButton">
			<script type="text/javascript">
				setInnerHtml("InsertButton", localization["OK"]);
			</script>
		</button>
	</div>
	<div class="insertSelectButtonCell">
		<button type="button" id="CancelButton">
			<script type="text/javascript">
				setInnerHtml("CancelButton", localization["Cancel"]);
			</script>
		</button>
	</div>
</div>