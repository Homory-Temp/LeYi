<%@ Page Language="C#" AutoEventWireup="true" CodeFile="WeChatBinding.aspx.cs" Inherits="Patch_WeChatBinding" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=Edge,Chrome=1" />
    <meta name="viewport" content="width=device-width,initial-scale=1,minimum-scale=1,maximum-scale=1" />
    <title>微信绑定查询</title>
    <script src="../Content/jQuery/jquery.min.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <telerik:RadScriptManager ID="sm" runat="server"></telerik:RadScriptManager>
            <telerik:RadAjaxPanel ID="panel" runat="server">
                <telerik:RadComboBox ID="combo" runat="server" AutoPostBack="true" DataTextField="Name" DataValueField="Id" OnSelectedIndexChanged="combo_SelectedIndexChanged"></telerik:RadComboBox>
                <br />
                <br />
                <telerik:RadGrid ID="grid" runat="server" OnNeedDataSource="grid_NeedDataSource" AutoGenerateColumns="false">
                    <MasterTableView runat="server">
                        <Columns>
                            <telerik:GridBoundColumn DataField="用户姓名" HeaderText="未绑定用户姓名"></telerik:GridBoundColumn>
                        </Columns>
                        <NoRecordsTemplate>
                            无记录
                        </NoRecordsTemplate>
                    </MasterTableView>
                </telerik:RadGrid>
            </telerik:RadAjaxPanel>
        </div>
    </form>
</body>
</html>
