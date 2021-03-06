﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RoomsX.aspx.cs" Inherits="Go.GoRoomsX" %>

<%@ Register Src="~/Control/SideBar.ascx" TagPrefix="homory" TagName="SideBar" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=Edge,Chrome=1" />
    <meta name="viewport" content="width=device-width,initial-scale=1,minimum-scale=1,maximum-scale=1" />
    <title>基础平台</title>
    <script src="../Content/jQuery/jquery.min.js"></script>
    <link href="../assets/css/bootstrap.min.css" rel="stylesheet" />
    <link href="../assets/css/style-responsive.css" rel="stylesheet" />
    <link href="../assets/css/style.css" rel="stylesheet" />
    <script src="../assets/js/bootstrap.min.js"></script>
    <link href="../Content/Homory/css/common.css" rel="stylesheet" />
    <link href="../Content/Core/css/common.css" rel="stylesheet" />
    <script src="../Content/Homory/js/common.js"></script>
    <script src="../Content/Homory/js/notify.min.js"></script>
    <!--[if lt IE 9]>
	    <script src="../Content/Homory/js/html5shiv.js"></script>
	    <script src="../Content/Homory/js/respond.min.js"></script>
    <![endif]-->
</head>
<body>
    <form id="formHome" runat="server">
        <telerik:RadWindowManager runat="server" ID="Rwm" Skin="Metro">
            <Windows>
                <telerik:RadWindow ID="popup" runat="server" AutoSize="True" ShowContentDuringLoad="False" ReloadOnShow="True" KeepInScreenBounds="true" VisibleStatusbar="false" Behaviors="Close" Modal="True" Localization-Close="关闭" Title="直播回放" EnableEmbeddedScripts="True" EnableEmbeddedBaseStylesheet="True" VisibleTitlebar="True">
                </telerik:RadWindow>
            </Windows>
        </telerik:RadWindowManager>
        <script>
            function popupPopup(video, url) {
                window.radopen(url + "?" + encodeURIComponent("../Common/临时/" + video), "popup");
                return false;
            }
        </script>
        <div>
            <homory:SideBar runat="server" ID="SideBar" />
        </div>
        <telerik:RadAjaxPanel ID="panel" runat="server" CssClass="container-fluid" LoadingPanelID="loading">
            <div class="row">
                <div class="col-md-12">
                    <div style="float: left; width: 50%; text-align: left; line-height: 36px;">
                        <h6 class="btn btn-primary">直播视频</h6>
                        <br />
                        <asp:Repeater ID="rLeft" runat="server">
                            <ItemTemplate>
                                <%# LabelLeft(Eval("Name")) %>&nbsp;&nbsp;<a style="background-color: rgb(110, 207, 245); padding: 4px 8px; color: white; cursor: pointer;" onclick='<%# "popupPopup(\""+Eval("Name")+"\", \"" + System.Web.Configuration.WebConfigurationManager.AppSettings["ResourceCenter"].Replace("/Go/Center", "/Go/PlayVideoX") + "\");" %>'>预览</a>&nbsp;<a style="background-color: rgb(110, 207, 245); padding: 4px 8px; color: white;" target="_blank" href='<%# System.Web.Configuration.WebConfigurationManager.AppSettings["ResourceCenter"].Replace("/Go/Center", "/Go/Publishing?Type=Media") %>'>发布</a>
                                <br />
                            </ItemTemplate>
                        </asp:Repeater>
                    </div>
                    <div style="float: left; width: 50%; text-align: left; line-height: 36px;">
                        <h6 class="btn btn-primary">在线评论</h6>
                        <br />
                        直播间号：<telerik:RadComboBox runat="server" ID="num" Width="60" DataTextField="Ordinal" DataValueField="Id"></telerik:RadComboBox>
                        <br />
                        开始时间：<telerik:RadDateTimePicker runat="server" ID="ts" TimeView-Caption="选择时间"></telerik:RadDateTimePicker>
                        <br />
                        结束时间：<telerik:RadDateTimePicker runat="server" ID="te" TimeView-Caption="选择时间"></telerik:RadDateTimePicker>
                        <br />
                        <a id="filterGo" runat="server" onserverclick="filterGo_ServerClick" style="background-color: rgb(110, 207, 245); padding: 4px 8px; color: white; margin-left: 80px;">查询</a><br />
                        <asp:CheckBox runat="server" ID="chk" AutoPostBack="true" Text="全选" OnCheckedChanged="chk_CheckedChanged" Style="margin-left: 1px;" />
                        <table style="line-height: 20px;">
                            <asp:Repeater ID="rRight" runat="server">
                                <ItemTemplate>
                                    <tr>
                                        <td>
                                            <asp:CheckBox runat="server" ID="chkX" AutoPostBack="true" /></td>
                                        <td style="width: 80px;">
                                            <label style="font-size: 12px;"><%# ((DateTime)Eval("Time")).ToString("yyyy-MM-dd HH:mm:ss") %></label></td>
                                        <td style="width: 60px;">
                                            <label style="font-size: 12px;"><%# U(Eval("UserId")).DisplayName %>：</label></td>
                                        <td>
                                            <label style="font-size: 12px;"><%# Eval("Content") %></label></td>
                                    </tr>
                                </ItemTemplate>
                            </asp:Repeater>
                        </table>
                    </div>
                    <div style="clear: both;"></div>
                </div>
            </div>
        </telerik:RadAjaxPanel>
    </form>
</body>
</html>
