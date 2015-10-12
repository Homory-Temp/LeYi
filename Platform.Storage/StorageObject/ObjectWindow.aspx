<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ObjectWindow.aspx.cs" Inherits="ObjectWindow" %>

<%@ Register Src="~/StorageObject/ObjectImage.ascx" TagPrefix="homory" TagName="ObjectImage" %>

<%@ Register Src="~/StorageObject/ObjectImageOne.ascx" TagPrefix="homory" TagName="ObjectImageOne" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <meta http-equiv="x-ua-compatible" content="ie=edge, chrome=1" />
    <meta name="viewport" content="width=device-width, initial-scale=1, minimum-scale=1, maximum-scale=1" />
    <title>物资详情</title>
    <link href="../Assets/stylesheets/amazeui.min.css" rel="stylesheet">
    <link href="../Assets/stylesheets/admin.css" rel="stylesheet">
    <link href="../Assets/stylesheets/bootstrap.min.css" rel="stylesheet">
    <link href="../Assets/stylesheets/bootstrap-theme.min.css" rel="stylesheet">
    <link href="../css/css.css" rel="stylesheet">
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
        <telerik:RadCodeBlock ID="cb" runat="server">
            <script>
                function peek() {
                    var oWindow = null;
                    if (window.radWindow) oWindow = window.radWindow;
                    else if (window.frameElement.radWindow) oWindow = window.frameElement.radWindow;
                    return oWindow;
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
                peek().maximize();
            </script>
        </telerik:RadCodeBlock>
        <telerik:RadAjaxPanel ID="ap" runat="server">
            <div class="jl_peo_con">
                <div class="am-fl am-cf" style="margin: 30px;">
                    <strong class="am-text-primary am-text-lg">物资详情</strong>
                </div>
                <div class="jl_peo_l ptn">
                    <div class="jl_peo_top mb17">
                        <div class="jl_peo_photo">
                            <homory:ObjectImageOne ID="ObjectImage1" runat="server" ImageWidth="180" ImageHeight="300" />

                        </div>
                        <div class="jl_peo_infor">
                            <ul>
                                <div class="marg_b over">
                                    <div class="jl_peo_name">
                                        <asp:Label ID="name" runat="server"></asp:Label>
                                    </div>



                                    <div class="jl_peo_infor_li">
                                        <label>顺序号：</label><span>
                                            <asp:Label ID="ordinal" runat="server"></asp:Label></span>
                                    </div>
                                    <div class="jl_peo_infor_li">
                                        <label>物资编号：</label><span><asp:Label ID="code" runat="server"></asp:Label></span>
                                    </div>
                                    <div class="jl_peo_infor_li">
                                        <label>单位：</label><span>
                                            <asp:Label ID="unit" runat="server"></asp:Label></span>
                                    </div>
                                    <div class="jl_peo_infor_li" id="fixedArea" runat="server">
                                        <label>规格：</label><span><asp:Label ID="specification" runat="server"></asp:Label></span>
                                    </div>
                                    <div class="jl_peo_infor_li">
                                        <label>固定资产编号：</label><span>
                                            <asp:Label ID="fixedSerial" runat="server"></asp:Label></span>
                                    </div>
                                    <div class="jl_peo_infor_li">
                                        <label>下限预警：</label><span>
                                            <asp:Label ID="low" runat="server"></asp:Label></span>
                                    </div>
                                    <div class="jl_peo_infor_li">
                                        <label>上限预警：</label><span>
                                            <asp:Label ID="high" runat="server"></asp:Label></span>
                                    </div>


                                    <div class="clear"></div>
                                </div>
                        </div>
                    </div>
                   
                    <div class="jl_peo_mid">
                        <div class="jl_peo_title">
                            <strong class="am-text-primary am-text-lg">备注:</strong>
                        </div>
                        <div class=" jl_peo_midbox" style="text-align:left;">
                              <asp:Label ID="note" runat="server"></asp:Label>
                        </div>
                    </div>
                     <homory:ObjectImage runat="server" ID="ObjectImage" />
                  
                    <div class="jl_peo_mid" id="jl_peo_mid" runat="server">
                        <div class="jl_peo_title">
                            <strong class="am-text-primary am-text-lg">存放地</strong>
                        </div>
                        <div class=" jl_peo_midbox">
                            <telerik:RadGrid ID="grid" runat="server" CssClass="coreCenter" AutoGenerateColumns="false" LocalizationPath="../Language" AllowSorting="True" PageSize="20" GridLines="None" OnNeedDataSource="grid_NeedDataSource" OnBatchEditCommand="grid_BatchEditCommand">
                                <MasterTableView EditMode="Batch" DataKeyNames="Id,Fixed,Ordinal" CommandItemDisplay="Top" CommandItemSettings-ShowAddNewRecordButton="false" HorizontalAlign="NotSet" ShowHeader="true" ShowHeadersWhenNoRecords="true" NoMasterRecordsText="">
                                    <BatchEditingSettings EditType="Row" OpenEditingEvent="DblClick" />
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <Columns>
                                        <telerik:GridTemplateColumn HeaderText="编号" DataField="Ordinal" SortExpression="Ordinal" UniqueName="Ordinal" ReadOnly="true">
                                            <ItemTemplate>
                                                <asp:Label runat="server" Text='<%# Eval("Ordinal") %>'></asp:Label>
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridTemplateColumn HeaderText="存放地" DataField="Place" SortExpression="Place" UniqueName="Place">
                                            <ItemTemplate>
                                                <asp:Label runat="server" Text='<%# Eval("Place") %>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <telerik:RadTextBox ID="Place" runat="server" EnabledStyle-HorizontalAlign="Center" Text='<%# Bind("Place") %>'>
                                                </telerik:RadTextBox>
                                            </EditItemTemplate>
                                        </telerik:GridTemplateColumn>
                                    </Columns>
                                </MasterTableView>
                            </telerik:RadGrid>
                        </div>
                    </div>
                </div>
            </div>







            <div style="margin-top: 10px;">
                <asp:ImageButton ID="cancel" runat="server" AlternateText="关闭" OnClick="cancel_Click" class="btn btn-xm btn-default"></asp:ImageButton>
            </div>

        </telerik:RadAjaxPanel>
    </form>
    <script src="../Assets/javascripts/jquery.js"></script>
    <script src="../Assets/javascripts/common.js"></script>
</body>
</html>
