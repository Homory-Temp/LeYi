<%@ Page Language="C#" AutoEventWireup="true" CodeFile="关系.aspx.cs" Inherits="VIP_关系" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>关系</title>
    <script src="../Content/jQuery/jquery.min.js"></script>
    <style>
        body {
            font-family: "Segoe UI",Arial,Helvetica,sans-serif;
            font-size: 14px;
            text-align: center;
        }

        input {
            width: 120px;
            height: 60px;
            font-size: 18px;
        }
    </style>
</head>
<body>
    <script>
        o_items = [];
    </script>
    <form id="form" runat="server">
        <telerik:RadScriptManager ID="sm" runat="server" LoadScriptsBeforeUI="true" EnablePartialRendering="true">
            <Scripts>
                <asp:ScriptReference Assembly="Telerik.Web.UI" Name="Telerik.Web.UI.Common.Core.js" />
                <asp:ScriptReference Assembly="Telerik.Web.UI" Name="Telerik.Web.UI.Common.jQuery.js" />
                <asp:ScriptReference Assembly="Telerik.Web.UI" Name="Telerik.Web.UI.Common.jQueryPlugins.js" />
            </Scripts>
        </telerik:RadScriptManager>

        <div style="width: 100%; vertical-align: top;">
            <telerik:RadAjaxPanel ID="ap" runat="server">
                <div style="width: 33%; float: left;">
                    <telerik:RadListView ID="user" runat="server" OnNeedDataSource="user_NeedDataSource">
                        <ItemTemplate>
                            <div><%# Eval("name") %></div>
                        </ItemTemplate>
                    </telerik:RadListView>
                </div>
                <div style="width: 33%; float: left;">
                    <table style="width: 100%;">
                        <tr>
                            <td colspan="2">
                                <input id="pk" runat="server" type="button" value="主职" onserverclick="pk_ServerClick" />
                            </td>
                        </tr>
                        <tr><td colspan="2">&nbsp;</td></tr>
                        <tr>
                            <td>
                                <input id="pv" runat="server" type="button" value="+兼职+可访" onserverclick="pv_ServerClick" />
                            </td>
                            <td>
                                <input id="pv_r" runat="server" type="button" value="-兼职-可访" onserverclick="pv_r_ServerClick" />
                            </td>
                        </tr>
                        <tr><td colspan="2">&nbsp;</td></tr>
                        <tr>
                            <td>
                                <input id="p" runat="server" type="button" value="+兼职" onserverclick="p_ServerClick" />
                            </td>
                            <td>
                                <input id="p_r" runat="server" type="button" value="-兼职" onserverclick="p_r_ServerClick" />
                            </td>
                        </tr>
                        <tr><td colspan="2">&nbsp;</td></tr>
                        <tr>
                            <td>
                                <input id="v" runat="server" type="button" value="+可访" onserverclick="v_ServerClick" />
                            </td>
                            <td>
                                <input id="v_r" runat="server" type="button" value="-可访" onserverclick="v_r_ServerClick" />
                            </td>
                        </tr>
                    </table>
                </div>
                <div style="width: 33%; float: left;">
                    <telerik:RadListView ID="org" runat="server" OnNeedDataSource="org_NeedDataSource">
                        <ItemTemplate>
                            <div><%# Eval("name") %></div>
                        </ItemTemplate>
                    </telerik:RadListView>
                </div>
            </telerik:RadAjaxPanel>
        </div>
    </form>
</body>
</html>
