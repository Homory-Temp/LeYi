<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Import.aspx.cs" Inherits="Import" %>

<%@ Register Src="~/Menu/Menu.ascx" TagPrefix="homory" TagName="Menu" %>
<%@ Register Src="~/StorageObject/ObjectImage.ascx" TagPrefix="homory" TagName="ObjectImage" %>
<%@ Register Src="~/StorageObject/ObjectImageOne.ascx" TagPrefix="homory" TagName="ObjectImageOne" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <meta http-equiv="x-ua-compatible" content="ie=edge, chrome=1" />
    <meta name="viewport" content="width=device-width, initial-scale=1, minimum-scale=1, maximum-scale=1" />
    <title>物资管理 - 数据导入</title>
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
        <telerik:RadScriptManager ID="sm" runat="server"></telerik:RadScriptManager>
        <homory:Menu runat="server" ID="menu" />
<telerik:RadAjaxPanel ID="ap" runat="server" CssClass="grid-container">
              <table style="margin-left: 10px; margin-top: 10px;" align="center">

                   <tr>
                        <td width="100" align="center" height="45" colspan="2">请选择财政局固定资产Excel文件：
                        </td>
               </tr>
                    <tr>
                        <td width="100" align="right" height="45">
                        </td>
                        <td width="300">
                <telerik:RadTextBox ID="code" runat="server" MaxLength="12" EmptyMessage="" width="300px"></telerik:RadTextBox></td>
                        <td>
                <asp:ImageButton ID="add" runat="server" AlternateText="上传" /></td></tr>

              
               
                 
            </table>
        </telerik:RadAjaxPanel>
        <telerik:RadAjaxPanel ID="apx" runat="server" CssClass="grid-container">
            <div class="grid-100 mobile-grid-100 grid-parent">
                <div>
                    <div style="margin-top: 10px;">
                        <asp:ImageButton AlternateText="  导入" ID="out" runat="server" class="btn btn-xm btn-default"  />
                    </div>
                </div>
            </div>
        </telerik:RadAjaxPanel>  
        </form>
     <style>
        html .RadSearchBox {
            display: -moz-inline-stack;
            display: inline-block;
            *display: inline:;
            *zoom: 1:;
            width: 300px;
            text-align: left;
            line-height: 30px;
            height: 30px;
            white-space: nowrap;
            vertical-align: middle;
        }
    </style>
    <script src="../Assets/javascripts/jquery.js"></script>
    <script src="../Assets/javascripts/common.js"></script>
</body>
</html>
