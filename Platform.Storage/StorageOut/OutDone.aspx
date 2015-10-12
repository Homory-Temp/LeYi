<%@ Page Language="C#" AutoEventWireup="true" CodeFile="OutDone.aspx.cs" Inherits="OutDone" %>

<%@ Register Src="~/Menu/Menu.ascx" TagPrefix="homory" TagName="Menu" %>
<%@ Register Src="~/StorageObject/ObjectImage.ascx" TagPrefix="homory" TagName="ObjectImage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <meta http-equiv="x-ua-compatible" content="ie=edge, chrome=1" />
    <meta name="viewport" content="width=device-width, initial-scale=1, minimum-scale=1, maximum-scale=1" />
    <title>物资管理 - 报废结果</title>
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
    <script>
    </script>
</head>
<body>
    <form id="form" runat="server">
        <telerik:RadScriptManager ID="sm" runat="server"></telerik:RadScriptManager>
        <homory:menu runat="server" id="menu" />
        <telerik:RadAjaxPanel ID="ap" runat="server" CssClass="grid-container">
                  <div class="grid-100 mobile-grid-100 grid-parent left">
                <h3>报废结果查询</h3>
            </div>
            <div class="grid-100 mobile-grid-100 grid-parent">
                <div class="grid-100 mobile-grid-100 grid-parent">
                    <table   class="table table-bordered">
                        <tr>
                            <td>时间</td>
                            <td>分类</td>
                            <td>物资</td>
                            <td>报废人</td>
                            <td>报废申请人</td>
                            <td>报废类型</td>
                            <td>申请数量/编号</td>
                            <td>报废数/编号</td>
                            <td>报废原因</td>
                            <td>备注</td>
                            <td>审核人</td>
                        </tr>
                        <telerik:RadListView ID="viewM" runat="server" OnNeedDataSource="viewM_NeedDataSource" DataKeyNames="Id">
                            <ItemTemplate>
                                <tr>
                                    <td>
                                        <%# ((DateTime)Eval("Time")).ToString("yyyy-MM-dd HH:mm") %>
                                    </td>
                                    <td>
                                        <%# db.Value.StorageObjectGetOne((Guid)Eval("ObjectId")).GeneratePath() %>
                                    </td>
                                    <td>
                                        <%# db.Value.StorageObjectGetOne((Guid)Eval("ObjectId")).Name %>
                                    </td>
                                    <td>
                                        <%# db.Value.UserName((Guid)Eval("OutUserId")) %>
                                    </td>
                                    <td>
                                        <%# db.Value.UserName((Guid)Eval("OperatorId")) %>
                                    </td>
                                    <td>
                                        <%# Eval("OutType").ToString() %>
                                    </td>
                                    <td>
                                        <%# (decimal)Eval("OutAmount") %>
                                    </td>
                                    <td>
                                        <%# (decimal)Eval("OutDoneAmount") %>
                                    </td>
                                    <td>
                                        <%# Eval("OutReason") %>
                                    </td>
                                    <td>
                                        <%# Eval("OutNote") %>
                                    </td>
                                    <td>
                                        <%# db.Value.UserName((Guid)Eval("AuditUserId")) %>
                                    </td>
                                </tr>
                            </ItemTemplate>
                        </telerik:RadListView>
                        <telerik:RadListView ID="viewR" runat="server" OnNeedDataSource="viewR_NeedDataSource" DataKeyNames="Id">
                            <ItemTemplate>
                                <tr>
                                    <td>
                                        <%# ((DateTime)Eval("Time")).ToString("yyyy-MM-dd HH:mm") %>
                                    </td>
                                    <td>
                                        <%# db.Value.StorageObjectGetOne((Guid)Eval("ObjectId")).GeneratePath() %>
                                    </td>
                                    <td>
                                        <%# db.Value.StorageObjectGetOne((Guid)Eval("ObjectId")).Name %>
                                    </td>
                                    <td>
                                        <%# db.Value.UserName((Guid)Eval("OutUserId")) %>
                                    </td>
                                    <td>
                                        <%# db.Value.UserName((Guid)Eval("OperatorId")) %>
                                    </td>
                                    <td>
                                        <%# Eval("OutType").ToString() %>
                                    </td>
                                    <td>
                                        <%# (decimal)Eval("OutAmount") %>
                                    </td>
                                    <td>
                                        <%# (decimal)Eval("OutDoneAmount") %>
                                    </td>
                                    <td>
                                        <%# Eval("OutReason") %>
                                    </td>
                                    <td>
                                        <%# Eval("OutNote") %>
                                    </td>
                                    <td>
                                        <%# db.Value.UserName((Guid)Eval("AuditUserId")) %>
                                    </td>
                                </tr>
                            </ItemTemplate>
                        </telerik:RadListView>
                        <telerik:RadListView ID="viewS" runat="server" OnNeedDataSource="viewS_NeedDataSource" DataKeyNames="Id">
                            <ItemTemplate>
                                <tr>
                                    <td>
                                        <%# ((DateTime)Eval("Time")).ToString("yyyy-MM-dd HH:mm") %>
                                    </td>
                                    <td>
                                        <%# db.Value.StorageObjectGetOne((Guid)Eval("ObjectId")).GeneratePath() %>
                                    </td>
                                    <td>
                                        <%# db.Value.StorageObjectGetOne((Guid)Eval("ObjectId")).Name %>
                                    </td>
                                    <td>
                                        <%# db.Value.UserName((Guid)Eval("OutUserId")) %>
                                    </td>
                                    <td>
                                        <%# db.Value.UserName((Guid)Eval("OperatorId")) %>
                                    </td>
                                    <td>
                                        <%# Eval("OutType").ToString() %>
                                    </td>
                                    <td>
                                        <asp:Repeater ID="r" runat="server" DataSource='<%# Eval("OutOrdinals") %>'>
                                            <ItemTemplate>
                                                <%# Container.DataItem %>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </td>
                                    <td>
                                        <asp:Repeater ID="ra" runat="server" DataSource='<%# Eval("OutDoneOrdinals") %>'>
                                            <ItemTemplate>
                                                <%# Container.DataItem %>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </td>
                                    <td>
                                        <%# Eval("OutReason") %>
                                    </td>
                                    <td>
                                        <%# Eval("OutNote") %>
                                    </td>
                                    <td>
                                        <%# db.Value.UserName((Guid)Eval("AuditUserId")) %>
                                    </td>
                                </tr>
                            </ItemTemplate>
                        </telerik:RadListView>
                    </table>
                </div>
            </div>
        </telerik:RadAjaxPanel>
    </form>
    <script src="../Assets/javascripts/jquery.js"></script>
    <script src="../Assets/javascripts/common.js"></script>
</body>
</html>
