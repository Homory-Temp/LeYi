<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ToCheck.aspx.cs" Inherits="ToCheck" %>

<%@ Register Src="~/Menu/MenuMobile.ascx" TagPrefix="homory" TagName="Menu" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <meta http-equiv="x-ua-compatible" content="ie=edge, chrome=1" />
    <meta name="viewport" content="width=device-width, initial-scale=1, minimum-scale=1, maximum-scale=1" />
    <title>物资管理 - 仓库盘库</title>
    <script src="../Assets/javascripts/jquery.js"></script>
<link href="../Assets/stylesheets/amazeui.min.css" rel="stylesheet">
<link href="../Assets/stylesheets/admin.css" rel="stylesheet">
<link href="../Assets/stylesheets/bootstrap.min.css" rel="stylesheet">
<link href="../Assets/stylesheets/bootstrap-theme.min.css" rel="stylesheet">

<script src="../Assets/javascripts/jquery.min.js"></script>
<script src="../Assets/javascripts/amazeui.min.js"></script>
<script src="../Assets/javascripts/app.js"></script>
    <!--[if lt IE 9]>
        <script src="../Assets/javascripts/html5.js"></script>
    <![endif]-->
    <!--[if (gt IE 8) | (IEMobile)]><!-->
    <link rel="stylesheet" href="../Assets/stylesheets/unsemantic-grid-responsive.css" />
    <!--<![endif]-->
    <!--[if (lt IE 9) & (!IEMobile)]>
        <link rel="stylesheet" href="../Assets/stylesheets/ie.css" />
    <![endif]-->
    <link href="../Assets/stylesheets/common.css" rel="stylesheet" />
    <script>
    </script>
</head>
<body>
    <form id="form" runat="server">
        <telerik:RadScriptManager ID="sm" runat="server"></telerik:RadScriptManager>
        <homory:Menu runat="server" ID="menu" />
        <telerik:RadAjaxPanel ID="ap" runat="server" >
            <div class="grid-100 mobile-grid-100 grid-parent center" style="margin-top: 60px;">
                <h4 style="margin-left: 20px;margin-top:10px;font-size:18px;color:#000000;">盘库任务</h4>
            </div>
            <div class="grid-100 mobile-grid-100">
                <table class="table table-bordered" style="margin-top: 10px;" align="center">
                    <thead>
                        <tr>
                            <th width="" style="text-align:center;">盘库名称</th>
                            <th width="85"  style="text-align:center;">时间</th>


                        </tr>
                    </thead>
                    <tbody>

                        <telerik:RadListView ID="pkList" runat="server" OnNeedDataSource="pkList_NeedDataSource">
                            <ItemTemplate>
                                <tr>
                                    <td align="left">
                                        <asp:LinkButton runat="server" ID="go" Text='<%# Eval("Name") %>' CommandArgument='<%# Eval("Id") %>' OnClick="go_Click" style="color:#000000;"></asp:LinkButton>
                                    </td>
                                    <td align="left">
                                        <%# ((int)Eval("TimeNode")).TimeNode() %>
                                    </td>
                                </tr>
                            </ItemTemplate>
                        </telerik:RadListView>
                    </tbody>
                </table>
            </div>
        </telerik:RadAjaxPanel>
    </form>
    <script src="../Assets/javascripts/jquery.js"></script>
    <script src="../Assets/javascripts/common.js"></script>
</body>
</html>
