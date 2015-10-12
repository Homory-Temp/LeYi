<%@ Page Language="C#" AutoEventWireup="true" CodeFile="StorageMobile.aspx.cs" Inherits="StorageMobile" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <meta http-equiv="x-ua-compatible" content="ie=edge, chrome=1" />
    <meta name="viewport" content="width=device-width, initial-scale=1, minimum-scale=1, maximum-scale=1" />
    <title>物资管理 - 仓库</title>
    
    <!--[if lt IE 9]>
        <script src="../Assets/javascripts/html5.js"></script>
    <![endif]-->
    <!--[if (gt IE 8) | (IEMobile)]><!-->
    <meta name="viewport" content="width=device-width, initial-scale=1.0, maximum-scale=1.0, user-scalable=0">
   

    <script type="text/javascript">

        //html root的字体计算应该放在最前面，这样计算就不会有误差了/
        var _htmlFontSize = (function () {
            var clientWidth = document.documentElement ? document.documentElement.clientWidth : document.body.clientWidth;
            if (clientWidth > 640) clientWidth = 640;
            document.documentElement.style.fontSize = clientWidth * 1 / 16 + "px";
            return clientWidth * 1 / 16;
        })();



    </script>
    <link href="../css/m.css" rel="stylesheet" type="text/css" />
<script src="../js/jquery-1.11.0.min.js" type="text/javascript"></script>
</head>
<body class="g_locale2052 mobiCol3" id="g_body" faiscomobi="true">
    <form id="form" runat="server">
        <telerik:RadScriptManager ID="sm" runat="server"></telerik:RadScriptManager>
        <telerik:RadCodeBlock ID="cb" runat="server">
            <script>
                function w_add() {
                    var w = window.radopen("../Storage/StorageAddPopup", "w_add");
                    w.maximize();
                    return false;
                }
                function w_edit(id) {
                    var w = window.radopen("../Storage/StorageEditPopup?Id=" + id, "w_edit");
                    w.maximize();
                    return false;
                }
                function w_remove(id) {
                    var w = window.radopen("../Storage/StorageRemovePopup?Id=" + id, "w_remove");
                    w.maximize();
                    return false;
                }
                function rebind() {
                    $find("<%= list.ClientID %>").rebind();
                }
            </script>
        </telerik:RadCodeBlock>
        <telerik:RadWindowManager ID="wm" runat="server" Modal="true" Behaviors="None" CenterIfModal="true" ShowContentDuringLoad="true" VisibleStatusbar="false" ReloadOnShow="true">
            <Windows>
                <telerik:RadWindow ID="w_add" runat="server"></telerik:RadWindow>
                <telerik:RadWindow ID="w_edit" runat="server"></telerik:RadWindow>
                <telerik:RadWindow ID="w_remove" runat="server"></telerik:RadWindow>
            </Windows>
        </telerik:RadWindowManager>
        <telerik:RadAjaxPanel ID="ap" runat="server">
            <div id="frame">

	<div id="top">

		<a id="logo" href="../Storage/StorageMobile"><img src="../images/home.png" align="top"></a>

		<a id="title">云物资管理系统</a>
         <span id="list"><a href="../login.aspx">
                            <img src="../images/goback.png" align="top"></a></span>

	</div>
	
	<section class="home clearfix">
	<ul class="icon-items clearfix">
		
      
       
          <telerik:RadListView ID="list" runat="server" DataKeyNames="Id" OnNeedDataSource="list_NeedDataSource">
                                                <ItemTemplate>
                                                 <li class="icon-mall">  
                                                        <a
                                                            class="cubeLink_a " id="cubeLink_a1_cubeNav313" href="<%# GenerateUrl(Eval("Id")) %>"
                                                            target="_self"><i></i><br /><%# Eval("Name") %>                                                              
                                                        </a>
                                                      <!--  <asp:ImageButton ID="edit" runat="server" AlternateText="编辑" CommandArgument='<%# Eval("Id") %>' OnClick="edit_Click" />
                                                        <asp:ImageButton ID="remove" runat="server" AlternateText="删除" CommandArgument='<%# Eval("Id") %>' OnClick="remove_Click" />-->
                                                    </li>

                                                </ItemTemplate>
                                            </telerik:RadListView>
                                         
                                                             <!--<asp:ImageButton ID="add" runat="server" AlternateText="新增仓库" OnClick="add_Click"  class="btn btn-xm btn-default"/>-->
	</ul>
</section>


                                      
                                                           
                                     
                                          
        </telerik:RadAjaxPanel>
    </form>
    <script src="../Assets/javascripts/jquery.js"></script>
    <script src="../Assets/javascripts/common.js"></script>
</body>
</html>
