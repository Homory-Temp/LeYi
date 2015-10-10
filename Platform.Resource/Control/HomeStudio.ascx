<%@ Control Language="C#" AutoEventWireup="true" CodeFile="HomeStudio.ascx.cs" Inherits="Control.ControlHomeStudio" %>
<%@ Import Namespace="System.Web.Configuration" %>
<%@ Import Namespace="Homory.Model" %>

<div class="box class-box" style="height: 288px;">
	<div class="box-hd">名师工作室<a class="fr" href="../Go/Studio">更多</a></div>
	<div class="box-bd" style="height: 270px;">
		<ul id="classList" class="class-list" style="margin-top: 8px;">
			<asp:Repeater ID="studio" runat="server">
				<ItemTemplate>
					<li style="display: list-item;">
						<a href='<%# string.Format("../Go/ViewStudio?Id={0}", Eval("Id")) %>'>
							<asp:Image runat="server" ImageUrl='<%# P(Eval("Icon")) %>' Width="70" Height="70" />
						</a>
						<div class="cl-r">
							<p class="f14"><a href='<%# string.Format("../Go/ViewStudio?Id={0}", Eval("Id")) %>'><%# Eval("Name") %></a></p>
							<p>负责人：<a href=<%# string.Format("../Go/Personal?Id={0}", ((Homory.Model.Group)Container.DataItem).GroupUser.FirstOrDefault(o=>o.State < State.审核 && o.Type == GroupUserType.创建者).UserId) %>><%# ((Homory.Model.Group)Container.DataItem).GroupUser.FirstOrDefault(o=>o.State < State.审核 && o.Type == GroupUserType.创建者) == null ? "" :  ((Homory.Model.Group)Container.DataItem).GroupUser.FirstOrDefault(o=>o.State < State.审核 && o.Type == GroupUserType.创建者).User.DisplayName%></a></p>
						</div>
					</li>
				</ItemTemplate>
			</asp:Repeater>
		</ul>
	</div>
</div>
