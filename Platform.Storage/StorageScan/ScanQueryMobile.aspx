<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ScanQueryMobile.aspx.cs" Inherits="ScanQueryMobile" %>

<%@ Register Src="~/Menu/MenuMobile.ascx" TagPrefix="homory" TagName="Menu" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <meta http-equiv="x-ua-compatible" content="ie=edge, chrome=1" />
    <meta name="viewport" content="width=device-width, initial-scale=1, minimum-scale=1, maximum-scale=1" />
    <title>物资管理 - 流通查询</title>

   
<script type="text/javascript" src="../js/jquery-1.7.2.min.js"></script>
<script type="text/javascript">
$(function(){
	$(".timeline").eq(0).animate({
		height:'600px'
	},3000);
});
</script>

<link rel="stylesheet" type="text/css" href="../css/liutong.css" />
</head>
<body>
    <form id="form" runat="server">
        <telerik:RadScriptManager ID="sm" runat="server"></telerik:RadScriptManager>
         <telerik:RadAjaxPanel ID="ap" runat="server">
      <div class="timeline">
	<div class="timeline-date">
         <telerik:RadTextBox ID="code" runat="server" MaxLength="12" Style="ime-mode: disabled;"></telerik:RadTextBox>
               
                <asp:ImageButton ID="query" runat="server" AlternateText="查询" OnClick="query_Click" />
		<ul>
			<h2 class="second" style="position: relative;">
				      物资名称：<asp:Label ID="name" runat="server"></asp:Label>当前库存：<asp:Label ID="number" runat="server"></asp:Label>
			</h2>
			<li>
				<h3>09.03<span>2013</span></h3>
				<dl class="right">
					<span>时间轴就要成功了！</span>
				</dl>
			</li>
			<li>
				<h3>08.15<span>2013</span></h3>
				<dl class="right">
					<span>为了时间轴奋斗吧！</span>
				</dl>
			</li>
		</ul>
	</div>
	
</div>

       
   
               
            </div>
            
            <div class="grid-100 mobile-grid-100 grid-parent">
                 <table class="table table-bordered">
                     <tr>
                   
                    </tr>
                    <tr>
                        <td>日期</td>
                        <td>人员</td>
                        <td>操作</td>
                        <td>数量</td>
                        <td>单位</td>
                        <td>库存</td>
                    </tr>
                <telerik:RadListView ID="view" runat="server" OnNeedDataSource="view_NeedDataSource">
                    <ItemTemplate>
                           <tr>
                                <td><%# Eval("日期") %>
                                </td>
                                 <td><%#Eval("人员") %></td>
                                <td><%#Eval("类型") %></td>

                                <td><%#Eval("数量") %></td>

                                <td><%#Eval("单位") %></td>

                                <td><%# Eval("库存") %></td>

                            </tr>
                    </ItemTemplate>
                </telerik:RadListView>
            </table>
            </div>
        </telerik:RadAjaxPanel>
    </form>
    <script src="../Assets/javascripts/jquery.js"></script>
    <script src="../Assets/javascripts/common.js"></script>
</body>
</html>
