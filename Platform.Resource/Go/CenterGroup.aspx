<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CenterGroup.aspx.cs" Inherits="Go.GoCenterGroup" %>

<%@ Register Src="~/Control/CommonTop.ascx" TagPrefix="homory" TagName="CommonTop" %>
<%@ Register Src="~/Control/CommonBottom.ascx" TagPrefix="homory" TagName="CommonBottom" %>
<%@ Register Src="~/Control/CenterLeft.ascx" TagPrefix="homory" TagName="CenterLeft" %>
<%@ Register Src="~/Control/CenterRight.ascx" TagPrefix="homory" TagName="CenterRight" %>

<!DOCTYPE html>

<html>
<head runat="server">
	<title>资源平台 - 个人中心</title>
	<meta http-equiv="Content-Type" content="text/html; charset=UTF-8">
	<meta http-equiv="Pragma" content="no-cache">
	<script src="../Script/jquery.min.js"></script>
	<link rel="stylesheet" href="../Style/common.css">
	<link rel="stylesheet" href="../Style/common(1).css">
	<link rel="stylesheet" href="../Style/index1.css">
	<link rel="stylesheet" href="../Style/2.css" id="skinCss">
	    <base target="_top" />
<style>
		html .rbPrimaryIcon {
			margin-top: 6px;
		}

.rootHidden {
  display: none;
}

.coreAuto {
	margin: auto;
}

.coreCenter {
	text-align: center;
}

.coreFull {
	width: 100%;
}

.coreLeft {
	text-align: left;
}

.coreRight {
	text-align: right;
}

.coreTop {
	vertical-align: top;
}

.coreMiddle {
	vertical-align: middle;
}

.coreFloatLeft {
	float: left;
}

.coreClear {
	clear: both;
}

html .RadGrid .rgBatchContainer {
  margin: auto;
}

html .RadSearchBox, html .RadSearchBox .rsbInner, html .RadSearchBox .rsbInput {
  text-align: center;
  vertical-align: middle;
  width: 250px;
  height: 30px;
  line-height: 24px;
  font-size: 16px;
}

  html .RadSearchBox .rsbButtonSearch {
    width: 24px;
    height: 24px;
    margin: 2px 2px 2px 0;
    cursor: pointer;
  }

.GroupBoxTitle {
  font-size:14px;
	font-family: "Segoe UI", Arial, Helvetica, sans-serif;
}

.FullWidth {
  width: 100%;
}

  .disappear_item {
	  display: none;
  }

  .appear_item {
	  display: block;
  }

  html .RadTreeView_Default {
	  font-size: 14px;
  }
	</style>
</head>
<body class="srx-phome">
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
                <telerik:RadWindow ID="popup_publish" Title="资源发布" runat="server" AutoSize="False" Width="320" Height="330" ShowContentDuringLoad="false" ReloadOnShow="False" KeepInScreenBounds="true" VisibleStatusbar="false" Behaviors="Close" Modal="True" Localization-Close="关闭" EnableEmbeddedScripts="True" EnableEmbeddedBaseStylesheet="True" VisibleTitlebar="True">
                    <ContentTemplate>
                        <style>
                            .pub_v, .pub_v:hover{display:block;margin:0 auto;background:url("../image/up/pub_v.png") 0 0 no-repeat;width:173px;height:48px;line-height:43px;color:#fff;padding-left:15px;overflow:hidden;text-decoration:none;font-size:16px;}
                            .pub_a, .pub_a:hover{display:block;margin:0 auto;background:url("../image/up/pub_a.png") 0 0 no-repeat;width:173px;height:48px;line-height:43px;color:#fff;padding-left:15px;overflow:hidden;text-decoration:none;font-size:16px;}
                            .pub_c, .pub_c:hover{display:block;margin:0 auto;background:url("../image/up/pub_c.png") 0 0 no-repeat;width:173px;height:48px;line-height:43px;color:#fff;padding-left:15px;overflow:hidden;text-decoration:none;font-size:16px;}
                            .pub_p, .pub_p:hover{display:block;margin:0 auto;background:url("../image/up/pub_p.png") 0 0 no-repeat;width:173px;height:48px;line-height:43px;color:#fff;padding-left:15px;overflow:hidden;text-decoration:none;font-size:16px;}
                        </style>
                        <div style="width: 280px; text-align: center; margin: auto;">
                            <a class="pub_v" style="cursor: pointer; margin: 20px auto 10px 50px;" href="Publishing.aspx?Type=Media">发布视频</a>
                            <a class="pub_a" style="cursor: pointer; margin: 10px auto 10px 50px;" href="Publishing.aspx?Type=Article">发布文章</a>
                            <a class="pub_c" style="cursor: pointer; margin: 10px auto 10px 50px;" href="Publishing.aspx?Type=Courseware">发布课件</a>
                            <a class="pub_p" style="cursor: pointer; margin: 10px auto 20px 50px;" href="Publishing.aspx?Type=Paper">发布试卷</a>
                        </div>
                    </ContentTemplate>
                </telerik:RadWindow>
				<telerik:RadWindow ID="popup_create" runat="server" AutoSize="True" Width="840" Height="350" ShowContentDuringLoad="False" ReloadOnShow="True" KeepInScreenBounds="true" VisibleStatusbar="false" Behaviors="Close" Modal="True" Localization-Close="关闭" Title="创建团队" EnableEmbeddedScripts="True" EnableEmbeddedBaseStylesheet="True" VisibleTitlebar="True">
				</telerik:RadWindow>
				<telerik:RadWindow ID="popup_edit" runat="server" AutoSize="True" Width="840" Height="350" ShowContentDuringLoad="False" ReloadOnShow="True" KeepInScreenBounds="true" VisibleStatusbar="false" Behaviors="Close" Modal="True" Localization-Close="关闭" Title="编辑团队" EnableEmbeddedScripts="True" EnableEmbeddedBaseStylesheet="True" VisibleTitlebar="True">
				</telerik:RadWindow>
				<telerik:RadWindow ID="windowCatalog" runat="server" NavigateUrl="~/Popup/StudioCatalog.aspx" Width="800" Height="400" CssClass="coreCenter coreMiddle" ReloadOnShow="true" VisibleStatusbar="false" Behaviors="Close" Modal="true" Title="栏目管理" Localization-Close="关闭">
				</telerik:RadWindow>
				<telerik:RadWindow ID="windowMember" runat="server" NavigateUrl="~/Popup/StudioMember.aspx" Width="800" Height="400" CssClass="coreCenter coreMiddle" ReloadOnShow="true" VisibleStatusbar="false" Behaviors="Close" Modal="true" Title="成员选择" Localization-Close="关闭">
                </telerik:RadWindow>
				<telerik:RadWindow ID="windowRes" runat="server" NavigateUrl="~/Popup/CenterStudio.aspx" Width="800" Height="400" CssClass="coreCenter coreMiddle" ReloadOnShow="true" VisibleStatusbar="false" Behaviors="Close" Modal="true" Title="资源呈送" Localization-Close="关闭">
                </telerik:RadWindow>
			</Windows>
		</telerik:RadWindowManager>
		<script>
			var window_publish;

			function popupPublish() {
				window_publish = window.radopen(null, "popup_publish");
				return false;
			}
			function closePublish() {
				window_publish.close();
				return false;
			}
			function popupCreate() {
				window.radopen("../Popup/GroupManage", "popup_create");
				return false;
			}
			function popupEdit(id) {
				window.radopen("../Popup/GroupManageX.aspx?Id=" + id, "popup_edit");
				return false;
			}
			function PopCatalog(id) {
				window.radopen("../Popup/StudioCatalog.aspx?" + id, "windowCatalog");
				return false;
			}
			function PopMember(id) {
				window.radopen("../Popup/StudioMember.aspx?" + id, "windowMember");
				return false;
			}
			function PopRes(id) {
				window.radopen("../Popup/CenterStudio.aspx?" + id, "windowRes");
				return false;
			}
		</script>
		<telerik:RadCodeBlock runat="server">
			<script>
				function groupCreated() {
					$find("<%= gList.ClientID %>").fireCommand("RebindListView");
				}
			</script>
		</telerik:RadCodeBlock>
		<homory:CommonTop runat="server" ID="CommonTop" />
		<div class="srx-bg">
			<div class="srx-wrap">
				<%--左上方个人信息区--%>
				<div class="srx-main srx-main-bg">
					<homory:CenterLeft runat="server" ID="CenterLeft" />
					<div class="srx-right">
						<div class="srx-r1">
							<div class="msgFeed mt15">
								<telerik:RadAjaxPanel runat="server" ID="groupPanel">
									<div class="frame-nav-inner" style="position: relative; top: -20px; border-bottom: 2px solid #CCC;">
										<ul class="fd-nav-list clearfix">
											<li class="fd-nav-item fd-nav-cur-item fd-nav-item-list"><a href="#">教研团队</a></li>
											<li class="fd-nav-item fd-nav-item-list" style="cursor: pointer;"><a onclick="popupCreate(); return false;">创建团队</a></li>
											<li class="fd-nav-item fd-nav-item-list" style="cursor: pointer;display: none;"><a href="../Go/Group.aspx">加入团队</a></li>
										</ul>
									</div>
									<br />
									<br />
									<telerik:RadListView runat="server" ID="gList" ItemPlaceholderID="gHolder" OnNeedDataSource="gList_NeedDataSource">
										<LayoutTemplate>
											<table style="width: 570px; text-align: center; font-size: 14px;">
												<tr style="background: #F1F4F9;">
													<td style="width: 50px; height: 35px;">头像</td>
													<td style="width: 100px;">名称</td>
													<td style="width: 80px;">编号</td>
													<td style="width: 80px;">学科</td>
													<td style="width: 80px;">年级</td>
													<td style="width: 180px;">操作</td>
												</tr>
												<tr>
													<td colspan="6" style="width: 100%;">
														<hr />
													</td>
												</tr>
												<asp:PlaceHolder ID="gHolder" runat="server"></asp:PlaceHolder>
											</table>
										</LayoutTemplate>
										<ItemTemplate>
											<tr>
												<td style="width: 50px;">
													<asp:Image runat="server" ImageUrl='<%# Eval("Icon") %>' Width="40" Height="40" /></td>
												<td style="width: 100px; text-align: left;"><a href='<%# string.Format("../Go/ViewGroup?Id={0}", Eval("Id")) %>'><%# Eval("Name") %></a></td>
												<td style="width: 80px;"><a href='<%# string.Format("../Go/ViewGroup?Id={0}", Eval("Id")) %>'><%# Eval("Serial") %></a></td>
												<td style="width: 80px;"><%# CatalogName(Eval("CourseId")) %></td>
												<td style="width: 80px;"><%# CatalogName(Eval("GradeId")) %></td>
												<td style="width: 180px; cursor: pointer;">
													<a onclick=<%# string.Format("PopCatalog('{0}'); return false;", Eval("Id")) %>>栏目</a>&nbsp;&nbsp;<a onclick=<%# string.Format("PopRes('{0}'); return false;", Eval("Id")) %>>资源</a>&nbsp;&nbsp;<a onclick=<%# string.Format("PopMember('{0}'); return false;", Eval("Id")) %>>成员</a>
													<br />
													<a onclick=<%# string.Format("popupEdit('{0}'); return false;", Eval("Id")) %>>编辑</a>&nbsp;&nbsp;<a id="btnDel" data-id='<%# Eval("Id") %>' runat="server" onclick="return window.confirm('确定删除该教研团队吗？');" OnServerClick="btnDel_OnServerClick">删除</a>
												</td>
											</tr>
											<tr>
												<td colspan="6" style="width: 100%;">
													<hr style="color: silver;" />
												</td>
											</tr>
										</ItemTemplate>
									</telerik:RadListView>
									<telerik:RadListView runat="server" ID="gListX" ItemPlaceholderID="gHolderX" OnNeedDataSource="gListX_NeedDataSource">
										<LayoutTemplate>
											<table style="width: 570px; text-align: center; font-size: 14px;">
												<asp:PlaceHolder ID="gHolderX" runat="server"></asp:PlaceHolder>
											</table>
										</LayoutTemplate>
										<ItemTemplate>
											<tr>
												<td style="width: 50px;">
													<asp:Image runat="server" ImageUrl='<%# Eval("Icon") %>' Width="40" Height="40" /></td>
												<td style="width: 100px; text-align: left;"><a href='<%# string.Format("../Go/ViewGroup?Id={0}", Eval("Id")) %>'><%# Eval("Name") %></a></td>
												<td style="width: 80px;"><a href='<%# string.Format("../Go/ViewGroup?Id={0}", Eval("Id")) %>'><%# Eval("Serial") %></a></td>
												<td style="width: 80px;"><%# CatalogName(Eval("CourseId")) %></td>
												<td style="width: 80px;"><%# CatalogName(Eval("GradeId")) %></td>
												<td style="width: 180px; cursor: pointer;">
													<a onclick=<%# string.Format("PopRes('{0}'); return false;", Eval("Id")) %>>资源</a>&nbsp;&nbsp;<a id="btnQuit" data-id='<%# Eval("Id") %>' runat="server" onclick="return window.confirm('确定退出该教研团队吗？');" OnServerClick="btnQuit_OnServerClick">退出</a>
												</td>
											</tr>
											<tr>
												<td colspan="6" style="width: 100%;">
													<hr style="color: silver;" />
												</td>
											</tr>
										</ItemTemplate>
									</telerik:RadListView>
								</telerik:RadAjaxPanel>
							</div>
						</div>
						<homory:CenterRight runat="server" ID="CenterRight" />
					</div>
				</div>
				<homory:CommonBottom runat="server" ID="CommonBottom" />
			</div>
		</div>
	</form>
</body>
</html>
