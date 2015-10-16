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
                            <li><a href="../StoreTarget/TargetAdd">购置登记</a></li>
                            <li style='<%= (CurrentStore.State == Models.StoreState.食品 ? "display: block;": "display: none;") %>'><a href='<%= "../StoreIn/InMultiple?StoreId={0}".Formatted(StoreId) %>'>快捷入库</a></li>
                            <li><a href="../StoreScan/Use">领用借用</a></li>
                            <li><a href="../StoreScan/Return">物资归还</a></li>
                            <li style='<%= (CurrentStore.State == Models.StoreState.固产 ? "display: block;": "display: none;") %>'><a href="../StoreImport/Go">数据导入</a></li>
                            <li><a href="../StoreObject/Object">物资管理</a></li>
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
