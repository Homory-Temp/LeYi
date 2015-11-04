<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CheckList.aspx.cs" Inherits="DepotAction_CheckList" %>

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
    <style>
        .depot {
            margin-left: 10px;
            float: left;
            text-decoration: none;
            font-weight: normal;
        }

        .depotx label {
            text-decoration: none;
            font-weight: normal;
        }
    </style>
</head>
<body>
    <form id="form" runat="server">
        <homory:SideBarSingle runat="server" ID="SideBarSingle" Crumb="物资条码 - 盘库任务列表" />
        <telerik:RadAjaxPanel ID="ap" runat="server" CssClass="container-fluid" LoadingPanelID="loading">
            <div class="row">
                <div class="col-md-12">
                    <telerik:RadListView ID="view" runat="server" OnNeedDataSource="view_NeedDataSource" ItemPlaceholderID="holder">
                        <LayoutTemplate>
                            <table class="storeTable">
                                <tr>
                                    <th>盘库任务</th>
                                    <th>盘库任务生成时间</th>
                                    <th>操作</th>
                                </tr>
                                <asp:PlaceHolder ID="holder" runat="server"></asp:PlaceHolder>
                            </table>
                        </LayoutTemplate>
                        <ItemTemplate>
                            <tr>
                                <td>
                                    <%# Eval("Name") %>
                                </td>
                                <td>
                                    <%#  ((DateTime)Eval("Time")).ToString("yyyy-MM-dd HH:mm:ss") %>
                                </td>
                                <td>
                                    <input type="button" class="btn btn-tumblr" value="盘库" id="start" runat="server" match='<%# Eval("BatchId") %>' onserverclick="start_ServerClick" />
                                    <input type="button" class="btn btn-tumblr" value="查看" id="view" runat="server" match='<%# Eval("BatchId") %>' onserverclick="view_ServerClick" />
                                    <input type="button" class="btn btn-tumblr" value="删除" id="del" runat="server" visible='<%# RightRoot %>' match='<%# Eval("BatchId") %>' onserverclick="del_ServerClick" />
                                </td>
                            </tr>
                        </ItemTemplate>
                    </telerik:RadListView>
                </div>
            </div>
        </telerik:RadAjaxPanel>
    </form>
</body>
</html>
