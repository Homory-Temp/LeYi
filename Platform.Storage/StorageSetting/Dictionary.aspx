<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Dictionary.aspx.cs" Inherits="Dictionary" %>

<%@ Register Src="~/Menu/Menu.ascx" TagPrefix="homory" TagName="Menu" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <meta http-equiv="x-ua-compatible" content="ie=edge, chrome=1" />
    <meta name="viewport" content="width=device-width, initial-scale=1, minimum-scale=1, maximum-scale=1" />
    <title>物资管理 - 数据字典</title>
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
                function w_add(type) {
                    var w = window.radopen("../StorageSetting/DictionaryAddPopup?Type=" + type + "&StorageId=" + "<%= StorageId %>", "w_add");
                    w.maximize();
                    return false;
                }
                function w_remove(name, type) {
                    var w = window.radopen("../StorageSetting/DictionaryRemovePopup?Name=" + encodeURIComponent(name) + "&Type=" + type + "&StorageId=" + "<%= StorageId %>", "w_remove");
                    w.maximize();
                    return false;
                }
                function rebind() {
                    $find("<%= list.ClientID %>").rebind();
                }
            </script>
        </telerik:RadCodeBlock>
        <telerik:RadWindowManager ID="wm" runat="server" Modal="true" Behaviors="None"  VisibleTitlebar="false" CenterIfModal="true" ShowContentDuringLoad="true" VisibleStatusbar="false" ReloadOnShow="true">
            <Windows>
                <telerik:RadWindow ID="w_add" runat="server"></telerik:RadWindow>
                <telerik:RadWindow ID="w_remove" runat="server"></telerik:RadWindow>
            </Windows>
        </telerik:RadWindowManager>
        <homory:Menu runat="server" ID="menu" />
        <telerik:RadAjaxPanel ID="ap" runat="server" CssClass="grid-container">
                  <div class="grid-20 left" style="margin-top: 10px; border-right: 1px solid #cdcdcd; min-height: 500px;">
                <div>
                    数据字典类型：
                </div>
                <div>
                    <telerik:RadTreeView ID="tree" runat="server" OnNodeClick="tree_NodeClick">
                        <Nodes>
                            <telerik:RadTreeNode Text="单位" Value="1" Selected="true"></telerik:RadTreeNode>
                            <telerik:RadTreeNode Text="规格" Value="2"></telerik:RadTreeNode>
                            <telerik:RadTreeNode Text="采购来源" Value="3"></telerik:RadTreeNode>
                            <telerik:RadTreeNode Text="使用对象" Value="4"></telerik:RadTreeNode>
                            <telerik:RadTreeNode Text="年龄段" Value="5"></telerik:RadTreeNode>
                        </Nodes>
                    </telerik:RadTreeView>
                </div>
            </div>
            <div class="grid-75 mobile-grid-100 grid-parent">
                <div class="am-cf am-padding">
                    <div class="am-fl am-cf">
                        <strong class="am-text-primary am-text-lg">分类管理</strong>&nbsp;&nbsp;/
                            <asp:ImageButton ID="add" runat="server" AlternateText="新增" OnClick="add_Click"  class="btn btn-xs btn-info"/>
                    </div>
                </div>
         <table class="table table-bordered" style="margin-left:10px;"  align="center"> 
                    <thead>
                        <tr>
                        
                            <th>名称</th>
                         
                            <th>删除</th>
                        </tr>
                    </thead>
                    <tbody>
                <telerik:RadListView ID="list" runat="server" DataKeyNames="StorageId,Type,Name,Type" OnNeedDataSource="list_NeedDataSource">
                    <ItemTemplate>
                         <tr>
                                 
                                    <td align="left"><%# Eval("Name") %></td>
                           <td align="left">
                            <asp:ImageButton ID="remove" runat="server" AlternateText="删除" CommandArgument='<%# Eval("Name") %>' OnClick="remove_Click" />
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
