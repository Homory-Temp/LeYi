<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Message.aspx.cs" Inherits="Message" %>
<%@ Import Namespace="System.Linq" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>梁溪教育网络寻呼</title>
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <style>
        html .RadButton.rbDisabled {
            opacity: 1;
        }

        html .RadInput .riEmpty, .RadInput_Empty {
            font-style: normal;
            font-size: 12px;
        }
    </style>
</head>
<body>
    <form id="form" runat="server">
        <telerik:RadScriptManager ID="sm" runat="server">
            <Scripts>
                <asp:ScriptReference Assembly="Telerik.Web.UI" Name="Telerik.Web.UI.Common.Core.js" />
                <asp:ScriptReference Assembly="Telerik.Web.UI" Name="Telerik.Web.UI.Common.jQuery.js" />
                <asp:ScriptReference Assembly="Telerik.Web.UI" Name="Telerik.Web.UI.Common.jQueryInclude.js" />
            </Scripts>
        </telerik:RadScriptManager>
        <telerik:RadAjaxPanel ID="ap" runat="server">
            <telerik:RadPageLayout ID="root" runat="server" GridType="Fluid">
                <telerik:LayoutRow>
                    <Content>
                        <telerik:RadButton ID="expand" runat="server" Skin="Office2010Blue" Height="24" ButtonType="ToggleButton" ToggleType="CustomToggle" AutoPostBack="false">
                            <ToggleStates>
                                <telerik:RadButtonToggleState Text="全部展开" Value="1" Selected="true" />
                                <telerik:RadButtonToggleState Text="全部收缩" Value="0" />
                            </ToggleStates>
                        </telerik:RadButton>
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <telerik:RadTextBox ID="name" runat="server" Skin="Office2010Blue" Height="24" EmptyMessage="请输入用户姓名查询"></telerik:RadTextBox>
                        &nbsp;
                        <telerik:RadButton ID="search" runat="server" Skin="Office2010Blue" Text="查询" Height="24" AutoPostBack="false"></telerik:RadButton>
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <telerik:RadButton runat="server" Enabled="false" ButtonType="LinkButton" ToggleType="None" ForeColor="#000000" Text="主职" Height="24" AutoPostBack="false"></telerik:RadButton>
                        <telerik:RadButton runat="server" Enabled="false" ButtonType="LinkButton" ToggleType="None" ForeColor="#FF0000" Text="主职在线" Height="24" AutoPostBack="false"></telerik:RadButton>
                        <telerik:RadButton runat="server" Enabled="false" ButtonType="LinkButton" ToggleType="None" ForeColor="#0000FF" Text="兼职" Height="24" AutoPostBack="false"></telerik:RadButton>
                        <telerik:RadButton runat="server" Enabled="false" ButtonType="LinkButton" ToggleType="None" ForeColor="#FF00BB" Text="兼职在线" Height="24" AutoPostBack="false"></telerik:RadButton>
                        &nbsp;
                        <telerik:RadButton ID="refresh" runat="server" Skin="Office2010Blue" Text="刷新" Height="24" AutoPostBack="false"></telerik:RadButton>
                    </Content>
                </telerik:LayoutRow>
                <telerik:LayoutRow>
                    <Content>
                        <telerik:RadTreeView ID="tree" runat="server" CheckBoxes="True" CheckChildNodes="True" DataTextField="机构名称" DataValueField="机构ID" DataFieldID="机构ID" DataFieldParentID="父级ID">
                        </telerik:RadTreeView>
                    </Content>
                </telerik:LayoutRow>
            </telerik:RadPageLayout>
        </telerik:RadAjaxPanel>
    </form>
</body>
</html>
