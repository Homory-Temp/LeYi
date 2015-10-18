<%@ Page Language="C#" AutoEventWireup="true" CodeFile="In.aspx.cs" Inherits="StoreAction_In" %>

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
        <homory:SideBarSingle runat="server" ID="SideBarSingle" Crumb="物资管理 - 物资入库" />
        <telerik:RadAjaxPanel ID="ap" runat="server" CssClass="container-fluid" LoadingPanelID="loading">
            <div class="row">
                <div class="col-md-2">
                    <div class="btn btn-tumblr dictionaryX">
                        购置单选择
                    </div>
                </div>
                <div class="col-md-10 text-left">
                    <telerik:RadMonthYearPicker ID="period" runat="server" LocalizationPath="~/Language" ShowPopupOnFocus="true" Width="100" AutoPostBack="true" OnSelectedDateChanged="period_SelectedDateChanged">
                        <DatePopupButton runat="server" Visible="false" />
                    </telerik:RadMonthYearPicker>
                    &nbsp;&nbsp;&nbsp;&nbsp;
                    <telerik:RadComboBox ID="source" runat="server" AutoPostBack="true" Width="120" AppendDataBoundItems="true" OnSelectedIndexChanged="source_SelectedIndexChanged">
                    </telerik:RadComboBox>
                    &nbsp;&nbsp;&nbsp;&nbsp;
                    <telerik:RadComboBox ID="usage" runat="server" AutoPostBack="true" Width="120" AppendDataBoundItems="true" DataTextField="Name" DataValueField="Id" OnSelectedIndexChanged="usage_SelectedIndexChanged">
                    </telerik:RadComboBox>
                    &nbsp;&nbsp;&nbsp;&nbsp;
                    <telerik:RadComboBox ID="people" runat="server" AutoPostBack="true" Width="120" DataTextField="RealName" DataValueField="Id" AppendDataBoundItems="true" OnSelectedIndexChanged="people_SelectedIndexChanged">
                    </telerik:RadComboBox>
                    &nbsp;&nbsp;&nbsp;&nbsp;
                    <telerik:RadComboBox ID="target" runat="server" AutoPostBack="true" LocalizationPath="~/Language" DataTextField="购置单号" DataValueField="Id" Filter="Contains" MarkFirstMatch="true" ShowToggleImage="true" Width="400" AllowCustomText="true" OnSelectedIndexChanged="target_SelectedIndexChanged"></telerik:RadComboBox>
                </div>
            </div>
            <div class="row">
                <telerik:RadListView ID="view_target" runat="server" OnNeedDataSource="view_target_NeedDataSource" ItemPlaceholderID="holder">
                    <LayoutTemplate>
                        <div class="col-md-12">
                            <table class="storeTable text-center">
                                <tr>
                                    <homory:TargetHeader runat="server" ID="TargetHeader" />
                                </tr>
                                <asp:PlaceHolder ID="holder" runat="server"></asp:PlaceHolder>
                            </table>
                        </div>
                    </LayoutTemplate>
                    <ItemTemplate>
                        <tr>
                            <homory:TargetBody runat="server" ID="TargetBody" />
                        </tr>
                    </ItemTemplate>
                    <EmptyDataTemplate>
                    </EmptyDataTemplate>
                </telerik:RadListView>
            </div>
            <div class="row">&nbsp;</div>
            <div class="row" id="x1" runat="server">
                <div class="col-md-2">
                    <div class="btn btn-tumblr dictionaryX">
                        入库物资选择
                    </div>
                </div>
            </div>
            <div class="row" id="x2" runat="server">
                <div class="col-md-12">
                    <telerik:RadListView ID="view_obj" runat="server" OnNeedDataSource="view_obj_NeedDataSource" ItemPlaceholderID="inHolder" OnItemDataBound="view_obj_ItemDataBound">
                        <LayoutTemplate>
                            <table class="storeTable text-center">
                                <homory:ObjectInHeader runat="server" ID="ObjectInHeader" />
                                <asp:PlaceHolder ID="inHolder" runat="server"></asp:PlaceHolder>
                            </table>
                        </LayoutTemplate>
                        <ItemTemplate>
                            <homory:ObjectInBody runat="server" ID="ObjectInBody" ItemIndex='<%# Container.DataItemIndex %>' TargetId='<%# target.SelectedValue.GlobalId() %>' />
                        </ItemTemplate>
                    </telerik:RadListView>
                </div>
                <div class="col-md-12">
                    <input type="button" class="btn btn-tumblr" id="plus" runat="server" value="+" onserverclick="plus_ServerClick" />
                    <input type="hidden" id="counter" runat="server" value="0" />
                    <input type="hidden" id="x" runat="server" value="" />
                </div>
                <div class="col-md-12 text-center">
                    <input type="button" class="btn btn-tumblr" id="do_in" runat="server" value="入库" onserverclick="do_in_ServerClick" />
                    &nbsp;&nbsp;
                    <input type="button" class="btn btn-tumblr" id="Button2" runat="server" value="入库并办结购置单" onserverclick="done_in_ServerClick" />
                </div>
            </div>
            <div class="row">&nbsp;</div>
            <div class="row" id="x3" runat="server">
                <div class="col-md-2">
                    <div class="btn btn-tumblr dictionaryX">
                        本购置单入库记录
                    </div>
                </div>
            </div>
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
                        <EmptyDataTemplate>
                            <div class="row"><div class="col-md-12 text-center" style="color: black;">暂无记录</div></div>
                        </EmptyDataTemplate>
                    </telerik:RadListView>
                </div>
            </div>
        </telerik:RadAjaxPanel>
    </form>
</body>
</html>
