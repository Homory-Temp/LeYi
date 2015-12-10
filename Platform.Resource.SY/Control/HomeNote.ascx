<%@ Control Language="C#" AutoEventWireup="true" CodeFile="HomeNote.ascx.cs" Inherits="Control.ControlHomeNote" %>
<%@ Import Namespace="Homory.Model" %>

<telerik:RadAjaxPanel runat="server" ID="HomeNotePanel">
	<asp:Timer runat="server" ID="HomeNoteTimer" Interval="30000" Enabled="False" OnTick="HomeNoteTimer_OnTick"></asp:Timer>
	<div class="box">
		<div class="box-hd">通知公告</div>
		<div class="box-bd">
			<ul class="hot-list">
				<asp:Repeater ID="homory_note" runat="server">
					<ItemTemplate>
						<li style="height: 23px; line-height: 23px;"><a onclick=<%# string.Format("PopupNote('{0}');", Eval("Id")) %> style="cursor: pointer;"><%# ((string)Eval("Title")).CutString(MaxTitleLength) %></a>
							<span class="fr tim"><%# ((DateTime)Eval("Time")).FormatTime() %></span>
						</li>
					</ItemTemplate>
				</asp:Repeater>
			</ul>
		</div>
	</div>
</telerik:RadAjaxPanel>
<script>
	function PopupNote(id) {
		window.radopen("../Popup/HomeNotePopup.aspx?" + id, "homory_note_view");
		return false;
	}
</script>
