<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Rooms.aspx.cs" Inherits="Go.GoRooms" %>

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
					<div id="set1" runat="server" class="srxsp-leftsp" style="background-color: #F6F6F6; border: #ddd 1px solid; width: 940px; ">
						<div style="width: 740px; height: 85px; text-align: center; margin: 0 auto; line-height: 85px; font-size: 20px; color: #227dc5; background-image: url(images/topsp.jpg); background-repeat: no-repeat; margin-top: 40px"></div>
						<div style="width: 740px; clear: both; height: 15px; border-top: #CCC dashed 1px; margin: 0 auto;"></div>
						<div id="toH" runat="server" style="width: 740px;  text-align: center; margin: 0 auto; background-image: url(../Image/ppbg.jpg); background-repeat: repeat-y;">
							<asp:Repeater runat="server" ID="roomList">
								<ItemTemplate>
									<div style="float: left; width: 300px; height: 240px; background-color: #fff; filter: alpha(opacity:60); opacity: 0.60; margin: 0 auto; text-align: center; margin-top: 45px; margin-left: 45px; font-size: 18px; color: #000; padding-top: 10px; z-index: -99">
										<div style="width: 280px; height: 30px; line-height: 30px; margin: 0 auto"><%# Eval("Name") %></div>
										<div style="width: 280px; height: 170px; margin: 0 auto; font-size: 16px; color: #333; text-align: left"><%# Eval("Description") %></div>
										<div style="width: 80px; height: 30px; line-height: 30px; margin: 0 auto; background-color: #000; font-size: 16px; text-align: center"><a target="_blank" href='<%# string.Format("../Go/ViewRoom?Id={0}", Eval("Id")) %>' style="color: #FFF">点击进入</a></div>
									</div>
								</ItemTemplate>
							</asp:Repeater>
						</div>

					</div>

					<div>
					</div>



				</div>
				<homory:CommonBottom runat="server" ID="CommonBottom" />

			</div>
		</div>
		<script src="js/h.js" type="text/javascript"></script>
	</form>
</body>
</html>
