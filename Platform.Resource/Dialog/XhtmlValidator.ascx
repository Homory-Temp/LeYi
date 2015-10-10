<%@ Control Language="C#" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI.Editor" TagPrefix="tools" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI.Widgets" TagPrefix="widgets" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI.Dialogs" TagPrefix="dialogs" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<script type="text/javascript">
	Type.registerNamespace("Telerik.Web.UI.Widgets");
	Telerik.Web.UI.Widgets.XhtmlValidator = function(element)
	{
		Telerik.Web.UI.Widgets.XhtmlValidator.initializeBase(this, [element]);

		this._editor = null;
	}

	Telerik.Web.UI.Widgets.XhtmlValidator.prototype = {
		initialize: function()
		{
			Telerik.Web.UI.Widgets.XhtmlValidator.callBaseMethod(this, "initialize");

			this.setupChildren();
		},

		setupChildren: function()
		{
			this._editorContent = $get("editorContent");
			this._editorFullPage = $get("editorFullPage");
		},

		clientInit: function(clientParameters)
		{
			this._editor = clientParameters;
			window.frames["responseIframe"].document.open();
			window.frames["responseIframe"].document.close();
			var content = ""
			if (this._editor.get_fullPage() == false)
			{
				content = "<div>" + this._editor.get_html(true) + "</div>";
				this._editorFullPage.value = "false";
			}
			else
			{
				content = this._editor.get_html(true);
				this._editorFullPage.value = "true";
			}
			this._editorContent.value = this.encodePostbackContent(content).replace(/%/g, "~");
			var form = document.forms[0];
			//Change target so that the form output is returned to the iframe
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
			this._editorContent = null;
			this._editorFullPage = null;
			Telerik.Web.UI.Widgets.XhtmlValidator.callBaseMethod(this, "dispose");
		}
	}

	Telerik.Web.UI.Widgets.XhtmlValidator.registerClass('Telerik.Web.UI.Widgets.XhtmlValidator', Telerik.Web.UI.RadWebControl, Telerik.Web.IParameterConsumer);
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
</style>
<asp:Panel ID="contentPanel" runat="server">
	<asp:RadioButtonList RepeatDirection="Horizontal" AutoPostBack="true" ID="xhtmlSelect"
		runat="server">
		<asp:ListItem Value='~3c!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.1//EN" "http://www.w3.org/TR/xhtml11/DTD/xhtml11.dtd"~3e' Selected="True"> XHTML 1.1</asp:ListItem>
		<asp:ListItem Value='~3c!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Strict//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-strict.dtd"~3e'>XHTML 1.0 Strict</asp:ListItem>
		<asp:ListItem Value='~3c!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd"~3e'> XHTML 1.0 Transitional</asp:ListItem>
		<asp:ListItem Value='~3c!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.01 Transitional//EN" "http://www.w3.org/TR/html4/loose.dtd"~3e'>HTML 4.01</asp:ListItem>
	</asp:RadioButtonList>
	<input id="editorContent" type="hidden" name="editorContent" runat="server" />
	<input id="editorFullPage" type="hidden" name="editorContent" runat="server" />
	<iframe id="responseIframe" frameborder="0" name="responseIframe" style="width: 100%; height: 95%;"></iframe>
</asp:Panel>
