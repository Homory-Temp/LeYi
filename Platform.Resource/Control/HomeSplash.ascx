<%@ Control Language="C#" AutoEventWireup="true" CodeFile="HomeSplash.ascx.cs" Inherits="Control.ControlHomeSplash" %>

<div id="focus" class="focus">
	<asp:Repeater runat="server" ID="r_img">
		<ItemTemplate>
			<a href='<%# ((XElement) Container.DataItem).Attribute("Url").Value %>' style='<%# Container.ItemIndex == 0 ? "display: inline;": "display: none;" %>'>
				<img alt="" src='<%# ((XElement) Container.DataItem).Attribute("Image").Value %>' width="690" height="300">
			</a>
		</ItemTemplate>
	</asp:Repeater>
	<ul class="focus-dots">
		<asp:Repeater runat="server" ID="r_div">
			<ItemTemplate>
				<li class='<%# Container.ItemIndex==0?"dot-hot": "" %>'></li>
			</ItemTemplate>
		</asp:Repeater>
	</ul>
</div>
