<%@ Control Language="C#" AutoEventWireup="true" CodeFile="HomeHonor.ascx.cs" Inherits="Control.ControlHomeHonor" %>
<%@ Import Namespace="System.Web.Configuration" %>
<%@ Import Namespace="Homory.Model" %>

<telerik:RadAjaxPanel runat="server" ID="HomeHonorPanel">
	<asp:Timer runat="server" ID="HomeHonorTimer" Interval="30000" Enabled="True" OnTick="HomeHonorTimer_OnTick"></asp:Timer>
	<div class="box">
		<div class="box-hd">荣誉排行</div>
		<div class="box-bd">
			<ul  id="classList2"  class="class-list"  style="margin-top: 0px; height: 420px;">
				<asp:Repeater ID="homory_article" runat="server">
					<ItemTemplate>
						<li style="display: list-item;">
						<div style="float: left">
							<img src='<%# string.Format("../Image/honor/{0}.jpg", Container.ItemIndex) %>' width="30" height="48">
						</div>
						<div style="float:left; margin-left:8px"><a href='<%# string.Format("../Go/Personal?Id={0}", Eval("Id")) %>'>
								<asp:Image Width="49"  Height="49"  ImageUrl='<%# P(((Homory.Model.ViewTS)Container.DataItem).Icon) %>'  CssClass="fl" runat="server"></asp:Image>
							</a></div>
						<div class="cl-r">
							<p class="f14"><%# UC(Eval("Id")).ToString().Replace("江苏省", string.Empty).Replace("江苏", string.Empty).Replace("无锡市", string.Empty).Replace("无锡", string.Empty) %><br /><a href='<%# string.Format("../Go/Personal?Id={0}", Eval("Id")) %>'><%# Eval("DisplayName") %></a></p>
							<p>荣誉值：<%# Eval("Credit") %></p>
						</div>
					</li>
					</ItemTemplate>
				</asp:Repeater>
			</ul>
		</div>
	</div>
</telerik:RadAjaxPanel>


        