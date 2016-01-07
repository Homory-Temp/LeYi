<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Home.aspx.cs" Inherits="Depot_Home" %>

<%@ Register Src="~/Control/SideBar.ascx" TagPrefix="homory" TagName="SideBar" %>

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
        <homory:SideBar runat="server" ID="SideBar" Crumb="物资管理" />
        <div class="container">
            <div class="row">
                <telerik:RadListView ID="view" runat="server" OnNeedDataSource="view_NeedDataSource">
                    <ItemTemplate>
                        <div class="col-md-4 col-xs-12 text-center" id="area" runat="server" style='<%# CanVisit((Guid)Eval("Id")) ? "display: block; cursor: pointer;": "display: none; cursor: pointer;" %>'>
                            <div class="row">&nbsp;</div>
                            <div class="row" onclick="top.location.href = '../Depot/DepotHome.aspx?DepotId=<%# Eval("Id") %>';">
                                <div class="col-md-12">
                                    <img src="../Content/Images/Store.png" />
                                </div>
                            </div>
                            <div class="row" onclick="top.location.href = '../Depot/DepotHome.aspx?DepotId=<%# Eval("Id") %>';">
                                <div class="col-md-12">
                                    <div class="btn btn-lg btn-info" style="width: 150px;" runat="server"><%# Eval("Name") %></div>
                                </div>
                            </div>
                        </div>
                    </ItemTemplate>
                </telerik:RadListView>
            </div>
            <div class="row">&nbsp;</div>
        </div>
    </form>
</body>
</html>
