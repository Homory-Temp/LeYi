<%@ Control Language="C#" AutoEventWireup="true" CodeFile="SideBar.ascx.cs" Inherits="Control_SideBar" %>

<telerik:RadScriptManager ID="sm" runat="server"></telerik:RadScriptManager>

<div class="container-fluid">
    <nav class="navbar navbar-info navbar-fixed-top">
        <div class="navbar-header rootPointer" onclick="top.location.href = '../Depot/Home.aspx';">
            <span class="navbar-brand">物品与资产</span><%--<img alt="" src="../Common/配置/StoreLogo.png" style="margin: 5px; padding: 0; height: 40px; float: left;" />--%>
            <a id="qb" runat="server" class="btn btn-warning navbar-btn" style="float: right; margin-right: 20px;" onserverclick="qb_ServerClick">退出</a>
            <a id="u" runat="server" class="btn btn-warning navbar-btn" style="float: right; margin-right: 10px; display: none;" onclick="return false;"></a>
        </div>
        <div style="clear: both;"></div>
    </nav>
</div>
<script>
    $('.dropdown-toggle').dropdown();
</script>
<style>
    .dropdown-toggle {
        cursor: pointer;
    }
</style>
