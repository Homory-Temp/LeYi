<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Group.aspx.cs" Inherits="Go.GoGroup" %>

<%@ Register Src="~/Control/HomeTop.ascx" TagPrefix="homory" TagName="HomeTop" %>
<%@ Register Src="~/Control/CommonBottom.ascx" TagPrefix="homory" TagName="CommonBottom" %>
<%@ Register Src="~/Control/CommonTop.ascx" TagPrefix="homory" TagName="CommonTop" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8">
    <meta http-equiv="Pragma" content="no-cache">
    <meta name="Description" content="">
    <meta name="Keywords">
    <title>团队教研首页</title>
    <link  rel="stylesheet"  href="css/common.css">
<link  rel="stylesheet"  href="css/common(1).css">
    <base target="_top" />

<link  rel="stylesheet"  href="css/plaza1.css">


<link  rel="stylesheet"  href="css/summary.css">
</head>
<body class="srx-pschl">
    <form runat="server">
        <telerik:RadScriptManager ID="Rsm" runat="server">
            <Scripts>
                <asp:ScriptReference Assembly="Telerik.Web.UI" Name="Telerik.Web.UI.Common.Core.js" />
                <asp:ScriptReference Assembly="Telerik.Web.UI" Name="Telerik.Web.UI.Common.jQuery.js" />
                <asp:ScriptReference Assembly="Telerik.Web.UI" Name="Telerik.Web.UI.Common.jQueryInclude.js" />
            </Scripts>
        </telerik:RadScriptManager>
        <div class="srx-bg">
            <div class="srx-wrap">
                <homory:CommonTop runat="server" ID="CommonTop" />
				<telerik:RadAjaxPanel runat="server">
                <div class="srx-main">
                    <div class="srx-left" style="background-color: #FFF">
                        <h3 class="sch_title">看看他们都在讨论些什么有趣的事儿！</h3>
                        <div class="pdtb10 pdlr10 mb10 clearfix">
                            <span class="jy_search clearfix">
	                            <input type="text" id="keyword" runat="server" name="keyword" class="sear_txt" value=""/>
                                <a id="queryG" runat="server" style="margin: 20px;;color: white;font-size: 16px;line-height: 32px;" OnServerClick="queryG_OnServerClick">搜索</a>
                            </span>
                            <span class="gray8 qkey ml50">

                           



                            </span>
                        </div>

                        <div class="c-lb-boarder">
                            <div class="c-lb-bank skin-bg-color10">
                                <div class="xy_sort">
                                    <ul>

                                        <li>
                                            <span style="width: 70px">学&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;科：</span>

											<asp:Repeater runat="server" ID="course">
												<ItemTemplate>
													<a id="xk" runat="server" data-id='<%# Eval("Id") %>' OnServerClick="xk_OnServerClick" href="../Go/Catalog.aspx"><%# Eval("Name") %></a>
												</ItemTemplate>
											</asp:Repeater>


                                        </li>


                                        <li>
                                            <span style="width: 70px">年&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;级：</span>


<asp:Repeater runat="server" ID="grade">
												<ItemTemplate>
													<a id="nj" runat="server" data-id='<%# Eval("Id") %>' OnServerClick="nj_OnServerClick" href="../Go/Catalog.aspx"><%# Eval("Name") %></a>
												</ItemTemplate>
											</asp:Repeater>



                                        </li>


                                     

                                    </ul>
                                </div>
                            </div>
                        </div>
                        <div style="margin-top: 15px">
                            <div class="them-sort-bar sb-sele1 clearfix mt15">
                               

                                <a class="fr btn-join" style="margin-right: 36px;" data-action="thematic" href="../Go/CenterGroup.aspx">我要创建团队</a>

                            </div>
                        </div>
                        <div class="sch_list">
                            <div id="schCol1" class="sch-col">
	                            <asp:Repeater runat="server" ID="col1">
		                            <ItemTemplate>
                                <div class="schbox">
                                    <div class="schinfo">

                                        <div class="schnameimg clearfix">
                                            <a>
	                                            <asp:Image Width="45" Height="45" runat="server" ImageUrl='<%# Eval("Icon") %>'/>
                                            </a>
                                        </div>
                                        <strong class="schname clearfix" style="width: 270px;">
                                            <a href='<%# string.Format("../Go/ViewGroup?Id={0}", Eval("Id")) %>'><%# Eval("Name") %>&nbsp;（<%# Eval("Serial") %>）</a>
                                            <br />
                                            <div class="fd-tags">
                                                学科：<i><%# CatalogName(Eval("CourseId")) %></i><br/>
                                                年级：<i><%# CatalogName(Eval("GradeId")) %></i>
                                            </div>

                                        </strong>
                                        <div class="schtxt">
                                            <div class="content">
                                               <%# Eval("Introduction") %>
                                            </div>
                                        </div>
                                     <div class="from" style="margin-bottom: 25px;width: 270px;">
                                        <em>创建人：</em>
                                        <a href='<%# string.Format("../Go/Personal?Id={0}", PPP(Eval("Id"))) %>'><%# UUU(Eval("Id")) %></a>
										 <a id="joinG" visible='<%# !NoJoin(Guid.Parse(PPP(Eval("Id"))), (Guid)Eval("Id")) %>' data-id='<%# Eval("Id") %>' style="float:right;margin:auto; padding: 0px 16px; color: white;line-height: 25px; cursor:pointer;background-color:#227DC5;" runat="server" onserverclick="joinG_ServerClick">加入</a>
                                    </div>
                                   </div>
                                </div>
		                            </ItemTemplate>
	                            </asp:Repeater>
                            </div>
                            <div id="schCol2" class="sch-col">
	                            <asp:Repeater runat="server" ID="col2">
		                            <ItemTemplate>
                                <div class="schbox">
                                    <div class="schinfo">

                                        <div class="schnameimg clearfix">
                                            <a>
	                                            <asp:Image Width="45" Height="45" runat="server" ImageUrl='<%# Eval("Icon") %>'/>
                                            </a>
                                        </div>
                                        <strong class="schname clearfix" style="width: 270px;">
                                            <a href='<%# string.Format("../Go/ViewGroup?Id={0}", Eval("Id")) %>'><%# Eval("Name") %>&nbsp;（<%# Eval("Serial") %>）</a>
                                            <br />
                                            <div class="fd-tags">
                                                所属学科：
                                            <i><%# CatalogName(Eval("CourseId")) %></i>
                                                <i class="ml15"><%# CatalogName(Eval("GradeId")) %></i>
                                            </div>

                                        </strong>
                                        <div class="schtxt">
                                            <div class="content">
                                               <%# Eval("Introduction") %>
                                            </div>
                                        </div>
                                     <div class="from" style="margin-bottom: 25px;width: 270px;">
                                        <em>创建人：</em>
                                        <a href='<%# string.Format("../Go/Personal?Id={0}", PPP(Eval("Id"))) %>'><%# UUU(Eval("Id")) %></a>
										 <a id="joinG" visible='<%# !NoJoin(Guid.Parse(PPP(Eval("Id"))), (Guid)Eval("Id")) %>' data-id='<%# Eval("Id") %>' style="float:right;margin:auto; padding: 0px 16px; color: white;line-height: 25px; cursor:pointer;background-color:#227DC5;" runat="server" onserverclick="joinG_ServerClick">加入</a>
                                    </div>
                                   </div>
                                </div>
		                            </ItemTemplate>
	                            </asp:Repeater>
                            </div>
                            <div id="schCol3" class="sch-col">
	                            <asp:Repeater runat="server" ID="col3">
		                            <ItemTemplate>
                                <div class="schbox">
                                    <div class="schinfo">

                                        <div class="schnameimg clearfix">
                                            <a>
	                                            <asp:Image Width="45" Height="45" runat="server" ImageUrl='<%# Eval("Icon") %>'/>
                                            </a>
                                        </div>
                                        <strong class="schname clearfix" style="width: 270px;">
                                            <a href='<%# string.Format("../Go/ViewGroup?Id={0}", Eval("Id")) %>'><%# Eval("Name") %>&nbsp;（<%# Eval("Serial") %>）</a>
                                            <br />
                                            <div class="fd-tags">
                                                所属学科：
                                            <i><%# CatalogName(Eval("CourseId")) %></i>
                                                <i class="ml15"><%# CatalogName(Eval("GradeId")) %></i>
                                            </div>

                                        </strong>
                                        <div class="schtxt">
                                            <div class="content">
                                               <%# Eval("Introduction") %>
                                            </div>
                                        </div>
                                     <div class="from" style="margin-bottom: 25px;width: 270px;">
                                        <em>创建人：</em>
                                        <a href='<%# string.Format("../Go/Personal?Id={0}", PPP(Eval("Id"))) %>'><%# UUU(Eval("Id")) %></a>
										 <a id="joinG" visible='<%# !NoJoin(Guid.Parse(PPP(Eval("Id"))), (Guid)Eval("Id")) %>' data-id='<%# Eval("Id") %>' style="float:right;margin:auto; padding: 0px 16px; color: white;line-height: 25px; cursor:pointer;background-color:#227DC5;" runat="server" onserverclick="joinG_ServerClick">加入</a>
                                    </div>
                                   </div>
                                </div>
		                            </ItemTemplate>
	                            </asp:Repeater>
                            </div>
                        </div>
                    </div>



                </div>
				</telerik:RadAjaxPanel>
                <homory:CommonBottom runat="server" ID="CommonBottom" />
            </div>
        </div>
        <script src="css/h.js" type="text/javascript"></script>
    </form>
</body>
</html>
