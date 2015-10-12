<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CheckStart.aspx.cs" Inherits="CheckStart" %>

<%@ Register Src="~/Menu/MenuMobile.ascx" TagPrefix="homory" TagName="MenuMobile" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <meta http-equiv="x-ua-compatible" content="ie=edge, chrome=1" />
    <meta name="viewport" content="width=device-width, initial-scale=1, minimum-scale=1, maximum-scale=1" />
    <title>物资管理 - 仓库盘库</title>
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
    <script>
        ajax_onRequestStart = function (sender, args) {
            if (args.get_eventTarget().indexOf("Button") >= 0) {
                args.set_enableAjax(false);
            }
        }
    </script>

    <script>

        window.uexOnload = function (type) {

            if (!type) {

                uexWidgetOne.onError = function (opCode, errorCode, errorDesc) {

                    console.log(errorCode + ':' + errorDesc);

                }

            }

        }

        function $$(id) {

            return document.getElementById(id);

        }

        function scannerOpen() {

            uexScanner.cbOpen = function (opCode, dataType, data) {

                var obj = eval('(' + data + ')');

                console.log('Result:' + obj.code + ' Format:' + obj.type);

            }

            uexScanner.open();

        }

</script>
</head>
<body style="width:320px;margin:0 auto;">
    <form id="form" runat="server">
        <telerik:RadScriptManager ID="sm" runat="server"></telerik:RadScriptManager>
        <telerik:RadAjaxPanel ID="ap" runat="server" ClientEvents-OnRequestStart="ajax_onRequestStart">
            <div class="grid-100 mobile-grid-100 grid-parent" style="width:320px;margin:0 auto;">
                盘库名称：<asp:Label ID="name_ex" runat="server"></asp:Label>
            </div>
            <div class="grid-100 mobile-grid-100 grid-parent" style="width:320px;margin:0 auto;">
                <table align="center"><tr><td>
                <telerik:RadTextBox ID="code" runat="server" MaxLength="12" Style="ime-mode: disabled;"></telerik:RadTextBox></td><td>

                <asp:ImageButton ID="add" runat="server" AlternateText="查询" OnClick="add_Click" /></td></tr></table>
               
            </div>
            <div class="grid-100 mobile-grid-100 grid-parent" style="width:320px;margin:0 auto;">
                <input id="kcuf" runat="server" type="hidden" />
                <input id="kcufX" runat="server" type="hidden" />
                <telerik:RadGrid ID="grid" runat="server" OnNeedDataSource="grid_NeedDataSource" AutoGenerateColumns="false" LocalizationPath="~/Language">
                    <MasterTableView DataKeyNames="单计标识">
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="Center" />
                        <AlternatingItemStyle HorizontalAlign="Center" />
                        <Columns>
                            <telerik:GridTemplateColumn HeaderText="物资名称">
                                <ItemTemplate>
                                    <%# Eval("物品名称") %>-<%# Eval("编号") %>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridBoundColumn DataField="固定资产编号" HeaderText="固定资产编号"></telerik:GridBoundColumn>
                           
                            <telerik:GridBoundColumn DataField="责任人" HeaderText="责任人"></telerik:GridBoundColumn>
                            <telerik:GridTemplateColumn HeaderText="状态">
                                <ItemTemplate>
                                    <asp:Label runat="server" ForeColor='<%# CDCColor((Guid)Eval("单计标识")) %>' Text='<%# CDC((Guid)Eval("单计标识")) ? "已盘" : "未盘" %>'></asp:Label>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                        </Columns>
                        <NoRecordsTemplate></NoRecordsTemplate>
                    </MasterTableView>
                </telerik:RadGrid>
            </div>
            <div class="grid-100 mobile-grid-100 grid-parent" style="width:320px;margin:0 auto;">
                <asp:ImageButton AlternateText="返回" ID="back" runat="server" OnClick="back_Click" />
                <asp:ImageButton AlternateText="保存并返回" ID="save" runat="server" OnClick="save_Click" />
            </div>
        </telerik:RadAjaxPanel>
    </form>
    <script src="../Assets/javascripts/jquery.js"></script>
    <script src="../Assets/javascripts/common.js"></script>
</body>
</html>
