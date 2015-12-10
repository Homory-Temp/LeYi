<%@ Page Language="C#" AutoEventWireup="true" CodeFile="UserSelector.aspx.cs" Inherits="Popup.CampusSelector" %>

<%@ Import Namespace="Homory.Model" %>

<!DOCTYPE html>

<html>
<head runat="server">
	<meta charset="utf-8" />
	<meta http-equiv="X-UA-Compatible" content="IE=Edge,Chrome=1" />
	<meta name="viewport" content="width=device-width,initial-scale=1,minimum-scale=1,maximum-scale=1" />
	<title>人员选择</title>
	<link href="../Content/Semantic/css/semantic.min.css" rel="stylesheet" />
	<link href="../Content/Homory/css/common.css" rel="stylesheet" />
	<link href="../Content/Core/css/common.css" rel="stylesheet" />
	<script src="../Content/jQuery/jquery.min.js"></script>
	<script src="../Content/Semantic/javascript/semantic.min.js"></script>
	<script src="../Content/Homory/js/common.js"></script>
	<script src="../Content/Homory/js/notify.min.js"></script>
</head>
<body style="overflow: hidden;">
	<form id="formHome" runat="server">
        <telerik:RadScriptManager ID="scriptManager" runat="server"></telerik:RadScriptManager>
        <style>
            .tree {
                float: left;
            }

            .users {
                float: left;
            }

            .btn {
                padding: 4px 8px;
                margin: 6px;
            }

            .sb {
                margin: auto;
            }
        </style>
		<script>
			function GetRadWindow() {
				var oWindow = null;
				if (window.radWindow) oWindow = window.radWindow;
				else if (window.frameElement.radWindow) oWindow = window.frameElement.radWindow;
				return oWindow;
			}

			function onUserSelected(sender, args) {
			    GetRadWindow().BrowserWindow.______su = sender.get_value() == $("#v").val() ? "0" : sender.get_value().toString();
			    GetRadWindow().close();
			}

			GetRadWindow().BrowserWindow.______su = "";
		</script>
		<telerik:RadAjaxPanel ID="panel" runat="server" CssClass="ui left aligned stackable page grid">
            <telerik:RadTreeView ID="tree" runat="server" DataTextField="Name" DataValueField="Id" DataFieldID="Id" DataFieldParentID="ParentId" Width="220px" Height="506px" OnNodeClick="tree_NodeClick" CssClass="tree"></telerik:RadTreeView>
            <div style="height: 506px; overflow: auto;">
                <div style="width: 100%; margin: auto; text-align: center; clear: right; height: 40px;">
                    <telerik:RadSearchBox ID="peek" runat="server" OnSearch="peek_Search" EmptyMessage="查找...." EnableAutoComplete="false" CssClass="sb">
                    </telerik:RadSearchBox>
                </div>
                <hr style="margin-bottom: 10px; margin-top: 10px;" />
                <telerik:RadListView ID="view" runat="server" OnNeedDataSource="view_NeedDataSource" CssClass="users" Width="740px" Height="476px">
                    <ItemTemplate>
                        <telerik:RadButton ID="btn" runat="server" OnClientClicked="onUserSelected" Text='<%# Eval("RealName") %>' Checked='<%# (Guid)Eval("Id") == Initial %>' Value='<%# Eval("Id") %>' ToggleType="CheckBox" CssClass="btn" Width="180" AutoPostBack="false"></telerik:RadButton>
                    </ItemTemplate>
                </telerik:RadListView>
            </div>
		</telerik:RadAjaxPanel>
        <input id="v" type="hidden" value="" />
	</form>
</body>
</html>
