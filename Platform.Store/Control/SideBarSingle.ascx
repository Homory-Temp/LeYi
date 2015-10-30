<%@ Control Language="C#" AutoEventWireup="true" CodeFile="SideBarSingle.ascx.cs" Inherits="Control_SideBar" %>

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
                            <li><a href='<%= "../StoreAction/Target?StoreId={0}".Formatted(StoreId) %>'>购置登记</a></li>
                            <li><a href='<%= "../StoreAction/In?StoreId={0}".Formatted(StoreId) %>'>物资入库</a></li>
                            <li><a href='<%= "../StoreAction/Use?StoreId={0}".Formatted(StoreId) %>'>物资出库</a></li>
                            <li style='<%= (CurrentStore.State != Models.StoreState.食品 ? "display: block;": "display: none;") %>'><a href='<%= "../StoreAction/Return?StoreId={0}".Formatted(StoreId) %>'>物资归还</a></li>
                            <li><a href='<%= "../StoreAction/Object?StoreId={0}".Formatted(StoreId) %>'>物资管理</a></li>
                            <li style='<%= (CurrentStore.State == Models.StoreState.固产 ? "display: block;": "display: none;") %>'><a href='<%= "../StoreAction/Import?StoreId={0}".Formatted(StoreId) %>'>资产导入</a></li>
                            <li style='<%= (CurrentStore.State == Models.StoreState.固产 ? "display: block;": "display: none;") %>'><a href='<%= "../StoreAction/Move?StoreId={0}".Formatted(StoreId) %>'>资产分库</a></li>
                        </ul>
                    </li>
                    <li class="dropdown">
                        <a class="dropdown-toggle navbar-link" data-toggle="dropdown">日常查询</a>
                        <ul class="dropdown-menu">
                            <li><a href='<%= "../StoreQuery/Target?StoreId={0}".Formatted(StoreId) %>'>购置单查询</a></li>
                            <li><a href='<%= "../StoreQuery/In?StoreId={0}".Formatted(StoreId) %>'>入库查询</a></li>
                            <li><a href='<%= "../StoreQuery/Use?StoreId={0}".Formatted(StoreId) %>'>出库单查询</a></li>
                            <li><a href='<%= "../StoreQuery/Used?StoreId={0}".Formatted(StoreId) %>'>出库查询</a></li>
                            <li style='<%= (CurrentStore.State != Models.StoreState.食品 ? "display: block;": "display: none;") %>'><a href='<%= "../StoreQuery/Return?StoreId={0}".Formatted(StoreId) %>'>归还查询</a></li>
                            <li style='<%= (CurrentStore.State == Models.StoreState.食品 ? "display: block;": "display: none;") %>'><a href='<%= "../StoreQuery/StatisticsMonthly?StoreId={0}".Formatted(StoreId) %>'>月库存查询</a></li>
                            <li style='<%= (CurrentStore.State == Models.StoreState.食品 ? "display: block;": "display: none;") %>'><a href='<%= "../StoreQuery/StatisticsDaily?StoreId={0}".Formatted(StoreId) %>'>汇总统计</a></li>
                            <li><a href='<%= "../StoreQuery/Statistics?StoreId={0}".Formatted(StoreId) %>'>库存统计</a></li>
                            <li style='<%= (CurrentStore.State == Models.StoreState.固产 ? "display: block;": "display: none;") %>'><a href='<%= "../StoreQuery/Import?StoreId={0}".Formatted(StoreId) %>'>导入查询</a></li>
                            <li style='<%= (CurrentStore.State == Models.StoreState.固产 ? "display: block;": "display: none;") %>'><a href='<%= "../StoreQuery/Move?StoreId={0}".Formatted(StoreId) %>'>分库查询</a></li>
                        </ul>
                    </li>
                    <li class="dropdown" style='<%= (CurrentStore.State != Models.StoreState.食品 ? "display: block;": "display: none;") %>'>
                        <a class="dropdown-toggle navbar-link" data-toggle="dropdown">物资条码</a>
                        <ul class="dropdown-menu">
                            <li><a href='<%= "../StoreScan/Code?StoreId={0}".Formatted(StoreId) %>'>条码打印</a></li>
                            <li><a href='<%= "../StoreScan/Use?StoreId={0}".Formatted(StoreId) %>'>扫码出库</a></li>
                            <li><a href='<%= "../StoreScan/Return?StoreId={0}".Formatted(StoreId) %>'>扫码归还</a></li>
                            <li><a href='<%= "../StoreScan/Flow?StoreId={0}".Formatted(StoreId) %>'>流通查询</a></li>
                        </ul>
                    </li>
                    <li class="dropdown">
                        <a class="dropdown-toggle navbar-link" data-toggle="dropdown">系统设置</a>
                        <ul class="dropdown-menu">
                            <li><a href='<%= "../StoreSetting/Catalog?StoreId={0}".Formatted(StoreId) %>'>物资类别</a></li>
                            <li><a href='<%= "../StoreSetting/Dictionary?StoreId={0}".Formatted(StoreId) %>'>基础数据</a></li>
                            <li style='<%= (RightAdvanced ? "display: block;": "display: none;") %>'><a href='<%= "../StoreSetting/Permission?StoreId={0}".Formatted(StoreId) %>'>权限设置</a></li>
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
                <input id="storeName" runat="server" type="button" class="btn btn-tumblr" onserverclick="storeName_ServerClick" />
                <span id="crumb" runat="server" class="btn btn-info" style="margin-left: 10px;"></span>
                <span class="btn btn-info" onclick="top.location.href = '../Store/Home';" style="float: right;">仓库切换</span>
                <span class="btn btn-info" onclick='<%= "top.location.href = \"../StoreAction/Object?StoreId={0}\"".Formatted(StoreId) %>' style="float: right; margin-right: 13px;">物资管理</span>
                <span class="btn btn-info" onclick='<%= "top.location.href = \"../StoreHome/Warn?StoreId={0}\"".Formatted(StoreId) %>' style="float: right; margin-right: 13px;">库存预警</span>
                <span class="btn btn-info" onclick='<%= "top.location.href = \"../StoreHome/Home?StoreId={0}\"".Formatted(StoreId) %>' style="float: right; margin-right: 13px;">快速导航</span>
                <span style="clear: both;"></span>
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
