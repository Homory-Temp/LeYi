<%@ Control Language="C#" AutoEventWireup="true" CodeFile="C6Article.ascx.cs" Inherits="Control.ControlC6Article" %>
<%@ Import Namespace="System.Web.Configuration" %>
<%@ Import Namespace="Homory.Model" %>

<telerik:RadAjaxPanel runat="server" ID="HomeArticlePanel">
	<asp:Timer runat="server" ID="HomeArticleTimer" Interval="10000" Enabled="False" OnTick="HomeArticleTimer_OnTick"></asp:Timer>
        <div  class="box class-feed" style="width:400px; height: 330px;">

		<div class="box-bd">
			<ul class="hot-list">
				<asp:Repeater ID="homory_article" runat="server">
					<ItemTemplate>
					

                              <dl  id="msg_2356466"  uid="105964"  style="display: block;">
		                                    <dt> 
			                <a target="_blank" href='<%# string.Format("../Go/Personal?Id={0}", Eval("UserId")) %>'>
                                <asp:Image Width="49"  Height="49"  ImageUrl='<%# Eval("Image") %>'  CssClass="face face_40" runat="server"></asp:Image></a>
		                </dt>
		                <dd>
			                <div>
				                <p  class="clearfix pb5">
					                <a target="_blank" class="fl"  href='<%# string.Format("../Go/Personal?Id={0}", Eval("UserId")) %>'><%# ((Homory.Model.Resource)Container.DataItem).User.DisplayName %></a>&nbsp;&nbsp;&nbsp;&nbsp;<%# UC(Eval("UserId")) %>
					                <span  class="fr tim"><%# ((DateTime)Eval("Time")).FormatTime() %></span>
				                </p>
				                <p  class="tle">
					
					                <a target="_blank" href='<%# string.Format("../Go/{1}?Id={0}", Eval("Id"),  ((Homory.Model.ResourceType)Eval("Type"))== Homory.Model.ResourceType.视频 ? "ViewVideo" : "ViewPlain") %>'>
									                <%# ((Homory.Model.Resource)Container.DataItem).Title.CutString(12) %>
								                </a>
				                </p>
				
				
			                </div>
		                </dd>
	                </dl>
					</ItemTemplate>
				</asp:Repeater>
			</ul>
		</div>
	</div>
</telerik:RadAjaxPanel>
