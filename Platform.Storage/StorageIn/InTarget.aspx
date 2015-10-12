<%@ Page Language="C#" AutoEventWireup="true" CodeFile="InTarget.aspx.cs" Inherits="InTarget" %>

<%@ Register Src="~/Menu/Menu.ascx" TagPrefix="homory" TagName="Menu" %>
<%@ Register Src="~/StorageObject/ObjectImage.ascx" TagPrefix="homory" TagName="ObjectImage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <meta http-equiv="x-ua-compatible" content="ie=edge, chrome=1" />
    <meta name="viewport" content="width=device-width, initial-scale=1, minimum-scale=1, maximum-scale=1" />
    <title>物资管理 - 入库准备</title>
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
        <telerik:RadWindowManager ID="wm" runat="server" Modal="true" Behaviors="None" CenterIfModal="true" ShowContentDuringLoad="true" VisibleStatusbar="false" ReloadOnShow="true">
            <Windows>
                <telerik:RadWindow ID="w_responsible" runat="server"></telerik:RadWindow>
            </Windows>
        </telerik:RadWindowManager>
        <homory:Menu runat="server" ID="menu" />
        <telerik:RadAjaxPanel ID="ap" runat="server" CssClass="grid-container">
            
            <div class="grid-100 mobile-grid-100 grid-parent left">
                <h3>购置单</h3>
            </div>
            <div class="grid-100 mobile-grid-100 grid-parent">
                <table class="table table-bordered" style="margin-top: 10px;" align="center">
                    <thead>
                        <tr>
                            <th style="text-align:center;width:8%;">购置单号</th>
                            <th style="text-align:center;width:8%;">发票编号</th>
                            <th style="text-align:center;width:8%;">购置时间</th>
                           
                            <th style="text-align:center;width:8%;">采购来源</th>
                            <th style="text-align:center;width:8%;">使用对象</th>

                            <th style="text-align:center;width:8%;">应付金额</th>
                            <th style="text-align:center;width:8%;">实付金额</th>

                            <th style="text-align:center;width:8%;">保管人</th>
                            <th style="text-align:center;width:8%;">经手人</th>
                             <th style="text-align:center;width:20%;">清单简述</th>

                        </tr>
                    </thead>
                    <tbody>
                        <tr>
                            <td align="left">
                                <input id="target_id" runat="server" type="hidden" />
                                <asp:Label ID="target_number" runat="server"></asp:Label>
                            </td>
                            <td align="left">
                                <asp:Label ID="target_receipt" runat="server"></asp:Label>
                            </td>
                             <td align="left">
                                <asp:Label ID="target_day" runat="server"></asp:Label>

                            </td>
                            <td align="left">
                                <asp:Label ID="target_source" runat="server"></asp:Label>
                            </td>
                            <td align="left">
                                <asp:Label ID="target_target" runat="server"></asp:Label>
                            </td>
                            <td align="left">
                                <asp:Label ID="target_toPay" runat="server"></asp:Label>
                            </td>
                            <td align="left">
                                <asp:Label ID="target_paid" runat="server"></asp:Label>
                            </td>
                            <td align="left">
                                <asp:Label ID="target_keeper" runat="server"></asp:Label>
                            </td>
                            <td align="left">
                                <asp:Label ID="target_brokerage" runat="server"></asp:Label>
                            </td>
                           
                            <td align="left" id="target_content" runat="server"></td>
                        </tr>
                    </tbody>
                </table>
            </div>
            <div class="grid-100 mobile-grid-100 grid-parent left">
                <h3>入库记录</h3>
            </div>
            <div class="grid-100 mobile-grid-100 grid-parent">
                <table class="table table-bordered" style="margin-top: 10px;" align="center">
                    <thead>
                        <tr>
                            <th style="text-align:center;width:8%;">名称</th>
                            <th style="text-align:center;width:8%;">分类</th>
                            <th style="text-align:center;width:8%;">单位</th>
                            <th style="text-align:center;width:8%;">规格</th>
                            <th style="text-align:center;width:8%;">年龄段</th>

                            <th style="text-align:center;width:8%;">存放地</th>
                            <th style="text-align:center;width:8%;">数量</th>

                            <th style="text-align:center;width:8%;">单价</th>
                            <th style="text-align:center;width:8%;">优惠价</th>
                            <th style="text-align:center;width:8%;">总价</th>
                            <th style="text-align:center;width:8%;">日期</th>
                            <th style="text-align:center;width:8%;">备注</th>
                        </tr>
                    </thead>
                    <tbody>
                        <telerik:RadListView ID="ins" runat="server" OnNeedDataSource="ins_NeedDataSource">
                            <EmptyDataTemplate>
                                <div>暂无入库记录</div>
                            </EmptyDataTemplate>
                            <ItemTemplate>

                                <tr>
                                    <td align="left">

                                        <%# (Container.DataItem as Models.StorageIn).StorageObject.Name %>
                                    </td>
                                    <td align="left">
                                        <%# (Container.DataItem as Models.StorageIn).StorageObject.GeneratePath() %>
                                    </td>
                                    <td align="left">
                                        <%# (Container.DataItem as Models.StorageIn).StorageObject.Unit %>
                                    </td>
                                    <td align="left">
                                        <%# (Container.DataItem as Models.StorageIn).StorageObject.Specification %>
                                    </td>
                                    <td align="left">
                                        <%# Eval("Age") %>
                                    </td>
                                    <td align="left">
                                        <%# Eval("Place") %>
                                    </td>
                                    <td align="left"><%# Eval("Amount") %></td>
                                    <td align="left">
                                        <%# Eval("PerPrice").Money() %></td>
                                    <td align="left">
                                        <%# Eval("AdditionalFee").Money() %></td>
                                    <td align="left">
                                        <%# Eval("TotalMoney").Money() %></td>
                                    <td align="left">

                                        <%# ((int)Eval("TimeNode")).TimeNode() %></td>
                                    <td align="left">（<%# Eval("Note") %>）
                                    </td>
                                </tr>

                            </ItemTemplate>

                        </telerik:RadListView>

                    </tbody>
                </table>
            </div>
            <div class="grid-100 mobile-grid-100 grid-parent left">
                <h3>物品选择</h3>
            </div>
            <div class="grid-100 mobile-grid-100 grid-parent">
                <div>
                    <telerik:RadSearchBox ID="obj_search" runat="server" DataTextField="Name" DataValueField="Id" ShowSearchButton="false" OnDataSourceSelect="obj_search_DataSourceSelect" ShowLoadingIcon="false" OnSearch="obj_search_Search" Width="40%"></telerik:RadSearchBox>
                    <asp:ImageButton AlternateText="新增" ID="new_obj" runat="server" OnClick="new_obj_Click" />
                </div>
                <div>
                    <table class="table table-bordered" style="margin-top: 10px;" align="center">
                        <thead>
                            <tr>
                                <th>名称</th>
                                <th>分类</th>
                                <th>单位</th>
                                <th>规格</th>

                                <th>库存</th>
                                <th>图片</th>
                            </tr>
                        </thead>
                        <tbody>
                            <telerik:RadListView ID="obj_view" runat="server" OnNeedDataSource="obj_view_NeedDataSource">
                                <ItemTemplate>

                                    <tr>
                                        <td align="left">

                                            <telerik:RadButton ID="obj_set" AutoPostBack="true" ButtonType="ToggleButton" ToggleType="Radio" runat="server" Checked='<%# obj_id.Value == Eval("Id").ToString() %>' Text='<%# Eval("Name") %>' Value='<%# Eval("Id") %>' OnClick="obj_set_Click"></telerik:RadButton>
                                        </td>
                                        <td align="left">
                                            <%# (Container.DataItem as Models.StorageObject).GeneratePath() %>
                                        </td>

                                        <td align="left">
                                            <%# Eval("Unit") %>
                                        </td>
                                        <td align="left">
                                            <%# Eval("Specification") %>
                                        </td>
                                        <td align="left">
                                            <%# Eval("InAmount") %>
                                            <asp:Image AlternateText="低" Visible='<%# (decimal)Eval("Low") > 0 && (decimal)Eval("Low") > (decimal)Eval("InAmount") %>' ID="low" runat="server" />
                                            <asp:Image AlternateText="超" Visible='<%# (decimal)Eval("High") > 0 && (decimal)Eval("High") < (decimal)Eval("InAmount") %>' ID="high" runat="server" />
                                        </td>
                                        <td align="left">
                                            <homory:ObjectImage ID="ObjectImage" runat="server" ImageJson='<%# Eval("Image") %>' />
                                        </td>
                                    </tr>

                                </ItemTemplate>
                            </telerik:RadListView>
                        </tbody>
                    </table>
                    <input id="obj_id" runat="server" type="hidden" />
                </div>
            </div>
            <div class="grid-100 mobile-grid-100 grid-parent">
                <asp:ImageButton AlternateText="入库" ID="in" runat="server" OnClick="in_Click" class="btn btn-xm btn-default" />
            </div>
        </telerik:RadAjaxPanel>
    </form>
    <script src="../Assets/javascripts/jquery.js"></script>
    <script src="../Assets/javascripts/common.js"></script>
</body>
</html>
