<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Teachers.aspx.cs" Inherits="Go_Teachers" %>

<%@ Register Src="~/Control/CommonTop.ascx" TagPrefix="uc1" TagName="CommonTop" %>
<%@ Register Src="~/Control/CommonBottom.ascx" TagPrefix="uc1" TagName="CommonBottom" %>



<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>资源平台 - 教师研训</title>
    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8">
    <meta http-equiv="Pragma" content="no-cache">
    <script src="../Script/jquery.min.js"></script>
    <link rel="stylesheet" href="../Style/common.css">
    <link rel="stylesheet" href="../Style/common(1).css">
    <link rel="stylesheet" href="../Style/common(2).css">
    <link rel="stylesheet" href="../Style/index.css">
    <link rel="stylesheet" href="../Style/2.css" id="skinCss">
    <link href="../Style/center.css" rel="stylesheet" />
    <base target="_top" />
</head>
<body>
    <form id="form" runat="server">
        <telerik:RadScriptManager runat="server"></telerik:RadScriptManager>
        <uc1:CommonTop runat="server" ID="CommonTop" />
        <div class="srx-bg22">

            <div class="srx-wrap11">
                <div style="margin: 0 auto; width: 1000px; background: #ffffff; min-height: 600px; margin-top: 30px;">

                    <telerik:RadAjaxPanel runat="server">

                        <telerik:RadComboBox runat="server" Skin="MetroTouch" ID="combo" Width="250px" Label="选择学校：" DataTextField="Name" DataValueField="Id" AutoPostBack="true" OnSelectedIndexChanged="combo_SelectedIndexChanged" style="margin: 30px 0 0 100px;"></telerik:RadComboBox>



                    <div style="clear: both;"></div>
                    <br /><br /><br />
                            <asp:Repeater ID="list" runat="server">
                                <ItemTemplate>
                                    <div style="width: 150px; float: left; text-align: center;">
                                        <a href='<%# string.Format("../Go/Personal?Id={0}", Eval("Id")) %>'>
                                            <img src='<%# P(Eval("Icon")).ToString().Replace("~", "..") %>' width="80" height="80" /></a></br>
                                            <a href='<%# string.Format("../Go/Personal?Id={0}", Eval("Id")) %>'><%# Eval("DisplayName") %></a>
                                        <br />
                                        <br />
                                    </div>
                                </ItemTemplate>
                            </asp:Repeater>
                            <div style="clear: both;"></div>
                    </telerik:RadAjaxPanel>

                </div>
                <uc1:CommonBottom runat="server" ID="CommonBottom" />
            </div>
        </div>

    </form>
</body>
</html>
