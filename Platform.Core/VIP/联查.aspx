<%@ Page Language="C#" AutoEventWireup="true" CodeFile="联查.aspx.cs" Inherits="VIP_联查" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>联查</title>
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
    <form id="form" runat="server">
        <telerik:RadScriptManager ID="sm" runat="server" LoadScriptsBeforeUI="true" EnablePartialRendering="true">
            <Scripts>
                <asp:ScriptReference Assembly="Telerik.Web.UI" Name="Telerik.Web.UI.Common.Core.js" />
                <asp:ScriptReference Assembly="Telerik.Web.UI" Name="Telerik.Web.UI.Common.jQuery.js" />
                <asp:ScriptReference Assembly="Telerik.Web.UI" Name="Telerik.Web.UI.Common.jQueryPlugins.js" />
            </Scripts>
        </telerik:RadScriptManager>

        <div style="width: 100%;">
            <telerik:RadAjaxPanel ID="ap" runat="server">
                <p>
                    <telerik:RadTextBox ID="text" EmptyMessage="输入要检索的内容" runat="server"></telerik:RadTextBox>
                    <telerik:RadButton ID="search" runat="server" Text="检索" OnClick="search_Click"></telerik:RadButton>
                </p>
                <p>
                    <telerik:RadGrid ID="view" runat="server" AutoGenerateColumns="false" OnNeedDataSource="view_NeedDataSource">
                        <HeaderStyle HorizontalAlign="Center" />
                        <MasterTableView runat="server">
                            <Columns>
                                <telerik:GridBoundColumn DataField="用户ID" HeaderText="用户ID"></telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="金和ID" HeaderText="金和ID"></telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="用户账号" HeaderText="用户账号"></telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="金和账号" HeaderText="金和账号"></telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="用户姓名" HeaderText="用户姓名"></telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="用户拼音" HeaderText="用户拼音"></telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="用户手机" HeaderText="用户手机"></telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="用户证件" HeaderText="用户证件"></telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="用户在编" HeaderText="用户在编"></telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="用户同步" HeaderText="用户同步"></telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="用户部门" HeaderText="用户部门"></telerik:GridBoundColumn>
                                <telerik:GridTemplateColumn>
                                    <ItemTemplate>
                                        <asp:Image runat="server" ImageUrl='<%# Eval("用户头像") %>' Width="60" Height="60" />
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                            </Columns>
                        </MasterTableView>
                    </telerik:RadGrid>
                </p>
            </telerik:RadAjaxPanel>
        </div>
    </form>
</body>
</html>
