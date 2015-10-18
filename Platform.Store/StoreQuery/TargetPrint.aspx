<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TargetPrint.aspx.cs" Inherits="StoreQuery_TargetPrint" %>

<%@ Register Src="~/Control/SideBarSingle.ascx" TagPrefix="homory" TagName="SideBarSingle" %>
<%@ Register Src="~/Control/TargetHeader.ascx" TagPrefix="homory" TagName="TargetHeader" %>
<%@ Register Src="~/Control/TargetBody.ascx" TagPrefix="homory" TagName="TargetBody" %>
<%@ Register Src="~/Control/ObjectInHeader.ascx" TagPrefix="homory" TagName="ObjectInHeader" %>
<%@ Register Src="~/Control/ObjectInBody.ascx" TagPrefix="homory" TagName="ObjectInBody" %>
<%@ Register Src="~/Control/RecordInHeader.ascx" TagPrefix="homory" TagName="RecordInHeader" %>
<%@ Register Src="~/Control/RecordInBody.ascx" TagPrefix="homory" TagName="RecordInBody" %>

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
        <telerik:RadScriptManager ID="sm" runat="server"></telerik:RadScriptManager>
        <telerik:RadAjaxLoadingPanel ID="loading" runat="server" InitialDelayTime="1000">
            <div>&nbsp;</div>
            <div class="btn btn-lg btn-warning" style="margin-top: 50px;">正在加载 请稍候....</div>
        </telerik:RadAjaxLoadingPanel>
        <telerik:RadAjaxPanel ID="ap" runat="server" CssClass="container-fluid" LoadingPanelID="loading">
            <div class="row" id="x4" runat="server">
                <div class="col-md-12">
                    <telerik:RadListView ID="view_record" runat="server" OnNeedDataSource="view_record_NeedDataSource" ItemPlaceholderID="recordHolder">
                        <LayoutTemplate>
                            <table class="storeTable text-center">
                                <tr>
                                    <homory:RecordInHeader runat="server" ID="ObjectInHeader" />
                                </tr>
                                <asp:PlaceHolder ID="recordHolder" runat="server"></asp:PlaceHolder>
                            </table>
                        </LayoutTemplate>
                        <ItemTemplate>
                            <tr>
                                <homory:RecordInBody runat="server" ID="ObjectInBody" ItemIndex='<%# Container.DataItemIndex %>' />
                            </tr>
                        </ItemTemplate>
                    </telerik:RadListView>
                </div>
            </div>
        </telerik:RadAjaxPanel>
    </form>
</body>
</html>
