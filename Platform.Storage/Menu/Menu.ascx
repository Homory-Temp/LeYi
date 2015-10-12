<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Menu.ascx.cs" Inherits="Menu" %>
<script src="../Assets/javascripts/jquery.js"></script>
<link href="../Assets/stylesheets/amazeui.min.css" rel="stylesheet">
<link href="../Assets/stylesheets/admin.css" rel="stylesheet">
<link href="../Assets/stylesheets/bootstrap.min.css" rel="stylesheet">
<link href="../Assets/stylesheets/bootstrap-theme.min.css" rel="stylesheet">

<script src="../Assets/javascripts/jquery.min.js"></script>
<script src="../Assets/javascripts/amazeui.min.js"></script>
<script src="../Assets/javascripts/app.js"></script>
<style>
    .menu_parent li {
        float: left;
        width: 100px;
        text-align: left;
    }

    .menu_child li {
        float: none;
        margin-left: -40px;
        text-align: left;
    }
</style>
<header class="am-topbar admin-header">

    <div class="am-collapse am-topbar-collapse" id="topbar-collapse">
        <table width=98%">
            <tr>
                <td align="left">
                    <style>
                        #sa a:hover {
                            text-decoration: none;
                            color: white;
                        }
                    </style>
                    <div class="am-topbar-brand" style="width: 210px;" id="sa">
                        <a href="../Storage/Storage.aspx">
                            <img src="../StorageCode/Icon.png" /><strong>云物资管理平台</strong> </a>
                    </div>
                </td>
                <td align="center" width="150"></td>
                <td align="left" width="80%">
                    <nav>
                        <telerik:RadCodeBlock runat="server">

                            <ul class="nav-main">
                                当前仓库：<label runat="server" id="menu_storage"></label>
                                  <li> <a id="off" runat="server" onserverclick="off_ServerClick">退出</a></li>
                                <li>您好：<label runat="server" id="menu_user"></label></li>
                              
                                <li id="li-3">系统设置</li>
                                <li id="li-2">查询打印</li>
                                <li id="li-1">物资管理</li>
                                <li><a href='<%= "../StorageHome/Home?StorageId={0}".Formatted(StorageId) %>'>仓库首页</a></li>


                            </ul>

                            <div id="box-1" class="hidden-box hidden-loc-index1">
                                <ul>
                                    <li id="wzbf" runat="server"><a href='<%= "../StorageScan/ScanOut?StorageId={0}".Formatted(StorageId) %>'>物资报废</a></li>
                                    <li><a href='<%= "../StorageScan/ScanReturn?StorageId={0}".Formatted(StorageId) %>'>物资归还</a></li>
                                    <li><a href='<%= "../StorageScan/ScanUse?StorageId={0}".Formatted(StorageId) %>'>领用借用</a></li>
                                    <li><a href='<%= "../StorageTarget/TargetAdd?StorageId={0}".Formatted(StorageId) %>'>购置登记</a></li>
                                    <li><a href='<%= "../StorageObject/Object?StorageId={0}".Formatted(StorageId) %>'>物资管理</a></li>
                                    <li><a href='<%= "../StorageObject/Import?StorageId={0}".Formatted(StorageId) %>'>数据导入</a></li>
                                </ul>
                            </div>
                            <div id="box-2" class="hidden-box hidden-loc-index2">
                                <ul>
                                    <li><a href='<%= "../StorageCode/Code?StorageId={0}".Formatted(StorageId) %>'>物资条码</a></li>
                                    <li><a href='<%= "../StorageQuery/QueryTarget?StorageId={0}".Formatted(StorageId) %>'>购置查询</a></li>
                                    <li><a href='<%= "../StorageQuery/QueryIn?StorageId={0}".Formatted(StorageId) %>'>入库查询</a></li>
                                    <li><a href='<%= "../StorageQuery/QueryConsume?StorageId={0}".Formatted(StorageId) %>'>领用查询</a></li>
                                    <li><a href='<%= "../StorageQuery/QueryLend?StorageId={0}".Formatted(StorageId) %>'>借用查询</a></li>
                                    <li><a href='<%= "../StorageQuery/QueryReturn?StorageId={0}".Formatted(StorageId) %>'>归还查询</a></li>
                                    <li><a href='<%= "../StorageQuery/QueryOut?StorageId={0}".Formatted(StorageId) %>'>报废查询</a></li>
                                    <li><a href='<%= "../StorageOut/OutDone?StorageId={0}".Formatted(StorageId) %>'>报审查询</a></li>
                                    <li><a href='<%= "../StorageQuery/QueryFlow?StorageId={0}".Formatted(StorageId) %>'>库存查询</a></li>
                                    <li><a href='<%= "../StorageCheck/Check?StorageId={0}".Formatted(StorageId) %>'>物资盘库</a></li>
                                    <li><a href='<%= "../StorageQuery/QueryCheck?StorageId={0}".Formatted(StorageId) %>'>盘库查询</a></li>
                                    <%--<li><a href='<%= "../StorageScan/ScanQuery?StorageId={0}".Formatted(StorageId) %>'>流通查询</a></li>--%>
                                </ul>
                            </div>

                            <div id="box-3" class="hidden-box hidden-loc-index3">
                                <ul>
                                    <li><a href='<%= "../StorageSetting/Catalog?StorageId={0}".Formatted(StorageId) %>'>分类管理</a></li>
                                    <li><a href='<%= "../StorageSetting/Dictionary?StorageId={0}".Formatted(StorageId) %>'>数据字典</a></li>
                                    <li><a href='<%= "../StorageSetting/RolePermission?StorageId={0}".Formatted(StorageId) %>'>角色权限</a></li>
                                    <li><a href='<%= "../StorageSetting/UserRole?StorageId={0}".Formatted(StorageId) %>'>用户角色</a></li>
                                </ul>
                            </div>




                        </telerik:RadCodeBlock>
                    </nav>
                </td>
            </tr>
        </table>
    </div>
</header>

<script src="../Assets/javascripts/main.js"></script>


