<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Publishing.aspx.cs" Inherits="Go.GoPublishing" %>

<%@ Register Src="~/Control/CommonTop.ascx" TagPrefix="homory" TagName="CommonTop" %>
<%@ Register Src="~/Control/CommonBottom.ascx" TagPrefix="homory" TagName="CommonBottom" %>
<%@ Register Src="~/Control/PublishAttachment.ascx" TagPrefix="homory" TagName="PublishAttachment" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <title>资源平台 - 资源发布</title>
    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8">
    <meta http-equiv="Pragma" content="no-cache">
    <script src="../Script/jquery.min.js"></script>
    <script src="../Script/jquery.media.js"></script>
    <link href="../Style/common.css" rel="stylesheet" />
    <base target="_top" />
</head>
<body>
    <form runat="server">
        <telerik:RadScriptManager ID="Rsm" runat="server">
            <Scripts>
                <asp:ScriptReference Assembly="Telerik.Web.UI" Name="Telerik.Web.UI.Common.Core.js" />
                <asp:ScriptReference Assembly="Telerik.Web.UI" Name="Telerik.Web.UI.Common.jQuery.js" />
                <asp:ScriptReference Assembly="Telerik.Web.UI" Name="Telerik.Web.UI.Common.jQueryInclude.js" />
            </Scripts>
        </telerik:RadScriptManager>
        <telerik:RadWindowManager runat="server" ID="Rwm" Skin="Metro">
            <Windows>
                <telerik:RadWindow ID="popup_import" runat="server" Title="导入" ReloadOnShow="True" Width="500" Height="250" VisibleStatusbar="false" Behaviors="Move,Close" Modal="True" CenterIfModal="True" Localization-Close="关闭">
                </telerik:RadWindow>
                <telerik:RadWindow ID="popup_attachment" runat="server" Title="附件" ReloadOnShow="True" Width="500" Height="250" VisibleStatusbar="false" Behaviors="Move,Close" CenterIfModal="True" Modal="True" Localization-Close="关闭">
                </telerik:RadWindow>
                <telerik:RadWindow ID="popup_user" runat="server" OnClientClose="userSelected" Title="人员选择" ShowContentDuringLoad="false" ReloadOnShow="true" Width="1000" Height="560" VisibleStatusbar="false" Behaviors="Move,Close" CenterIfModal="true" Modal="true" Localization-Close="关闭">
                </telerik:RadWindow>
                <telerik:RadWindow ID="popup_notify" runat="server" Title="提示" ReloadOnShow="True" AutoSize="true" VisibleStatusbar="false" Behaviors="Move,Close" CenterIfModal="True" Modal="True" Localization-Close="关闭">
                    <ContentTemplate>
                        <div style="width: 180px;">
                            <p>&nbsp;</p>
                            <p>请完成以下操作后再发布资源：</p>
                            <p>&nbsp;</p>
                            <p>1、导入资源</p>
                            <p>2、选择资源类别</p>
                            <p>&nbsp;</p>
                        </div>
                    </ContentTemplate>
                </telerik:RadWindow>
            </Windows>
        </telerik:RadWindowManager>
        <script>
            function popImport() {
                window.radopen(null, "popup_import");
                return false;
            }

            function popAttachment() {
                window.radopen(null, "popup_attachment");
                return false;
            }

            function popNotify() {
                window.radopen(null, "popup_notify");
                return false;
            }

            var ______su = "0";

            function popupOther(sender, args) {
                window.radopen("../Popup/UserSelector?Id=" + sender.get_value(), "popup_user");
                return false;
            }
        </script>
        <div>
            <homory:CommonTop runat="server" ID="CommonTop" />
        </div>
        <div class="srx-bg">
            <div class="srx-wrap">
                <div class="srx-main clearfix">
                    <div class="srx-l0">
                        <div class="editor-top">
                            <label id="publish_title_label" class="title">标题：</label>
                            <input runat="server" class="editor-title" id="publish_title_content" type="text" />
                            <br />
                            <br />
                                <style>
                                    .cccc {
                                        line-height: 32px;
                                        height: 32px;
                                        vertical-align: middle;
                                        margin: auto;
                                    }

                                    .inFR {
                                        float: right;
                                        margin-left: 0;
                                        margin-right: 10px;
                                    }

                                    .btnFR {
                                        float: right;
                                        margin-left: 10px;
                                        margin-right: 10px;
                                    }

                                    .btnFR2 {
                                        float: right;
                                        margin-left: 10px;
                                        margin-right: 0;
                                    }
                                </style>
                            <telerik:RadAjaxPanel runat="server" ID="apxx" CssClass="cccc" OnAjaxRequest="apxx_AjaxRequest">
                                <label class="title" style="float: left;">作者：</label>
                                <telerik:RadButton ID="btnOher" runat="server" Text="代发" ToggleType="CheckBox" CssClass="btnFR2" TabIndex="2" Width="100px" Height="32px" OnClientClicked="popupOther" AutoPostBack="false"></telerik:RadButton>
                                <telerik:RadButton ID="btnMe" runat="server" Text="本人" ToggleType="CheckBox" CssClass="btnFR" Width="100px" TabIndex="1" Height="32px" AutoPostBack="true" OnClick="btnMe_Click"></telerik:RadButton>
                                <input runat="server" class="editor-title inFR" id="publish_ohter_publish" onfocus="this.blur();" type="text" style="width: 456px; cursor: default;" />
                            </telerik:RadAjaxPanel>
                            <telerik:RadCodeBlock runat="server">
                                <script>
                                    function userSelected() {
                                        $find("<%= apxx.ClientID %>").ajaxRequest(______su);
                                    }
                                </script>
                            </telerik:RadCodeBlock>
                            <br />
                            <br />
                            <div style="width: 738px; height: auto;">
                                <telerik:RadAjaxPanel runat="server" ID="publish_preview_empty">
                                    <div style="margin: 40px;">
                                        <p>
                                            请点击右侧“导入”按钮导入资源，如需修改请重新导入
                                        </p>
                                        <p>&nbsp;</p>
                                        <p>
                                            系统只保留最近一次导入的资源，再次导入会覆盖当前资源
                                        </p>
                                    </div>
                                </telerik:RadAjaxPanel>
                                <br />
                                <br />
                                <telerik:RadAjaxPanel runat="server" ID="publish_preview_plain">
                                    <iframe runat="server" src="../Document/web/PdfViewer.aspx" width="738" height="500" id="publish_preview_pdf" style="margin-top: 10px;"></iframe>
                                </telerik:RadAjaxPanel>
                                <telerik:RadAjaxPanel runat="server" ID="publish_preview_media">
                                    <asp:Timer runat="server" ID="preview_timer" Interval="3000" Enabled="True" OnTick="preview_timer_Tick"></asp:Timer>
                                    <telerik:RadMediaPlayer ID="publish_preview_player" runat="server" Width="738px" Height="500px" FullScreenButtonToolTip="全屏" HDButtonToolTip="高清" VolumeButtonToolTip="静音"></telerik:RadMediaPlayer>
                                </telerik:RadAjaxPanel>
                            </div>
                            <div id="journalEditor" class="editor-content">
                                <asp:Timer ID="publish_editor_timer" runat="server" Enabled="False" Interval="60000" OnTick="publish_editor_timer_OnTick"></asp:Timer>
                                <label runat="server" id="publish_editor_label"></label>
                                <telerik:RadEditor runat="server" ID="publish_editor" Width="100%" Height="200px" MaxHtmlLength="4000" ContentAreaMode="Iframe" RenderMode="Auto" ExternalDialogsPath="../Dialog" DialogHandlerUrl="Telerik.Web.UI.DialogHandler.axd" Language="zh-CN" LocalizationPath="../Language">
                                    <Tools>
                                        <telerik:EditorToolGroup Tag="InsertToolbar">
                                            <telerik:EditorTool Name="JustifyLeft" />
                                            <telerik:EditorTool Name="JustifyCenter" />
                                            <telerik:EditorTool Name="JustifyRight" />
                                            <telerik:EditorTool Name="JustifyFull" />
                                            <telerik:EditorTool Name="JustifyNone" />
                                            <telerik:EditorSeparator />
                                            <telerik:EditorTool Name="Bold" />
                                            <telerik:EditorTool Name="Italic" />
                                            <telerik:EditorTool Name="Underline" />
                                            <telerik:EditorTool Name="StrikeThrough" />
                                            <telerik:EditorSeparator />
                                            <telerik:EditorSplitButton Name="ForeColor"></telerik:EditorSplitButton>
                                            <telerik:EditorSplitButton Name="BackColor"></telerik:EditorSplitButton>
                                            <telerik:EditorSeparator />
                                            <telerik:EditorTool Name="InsertDate" />
                                            <telerik:EditorTool Name="InsertTime" />
                                            <telerik:EditorSplitButton Name="InsertSymbol"></telerik:EditorSplitButton>
                                            <telerik:EditorSeparator />
                                            <telerik:EditorTool Name="ImageManager" ShortCut="CTRL+M" />
                                            <telerik:EditorTool Name="FlashManager" />
                                            <telerik:EditorTool Name="MediaManager" />
                                            <telerik:EditorSeparator />
                                            <telerik:EditorTool Name="InsertOrderedList" />
                                            <telerik:EditorTool Name="InsertUnorderedList" />
                                            <telerik:EditorTool Name="LinkManager" />
                                            <telerik:EditorTool Name="Unlink" />
                                            <telerik:EditorSeparator />
                                            <telerik:EditorTool Name="Superscript" />
                                            <telerik:EditorTool Name="Subscript" />
                                            <telerik:EditorTool Name="Indent" />
                                            <telerik:EditorTool Name="Outdent" />
                                            <telerik:EditorSeparator />
                                            <telerik:EditorDropDown Name="FontSize" Width="30">
                                            </telerik:EditorDropDown>
                                        </telerik:EditorToolGroup>
                                    </Tools>
                                    <Content>
                                    </Content>
                                    <ImageManager AllowFileExtensionRename="False" AllowMultipleSelection="True" EnableAsyncUpload="True" ImageEditorFileSuffix="_缩略图" MaxUploadFileSize="10240000" />
                                    <FlashManager AllowFileExtensionRename="False" AllowMultipleSelection="True" EnableAsyncUpload="True" MaxUploadFileSize="10240000" />
                                    <MediaManager AllowFileExtensionRename="False" AllowMultipleSelection="True" EnableAsyncUpload="True" MaxUploadFileSize="1024000000" />
                                    <TrackChangesSettings CanAcceptTrackChanges="False" />
                                </telerik:RadEditor>
                                <asp:UpdatePanel runat="server" UpdateMode="Conditional">
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="publish_editor_timer" />
                                    </Triggers>
                                </asp:UpdatePanel>
                            </div>
                        </div>
                        <div>
                            <div>
                                <homory:PublishAttachment runat="server" ID="PublishAttachment" />
                            </div>
                            <div>
                                <telerik:RadAjaxPanel runat="server" ID="publish_attachment_list_panel" OnAjaxRequest="publish_attachment_list_panel_OnAjaxRequest">
                                    <telerik:RadListView ID="publish_attachment_list" runat="server" OnNeedDataSource="publish_attachment_list_OnNeedDataSource">
                                        <ItemTemplate>
                                            <img src='<%# string.Format("../Image/img/{0}.jpg", (int)Eval("FileType")) %>' />
                                            <a href='<%# string.Format("{0}", Eval("Source")) %>'><%# Eval("Title") %></a>
                                            <asp:ImageButton ImageUrl="../Image/img/Delete.png" runat="server" ID="publish_attachment_delete" CommandArgument='<%# Eval("Id") %>' OnClick="publish_attachment_delete_OnClick" />
                                        </ItemTemplate>
                                    </telerik:RadListView>
                                </telerik:RadAjaxPanel>
                            </div>
                        </div>
                        <p>&nbsp;</p>
                    </div>
                    <div class="srx-l1" id="columnRight">
                        <input type="hidden" id="status" name="status" />
                        <div class="u-rbox sendto" id="sendto">
                            <div id="addBlog_otherTabs" class="addBlog-box1">
                                <!--发布导入开始-->
                                <telerik:RadAjaxPanel runat="server" ID="publish_publish_panel">
                                    <asp:ImageButton ID="pubish_publish_go" runat="server" Style="cursor: pointer;" ImageUrl="../image/img/fb.png" OnClick="pubish_publish_go_OnClick" />
                                </telerik:RadAjaxPanel>
                                <telerik:RadCodeBlock ID="block" runat="server">
                                    <script type="text/javascript">
                                        function refreshAttachments() {
                                            var attachments = $find("<%=publish_attachment_list.ClientID %>");
                                            attachments.rebind();
                                        }
                                    </script>
                                </telerik:RadCodeBlock>
                                <div class="u-rbox sendto" id="sendto" style="padding-left: 8px; cursor: pointer;">
                                    <img alt="资源导入图片" src="../image/img/dr.png" onclick="popImport();" />
                                </div>
                                <!--发布导入结束-->
                            </div>
                        </div>
                        <div class="u-rbox sendto" id="sendto">
                            <div id="addBlog_ownerTabs" class="addBlog-box">
                                <div style="clear: both; height: 10px;"></div>
                                <div id="addBlog_privacySet" class="ui-renren-privacy ui-widget ui-select">
                                    <telerik:RadAjaxPanel runat="server" ID="publish_main_catalog_panel">
                                        <asp:Panel runat="server" ID="publish_catalog_panel">
                                            <h5>栏目：</h5>
                                            <telerik:RadDropDownTree runat="server" ID="publish_catalog" Width="140" CheckBoxes="SingleCheck" ExpandNodeOnSingleClick="true" AutoPostBack="True" DataTextField="Name" DataValueField="Id" DataFieldID="Id" DataFieldParentID="ParentId" OnEntryAdded="publish_catalog_OnEntryAdded" OnEntryRemoved="publish_catalog_OnEntryRemoved"></telerik:RadDropDownTree>
                                        </asp:Panel>
                                        <asp:Panel runat="server" ID="publish_course_panel">
                                            <h5>学科：</h5>
                                            <telerik:RadComboBox runat="server" ID="publish_course" Width="140" AppendDataBoundItems="true" AutoPostBack="true" DataTextField="Name" DataValueField="Id" OnSelectedIndexChanged="publish_course_SelectedIndexChanged">
                                                <Items>
                                                    <telerik:RadComboBoxItem Text="" Value="" />
                                                </Items>
                                            </telerik:RadComboBox>
                                        </asp:Panel>
                                        <asp:Panel runat="server" ID="publish_grade_panel">
                                            <h5>年级：</h5>
                                            <telerik:RadComboBox runat="server" ID="publish_grade" Width="140" AppendDataBoundItems="true" AutoPostBack="true" DataTextField="Name" DataValueField="Id" OnSelectedIndexChanged="publish_grade_SelectedIndexChanged">
                                                <Items>
                                                    <telerik:RadComboBoxItem Text="" Value="" />
                                                </Items>
                                            </telerik:RadComboBox>
                                        </asp:Panel>
                                        <asp:Panel ID="sync_assistant" runat="server" Style="margin-top: 6px;">
                                            <asp:CheckBox ID="sync_ass" runat="server" Text="发布至资源小助手" OnCheckedChanged="sync_ass_CheckedChanged" Style="margin: 0 0 0 16px;" AutoPostBack="true" />
                                        </asp:Panel>
                                    </telerik:RadAjaxPanel>
                                </div>
                            </div>
                        </div>
                        <div class="u-rbox sendto" id="sendto">
                            <div id="addBlog_otherTabs" class="addBlog-box">
                                <telerik:RadAjaxPanel runat="server" ID="publish_tag_panel">
                                    <div style="height: 2px;">&nbsp;</div>
                                    <h3>自定义标签：</h3>
                                    <div style="height: 2px;"></div>
                                    <telerik:RadTextBox runat="server" ID="publish_tag_content" MaxLength="5" Width="100" CssClass="coreFloatLeft"></telerik:RadTextBox>&nbsp;&nbsp;
									<a runat="server" id="publish_tag_add" onserverclick="publish_tag_add_OnClick">添加</a><br />
                                    <br />
                                    <asp:Repeater runat="server" ID="publish_tag_tags">
                                        <ItemTemplate>
                                            <a runat="server" id="publish_tag_delete" onserverclick="publish_tag_delete_OnServerClick"><%# Container.DataItem %></a>&nbsp;&nbsp;
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </telerik:RadAjaxPanel>
                            </div>
                        </div>
                        <div class="u-rbox sendto" id="sendto">
                            <div id="addBlog_otherTabs" class="addBlog-box">
                                <div class="u-rbox auth">
                                    <div style="height: 2px;">&nbsp;</div>
                                    <h3>公开范围：</h3>
                                    <telerik:RadAjaxPanel runat="server" ID="publish_open_panel">
                                        <telerik:RadButton runat="server" Width="50" ButtonType="ToggleButton" ToggleType="Radio" Checked="True" GroupName="OpenTypeRadio" Text="互联网&nbsp;&nbsp;&nbsp;" Value="1" OnClick="OnOpenClick"></telerik:RadButton>
                                        <telerik:RadButton runat="server" Width="50" ButtonType="ToggleButton" ToggleType="Radio" GroupName="OpenTypeRadio" Text="学校" Value="2" OnClick="OnOpenClick"></telerik:RadButton>
                                        <%--<telerik:RadButton runat="server" Width="50" ButtonType="ToggleButton" ToggleType="Radio" GroupName="OpenTypeRadio" Text="所在园区" Value="3" OnClick="OnOpenClick"></telerik:RadButton>--%>
                                        <telerik:RadButton runat="server" Width="50" ButtonType="ToggleButton" ToggleType="Radio" GroupName="OpenTypeRadio" Text="不公开&nbsp;&nbsp;&nbsp;" Value="4" OnClick="OnOpenClick"></telerik:RadButton>
                                    </telerik:RadAjaxPanel>
                                </div>
                            </div>
                        </div>
                        <div class="u-rbox sendto" id="sendto" style="padding-bottom: 5px;">
                            <div id="addBlog_otherTabs" class="addBlog-box">
                                <div class="u-rbox auth">
                                    <div style="height: 2px;">&nbsp;</div>
                                    <h3>获奖情况：</h3>
                                    <telerik:RadAjaxPanel runat="server" ID="publish_prize_panel">
                                        <asp:Panel runat="server" ID="publish_prize_range_panel">
                                            <telerik:RadComboBox runat="server" ID="publish_prize_range" Width="100" Label="范围：" OnSelectedIndexChanged="publish_prize_range_OnSelectedIndexChanged" AutoPostBack="True">
                                                <Items>
                                                    <telerik:RadComboBoxItem runat="server" Text="" Value="0" />
                                                    <telerik:RadComboBoxItem runat="server" Text="全国" Value="1" />
                                                    <telerik:RadComboBoxItem runat="server" Text="省级" Value="2" />
                                                    <telerik:RadComboBoxItem runat="server" Text="市级" Value="3" />
                                                    <telerik:RadComboBoxItem runat="server" Text="区级" Value="4" />
                                                    <telerik:RadComboBoxItem runat="server" Text="校级" Value="5" />
                                                    <telerik:RadComboBoxItem runat="server" Text="其他" Value="6" />
                                                </Items>
                                            </telerik:RadComboBox>
                                        </asp:Panel>
                                        <asp:Panel runat="server" ID="publish_prize_level_panel">
                                            <telerik:RadComboBox runat="server" ID="publish_prize_level" Width="100" Label="奖项：" OnSelectedIndexChanged="publish_prize_level_OnSelectedIndexChanged" AutoPostBack="True">
                                                <Items>
                                                    <telerik:RadComboBoxItem runat="server" Text="" Value="0" />
                                                    <telerik:RadComboBoxItem runat="server" Text="特等奖" Value="1" />
                                                    <telerik:RadComboBoxItem runat="server" Text="一等奖" Value="2" />
                                                    <telerik:RadComboBoxItem runat="server" Text="二等奖" Value="3" />
                                                    <telerik:RadComboBoxItem runat="server" Text="三等奖" Value="4" />
                                                    <telerik:RadComboBoxItem runat="server" Text="其他" Value="5" />
                                                </Items>
                                            </telerik:RadComboBox>
                                        </asp:Panel>
                                    </telerik:RadAjaxPanel>
                                </div>
                            </div>
                        </div>
                        <span style="height: 5px;">&nbsp;</span>
                    </div>
                </div>
                <homory:CommonBottom runat="server" ID="CommonBottom" />
            </div>
        </div>
        <style>
            html .RadSearchBox .rsbInput {
                color: black;
                background-color: white;
                display: block;
                border: solid #ddd 1px;
                height: 32px;
                line-height: 32px;
                text-align: left;
                color: #727272;
                font-size: 12px;
                font-weight: bolder;
                float: left;
                margin-left: 8px;
            }

            html .RadSearchBox .rsbInner {
                display: block;
                line-height: 0;
                border: none;
                padding: 0;
                position: relative;
                _overflow: hidden;
                width: 610px;
                margin-top: -1px;
            }

            html .rbToggleRadio, html .rbToggleRadioChecked {
                margin-top: 8px;
            }

            .xs{
                background-color: wheat;
                margin-left: 8px;
            }
        </style>
    </form>
</body>
</html>
