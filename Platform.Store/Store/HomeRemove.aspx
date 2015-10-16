<%@ Page Language="C#" AutoEventWireup="true" CodeFile="HomeRemove.aspx.cs" Inherits="Store_HomeRemove" %>

<%@ Register Src="~/Control/SideBar.ascx" TagPrefix="homory" TagName="SideBar" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="../Content/jQuery/jquery.min.js"></script>
    <link href="../assets/css/bootstrap.min.css" rel="stylesheet" />
    <link href="../assets/css/style-responsive.css" rel="stylesheet" />
    <link href="../assets/css/style.css" rel="stylesheet" />
    <link href="../Content/Core/css/common.css" rel="stylesheet" />
    <link href="../Content/Core/css/fix.css" rel="stylesheet" />
    <script src="../assets/js/bootstrap.min.js"></script>
    <script src="../Content/Homory/js/common.js"></script>
    <script src="../Content/Homory/js/notify.min.js"></script>
    <!--[if lt IE 9]>
	    <script src="../Content/Homory/js/html5shiv.js"></script>
	    <script src="../Content/Homory/js/respond.min.js"></script>
    <![endif]-->
</head>
<body>
    <form id="form" runat="server">
        <telerik:RadScriptManager ID="sm" runat="server"></telerik:RadScriptManager>
        <telerik:RadAjaxPanel ID="ap" runat="server" CssClass="container" LoadingPanelID="loading">
            <div class="row">
                <div class="col-md-12">
                    <input type="button" class="btn btn-tumblr" value="删除仓库" />
                    <hr style="color: #2B2B2B; margin-top: 4px;" />
                </div>
            </div>
            <div class="row">
                <div class="col-md-2"></div>
                <div class="col-md-8">
                    <div class="row">
                        <div class="col-md-4 text-right">仓库序号：</div>
                        <div class="col-md-8 text-left">
                            <telerik:RadNumericTextBox ID="ordinal" runat="server" Width="400" NumberFormat-DecimalDigits="0" MinValue="1"></telerik:RadNumericTextBox></div>
                    </div>
                    <div class="row">
                        <div class="col-md-4 text-right">仓库名称：</div>
                        <div class="col-md-8 text-left">
                            <telerik:RadTextBox ID="name" runat="server" Width="400"></telerik:RadTextBox></div>
                    </div>
                     <div class="row">
                        <div class="col-md-4 text-right">物资视图：</div>
                        <div class="col-md-8 text-left">
                            <telerik:RadComboBox ID="view" runat="server" Width="400">
                                <Items>
                                    <telerik:RadComboBoxItem Text="简洁模式" Value="1" Selected="true" />
                                    <telerik:RadComboBoxItem Text="图文模式" Value="2" />
                                </Items>
                            </telerik:RadComboBox>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-4 text-right">物资类型：</div>
                        <div class="col-md-8 text-left">
                            <div>
                                <table>
                                    <tr>
                                        <td>
                                            <telerik:RadButton ID="t1" runat="server" Text="易耗品（领用）" AutoPostBack="true" ButtonType="ToggleButton" ToggleType="CheckBox" Value="0" OnCheckedChanged="t_CheckedChanged"></telerik:RadButton>
                                        </td>
                                        <td class="tableSpan">&nbsp;</td>
                                        <td>
                                            <telerik:RadButton ID="t1x" runat="server" Text="默认" Visible="false" AutoPostBack="true" ButtonType="ToggleButton" ToggleType="CheckBox" Value="0" OnCheckedChanged="tx_CheckedChanged"></telerik:RadButton>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <telerik:RadButton ID="t2" runat="server" Text="低值非易耗（领用借用）" AutoPostBack="true" ButtonType="ToggleButton" ToggleType="CheckBox" Value="1" OnCheckedChanged="t_CheckedChanged"></telerik:RadButton>
                                        </td>
                                        <td class="tableSpan">&nbsp;</td>
                                        <td>
                                            <telerik:RadButton ID="t2x" runat="server" Text="默认" Visible="false" AutoPostBack="true" ButtonType="ToggleButton" ToggleType="CheckBox" Value="1" OnCheckedChanged="tx_CheckedChanged"></telerik:RadButton>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <telerik:RadButton ID="t3" runat="server" Text="准固定资产（自动拆分）" AutoPostBack="true" ButtonType="ToggleButton" ToggleType="CheckBox" Value="2" OnCheckedChanged="t_CheckedChanged"></telerik:RadButton>
                                        </td>
                                        <td class="tableSpan">&nbsp;</td>
                                        <td>
                                            <telerik:RadButton ID="t3x" runat="server" Text="默认" Visible="false" AutoPostBack="true" ButtonType="ToggleButton" ToggleType="CheckBox" Value="2" OnCheckedChanged="tx_CheckedChanged"></telerik:RadButton>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </div>
                    </div>
                     <div class="row" id="sp" runat="server">
                        <div class="col-md-4 text-right">特殊仓库：</div>
                        <div class="col-md-8 text-left">
                            <telerik:RadComboBox ID="state" runat="server" Width="400">
                                <Items>
                                    <telerik:RadComboBoxItem Text="非特殊仓库" Value="1" Selected="true" />
                                    <telerik:RadComboBoxItem Text="食品进出库" Value="-1" />
                                    <telerik:RadComboBoxItem Text="固定资产库" Value="-2" />
                                </Items>
                            </telerik:RadComboBox>
                        </div>
                    </div>
                    <div class="row">&nbsp;</div>
                    <div class="row">
                        <div class="col-md-4 text-right">&nbsp;</div>
                        <div class="col-md-8 text-left">
                            <input type="button" class="btn btn-tumblr" id="add" runat="server" value="保存" onserverclick="add_ServerClick" />
                            &nbsp;&nbsp;&nbsp;&nbsp;
                            <input type="button" class="btn btn-tumblr" id="cancel" runat="server" value="取消" onserverclick="cancel_ServerClick" />
                        </div>
                    </div>
                </div>
                <div class="col-md-2"></div>
            </div>
        </telerik:RadAjaxPanel>
    </form>
</body>
</html>
