<%@ Control Language="C#" AutoEventWireup="true" CodeFile="HomeCatalog.ascx.cs" Inherits="Control.ControlHomeCatalog" %>

<div class="box function">
	<div class="box-hd">资源目录</div>
	<div class="box-bd">
		<ul class="func-list">
			<asp:Repeater ID="homory_course" runat="server">
				<ItemTemplate>
					<li class='<%# string.Format("func{0}", Container.ItemIndex + 1) %>' style="cursor: pointer;" onclick=<%# string.Format("window.open('../Go/Catalog?Course={0}');", Eval("Id")) %>>
						<p class="timu-a"><a href='<%# string.Format("../Go/Catalog?#jump_hm{0}", Container.ItemIndex + 1) %>'><%# Eval("Name") %></a></p>
					</li>
				</ItemTemplate>
			</asp:Repeater>
		</ul>
	</div>
</div>
