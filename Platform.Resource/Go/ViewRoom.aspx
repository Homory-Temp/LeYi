<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ViewRoom.aspx.cs" Inherits="Go.GoViewRoom" %>


<%@ Import Namespace="Homory.Model" %>
<%@ Register Src="~/Control/CommonTop.ascx" TagPrefix="homory" TagName="CommonTop" %>
<%@ Register Src="~/Control/CommonBottom.ascx" TagPrefix="homory" TagName="CommonBottom" %>


<!DOCTYPE html>

<html>
<head runat="server">
	<meta http-equiv="Content-Type" content="text/html; charset=UTF-8">
	<meta http-equiv="Pragma" content="no-cache">
	<title>互动资源平台 阳光课堂</title>
	<link rel="stylesheet" href="css/common.css">
	<link rel="stylesheet" href="css/common_002.css">
    <base target="_top" />
	<link rel="stylesheet" href="css/commentInputBox1.css">

	<link rel="stylesheet" href="css/play_v3.css">
	<link rel="stylesheet" href="css/detail.css">

	<link rel="stylesheet" href="css/1.css" id="skinCss">
</head>
<body class="srx-pclass">
	<form id="form1" runat="server">
		<telerik:RadScriptManager ID="Rsm" runat="server">
			<Scripts>
				<asp:ScriptReference Assembly="Telerik.Web.UI" Name="Telerik.Web.UI.Common.Core.js" />
				<asp:ScriptReference Assembly="Telerik.Web.UI" Name="Telerik.Web.UI.Common.jQuery.js" />
				<asp:ScriptReference Assembly="Telerik.Web.UI" Name="Telerik.Web.UI.Common.jQueryInclude.js" />
			</Scripts>
		</telerik:RadScriptManager>
		<homory:CommonTop runat="server" ID="CommonTop" />
		<div class="srx-bg">
			<div class="srx-wrap">


				<div class="srx-main" id="mainBox">
					<div class="srxsp-leftsp" style="background-color: #FFF">
						<div class="title-bar clearfix">
							<h1 class="p-title fl" id="t1" runat="server"></h1>

						</div>
						<div class="photo-info j-content ">
							<a id="t2" runat="server"></a>
						</div>


						<div>
							<iframe runat="server" id="t3" width="680" height="444" scrolling="no" style="border: solid 1px silver; overflow: hidden;"></iframe>
						</div>







					</div>


					<div class="srxsp-rightsp">



						<div class="mb10">
							<span style="font-size: 16px; color: #666; font-weight: bold" class="fl">在线评论</span>
						</div>

						<div class="cl"></div>
						<div class="tab1" id="tab1">

							<div style="height: 170px; border-top: 0;">
								<div id="con_one_1">
									<div class="c-prb-role clearfix">
										<div class="srx-comment-iptboxbj" id="srxCommentInputBox">
											<div style="width: 240px; height: 440px; overflow: auto; border: solid 1px silver;">
													<telerik:RadAjaxPanel runat="server" ID="cPanel" OnAjaxRequest="cPanel_AjaxRequest">
														<asp:Timer runat="server" ID="timer" Interval="3000" Enabled="True" OnTick="timer_Tick"></asp:Timer>
												<table>
														<asp:Repeater runat="server" ID="cComment">
															<ItemTemplate>
																<tr style="vertical-align: top;">
																	<td style="width: 40px;"><%# ((DateTime)Eval("Time")).ToShortTimeString() %></td>
																	<td style="width: 50px;"><%# U(Eval("UserId")).DisplayName %>：</td>
																	<td><%# Eval("Content") %></td>
																</tr>
															</ItemTemplate>
														</asp:Repeater>
												</table>
													</telerik:RadAjaxPanel>
											</div>
											<br />
											<telerik:RadAjaxPanel runat="server" ID="cDo">
												<textarea id="cContent" runat="server" style="width: 230px; height: 32px;"></textarea>
												<div class="srx-ciptbox-toolbar">
													<span class="srx-ciptbox-acts"></span>
													<a id="dodo" runat="server" class="button24 srx-ciptbox-submit" data-action="submit" onserverclick="dodo_ServerClick"><em>评论</em></a>
													<span class="srx-ciptbox-counter" data-ui-role="counter"></span>

												</div>
											</telerik:RadAjaxPanel>
										</div>
									</div>
								</div>


							</div>

						</div>











					</div>

					<br />
					<br />
					<homory:CommonBottom runat="server" ID="CommonBottom" />

				</div>
			</div>
		</div>
		<script src="js/h.js" type="text/javascript"></script>
	</form>
</body>
</html>
