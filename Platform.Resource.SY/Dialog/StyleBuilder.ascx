<%@ Control Language="C#" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI.Editor" TagPrefix="tools" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI.Widgets" TagPrefix="widgets" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI.Dialogs" TagPrefix="dialogs" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<style type="text/css">
	legend 
	{
		margin-left: 1em;
	}
	label.inlineLabel
	{
		margin: 4px 0.5em 0 0;
	}
	label.floater
	{
		float: left;
		padding-right: 3px;
	}
	label.sLabel 
	{
		display: inline-block;
		width: 100px;
		text-align: right;
		margin: 5px 0;
	}
	label.columnLabel
	{
		display: inline-block;
		width: 50px;
		text-align: right;
		margin: 5px 0;
	}
	label.standalone
	{
		display: block;
	}
	
	ul 
	{
		margin: 0;
		padding: 0;
		list-style: none none outside;
	}
	ul li
	{
		clear: both;
	}

	#fontSizeRelative
	{
		margin-top: 2px;
	}
	
	#dialogControl {
		width: 545px;
		overflow: hidden;
	}
	
	.RadForm_MetroTouch #dialogControl,
	.RadForm_BlackMetroTouch #dialogControl {
		width: 640px;
	}
	
	.RadForm_Glow #dialogControl,
	.RadForm_Silk #dialogControl {
		width: 590px;
	}
	
	.RadForm_Glow div#sbMainPane,
	.RadForm_Silk div#sbMainPane {
		width: 435px;
	}

	#styleBuilderTabs
	{
		float:left;
		width: 103px;
		overflow: hidden;
	}

	#sbMainPane
	{
		float: left;
		width: 435px;
		margin: 6px 0 0 5px;
	}
	fieldset
	{
		margin: 0.5em 0;
		padding-bottom: 0.5em;
		width: 396px;
	}
	fieldset fieldset
	{
		margin: 0.5em auto 0 30px;
		width: 350px;
	}
	#fontAttributes .RadComboBox
	{
		display: block;
		width: 90px;
	}
	
	#BorderContent .Labels
	{
		 float: left;
		 padding: 37px 2px 0 0;
		 line-height: 21px;
	}
	#BorderContent .Labels .columnLabel
	{
		width: 40px;
	}
	#BorderContent fieldset
	{
		float: left;
		padding: 0 0.3em 0.3em;
		height: 165px;
		margin-left: 4px;
		_margin-left: 2px;
	}
	#BorderContent fieldset .RadComboBox
	{
		margin: 2px 0;
	}
	#BorderContent fieldset .reToolWrapper
	{
		margin: 2px 0;
		height: 24px;
	}
	#borderStylePane
	{
		width: 100px;
	}
	#borderWidthPane
	{
		width: 130px;
		*width: 135px;
	}
	
	#borderWidthPane .RadComboBox {
		*margin-left: 5px !important;
	}
	
	#borderColorPane
	{
		width: 83px;
	}
	#borderColorPane legend
	{
		margin-left: 0.5em;
	}
	#borderColorPane ul li
	{
		height: 24px;
	}
	* html #borderColorPane ul li, *+html #borderColorPane ul li
	{
		height: 22px;
	}
	#borderCollapseContainer
	{
		clear: both;
		text-align: center;
		padding-top: 1em;
	}

	.radECtrlButtonsList
	{
		margin: 8px;
		text-align: right;
	}
	.radECtrlButtonsList button
	{
		width: 100px;
	}
</style>
<script type="text/javascript">
	Type.registerNamespace("Telerik.Web.UI.Widgets");

	Telerik.Web.UI.Widgets.StyleBuilder = function(element)
	{
		Telerik.Web.UI.Widgets.StyleBuilder.initializeBase(this, [element]);
		this._clientParameters = null;
		this._insertButton = null;
		this._htmlElement = null;
		this.controls = [];
		this.parsers = [];
		this.indexedControls = {};
	}
	Telerik.Web.UI.Widgets.StyleBuilder.prototype = {
		initialize: function() { },
		clientInit: function(clientParameters)
		{
			this._sourceElement = clientParameters.get_value();
			this._initDependentControls(clientParameters);
			this._listenToOKButton();
			this._readStyleFromSourceElement();
		},
		_initDependentControls: function(clientParameters)
		{
			this.initColorPickers(clientParameters.Colors);
			this.populateFontFamilyCombo(clientParameters.fontNames);

			$findSB("backgroundImage").set_editor(clientParameters.editor);
			$findSB("listBulletImage").set_editor(clientParameters.editor);
			$findSB("borderCollapse").get_element().style.visibility = this._sourceElement.tagName == "TABLE" ? "visible" : "hidden";
		},
		initColorPickers: function(colors)
		{
			$telerik.$("a[title$=Color]").each($telerik.$.proxy(function(i, picker) {
				if(picker.control)
					this.initColorPicker(picker.control, colors);
			}, this));
		},
		initColorPicker: function(colorPicker, colors)
		{
			colorPicker.set_items(colors);
			colorPicker.set_addCustomColorText(localization["AddCustomColor"]);
			colorPicker.set_color("");
		},
		populateFontFamilyCombo: function(fontNames)
		{
			var fontFamilyControl = $findSB("fontFamily");
			fontFamilyControl.clearItems();
			var fonts = [{text: "", value: ""}];
			for(var i = 0; i <fontNames.length; i++)
			{
				var font = fontNames[i];
				fonts.push({text: font, value: font.toLowerCase()});
			}
			Telerik.Web.UI.Widgets.StyleBuilder.Helpers.addComboBoxItems(fontFamilyControl, fonts);
			fontFamilyControl.set_selectedIndex(0);
		},
		_listenToOKButton: function()
		{
			var button = $get("insertButton");
			$addHandler(button, "click", Function.createDelegate(this, this._applyStyleToSourceElement));
		},
		_readStyleFromSourceElement: function()
		{
			var elementStyle = this._sourceElement.style;
			
			for(var parserName in this.parsers)
				this.parseStyle(this.parsers[parserName], elementStyle[parserName]);
		},
		parseStyle: function(parser, value)
		{
			if(value)
				parser.set_value(value);
			else
				parser.clear_value();
		},
		getParserByStyleName: function(styleName)
		{
			var parser = this.parsers[styleName];
			return parser && Telerik.Web.UI.Widgets.StyleBuilder.Control.isInstanceOfType(parser) ? parser : null;
		},
		_applyStyleToSourceElement: function()
		{
			var sourceElement = $telerik.$(this._sourceElement);
			for(var i = 0; i < this.controls.length; i++)
			{
				sourceElement.css(this.controls[i].get_css());
			}
			this.clearUpCssTextAfterIE();

			var arguments = new Telerik.Web.UI.EditorCommandEventArgs("StyleBuilder", null, this._sourceElement);
			Telerik.Web.UI.Dialogs.CommonDialogScript.get_windowReference().close(arguments);
		},
		registerControl: function(control)
		{
			var controlName = control.get_styleName();
			if(!this.indexedControls[controlName])
			{
				this.indexedControls[controlName] = control;
				this.controls.push(control);
				this.registerParser(control);
			}

			return control;
		},
		unregisterControl: function(name)
		{
			var control = this.indexedControls[name];
			if(control)
			{
				delete this.indexedControls[name];
				Array.remove(this.controls, control);
				delete this.parsers[name];
			}
		},
		registerParser: function(parser)
		{
			var parserName = parser.get_styleName();
			if(!this.parsers[parserName])
				this.parsers[parserName] = parser;

			return parser;
		},
		get_element: function()
		{
			return this._sourceElement;
		},
		clearControlValues: function()
		{
			Array.forEach(this.controls, function(control) { control.clear_value(); });
		},
		clearUpCssTextAfterIE: function() //IE6,7,8 have a strange bug when reseting fontFamily and textDecoration to the empty string. They still remain in the cssText string. Therefore brute force remove is needed.
		{
			if($telerik.isIE6 || $telerik.isIE7 || $telerik.isIE8)
			{
				var elementStyle = this._sourceElement.style;
				var cssText = elementStyle.cssText + ";";
				elementStyle.cssText = cssText.replace(/((font-family)|(text-decoration)):\s*;/gi, "");
			}
		},
		dispose: function()
		{
			while(this.parsers.length)
				this.parsers.pop().dispose();
		}
	};
	Telerik.Web.UI.Widgets.StyleBuilder.registerClass('Telerik.Web.UI.Widgets.StyleBuilder', Telerik.Web.UI.RadWebControl, Telerik.Web.IParameterConsumer);

	//StyleBuilder Controls
	(function($, $sb) {
	/// <summary>Base class for all StyleBuilder controls</summary>
	$sb.Control = function(options)
	{
		this._name = options.name;
		this._enabled = true;
		this._class = options._class;
	}
	$sb.Control.prototype =
	{
		get_css: function()
		{
			var result = {};
			result[this.get_styleName()] = this.get_value();
		
			return result;
		},
		toString: function()
		{
			var value = this.get_value();
			return value ? this.get_name() + ":" + value + ";" : "";
		},
		get_value: function() {},
		set_value: function() {},
		get_name: function()
		{
			return this._name;
		},
		get_styleName: function()
		{
			return $sb.Helpers.toStyleName(this._name);
		},
		clear_value: function() {},
		get_wrapped: function() {},
		set_enabled: function(enabled) { this._enabled = enabled; },
		get_enabled: function() { return this._enabled; },
		dispose: function() {},
		disposeControl: function(name)
		{
			var control = this[name];
			if(control && !control._disposed)
				control.dispose();

			delete this[name];
		}
	}
	$sb.Control.registerClass("Telerik.Web.UI.Widgets.StyleBuilder.Control");

	/// <summary>Class for managing css properties that depend on a single combo box</summary>
	$sb.ComboBox = function(options)
	{
		$sb.ComboBox.initializeBase(this, [$.extend({_class: $sb.ComboBox}, options)]);
		this._combo = options.combo;
		this._valueStripper = options.valueStripper || null;
	}
	$sb.ComboBox.prototype =
	{
		get_value: function()
		{
			return this._combo.get_value();
		},
		set_value: function(value)
		{
			if(this._valueStripper)
				value = value.replace(this._valueStripper, "");
			
			var option = this._combo.findItemByValue(value);
			if(value == "" && !option)
			{
				var firstItem = this._combo.get_items().getItem(0);
				if(firstItem.get_value() == value)
					option = firstItem;
			}
			
			if(option)
				option.select();
		},
		clear_value: function()
		{
			this.set_value("");
		},
		set_enabled: function(enabled)
		{
			this._combo[enabled ? "enable" : "disable"]();
			this._class.callBaseMethod(this, "set_enabled", [enabled]);
		},
		get_combo: function()
		{
			return this._combo;
		},
		dispose: function()
		{
			this.disposeControl("_combo");
		}
	};
	$sb.ComboBox.registerClass("Telerik.Web.UI.Widgets.StyleBuilder.ComboBox", $sb.Control);

	/// <summary>Class for managing numeric css properties</summary>
	$sb.Numeric = function(options)
	{
		$sb.Numeric.initializeBase(this, [$.extend({_class: $sb.Numeric}, options)]);
		this._number = options.number;
		this._metric = options.metric;
	}
	$sb.Numeric.prototype =
	{
		get_value: function()
		{
			var value = this._number.get_value();
			return this.get_enabled() && value ? this._number.get_value() + this._metric.get_value() : "";
		},
		set_value: function(value)
		{
			var number = parseFloat(value);
			var metric = value.replace(number, "").trimStart();
			this._number.set_value(value);
			this.set_metric(metric);
		},
		set_metric: function(value)
		{
			var option = this._metric.findItemByValue(value.toLowerCase());
			if(option)
				option.select();
		},
		clear_value: function()
		{
			this.set_value("");
		},
		set_enabled: function(enabled)
		{
			this._number.set_enabled(enabled);
			this._metric[enabled ? "enable" : "disable"]();
			this._class.callBaseMethod(this, "set_enabled", [enabled]);
		},
		dispose: function()
		{
			this.disposeControl("_number");
			this.disposeControl("_metric");
		}
	}
	$sb.Numeric.registerClass("Telerik.Web.UI.Widgets.StyleBuilder.Numeric", $sb.Control);

	/// <summary>Manages color oriented CSS rules</summary>
	$sb.ColorPicker = function(options)
	{
		$sb.ColorPicker.initializeBase(this, [$.extend({_class: $sb.ColorPicker}, options)]);
		this._picker = options.picker;
	}
	$sb.ColorPicker.prototype =
	{
		get_value: function()
		{
			return this._picker.get_color();
		},
		set_value: function(value)
		{
			this._picker.set_color(value);
		},
		clear_value: function()
		{
			this.set_value("");
		},
		dispose: function()
		{
			this.disposeControl("_picker");
		}
	}
	$sb.ColorPicker.registerClass("Telerik.Web.UI.Widgets.StyleBuilder.ColorPicker", $sb.Control);

	/// <summary>Manages image oriented CSS rules</summary>
	$sb.GenericControl = function(options)
	{
		this._syntax = {
			prefix: options.prefix || "",
			suffix: options.suffix || ""
		};
		this._syntax = $.extend({
			prefixStrip: options.prefixStrip || this._syntax.prefix,
			suffixStrip: options.suffixStrip || this._syntax.suffix
		}, this._syntax);

		$sb.GenericControl.initializeBase(this, [$.extend({_class: $sb.GenericControl}, options)]);
		this._control = options.control;
	}
	$sb.GenericControl.prototype =
	{
		get_value: function()
		{
			var value = this._control.get_value();
			return value ? this._syntax.prefix + value + this._syntax.suffix : "";
		},
		set_value: function(value)
		{
			this._control.set_value(this._parseValue(value));
		},
		_parseValue: function(value)
		{
			return value.replace(this._syntax.prefixStrip, "").replace(this._syntax.suffixStrip, "");
		},
		clear_value: function()
		{
			this.set_value("");
		},
		dispose: function()
		{
			this.disposeControl("_control");
		}
	};
	$sb.GenericControl.registerClass("Telerik.Web.UI.Widgets.StyleBuilder.GenericControl", $sb.Control);

	$sb.ToggleButton = function(options)
	{
		$sb.ToggleButton.initializeBase(this, [$.extend({_class: $sb.ToggleButton}, options)]);
		this._button = options.button;
	}
	$sb.ToggleButton.prototype =
	{
		get_value: function()
		{
			var button = this._button;
			return button.get_checked() ? button.get_commandName() : "";
		},
		set_value: function(value)
		{
			this._button.set_checked(!!value);
		},
		clear_value: function()
		{
			this.set_value(false);
		},
		set_enabled: function(enabled)
		{
			this._button.set_enabled(enabled);
		},
		dispose: function()
		{
			this.disposeControl("_button");
		}
	};
	$sb.ToggleButton.registerClass("Telerik.Web.UI.Widgets.StyleBuilder.ToggleButton", $sb.Control);

	$sb.Tuple = function(options)
	{
		this._sintax = {
			prefix: options.prefix || "",
			suffix: options.suffix || "",
			delimiter: options.delimiter || " "
		};
		$sb.Tuple.initializeBase(this, [$.extend({_class: $sb.Tuple}, options)]);
		this._controls = options.controls;
	}
	$sb.Tuple.prototype =
	{
		get_value: function()
		{
			return this._joinValues(this.get_values()).trim();
		},
		get_values: function()
		{
			var values = [];
			for(var i = 0; i < this._controls.length; i++)
				values.push(this._controls[i].get_value());

			return values;
		},
		_joinValues: function(values)
		{
			return this._sintax.prefix + values.join(this._sintax.delimiter) + this._sintax.suffix;
		},
		set_value: function(value)
		{
			var values = this._parseValue(value);
			this.set_values(values);
		},
		set_values: function(values)
		{
			Array.forEach(values, function(value, index)
			{
				this._controls[index].set_value(value);
			}, this);
		},
		clear_value: function()
		{
			Array.forEach(this._controls, function(control)
			{
				control.clear_value();
			});
		},
		_parseValue: function(value)
		{
			var values = value.split(this._sintax.delimiter);
			if(values.length > 0)
			{
				values[0] = values[0].replace(this._sintax.prefix, "");
				values[values.length - 1] = values[values.length - 1].replace(this._sintax.suffix, "");
			}
			return values;
		
		},
		get_tuple: function()
		{
			return this._controls;
		},
		dispose: function()
		{
			while(this._controls.length)
				this._controls.pop().dispose();

			this._controls = null;
		}
	};
	$sb.Tuple.registerClass("Telerik.Web.UI.Widgets.StyleBuilder.Tuple", $sb.Control);

	$sb.FourTuple = function(options)
	{
		$sb.FourTuple.initializeBase(this, [$.extend({_class: $sb.FourTuple}, options)]);
		this._controls = options.controls;
		this._sameForAll = options.sameForAll;
		this._toggleSameForAllHandler = Function.createDelegate(this, this._toggleSameForAll);
		this._sameForAll.add_checkedChanged(this._toggleSameForAllHandler);
	}
	$sb.FourTuple.prototype =
	{
		get_css: function()
		{
			if(this._sameForAll.get_checked())
				return this._class.callBaseMethod(this, "get_css");

			var emptyResults = 0;
			for(var i = 0; i < this._controls.length; i++)
				if(!this._controls[i].get_value())
					emptyResults++;

			if(emptyResults > 0 && emptyResults < this._controls.length)
				return this.get_css_separate();

			return this._class.callBaseMethod(this, "get_css");
		},
		get_css_separate: function()
		{
			var css = {};
			for(var i = 0; i < this._controls.length; i++)
			{
				$.extend(css, this._controls[i].get_css());
			}

			return css;
		},
		get_value: function()
		{
			if(this._sameForAll.get_checked())
				return this._controls[0].get_value();

			return this._class.callBaseMethod(this, "get_value");
		},
		_joinValues: function(values)
		{
			return this._class.callBaseMethod(this, "_joinValues", [this._normalizeValues(values)]);
		},
		_parseValue: function(value)
		{
			return this._denormalizeValues(this._class.callBaseMethod(this, "_parseValue", [value]));
		},
		_normalizeValues: function(values)
		{
			values = values.slice(0, 4);
			if(values[1] === values[3]) //right === left
			{
				values.pop();
				if(values[0] === values[2]) //top === bottom
				{
					values.pop();
					if(values[0] === values[1]) //top === right
						values.pop();
				}
			}
			return values;
		},
		_denormalizeValues: function(values)
		{
			switch(values.length)
			{
				case 1: values = values.concat(values);
				case 2: values = values.concat(values);
						break;
				case 3: values.push(values[1]);
				case 4: break;
				default: values = values.slice(0, 4);
					
			}
			return values;
		},
		_toggleSameForAll: function()
		{
			for(var i = 1; i < this._controls.length; i++)
				this._controls[i].set_enabled(!this._sameForAll.get_checked());
		},
		get_tupleObject: function()
		{
			return {
				top: this._controls[0],
				right: this._controls[1],
				bottom: this._controls[2],
				left: this._controls[3]
			};
		},
		dispose: function()
		{
			this._class.callBaseMethod(this, "dispose");
		
			this._sameForAll.remove_checkedChanged(this._toggleSameForAllHandler);
			this.disposeControl("_sameForAll");
		}
	};
	$sb.FourTuple.registerClass("Telerik.Web.UI.Widgets.StyleBuilder.FourTuple", $sb.Tuple);

	$sb.ComboCustomNumeric = function(options)
	{
		$sb.ComboCustomNumeric.initializeBase(this, [$.extend({_class: $sb.ComboCustomNumeric}, options)]);
		this._combo = options.combo;
		this._numeric = options.numeric;
	}
	$sb.ComboCustomNumeric.prototype =
	{
		get_value: function()
		{
			var comboValue = this._combo.get_value();
			return comboValue == "custom" ? this._numeric.get_value() : comboValue;
		},
		set_value: function(value)
		{
			var control = this._getControlByValue(value);
			control.set_value(value);
			if($sb.Numeric.isInstanceOfType(control))
				this._combo.set_value("custom");
		},
		clear_value: function()
		{
			this.set_value("");
		},
		_getControlByValue: function(value)
		{
			var dropDown = this._combo.get_combo();
			return dropDown.findItemByValue(value) ? this._combo : this._numeric;
		},
		dispose: function()
		{
			this.disposeControl("_combo");
			this.disposeControl("_numeric");
		}
	};
	$sb.ComboCustomNumeric.registerClass("Telerik.Web.UI.Widgets.StyleBuilder.ComboCustomNumeric", $sb.Control);

	$sb.FontSize = function(options)
	{
		$sb.FontSize.initializeBase(this, [$.extend({_class: $sb.FontSize}, options)]);
		this._absolute = options.absolute;
		this._relative = options.relative;
		this._absoluteButton = options.absoluteButton;
		this._relativeButton = options.relativeButton;
		this._attachButtons();
		this._changeModeHandler();
	}
	$sb.FontSize.prototype =
	{
		get_value: function()
		{
			return this.getActiveMode().get_value();
		},
		set_value: function(value)
		{
			this.getActiveMode().set_value(value);
		},
		clear_value: function()
		{
			this._absolute.clear_value();
			this._relative.clear_value();
		},
		_attachButtons: function()
		{
			var changeModeHandler = Function.createDelegate(this, this._changeModeHandler);
			$addHandler(this._absoluteButton, "click", changeModeHandler);
			$addHandler(this._relativeButton, "click", changeModeHandler);
		},
		_changeModeHandler: function()
		{
			this.changeMode($('input:radio[name=fSizeType]:checked').val());
		},
		changeMode: function(mode)
		{
			this._absolute.set_enabled(mode == "absolute");
			this._relative.set_enabled(!this._absolute.get_enabled());
		},
		getActiveMode: function()
		{
			return this._absolute.get_enabled() ? this._absolute : this._relative;
		},
		dispose: function()
		{
			this.disposeControl("_absolute");
			this.disposeControl("_relative");
			if (this._absoluteButton) $clearHandlers(this._absoluteButton);
			if (this._relativeButton) $clearHandlers(this._relativeButton);
			delete this._absoluteButton;
			delete this._relativeButton;
		}
	};
	$sb.FontSize.registerClass("Telerik.Web.UI.Widgets.StyleBuilder.FontSize", $sb.Control);

	$sb.BackgroundColor = function(options)
	{
		$sb.BackgroundColor.initializeBase(this, [$.extend({_class: $sb.BackgroundColor}, options)]);
		this._color = options.color;
		this._transparent = options.transparent;
		this._color._picker.add_valueSelected(Function.createDelegate(this, function() { this._transparent.clear_value(); }));
		this._transparent._button.add_clicked(Function.createDelegate(this, function(button) { if(button.get_checked()) this._color.clear_value(); }));
	}
	$sb.BackgroundColor.prototype =
	{
		get_value: function()
		{
			return this._transparent.get_value() || this._color.get_value();
		},
		set_value: function(value)
		{
			if(value == "transparent")
			{
				this._transparent.set_value(true);
				this._color.clear_value();
			}
			else
			{
				this._transparent.set_value(false);
				this._color.set_value(value);
			}
		},
		clear_value: function()
		{
			this.set_value("");
		},
		dispose: function()
		{
			this.disposeControl("_color");
			this.disposeControl("_transparent");
		}
	};
	$sb.BackgroundColor.registerClass("Telerik.Web.UI.Widgets.StyleBuilder.BackgroundColor", $sb.Control);

	})($telerik.$, Telerik.Web.UI.Widgets.StyleBuilder);

	if(!String.prototype.capitalize)
		String.prototype.capitalize = function()
		{
			var firstLetter = this.substring(0, 1);
			return firstLetter.toUpperCase() + this.substring(1);
		}

	Telerik.Web.UI.Widgets.StyleBuilder.Helpers = {
		addComboBoxArrayItems: function(combo, array)
		{
			this._addComboBoxItems(combo, array, this._singleItemValueDecorator);
		},
		addComboBoxItems: function(combo, items)
		{
			this._addComboBoxItems(combo, items, this._itemValueDecorator);
		},
		_addComboBoxItems: function(combo, items, itemDecorator)
		{
			var options = combo.get_items();
		
			combo.trackChanges();
			for(var i = 0; i < items.length; i++)
				this.addComboBoxItemToCollection(options, itemDecorator(items[i]));
			combo.commitChanges();
		},
		addComboBoxItemToCollection: function(collection, item)
		{
			var option = new Telerik.Web.UI.RadComboBoxItem();
				option.set_value(item.value);
				option.set_text(item.text);
			
				collection.add(option);
		},
		_singleItemValueDecorator: function(value) {
			return {value: value, text: value};
		},
		_itemValueDecorator: function(item) {
			return item;
		},
		toStyleName: function(cssName)
		{
			var words = cssName.split("-");
			var styleName = words.shift();
			while(words.length)
				styleName += words.shift().capitalize();

			return styleName;
		}
	};

	function $findSB(key)
	{
		return $find(key);
	}
</script>
<telerik:RadTabStrip ID="styleBuilderTabs" runat="server" Orientation="VerticalLeft" MultiPageID="styleBuilderContents" SelectedIndex="0">
	<Tabs>
		<telerik:RadTab Text="Font" />
		<telerik:RadTab Text="Background" />
		<telerik:RadTab Text="Text" />
		<telerik:RadTab Text="Layout" />
		<telerik:RadTab Text="Box" />
		<telerik:RadTab Text="Border" />
		<telerik:RadTab Text="Lists" />
	</Tabs>
</telerik:RadTabStrip>
<div id="sbMainPane">
	<telerik:RadMultiPage ID="styleBuilderContents" runat="server" SelectedIndex="0">
		<telerik:RadPageView ID="FontContent" runat="server">
		<fieldset class="reStyleBuilderFonts">
				<legend>
					<script type="text/javascript">document.write(localization["FontFamily"]);</script>
				</legend>
			<div>
				<label for="fontFamily" class="inlineLabel">
					<script type="text/javascript">document.write(localization["FontFamily"]);</script>:
				</label>
				<div style="display: inline-block; vertical-align: middle;">
					<telerik:RadComboBox ID="fontFamily" runat="server" Width="200" />
				</div>
			</div>
			<div>
				<label for="color" class="inlineLabel floater">
					<script type="text/javascript">document.write(localization["Color"]);</script>:
				</label>
				<div style="float: left;">
					<tools:ColorPicker ID="color" runat="server" CssClass="ColorPicker"></tools:ColorPicker>
				</div>
			</div>
			</fieldset>
			<fieldset class="reStyleBuilderSize">
				<legend>
					<script type="text/javascript">document.write(localization["FontSize"]);</script>
				</legend>
				<ul>
					<li>
						<input type="radio" id="fSizeAbsolute" name="fSizeType" value="absolute" checked="checked" />
						<div class="reInlineBlock reMetroTouchFix"><telerik:RadNumericTextBox ShouldResetWidthInPixels="false" ShowSpinButtons="true" IncrementSettings-InterceptArrowKeys="true" IncrementSettings-InterceptMouseWheel="true" runat="server" ID="fontSizeValue" Width="135px" MinValue="0" MaxValue="999" style="text-align: right;" Label="Absolute:">
							<NumberFormat DecimalDigits="0" />
						</telerik:RadNumericTextBox></div>
						<div class="reInlineBlock" style="*margin-left: 20px;"><telerik:RadComboBox ID="fontSizeMetric" runat="server" Width="50">
							<Items>
								<telerik:RadComboBoxItem Text="px" Value="px" />
								<telerik:RadComboBoxItem Text="pc" Value="pc" />
								<telerik:RadComboBoxItem Text="pt" Value="pt" />
								<telerik:RadComboBoxItem Text="mm" Value="mm" />
								<telerik:RadComboBoxItem Text="cm" Value="cm" />
								<telerik:RadComboBoxItem Text="in" Value="in" />
								<telerik:RadComboBoxItem Text="em" Value="em" />
								<telerik:RadComboBoxItem Text="ex" Value="ex" />
								<telerik:RadComboBoxItem Text="%" Value="%" />
							</Items>
						</telerik:RadComboBox></div>
					</li>
					<li>
						<input type="radio" id="fSizeRelative" name="fSizeType" value="relative" />
						<telerik:RadComboBox ID="fontSizeRelative" runat="server" Width="100" Label="Relative:"></telerik:RadComboBox>
					</li>
				</ul>
			</fieldset>
			<fieldset id="fontAttributes" class="reStyleBuilderFontAttr">
				<legend>
					<script type="text/javascript">document.write(localization["FontAttributes"]);</script>
				</legend>
				<div style="float: left;">
					<span>
						<script type="text/javascript">document.write(localization["Bold"]);</script>
					</span>
					<telerik:RadComboBox ID="fontBold" runat="server" Width="90"></telerik:RadComboBox>
				</div>
				<div style="float: left; margin-left: 3px;">
					<span>
						<script type="text/javascript">document.write(localization["Italics"]);</script>
					</span>
					<telerik:RadComboBox ID="fontStyle" runat="server" Width="90"></telerik:RadComboBox>
				</div>
				<div style="float: left; margin-left: 3px;">
					<span>
						<script type="text/javascript">document.write(localization["SmallCaps"]);</script>
					</span>
					<telerik:RadComboBox ID="fontVariant" runat="server" Width="90"></telerik:RadComboBox>
				</div>
				<div style="float: left; margin-left: 3px;">
					<span>
						<script type="text/javascript">document.write(localization["Capitalization"]);</script>
					</span>
					<telerik:RadComboBox ID="Capitalization" runat="server" Width="90"></telerik:RadComboBox>
				</div>
			</fieldset>
			<fieldset class="reStyleBuilderEffects">
				<legend>
					<script type="text/javascript">document.write(localization["Effects"]);</script>
				</legend>
				<telerik:RadButton ID="fdUnderline" runat="server" ToggleType="CheckBox" ButtonType="ToggleButton" Text="Underline" CommandName="underline" AutoPostBack="false" />
				<telerik:RadButton ID="fdStrikethrough" runat="server" ToggleType="CheckBox" ButtonType="ToggleButton" Text="Strikethrough" CommandName="line-through" AutoPostBack="false" />
				<telerik:RadButton ID="fdOverline" runat="server" ToggleType="CheckBox" ButtonType="ToggleButton" Text="Overline" CommandName="overline" AutoPostBack="false" />
			</fieldset>
		</telerik:RadPageView>
		<telerik:RadPageView ID="BackgroundContent" runat="server">
			<fieldset class="reStyleBuilderBgColor">
				<legend>
					<script type="text/javascript">document.write(localization["BackgroundColor"]);</script>
				</legend>
				<div>
					<label class="reInlineBlock" for="backgroundColor">
						<script type="text/javascript">document.write(localization["Color"]);</script>
					</label>
					<div class="reInlineBlock">
						<tools:ColorPicker ID="backgroundColor" runat="server"></tools:ColorPicker>
					</div>
					<div class="reInlineBlock">or</div>
					<div class="reInlineBlock"><telerik:RadButton ID="transparent" runat="server" Text="Transparent" ToggleType="CheckBox" ButtonType="ToggleButton" CommandName="transparent" AutoPostBack="false" /></div>
				</div>
			</fieldset>
			<fieldset class="reStyleBuilderBgImg">
				<legend>
					<script type="text/javascript">document.write(localization["BackgroundImage"]);</script>
				</legend>
				<ul id="background">
					<li>
						<label class="sLabel floater">
							<script type="text/javascript">document.write(localization["Image"]);</script>
						</label>
						<div class="reInlineBlock">
							<tools:ImageDialogCaller ID="backgroundImage" runat="server" />
						</div>
					</li>
					<li>
						<label for="backgroundRepeat" class="sLabel">
							<script type="text/javascript">document.write(localization["Tiling"]);</script>
						</label>
						<telerik:RadComboBox ID="backgroundRepeat" runat="server" />
					</li>
					<li>
						<label for="backgroundAttachment" class="sLabel">
							<script type="text/javascript">document.write(localization["Scrolling"]);</script>
						</label>
						<telerik:RadComboBox ID="backgroundAttachment" runat="server" />
					</li>
				</ul>
				<fieldset>
					<legend>
						<script type="text/javascript">document.write(localization["Position"]);</script>
					</legend>
					<ul id="backgroundPosition">
						<li>
							<label for="positionHorizontal" class="sLabel">
								<script type="text/javascript">document.write(localization["Horizontal"]);</script>
							</label>
							<telerik:RadComboBox ID="positionHorizontal" runat="server" Width="90" />
							<telerik:RadNumericTextBox ShouldResetWidthInPixels="false" ShowSpinButtons="true" IncrementSettings-InterceptArrowKeys="true" IncrementSettings-InterceptMouseWheel="true" runat="server" ID="positionHorizontalValue" Width="75px" style="text-align: right;">
								<NumberFormat DecimalDigits="0" />
							</telerik:RadNumericTextBox>
							<telerik:RadComboBox ID="positionHorizontalMetric" runat="server" Width="50">
								<Items>
									<telerik:RadComboBoxItem Text="px" Value="px" />
									<telerik:RadComboBoxItem Text="pc" Value="pc" />
									<telerik:RadComboBoxItem Text="pt" Value="pt" />
									<telerik:RadComboBoxItem Text="mm" Value="mm" />
									<telerik:RadComboBoxItem Text="cm" Value="cm" />
									<telerik:RadComboBoxItem Text="in" Value="in" />
									<telerik:RadComboBoxItem Text="em" Value="em" />
									<telerik:RadComboBoxItem Text="ex" Value="ex" />
									<telerik:RadComboBoxItem Text="%" Value="%" />
								</Items>
							</telerik:RadComboBox>
						</li>
						<li>
							<label for="positionVertical" class="sLabel">
								<script type="text/javascript">document.write(localization["Vertical"]);</script>
							</label>
							<telerik:RadComboBox ID="positionVertical" runat="server" Width="90" />
							<telerik:RadNumericTextBox ShouldResetWidthInPixels="false" ShowSpinButtons="true" IncrementSettings-InterceptArrowKeys="true" IncrementSettings-InterceptMouseWheel="true" runat="server" ID="positionVerticalValue" Width="75px" style="text-align: right;">
								<NumberFormat DecimalDigits="0" />
							</telerik:RadNumericTextBox>
							<telerik:RadComboBox ID="positionVerticalMetric" runat="server" Width="50">
								<Items>
									<telerik:RadComboBoxItem Text="px" Value="px" />
									<telerik:RadComboBoxItem Text="pc" Value="pc" />
									<telerik:RadComboBoxItem Text="pt" Value="pt" />
									<telerik:RadComboBoxItem Text="mm" Value="mm" />
									<telerik:RadComboBoxItem Text="cm" Value="cm" />
									<telerik:RadComboBoxItem Text="in" Value="in" />
									<telerik:RadComboBoxItem Text="em" Value="em" />
									<telerik:RadComboBoxItem Text="ex" Value="ex" />
									<telerik:RadComboBoxItem Text="%" Value="%" />
								</Items>
							</telerik:RadComboBox>
						</li>
					</ul>
				</fieldset>
			</fieldset>
		</telerik:RadPageView>
		<telerik:RadPageView ID="TextContent" runat="server">
			<fieldset class="reStyleBuilderTextAlignment">
				<legend>
					<script type="text/javascript">document.write(localization["Alignment"]);</script>
				</legend>
				<ul>
					<li id="textAlignContainer">
				
						<label class="sLabel">
							<script type="text/javascript">document.write(localization["Horizontal"]);</script>
						</label>
						<telerik:RadComboBox ID="textAlign" runat="server" />
					</li>
					<li id="verticalAlignContainer">
						<label class="sLabel">
							<script type="text/javascript">document.write(localization["Vertical"]);</script>
						</label>
						<telerik:RadComboBox ID="verticalAlign" runat="server" />
					</li>
				</ul>
			</fieldset>
			<fieldset class="reStyleBuilderTextSpacing">
				<legend>
					<script type="text/javascript">document.write(localization["SpacingBetween"]);</script>
				</legend>
				<ul>
					<li id="letterSpacingContainer">
						<label for="Letters" class="sLabel">
							<script type="text/javascript">document.write(localization["Letters"]);</script>
						</label>
						<telerik:RadComboBox ID="letterSpacing" runat="server" />
						<telerik:RadNumericTextBox ShouldResetWidthInPixels="false" ShowSpinButtons="true" IncrementSettings-InterceptArrowKeys="true" IncrementSettings-InterceptMouseWheel="true" runat="server" ID="letterSpacingValue" Width="55px" MinValue="0" MaxValue="999" style="text-align: right;">
							<NumberFormat DecimalDigits="0" />
						</telerik:RadNumericTextBox>
						<telerik:RadComboBox ID="letterSpacingMetric" runat="server" Width="50">
							<Items>
								<telerik:RadComboBoxItem Text="px" Value="px" />
								<telerik:RadComboBoxItem Text="pc" Value="pc" />
								<telerik:RadComboBoxItem Text="pt" Value="pt" />
								<telerik:RadComboBoxItem Text="mm" Value="mm" />
								<telerik:RadComboBoxItem Text="cm" Value="cm" />
								<telerik:RadComboBoxItem Text="in" Value="in" />
								<telerik:RadComboBoxItem Text="em" Value="em" />
								<telerik:RadComboBoxItem Text="ex" Value="ex" />
								<telerik:RadComboBoxItem Text="%" Value="%" />
							</Items>
						</telerik:RadComboBox>
					</li>
					<li id="wordSpacingContainer">
						<label for="Letters" class="sLabel">
							<script type="text/javascript">document.write(localization["Words"]);</script>
						</label>
						<telerik:RadComboBox ID="wordSpacing" runat="server" />
						<telerik:RadNumericTextBox ShouldResetWidthInPixels="false" ShowSpinButtons="true" IncrementSettings-InterceptArrowKeys="true" IncrementSettings-InterceptMouseWheel="true" runat="server" ID="wordSpacingValue" Width="55px" MinValue="0" MaxValue="999" style="text-align: right;">
							<NumberFormat DecimalDigits="0" />
						</telerik:RadNumericTextBox>
						<telerik:RadComboBox ID="wordSpacingMetric" runat="server" Width="50">
							<Items>
								<telerik:RadComboBoxItem Text="px" Value="px" />
								<telerik:RadComboBoxItem Text="pc" Value="pc" />
								<telerik:RadComboBoxItem Text="pt" Value="pt" />
								<telerik:RadComboBoxItem Text="mm" Value="mm" />
								<telerik:RadComboBoxItem Text="cm" Value="cm" />
								<telerik:RadComboBoxItem Text="in" Value="in" />
								<telerik:RadComboBoxItem Text="em" Value="em" />
								<telerik:RadComboBoxItem Text="ex" Value="ex" />
								<telerik:RadComboBoxItem Text="%" Value="%" />
							</Items>
						</telerik:RadComboBox>
					</li>
					<li id="lineHeightContainer">
						<label for="Letters" class="sLabel">
							<script type="text/javascript">document.write(localization["Lines"]);</script>
						</label>
						<telerik:RadComboBox ID="lineHeight" runat="server" />
						<telerik:RadNumericTextBox ShouldResetWidthInPixels="false" ShowSpinButtons="true" IncrementSettings-InterceptArrowKeys="true" IncrementSettings-InterceptMouseWheel="true" runat="server" ID="lineHeightValue" Width="55px" MinValue="0" MaxValue="999" style="text-align: right;">
							<NumberFormat DecimalDigits="0" />
						</telerik:RadNumericTextBox>
						<telerik:RadComboBox ID="lineHeightMetric" runat="server" Width="50">
							<Items>
								<telerik:RadComboBoxItem Text="px" Value="px" />
								<telerik:RadComboBoxItem Text="pc" Value="pc" />
								<telerik:RadComboBoxItem Text="pt" Value="pt" />
								<telerik:RadComboBoxItem Text="mm" Value="mm" />
								<telerik:RadComboBoxItem Text="cm" Value="cm" />
								<telerik:RadComboBoxItem Text="in" Value="in" />
								<telerik:RadComboBoxItem Text="em" Value="em" />
								<telerik:RadComboBoxItem Text="ex" Value="ex" />
								<telerik:RadComboBoxItem Text="%" Value="%" />
							</Items>
						</telerik:RadComboBox>
					</li>
				</ul>
			</fieldset>
			<fieldset class="reStyleBuilderTextFlow">
				<legend>
					<script type="text/javascript">document.write(localization["TextFlow"]);</script>
				</legend>
				<ul>
					<li id="textIndent">
						<label class="sLabel">
							<script type="text/javascript">document.write(localization["Indentation"]);</script>
						</label>
						<telerik:RadNumericTextBox ShouldResetWidthInPixels="false" ShowSpinButtons="true" IncrementSettings-InterceptArrowKeys="true" IncrementSettings-InterceptMouseWheel="true" runat="server" ID="textIndentValue" Width="75px" style="text-align: right;">
							<NumberFormat DecimalDigits="0" />
						</telerik:RadNumericTextBox>
						<telerik:RadComboBox ID="textIndentMetric" runat="server" Width="50">
							<Items>
								<telerik:RadComboBoxItem Text="px" Value="px" />
								<telerik:RadComboBoxItem Text="pc" Value="pc" />
								<telerik:RadComboBoxItem Text="pt" Value="pt" />
								<telerik:RadComboBoxItem Text="mm" Value="mm" />
								<telerik:RadComboBoxItem Text="cm" Value="cm" />
								<telerik:RadComboBoxItem Text="in" Value="in" />
								<telerik:RadComboBoxItem Text="em" Value="em" />
								<telerik:RadComboBoxItem Text="ex" Value="ex" />
								<telerik:RadComboBoxItem Text="%" Value="%" />
							</Items>
						</telerik:RadComboBox>
					</li>
					<li id="direction">
						<label class="sLabel">
							<script type="text/javascript">document.write(localization["TextDirection"]);</script>
						</label>
						<telerik:RadComboBox ID="textDecoration" runat="server" />
					</li>
				</ul>
			</fieldset>
		</telerik:RadPageView>
		<telerik:RadPageView ID="LayoutContent" runat="server">
			<fieldset class="reStyleBuilderLayoutFlow">
				<legend>
					<script type="text/javascript">document.write(localization["FlowControl"]);</script>
				</legend>
				<ul>
					<li id="displayContainer">
						<label for="display" class="sLabel">
							<script type="text/javascript">document.write(localization["Display"]);</script>
						</label>
						<telerik:RadComboBox ID="display" runat="server" />
					</li>
					<li id="visibilityContainer">
						<label for="visibility" class="sLabel">
							<script type="text/javascript">document.write(localization["Visibility"]);</script>
						</label>
						<telerik:RadComboBox ID="visibility" runat="server" />
					</li>
					<li id="floatContainer">
						<label for="float" class="sLabel">
							Float
						</label>
						<telerik:RadComboBox ID="float" runat="server" />
					</li>
					<li id="clearContainer">
						<label for="clear" class="sLabel">
							Clear
						</label>
						<telerik:RadComboBox ID="clear" runat="server" />
					</li>
				</ul>
			</fieldset>
			<fieldset class="reStyleBuilderLayoutContent">
				<legend>
					<script type="text/javascript">document.write(localization["Content"]);</script>
				</legend>
				<ul>
					<li id="overflowContainer">
						<label for="overflow" class="sLabel">
							<script type="text/javascript">document.write(localization["Overflow"]);</script>
						</label>
						<telerik:RadComboBox ID="overflow" runat="server" />
					</li>
				</ul>
				<fieldset>
					<legend>
						<script type="text/javascript">document.write(localization["Clipping"]);</script>
					</legend>
					<ul id="clip">
						<li style="float: left;">
							<label for="" class="columnLabel">
								<script type="text/javascript">document.write(localization["Top"]);</script>
							</label>
							<telerik:RadNumericTextBox ShouldResetWidthInPixels="false" ShowSpinButtons="true" IncrementSettings-InterceptArrowKeys="true" IncrementSettings-InterceptMouseWheel="true" runat="server" ID="clipTop" Width="60px" MinValue="0" MaxValue="99999" style="text-align: right;">
								<NumberFormat DecimalDigits="0" />
							</telerik:RadNumericTextBox>
							<telerik:RadComboBox ID="clipTopMetric" runat="server" Width="50">
								<Items>
									<telerik:RadComboBoxItem Text="px" Value="px" />
									<telerik:RadComboBoxItem Text="pc" Value="pc" />
									<telerik:RadComboBoxItem Text="pt" Value="pt" />
									<telerik:RadComboBoxItem Text="mm" Value="mm" />
									<telerik:RadComboBoxItem Text="cm" Value="cm" />
									<telerik:RadComboBoxItem Text="in" Value="in" />
									<telerik:RadComboBoxItem Text="em" Value="em" />
									<telerik:RadComboBoxItem Text="ex" Value="ex" />
									<telerik:RadComboBoxItem Text="%" Value="%" />
								</Items>
							</telerik:RadComboBox>
						</li>
						<li>
							<label for="" class="columnLabel">
								<script type="text/javascript">document.write(localization["Bottom"]);</script>
							</label>
							<telerik:RadNumericTextBox ShouldResetWidthInPixels="false" ShowSpinButtons="true" IncrementSettings-InterceptArrowKeys="true" IncrementSettings-InterceptMouseWheel="true" runat="server" ID="clipBottom" Width="60px" MinValue="0" MaxValue="99999" style="text-align: right;">
								<NumberFormat DecimalDigits="0" />
							</telerik:RadNumericTextBox>
							<telerik:RadComboBox ID="clipBottomMetric" runat="server" Width="50">
								<Items>
									<telerik:RadComboBoxItem Text="px" Value="px" />
									<telerik:RadComboBoxItem Text="pc" Value="pc" />
									<telerik:RadComboBoxItem Text="pt" Value="pt" />
									<telerik:RadComboBoxItem Text="mm" Value="mm" />
									<telerik:RadComboBoxItem Text="cm" Value="cm" />
									<telerik:RadComboBoxItem Text="in" Value="in" />
									<telerik:RadComboBoxItem Text="em" Value="em" />
									<telerik:RadComboBoxItem Text="ex" Value="ex" />
									<telerik:RadComboBoxItem Text="%" Value="%" />
								</Items>
							</telerik:RadComboBox>
						</li>
						<li style="float: left;">
							<label for="" class="columnLabel">
								<script type="text/javascript">document.write(localization["Left"]);</script>
							</label>
							<telerik:RadNumericTextBox ShouldResetWidthInPixels="false" ShowSpinButtons="true" IncrementSettings-InterceptArrowKeys="true" IncrementSettings-InterceptMouseWheel="true" runat="server" ID="clipLeft" Width="60px" MinValue="0" MaxValue="99999" style="text-align: right;">
								<NumberFormat DecimalDigits="0" />
							</telerik:RadNumericTextBox>
							<telerik:RadComboBox ID="clipLeftMetric" runat="server" Width="50">
								<Items>
									<telerik:RadComboBoxItem Text="px" Value="px" />
									<telerik:RadComboBoxItem Text="pc" Value="pc" />
									<telerik:RadComboBoxItem Text="pt" Value="pt" />
									<telerik:RadComboBoxItem Text="mm" Value="mm" />
									<telerik:RadComboBoxItem Text="cm" Value="cm" />
									<telerik:RadComboBoxItem Text="in" Value="in" />
									<telerik:RadComboBoxItem Text="em" Value="em" />
									<telerik:RadComboBoxItem Text="ex" Value="ex" />
									<telerik:RadComboBoxItem Text="%" Value="%" />
								</Items>
							</telerik:RadComboBox>
						</li>
						<li>
							<label for="" class="columnLabel">
								<script type="text/javascript">document.write(localization["Right"]);</script>
							</label>
							<telerik:RadNumericTextBox ShouldResetWidthInPixels="false" ShowSpinButtons="true" IncrementSettings-InterceptArrowKeys="true" IncrementSettings-InterceptMouseWheel="true" runat="server" ID="clipRight" Width="60px" MinValue="0" MaxValue="99999" style="text-align: right;">
								<NumberFormat DecimalDigits="0" />
							</telerik:RadNumericTextBox>
							<telerik:RadComboBox ID="clipRightMetric" runat="server" Width="50">
								<Items>
									<telerik:RadComboBoxItem Text="px" Value="px" />
									<telerik:RadComboBoxItem Text="pc" Value="pc" />
									<telerik:RadComboBoxItem Text="pt" Value="pt" />
									<telerik:RadComboBoxItem Text="mm" Value="mm" />
									<telerik:RadComboBoxItem Text="cm" Value="cm" />
									<telerik:RadComboBoxItem Text="in" Value="in" />
									<telerik:RadComboBoxItem Text="em" Value="em" />
									<telerik:RadComboBoxItem Text="ex" Value="ex" />
									<telerik:RadComboBoxItem Text="%" Value="%" />
								</Items>
							</telerik:RadComboBox>
						</li>
					</ul>
				</fieldset>
				<fieldset style="display: none;">
					<legend>
						Printing page breaks
					</legend>
					<ul>
						<li>
							<label for="Before" style="padding-right: 63px;">
								Before:</label>
							<select id="Before" class="xl_input">
								<option selected="selected"></option>
							</select>
						</li>
						<li>
							<label for="After" style="padding-right: 75px;">
								After:</label>
							<select id="After" class="m_input">
								<option selected="selected"></option>
							</select>
						</li>
					</ul>
				</fieldset>
			</fieldset>
		</telerik:RadPageView>
		<telerik:RadPageView ID="BoxContent" runat="server">
		<fieldset class="reStyleBuilderBoxSize">
			<legend>
					<script type="text/javascript">document.write(localization[""]);</script>
			</legend>
			<div id="BoxWidthContainer">
			
				<label for="BoxWidth" class="sLabel">
					Width
				</label>
				<telerik:RadNumericTextBox ShouldResetWidthInPixels="false" ShowSpinButtons="true" IncrementSettings-InterceptArrowKeys="true" IncrementSettings-InterceptMouseWheel="true" runat="server" ID="width" Width="70px" MinValue="0" style="text-align: right;">
					<NumberFormat DecimalDigits="0" />
				</telerik:RadNumericTextBox>
				<telerik:RadComboBox ID="widthMetric" runat="server" Width="50">
					<Items>
						<telerik:RadComboBoxItem Text="px" Value="px" />
						<telerik:RadComboBoxItem Text="pc" Value="pc" />
						<telerik:RadComboBoxItem Text="pt" Value="pt" />
						<telerik:RadComboBoxItem Text="mm" Value="mm" />
						<telerik:RadComboBoxItem Text="cm" Value="cm" />
						<telerik:RadComboBoxItem Text="in" Value="in" />
						<telerik:RadComboBoxItem Text="em" Value="em" />
						<telerik:RadComboBoxItem Text="ex" Value="ex" />
						<telerik:RadComboBoxItem Text="%" Value="%" />
					</Items>
				</telerik:RadComboBox>
			</div>
			<div id="BoxHeightContainer">
				<label for="BoxHeight" class="sLabel">
					Height
				</label>
				<telerik:RadNumericTextBox ShouldResetWidthInPixels="false" ShowSpinButtons="true" IncrementSettings-InterceptArrowKeys="true" IncrementSettings-InterceptMouseWheel="true" runat="server" ID="height" Width="70px" MinValue="0" style="text-align: right;">
					<NumberFormat DecimalDigits="0" />
				</telerik:RadNumericTextBox>
				<telerik:RadComboBox ID="heightMetric" runat="server" Width="50">
					<Items>
						<telerik:RadComboBoxItem Text="px" Value="px" />
						<telerik:RadComboBoxItem Text="pc" Value="pc" />
						<telerik:RadComboBoxItem Text="pt" Value="pt" />
						<telerik:RadComboBoxItem Text="mm" Value="mm" />
						<telerik:RadComboBoxItem Text="cm" Value="cm" />
						<telerik:RadComboBoxItem Text="in" Value="in" />
						<telerik:RadComboBoxItem Text="em" Value="em" />
						<telerik:RadComboBoxItem Text="ex" Value="ex" />
						<telerik:RadComboBoxItem Text="%" Value="%" />
					</Items>
				</telerik:RadComboBox>
			</div></fieldset>
			<fieldset style="float: left; width:190px; margin-right: 4px;" class="reStyleBuilderBoxPadding">
				<legend>
					<script type="text/javascript">document.write(localization["Padding"]);</script>
				</legend>
				<div style="text-align: center;">
					<telerik:RadButton ID="samePaddingForAll" runat="server" ToggleType="CheckBox" ButtonType="ToggleButton" Text="Same for all" AutoPostBack="false" />
				</div>
				<ul>
					<li id="Li1">
						<label for="PaddingTop" class="columnLabel">
							<script type="text/javascript">document.write(localization["Top"]);</script>
						</label>
						<telerik:RadNumericTextBox ShouldResetWidthInPixels="false" ShowSpinButtons="true" IncrementSettings-InterceptArrowKeys="true" IncrementSettings-InterceptMouseWheel="true" runat="server" ID="paddingTop" Width="65px" style="text-align: right;">
							<NumberFormat DecimalDigits="0" />
						</telerik:RadNumericTextBox>
						<telerik:RadComboBox ID="paddingTopMetric" runat="server" Width="50">
							<Items>
								<telerik:RadComboBoxItem Text="px" Value="px" />
								<telerik:RadComboBoxItem Text="pc" Value="pc" />
								<telerik:RadComboBoxItem Text="pt" Value="pt" />
								<telerik:RadComboBoxItem Text="mm" Value="mm" />
								<telerik:RadComboBoxItem Text="cm" Value="cm" />
								<telerik:RadComboBoxItem Text="in" Value="in" />
								<telerik:RadComboBoxItem Text="em" Value="em" />
								<telerik:RadComboBoxItem Text="ex" Value="ex" />
								<telerik:RadComboBoxItem Text="%" Value="%" />
							</Items>
						</telerik:RadComboBox>
					</li>
					<li id="Li2">
						<label for="PaddingBottom" class="columnLabel">
							<script type="text/javascript">document.write(localization["Bottom"]);</script>
						</label>
						<telerik:RadNumericTextBox ShouldResetWidthInPixels="false" ShowSpinButtons="true" IncrementSettings-InterceptArrowKeys="true" IncrementSettings-InterceptMouseWheel="true" runat="server" ID="paddingBottom" Width="65px" style="text-align: right;">
							<NumberFormat DecimalDigits="0" />
						</telerik:RadNumericTextBox>
						<telerik:RadComboBox ID="paddingBottomMetric" runat="server" Width="50">
							<Items>
								<telerik:RadComboBoxItem Text="px" Value="px" />
								<telerik:RadComboBoxItem Text="pc" Value="pc" />
								<telerik:RadComboBoxItem Text="pt" Value="pt" />
								<telerik:RadComboBoxItem Text="mm" Value="mm" />
								<telerik:RadComboBoxItem Text="cm" Value="cm" />
								<telerik:RadComboBoxItem Text="in" Value="in" />
								<telerik:RadComboBoxItem Text="em" Value="em" />
								<telerik:RadComboBoxItem Text="ex" Value="ex" />
								<telerik:RadComboBoxItem Text="%" Value="%" />
							</Items>
						</telerik:RadComboBox>
					</li>
					<li id="Li3">
						<label for="PaddingLeft" class="columnLabel">
							<script type="text/javascript">document.write(localization["Left"]);</script>
						</label>
						<telerik:RadNumericTextBox ShouldResetWidthInPixels="false" ShowSpinButtons="true" IncrementSettings-InterceptArrowKeys="true" IncrementSettings-InterceptMouseWheel="true" runat="server" ID="paddingLeft" Width="65px" style="text-align: right;">
							<NumberFormat DecimalDigits="0" />
						</telerik:RadNumericTextBox>
						<telerik:RadComboBox ID="paddingLeftMetric" runat="server" Width="50">
							<Items>
								<telerik:RadComboBoxItem Text="px" Value="px" />
								<telerik:RadComboBoxItem Text="pc" Value="pc" />
								<telerik:RadComboBoxItem Text="pt" Value="pt" />
								<telerik:RadComboBoxItem Text="mm" Value="mm" />
								<telerik:RadComboBoxItem Text="cm" Value="cm" />
								<telerik:RadComboBoxItem Text="in" Value="in" />
								<telerik:RadComboBoxItem Text="em" Value="em" />
								<telerik:RadComboBoxItem Text="ex" Value="ex" />
								<telerik:RadComboBoxItem Text="%" Value="%" />
							</Items>
						</telerik:RadComboBox>
					</li>
					<li id="Li4">
						<label for="PaddingRight" class="columnLabel">
							<script type="text/javascript">document.write(localization["Right"]);</script>
						</label>
						<telerik:RadNumericTextBox ShouldResetWidthInPixels="false" ShowSpinButtons="true" IncrementSettings-InterceptArrowKeys="true" IncrementSettings-InterceptMouseWheel="true" runat="server" ID="paddingRight" Width="65px" style="text-align: right;">
							<NumberFormat DecimalDigits="0" />
						</telerik:RadNumericTextBox>
						<telerik:RadComboBox ID="paddingRightMetric" runat="server" Width="50">
							<Items>
								<telerik:RadComboBoxItem Text="px" Value="px" />
								<telerik:RadComboBoxItem Text="pc" Value="pc" />
								<telerik:RadComboBoxItem Text="pt" Value="pt" />
								<telerik:RadComboBoxItem Text="mm" Value="mm" />
								<telerik:RadComboBoxItem Text="cm" Value="cm" />
								<telerik:RadComboBoxItem Text="in" Value="in" />
								<telerik:RadComboBoxItem Text="em" Value="em" />
								<telerik:RadComboBoxItem Text="ex" Value="ex" />
								<telerik:RadComboBoxItem Text="%" Value="%" />
							</Items>
						</telerik:RadComboBox>
					</li>
				</ul>
			</fieldset>
			<fieldset style="float: left; width:190px;" class="reStyleBuilderBoxMargin">
				<legend>
					<script type="text/javascript">document.write(localization["Margin"]);</script>
				</legend>
				<div style="text-align: center;">
					<telerik:RadButton ID="sameMarginForAll" runat="server" ToggleType="CheckBox" ButtonType="ToggleButton" Text="Same for all" AutoPostBack="false" />
				</div>
				<ul>
					<li>
						<label for="MarginTop" class="columnLabel">
							<script type="text/javascript">document.write(localization["Top"]);</script>
						</label>
						<telerik:RadNumericTextBox ShouldResetWidthInPixels="false" ShowSpinButtons="true" IncrementSettings-InterceptArrowKeys="true" IncrementSettings-InterceptMouseWheel="true" runat="server" ID="marginTop" Width="65px" style="text-align: right;">
							<NumberFormat DecimalDigits="0" />
						</telerik:RadNumericTextBox>
						<telerik:RadComboBox ID="marginTopMetric" runat="server" Width="50">
							<Items>
								<telerik:RadComboBoxItem Text="px" Value="px" />
								<telerik:RadComboBoxItem Text="pc" Value="pc" />
								<telerik:RadComboBoxItem Text="pt" Value="pt" />
								<telerik:RadComboBoxItem Text="mm" Value="mm" />
								<telerik:RadComboBoxItem Text="cm" Value="cm" />
								<telerik:RadComboBoxItem Text="in" Value="in" />
								<telerik:RadComboBoxItem Text="em" Value="em" />
								<telerik:RadComboBoxItem Text="ex" Value="ex" />
								<telerik:RadComboBoxItem Text="%" Value="%" />
							</Items>
						</telerik:RadComboBox>
					</li>
					<li>
						<label for="MarginBottom" class="columnLabel">
							<script type="text/javascript">document.write(localization["Bottom"]);</script>
						</label>
						<telerik:RadNumericTextBox ShouldResetWidthInPixels="false" ShowSpinButtons="true" IncrementSettings-InterceptArrowKeys="true" IncrementSettings-InterceptMouseWheel="true" runat="server" ID="marginBottom" Width="65px" style="text-align: right;">
							<NumberFormat DecimalDigits="0" />
						</telerik:RadNumericTextBox>
						<telerik:RadComboBox ID="marginBottomMetric" runat="server" Width="50">
							<Items>
								<telerik:RadComboBoxItem Text="px" Value="px" />
								<telerik:RadComboBoxItem Text="pc" Value="pc" />
								<telerik:RadComboBoxItem Text="pt" Value="pt" />
								<telerik:RadComboBoxItem Text="mm" Value="mm" />
								<telerik:RadComboBoxItem Text="cm" Value="cm" />
								<telerik:RadComboBoxItem Text="in" Value="in" />
								<telerik:RadComboBoxItem Text="em" Value="em" />
								<telerik:RadComboBoxItem Text="ex" Value="ex" />
								<telerik:RadComboBoxItem Text="%" Value="%" />
							</Items>
						</telerik:RadComboBox>
					</li>
					<li>
						<label for="MarginLeft" class="columnLabel">
							<script type="text/javascript">document.write(localization["Left"]);</script>
						</label>
						<telerik:RadNumericTextBox ShouldResetWidthInPixels="false" ShowSpinButtons="true" IncrementSettings-InterceptArrowKeys="true" IncrementSettings-InterceptMouseWheel="true" runat="server" ID="marginLeft" Width="65px" style="text-align: right;">
							<NumberFormat DecimalDigits="0" />
						</telerik:RadNumericTextBox>
						<telerik:RadComboBox ID="marginLeftMetric" runat="server" Width="50">
							<Items>
								<telerik:RadComboBoxItem Text="px" Value="px" />
								<telerik:RadComboBoxItem Text="pc" Value="pc" />
								<telerik:RadComboBoxItem Text="pt" Value="pt" />
								<telerik:RadComboBoxItem Text="mm" Value="mm" />
								<telerik:RadComboBoxItem Text="cm" Value="cm" />
								<telerik:RadComboBoxItem Text="in" Value="in" />
								<telerik:RadComboBoxItem Text="em" Value="em" />
								<telerik:RadComboBoxItem Text="ex" Value="ex" />
								<telerik:RadComboBoxItem Text="%" Value="%" />
							</Items>
						</telerik:RadComboBox>
					</li>
					<li>
						<label for="MarginRight" class="columnLabel">
							<script type="text/javascript">document.write(localization["Right"]);</script>
						</label>
						<telerik:RadNumericTextBox ShouldResetWidthInPixels="false" ShowSpinButtons="true" IncrementSettings-InterceptArrowKeys="true" IncrementSettings-InterceptMouseWheel="true" runat="server" ID="marginRight" Width="65px" style="text-align: right;">
							<NumberFormat DecimalDigits="0" />
						</telerik:RadNumericTextBox>
						<telerik:RadComboBox ID="marginRightMetric" runat="server" Width="50">
							<Items>
								<telerik:RadComboBoxItem Text="px" Value="px" />
								<telerik:RadComboBoxItem Text="pc" Value="pc" />
								<telerik:RadComboBoxItem Text="pt" Value="pt" />
								<telerik:RadComboBoxItem Text="mm" Value="mm" />
								<telerik:RadComboBoxItem Text="cm" Value="cm" />
								<telerik:RadComboBoxItem Text="in" Value="in" />
								<telerik:RadComboBoxItem Text="em" Value="em" />
								<telerik:RadComboBoxItem Text="ex" Value="ex" />
								<telerik:RadComboBoxItem Text="%" Value="%" />
							</Items>
						</telerik:RadComboBox>
					</li>
				</ul>
			</fieldset>
		</telerik:RadPageView>
		<telerik:RadPageView ID="BorderContent" runat="server">
			<div class="Labels">
				<label class="columnLabel standalone">
					<script type="text/javascript">document.write(localization["Top"]);</script>
				</label>
				<label class="columnLabel standalone">
					<script type="text/javascript">document.write(localization["Bottom"]);</script>
				</label>
				<label class="columnLabel standalone">
					<script type="text/javascript">document.write(localization["Left"]);</script>
				</label>
				<label class="columnLabel standalone">
					<script type="text/javascript">document.write(localization["Right"]);</script>
				</label>
			</div>
			<fieldset id="borderStylePane">
				<legend>
					Border style
				</legend>
				<div style="text-align: center;">
					<telerik:RadButton ID="sameBorderStyleForAll" runat="server" ToggleType="CheckBox" ButtonType="ToggleButton" Text="Same for all" AutoPostBack="false" />
				</div>
				<ul>
					<li>
						<telerik:RadComboBox ID="borderStyleTop" runat="server" Width="100" />
					</li>
					<li>
						<telerik:RadComboBox ID="borderStyleBottom" runat="server" Width="100" />
					</li>
					<li>
						<telerik:RadComboBox ID="borderStyleLeft" runat="server" Width="100" />
					</li>
					<li>
						<telerik:RadComboBox ID="borderStyleRight" runat="server" Width="100" />
					</li>
				</ul>
			</fieldset>
			<fieldset id="borderWidthPane">
				<legend>
					Border width
				</legend>
				<div style="text-align: center;">
					<telerik:RadButton ID="sameBorderWidthForAll" runat="server" ToggleType="CheckBox" ButtonType="ToggleButton" Text="Same for all" AutoPostBack="false" />
				</div>
				<ul>
					<li>
						<telerik:RadNumericTextBox ShouldResetWidthInPixels="false" ShowSpinButtons="true" IncrementSettings-InterceptArrowKeys="true" IncrementSettings-InterceptMouseWheel="true" runat="server" ID="borderWidthTop" Width="60px" MinValue="0" MaxValue="99999" style="text-align: right;">
							<NumberFormat DecimalDigits="0" />
						</telerik:RadNumericTextBox>
						<telerik:RadComboBox ID="borderWidthTopMetric" runat="server" Width="50">
							<Items>
								<telerik:RadComboBoxItem Text="px" Value="px" />
								<telerik:RadComboBoxItem Text="pc" Value="pc" />
								<telerik:RadComboBoxItem Text="pt" Value="pt" />
								<telerik:RadComboBoxItem Text="mm" Value="mm" />
								<telerik:RadComboBoxItem Text="cm" Value="cm" />
								<telerik:RadComboBoxItem Text="in" Value="in" />
								<telerik:RadComboBoxItem Text="em" Value="em" />
								<telerik:RadComboBoxItem Text="ex" Value="ex" />
								<telerik:RadComboBoxItem Text="%" Value="%" />
							</Items>
						</telerik:RadComboBox>
					</li>
					<li>
						<telerik:RadNumericTextBox ShouldResetWidthInPixels="false" ShowSpinButtons="true" IncrementSettings-InterceptArrowKeys="true" IncrementSettings-InterceptMouseWheel="true" runat="server" ID="borderWidthBottom" Width="60px" MinValue="0" MaxValue="99999" style="text-align: right;">
							<NumberFormat DecimalDigits="0" />
						</telerik:RadNumericTextBox>
						<telerik:RadComboBox ID="borderWidthBottomMetric" runat="server" Width="50">
							<Items>
								<telerik:RadComboBoxItem Text="px" Value="px" />
								<telerik:RadComboBoxItem Text="pc" Value="pc" />
								<telerik:RadComboBoxItem Text="pt" Value="pt" />
								<telerik:RadComboBoxItem Text="mm" Value="mm" />
								<telerik:RadComboBoxItem Text="cm" Value="cm" />
								<telerik:RadComboBoxItem Text="in" Value="in" />
								<telerik:RadComboBoxItem Text="em" Value="em" />
								<telerik:RadComboBoxItem Text="ex" Value="ex" />
								<telerik:RadComboBoxItem Text="%" Value="%" />
							</Items>
						</telerik:RadComboBox>
					</li>
					<li>
						<telerik:RadNumericTextBox ShouldResetWidthInPixels="false" ShowSpinButtons="true" IncrementSettings-InterceptArrowKeys="true" IncrementSettings-InterceptMouseWheel="true" runat="server" ID="borderWidthLeft" Width="60px" MinValue="0" MaxValue="99999" style="text-align: right;">
							<NumberFormat DecimalDigits="0" />
						</telerik:RadNumericTextBox>
						<telerik:RadComboBox ID="borderWidthLeftMetric" runat="server" Width="50">
							<Items>
								<telerik:RadComboBoxItem Text="px" Value="px" />
								<telerik:RadComboBoxItem Text="pc" Value="pc" />
								<telerik:RadComboBoxItem Text="pt" Value="pt" />
								<telerik:RadComboBoxItem Text="mm" Value="mm" />
								<telerik:RadComboBoxItem Text="cm" Value="cm" />
								<telerik:RadComboBoxItem Text="in" Value="in" />
								<telerik:RadComboBoxItem Text="em" Value="em" />
								<telerik:RadComboBoxItem Text="ex" Value="ex" />
								<telerik:RadComboBoxItem Text="%" Value="%" />
							</Items>
						</telerik:RadComboBox>
					</li>
					<li>
						<telerik:RadNumericTextBox ShouldResetWidthInPixels="false" ShowSpinButtons="true" IncrementSettings-InterceptArrowKeys="true" IncrementSettings-InterceptMouseWheel="true" runat="server" ID="borderWidthRight" Width="60px" MinValue="0" MaxValue="99999" style="text-align: right;">
							<NumberFormat DecimalDigits="0" />
						</telerik:RadNumericTextBox>
						<telerik:RadComboBox ID="borderWidthRightMetric" runat="server" Width="50">
							<Items>
								<telerik:RadComboBoxItem Text="px" Value="px" />
								<telerik:RadComboBoxItem Text="pc" Value="pc" />
								<telerik:RadComboBoxItem Text="pt" Value="pt" />
								<telerik:RadComboBoxItem Text="mm" Value="mm" />
								<telerik:RadComboBoxItem Text="cm" Value="cm" />
								<telerik:RadComboBoxItem Text="in" Value="in" />
								<telerik:RadComboBoxItem Text="em" Value="em" />
								<telerik:RadComboBoxItem Text="ex" Value="ex" />
								<telerik:RadComboBoxItem Text="%" Value="%" />
							</Items>
						</telerik:RadComboBox>
					</li>
				</ul>
			</fieldset>
			<fieldset id="borderColorPane">
				<legend>
					Border color
				</legend>
				<div style="text-align: center;">
					<telerik:RadButton ID="sameBorderColorForAll" runat="server" ToggleType="CheckBox" ButtonType="ToggleButton" Text="Same for all" AutoPostBack="false" />
				</div>
				<ul>
					<li>
						<tools:ColorPicker ID="borderColorTop" runat="server" Enabled="true"></tools:ColorPicker>
					</li>
					<li>
						<tools:ColorPicker ID="borderColorBottom" runat="server" Enabled="false"></tools:ColorPicker>
					</li>
					<li>
						<tools:ColorPicker ID="borderColorLeft" runat="server" Enabled="false"></tools:ColorPicker>
					</li>
					<li>
						<tools:ColorPicker ID="borderColorRight" runat="server" Enabled="false"></tools:ColorPicker>
					</li>
				</ul>
			</fieldset>
			<div id="borderCollapseContainer">
				<telerik:RadComboBox ID="borderCollapse" runat="server" Label="Collapse borders:">
					<Items>
						<telerik:RadComboBoxItem Value="" Text="" />
						<telerik:RadComboBoxItem Value="collapse" Text="Collapse" />
						<telerik:RadComboBoxItem Value="separate" Text="Separate" />
					</Items>
				</telerik:RadComboBox>
			</div>
		</telerik:RadPageView>
		<telerik:RadPageView ID="ListsContent" runat="server">
			<fieldset class="reStyleBuilderLists">
				<ul>
					<li>
						<label for="listStyleType" class="sLabel">
							<script type="text/javascript">document.write(localization["Style"]);</script>
						</label>
						<telerik:RadComboBox ID="listStyleType" runat="server" />
					</li>
					<li>
						<label for="listStyleImage" class="sLabel floater">
							<script type="text/javascript">document.write(localization["Image"]);</script>
						</label>
						<div class="reInlineBlock">
							<tools:ImageDialogCaller ID="listBulletImage" runat="server" />
						</div>
					</li>
					<li>
						<label for="listStylePosition" class="sLabel">
							<script type="text/javascript">document.write(localization["Position"]);</script>
						</label>
						<telerik:RadComboBox ID="listStylePosition" runat="server" />
					</li>
				</ul>
			</fieldset>
		</telerik:RadPageView>
	</telerik:RadMultiPage>
	<div class="radECtrlButtonsList">
		<button type="button" id="insertButton">
			<script type="text/javascript">setInnerHtml("insertButton", localization["OK"]);</script>
		</button>
		<button type="button" id="cancelButton" onclick="Telerik.Web.UI.Dialogs.CommonDialogScript.get_windowReference().close()">
			<script type="text/javascript">setInnerHtml("cancelButton", localization["Cancel"]);</script>
		</button>
	</div>
</div>
<script type="text/javascript">
	function initStyleBuilderControls()
	{
		var $sb = Telerik.Web.UI.Widgets.StyleBuilder;
		//Font Controls
		initStyleBuilderControl("ComboBox", { name: "font-family", combo: $findSB("fontFamily"), valueStripper: /\'|\"/g });
		initStyleBuilderControl("ColorPicker", { name: "color", picker: $findSB("color") });
		initStyleBuilderControl("FontSize", { name: "font-size",
			absolute: new $sb.Numeric({ number: $findSB("fontSizeValue"), metric: $findSB("fontSizeMetric") }),
			absoluteButton: $get("fSizeAbsolute"),
			relative: new $sb.ComboBox({ combo: $findSB("fontSizeRelative") }),
			relativeButton: $get("fSizeRelative")
		});
		initStyleBuilderControl("ComboBox", {name: "font-weight", combo: $findSB("fontBold") });
		initStyleBuilderControl("ComboBox", {name: "font-style", combo: $findSB("fontStyle") });
		initStyleBuilderControl("ComboBox", {name: "font-variant", combo: $findSB("fontVariant") });
		initStyleBuilderControl("ComboBox", {name: "text-transform", combo: $findSB("Capitalization") });

		initStyleBuilderControl("Tuple", {name: "text-decoration", controls: [
			new $sb.ToggleButton({ button: $findSB("fdUnderline") }),
			new $sb.ToggleButton({ button: $findSB("fdStrikethrough") }),
			new $sb.ToggleButton({ button: $findSB("fdOverline") })
		]});
	
		//Background Controls
		initStyleBuilderControl("BackgroundColor", { name: "background-color",
			color: new $sb.ColorPicker({ picker: $findSB("backgroundColor") }),
			transparent: new $sb.ToggleButton({ button: $findSB("transparent") })
		});
		initStyleBuilderControl("GenericControl", { name: "background-image", control: $findSB("backgroundImage"), prefix: 'url("', suffix: '")', prefixStrip: new RegExp("url\\(['\"]*"), suffixStrip: new RegExp("['\"]*\\)") });
		initStyleBuilderControl("ComboBox", {name: "background-repeat", combo: $findSB("backgroundRepeat")});
		initStyleBuilderControl("ComboBox", {name: "background-attachment", combo: $findSB("backgroundAttachment")});
		
		if($telerik.isIE6 || $telerik.isIE7 || $telerik.isIE8)
		{
			initStyleBuilderControl("ComboCustomNumeric", { name: "background-position-x",
				combo: new $sb.ComboBox({ combo: $findSB("positionHorizontal") }),
				numeric: new $sb.Numeric({ number: $findSB("positionHorizontalValue"), metric: $findSB("positionHorizontalMetric") })
			});
			initStyleBuilderControl("ComboCustomNumeric", { name: "background-position-y",
				combo: new $sb.ComboBox({ combo: $findSB("positionVertical") }),
				numeric: new $sb.Numeric({ number: $findSB("positionVerticalValue"), metric: $findSB("positionVerticalMetric") })
			});
		}
		else
		{
			initStyleBuilderControl("Tuple", {name: "background-position", controls: [
				new $sb.ComboCustomNumeric({
					combo: new $sb.ComboBox({ combo: $findSB("positionHorizontal") }),
					numeric: new $sb.Numeric({ number: $findSB("positionHorizontalValue"), metric: $findSB("positionHorizontalMetric") })
				}),
				new $sb.ComboCustomNumeric({
					combo: new $sb.ComboBox({ combo: $findSB("positionVertical") }),
					numeric: new $sb.Numeric({ number: $findSB("positionVerticalValue"), metric: $findSB("positionVerticalMetric") })
				})
			]});
		}
		

		//Text controls
		initStyleBuilderControl("ComboBox", {name: "text-align", combo: $findSB("textAlign")});
		initStyleBuilderControl("ComboBox", {name: "vertical-align", combo: $findSB("verticalAlign")});
		initStyleBuilderControl("ComboCustomNumeric", {name: "letter-spacing",
			combo: new $sb.ComboBox({combo: $findSB("letterSpacing")}),
			numeric: new $sb.Numeric({number: $findSB("letterSpacingValue"), metric: $findSB("letterSpacingMetric")})
		});
		initStyleBuilderControl("ComboCustomNumeric", {name: "word-spacing",
			combo: new $sb.ComboBox({combo: $findSB("wordSpacing")}),
			numeric: new $sb.Numeric({number: $findSB("wordSpacingValue"), metric: $findSB("wordSpacingMetric")})
		});
		initStyleBuilderControl("ComboCustomNumeric", {name: "line-height",
			combo: new $sb.ComboBox({combo: $findSB("lineHeight")}),
			numeric: new $sb.Numeric({number: $findSB("lineHeightValue"), metric: $findSB("lineHeightMetric")})
		});
		initStyleBuilderControl("Numeric", {name: "text-indent", number: $findSB("textIndentValue"), metric: $findSB("textIndentMetric")});
		initStyleBuilderControl("ComboBox", {name: "direction", combo: $findSB("textDecoration")});

		//Layout controls
		initStyleBuilderControl("ComboBox", {name: "display", combo: $findSB("display")});
		initStyleBuilderControl("ComboBox", {name: "visibility", combo: $findSB("visibility")});
		initStyleBuilderControl("ComboBox", {name: getFloatStyleName(), combo: $findSB("float")});
		initStyleBuilderControl("ComboBox", {name: "clear", combo: $findSB("clear")});
		initStyleBuilderControl("ComboBox", {name: "overflow", combo: $findSB("overflow")});
		initStyleBuilderControl("Tuple", {name: "clip" ,prefix: "rect(", suffix: ")", delimiter: ",", controls: [
			createStyleBuilderControl("Numeric", {number: $findSB("clipTop"), metric: $findSB("clipTopMetric")}),
			createStyleBuilderControl("Numeric", {number: $findSB("clipRight"), metric: $findSB("clipRightMetric")}),
			createStyleBuilderControl("Numeric", {number: $findSB("clipBottom"), metric: $findSB("clipBottomMetric")}),
			createStyleBuilderControl("Numeric", {number: $findSB("clipLeft"), metric: $findSB("clipLeftMetric")})
		]});

		//Box controls
		initStyleBuilderControl("Numeric", {name: "width", number: $findSB("width"), metric: $findSB("widthMetric")});
		initStyleBuilderControl("Numeric", {name: "height", number: $findSB("height"), metric: $findSB("heightMetric")});
	
		var paddingTop = createStyleBuilderControl("Numeric", {name: "padding-top", number: $findSB("paddingTop"), metric: $findSB("paddingTopMetric")});
		var paddingRight = createStyleBuilderControl("Numeric", {name: "padding-right", number: $findSB("paddingRight"), metric: $findSB("paddingRightMetric")});
		var paddingBottom = createStyleBuilderControl("Numeric", {name: "padding-bottom", number: $findSB("paddingBottom"), metric: $findSB("paddingBottomMetric")});
		var paddingLeft = createStyleBuilderControl("Numeric", {name: "padding-left", number: $findSB("paddingLeft"), metric: $findSB("paddingLeftMetric")});
		initStyleBuilderControl("FourTuple", {name: "padding", sameForAll: $findSB("samePaddingForAll"), controls: [
			paddingTop, paddingRight, paddingBottom, paddingLeft
		]});
		registerParsers(paddingTop, paddingRight, paddingBottom, paddingLeft);
	
		var marginTop = createStyleBuilderControl("Numeric", {name: "margin-top", number: $findSB("marginTop"), metric: $findSB("marginTopMetric")});
		var marginRight = createStyleBuilderControl("Numeric", {name: "margin-right", number: $findSB("marginRight"), metric: $findSB("marginRightMetric")});
		var marginBottom = createStyleBuilderControl("Numeric", {name: "margin-bottom", number: $findSB("marginBottom"), metric: $findSB("marginBottomMetric")});
		var marginLeft = createStyleBuilderControl("Numeric", {name: "margin-left", number: $findSB("marginLeft"), metric: $findSB("marginLeftMetric")});
		initStyleBuilderControl("FourTuple", {name: "margin", sameForAll: $findSB("sameMarginForAll"), controls: [
			marginTop, marginRight, marginBottom, marginLeft
		]});
		registerParsers(marginTop, marginRight, marginBottom, marginLeft);

		//Border controls
		var borderStyleTop = createStyleBuilderControl("ComboBox", {combo: $findSB("borderStyleTop"), name: "border-top-style" });
		var borderStyleRight = createStyleBuilderControl("ComboBox", {combo: $findSB("borderStyleRight"), name: "border-right-style" });
		var borderStyleBottom = createStyleBuilderControl("ComboBox", {combo: $findSB("borderStyleBottom"), name: "border-bottom-style" });
		var borderStyleLeft = createStyleBuilderControl("ComboBox", {combo: $findSB("borderStyleLeft"), name: "border-left-style" });
		initStyleBuilderControl("FourTuple", {name: "border-style", sameForAll: $findSB("sameBorderStyleForAll"), controls: [
			borderStyleTop, borderStyleRight, borderStyleBottom, borderStyleLeft
		]});
		registerParsers(borderStyleTop, borderStyleRight, borderStyleBottom, borderStyleLeft);
	
		var borderWidthTop = createStyleBuilderControl("Numeric", {name: "border-top-width", number: $findSB("borderWidthTop"), metric: $findSB("borderWidthTopMetric")});
		var borderWidthRight = createStyleBuilderControl("Numeric", {name: "border-right-width", number: $findSB("borderWidthRight"), metric: $findSB("borderWidthRightMetric")});
		var borderWidthBottom = createStyleBuilderControl("Numeric", {name: "border-bottom-width", number: $findSB("borderWidthBottom"), metric: $findSB("borderWidthBottomMetric")});
		var borderWidthLeft = createStyleBuilderControl("Numeric", {name: "border-left-width", number: $findSB("borderWidthLeft"), metric: $findSB("borderWidthLeftMetric")});
		initStyleBuilderControl("FourTuple", {name: "border-width", sameForAll: $findSB("sameBorderWidthForAll"), controls: [
			borderWidthTop, borderWidthRight, borderWidthBottom, borderWidthLeft
		]});
		registerParsers(borderWidthTop, borderWidthRight, borderWidthBottom, borderWidthLeft);

		var borderColorTop = createStyleBuilderControl("ColorPicker", {picker: $findSB("borderColorTop"), name: "border-top-color"});
		var borderColorRight = createStyleBuilderControl("ColorPicker", {picker: $findSB("borderColorRight"), name: "border-right-color" });
		var borderColorBottom = createStyleBuilderControl("ColorPicker", {picker: $findSB("borderColorBottom"), name: "border-bottom-color" });
		var borderColorLeft = createStyleBuilderControl("ColorPicker", {picker: $findSB("borderColorLeft"), name: "border-left-color" });
		initStyleBuilderControl("FourTuple", {name: "border-color", sameForAll: $findSB("sameBorderColorForAll"), controls: [
			borderColorTop, borderColorRight, borderColorBottom, borderColorLeft
		]});
		registerParsers(borderColorTop, borderColorRight, borderColorBottom, borderColorLeft);

		initStyleBuilderControl("ComboBox", {name: "border-collapse", combo: $findSB("borderCollapse")});
		
		//List controls
		initStyleBuilderControl("ComboBox", {name: "list-style-type", combo: $findSB("listStyleType")});
		initStyleBuilderControl("GenericControl", { name: "list-style-image", control: $findSB("listBulletImage"), prefix: 'url("', suffix: '")', prefixStrip: new RegExp("url\\(['\"]*"), suffixStrip: new RegExp("['\"]*\\)") });
		initStyleBuilderControl("ComboBox", {name: "list-style-position", combo: $findSB("listStylePosition")});
	}

	function initStyleBuilderControl(type, options)
	{
		var control = new Telerik.Web.UI.Widgets.StyleBuilder[type](options);
		return getStyleBuilder().registerControl(control);
	}
	function createStyleBuilderControl(type, options)
	{
		return new Telerik.Web.UI.Widgets.StyleBuilder[type](options);
	}

	function registerParsers()
	{
		var sb = getStyleBuilder();
		for(var i = 0; i < arguments.length; i++)
			sb.registerParser(arguments[i]);
	}

	function getStyleBuilder()
	{
		return $find("dialogControl");
	}
	function getFloatStyleName()
	{
		return $get("cancelButton").style.cssFloat == undefined ? "styleFloat" : "cssFloat";
	}

	var borderStyleData = [{value: "", text: ""},{value: "none", text: localization["None"]},{value: "dotted", text: localization["Dotted"]},{value: "dashed", text: localization["Dashed"]},{value: "solid", text: localization["SolidLine"]},{value: "double", text: localization["DoubleLine"]},{value: "groove", text: localization["Groove"]},{value: "ridge", text: localization["Ridge"]},{value: "inset", text: localization["Inset"]},{value: "outset", text: localization["Outset"]}];
	var StyleBuilderComboData = {
		fontSizeRelative: [{value: "smaller", text: localization["Smaller"]}, {value: "larger", text: localization["Larger"]}],
		fontBold: [{value: "", text: ""},{value: "lighter", text: localization["Lighter"]}, {value: "normal", text: localization["Normal"]}, {value: "bold", text: localization["Bold"]}, {value: "bolder", text: localization["Bolder"]} ],
		fontStyle: [{value: "", text: ""},{value: "normal", text: localization["Normal"]}, {value: "italic", text: localization["Italics"]}, {value: "oblique", text: "Oblique"}],
		fontVariant: [{value: "", text: ""},{value: "normal", text: localization["Normal"]}, {value: "small-caps", text: localization["SmallCaps"]}],
		Capitalization: [{value: "", text: ""}, {value: "none", text: localization["None"]}, {value: "capitalize", text: localization["Capitalize"]}, {value: "uppercase", text: localization["Uppercase"]}, {value: "lowercase", text: localization["Lowercase"]} ],
		backgroundRepeat: [{value: "", text: ""},{value: "repeat-x", text: localization["Horizontal"]},{value: "repeat-y", text: localization["Vertical"]},{value: "repeat", text: localization["Both"]},{value: "no-repeat", text: localization["None"]}],
		backgroundAttachment: [{value: "", text: ""},{value: "scroll", text: localization["ScrollingBackground"]},{value: "fixed", text: localization["FixedBackground"]}],
		positionHorizontal: [{value: "", text: ""},{value: "left", text: localization["Left"]},{value: "center", text: localization["Center"]},{value: "right", text: localization["Right"]},{value: "custom", text: localization["Custom"]}],
		positionVertical: [{value: "", text: ""},{value: "top", text: localization["Top"]},{value: "center", text: localization["Center"]},{value: "bottom", text: localization["Bottom"]},{value: "custom", text: localization["Custom"]}],
		textAlign: [{value: "", text: ""},{value: "left", text: localization["Left"]},{value: "center", text: localization["Center"]},{value: "right", text: localization["Right"]},{value: "justify", text: localization["Justify"]}],
		verticalAlign: [{value: "", text: ""},{value: "baseline", text: localization["Baseline"]},{value: "sub", text: localization["Sub"]},{value: "super", text: localization["Super"]},{value: "top", text: localization["Top"]},{value: "text-top", text: localization["TextTop"]},{value: "middle", text: localization["Middle"]},{value: "bottom", text: localization["Bottom"]},{value: "text-bottom", text: localization["TextBottom"]}],
		letterSpacing: [{value: "", text: ""},{value: "normal", text: localization["Normal"]},{value: "custom", text: localization["Custom"]}],
		wordSpacing: [{value: "", text: ""},{value: "normal", text: localization["Normal"]},{value: "custom", text: localization["Custom"]}],
		lineHeight: [{value: "", text: ""},{value: "normal", text: localization["Normal"]},{value: "custom", text: localization["Custom"]}],
		textDecoration: [{value: "", text: ""},{value: "ltr", text: localization["LeftToRight"]},{value: "rtl", text: localization["RightToLeft"]}],
		display: [{value: "", text: ""},{value: "none", text: localization["None"]},{value: "block", text: localization["Block"]},{value: "inline", text: localization["Inline"]}],
		visibility: [{value: "", text: ""},{value: "visible", text: localization["Visible"]},{value: "hidden", text: localization["Hidden"]},{value: "collapse", text: localization["Collapse"]}],
		float: [{value: "", text: ""},{value: "none", text: localization["DontAllowTextOnSides"]},{value: "right", text: localization["ToTheRight"]},{value: "left", text: localization["ToTheLeft"]}],
		clear: [{value: "", text: ""},{value: "none", text: localization["None"]},{value: "left", text: localization["Left"]},{value: "right", text: localization["Right"]},{value: "both", text: "Both"}],
		overflow: [{value: "", text: ""},{value: "auto", text: localization["UseScrollbarsIfNeeded"]},{value: "scroll", text: localization["AlwaysUseScrollbars"]},{value: "visible", text: localization["ContentIsNotClipped"]},{value: "hidden", text: localization["ContentIsClipped"]}],
		borderStyleTop: borderStyleData,
		borderStyleBottom: borderStyleData,
		borderStyleLeft: borderStyleData,
		borderStyleRight: borderStyleData,
		listStyleType: [{value: "", text: ""},{value: "none", text: localization["None"]},{value: "circle", text: localization["Circle"]},{value: "disc", text: localization["Disk"]},{value: "square", text: localization["Square"]},{value: "decimal", text: " 1, 2, 3, 4 ..."},{value: "lower-roman", text: localization["Lowercase"] + " i, ii, iii, iv ..."},{value: "upper-roman", text: localization["Uppercase"] + " I, II, III, IV ..."},{value: "lower-alpha", text: localization["Lowercase"] + " a, b, c, d ..."},{value: "upper-alpha", text: localization["Uppercase"] + " A, B, C, D ..."}],
		listStylePosition: [{value: "", text: ""},{value: "outside", text: localization["Outside"]},{value: "inside", text: localization["Inside"]}]
	};
	var sameForAllLocalized = localization["SameForAll"];
	var StyleBuilderButtonData = {
		fdUnderline: localization["Underline"],
		fdStrikethrough: localization["Strikethrough"],
		fdOverline: localization["Overline"],
		samePaddingForAll: sameForAllLocalized,
		sameMarginForAll: sameForAllLocalized,
		sameBorderStyleForAll: sameForAllLocalized,
		sameBorderWidthForAll: sameForAllLocalized,
		sameBorderColorForAll: sameForAllLocalized
	};

	Sys.Application.add_load(function() {
		var helpers = Telerik.Web.UI.Widgets.StyleBuilder.Helpers;

		for(var key in StyleBuilderComboData)
			helpers.addComboBoxItems($findSB(key), StyleBuilderComboData[key]);

		for(var keyy in StyleBuilderButtonData)
			$findSB(keyy).set_text(StyleBuilderButtonData[keyy]);

		initStyleBuilderControls();
	});
</script>
