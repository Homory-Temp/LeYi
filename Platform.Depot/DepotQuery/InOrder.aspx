<%@ Page Language="C#" AutoEventWireup="true" CodeFile="InOrder.aspx.cs" Inherits="DepotQuery_InPrint" %>

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
<body style="margin: 0;">
    <form id="form" runat="server" style="margin: 0;">
        <telerik:RadScriptManager ID="sm" runat="server"></telerik:RadScriptManager>
        <telerik:RadAjaxPanel ID="ap" runat="server">
            <div>&nbsp;</div>
            <div>&nbsp;</div>
            <div style="width: 100%; text-align: center;">
                <table style="margin: auto; text-align: left;">
                    <tr>
                        <td>购置单号：&nbsp;&nbsp;
                        </td>
                        <td>
                            <asp:Label ID="x" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">&nbsp;</td>
                    </tr>
                    <tr>
                        <td>流程编号：&nbsp;&nbsp;
                        </td>
                        <td>
                            <telerik:RadTextBox ID="name" runat="server" Width="400"></telerik:RadTextBox>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">&nbsp;</td>
                    </tr>
                    <tr>
                        <td colspan="2" style="text-align: center;">
                            <input type="button" class="btn btn-tumblr" id="go" runat="server" value="保存" style="margin: auto;" onserverclick="go_ServerClick" />
                        </td>
                    </tr>
                </table>
            </div>
        </telerik:RadAjaxPanel>
    </form>
</body>
</html>
