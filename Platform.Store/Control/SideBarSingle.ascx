﻿<%@ Control Language="C#" AutoEventWireup="true" CodeFile="SideBarSingle.ascx.cs" Inherits="Control_SideBar" %>

<telerik:RadScriptManager ID="sm" runat="server"></telerik:RadScriptManager>

<telerik:RadCodeBlock runat="server">
    <div class="container-fluid">
        <nav class="navbar navbar-info navbar-fixed-top">
            <div class="navbar-header rootPointer" onclick="top.location.href = '../Store/Home';">
                <img alt="" src="../Common/配置/StoreLogo.png" style="margin: 5px; padding: 0; height: 40px; float: left;" /><span class="navbar-brand">物资管理云平台</span>
            </div>
            <div class="collapse navbar-collapse">
                <a id="qb" runat="server" class="btn btn-warning navbar-btn" style="float: right; margin-right: 20px;" onserverclick="qb_ServerClick">退出</a>
                <a id="u" runat="server" class="btn btn-warning navbar-btn" style="float: right; margin-right: 10px;" onclick="return false;"></a>
                <ul class="nav navbar-nav" style="float: right; margin-right: 80px;">
                    <li class="dropdown">
                        <a class="dropdown-toggle navbar-link" data-toggle="dropdown">物资管理</a>
                        <ul class="dropdown-menu">
                            <li><a href='<%= "../StoreIn/InMultiple?StoreId={0}".Formatted(StoreId) %>'>购置登记</a></li>
                            <li><a href='<%= "../StoreIn/InMultiple?StoreId={0}".Formatted(StoreId) %>'>物资入库</a></li>
                            <li><a href='<%= "../StoreIn/InMultiple?StoreId={0}".Formatted(StoreId) %>'>物资借领</a></li>
                            <li><a href='<%= "../StoreIn/InMultiple?StoreId={0}".Formatted(StoreId) %>'>物资归还</a></li>
                            <li><a href='<%= "../StoreIn/InMultiple?StoreId={0}".Formatted(StoreId) %>'>物资管理</a></li>
                            <li style='<%= (CurrentStore.State == Models.StoreState.固产 ? "display: block;": "display: none;") %>'><a href="../StoreImport/Go">物资分库</a></li>
                            <li style='<%= (CurrentStore.State == Models.StoreState.固产 ? "display: block;": "display: none;") %>'><a href="../StoreImport/Go">数据导入</a></li>
                        </ul>
                    </li>
                    <li class="dropdown">
                        <a class="dropdown-toggle navbar-link" data-toggle="dropdown">日常查询</a>
                        <ul class="dropdown-menu">
                            <li><a href='<%= "../StoreIn/InMultiple?StoreId={0}".Formatted(StoreId) %>'>购置查询</a></li>
                            <li><a href='<%= "../StoreIn/InMultiple?StoreId={0}".Formatted(StoreId) %>'>入库查询</a></li>
                            <li><a href='<%= "../StoreIn/InMultiple?StoreId={0}".Formatted(StoreId) %>'>借领查询</a></li>
                            <li><a href='<%= "../StoreIn/InMultiple?StoreId={0}".Formatted(StoreId) %>'>归还查询</a></li>
                            <li><a href='<%= "../StoreIn/InMultiple?StoreId={0}".Formatted(StoreId) %>'>物资查询</a></li>
                            <li style='<%= (CurrentStore.State == Models.StoreState.固产 ? "display: block;": "display: none;") %>'><a href="../StoreImport/Go">导入查询</a></li>
                        </ul>
                    </li>
                    <li class="dropdown">
                        <a class="dropdown-toggle navbar-link" data-toggle="dropdown">统计报表</a>
                        <ul class="dropdown-menu">
                            <li><a href='<%= "../StoreIn/InMultiple?StoreId={0}".Formatted(StoreId) %>'>购置报表</a></li>
                            <li><a href='<%= "../StoreIn/InMultiple?StoreId={0}".Formatted(StoreId) %>'>入库报表</a></li>
                            <li><a href='<%= "../StoreIn/InMultiple?StoreId={0}".Formatted(StoreId) %>'>出库报表</a></li>
                            <li><a href='<%= "../StoreIn/InMultiple?StoreId={0}".Formatted(StoreId) %>'>借还报表</a></li>
                            <li><a href='<%= "../StoreIn/InMultiple?StoreId={0}".Formatted(StoreId) %>'>库存报表</a></li>
                            <li style='<%= (CurrentStore.State == Models.StoreState.固产 ? "display: block;": "display: none;") %>'><a href="../StoreImport/Go">导入查询</a></li>
                        </ul>
                    </li>
                    <li class="dropdown">
                        <a class="dropdown-toggle navbar-link" data-toggle="dropdown">系统设置</a>
                        <ul class="dropdown-menu">
                            <li><a href='<%= "../StoreIn/InMultiple?StoreId={0}".Formatted(StoreId) %>'>物资类别</a></li>
                            <li><a href='<%= "../StoreIn/InMultiple?StoreId={0}".Formatted(StoreId) %>'>基础数据</a></li>
                            <li><a href='<%= "../StoreIn/InMultiple?StoreId={0}".Formatted(StoreId) %>'>权限设置</a></li>
                        </ul>
                    </li>
                </ul>
            </div>
            <div style="clear: both;"></div>
        </nav>
    </div>

    <div class="container-fluid">
        <div class="row">
            <div class="col-md-12">
                <input type="button" class="btn btn-tumblr" value='<%= CurrentStore.Name %>' />
                <span id="crumb" runat="server" class="btn btn-info"></span>
                <span class="btn btn-info" onclick="top.location.href = '../Store/Home';" style="float: right;">仓库切换</span>
                <hr style="color: #2B2B2B; margin-top: 4px;" />
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
