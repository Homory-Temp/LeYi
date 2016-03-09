<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Searching.aspx.cs" Inherits="DepotSample_Searching" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Searching</title>
    <script>
        function combo_itemsRequesting(sender, args) {
            var content = sender.get_text();
            if (!content) {
                sender.trackChanges();
                sender.get_items().clear();
                sender.commitChanges();
                args.set_cancel(false);
                return;
            }
            var current = new RegExp(content);
            var list = new Array(5);
            list[0] = { "text": "李佳", "value": "lj" };
            list[1] = { "text": "张二猛", "value": "zem" };
            list[2] = { "text": "凌俊伟", "value": "ljw" };
            list[3] = { "text": "陈海燕", "value": "chy" };
            list[4] = { "text": "袁益鹏", "value": "yyp" };
            sender.trackChanges();
            sender.get_items().clear();
            for (var i = 0; i < list.length; i++) {
                if (current.test(list[i].value)) {
                    var item = new Telerik.Web.UI.RadComboBoxItem();
                    item.set_text(list[i].text);
                    item.set_value(list[i].value);
                    sender.get_items().add(item);
                    sender.bindTemplate();
                }
            }
            sender.commitChanges();
            args.set_cancel(false);
        }
    </script>
</head>
<body>
    <form id="form" runat="server">
        <telerik:RadScriptManager runat="server"></telerik:RadScriptManager>
        <telerik:RadAjaxPanel runat="server">
            <telerik:RadComboBox ID="combo" runat="server" Height="100" EnableItemCaching="false" OnClientItemsRequesting="combo_itemsRequesting" EnableLoadOnDemand="true" AllowCustomText="true"></telerik:RadComboBox>
        </telerik:RadAjaxPanel>
    </form>
</body>
</html>
