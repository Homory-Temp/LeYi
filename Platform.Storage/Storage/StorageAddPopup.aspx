<%@ Page Language="C#" AutoEventWireup="true" CodeFile="StorageAddPopup.aspx.cs" Inherits="StorageAddPopup" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <meta http-equiv="x-ua-compatible" content="ie=edge, chrome=1" />
    <meta name="viewport" content="width=device-width, initial-scale=1, minimum-scale=1, maximum-scale=1" />
        <link href="../Assets/stylesheets/amazeui.min.css" rel="stylesheet">
    <link href="../Assets/stylesheets/admin.css" rel="stylesheet">
    <link href="../Assets/stylesheets/bootstrap.min.css" rel="stylesheet">
    <link href="../Assets/stylesheets/bootstrap-theme.min.css" rel="stylesheet">

    <script src="../Assets/javascripts/jquery.min.js"></script>
    <script src="../Assets/javascripts/amazeui.min.js"></script>
    <script src="../Assets/javascripts/app.js"></script>
    <title>新增仓库</title>
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
</head>
<body>
    <form id="form" runat="server">
                    <div class="am-cf am-padding" style="border-bottom: 1px solid #E1E1E1;">
                        <div class="am-fl am-cf">
                            <strong class="am-text-primary am-text-lg">添加仓库</strong>
                        </div>
                    </div>
        <telerik:RadScriptManager ID="sm" runat="server"></telerik:RadScriptManager>
        <telerik:RadCodeBlock ID="cb" runat="server">
            <script>
                function peek() {
                    var oWindow = null;
                    if (window.radWindow) oWindow = window.radWindow;
                    else if (window.frameElement.radWindow) oWindow = window.frameElement.radWindow;
                    return oWindow;
                }
                function ok() {
                    peek().BrowserWindow.rebind();
                    peek().close();
                }
                function cancel() {
                    peek().close();
                }
            </script>
        </telerik:RadCodeBlock>

        <telerik:RadAjaxPanel ID="ap" runat="server" CssClass="grid-container">
                <table width="100%" align="center">
                <tr>
                    <td width="100%" align="center">
                        <div  class="grid-100"  style="height:45px;margin-top:50px;" >
                    顺序号：<telerik:RadNumericTextBox ID="ordinal" runat="server" NumberFormat-DecimalDigits="0" DataType="System.Int32" AllowOutOfRangeAutoCorrect="true" Width="40%"></telerik:RadNumericTextBox>
                         </div>
                        <div  class="grid-100"  style="height:65px;" >
                    名称：<telerik:RadTextBox ID="name" runat="server" Width="40%"></telerik:RadTextBox>
                       </div>
                        <div  class="grid-100"  style="height:65px;" >
                    <asp:ImageButton ID="ok" runat="server" AlternateText="保存" OnClick="ok_Click" class="btn btn-xm btn-default"></asp:ImageButton>
                    <asp:ImageButton ID="cancel" runat="server" AlternateText="取消" OnClick="cancel_Click" class="btn btn-xm btn-default"></asp:ImageButton>
                </div>
                 
                    </td>
                </tr>
            </table>
        </telerik:RadAjaxPanel>
    </form>
    <script src="../Assets/javascripts/jquery.js"></script>
    <script src="../Assets/javascripts/common.js"></script>
</body>
</html>
