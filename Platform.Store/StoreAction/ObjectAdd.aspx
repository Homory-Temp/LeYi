<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ObjectAdd.aspx.cs" Inherits="StoreAction_ObjectAdd" %>

<%@ Register Src="~/Control/SideBarSingle.ascx" TagPrefix="homory" TagName="SideBarSingle" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=Edge,Chrome=1" />
    <meta name="viewport" content="width=device-width,initial-scale=1,minimum-scale=1,maximum-scale=1" />
    <script src="../Content/jQuery/jquery.min.js"></script>
    <link href="../assets/css/bootstrap.min.css" rel="stylesheet" />
    <link href="../assets/css/style-responsive.css" rel="stylesheet" />
    <link href="../assets/css/style.css" rel="stylesheet" />
    <link href="../Content/Core/css/common.css" rel="stylesheet" />
    <link href="../Content/Core/css/fix.css" rel="stylesheet" />
    <script src="../assets/js/bootstrap.min.js"></script>
    <script src="../Content/Homory/js/common.js"></script>
    <script src="../Content/Homory/js/notify.min.js"></script>
    <script>
        var t_day; var t_source; var t_usage;
    </script>
    <!--[if lt IE 9]>
	    <script src="../Content/Homory/js/html5shiv.js"></script>
	    <script src="../Content/Homory/js/respond.min.js"></script>
    <![endif]-->
</head>
<body>
    <form id="form" runat="server">
        <homory:SideBarSingle runat="server" ID="SideBarSingle" Crumb="物资管理 - 新增物资" />
        <telerik:RadCodeBlock ID="cb" runat="server">
            <script>
                function sh(sender, args) {
                    var c = sender.get_checked();
                    if (!c)
                        sender.set_checked(true);
                    var v = sender.get_value();
                    if ($find("<%= t1.ClientID %>"))
                        $find("<%= t1.ClientID %>").set_checked(v == "0");
                    if ($find("<%= t2.ClientID %>"))
                        $find("<%= t2.ClientID %>").set_checked(v == "1");
                    if ($find("<%= t3.ClientID %>"))
                        $find("<%= t3.ClientID %>").set_checked(v == "2");
                }

                function img_up(sender, e) {
                    $find("<%= ap.ClientID %>").ajaxRequest("Upload");
                }
            </script>
        </telerik:RadCodeBlock>
        <telerik:RadAjaxPanel ID="ap" runat="server" CssClass="container-fluid" LoadingPanelID="loading">
            <div class="row">
                <div class="col-md-2"></div>
                <div class="col-md-8">
                    <div class="row">
                        <div class="col-md-4 text-right">物资序号：</div>
                        <div class="col-md-8 text-left">
                            <telerik:RadNumericTextBox ID="ordinal" runat="server" Width="400" NumberFormat-DecimalDigits="0" MinValue="1"></telerik:RadNumericTextBox>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-4 text-right">物资名称：</div>
                        <div class="col-md-8 text-left">
                            <telerik:RadTextBox ID="name" runat="server" Width="400"></telerik:RadTextBox>
                        </div>
                    </div>
                    <div class="row" id="sp" runat="server">
                        <div class="col-md-4 text-right">物资编号：</div>
                        <div class="col-md-8 text-left">
                            <telerik:RadTextBox ID="code" runat="server" Width="400" EmptyMessage="用于该仓库内的物资与其他系统对接"></telerik:RadTextBox>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-4 text-right">物资类别：</div>
                        <div class="col-md-8 text-left">
                            <telerik:RadDropDownTree ID="tree" runat="server" Width="400" DataTextField="Name" DataValueField="Id" DataFieldID="Id" DataFieldParentID="ParentId" DropDownSettings-CloseDropDownOnSelection="true"></telerik:RadDropDownTree>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-4 text-right">单位：</div>
                        <div class="col-md-8 text-left">
                            <telerik:RadComboBox ID="unit" runat="server" LocalizationPath="~/Language" DataTextField="Name" DataValueField="Name" Filter="Contains" MarkFirstMatch="true" AppendDataBoundItems="true" ShowToggleImage="false" Width="400" AllowCustomText="true">
                                <Items>
                                    <telerik:RadComboBoxItem Text="" Value="" Selected="true" />
                                </Items>
                                <ItemTemplate>
                                    <%# Eval("Name") %><span style="display: none;"><%# Eval("PinYin") %></span>
                                </ItemTemplate>
                            </telerik:RadComboBox>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-4 text-right">规格：</div>
                        <div class="col-md-8 text-left">
                            <telerik:RadComboBox ID="specification" runat="server" LocalizationPath="~/Language" DataTextField="Name" DataValueField="Name" Filter="Contains" MarkFirstMatch="true" AppendDataBoundItems="true" ShowToggleImage="false" Width="400" AllowCustomText="true">
                                <Items>
                                    <telerik:RadComboBoxItem Text="" Value="" Selected="true" />
                                </Items>
                                <ItemTemplate>
                                    <%# Eval("Name") %><span style="display: none;"><%# Eval("PinYin") %></span>
                                </ItemTemplate>
                            </telerik:RadComboBox>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-4 text-right">物资属性：</div>
                        <div class="col-md-8 text-left">
                            <div>
                                <table>
                                    <tr id="r1" runat="server">
                                        <td>
                                            <telerik:RadButton ID="t1" runat="server" GroupName="c" Text="易耗品（领用）" AutoPostBack="false" ButtonType="ToggleButton" ToggleType="CheckBox" Value="0" OnClientClicked="sh"></telerik:RadButton>
                                        </td>
                                    </tr>
                                    <tr id="r2" runat="server">
                                        <td>
                                            <telerik:RadButton ID="t2" runat="server" GroupName="c" Text="低值非易耗（领用借用）" AutoPostBack="false" ButtonType="ToggleButton" ToggleType="CheckBox" Value="1" OnClientClicked="sh"></telerik:RadButton>
                                        </td>
                                    </tr>
                                    <tr id="r3" runat="server">
                                        <td>
                                            <telerik:RadButton ID="t3" runat="server" GroupName="c" Text="准固定资产（自动拆分）" AutoPostBack="false" ButtonType="ToggleButton" ToggleType="CheckBox" Value="2" OnClientClicked="sh"></telerik:RadButton>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-4 text-right">低库存量：</div>
                        <div class="col-md-8 text-left">
                            <telerik:RadNumericTextBox ID="low" runat="server" EmptyMessage="库存量低于该值时提醒，为0时不提醒" MinValue="0" NumberFormat-DecimalDigits="0" DataType="System.Decimal" AllowOutOfRangeAutoCorrect="true" Width="400"></telerik:RadNumericTextBox>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-4 text-right">高库存量：</div>
                        <div class="col-md-8 text-left">
                            <telerik:RadNumericTextBox ID="high" runat="server" EmptyMessage="库存量高于该值时提醒，为0时不提醒" MinValue="0" NumberFormat-DecimalDigits="0" DataType="System.Decimal" AllowOutOfRangeAutoCorrect="true" Width="400"></telerik:RadNumericTextBox>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-4 text-right">图片：</div>
                        <div class="col-md-8 text-left">
                            <div class="row">
                                <div class="col-md-12">
                                    <telerik:RadAsyncUpload ID="upload" runat="server" LocalizationPath="~/Language" TemporaryFolder="~/StoreUpload" TargetFolder="~/Common/物资/图片" AllowedFileExtensions="bmp,gif,jpg,jpeg,png" MaxFileSize="10240000" MultipleFileSelection="Disabled" ManualUpload="false" MaxFileInputsCount="4" HideFileInput="true" OnClientFileUploaded="img_up" OnFileUploaded="upload_FileUploaded"></telerik:RadAsyncUpload>
                                    <telerik:RadButton ID="clear" runat="server" Visible="false" Text="清除" Width="43" OnClick="clear_Click"></telerik:RadButton>
                                </div>
                            </div>
                            <div class="row" id="imgRow" runat="server" visible="false">
                                <div class="col-md-2 text-center">
                                    <img id="p0" runat="server" src="../Content/Images/Transparent.png" class="img-responsive image-thumb-result storeObjThumb" onclick="window.open(this.src, '_blank');" />
                                </div>
                                <div class="col-md-2 text-center">
                                    <img id="p1" runat="server" src="../Content/Images/Transparent.png" class="img-responsive image-thumb-result storeObjThumb" onclick="window.open(this.src, '_blank');" />
                                </div>
                                <div class="col-md-2 text-center">
                                    <img id="p2" runat="server" src="../Content/Images/Transparent.png" class="img-responsive image-thumb-result storeObjThumb" onclick="window.open(this.src, '_blank');" />
                                </div>
                                <div class="col-md-2 text-center">
                                    <img id="p3" runat="server" src="../Content/Images/Transparent.png" class="img-responsive image-thumb-result storeObjThumb" onclick="window.open(this.src, '_blank');" />
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-4 text-right">备注：</div>
                        <div class="col-md-8 text-left">
                            <telerik:RadTextBox ID="content" runat="server" Width="400"></telerik:RadTextBox>
                        </div>
                    </div>
                    <div class="row">&nbsp;</div>
                    <div class="row">
                        <div class="col-md-4 text-right">&nbsp;</div>
                        <div class="col-md-8 text-left">
                            <input type="button" class="btn btn-tumblr" id="go" runat="server" value="保存" onserverclick="go_ServerClick" />
                            &nbsp;&nbsp;&nbsp;&nbsp;
                            <input type="button" class="btn btn-tumblr" id="goon" runat="server" value="保存并继续" onserverclick="goon_ServerClick" />
                            &nbsp;&nbsp;&nbsp;&nbsp;
                            <input type="button" class="btn btn-tumblr" style="display: none;" id="in" runat="server" value="保存并入库" onserverclick="in_ServerClick" />
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
