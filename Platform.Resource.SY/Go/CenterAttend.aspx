<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CenterAttend.aspx.cs" Inherits="Go.GoCenterAttend" EnableEventValidation="false" %>

<%@ Import Namespace="System.Web.Configuration" %>

<%@ Register Src="~/Control/CommonTop.ascx" TagPrefix="homory" TagName="CommonTop" %>
<%@ Register Src="~/Control/CommonBottom.ascx" TagPrefix="homory" TagName="CommonBottom" %>
<%@ Register Src="~/Control/PersonalActionvideo.ascx" TagPrefix="homory" TagName="PersonalActionvideo" %>
<%@ Register Src="~/Control/CenterLeft.ascx" TagPrefix="homory" TagName="CenterLeft" %>
<%@ Register Src="~/Control/CenterRight.ascx" TagPrefix="homory" TagName="CenterRight" %>




<!DOCTYPE html>

<html>
<head runat="server">
    <title>我的关注- 个人中心-资源平台 </title>
    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8">
    <meta http-equiv="Pragma" content="no-cache">
    <script src="../Script/jquery.min.js"></script>
    <link rel="stylesheet" href="../Style/common.css">
    <link rel="stylesheet" href="../Style/common(1).css">
    <link rel="stylesheet" href="../Style/index1.css">
    <link rel="stylesheet" href="../Style/2.css" id="skinCss">
    <link href="../Style/public.css" rel="stylesheet" />
    <link href="../Style/mhzy.css" rel="stylesheet" />
    <link href="../Style/center.css" rel="stylesheet" />
        <base target="_top" />

</head>
<body class="srx-phome">
    <form runat="server">
        <telerik:RadScriptManager ID="Rsm" runat="server">
            <Scripts>
                <asp:ScriptReference Assembly="Telerik.Web.UI" Name="Telerik.Web.UI.Common.Core.js" />
                <asp:ScriptReference Assembly="Telerik.Web.UI" Name="Telerik.Web.UI.Common.jQuery.js" />
                <asp:ScriptReference Assembly="Telerik.Web.UI" Name="Telerik.Web.UI.Common.jQueryInclude.js" />
            </Scripts>
        </telerik:RadScriptManager>
        <homory:CommonTop runat="server" ID="CommonTop" />

        <div class="srx-bg">
            <div class="srx-wrap">

                <%--左上方个人信息区--%>
                <div class="srx-main srx-main-bg">
                    <homory:CenterLeft runat="server" ID="CenterLeft" />

                    <div class="srx-right">
                        <div class="srx-r1">
                            <div class="msgFeed user_feed mt15">
                                <div style="background-color: #FFF; margin-top: 8px;">


                                    <div id="tabA" class="tabControl" style="width: 575px; height: 280px; float: left; background-color: #FFF">

                                        <div class="box doing">
                                            <div style="width: 575px; margin: auto;">
                                                <div class="tabs">
                                                </div>


                                                <div class="tabClear"></div>
                                                <div class="tabContents" style="border-top: 2px solid #EFEFEF;">

                                                    <div class="tabContent">

                                                        <div style="clear: both;">
                                                            <h3>相互关注：</h3>
                                                            <br />
                                                            <telerik:RadAjaxPanel ID="up1" runat="server" OnAjaxRequest="ajax1">
                                                                <asp:Repeater ID="both" runat="server">
                                                                    <ItemTemplate>
                                                                        <div style="width: 150px; float: left; text-align: center;">
                                                                            <a href='<%# string.Format("../Go/Personal?Id={0}", Eval("Id")) %>'>
                                                                                <asp:Image runat="server" ImageUrl='<%# P(Eval("Icon")) %>' Width="100" Height="100" /></a></br>
                                            <a href='<%# string.Format("../Go/Personal?Id={0}", Eval("Id")) %>'><%# Eval("DisplayName") %></a><br />
                                                                            <a data-id='<%# Eval("Id") %>' id="removeAttend" target="_self" runat="server" onserverclick="removeAttend1_ServerClick">取消关注</a>
                                                                        </div>
                                                                    </ItemTemplate>
                                                                </asp:Repeater>
                                                            </telerik:RadAjaxPanel>
                                                        </div>
                                                        <p style="clear: both;">&nbsp;</p>
                                                        <p style="clear: both;">&nbsp;</p>
                                                        <div style="clear: both;">
                                                            <h3>我关注的：</h3>
                                                            <br />
                                                            <telerik:RadAjaxPanel ID="up2" runat="server" OnAjaxRequest="ajax2">
                                                                <asp:Repeater ID="positive" runat="server">
                                                                    <ItemTemplate>
                                                                        <div style="width: 150px; float: left; text-align: center;">
                                                                            <a href='<%# string.Format("../Go/Personal?Id={0}", Eval("Id")) %>'>
                                                                                <asp:Image runat="server" ImageUrl='<%# P(Eval("Icon")) %>' Width="100" Height="100" /></a></br>
                                            <a href='<%# string.Format("../Go/Personal?Id={0}", Eval("Id")) %>'><%# Eval("DisplayName") %></a><br />
                                                                            <a data-id='<%# Eval("Id") %>' id="removeAttend" target="_self" runat="server" onserverclick="removeAttend2_ServerClick">取消关注</a>
                                                                        </div>
                                                                    </ItemTemplate>
                                                                </asp:Repeater>
                                                            </telerik:RadAjaxPanel>
                                                        </div>
                                                        <p style="clear: both;">&nbsp;</p>
                                                        <p style="clear: both;">&nbsp;</p>
                                                        <div style="clear: both;">
                                                            <h3>关注我的</h3>
                                                            <br />
                                                            <telerik:RadAjaxPanel ID="up3" runat="server" OnAjaxRequest="ajax3">
                                                                <asp:Repeater ID="negative" runat="server">
                                                                    <ItemTemplate>
                                                                        <div style="width: 150px; float: left; text-align: center;">
                                                                            <a href='<%# string.Format("../Go/Personal?Id={0}", Eval("Id")) %>'>
                                                                                <asp:Image runat="server" ImageUrl='<%# P(Eval("Icon")) %>' Width="100" Height="100" /></a></br>
                                            <a href='<%# string.Format("../Go/Personal?Id={0}", Eval("Id")) %>'><%# Eval("DisplayName") %></a><br />
                                                                            <a data-id='<%# Eval("Id") %>' id="addAttend" target="_self" runat="server" onserverclick="addAttend_ServerClick">添加关注</a>
                                                                        </div>
                                                                    </ItemTemplate>
                                                                </asp:Repeater>
                                                            </telerik:RadAjaxPanel>
                                                        </div>


                                                    </div>



                                                </div>

                                            </div>
                                        </div>
                                    </div>
                                </div>

                                <script src="../Script/index.js"></script>




                            </div>

                        </div>

                        <homory:CenterRight runat="server" ID="CenterRight" />
                    </div>
                </div>
                <homory:CommonBottom runat="server" ID="CommonBottom" />



            </div>
        </div>
    </form>
</body>
</html>













