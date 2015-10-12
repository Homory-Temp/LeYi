<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ObjectAddPopup.aspx.cs" Inherits="ObjectAddPopup" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <meta http-equiv="x-ua-compatible" content="ie=edge, chrome=1" />
    <meta name="viewport" content="width=device-width, initial-scale=1, minimum-scale=1, maximum-scale=1" />
    <title>新增物品</title>
    <link href="../Assets/stylesheets/amazeui.min.css" rel="stylesheet">
    <link href="../Assets/stylesheets/admin.css" rel="stylesheet">
    <link href="../Assets/stylesheets/bootstrap.min.css" rel="stylesheet">
    <link href="../Assets/stylesheets/bootstrap-theme.min.css" rel="stylesheet">
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
</head>
<body>
    <form id="form" runat="server">
        <telerik:RadScriptManager ID="sm" runat="server"></telerik:RadScriptManager>
        <div class="am-cf am-padding" style="border-bottom: 1px solid #E1E1E1;">
            <div class="am-fl am-cf">
                <strong class="am-text-primary am-text-lg">添加物品</strong>
            </div>
        </div>

        <telerik:RadCodeBlock ID="cb" runat="server">
            <script>
                function peek() {
                    var oWindow = null;
                    if (window.radWindow) oWindow = window.radWindow;
                    else if (window.frameElement.radWindow) oWindow = window.frameElement.radWindow;
                    return oWindow;
                }
                function ok() {
                    peek().BrowserWindow.rebind();
                    peek().close();
                }
                function cancel() {
                    peek().close();
                }
                function fix_sh(sender, args) {
                    var isFixed = args.get_checked();
                    if (isFixed) {
                        $("#<%= fixedArea.ClientID %>").show();
                        $find("<%= consumable.ClientID %>").set_visible(false);
                    }
                    else {
                        $("#<%= fixedArea.ClientID %>").hide();
                        $find("<%= consumable.ClientID %>").set_visible(true);
                    }
                }
                function cf_sh(sender, args) {
                    var isC = args.get_checked();
                    if (isC) {
                        $("#<%= fixedArea.ClientID %>").hide();
                            $find("<%= @fixed.ClientID %>").set_visible(false);
                        }
                        else {
                            $("#<%= fixedArea.ClientID %>").hide();
                            $find("<%= @fixed.ClientID %>").set_visible(true);
                        }
                    }
                    function img_up(sender, e) {
                        $find("<%= ap.ClientID %>").ajaxRequest("Upload");
                    }
                    function sh(sender, args) {
                        var c = sender.get_checked();
                        if (!c)
                            sender.set_checked(true);
                        var v = sender.get_value();
                        $find("<%= consumable.ClientID %>").set_checked(v == "1");
                        $find("<%= multiple.ClientID %>").set_checked(v == "2");
                        $find("<%= @single.ClientID %>").set_checked(v == "3");
                        $find("<%= @fixed.ClientID %>").set_checked(v == "4");
                        $find("<%= fixedSerial.ClientID %>").set_visible(v == "4");
                        if (v == "4") {
                            $("#<%= fixedArea.ClientID %>").show();
           $("#<%= trtr.ClientID %>").show();
       }
       else {
           $("#<%= fixedArea.ClientID %>").hide();
           $("#<%= trtr.ClientID %>").hide();
       }
                    }

            </script>
        </telerik:RadCodeBlock>
        <telerik:RadAjaxPanel ID="ap" runat="server">
            <div style="margin-left: 400px;">
                <table width="650" align="center" style="margin-top: 10px;">
                    <tr>
                        <td width="300" align="right">顺序号：</td>
                        <td align="left">
                            <telerik:RadNumericTextBox ID="ordinal" runat="server" NumberFormat-DecimalDigits="0" DataType="System.Int32" AllowOutOfRangeAutoCorrect="true" Width="40%"></telerik:RadNumericTextBox>
                        </td>
                    </tr>
                    <tr>
                        <td width="300" align="right">编号：</td>
                        <td width="" align="left">
                            <telerik:RadTextBox ID="code" runat="server" Width="40%"></telerik:RadTextBox>
                        </td>
                    </tr>
                    <tr>
                        <td width="300" align="right">名称：</td>
                        <td align="left">
                            <telerik:RadTextBox ID="name" runat="server" Width="40%"></telerik:RadTextBox>
                        </td>
                    </tr>

                    <tr>
                        <td width="300" align="right">单位：</td>
                        <td align="left">
                            <telerik:RadComboBox ID="unit" runat="server" LocalizationPath="~/Language" DataTextField="Name" DataValueField="Name" Filter="Contains" MarkFirstMatch="true" AppendDataBoundItems="true" ShowToggleImage="false" Width="40%" AllowCustomText="true">
                                <Items>
                                    <telerik:RadComboBoxItem Text="" Value="" Selected="true" />
                                </Items>
                                <ItemTemplate>
                                    <%# Eval("Name") %><span style="display: none;"><%# Eval("PinYin") %></span>
                                </ItemTemplate>
                            </telerik:RadComboBox>
                        </td>
                    </tr>
                    <tr>
                        <td width="300" align="right">规格：</td>
                        <td align="left">
                            <telerik:RadComboBox ID="specification" runat="server" LocalizationPath="~/Language" DataTextField="Name" DataValueField="Name" Filter="Contains" MarkFirstMatch="true" AppendDataBoundItems="true" ShowToggleImage="false" Width="40%" AllowCustomText="true">
                                <Items>
                                    <telerik:RadComboBoxItem Text="" Value="" Selected="true" />
                                </Items>
                                <ItemTemplate>
                                    <%# Eval("Name") %><span style="display: none;"><%# Eval("PinYin") %></span>
                                </ItemTemplate>
                            </telerik:RadComboBox>
                        </td>
                    </tr>

                    <tr>
                        <td></td>
                        <td align="left">
                            <telerik:RadButton ID="consumable" runat="server" Checked="false" Text="低值易耗（可领用）" AutoPostBack="false" ButtonType="ToggleButton" ToggleType="CheckBox" Value="1" OnClientClicked="sh"></telerik:RadButton>
                        </td>

                    </tr>

                    <tr>
                        <td></td>
                        <td align="left">
                            <telerik:RadButton ID="multiple" runat="server" Checked="true" Text="低值非易耗（可借用）" AutoPostBack="false" ButtonType="ToggleButton" ToggleType="CheckBox" Value="2" OnClientClicked="sh"></telerik:RadButton>
                        </td>

                    </tr>

                    <tr>
                        <td></td>
                        <td align="left">
                            <telerik:RadButton ID="single" runat="server" Checked="false" Text="准固定资产（可借用，单件管理）" AutoPostBack="false" ButtonType="ToggleButton" ToggleType="CheckBox" Value="3" OnClientClicked="sh"></telerik:RadButton>
                        </td>

                    </tr>

                    <tr>
                        <td></td>
                        <td align="left">
                            <telerik:RadButton ID="fixed" runat="server" Checked="false" Text="固定资产（可借用，单件管理）" AutoPostBack="false" ButtonType="ToggleButton" ToggleType="CheckBox" Value="4" OnClientClicked="sh"></telerik:RadButton>
                        </td>

                    </tr>
                    <tr id="trtr" runat="server">
                        <td align="left" width="300">
                            <div id="fixedArea" runat="server" style="height: 35px;" align="right">固定资产编号：</div>
                        </td>
                        <td align="left">
                            <telerik:RadTextBox ID="fixedSerial" runat="server" Width="40%"></telerik:RadTextBox>
                        </td>
                    </tr>
                    <tr>
                        <td width="300" align="right">下限预警：</td>
                        <td align="left">
                            <telerik:RadNumericTextBox ID="low" runat="server" NumberFormat-DecimalDigits="2" DataType="System.Decimal" AllowOutOfRangeAutoCorrect="true" Width="40%"></telerik:RadNumericTextBox>
                        </td>
                    </tr>
                    <tr>
                        <td width="300" align="right">上限预警：</td>
                        <td align="left">
                            <telerik:RadNumericTextBox ID="high" runat="server" NumberFormat-DecimalDigits="2" DataType="System.Decimal" AllowOutOfRangeAutoCorrect="true" Width="40%"></telerik:RadNumericTextBox>
                        </td>
                    </tr>
                    <tr>
                        <td width="300" align="right">备注：</td>
                        <td align="left">
                            <telerik:RadTextBox ID="note" runat="server" Width="40%" TextMode="MultiLine" Height="60"></telerik:RadTextBox>
                        </td>
                    </tr>
                    <tr>
                        <td width="300" align="right">图片：</td>
                        <td algin="center">
                            <table height="150" align="center" width="800">
                                <tr>
                                    <td align="left">
                                        <div style="height: 120px; margin: 10px;" id="div1" runat="server" visible="false">
                                            <asp:Image ID="img1" runat="server" Width="80" Height="80" /><asp:ImageButton ID="btn1" runat="server" AlternateText="删除" OnClick="btn1_Click"></asp:ImageButton>
                                        </div>
                                    </td>
                                    <td align="left">
                                        <div style="height: 120px; margin: 10px;" id="div2" runat="server" visible="false">
                                            <asp:Image ID="img2" runat="server" Width="80" Height="80" /><asp:ImageButton ID="btn2" runat="server" AlternateText="删除" OnClick="btn2_Click"></asp:ImageButton>
                                        </div>
                                    </td>
                                    <td align="left" valign="middle">
                                        <div style="height: 120px; margin: 10px;" id="div3" runat="server" visible="false">
                                            <asp:Image ID="img3" runat="server" Width="80" Height="80" /><asp:ImageButton ID="btn3" runat="server" AlternateText="删除" OnClick="btn3_Click"></asp:ImageButton>
                                        </div>
                                    </td>
                                    <td align="left">
                                        <div style="height: 120px; margin: 10px;" id="div4" runat="server" visible="false">
                                            <asp:Image ID="img4" runat="server" Width="80" Height="80" /><asp:ImageButton ID="btn4" runat="server" AlternateText="删除" OnClick="btn4_Click"></asp:ImageButton>
                                        </div>
                                    </td>
                                    <td align="left">
                                        <div style="height: 35px; margin-left: 35px; text-align: left;">
                                            <telerik:RadAsyncUpload ID="image" runat="server" LocalizationPath="~/Language" TemporaryFolder="~/Temp" TargetFolder="~/Upload" AllowedFileExtensions="bmp,gif,jpg,jpeg,png" MaxFileSize="10240000" MultipleFileSelection="Disabled" ManualUpload="false" MaxFileInputsCount="4" HideFileInput="true" OnClientFileUploaded="img_up" OnFileUploaded="image_FileUploaded"></telerik:RadAsyncUpload>
                                        </div>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            <asp:ImageButton ID="ok" runat="server" AlternateText="保存" OnClick="ok_Click" class="btn btn-xm btn-default"></asp:ImageButton></td>



                        <td align="left">
                            <asp:ImageButton ID="ok_in" runat="server" AlternateText="保存并入库" OnClick="ok_in_Click" class="btn btn-xm btn-default"></asp:ImageButton>
                            <asp:ImageButton ID="cancel" runat="server" AlternateText="取消" OnClick="cancel_Click" class="btn btn-xm btn-default"></asp:ImageButton>
                        </td>
                    </tr>
                </table>
            </div>
        </telerik:RadAjaxPanel>

    </form>
    <script src="../Assets/javascripts/jquery.js"></script>
    <script src="../Assets/javascripts/common.js"></script>
    <style>
        html .RadSearchBox_Bootstrap .rsbInput {
            height: 32px;
            line-height: 32px;
        }
    </style>
</body>
</html>
