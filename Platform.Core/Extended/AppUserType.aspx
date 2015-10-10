<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AppUserType.aspx.cs" Inherits="Extended.ExtendedAppUserType" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=Edge,Chrome=1" />
    <meta name="viewport" content="width=device-width,initial-scale=1,minimum-scale=1,maximum-scale=1" />
    <title>选择应用可访问对象</title>
	<%-- ReSharper disable Html.PathError --%>
    <link href="../Content/Semantic/css/semantic.min.css" rel="stylesheet" />
    <link href="../Content/Homory/css/common.css" rel="stylesheet" />
    <link href="../Content/Core/css/home.css" rel="stylesheet" />
    <link href="../Content/Core/css/common.css" rel="stylesheet" />
    <script src="../Content/jQuery/jquery.min.js"></script>
    <script src="../Content/Semantic/javascript/semantic.min.js"></script>
    <script src="../Content/Homory/js/common.js"></script>
    <script src="../Content/Homory/js/notify.min.js"></script>
    <script src="../Content/Core/js/home.js"></script>
	<%-- ReSharper restore Html.PathError --%>
</head>
<body style="margin: 0; padding: 0; overflow: hidden; text-align: center">
    <form id="formHome" runat="server">
        <telerik:RadScriptManager ID="scriptManager" runat="server"></telerik:RadScriptManager>
        <script type="text/javascript">
            function GetRadWindow() {
                var oWindow = null;
                if (window.radWindow) oWindow = window.radWindow;
                else if (window.frameElement.radWindow) oWindow = window.frameElement.radWindow;
                return oWindow;
            }

            function RadClose() {
                GetRadWindow().BrowserWindow.refreshGrid();
                GetRadWindow().close();
            }
        </script>
        <telerik:RadAjaxPanel ID="panelInner" runat="server">
            <table style="margin: auto; margin-top: 30px;">
                <tr>
                    <td>
                        <telerik:RadButton ID="b1" runat="server" OnCheckedChanged="b_CheckedChanged" Text="教师" Value="1" GroupName="AUT" ButtonType="ToggleButton" ToggleType="CheckBox" Skin="MetroTouch" AutoPostBack="true"></telerik:RadButton>
                    </td>
                    <td>
                        <telerik:RadButton ID="b2" runat="server" OnCheckedChanged="b_CheckedChanged" Text="学生" Value="2" GroupName="AUT" ButtonType="ToggleButton" ToggleType="CheckBox" Skin="MetroTouch" AutoPostBack="true"></telerik:RadButton>
                    </td>
                    <td>
                        <telerik:RadButton ID="b3" runat="server" OnCheckedChanged="b_CheckedChanged" Text="注册" Value="3" GroupName="AUT" ButtonType="ToggleButton" ToggleType="CheckBox" Skin="MetroTouch" AutoPostBack="true"></telerik:RadButton>
                    </td>
                </tr>
            </table>
        </telerik:RadAjaxPanel>
    </form>
</body>
</html>
