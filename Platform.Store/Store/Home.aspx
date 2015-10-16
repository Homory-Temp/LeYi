<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Home.aspx.cs" Inherits="Store_Home" %>

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
            <div class="row" id="creating" runat="server">
                <div class="col-md-12">
                    <input type="button" class="btn btn-tumblr" id="add" runat="server" value="新增仓库" onserverclick="add_ServerClick" />
                    <hr style="color: #2B2B2B; margin-top: 4px;" />
                </div>
            </div>
            <div class="row">
                <telerik:RadListView ID="view" runat="server" OnNeedDataSource="view_NeedDataSource">
                    <ItemTemplate>
                        <div class="col-md-4 text-center" style="cursor: pointer;" id="area" runat="server" visible='<%# CanVisit((Guid)Eval("Id")) %>'>
                            <div class="row" onclick="top.location.href = '../StoreHome/Home?StoreId=<%# Eval("Id") %>';">
                                <div class="col-md-12">
                                    <img src="../Content/Images/Store.png" />
                                </div>
                            </div>
                            <div class="row" onclick="top.location.href = '../StoreHome/Home?StoreId=<%# Eval("Id") %>';">
                                <div class="col-md-12">
                                    <div class="btn btn-lg btn-info" style="width: 150px;"><%# Eval("Name") %></div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-12">
                                    <input type="button" class="btn btn-tumblr" id="edit" runat="server" value="编辑" visible='<%# Right_Create %>' onclick=<%# "top.location.href='../Store/HomeEdit?StoreId={0}'; return false;".Formatted(Eval("Id")) %> />
                                    <input type="button" class="btn btn-tumblr" id="remove" runat="server" value="删除" visible='<%# (Models.StoreState)Eval("State") > Models.StoreState.内置 && Right_Create %>' onclick=<%# "top.location.href='../Store/HomeRemove?StoreId={0}'; return false;".Formatted(Eval("Id")) %> />
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
