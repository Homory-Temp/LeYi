<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Home.aspx.cs" Inherits="StoreHome_Home" %>

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
        <homory:SideBarSingle runat="server" ID="SideBarSingle" Crumb="快速导航" NoCrumb="true" />
        <div class="container-fluid">
            <div class="row text-center">
                <telerik:RadListView ID="view" runat="server" OnNeedDataSource="view_NeedDataSource">
                    <ItemTemplate>
                        <div class="col-md-3 text-center" style="cursor: pointer; margin-top: 50px;">
                            <div class="row" style="display: none;" onclick="top.location.href = '<%# Eval("Url") %>';">
                                <div class="col-md-12">
                                    <img src="../Content/Images/Store.png" />
                                </div>
                            </div>
                            <div class="row" onclick="top.location.href = '<%# Eval("Url") %>';">
                                <div class="col-md-12">
                                    <div class='btn btn-lg btn-rounded-lg <%# Eval("Class") %>'><%# Eval("Name") %></div>
                                </div>
                            </div>
                        </div>
                    </ItemTemplate>
                </telerik:RadListView>
            </div>
        </div>
    </form>
</body>
</html>
