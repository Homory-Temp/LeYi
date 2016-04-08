<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Online.aspx.cs" Inherits="Online" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>在线统计</title>
</head>
<body>
    <form id="form" runat="server">
        <textarea style="width: 100%; color: red;">将下方文本框内全选复制并粘贴入Excel.</textarea>
        <br /><br />
        <textarea id="table" runat="server" style="width: 100%; height: 600px;"></textarea>
    </form>
</body>
</html>
