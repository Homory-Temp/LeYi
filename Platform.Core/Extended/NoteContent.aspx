<%@ Page Language="C#" AutoEventWireup="true" CodeFile="NoteContent.aspx.cs" Inherits="Extended.ExtendedNoteContent" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=Edge,Chrome=1" />
    <meta name="viewport" content="width=device-width,initial-scale=1,minimum-scale=1,maximum-scale=1" />
    <title>编辑正文</title>
	<%-- ReSharper disable Html.PathError --%>
    <link href="../Content/Homory/css/common.css" rel="stylesheet" />
    <link href="../Content/Core/css/home.css" rel="stylesheet" />
    <link href="../Content/Core/css/common.css" rel="stylesheet" />
    <script src="../Content/jQuery/jquery.min.js"></script>
    <script src="../Content/Homory/js/common.js"></script>
    <script src="../Content/Homory/js/notify.min.js"></script>
    <script src="../Content/Core/js/home.js"></script>
	<%-- ReSharper restore Html.PathError --%>
</head>
<body style="margin: 0; padding: 0; overflow: hidden; text-align: center;">
    <form id="formHome" runat="server">
        <telerik:RadScriptManager ID="scriptManager" runat="server"></telerik:RadScriptManager>
        <script type="text/javascript">
            function GetRadWindow() {
                var oWindow = null;
                if (window.radWindow) oWindow = window.radWindow;
                else if (window.frameElement.radWindow) oWindow = window.frameElement.radWindow;
                return oWindow;
            }

            function RadClose() {
                GetRadWindow().close();
            }
        </script>
        <telerik:RadAjaxPanel ID="panelInner" runat="server">
        </telerik:RadAjaxPanel>
        <br />
        <br />
        <telerik:RadEditor ID="homory_editor" runat="server" Style="margin-left: 15px; text-align: left;" Width="750px" Height="330px" Language="zh-CN" ContentAreaMode="Div" ExternalDialogsPath="~/Dialog/" DialogHandlerUrl="~/Telerik.Web.UI.DialogHandler.axd">
            <Tools>
                <telerik:EditorToolGroup Tag="MainToolbar">
                    <telerik:EditorSplitButton Name="Undo">
                    </telerik:EditorSplitButton>
                    <telerik:EditorSplitButton Name="Redo">
                    </telerik:EditorSplitButton>
                </telerik:EditorToolGroup>
                <telerik:EditorToolGroup Tag="InsertToolbar">
                    <telerik:EditorTool Name="LinkManager" ShortCut="CTRL+K / CMD+K" />
                    <telerik:EditorTool Name="Unlink" ShortCut="CTRL+SHIFT+K / CMD+SHIFT+K" />
                </telerik:EditorToolGroup>
                <telerik:EditorToolGroup>
                    <telerik:EditorTool Name="Superscript" />
                    <telerik:EditorTool Name="Subscript" />
                    <telerik:EditorTool Name="InsertHorizontalRule" />
                    <telerik:EditorTool Name="InsertDate" />
                    <telerik:EditorTool Name="InsertTime" />
                    <telerik:EditorSeparator />
                </telerik:EditorToolGroup>
                <telerik:EditorToolGroup>
                    <telerik:EditorDropDown Name="RealFontSize">
                    </telerik:EditorDropDown>
                </telerik:EditorToolGroup>
                <telerik:EditorToolGroup>
                    <telerik:EditorTool Name="Bold" ShortCut="CTRL+B / CMD+B" />
                    <telerik:EditorTool Name="Italic" ShortCut="CTRL+I / CMD+I" />
                    <telerik:EditorTool Name="Underline" ShortCut="CTRL+U / CMD+U" />
                    <telerik:EditorTool Name="StrikeThrough" />
                    <telerik:EditorSeparator />
                    <telerik:EditorTool Name="JustifyLeft" />
                    <telerik:EditorTool Name="JustifyCenter" />
                    <telerik:EditorTool Name="JustifyRight" />
                    <telerik:EditorTool Name="JustifyFull" />
                    <telerik:EditorTool Name="JustifyNone" />
                    <telerik:EditorSeparator />
                    <telerik:EditorTool Name="Indent" />
                    <telerik:EditorTool Name="Outdent" />
                    <telerik:EditorSeparator />
                    <telerik:EditorTool Name="InsertOrderedList" />
                    <telerik:EditorTool Name="InsertUnorderedList" />
                    <telerik:EditorSeparator />
                </telerik:EditorToolGroup>
                <telerik:EditorToolGroup>
                    <telerik:EditorSplitButton Name="ForeColor">
                    </telerik:EditorSplitButton>
                    <telerik:EditorSplitButton Name="BackColor">
                    </telerik:EditorSplitButton>
                </telerik:EditorToolGroup>
            </Tools>
            <TrackChangesSettings CanAcceptTrackChanges="False"></TrackChangesSettings>
        </telerik:RadEditor>
        <br />
        <br />
        <telerik:RadButton ID="buttonOk" runat="server" Skin="MetroTouch" Text="保存" OnClick="buttonOk_Click"></telerik:RadButton>
    </form>
</body>
</html>
