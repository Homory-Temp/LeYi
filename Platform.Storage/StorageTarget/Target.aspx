<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Target.aspx.cs" Inherits="Target" %>

<%@ Register Src="~/Menu/Menu.ascx" TagPrefix="homory" TagName="Menu" %>
<%@ Register Src="~/StorageObject/ObjectImage.ascx" TagPrefix="homory" TagName="ObjectImage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <meta http-equiv="x-ua-compatible" content="ie=edge, chrome=1" />
    <meta name="viewport" content="width=device-width, initial-scale=1, minimum-scale=1, maximum-scale=1" />
    <title>物资管理 - 待入库单</title>
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
        <telerik:RadCodeBlock ID="cb" runat="server">
            <script>
                function w_edit(id, sid) {
                    var w = window.radopen("../StorageTarget/TargetEditPopup?Id=" + id + "&StorageId=" + sid, "w_edit");
                    w.maximize();
                    return false;
                }
                function w_remove(id) {
                    var w = window.radopen("../StorageTarget/TargetRemovePopup?Id=" + id, "w_remove");
                    w.maximize();
                    return false;
                }
                function rebind() {
                    $find("<%= list.ClientID %>").rebind();
                }
            </script>
        </telerik:RadCodeBlock>
        <telerik:RadWindowManager ID="wm" runat="server" Modal="true" Behaviors="None" CenterIfModal="true" ShowContentDuringLoad="true" VisibleStatusbar="false"  VisibleTitlebar="false" ReloadOnShow="true">
            <Windows>
                <telerik:RadWindow ID="w_edit" runat="server"></telerik:RadWindow>
                <telerik:RadWindow ID="w_remove" runat="server"></telerik:RadWindow>
            </Windows>
        </telerik:RadWindowManager>
        <homory:Menu runat="server" ID="menu" />
        <div class="am-cf am-padding" style="border-bottom: 1px solid #E1E1E1;">
            <div class="am-fl am-cf">
                <strong class="am-text-primary am-text-lg">待入库单</strong> / 
                <asp:ImageButton ID="add" runat="server" AlternateText="新入库单" OnClick="add_Click" />
            </div>
        </div>
        <telerik:RadAjaxPanel ID="ap" runat="server" CssClass="grid-container">
            <div class="grid-100 mobile-grid-100 grid-parent">
                <table class="table table-bordered" style="margin-top: 10px;" align="center">
                    <thead>
                        <tr>
                            <th  style="text-align:center;width:8%;">购置单号</th>
                            <th style="text-align:center;width:8%;">发票编号</th>
                            <th style="text-align:center;width:8%;">购置时间</th>
                            <th style="text-align:center;width:8%;">采购来源</th>
                            <th style="text-align:center;width:8%;">使用对象</th>
                            
                            <th style="text-align:center;width:8%;">应付</th>
                            <th style="text-align:center;width:8%;">实付</th>
                            <th style="text-align:center;width:8%;">保管人</th>
                            <th style="text-align:center;width:8%;">经手人</th>
                            
                            <th style="text-align:center;width:20%;">清单简述</th>
                            <th style="text-align:center;width:8%;">操作</th>
                        </tr>
                    </thead>
                    <tbody>
                        <telerik:RadListView ID="list" runat="server" DataKeyNames="Id" OnNeedDataSource="list_NeedDataSource">
                            <ItemTemplate>
                                <tr>
                                    <td align="left">
                                      <a target="_blank" href='<%# "../StorageTarget/TargetIn?StorageId={0}&TargetId={1}".Formatted(StorageId, Eval("Id")) %>'><%# Eval("Number") %></a></td>
                                    <td align="left">
                                        <asp:Label runat="server" Text='<%# Eval("ReceiptNumber") %>' Visible='<%# !Eval("ReceiptNumber").ToString().Null() %>'></asp:Label>
                                        <telerik:RadTextBox ID="receipt" runat="server" Visible='<%# Eval("ReceiptNumber").ToString().Null() %>'></telerik:RadTextBox>
                                    </td>
                                    <td align="left">
                                        <%# Eval("OrderSource") %></td>
                                    <td align="left">
                                        <%# Eval("UsageTarget") %></td>
                                    <td align="left">
                                        <%# ((int)Eval("TimeNode")).TimeNode() %>
                                    </td>
                                    <td align="left">
                                        <%# Eval("ToPay").Money() %></td>
                                    <td align="left">
                                        <%# Eval("Paid").Money() %>
                                    </td>
                                    <td align="left">
                                        <%# db.Value.UserName(Eval("KeepUserId")) %>
                                    </td>
                                    <td align="left">
                                        <%# db.Value.UserName(Eval("BrokerageUserId")) %>
                                    </td>
                                    
                                    <td align="left">
                                        <%# Eval("Content") %>
                                    </td>
                                    <td align="left">
                                        <div runat="server" visible='<%# Eval("ReceiptNumber").ToString().Null() %>'>
                                            <telerik:RadButton ID="confirm" runat="server" Checked="false" Text="确认发票编号填写正确" AutoPostBack="true" ButtonType="ToggleButton" ToggleType="CheckBox"></telerik:RadButton>
                                        </div>
                                     <asp:ImageButton AlternateText="入库" ID="in" runat="server" CommandArgument='<%# Eval("Id") %>' OnClick="in_Click"  class="btn btn-xs btn-default" />
                                <asp:ImageButton AlternateText="办结" ID="done" runat="server" CommandArgument='<%# Eval("Id") %>' OnClick="done_Click"  class="btn btn-xs btn-default" />
                                <asp:ImageButton AlternateText="保存发票" ID="save" runat="server" Visible='<%# Eval("ReceiptNumber").ToString().Null() %>' CommandArgument='<%# Eval("Id") %>' OnClick="save_Click"  class="btn btn-xm btn-default" />
                                        <asp:ImageButton AlternateText="编辑" ID="edit" runat="server" CommandArgument='<%# Eval("Id") %>' OnClick="edit_Click" />
                                <asp:ImageButton AlternateText="删除" ID="del" runat="server" Visible='<%# (Container.DataItem as Models.StorageTarget).StorageIn.Count == 0 %>' CommandArgument='<%# Eval("Id") %>' OnClick="remove_Click" />

                                    </td>
                                </tr>

                            </ItemTemplate>
                        </telerik:RadListView>
                    </tbody>
                </table>
            </div>
        </telerik:RadAjaxPanel>
    </form>
    <script src="../Assets/javascripts/jquery.js"></script>
    <script src="../Assets/javascripts/common.js"></script>
</body>
</html>
