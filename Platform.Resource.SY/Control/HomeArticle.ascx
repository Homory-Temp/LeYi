<%@ Control Language="C#" AutoEventWireup="true" CodeFile="HomeArticle.ascx.cs" Inherits="Control.ControlHomeArticle" %>
<%@ Import Namespace="System.Web.Configuration" %>
<%@ Import Namespace="Homory.Model" %>

<telerik:RadAjaxPanel runat="server" ID="HomeArticlePanel">
	<asp:Timer runat="server" ID="HomeArticleTimer" Interval="10000" Enabled="False" OnTick="HomeArticleTimer_OnTick"></asp:Timer>
        <div  class="box class-feed" style="width:262px; height: 630px;">
		<div class="box-hd">最新资源</div>
		<div class="box-bd" style="height: 600px;">
			<ul class="hot-list">
				<asp:Repeater ID="homory_article" runat="server">
					<ItemTemplate>
					

                              <dl  id="msg_2356466"  uid="105964"  style="display: block;">
		                                    <dt> 
			                <a href='<%# string.Format("../Go/{0}?Id={1}", ((Homory.Model.ResourceType)Eval("Type"))== Homory.Model.ResourceType.视频 ? "ViewVideo" : "ViewPlain", Eval("Id")) %>'>
                                <asp:Image Width="49"  Height="49"  ImageUrl='<%# string.Format("../Image/img/{0}.png", Eval("Thumbnail")) %>'  CssClass="face face_40" runat="server"></asp:Image></a>
		                </dt>
		                <dd>
			                <div>
				                <p  class="tle">
					
					                <a style="color:#227DC5;" href='<%# string.Format("../Go/{0}?Id={1}", ((Homory.Model.ResourceType)Eval("Type"))== Homory.Model.ResourceType.视频 ? "ViewVideo" : "ViewPlain", Eval("Id")) %>'>
									                <%# ((Homory.Model.Resource)Container.DataItem).Title.CutString(12) %>
								                </a>
				                </p>
				                <p  class="clearfix pb5">
					                <span  class="fl" ><%# UC(Eval("UserId")) %>&nbsp;&nbsp;&nbsp;&nbsp;</span><a  class="fl"  href='<%# string.Format("../Go/Personal?Id={0}", Eval("UserId")) %>'><%# ((Homory.Model.Resource)Container.DataItem).User.DisplayName %></a>
					                <span  class="fr tim"><%# ((DateTime)Eval("Time")).FormatTime() %></span>
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
