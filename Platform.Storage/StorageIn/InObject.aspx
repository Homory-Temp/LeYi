<%@ Page Language="C#" AutoEventWireup="true" CodeFile="InObject.aspx.cs" Inherits="InObject" %>

<%@ Register Src="~/Menu/Menu.ascx" TagPrefix="homory" TagName="Menu" %>

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
        <homory:Menu runat="server" ID="menu" />
        <telerik:RadAjaxPanel ID="ap" runat="server" CssClass="grid-container">

            <div class="grid-100 mobile-grid-100 grid-parent left">
                <h3>物品</h3>
            </div>
            <div class="grid-100 mobile-grid-100 grid-parent">
                <table class="table table-bordered" style="margin-top: 10px;" align="center">
                    <thead>
                        <tr>
                            <th width="15%" style="text-align:center;">名称</th>
                            <th width="15%" style="text-align:center;">分类</th>
                            <th width="15%" style="text-align:center;">单位</th>
                            <th width="15%" style="text-align:center;">规格</th>
                             <th width="15%" style="text-align:center;">物资编号</th>
                            <th width="15%" style="text-align:center;">库存</th>

                        </tr>
                    </thead>
                    <tbody>
                        <tr>
                            <td align="left" width="15%">
                                <input id="object_id" runat="server" type="hidden" />

                                <asp:Label ID="object_name" runat="server"></asp:Label>/         
                                <asp:Image AlternateText="固" ID="object_fixed" runat="server" />
                                <asp:Image AlternateText="易" ID="object_consumable" runat="server" />
                            </td>
                            <td align="left" width="15%">
                                <asp:Label ID="object_catalog" runat="server"></asp:Label>
                            </td>
                            <td align="left" width="15%">
                                <asp:Label ID="object_unit" runat="server"></asp:Label>
                            </td>
                            <td align="left" width="15%">
                                <asp:Label ID="object_specification" runat="server"></asp:Label>
                            </td>

                            <td align="left" id="fixedArea" runat="server" width="15%">
                                <asp:Label ID="object_fixed_serial" runat="server"></asp:Label>

                            </td>
                            <td align="left" width="15%">
                                <asp:Label ID="object_inAmount" runat="server"></asp:Label>
                                <asp:Image AlternateText="低" ID="object_low" runat="server" />
                                <asp:Image AlternateText="超" ID="object_high" runat="server" />

                            </td>
                        </tr>
                    </tbody>
                </table>
            </div>
            <div class="grid-100 mobile-grid-100 grid-parent left">
                <h3>购置单选择</h3>
            </div>
            <div class="grid-100 mobile-grid-100 grid-parent">
                <div>
                    购置单：<telerik:RadSearchBox ID="tar_search" runat="server" DataTextField="Number" DataValueField="Id" ShowSearchButton="false" OnDataSourceSelect="tar_search_DataSourceSelect" ShowLoadingIcon="false" OnSearch="tar_search_Search"></telerik:RadSearchBox>
                    <asp:ImageButton AlternateText="新增购置单" ID="new_tar" runat="server" OnClick="new_tar_Click" />
                </div>
                <div>


                    <telerik:RadListView ID="tar_view" runat="server" OnNeedDataSource="tar_view_NeedDataSource" ItemPlaceholderID="holder">
                        <LayoutTemplate>
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
                                        <asp:PlaceHolder ID="holder" runat="server"></asp:PlaceHolder>
                                </tbody>
                            </table>
                        </LayoutTemplate>
                        <ItemTemplate>
                                                              <tr>
  
                                        <td align="left">

                                            <telerik:RadButton ID="tar_set" AutoPostBack="true" ButtonType="ToggleButton" ToggleType="Radio" runat="server" Checked='<%# tar_id.Value == Eval("Id").ToString() %>' Text='<%# Eval("Number") %>' Value='<%# Eval("Id") %>' OnClick="tar_set_Click"></telerik:RadButton>
                                        </td>
                                        <td align="left" >
                                            <asp:Label runat="server" Text='<%# Eval("ReceiptNumber") %>' Visible='<%# !Eval("ReceiptNumber").ToString().Null() %>'></asp:Label>
                                            <telerik:RadTextBox ID="receipt" runat="server" Visible='<%# Eval("ReceiptNumber").ToString().Null() %>'></telerik:RadTextBox>
                                        </td>
                                                                    <td align="left" >
                                            <%# ((int)Eval("TimeNode")).TimeNode() %>
                                           
                                        </td>
                                        <td align="left" >
                                            <%# Eval("OrderSource") %>        </td>
                                        <td align="left" >
                                            <%# Eval("UsageTarget") %>
                                        </td>
                                       
                                        <td align="left">
                                            <%# Eval("ToPay").Money() %>        </td>
                                        <td align="left" >
                                            <%# Eval("Paid").Money() %>
                                        </td>
                                        <td align="left">
                                            <%# db.Value.UserName(Eval("KeepUserId")) %>        </td>
                                        <td align="left">
                                            <%# db.Value.UserName(Eval("BrokerageUserId")) %>
                                        </td>

                                      
                                                                   <td align="left" >
                                            <%# Eval("Content") %>
                                        </td>
                                                   </tr>
         </ItemTemplate>
                    </telerik:RadListView>
                    <input id="tar_id" runat="server" type="hidden" />

                </div>
            </div>
            <div class="grid-100 mobile-grid-100 grid-parent">
                <asp:ImageButton AlternateText="入库" ID="in" runat="server" OnClick="in_Click" class="btn btn-xm btn-default" />
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
