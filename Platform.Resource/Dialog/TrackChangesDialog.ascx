<%@ Control Language="C#" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI.Editor" TagPrefix="tools" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI.Widgets" TagPrefix="widgets" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI.Dialogs" TagPrefix="dialogs" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<script type="text/javascript">
	Type.registerNamespace("Telerik.Web.UI.Widgets");
	Telerik.Web.UI.Widgets.TrackChangesDialog = function(element)
	{
		Telerik.Web.UI.Widgets.TrackChangesDialog.initializeBase(this, [element]);
		this._editor = null;
	}

	Telerik.Web.UI.Widgets.TrackChangesDialog.prototype = {
		initialize: function()
		{
			Telerik.Web.UI.Widgets.TrackChangesDialog.callBaseMethod(this, "initialize");
			this.setupChildren();
		},

		setupChildren: function()
		{
			this._initContentField = $get("initialContent");
			this._curContentField = $get("currentContent");
		},

		clientInit: function(clientParameters)
		{
			this._editor = clientParameters;
			window.frames["responseIframe"].document.open();
			window.frames["responseIframe"].document.close();
			this._curContentField.value = this.encodePostbackContent(this._editor.get_html(true)).replace(/%/g, "~");
			this._initContentField.value = this.encodePostbackContent(this._editor.get_initialContent()).replace(/%/g, "~");

			//Force postback, and change target so that the form output is returned to the iframe
			var form = document.forms[0];
			form.target = "responseIframe";
			form.submit();
		},

		_encodeHtmlContent: function(content, toEncode)
		{
			var characters = new Array('%', '<', '>', '!', '"', '#', '$', '&', '\'', '(', ')', ',', ':', ';', '=', '?',
										'[', ']', '\\', '^', '`', '{', '|', '}', '~', '+');
			var newContent = content;
			var i;
			if (toEncode)
			{
				for (i = 0; i < characters.length; i++)
				{
					newContent = newContent.replace(new RegExp("\\x" + characters[i].charCodeAt(0).toString(16), "ig"), '%' + characters[i].charCodeAt(0).toString(16));
				}
			}
			else
			{
				for (i = characters.length - 1; i >= 0; i--)
				{
					newContent = newContent.replace(new RegExp('\%' + characters[i].charCodeAt(0).toString(16), "ig"), characters[i]);
				}
			}
			return newContent;
		},

		encodePostbackContent: function(content)
		{
			return this._encodeHtmlContent(content, true);
		},

		decodePostbackContent: function(content)
		{
			return this._encodeHtmlContent(content, false);
		},

		dispose: function()
		{
			this._editor = null;
			this._initContentField = null;
			this._curContentField = null;
			Telerik.Web.UI.Widgets.TrackChangesDialog.callBaseMethod(this, "dispose");
		}

	}

	Telerik.Web.UI.Widgets.TrackChangesDialog.registerClass('Telerik.Web.UI.Widgets.TrackChangesDialog', Telerik.Web.UI.RadWebControl, Telerik.Web.IParameterConsumer);

</script>

<style type="text/css">
	html, body, form
	{
		height: 100%;
		overflow: hidden;
	}
	#contentPanel, #initializer
	{
		height: 100%;
	}
	#dialogControl
	{
		height: 97%;
		display: block;
	}
</style>
<input id="initialContent" type="hidden" name="initialContent" runat="server" />
<input id="currentContent" type="hidden" name="currentContent" runat="server" />

<asp:Panel ID="responsePanel" runat="server" EnableViewState="false" CssClass="reTrackChangesWrapper">
	<style runat="server" id="cssFormatting" type="text/css">
		body
		{
			background-color: transparent;
		}
		.reTrackChangesWrapper {
			background-color: #fff;
		}
		legend
		{
			font: normal 12px Arial, Verdana, Sans-serif;
		}
		.diff_new
		{
			background-color: #00ff00;
		}
		.diff_deleted
		{
			color: Red;
			text-decoration: line-through;
		}
		table.diff_new
		{
			border: 2px solid green;
		}
		.diff_new td
		{
			padding: 5px, 5px, 5px, 5px;
			background-color: #00ff00;
		}
		table.diff_deleted
		{
			padding: 5px, 5px, 5px, 5px;
			border: 2px solid red;
			filter: alpha(opacity=20);
			-moz-opacity: 0.2;
			opacity: 0.2;
		}

		.obsoleteContainer {
			border: 1px dashed #BB0000;
			margin: 1em 3px;
			padding: 1em;
		}
		.obsoleteMessage {
			color: #d00;
		}
		.obsoleteMessage a {
			color: #b00;
		}
	</style>
</asp:Panel>
<iframe id="responseIframe" frameborder="0" name="responseIframe" style="width: 100%; height: 100%;"></iframe>
