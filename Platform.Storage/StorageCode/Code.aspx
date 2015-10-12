<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Code.aspx.cs" Inherits="Code" %>

<%@ Register Src="~/Menu/Menu.ascx" TagPrefix="homory" TagName="Menu" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <meta http-equiv="x-ua-compatible" content="ie=edge, chrome=1" />
    <meta name="viewport" content="width=device-width, initial-scale=1, minimum-scale=1, maximum-scale=1" />
    <title>物资管理 - 物资条码</title>
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
    <script src="../Assets/javascripts/jquery.qrcode.js"></script>
    <script>
        function preview() {
            bdhtml = window.document.body.innerHTML;
            sprnstr = "<!-- Start Printing -->";
            eprnstr = "<!-- End Printing -->";
            prnhtml = bdhtml.substring(bdhtml.indexOf(sprnstr) + 23);

            prnhtml = prnhtml.substring(0, prnhtml.indexOf(eprnstr));
            window.document.body.innerHTML = "<body>" + prnhtml + "</body>";
            window.print();
            window.document.body.innerHTML = bdhtml;
        }
    </script>
</head>
<body>
    <form id="form" runat="server">
        <telerik:RadScriptManager ID="sm" runat="server"></telerik:RadScriptManager>
        <homory:Menu runat="server" ID="menu" />
        <telerik:RadAjaxPanel ID="ap" runat="server" CssClass="grid-container">
            <div class="grid-25 mobile-grid-100 grid-parent left">
                <asp:RadioButtonList ID="rb" runat="server"  OnSelectedIndexChanged="rb_SelectedIndexChanged" RepeatDirection="Horizontal" AutoPostBack="true">
                    <asp:ListItem Text="全部&nbsp;&nbsp;&nbsp;&nbsp;" Value="0" Selected="True"></asp:ListItem>
                    <asp:ListItem Text="固定资产&nbsp;&nbsp;&nbsp;&nbsp;" Value="1"></asp:ListItem>
                    <asp:ListItem Text="准固定资产" Value="2"></asp:ListItem>
                </asp:RadioButtonList>
                <br />
                <telerik:RadTreeView ID="tree" runat="server" DataTextField="Name" DataValueField="Id" DataFieldID="Id" DataFieldParentID="ParentId" CheckBoxes="true" CheckChildNodes="false" OnNodeClick="tree_NodeClick" OnNodeCheck="tree_NodeCheck" OnNodeDataBound="tree_NodeDataBound">
                </telerik:RadTreeView>
            </div>
            <div class="grid-75 mobile-grid-100 grid-parent">
                <div class="grid-100 mobile-grid-100 grid-parent">
                    <asp:ImageButton ID="printx" runat="server" AlternateText="生成并下载" OnClick="print_Click" />
                </div>
                <div class="grid-100 mobile-grid-100 grid-parent" style="font-family: 'Microsoft YaHei UI', SimHei;">
                    <!-- Start Printing -->
                    <telerik:RadListView ID="view" runat="server" OnNeedDataSource="view_NeedDataSource">
                        <ItemTemplate>
                            <asp:Panel runat="server" Visible='<%# (Container.DataItem as Models.QRC).Obj != null && (Container.DataItem as Models.QRC).Obj.Single %>'>
                                <div style="float: left; margin: 5px; border: solid 1px #2B2B2B;">
                                    <div style="width: 410px; height: 46px; line-height: 46px; font-size: 21px; vertical-align: middle; font-weight: bold;">
                                        <img src="Logo.png" style="height: 40px;" /> 乐翼教育云物资 <%# ((Container.DataItem as Models.QRC).Obj.Fixed ? "固定资产标签" : "准固定资产标签") %>
                                    </div>
                                    <div style="width: 240px; height: 180px; float: left; text-align: left; font-size: 16px; padding-left: 20px;">
                                        <div style="margin-top: 20px;">
                                            <label style="font-weight: normal;">物资名称</label>：<%# Eval("Name") %>
                                        </div>
                                        <div style="margin-top: 0;">
                                            <label style="font-weight: normal;">规格型号</label>：<%# "{0}".Formatted((Container.DataItem as Models.QRC).Obj.Specification) %>
                                        </div>
                                        <div style="margin-top: 0;">
                                            <label style="font-weight: normal;">物资编号</label>：<%# (Container.DataItem as Models.QRC).Obj.Consumable ? "（低值易耗）" : ((Container.DataItem as Models.QRC).Fixed.Null() ? ((Container.DataItem as Models.QRC).Obj.Fixed ? (Container.DataItem as Models.QRC).Name.Split(new char[] { '-' })[(Container.DataItem as Models.QRC).Name.Split(new char[] { '-' }).Length-1] : "（低值非易耗）") : (Container.DataItem as Models.QRC).Fixed) %>
                                        </div>
                                        <div style="margin-top: 0;">
                                            <label style="font-weight: normal;">存放地　</label>：<%# (Container.DataItem as Models.QRC).Obj.Single ? P((Container.DataItem as Models.QRC)) : "" %>
                                        </div>
                                        <div style="margin-top: 0;">
                                            <label style="font-weight: normal;">责任人　</label>：<%# (Container.DataItem as Models.QRC).Obj.Single ? U((Container.DataItem as Models.QRC).Id) : "" %>
                                        </div>
                                    </div>
                                    <div style="width: 150px; height: 180px; float: left; text-align: center;">
                                        <telerik:RadBarcode runat="server" Type="QRCode" Text='<%# ConvertCode((string)Eval("Type"), (int)Eval("AutoId")) %>' OutputType="EmbeddedPNG" Width="150">
                                            <QRCodeSettings Mode="Alphanumeric" ErrorCorrectionLevel="M" ECI="None" Version="0" AutoIncreaseVersion="true" DotSize="5" />
                                        </telerik:RadBarcode>
                                        <%# ConvertCode((string)Eval("Type"), (int)Eval("AutoId")) %>
                                    </div>
                                </div>
                            </asp:Panel>
                            <asp:Panel runat="server" Visible='<%# (Container.DataItem as Models.QRC).Obj != null && !(Container.DataItem as Models.QRC).Obj.Single %>'>
                                <div style="float: left; margin: 5px; border: solid 1px #2B2B2B;">
                                    <div style="width: 410px; height: 46px; line-height: 46px; font-size: 21px; vertical-align: middle; font-weight: bold;">
                                            <img src="Logo.png" style="height: 40px;" /> 乐翼教育云物资 物资标签
                                    </div>
                                    <div style="width: 240px; height: 180px; float: left; text-align: left; font-size: 16px; padding-left: 20px;">
                                        <div style="margin-top: 20px;">
                                            <label style="font-weight: normal;">物资名称</label>：<%# Eval("Name") %>
                                        </div>
                                        <div style="margin-top: 0;">
                                            <label style="font-weight: normal;">规格型号</label>：<%# "{0}".Formatted((Container.DataItem as Models.QRC).Obj.Specification) %>
                                        </div>
                                        <div style="margin-top: 0;">
                                            <label style="font-weight: normal;">物资分类</label>：<%# (Container.DataItem as Models.QRC).Obj.GeneratePath() %>
                                        </div>
                                    </div>
                                    <div style="width: 150px; height: 180px; float: left; text-align: center;">
                                        <telerik:RadBarcode runat="server" Type="QRCode" Text='<%# ConvertCode((string)Eval("Type"), (int)Eval("AutoId")) %>' OutputType="EmbeddedPNG" Width="150">
                                            <QRCodeSettings Mode="Alphanumeric" ErrorCorrectionLevel="M" ECI="None" Version="0" AutoIncreaseVersion="true" DotSize="5" />
                                        </telerik:RadBarcode>
                                        <%# ConvertCode((string)Eval("Type"), (int)Eval("AutoId")) %>
                                    </div>
                                </div>
                            </asp:Panel>
                        </ItemTemplate>
                    </telerik:RadListView>
                    <!-- End Printing -->
                </div>
            </div>
        </telerik:RadAjaxPanel>
    </form>
    <script src="../Assets/javascripts/jquery.js"></script>
    <script src="../Assets/javascripts/common.js"></script>
</body>
</html>
