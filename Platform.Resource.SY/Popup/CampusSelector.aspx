<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CampusSelector.aspx.cs" Inherits="Popup.CampusSelector" %>

<%@ Import Namespace="Homory.Model" %>

<!DOCTYPE html>

<html>
<head runat="server">
	<meta charset="utf-8" />
	<meta http-equiv="X-UA-Compatible" content="IE=Edge,Chrome=1" />
	<meta name="viewport" content="width=device-width,initial-scale=1,minimum-scale=1,maximum-scale=1" />
	<title>学校切换</title>
	<link href="../Content/Semantic/css/semantic.min.css" rel="stylesheet" />
	<link href="../Content/Homory/css/common.css" rel="stylesheet" />
	<link href="../Content/Core/css/common.css" rel="stylesheet" />
	<script src="../Content/jQuery/jquery.min.js"></script>
	<script src="../Content/Semantic/javascript/semantic.min.js"></script>
	<script src="../Content/Homory/js/common.js"></script>
	<script src="../Content/Homory/js/notify.min.js"></script>
</head>
<body>
    <style>
        .bSelector {
            margin: 6px;
            padding: 4px;
            float: left;
        }

        .bPIcon {
            margin-top: 4px;
        }
    </style>
    <script>
        function jjjj(sender, args) {
            var v = sender.get_value();
            if (v == "")
                top.location.href = "../Go/Home" + sender.get_value();
            else
                top.location.href = "../Go/CampusHome?Campus=" + sender.get_value();
            return false;
        }
    </script>
	<form id="formHome" runat="server">
        <telerik:RadScriptManager ID="scriptManager" runat="server"></telerik:RadScriptManager>
		<telerik:RadAjaxPanel ID="panel" runat="server" CssClass="ui left aligned stackable page grid">
			<div class="column">
                <telerik:RadButton OnClientClicked="jjjj" ID="all" ButtonType="ToggleButton" Icon-PrimaryIconCssClass="bPIcon" ToggleType="Radio" runat="server" Text="全部学校" Value="" Font-Bold="true" Width="170px" ForeColor="#227dc5" CssClass="bSelector"></telerik:RadButton>
                <telerik:RadListView ID="view" runat="server">
                    <ItemTemplate>
                        <telerik:RadButton ID="one" OnClientClicked="jjjj" ButtonType="ToggleButton" Icon-PrimaryIconCssClass="bPIcon" ToggleType="Radio" runat="server" Text='<%# Eval("Name") %>' Value='<%# Eval("Id") %>' Width="170px" ForeColor="#227dc5" CssClass="bSelector"></telerik:RadButton>
                    </ItemTemplate>
                </telerik:RadListView>
			</div>
		</telerik:RadAjaxPanel>
	</form>
</body>
</html>
