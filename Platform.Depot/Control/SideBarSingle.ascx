<%@ Control Language="C#" AutoEventWireup="true" CodeFile="SideBarSingle.ascx.cs" Inherits="Control_SideBarSingle" %>

<telerik:RadScriptManager ID="sm" runat="server"></telerik:RadScriptManager>

<telerik:RadCodeBlock runat="server">
    <div class="container-fluid">
        <nav class="navbar navbar-info navbar-fixed-top">
            <div class="navbar-header rootPointer" onclick="top.location.href = '../Depot/Home';">
                <img alt="" src="../Common/配置/StoreLogo.png" style="margin: 5px; padding: 0; height: 40px; float: left;" /><span class="navbar-brand">物资管理云平台</span>
            </div>
            <div class="collapse navbar-collapse">
                <a id="qb" runat="server" class="btn btn-warning navbar-btn" style="float: right; margin-right: 20px;" onserverclick="qb_ServerClick">退出</a>
                <a id="u" runat="server" class="btn btn-warning navbar-btn" style="float: right; margin-right: 10px;" onclick="return false;"></a>
                <ul class="nav navbar-nav" style="float: right; margin-right: 80px;">
                    <li class="dropdown">
                        <a class="dropdown-toggle navbar-link" data-toggle="dropdown">物资管理</a>
                        <ul class="dropdown-menu">
                            <li><a href='<%= "../DepotAction/Order?DepotId={0}".Formatted(Depot.Id) %>'>购置登记</a></li>
                            <li><a href='<%= "../DepotAction/In?DepotId={0}".Formatted(Depot.Id) %>'>物资入库</a></li>
                            <li><a href='<%= "../DepotAction/Use?DepotId={0}".Formatted(Depot.Id) %>'>物资出库</a></li>
                            <li><a href='<%= "../DepotAction/Return?DepotId={0}".Formatted(Depot.Id) %>'>物资归还</a></li>
                            <li><a href='<%= "../DepotAction/Out?DepotId={0}".Formatted(Depot.Id) %>'>物资报废</a></li>
                            <li><a href='<%= "../DepotAction/Object?DepotId={0}".Formatted(Depot.Id) %>'>物资管理</a></li>
                            <li><a href='<%= "../DepotAction/Import?DepotId={0}".Formatted(Depot.Id) %>' style='<%= (Depot.Featured(Models.DepotType.固定资产库) ? "display: block;": "display: none;") %>'>资产导入</a></li>
                            <li><a href='<%= "../DepotAction/Move?DepotId={0}".Formatted(Depot.Id) %>' style='<%= (Depot.Featured(Models.DepotType.固定资产库) ? "display: block;": "display: none;") %>'>资产分库</a></li>
                        </ul>
                    </li>
                    <li class="dropdown">
                        <a class="dropdown-toggle navbar-link" data-toggle="dropdown">日常查询</a>
                        <ul class="dropdown-menu">
                            <li><a href='<%= "../DepotQuery/In?DepotId={0}".Formatted(Depot.Id) %>'>购置单查询</a></li>
                            <li><a href='<%= "../DepotQuery/InX?DepotId={0}".Formatted(Depot.Id) %>'>入库查询</a></li>
                            <li><a href='<%= "../DepotQuery/Use?DepotId={0}".Formatted(Depot.Id) %>'>出库单查询</a></li>
                            <li><a href='<%= "../DepotQuery/UseX?DepotId={0}".Formatted(Depot.Id) %>'>出库查询</a></li>
                            <li><a href='<%= "../DepotQuery/Return?DepotId={0}".Formatted(Depot.Id) %>'>归还查询</a></li>
                            <li><a href='<%= "../DepotQuery/Out?DepotId={0}".Formatted(Depot.Id) %>'>报废查询</a></li>
                            <li><a href='<%= "../DepotQuery/Statistics?DepotId={0}".Formatted(Depot.Id) %>'>库存查询</a></li>
                            <li><a href='<%= "../DepotQuery/Redo?DepotId={0}".Formatted(Depot.Id) %>'>退货查询</a></li>
                        </ul>
                    </li>
                    <li class="dropdown">
                        <a class="dropdown-toggle navbar-link" data-toggle="dropdown">物资条码</a>
                        <ul class="dropdown-menu">
                            <li><a href='<%= "../DepotScan/Code?DepotId={0}".Formatted(Depot.Id) %>'>条码打印</a></li>
                            <li><a href='<%= "../DepotScan/Use?DepotId={0}".Formatted(Depot.Id) %>'>扫码出库</a></li>
                            <li><a href='<%= "../DepotScan/Return?DepotId={0}".Formatted(Depot.Id) %>'>扫码归还</a></li>
                            <li><a href='<%= "../DepotScan/Out?DepotId={0}".Formatted(Depot.Id) %>'>扫码报废</a></li>
                            <li><a href='<%= "../DepotScan/Flow?DepotId={0}".Formatted(Depot.Id) %>'>流通查询</a></li>
                        </ul>
                    </li>
                    <li class="dropdown">
                        <a class="dropdown-toggle navbar-link" data-toggle="dropdown">系统设置</a>
                        <ul class="dropdown-menu">
                            <li><a href='<%= "../DepotSetting/Catalog?DepotId={0}".Formatted(Depot.Id) %>'>物资类别</a></li>
                            <li><a href='<%= "../DepotSetting/Dictionary?DepotId={0}".Formatted(Depot.Id) %>'>基础数据</a></li>
                            <li><a href='<%= "../DepotSetting/Permission?DepotId={0}".Formatted(Depot.Id) %>'>权限设置</a></li>
                            <li><a href='<%= "../DepotSetting/Period?DepotId={0}".Formatted(Depot.Id) %>'>借还时限</a></li>
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
                <span class="btn btn-info" onclick="top.location.href = '../Depot/Home';" style="float: right;">仓库切换</span>
                <span class="btn btn-info" onclick='<%= "top.location.href = \"../DepotAction/Object?DepotId={0}\"".Formatted(Depot.Id) %>' style="float: right; margin-right: 13px;">物资管理</span>
                <span class="btn btn-info" onclick='<%= "top.location.href = \"../Depot/DepotHome?DepotId={0}\"".Formatted(Depot.Id) %>' style="float: right; margin-right: 13px;">快速导航</span>
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
