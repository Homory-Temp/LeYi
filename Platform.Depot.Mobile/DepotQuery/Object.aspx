<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Object.aspx.cs" Inherits="DepotQuery_Object" %>

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
    <style>
        .objInfo {
            color: black;
        }
    </style>
    <script>
        function showPic(obj) {
            var id = $(obj).attr("src");
            if (id)
                window.open(id, '_blank');
        }
    </script>
</head>
<body>
    <form id="form" runat="server">
        <telerik:RadScriptManager ID="sm" runat="server"></telerik:RadScriptManager>
        <telerik:RadAjaxPanel ID="ap" runat="server" CssClass="container" LoadingPanelID="loading">
            <div class="row">
                <div class="col-md-3 col-xs-12">
                    <span class="btn btn-tumblr">物资详情</span>
                </div>
                <div class="col-md-6 col-xs-12">
                    <span class="btn btn-danger" id="name" runat="server"></span>
                </div>
                <div class="col-md-3 col-xs-12 text-right">
                    <input type="button" class="btn btn-info" id="back" runat="server" value="返回" onserverclick="back_ServerClick" />
                </div>
            </div>
            <div class="row">
                <div class="col-md-12 text-center">
                    <hr style="color: #2B2B2B; margin-top: 4px;" />
                </div>
            </div>
            <div class="row">
                <div class="col-md-12">
                    <table class="storeTable">
                        <tr>
                            <td style="width: 65px;">
                                <span>品牌：</span>
                            </td>
                            <td>
                                <span id="brand" runat="server"></span>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 65px;">
                                <span>规格：</span>
                            </td>
                            <td>
                                <span id="sp" runat="server"></span>
                            </td>
                        </tr>
                        <tr id="xRow" runat="server">
                            <td style="width: 65px;">
                                <span>年龄段：</span>
                            </td>
                            <td colspan="3">
                                <span id="age" runat="server"></span>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 65px;">
                                <span>备注：</span>
                            </td>
                            <td>
                                <span id="note" runat="server"></span>
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
            <div class="row">&nbsp;</div>
            <div class="row">
                <div class="col-md-12 text-center">
                    <input type="button" class="btn btn-info" id="do_up" runat="server" value="上传图片" onserverclick="do_up_ServerClick" />
                </div>
            </div>
            <div class="row">
                <div class="col-md-6" id="da" runat="server">
                    <img class="img-responsive" onclick="showPic(this);" style="width: 100%; cursor: pointer;" id="pa" runat="server" />
                </div>
                <div class="col-md-6" id="db" runat="server">
                    <img class="img-responsive" onclick="showPic(this);" style="width: 100%; cursor: pointer;" id="pb" runat="server" />
                </div>
                <div class="col-md-6" id="dc" runat="server">
                    <img class="img-responsive" onclick="showPic(this);" style="width: 100%; cursor: pointer;" id="pc" runat="server" />
                </div>
                <div class="col-md-6" id="dd" runat="server">
                    <img class="img-responsive" onclick="showPic(this);" style="width: 100%; cursor: pointer;" id="pd" runat="server" />
                </div>
            </div>
            <div class="row"><span class="btn btn-info dictionaryX">存放地</span></div>
            <div class="row">
                <telerik:RadListView ID="view" runat="server" CssClass="col-md-12" OnNeedDataSource="view_NeedDataSource" ItemPlaceholderID="holder">
                    <LayoutTemplate>
                        <table>
                            <asp:PlaceHolder ID="holder" runat="server"></asp:PlaceHolder>
                        </table>
                    </LayoutTemplate>
                    <ItemTemplate>
                        <tr>
                            <td>编号：<%# Eval("Ordinal") %></td>
                            <td>条码：<%# Eval("Code") %></td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <asp:TextBox ID="place" runat="server" Text='<%# Eval("Place") %>'></asp:TextBox>
                            </td>
                        </tr>
                    </ItemTemplate>
                </telerik:RadListView>
            </div>
            <div class="row">&nbsp;</div>
            <div class="row">&nbsp;</div>
        </telerik:RadAjaxPanel>
    </form>
</body>
</html>
