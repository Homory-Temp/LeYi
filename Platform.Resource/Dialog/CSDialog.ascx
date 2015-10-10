<%@ Control Language="C#" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI.Editor" TagPrefix="tools" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI.Widgets" TagPrefix="widgets" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI.Dialogs" TagPrefix="dialogs" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<script type="text/javascript">
	Type.registerNamespace("Telerik.Web.UI.Widgets");
	Telerik.Web.UI.Widgets.CSDialog = function (element)
	{
		Telerik.Web.UI.Widgets.CSDialog.initializeBase(this, [element]);

		this._editor = null;
	}

	Telerik.Web.UI.Widgets.CSDialog.prototype = {
		initialize: function ()
		{
			Telerik.Web.UI.Widgets.CSDialog.callBaseMethod(this, "initialize");

			this.setupChildren();
		},

		setupChildren: function ()
		{
			this._editorContent = $get("editorContent");
			this._editorFullPage = $get("editorFullPage");
		},

		clientInit: function (clientParameters)
		{
			this._editor = clientParameters;
			var content = this._editor.get_html(true);
			window.frames["responseIframe"].document.open();
			window.frames["responseIframe"].document.close();
			if (this._editor.get_fullPage() == false)
			{
				this._editorContent.value = this.encodePostbackContent("<div>" + content + "</div>");
				this._editorFullPage.value = "false";
			}
			else
			{
				this._editorContent.value = this.encodePostbackContent(content);
				this._editorFullPage.value = "true";
			}

			var form = document.forms[0];
			//Change target so that the form output is returned to the iframe
			form.target = "responseIframe";
			form.submit();
			var panel = $find("LoadingPanel1");
			panel.show("responseIframe");
		},

		_encodeHtmlContent: function (content, toEncode)
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

		encodePostbackContent: function (content)
		{
			return this._encodeHtmlContent(content, true);
		},

		decodePostbackContent: function (content)
		{
			return this._encodeHtmlContent(content, false);
		},

		dispose: function ()
		{
			this._editor = null;
			this._editorContent = null;
			this._editorFullPage = null;
			Telerik.Web.UI.Widgets.CSDialog.callBaseMethod(this, "dispose");
		}
	}

	Telerik.Web.UI.Widgets.CSDialog.registerClass('Telerik.Web.UI.Widgets.CSDialog', Telerik.Web.UI.RadWebControl, Telerik.Web.IParameterConsumer);

	function hideLoadingPanel()
	{
		$find("LoadingPanel1").hide("responseIframe");
	}
</script>
<style type="text/css">
	html, body, form
	{
		height: 100%;
		overflow: hidden;
		font: normal 12px Arial, Verdana, Sans-serif;
	}
	#contentPanel, #dialogControl, #initializer
	{
		height: 100%;
	}
	.loadingLabel
	{
		position: absolute;
		left: 250px;
		top: 225px;
	}
	RadAjax_Windows7 .raColor
	{
		background-color: #ffffff;
	}
</style>
<telerik:RadAjaxLoadingPanel ID="LoadingPanel1" runat="server" Skin="Windows7" Transparency="0">
	<asp:Label ID="Label2" runat="server" CssClass="loadingLabel" ForeColor="Navy" Font-Size="Large">Currently scanning your content contribution for compliance... </asp:Label>
</telerik:RadAjaxLoadingPanel>
<asp:Panel ID="contentPanel" runat="server">
	<input id="editorContent" type="hidden" name="editorContent" runat="server" />
	<input id="editorFullPage" type="hidden" name="editorContent" runat="server" />
	<iframe id="responseIframe" runat="server" scrolling="auto" frameborder="0" name="responseIframe"
		style="width: 1000px; height: 100%;"></iframe>
</asp:Panel>
