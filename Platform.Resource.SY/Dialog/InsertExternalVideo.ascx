<%@ Control Language="C#" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI.Editor" TagPrefix="tools" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI.Widgets" TagPrefix="widgets" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI.Dialogs" TagPrefix="dialogs" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<style type="text/css">
	*{margin:0;padding: 0;}
	body
	{
		font-family: Arial;
		font-size: 12px;
	}
	hr
	{
		border: 1px solid #ccc;
		border-bottom-color: #fff;
		color: #eee;
		margin: 1em 0;
	}
	label.newLineLabel
	{
		display: block;
	}
			
	#externaVideoContainer
	{
		width: 716px;
		margin: auto;
		padding: 10px 0px;
	}
	#videoSettingsContainer
	{
		padding: 10px;
		height: 505px;
	}
	#inputUrlContainer label
	{
		font-weight: bold;
		margin-bottom: 0.5em;
	}
	#inputUrlContainer input
	{
		width: 90%;
		height: 22px;
		margin: 10px 0 0;
	}
			
	#videoSettings
	{
		margin: 1em 0;
	}
	#videoSettings #videoViewport
	{
		float: left;
		display: block;
		width: 405px;
		height: 305px;
	}
	#width, #height
	{
		width: 70px;
	}
	#videoSettingsControls
	{
		padding-left: 20px;
		float: left;
	}
	#videoSettingsControls .settingsPanel
	{
		margin: 2em 0;
		clear: both;
		width: 250px;
	}
	#aspectRatio
	{
		width: 84px;
	}
	#videoSizeContainer
	{
		height: 50px;
	}
	#videoSizeContainer div
	{
		float: left;
		line-height: 26px;
		width: 45px;
	}
	#videoSizeContainer table.rfdRoundedWrapper div
	{
		float: none;
		line-height: 26px;
		width: 1px;
	}
	#videoSizeContainer label, #videoSizeContainer input
	{
		display: block;
		margin-bottom: 3px;
	}
	#videoSizeContainer input
	{
		width: 41px;
	}
	#sizeDivider
	{
		line-height: 50px;
		margin: 0 0.5em;
	}
	#aspectRatioIcon
	{
		width: 24px;
		height: 55px;
		cursor: pointer;
	}
	#aspectRatioIcon.noRatio
	{
		background-position: 10px center;
	}
			
	#advancedMode
	{
		clear: both;
		padding: 2em 0 1em;
	}
	#toggleEmbedCode
	{
		text-decoration: none;
		color: #555;
		padding-left: 12px;
		background: transparent;
		display: block;
		margin-bottom: 0.5em;
	}
	#toggleEmbedCode.toggled
	{
		background-position: left center;
	}
	#toggleEmbedCode strong
	{
		font-style: normal;
	}
	#toggleEmbedCode span
	{
		font-style: italic;
		font-size: 90%;
	}
	#embedCode
	{
		width: 400px;
		height: 80px;
	}
	#dialogButtons
	{
		text-align: right;
		padding: 5px 0;
	}
</style>
<div id="externaVideoContainer">
	<fieldset id="videoSettingsContainer">
		<legend style="padding: 0px 5px;">
			<asp:Literal ID="PasteVideoURLText" runat="server" />
		</legend>
		<div id="inputUrlContainer">
			<input type="text" id="videoUrl" name="videoUrl" />
		</div>
		<div id="videoSettings" style="display: none;">
			<div id="videoViewport"></div>
			<div id="videoSettingsControls">
				<strong>
					<asp:Literal ID="EmbedVideoSettingsText" runat="server" />:
				</strong>
						
				<div class="settingsPanel">
					<label for="aspectRatio" class="newLineLabel">
						<asp:Literal ID="AspectRatioText" runat="server" />:
					</label>
					<select id="aspectRatio" name="aspectRatio">
						<option value="4x3">4x3</option>
						<option value="16x9">16x9</option>
					</select>
				</div>
				<div class="settingsPanel" id="videoSizeContainer">
					<div>
						<label for="width"><asp:Literal ID="VideoWidthText" runat="server" /></label>
						<label for="height"><asp:Literal ID="VideoHeightText" runat="server" /></label>
					</div>
					<div>
						<input type="text" id="width" name="width" value="400" />
						<input type="text" id="height" name="height" value="300" />
					</div>
					<div id="aspectRatioIcon"></div>
				</div>
				<div class="settingsPanel">
					<div>
						<input type="checkbox" id="autoplay" name="autoplay" checked="checked" />
						<label for="autoplay">
							<asp:Literal ID="VideoAutoplayText" runat="server" />
						</label>
					</div>
					<div>
						<input type="checkbox" id="showTitle" name="showTitle" checked="checked" />
						<label for="showTitle">
							<asp:Literal ID="ShowTitleText" runat="server" />
						</label>
					</div>
					<div>
						<input type="checkbox" id="fullscreen" name="fullscreen" checked="checked" />
						<label for="fullscreen">
							<asp:Literal ID="EnableFullscreenText" runat="server" />
						</label>
					</div>
					<div class="youtubeSpecific" style="display: none;">
						<input type="checkbox" id="enhancedPrivacy" name="enhancedPrivacy" />
						<label for="enhancedPrivacy">
							<asp:Literal ID="EnablePrivacyEnhancedText" runat="server" />
						</label>
					</div>
				</div>
			</div>
		</div>
		<div id="advancedMode">
			<a href="javascript: void 0;" id="toggleEmbedCode" title="Toggle advanced mode embed code" runat="server">
				<strong><asp:Literal ID="AdvancedModeText" runat="server" /></strong>
				<span>(<asp:Literal ID="EmbedCodeText" runat="server" />)</span>
			</a>
			<div id="embedCodeContainer" style="display: none;">
				<textarea id="embedCode" name="embedCode" rows="4" cols="30" readonly="readonly"></textarea>
			</div>
		</div>
	</fieldset>
	<div id="dialogButtons" >
		<input type="button" id="save" name="save" value="Save" />
		<input type="button" id="cancel" name="cancel" value="Cancel" />
	</div>
</div>
<script type="text/javascript" id="toggleAdvancedModeScript">
	$telerik.$(document).ready(function ()
	{
		$telerik.$("#toggleEmbedCode").click(function ()
		{
			$telerik.$(this).toggleClass("toggled");
			$telerik.$("#embedCodeContainer").slideToggle(200);
		});
	});
</script>
<script type="text/javascript" id="dialogHandlers">
	Type.registerNamespace("Telerik.Web.UI.Widgets");

	Telerik.Web.UI.Widgets.InsertExternalVideo = function (element)
	{
		Telerik.Web.UI.Widgets.InsertExternalVideo.initializeBase(this, [element]);
		this.dialogImporter = null;
	};
	Telerik.Web.UI.Widgets.InsertExternalVideo.prototype =
	{
		initialize: function()
		{
			this.dialogImporter = new ExternalVideoImporter($get("externaVideoContainer"));
			this.dialogImporter.registerParsers([new YouTubeEmbedCodeParser(), new VimeoEmbedCodeParser()]);

			$telerik.$("#save").click($telerik.$.proxy(this._saveHandler, this));
			$telerik.$("#cancel").click($telerik.$.proxy(this._cancelHandler, this));
		},
		clientInit: function (clientParameters)
		{
			
		},
		insertVideoEmbedCode: function ()
		{
			var embedCode = this.dialogImporter.ui.embedCodeTxt.val();
			this._closeDialogWindow(new Telerik.Web.UI.EditorCommandEventArgs("InsertExternalDialog", null, embedCode));
		},
		dispose: Function.emptyFunction,
		_saveHandler: function (event)
		{
			event.preventDefault();
			this.insertVideoEmbedCode();
		},
		_cancelHandler: function (event)
		{
			event.preventDefault();
			this._closeDialogWindow();
		},
		_closeDialogWindow: function (args)
		{
			Telerik.Web.UI.Dialogs.CommonDialogScript.get_windowReference().close(args);
		}
	};
	Telerik.Web.UI.Widgets.InsertExternalVideo.registerClass("Telerik.Web.UI.Widgets.InsertExternalVideo", Telerik.Web.UI.RadWebControl, Telerik.Web.IParameterConsumer);
</script>
<script type="text/javascript" id="baseCode">
	ExternalVideoImporterUI = function (container)
	{
		this.jSelf = $telerik.$(this);
		this.container = $telerik.$(container);
		this.videoUrlTxt = this.container.find("#videoUrl:text");
		this.videoSettings = this.container.find("#videoSettings");
		this.videoViewport = this.container.find("#videoViewport");
		this.aspectRatioDD = this.container.find("select#aspectRatio");
		this.aspectRatioToggler = this.container.find("#aspectRatioIcon");
		this.widthTxt = this.container.find("#width:text");
		this.heightTxt = this.container.find("#height:text");
		this.autoplayCB = this.container.find("#autoplay:checkbox");
		this.showTitleCB = this.container.find("#showTitle:checkbox");
		this.fullscreenCB = this.container.find("#fullscreen:checkbox");
		this.privacyEnhancedCB = this.container.find("#enhancedPrivacy:checkbox");
		this.embedCodeTxt = this.container.find("textarea#embedCode");

		this.attachEventHandlers();
	}
	ExternalVideoImporterUI.prototype =
	{
		attachEventHandlers: function ()
		{
			this.videoUrlTxt.change($telerik.$.proxy(this.raiseVideoUrlEvent, this));

			this.aspectRatioDD.bind("change", $telerik.$.proxy(this.ratioChange, this));
			this.aspectRatioToggler.click(function () { $(this).toggleClass("noRatio") });
			this.widthTxt.keyup($telerik.$.proxy(this.widthChange, this));
			this.heightTxt.keyup($telerik.$.proxy(this.heightChange, this));

			this.raiseOptionEventHandler = $telerik.$.proxy(this.raiseOptionEvent, this);
			this.autoplayCB
				.add(this.showTitleCB)
				.add(this.fullscreenCB)
				.add(this.privacyEnhancedCB)
					.click(this.raiseOptionEventHandler);

			this.embedCodeTxt.click(function () { this.select(); });
		},
		raiseEvent: function (name, args)
		{
			this.jSelf.trigger(name, [args]);
		},
		raiseOptionEvent: function ()
		{
			this.raiseEvent("videoOptionChange", this._extractVideoOptions());
		},
		raiseVideoUrlEvent: function ()
		{
			this.raiseEvent("videoUrlChange", this._extractVideoOptions());
		},
		ratioChange: function ()
		{
			this.setHeightFromWidth();
			this.raiseOptionEvent();
		},
		widthChange: function ()
		{
			this.setHeightFromWidth();
			this.raiseOptionEvent();
		},
		heightChange: function ()
		{
			this.setWidthFromHeight();
			this.raiseOptionEvent();
		},
		setHeightFromWidth: function ()
		{
			var ratio = this.get_aspectRatio();
			if (ratio)
				this.heightTxt.val(Math.round(parseInt(this.widthTxt.val()) / ratio) || 0);
		},
		setWidthFromHeight: function ()
		{
			var ratio = this.get_aspectRatio();
			if (ratio)
				this.widthTxt.val(Math.round(parseInt(this.heightTxt.val()) * ratio) || 0);
		},

		showVideoSettings: function(videoProvider)
		{
			this.videoSettings.show();
			this.displaySpecificControls(videoProvider);
		},
		hideVideoSettings: function()
		{
			this.videoSettings.hide();
			this.displaySpecificControls();
		},
		displaySpecificControls: function(videoProvider)
		{
			$telerik.$("[class$=Specific]").hide();
			
			var selector = null;
			switch(videoProvider) {
				case VideoProvider.YouTube:
					selector = ".youtubeSpecific"; break;
				case VideoProvider.Vimeo:
					selector = ".vimeoSpecific"; break;
				default:
					selector = null;
			}

			if(selector)
				$telerik.$(selector).show();
		},

		get_aspectRatio: function ()
		{
			if (!this.get_hasAspectRatio())
				return null;

			var parsed = this.aspectRatioDD.val().split("x");

			return parseInt(parsed[0]) / parseInt(parsed[1]);
		},
		get_hasAspectRatio: function ()
		{
			return !this.aspectRatioToggler.hasClass("noRatio");
		},

		add_videoUrlChange: function (handler) { this.add_eventHandler("videoUrlChange", handler) },
		remove_videoUrlChange: function (handler) { this.remove_eventHandler("videoUrlChange", handler) },
		add_videoOptionChange: function (handler) { this.add_eventHandler("videoOptionChange", handler) },
		remove_videoOptionChange: function (handler) { this.remove_eventHandler("videoOptionChange", handler) },
		add_eventHandler: function (name, handler) { this.jSelf.bind(name, handler); },
		remove_eventHandler: function (name, handler) { this.jSelf.bind(name, handler); },

		_extractVideoOptions: function ()
		{
			return {
				baseUrl: this.videoUrlTxt.val(),
				width: parseInt(this.widthTxt.val()) || 0,
				height: parseInt(this.heightTxt.val()) || 0,
				enableAutoplay: this.autoplayCB.prop("checked"),
				hasTitle: this.showTitleCB.prop("checked"),
				hasFullscreenButton: this.fullscreenCB.prop("checked"),
				enablePrivacyEnhanced: this.privacyEnhancedCB.prop("checked")
			};
		},
		dispose: function ()
		{
			this.jSelf.unbind();
			this.videoUrlTxt.unbind();
			this.videoViewport.unbind();
			this.aspectRatioDD.unbind();
			this.widthTxt.unbind();
			this.heightTxt.unbind();
			this.autoplayCB.unbind();
			this.showTitleCB.unbind();
			this.fullscreenCB.unbind();
			this.jSelf = this.container = null;
		}
	};

	ExternalVideoImporter = function (container)
	{
		this.parsers = {};
		this.ui = new ExternalVideoImporterUI(container);
		this.ui.add_videoUrlChange($telerik.$.proxy(this.videoUrlChangeHandler, this));
		this.ui.add_videoOptionChange($telerik.$.proxy(this.videoOptionChangeHandler, this));
	}
	ExternalVideoImporter.prototype =
	{
		videoUrlChangeHandler: function (event, args)
		{
			this.importVideoUrl(args.baseUrl);
			this.updateEmbedCode(args);
		},
		importVideoUrl: function (url)
		{
			this.chooseCurrentParser(url);

			if (this.parser == VideoEmbedCodeParser.NullParser)
				this.ui.hideVideoSettings();
			else
			{
				this.parser.setOptions({ videoId: "", baseUrl: url, enableAutoplay: false, width: 400, height: 300, hasTitle: true, hasFullscreenButton: true });
				this.ui.videoViewport.empty().html(this.getVideoEmbedCode());
				this.ui.showVideoSettings(this.getCurrentVideoProvider());
			}
		},
		getVideoEmbedCode: function ()
		{
			return this.parser.getVideoEmbedCode();
		},
		videoOptionChangeHandler: function (event, args)
		{
			this.updateEmbedCode(args);
		},
		updateEmbedCode: function (args)
		{
			this.parser.setOptions(args);
			this.ui.embedCodeTxt.val(this.getVideoEmbedCode());
		},
		registerParsers: function (parsers)
		{
			Array.forEach(parsers, $telerik.$.proxy(function (parser)
			{
				this.parsers[parser.getParserType()] = parser;
			}, this));
		},
		chooseParser: function (url)
		{
			if (/(?:youtube\.)|(?:youtu\.be)/i.test(url))
				return this.parsers["youtube"];
			else if (/vimeo\./.test(url))
				return this.parsers["vimeo"];
			else
				return VideoEmbedCodeParser.NullParser;
		},
		getCurrentVideoProvider: function()
		{
			return this.parser.getProviderType();
		},
		chooseCurrentParser: function (url)
		{
			this.parser = this.chooseParser(url);
		},
		dispose: function ()
		{
			this.ui.dispose();
			delete this.ui;
		}
	};
	ExternalVideoImporter.registerClass("ExternalVideoImporter");
</script>
<script type="text/javascript" id="parsers">
	var VideoEmbedCodeParser = function (options)
	{
		this.options = this._getDefaultOptions();
		this.setOptions(options);
	};
	VideoEmbedCodeParser.prototype =
	{
		getVideoEmbedCode: function ()
		{
			var embedCode = VideoEmbedCodeParser.iframeTemplate;
			this.options.url = this.getVideoUrl();

			if (!this.options.url)
				return "";

			for (var option in this.options)
				embedCode = embedCode.replace("{" + option + "}", this.options[option]);

			return embedCode;
		},
		getVideoUrl: function ()
		{
			var url = this.buildEmbedUrl();

			var parameters = [];
			this.addVideoParameters(parameters);

			var queryString = parameters.join("&amp;");

			return url + (queryString ? "?" + queryString : "");
		},
		getParserType: Function.emptyFunction,
		getProviderType: Function.emptyFunction,
		addVideoParameters: Function.emptyFunction,
		buildEmbedUrl: Function.emptyFunction,
		extractVideoId: Function.emptyFunction,
		setOptions: function (options)
		{
			$telerik.$.extend(this.options, options || {});

			this.options.videoId = this.options.videoId || this.extractVideoId(this.options.baseUrl);
		},
		_getDefaultOptions: function ()
		{
			return {
				width: 400,
				height: 300,
				hasFullscreenButton: false,
				hasTitle: false,
				enableAutoplay: false,
				baseUrl: "",
				videoId: ""
			};
		}
	};
	VideoEmbedCodeParser.iframeTemplate = '<iframe src="{url}" width="{width}" height="{height}" frameborder="0"></iframe>';
	VideoEmbedCodeParser.registerClass("VideoEmbedCodeParser");

	YouTubeEmbedCodeParser = function (options)
	{
		YouTubeEmbedCodeParser.initializeBase(this, [options]);
	};
	YouTubeEmbedCodeParser.prototype =
	{
		getParserType: function () { return "youtube"; },
		getProviderType: function() { return VideoProvider.YouTube },
		addVideoParameters: function (parameters)
		{
			if (!this.options.hasTitle)
				parameters.push("title=");
			if (!this.options.hasFullscreenButton)
				parameters.push("fs=0");
			if (this.options.enableAutoplay)
				parameters.push("autoplay=1");
		},
		buildEmbedUrl: function ()
		{
			return "http://www.youtube" + (this.options.enablePrivacyEnhanced ? "-nocookie" : "") + ".com/embed/" + this.options.videoId;
		},
		extractVideoId: function (url)
		{
			var result = url.match(/v=([^&]+)/) || url.match(/([^\/]+)(?=[&#])/) || url.match(/([^\/]+)$/);

			return result ? result[1] : "";
		}
	};
	YouTubeEmbedCodeParser.registerClass("YouTubeEmbedCodeParser", VideoEmbedCodeParser);

	VimeoEmbedCodeParser = function (options)
	{
		VimeoEmbedCodeParser.initializeBase(this, [options]);
	};
	VimeoEmbedCodeParser.prototype =
	{
		getParserType: function () { return "vimeo"; },
		getProviderType: function() { return VideoProvider.Vimeo },
		addVideoParameters: function (parameters)
		{
			if (!this.options.hasTitle)
				parameters.push("title=0");
			if (!this.options.hasFullscreenButton)
				parameters.push("fullscreen=0");
			if (this.options.enableAutoplay)
				parameters.push("autoplay=1");

			parameters.push("byline=0");
			parameters.push("portrait=0");
		},
		buildEmbedUrl: function ()
		{
			return "http://player.vimeo.com/video/" + this.options.videoId;
		},
		extractVideoId: function (url)
		{
			var result = url.match(/([^\/]+)(?=[&#])/) || url.match(/([^\/]+)$/);

			return result ? result[1] : "";
		}
	};
	VimeoEmbedCodeParser.registerClass("VimeoEmbedCodeParser", VideoEmbedCodeParser);
	NullEmbedCodeParser = function ()
	{
		NullEmbedCodeParser.initializeBase(this);
	}
	NullEmbedCodeParser.prototype =
	{
		getParserType: function () { return "nullParser" },
		buildEmbedUrl: function () { return ""; },
		extractVideoId: function () { return ""; }
	};
	NullEmbedCodeParser.registerClass("NullEmbedCodeParser", VideoEmbedCodeParser);
	VideoEmbedCodeParser.NullParser = new NullEmbedCodeParser();

	VideoProvider = function() {}
	VideoProvider.prototype = {
		YouTube: 1,
		Vimeo: 2
	};
	VideoProvider.registerEnum("VideoProvider");
</script>