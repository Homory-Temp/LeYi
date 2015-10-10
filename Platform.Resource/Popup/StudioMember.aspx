<%@ Page Language="C#" AutoEventWireup="true" CodeFile="StudioMember.aspx.cs" Inherits="Popup.ExtendedStudioMember" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=Edge,Chrome=1" />
    <meta name="viewport" content="width=device-width,initial-scale=1,minimum-scale=1,maximum-scale=1" />
    <title>选择成员</title>
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
<body style="margin: 0; padding: 0; overflow: auto; text-align: center;">
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
                GetRadWindow().close();
            }
        </script>
        <telerik:RadAjaxPanel ID="panelInner" runat="server">
            <br /><br />
            <telerik:RadSearchBox ID="peek" runat="server" Font-Size="12px" Skin="MetroTouch" OnSearch="peek_Search" EmptyMessage="查找...." EnableAutoComplete="false">
            </telerik:RadSearchBox>
            <br /><br />
            <telerik:RadListView ID="view" runat="server" OnNeedDataSource="view_NeedDataSource">
                <ItemTemplate>
                    <telerik:RadButton ID="btn" runat="server" Width="80" GroupName="LeaderGroup" AutoPostBack="true" Skin="Metro" ToggleType="CheckBox" Checked='<%# Count((Guid)Eval("Id")) %>' Text='<%# Eval("RealName") %>' Value='<%# Eval("Id") %>' OnClick="btn_Click"></telerik:RadButton>&nbsp;&nbsp;
                </ItemTemplate>
            </telerik:RadListView>
            <br /><br />
            <telerik:RadButton ID="buttonOk" runat="server" Skin="MetroTouch" Text="完成" OnClick="buttonOk_Click"></telerik:RadButton>
        </telerik:RadAjaxPanel>
    </form>
</body>
</html>
