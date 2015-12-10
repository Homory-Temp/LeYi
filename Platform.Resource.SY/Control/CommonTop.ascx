<%@ Control Language="C#" AutoEventWireup="true" CodeFile="CommonTop.ascx.cs" Inherits="Control.ControlCommonTop" %>

<div class="srx-top">
	<div class="srx-nav pr">
		<h1 class="srx-nav-logo"><a href="../Go/Home">乐翼云资源平台</a></h1>
		<div class="srx-nav-left" id="srxNav">
			
			<div class="srx-nav-i">
				<a class="srx-ni-a" href="../Go/Home" title="首页">首页</a>
			</div>
						<div class="srx-nav-i srx-nav-iclass">
				<a class="srx-ni-a"  href="../Go/Studio" title="名师工作室">名师工作室</a>
			</div>
			<div class="srx-nav-i srx-nav-ischl">
				<a class="srx-ni-a"  href="../Go/Group" title="团队教研">团队教研</a>
			</div>
			<div class="srx-nav-i srx-nav-ishare">
				<a class="srx-ni-a" href="../Go/Rooms" title="阳光课堂">阳光课堂</a>
			</div>
            <div class="srx-nav-i srx-nav-ishare">
				<a class="srx-ni-a" href="../Go/Catalog" title="资源目录">资源目录</a>
			</div>
		</div>			
	
		<div class="srx-nav-right">
			<a runat="server" target="_self" id="common_top_sign_on_go" class="srx-ni-a" onserverclick="common_top_sign_on_go_OnServerClick">登录</a>
            <a runat="server" id="common_top_user_label" href="../Go/Center" class="srx-ni-a"></a>
            <a runat="server" target="_self" id="common_top_sign_off_go" class="srx-ni-a" onserverclick="common_top_sign_off_go_OnServerClick">退出</a>
		</div>
		
		<div class="srx-nav-search" style="width:158px;">
			<input style="color: rgb(170, 170, 170); width: 101px;" runat="server" class="srx-ns-input" id="common_top_search_content" type="text" />
			<a runat="server" target="_self" id="common_top_search_go" onserverclick="common_top_search_go_OnServerClick" class="srx-ns-btn">检索</a>
			<ul class="srx-ns-suggest" id="srxSearchSuggest" style="display:none;"></ul>
		</div>
	</div>
	<div class="srx-top-bg">
	</div>
</div>
