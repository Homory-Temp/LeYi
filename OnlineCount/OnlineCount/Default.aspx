<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>在线统计</title>
    <script>
        function st() {
            var d = document.getElementById('departments').options[document.getElementById('departments').selectedIndex].value;
            var y = document.getElementById('years').options[document.getElementById('years').selectedIndex].value;
            var m = document.getElementById('months').options[document.getElementById('months').selectedIndex].value;
            window.open('Online.aspx?D=' + d + '&Y=' + y + '&M=' + m, '_blank');
            return false;
        }
    </script>
</head>
<body>
    <form id="form" runat="server">
        <table>
            <tr>
                <td>
                    <asp:Label runat="server" Text="部门："></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="departments" runat="server" AutoPostBack="false">
                        <asp:ListItem Text="局工作部门" Value="4006" Selected="True"></asp:ListItem>
                        <asp:ListItem Text="公文账号" Value="4007"></asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td>
                    <asp:Label runat="server" Text="年份："></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="years" runat="server" AutoPostBack="false">
                    </asp:DropDownList>
                </td>
                <td>
                    <asp:Label runat="server" Text="月份："></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="months" runat="server" AutoPostBack="false">
                    </asp:DropDownList>
                </td>
                <td>
                    <asp:Button ID="ok" runat="server" OnClientClick="return st();" Text="统计" />
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
