<%@ Control Language="C#" AutoEventWireup="true" CodeFile="SideBar.ascx.cs" Inherits="Control.ControlSideBar" %>

<link href="../Content/Core/css/sidebar.css" rel="stylesheet" />

<telerik:RadScriptManager ID="sm" runat="server"></telerik:RadScriptManager>

<div class="container-fluid">
    <nav class="navbar navbar-info navbar-fixed-top" style="color: black;">
        <div class="navbar-header">
            <a class="navbar-brand" href="../Go/Home.aspx">数据中心&nbsp;&nbsp;》&nbsp;&nbsp;</a>
        </div>
        <div class="collapse navbar-collapse">
            <ul class="nav navbar-nav">
                <asp:Repeater ID="repeater" runat="server">
                    <ItemTemplate>
                        <li class="dropdown">
                            <a class="dropdown-toggle navbar-link" data-toggle="dropdown"><%# Eval("Name") %></a>
                            <ul class="dropdown-menu">
                                <%# SubMenu(Container.DataItem as Homory.Model.Menu) %>
                            </ul>
                        </li>
                    </ItemTemplate>
                </asp:Repeater>
            </ul>
            <a id="qb" runat="server" class="btn btn-warning navbar-btn" style="float: right; margin-right: 20px;" onserverclick="qb_Click">退出</a>
            <a id="u" runat="server" class="btn btn-warning navbar-btn" style="float: right; margin-right: 10px;" onclick="return false;"></a>
        </div>
        <div style="clear: both;"></div>
    </nav>
</div>
<div class="container-fluid">
    <div class="row">
        <div class="col-md-12">
            <div id="title" class="btn-info btn btn-lg" style="display: none;"></div>
        </div>
    </div>
    <div class="row">&nbsp;</div>
</div>
<telerik:RadAjaxLoadingPanel ID="loading" runat="server">
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
