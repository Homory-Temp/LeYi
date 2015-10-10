<%@ Control Language="C#" AutoEventWireup="true" CodeFile="HomeGroup.ascx.cs" Inherits="Control.ControlHomeGroup" %>
<%@ Import Namespace="System.Web.Configuration" %>
<%@ Import Namespace="Homory.Model" %>

<div class="box class-box" style="height: 290px;">
	<div class="box-hd">团队教研<a class="fr" href="../Go/Group">更多</a></div>
	<div class="box-bd" style="height: 270px;">
		<ul id="classList" class="class-list" style="margin-top: 8px;">
			<asp:Repeater ID="group" runat="server">
				<ItemTemplate>
					<li style="display: list-item;">
						<a href='<%# string.Format("../Go/ViewGroup?Id={0}", Eval("Id")) %>'>
							<asp:Image runat="server" ImageUrl='<%# Eval("Icon") %>' Width="70" Height="70" />
						</a>
						<div class="cl-r">
							<p class="f14"><a href='<%# string.Format("../Go/ViewGroup?Id={0}", Eval("Id")) %>'><%# Eval("Name") %></a></p>
							<p>创建人：<a href=<%# string.Format("../Go/Personal?Id={0}", ((Homory.Model.Group)Container.DataItem).GroupUser.FirstOrDefault(o=>o.State < State.审核 && o.Type == GroupUserType.创建者).UserId) %>><%# ((Homory.Model.Group)Container.DataItem).GroupUser.FirstOrDefault(o=>o.State < State.审核 && o.Type == GroupUserType.创建者) == null ? "" :  ((Homory.Model.Group)Container.DataItem).GroupUser.FirstOrDefault(o=>o.State < State.审核 && o.Type == GroupUserType.创建者).User.DisplayName%></a></p>
						</div>
					</li>
				</ItemTemplate>
			</asp:Repeater>
		</ul>
	</div>
</div>
