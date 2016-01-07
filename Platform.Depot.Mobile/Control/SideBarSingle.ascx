<%@ Control Language="C#" AutoEventWireup="true" CodeFile="SideBarSingle.ascx.cs" Inherits="Control_SideBarSingle" %>

<telerik:RadScriptManager ID="sm" runat="server"></telerik:RadScriptManager>

<telerik:RadCodeBlock runat="server">
    <div class="container-fluid">
        <nav class="navbar navbar-info navbar-fixed-top">
            <div class="navbar-header rootPointer" onclick="top.location.href = '../Depot/Home.aspx';">
                <img alt="" src="../Common/配置/StoreLogo.png" style="margin: 5px; padding: 0; height: 40px; float: left;" /><span class="navbar-brand">物品与资产</span>
                <a id="qb" runat="server" class="btn btn-warning navbar-btn" style="float: right; margin-right: 20px;" onserverclick="qb_ServerClick">退出</a>
                <a id="u" runat="server" class="btn btn-warning navbar-btn" style="float: right; margin-right: 10px; display: none;" onclick="return false;"></a>
            </div>
            <div style="clear: both;"></div>
        </nav>
    </div>

    <div class="container-fluid">
        <div class="row">
            <div class="col-md-12">
                <input id="storeName" runat="server" type="button" class="btn btn-tumblr" onserverclick="storeName_ServerClick" style="display: none;" />
                <span id="crumb" runat="server" class="btn btn-info"></span>
                <span class="btn btn-info" onclick='<%= "top.location.href = \"../Depot/DepotHome.aspx?DepotId={0}\"".Formatted(Depot.Id) %>' style="float: right; margin-right: 13px;">快捷菜单</span>
                <span style="clear: both;"></span>
            </div>
            <div class="col-md-8">
            </div>
            <div class="col-md-12">
            </div>
        </div>
        <div class="row">
        </div>
    </div>
</telerik:RadCodeBlock>

<telerik:RadAjaxLoadingPanel ID="loading" runat="server" InitialDelayTime="1000" Enabled="false">
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
