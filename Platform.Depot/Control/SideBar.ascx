<%@ Control Language="C#" AutoEventWireup="true" CodeFile="SideBar.ascx.cs" Inherits="Control_SideBar" %>

<telerik:RadScriptManager ID="sm" runat="server"></telerik:RadScriptManager>

<div class="container-fluid">
    <nav class="navbar navbar-info navbar-fixed-top">
        <div class="navbar-header rootPointer" onclick="top.location.href = '../Store/Home';">
            <img alt="" src="../Common/配置/StoreLogo.png" style="margin: 5px; padding: 0; height: 40px; float: left;" /><span class="navbar-brand">物资管理云平台</span>
        </div>
        <div class="collapse navbar-collapse">
            <ul class="nav navbar-nav">
                <li class="dropdown">
                </li>
            </ul>
            <a id="qb" runat="server" class="btn btn-warning navbar-btn" style="float: right; margin-right: 20px;" onserverclick="qb_ServerClick">退出</a>
            <a id="u" runat="server" class="btn btn-warning navbar-btn" style="float: right; margin-right: 10px;" onclick="return false;"></a>
        </div>
        <div style="clear: both;"></div>
    </nav>
</div>
<telerik:RadAjaxLoadingPanel ID="loading" runat="server" InitialDelayTime="1000">
    <div>&nbsp;</div>
    <div class="btn btn-lg btn-warning" style="margin-top: 50px;">正在加载 请稍候....</div>
</telerik:RadAjaxLoadingPanel>

<script src="../Content/Core/js/sidebar.js"></script>
<script>
    $('.dropdown-toggle').dropdown();
</script>
<style>
    .dropdown-toggle {
        cursor: pointer;
    }
</style>
