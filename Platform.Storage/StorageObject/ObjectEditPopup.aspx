<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ObjectEditPopup.aspx.cs" Inherits="ObjectEditPopup" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <meta http-equiv="x-ua-compatible" content="ie=edge, chrome=1" />
    <meta name="viewport" content="width=device-width, initial-scale=1, minimum-scale=1, maximum-scale=1" />
    <title>编辑物品</title>
    <link href="../Assets/stylesheets/amazeui.min.css" rel="stylesheet">
    <link href="../Assets/stylesheets/admin.css" rel="stylesheet">
    <link href="../Assets/stylesheets/bootstrap.min.css" rel="stylesheet">
    <link href="../Assets/stylesheets/bootstrap-theme.min.css" rel="stylesheet">

    <link href="../Assets/stylesheets/common.css" rel="stylesheet" />
</head>
<body>
    <form id="form" runat="server">
        <telerik:RadScriptManager ID="sm" runat="server"></telerik:RadScriptManager>
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
                    if (isFixed)
                        $("#<%= fixedArea.ClientID %>").show();
                    else
                        $("#<%= fixedArea.ClientID %>").hide();
                }
                function img_up(sender, e) {
                    $find("<%= ap.ClientID %>").ajaxRequest("Upload");
                }
            </script>
        </telerik:RadCodeBlock>
        
        <div style="margin: 0 auto;height:800px; width: 1000px;">
            <telerik:RadAjaxPanel ID="ap" runat="server">
                <div class="grid-100 mobile-grid-100 grid-parent">
                    <div style="height: 35px; margin-top: 5px;">
                        顺序号：<telerik:RadNumericTextBox ID="ordinal" runat="server" NumberFormat-DecimalDigits="0" DataType="System.Int32" AllowOutOfRangeAutoCorrect="true" Width="40%"></telerik:RadNumericTextBox>
                    </div>
                    <div style="height: 35px;">
                      &nbsp;&nbsp;分类&nbsp;：<telerik:RadDropDownTree ID="tree" runat="server" Width="40%" DataTextField="Name" DataValueField="Id" DataFieldID="Id" DataFieldParentID="ParentId" DropDownSettings-CloseDropDownOnSelection="true"></telerik:RadDropDownTree>
                    </div>
                    <div style="height: 35px;">
                      &nbsp;&nbsp;编码&nbsp;：<telerik:RadTextBox ID="code" runat="server" Width="40%"></telerik:RadTextBox>
                    </div>
                    <div style="height: 35px;">
                        &nbsp;&nbsp;名称&nbsp;：<telerik:RadTextBox ID="name" runat="server" Width="40%"></telerik:RadTextBox>
                    </div>
                    <div style="height: 35px;">
                        &nbsp;&nbsp;单位&nbsp;：<telerik:RadComboBox ID="unit" runat="server" LocalizationPath="~/Language" DataTextField="Name" DataValueField="Name" Filter="Contains" MarkFirstMatch="true" AppendDataBoundItems="true" ShowToggleImage="false" Width="40%" AllowCustomText="true">
                            <Items>
                                <telerik:RadComboBoxItem Text="" Value="" Selected="true" />
                            </Items>
                            <ItemTemplate>
                                <%# Eval("Name") %><span style="display: none;"><%# Eval("PinYin") %></span>
                            </ItemTemplate>
                        </telerik:RadComboBox>
                    </div>
                    <div style="height: 35px;">
                        &nbsp;&nbsp;规格&nbsp;：<telerik:RadComboBox ID="specification" runat="server" LocalizationPath="~/Language" DataTextField="Name" DataValueField="Name" Filter="Contains" MarkFirstMatch="true" AppendDataBoundItems="true" ShowToggleImage="false" Width="40%" AllowCustomText="true">
                            <Items>
                                <telerik:RadComboBoxItem Text="" Value="" Selected="true" />
                            </Items>
                            <ItemTemplate>
                                <%# Eval("Name") %><span style="display: none;"><%# Eval("PinYin") %></span>
                            </ItemTemplate>
                        </telerik:RadComboBox>
                    </div>
                    <div id="fixedArea" runat="server" style="display: none;">
                        固定资产编号：<telerik:RadTextBox ID="fixedSerial" runat="server" Width="40%"></telerik:RadTextBox>
                    </div>
                    <div style="height: 35px;">
                        &nbsp;&nbsp;低值&nbsp;：<telerik:RadNumericTextBox ID="low" runat="server" NumberFormat-DecimalDigits="2" DataType="System.Decimal" AllowOutOfRangeAutoCorrect="true" Width="40%"></telerik:RadNumericTextBox>
                    </div>
                    <div style="height: 35px;">
                        &nbsp;&nbsp;高值&nbsp;：<telerik:RadNumericTextBox ID="high" runat="server" NumberFormat-DecimalDigits="2" DataType="System.Decimal" AllowOutOfRangeAutoCorrect="true" Width="40%"></telerik:RadNumericTextBox>
                    </div>
                    <div style="height: 60px;">
                        &nbsp;&nbsp;备注&nbsp;：<telerik:RadTextBox ID="note" runat="server" Width="40%" TextMode="MultiLine" height="60"></telerik:RadTextBox>
                    </div>
                    <div style="height: 35px; margin-left: 265px; text-align: left;">
                        &nbsp;&nbsp;图片&nbsp;&nbsp;：
                    </div>
                    <table height="150" align="center" width="500">
                                <tr>
                                    <td align="left" >
                                        <div style="height: 120px;margin:10px;" id="div1" runat="server" visible="false">
                                            <asp:Image ID="img1" runat="server" Width="80" Height="80" /><asp:ImageButton ID="btn1" runat="server" AlternateText="删除" OnClick="btn1_Click"></asp:ImageButton>
                                        </div>
                                    </td>
                                    <td align="left">
                                        <div style="height: 120px;margin:10px;" id="div2" runat="server" visible="false">
                                            <asp:Image ID="img2" runat="server" Width="80" Height="80" /><asp:ImageButton ID="btn2" runat="server" AlternateText="删除" OnClick="btn2_Click"></asp:ImageButton>
                                        </div>
                                    </td>
                                    <td align="left" valign="middle">
                                        <div style="height: 120px;margin:10px;" id="div3" runat="server" visible="false">
                                            <asp:Image ID="img3" runat="server" Width="80" Height="80" /><asp:ImageButton ID="btn3" runat="server" AlternateText="删除" OnClick="btn3_Click"></asp:ImageButton>
                                        </div>
                                    </td>
                                    <td align="left">
                                        <div style="height: 120px;margin:10px;" id="div4" runat="server" visible="false">
                                            <asp:Image ID="img4" runat="server" Width="80" Height="80" /><asp:ImageButton ID="btn4" runat="server" AlternateText="删除" OnClick="btn4_Click"></asp:ImageButton>
                                        </div>
                                    </td>
                                    <td align="left">
                                        <div style="height: 35px; margin-left:0px; text-align: left;">
                                            <telerik:RadAsyncUpload ID="image" runat="server" LocalizationPath="~/Language" TemporaryFolder="~/Temp" TargetFolder="~/Upload" AllowedFileExtensions="bmp,gif,jpg,jpeg,png" MaxFileSize="10240000" MultipleFileSelection="Disabled" ManualUpload="false" MaxFileInputsCount="4" HideFileInput="true" OnClientFileUploaded="img_up" OnFileUploaded="image_FileUploaded"></telerik:RadAsyncUpload>
                                        </div>
                                    </td>
                                </tr>
                            </table>
                    <div style="height: 10px;">
                        <asp:ImageButton ID="ok" runat="server" AlternateText="保存" OnClick="ok_Click" class="btn btn-xm btn-default"></asp:ImageButton>
                        <asp:ImageButton ID="cancel" runat="server" AlternateText="取消" OnClick="cancel_Click" class="btn btn-xm btn-default"></asp:ImageButton>
                    </div>
                </div>

            </telerik:RadAjaxPanel>
        </div>
    </form>
    <script src="../Assets/javascripts/jquery.js"></script>
    <script src="../Assets/javascripts/common.js"></script>
    <style>
        html .RadSearchBox_Bootstrap .rsbInput {
            height: 32px;
            line-height: 32px;
        }

        html .RadUpload {
            width: 40%;
            text-align: center;
        }

        html .rwTable {
            height: 800px;
        }

        html .RadAjaxPanel {
            height: 800px;
        }
    </style>
</body>
</html>
