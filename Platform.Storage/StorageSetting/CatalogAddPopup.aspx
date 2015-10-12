<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CatalogAddPopup.aspx.cs" Inherits="CatalogAddPopup" %>

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
    <title>新增分类</title>
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
        <telerik:RadScriptManager ID="sm" runat="server" ></telerik:RadScriptManager>
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
              <div class="am-cf am-padding" style="border-bottom: 1px solid #E1E1E1;">
                        <div class="am-fl am-cf">
                            <strong class="am-text-primary am-text-lg">添加分类</strong>
                        </div>
                    </div>
        <telerik:RadAjaxPanel ID="ap" runat="server" CssClass="grid-container">
            <div class="grid-100 mobile-grid-100 grid-parent">
                <div  style="height:65px;margin-top:50px;" >
                  &nbsp;&nbsp;&nbsp;&nbsp;顺序号：<telerik:RadNumericTextBox ID="ordinal" runat="server" NumberFormat-DecimalDigits="0" DataType="System.Int32" AllowOutOfRangeAutoCorrect="true" Width="40%"></telerik:RadNumericTextBox>
                </div>
                <div style="height:65px;">
                     &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;名称：<telerik:RadTextBox ID="name" runat="server"  Width="40%"></telerik:RadTextBox>
                </div>
                <div style="height:65px;">
                    分类编号：<telerik:RadTextBox ID="code" runat="server"  Width="40%"></telerik:RadTextBox>
                </div>
                <div style="height:65px;">
                    <asp:ImageButton ID="ok" runat="server" AlternateText="保存" OnClick="ok_Click"  class="btn btn-xm btn-default"></asp:ImageButton>
                    <asp:ImageButton ID="cancel" runat="server" AlternateText="取消" OnClick="cancel_Click"  class="btn btn-xm btn-default"></asp:ImageButton>
                </div>
            </div>
        </telerik:RadAjaxPanel>
    </form>
    <script src="../Assets/javascripts/jquery.js"></script>
    <script src="../Assets/javascripts/common.js"></script>
</body>
</html>
