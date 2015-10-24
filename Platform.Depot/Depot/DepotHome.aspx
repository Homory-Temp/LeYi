﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DepotHome.aspx.cs" Inherits="Depot_DepotHome" %>

<%@ Register Src="~/Control/SideBarSingle.ascx" TagPrefix="homory" TagName="SideBarSingle" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=Edge,Chrome=1" />
    <meta name="viewport" content="width=device-width,initial-scale=1,minimum-scale=1,maximum-scale=1" />
    <script src="../Content/jQuery/jquery.min.js"></script>
    <link href="../assets/css/bootstrap.min.css" rel="stylesheet" />
    <link href="../assets/css/style-responsive.css" rel="stylesheet" />
    <link href="../assets/css/style.css" rel="stylesheet" />
    <link href="../Content/Core/css/common.css" rel="stylesheet" />
    <link href="../Content/Core/css/fix.css" rel="stylesheet" />
    <script src="../assets/js/bootstrap.min.js"></script>
    <script src="../Content/Homory/js/common.js"></script>
    <script src="../Content/Homory/js/notify.min.js"></script>
    <!--[if lt IE 9]>
	    <script src="../Content/Homory/js/html5shiv.js"></script>
	    <script src="../Content/Homory/js/respond.min.js"></script>
    <![endif]-->
</head>
<body>
    <form id="form" runat="server">
        <homory:SideBarSingle runat="server" ID="SideBarSingle" NoCrumb="true" />
        <div class="container">
            <div class="row">
                <div class="col-md-6">
                    <div class="panel panel-info">
                        <div class="panel-heading panel">
                            <div class="panel-title">
                                物资管理
                            </div>
                        </div>
                        <div class="panel-body">
                            <a class="btn btn-info dictionaryX" href='<%= "../DepotAction/Target?DepotId={0}".Formatted(Depot.Id) %>'>购置登记</a>
                            <a class="btn btn-info dictionaryX" href='<%= "../DepotAction/In?DepotId={0}".Formatted(Depot.Id) %>'>物资入库</a>
                            <a class="btn btn-info dictionaryX" href='<%= "../DepotAction/Use?DepotId={0}".Formatted(Depot.Id) %>'>物资出库</a>
                            <a class="btn btn-info dictionaryX" href='<%= "../DepotAction/Return?DepotId={0}".Formatted(Depot.Id) %>'>物资归还</a>
                            <a class="btn btn-info dictionaryX" href='<%= "../DepotAction/Object?DepotId={0}".Formatted(Depot.Id) %>'>物资管理</a>
                            <a class="btn btn-info dictionaryX" href='<%= "../DepotAction/Import?DepotId={0}".Formatted(Depot.Id) %>'>资产导入</a>
                            <a class="btn btn-info dictionaryX" href='<%= "../DepotAction/Move?DepotId={0}".Formatted(Depot.Id) %>'>资产分库</a>
                        </div>
                    </div>
                </div>
                <div class="col-md-6">
                    <div class="panel panel-info">
                        <div class="panel-heading panel">
                            <div class="panel-title">
                                日常查询
                            </div>
                        </div>
                        <div class="panel-body">
                            <a class="btn btn-info dictionaryX" href='<%= "../DepotQuery/In?DepotId={0}".Formatted(Depot.Id) %>'>购置单查询</a>
                            <a class="btn btn-info dictionaryX" href='<%= "../DepotQuery/InX?DepotId={0}".Formatted(Depot.Id) %>'>入库查询</a>
                            <a class="btn btn-info dictionaryX" href='<%= "../DepotQuery/Use?DepotId={0}".Formatted(Depot.Id) %>'>出库单查询</a>
                            <a class="btn btn-info dictionaryX" href='<%= "../DepotQuery/UseX?DepotId={0}".Formatted(Depot.Id) %>'>出库查询</a>
                            <a class="btn btn-info dictionaryX" href='<%= "../DepotQuery/Return?DepotId={0}".Formatted(Depot.Id) %>'>归还查询</a>
                            <a class="btn btn-info dictionaryX" href='<%= "../DepotQuery/Statistics?DepotId={0}".Formatted(Depot.Id) %>'>库存查询</a>
                            <a class="btn btn-info dictionaryX" href='<%= "../DepotQuery/Import?DepotId={0}".Formatted(Depot.Id) %>'>导入查询</a>
                            <a class="btn btn-info dictionaryX" href='<%= "../DepotQuery/Move?DepotId={0}".Formatted(Depot.Id) %>'>分库查询</a>
                        </div>
                    </div>
                </div>
            </div>
            <div class="row">
                &nbsp;
            </div>
            <div class="row">
                <div class="col-md-6">
                    <div class="panel panel-info">
                        <div class="panel-heading panel">
                            <div class="panel-title">
                                物资条码
                            </div>
                        </div>
                        <div class="panel-body">
                            <a class="btn btn-info dictionaryX" href='<%= "../DepotScan/Code?DepotId={0}".Formatted(Depot.Id) %>'>条码打印</a>
                            <a class="btn btn-info dictionaryX" href='<%= "../DepotScan/Use?DepotId={0}".Formatted(Depot.Id) %>'>扫码出库</a>
                            <a class="btn btn-info dictionaryX" href='<%= "../DepotScan/Return?DepotId={0}".Formatted(Depot.Id) %>'>扫码归还</a>
                            <a class="btn btn-info dictionaryX" href='<%= "../DepotScan/Flow?DepotId={0}".Formatted(Depot.Id) %>'>流通查询</a>
                        </div>
                    </div>
                </div>
                <div class="col-md-6">
                    <div class="panel panel-info">
                        <div class="panel-heading panel">
                            <div class="panel-title">
                                系统设置
                            </div>
                        </div>
                        <div class="panel-body">
                            <a class="btn btn-info dictionaryX" href='<%= "../DepotSetting/Catalog?DepotId={0}".Formatted(Depot.Id) %>'>物资类别</a>
                            <a class="btn btn-info dictionaryX" href='<%= "../DepotSetting/Dictionary?DepotId={0}".Formatted(Depot.Id) %>'>基础数据</a>
                            <a class="btn btn-info dictionaryX" href='<%= "../DepotSetting/Permission?DepotId={0}".Formatted(Depot.Id) %>'>权限设置</a>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
