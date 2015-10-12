<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Home.aspx.cs" Inherits="Home" %>

<%@ Register Src="~/Menu/Menu.ascx" TagPrefix="homory" TagName="Menu" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <meta http-equiv="x-ua-compatible" content="ie=edge, chrome=1" />
    <meta name="viewport" content="width=device-width, initial-scale=1, minimum-scale=1, maximum-scale=1" />
    <title>物资管理 - 仓库首页</title>
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
        <homory:Menu runat="server" ID="menu" />
        <telerik:RadAjaxPanel ID="ap" runat="server" CssClass="grid-container">

            <div class="grid-100 mobile-grid-100 grid-parent">
                仓库切换：<telerik:RadComboBox ID="switcher" runat="server" DataTextField="Name" DataValueField="Id" AutoPostBack="true" OnSelectedIndexChanged="switcher_SelectedIndexChanged"></telerik:RadComboBox>
            </div>
            <div class="grid-100 mobile-grid-100 grid-parent">
                <div style="height: 30px;text-align: left;">
                    <h3>待入库购置单（<asp:HyperLink ID="targetCount" runat="server"></asp:HyperLink>）</h3>
                </div>

                <table class="table table-bordered">
                    <thead>
                        <tr>
                            <th style="text-align:center;width:20%;">购置单号</th>
                            <th style="text-align:center;width:20%;">购置日期</th>
                            <th style="text-align:center;width:40%;">清单简述</th>
                            
                            <th style="text-align:center;width:20%;">操作</th>
                        </tr>
                    </thead>
                    <tbody>
                        <telerik:RadListView ID="target" runat="server" OnNeedDataSource="target_NeedDataSource">
                            <ItemTemplate>
                                <tr>
                                    <td align="left"><%# Eval("Number") %></td>
                                    
                                    <td align="left"><%# ((int)Eval("TimeNode")).TimeNode() %></td>
                                    <td align="left"><%# Eval("Content") %></td>
                                    <td align="left">
                                        <asp:ImageButton AlternateText="入库" ID="in" runat="server" Visible='<%# !Eval("ReceiptNumber").ToString().Null() %>' CommandArgument='<%# Eval("Id") %>' OnClick="in_Click" />
                                        <asp:ImageButton AlternateText="补填发票" ID="save" runat="server" Visible='<%# Eval("ReceiptNumber").ToString().Null() %>' CommandArgument='<%# Eval("Id") %>' OnClick="save_Click" /></td>
                                </tr>
                            </ItemTemplate>
                        </telerik:RadListView>
                    </tbody>
                </table>
            </div>


            <div class="grid-100 mobile-grid-100 grid-parent">
                <div style="height: 30px;text-align: left;">
                    <h3>库存预警</h3>
                </div>

                <table class="table table-bordered" >
                    <thead>
                        <tr>
                            <th>名称</th>
                            <th>库存</th>
                            <th>备注</th>

                        </tr>
                    </thead>
                    <tbody>
                        <telerik:RadListView ID="warn" runat="server" OnNeedDataSource="warn_NeedDataSource">
                            <ItemTemplate>
                                <tr>
                                    <td align="left"><%# Eval("Name") %></td>
                                    <td align="left"><%# Eval("InAmount") %></td>
                                    <td align="left">
                                        <asp:Image AlternateText="低" Visible='<%# (decimal)Eval("Low") > 0 && (decimal)Eval("Low") > (decimal)Eval("InAmount") %>' ID="low" runat="server" />
                                        <asp:Image AlternateText="超" Visible='<%# (decimal)Eval("High") > 0 && (decimal)Eval("High") < (decimal)Eval("InAmount") %>' ID="high" runat="server" />
                                    </td>
                                </tr>
                            </ItemTemplate>
                        </telerik:RadListView>
                    </tbody>
                </table>
            </div>

        
                <div class="grid-100 mobile-grid-100 grid-parent">
                    <div style="height: 15px; text-align: left;">
                        <h3>仓库信息</h3>
                        <div style="float: right;">
                            分类总数：<asp:Label ID="a" runat="server"></asp:Label>
                            物资种数：<asp:Label ID="b" runat="server"></asp:Label>
                            操作总数：<asp:Label ID="c" runat="server"></asp:Label>
                        </div>
                    </div>
                    <table class="table table-bordered">
                        <thead>
                            <tr>
                                <th>库存总数</th>
                                <th>库存总金额</th>
                                <th>领用总数</th>
                                <th>领用总金额</th>
                                <th>借用总数</th>
                                <th>借用总金额</th>
                                <th>报废总数</th>
                                <th>报废总金额</th>
                                <th>调整总数</th>
                                <th>调整总金额</th>

                            </tr>
                        </thead>
                        <tbody>
                            <tr>
                                <td align="left">
                                    <asp:Label ID="d1" runat="server"></asp:Label></td>
                                <td align="left">
                                    <asp:Label ID="d2" runat="server"></asp:Label></td>
                                <td align="left">
                                    <asp:Label ID="d3" runat="server"></asp:Label></td>
                                <td align="left">
                                    <asp:Label ID="d4" runat="server"></asp:Label></td>
                                <td align="left">
                                    <asp:Label ID="d5" runat="server"></asp:Label></td>
                                <td align="left">
                                    <asp:Label ID="d6" runat="server"></asp:Label></td>
                                <td align="left">
                                    <asp:Label ID="d7" runat="server"></asp:Label></td>
                                <td align="left">
                                    <asp:Label ID="d8" runat="server"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:Label ID="dx" runat="server"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:Label ID="d9" runat="server"></asp:Label>
                                </td>
                            </tr>
                    </table>
                    <div style="height: 30px; text-align: left;">
                        <h3>
                            <telerik:RadCodeBlock runat="server">
                                <%= "本月（" + DateTime.Today.Month.ToString() + "）" %>
                            </telerik:RadCodeBlock>
                        </h3>
                    </div>
                    <table class="table table-bordered">
                        <thead>
                            <tr>
                                <th>入库总数</th>
                                <th>入库总金额</th>
                                <th>领用总数</th>
                                <th>领用总金额</th>
                                <th>借用总数</th>
                                <th>借用总金额</th>
                                <th>报废总数</th>
                                <th>报废总金额</th>
                                <th>调整总数</th>
                                <th>调整总金额</th>

                            </tr>
                        </thead>
                        <tbody>

                            <tr>
                                <td align="left">
                                    <asp:Label ID="f1" runat="server"></asp:Label></td>
                                <td align="left">
                                    <asp:Label ID="f2" runat="server"></asp:Label></td>
                                <td align="left">
                                    <asp:Label ID="f3" runat="server"></asp:Label></td>
                                <td align="left">
                                    <asp:Label ID="f4" runat="server"></asp:Label></td>
                                <td align="left">
                                    <asp:Label ID="f5" runat="server"></asp:Label></td>
                                <td align="left">
                                    <asp:Label ID="f6" runat="server"></asp:Label></td>
                                <td align="left">
                                    <asp:Label ID="f7" runat="server"></asp:Label></td>
                                <td align="left">
                                    <asp:Label ID="f8" runat="server"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:Label ID="fx" runat="server"></asp:Label>
                                <td align="left">
                                    <asp:Label ID="f9" runat="server"></asp:Label>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                    <div style="height: 30px; text-align: left;">
                        <h3>
                            <telerik:RadCodeBlock runat="server">
                                <%= "本年（" + DateTime.Today.Year.ToString() + "）" %>
                            </telerik:RadCodeBlock>
                        </h3>
                    </div>
                    <table class="table table-bordered">
                        <thead>
                            <tr>
                                <th>入库总数</th>
                                <th>入库总金额</th>
                                <th>领用总数</th>
                                <th>领用总金额</th>
                                <th>借用总数</th>
                                <th>借用总金额</th>
                                <th>报废总数</th>
                                <th>报废总金额</th>
                                <th>调整总数</th>
                                <th>调整总金额</th>

                            </tr>
                        </thead>
                        <tbody>

                            <tr>
                                <td align="left">
                                    <asp:Label ID="g1" runat="server"></asp:Label></td>
                                <td align="left">
                                    <asp:Label ID="g2" runat="server"></asp:Label></td>
                                <td align="left">
                                    <asp:Label ID="g3" runat="server"></asp:Label></td>
                                <td align="left">
                                    <asp:Label ID="g4" runat="server"></asp:Label></td>
                                <td align="left">
                                    <asp:Label ID="g5" runat="server"></asp:Label></td>
                                <td align="left">
                                    <asp:Label ID="g6" runat="server"></asp:Label></td>
                                <td align="left">
                                    <asp:Label ID="g7" runat="server"></asp:Label></td>
                                <td align="left">
                                    <asp:Label ID="g8" runat="server"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:Label ID="gx" runat="server"></asp:Label>
                                <td align="left">
                                    <asp:Label ID="g9" runat="server"></asp:Label>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </div>
              <div class="grid-100 mobile-grid-100 grid-parent">
                <div style="height: 30px;text-align: left;">
                    <h3>
                        <div id="outConfirm" runat="server">
                            报废审核（<asp:HyperLink ID="outCount" runat="server"></asp:HyperLink>）
                        </div>
                    </h3>
                </div>

                <table class="table table-bordered" style="margin-left: 10px;">
                    <thead>
                        <tr>
                            <th>时间</th>
                            <th>分类</th>
                            <th>物资</th>
                            <th>报废人</th>
                            <th>报废类型</th>
                            <th>报废原因</th>
                            <th>备注</th>
                        </tr>
                    </thead>
                    <tbody>
                        <telerik:RadListView ID="viewOut" runat="server" OnNeedDataSource="viewOut_NeedDataSource">
                            <ItemTemplate>
                                <tr>
                                    <td align="left">
                                        <%# ((DateTime)Eval("Time")).ToString("yyyy-MM-dd HH:mm") %></td>
                                    <td align="left">
                                        <%# db.Value.StorageObjectGetOne((Guid)Eval("ObjectId")).GeneratePath() %></td>
                                    <td align="left">
                                        <%# db.Value.StorageObjectGetOne((Guid)Eval("ObjectId")).Name %></td>
                                    <td align="left">
                                        <%# db.Value.UserName((Guid)Eval("OutUserId")) %></td>
                                    <td align="left">
                                        <%# Eval("OutType").ToString() %></td>
                                    <td align="left">
                                        <%# Eval("OutReason") %></td>
                                    <td align="left">
                                        <%# Eval("OutNote") %></td>
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
    <style type="text/css">
        th {
            text-align: center;
        }

        td {
            text-align: center;
        }
    </style>
</body>

</html>
