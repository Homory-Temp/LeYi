<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DepotHome.aspx.cs" Inherits="Depot_DepotHome" %>

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
                &nbsp;
            </div>
            <div class="row">
                <div class="col-md-12">
                    <div class="panel panel-info">
                        <div class="panel-heading panel">
                            <div class="panel-title text-center">
                                快捷菜单
                            </div>
                        </div>
                        <div class="panel-body">
                            <a class="btn btn-info dictionaryX" href='<%= "../DepotScan/Object?DepotId={0}".Formatted(Depot.Id) %>'>物资查询</a>
                            <a class="btn btn-info dictionaryX" href='<%= "../DepotScan/Flow?DepotId={0}".Formatted(Depot.Id) %>'>流通查询</a>
                            <a class="btn btn-info dictionaryX" href='<%= "../DepotScan/Use?DepotId={0}".Formatted(Depot.Id) %>'>扫码出库</a>
                            <a class="btn btn-info dictionaryX" href='<%= "../DepotScan/Return?DepotId={0}".Formatted(Depot.Id) %>'>扫码归还</a>
                            <a class="btn btn-info dictionaryX" href='<%= "../DepotScan/Out?DepotId={0}".Formatted(Depot.Id) %>'>扫码报废</a>
                            <a class="btn btn-info dictionaryX" href='<%= "../DepotScan/CheckList?DepotId={0}".Formatted(Depot.Id) %>'>扫码盘库</a>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
