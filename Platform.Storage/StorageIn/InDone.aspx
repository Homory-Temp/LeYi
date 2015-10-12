<%@ Page Language="C#" AutoEventWireup="true" CodeFile="InDone.aspx.cs" Inherits="InDoing" %>

<%@ Register Src="~/Menu/Menu.ascx" TagPrefix="homory" TagName="Menu" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <meta http-equiv="x-ua-compatible" content="ie=edge, chrome=1" />
    <meta name="viewport" content="width=device-width, initial-scale=1, minimum-scale=1, maximum-scale=1" />
    <title>物资管理 - 入库完成</title>
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
        <homory:Menu runat="server" ID="menu" />
        <telerik:RadAjaxPanel ID="ap" runat="server" CssClass="grid-container">
  
            <div class="grid-100 mobile-grid-100 grid-parent left">
                <h3>物品</h3>
            </div>
            <div class="grid-100 mobile-grid-100 grid-parent">
                <table class="table table-bordered" style="margin-top: 10px;" align="center">
                    <thead>
                        <tr>
                            <th>名称</th>
                            <th>分类</th>
                            <th>单位</th>
                            <th>规格</th>
                             <th>物资编号</th>
                            <th>库存</th>

                        </tr>
                    </thead>
                    <tbody>
                        <tr>
                            <td align="left">
                                <input id="object_id" runat="server" type="hidden" />

                                <asp:Label ID="object_name" runat="server"></asp:Label>/         
                                <asp:Image AlternateText="固" ID="object_fixed" runat="server" />
                                <asp:Image AlternateText="易" ID="object_consumable" runat="server" />
                            </td>
                            <td align="left">
                                <asp:Label ID="object_catalog" runat="server"></asp:Label>
                            </td>
                            <td align="left">
                                <asp:Label ID="object_unit" runat="server"></asp:Label>
                            </td>
                            <td align="left">
                                <asp:Label ID="object_specification" runat="server"></asp:Label>
                            </td>

                            <td align="left" id="fixedArea" runat="server">
                                <asp:Label ID="object_fixed_serial" runat="server"></asp:Label>

                            </td>
                            <td align="left">
                                <asp:Label ID="object_inAmount" runat="server"></asp:Label>
                                <asp:Image AlternateText="低" ID="object_low" runat="server" />
                                <asp:Image AlternateText="超" ID="object_high" runat="server" />

                            </td>
                        </tr>
                    </tbody>
                </table>
            </div>
            <div class="grid-100 mobile-grid-100 grid-parent left">
                <h3>购置单</h3>
            </div>
            <div class="grid-100 mobile-grid-100 grid-parent">
                <table class="table table-bordered" style="margin-top: 10px;" align="center">
                    <thead>
                        <tr>
                            <th  style="text-align:center;width:8%;">购置单号</th>
                                        <th  style="text-align:center;width:8%;">发票编号</th>
                                           <th  style="text-align:center;width:8%;">购置时间</th>
                                        <th  style="text-align:center;width:8%;">采购来源</th>
                                        <th  style="text-align:center;width:8%;">使用对象</th>

                                        <th  style="text-align:center;width:8%;">应付金额</th>
                                        <th  style="text-align:center;width:8%;">实付金额</th>

                                        <th  style="text-align:center;width:8%;">保管人</th>

                                        <th  style="text-align:center;width:8%;">经手人</th>
                                          <th  style="text-align:center;width:20%;">清单简述</th>
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
                            <th>名称</th>
                            <th>分类</th>
                            <th>单位</th>
                            <th>规格</th>
                            <th>年龄段</th>

                            <th>存放地</th>
                            <th>数量</th>

                            <th>单价</th>
                            <th>优惠价</th>
                            <th>总价</th>
                            <th>日期</th>
                            <th>备注</th>
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
                                    <td align="left"><%# Eval("Note") %>
                                    </td>
                                </tr>

                            </ItemTemplate>
                        </telerik:RadListView>
                    </tbody>
                </table>
            </div>

             <div class="grid-100 mobile-grid-100 grid-parent">
                        <telerik:RadButton ID="confirm" runat="server" Checked="false" Text="办结本购置单" AutoPostBack="true" ButtonType="ToggleButton" ToggleType="CheckBox" OnCheckedChanged="confirm_CheckedChanged"></telerik:RadButton>
                <telerik:RadButton ID="print" runat="server" Checked="false" Text="打印" AutoPostBack="true" Visible="false" ButtonType="ToggleButton" ToggleType="CheckBox"></telerik:RadButton>



                </div>
            <div class="grid-100 mobile-grid-100 grid-parent" style="margin-top:5px;">

                <asp:ImageButton AlternateText="入库同类物品" ID="in_obj" runat="server" OnClick="in_obj_Click"  class="btn btn-xm btn-default"/>
                <asp:ImageButton AlternateText="入库本购置单" ID="in_tar" runat="server" OnClick="in_tar_Click" class="btn btn-xm btn-default" />
                <asp:ImageButton AlternateText="确定" ID="in_end" runat="server" OnClick="in_end_Click" class="btn btn-xm btn-default" />
             
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
        html .RadSearchBox_Bootstrap .rsbInput {
        z-index:-99;
        }
        .RadSearchBox .rsbInner{
              z-index:-99;
        }
    </style>
</body>
</html>
