<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Check.aspx.cs" Inherits="Check" %>

<%@ Register Src="~/Menu/Menu.ascx" TagPrefix="homory" TagName="Menu" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <meta http-equiv="x-ua-compatible" content="ie=edge, chrome=1" />
    <meta name="viewport" content="width=device-width, initial-scale=1, minimum-scale=1, maximum-scale=1" />
    <title>物资管理 - 仓库盘库</title>
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
        <telerik:RadAjaxPanel ID="ap" runat="server" CssClass="grid-container">
            <div class="grid-100 mobile-grid-100 grid-parent">
                物资名称：<telerik:RadTextBox ID="name" runat="server"></telerik:RadTextBox>
                存放地：<telerik:RadTextBox ID="place" runat="server"></telerik:RadTextBox>
                责任人：<telerik:RadTextBox ID="people" runat="server"></telerik:RadTextBox>
                <asp:ImageButton AlternateText="查询" ID="query" runat="server" OnClick="query_Click" />
            </div>
           
            <div class="grid-100 mobile-grid-100 grid-parent" style="margin-top:10px;margin-bottom:10px;">
                <table class="table table-bordered">
                    <tr>
                        <td width="70">选定</td>
                        <td width="250">物资</td>
                        <td width="100">入库存放地</td>
                        <td width="70">责任人</td>
                        <td width="70">现有数量</td>
                        <td width="200">固定资产编号</td>
                        <td width="300">现存放地/编号列表</td>

                    </tr>
                    <telerik:RadListView ID="view" runat="server" OnNeedDataSource="view_NeedDataSource" DataKeyNames="Id" OnItemDataBound="view_ItemDataBound">
                        <ItemTemplate>
                            <tr>
                                <td>
                                    <telerik:RadButton ID="s" runat="server" ButtonType="ToggleButton" ToggleType="CheckBox" Text="选定"></telerik:RadButton>
                                </td>
                                <td>
                                    <%# (Eval("Obj") as Models.StorageObject).Name %>（<%# (Eval("Obj") as Models.StorageObject).GeneratePath() %>）
                                </td>
                                <td><%# (Eval("In") as Models.StorageIn).Place %>
                                </td>
                                <td><%# Eval("People") %>
                                </td>
                                <td><%# Eval("Amount") %>
                                </td>
                                <td><%# (Eval("Obj") as Models.StorageObject).FixedSerial %> </td>

                             <td align="left">
                                    <asp:Repeater ID="rp" runat="server">
                                        <ItemTemplate>

                                           <%# Eval("Place") %>-<%# Agg(Eval("Ordinals") as List<int>) %></br>
                                        </ItemTemplate>
                                    </asp:Repeater>
                             </td>
                            </tr>

                        </ItemTemplate>
                    </telerik:RadListView>
                </table>
                 <div class="grid-100 mobile-grid-100" style="margin-top:10px;margin-bottom:10px;display:none ;">
                <telerik:RadButton ID="sa" runat="server" ButtonType="ToggleButton" ToggleType="CheckBox" AutoPostBack="true" Text="全部选定" OnCheckedChanged="sa_CheckedChanged"></telerik:RadButton>
            </div>
                <div class="grid-100 mobile-grid-100">
                    盘库名称：<telerik:RadTextBox ID="name_ex" runat="server"></telerik:RadTextBox>
                    <asp:ImageButton AlternateText="新建盘库" ID="start" runat="server" OnClick="start_Click" />
                </div>


                
        </telerik:RadAjaxPanel>
    </form>
    <script src="../Assets/javascripts/jquery.js"></script>
    <script src="../Assets/javascripts/common.js"></script>
</body>
</html>
