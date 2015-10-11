<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Policy.aspx.cs" Inherits="Go.GoPolicy" %>

<%@ Register Src="~/Control/SideBar.ascx" TagPrefix="homory" TagName="SideBar" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=Edge,Chrome=1" />
    <meta name="viewport" content="width=device-width,initial-scale=1,minimum-scale=1,maximum-scale=1" />
    <title>基础平台</title>
    <script src="../Content/jQuery/jquery.min.js"></script>
    <link href="../assets/css/bootstrap.min.css" rel="stylesheet" />
    <link href="../assets/css/style-responsive.css" rel="stylesheet" />
    <link href="../assets/css/style.css" rel="stylesheet" />
    <script src="../assets/js/bootstrap.min.js"></script>
    <link href="../Content/Semantic/css/semantic.min.css" rel="stylesheet" />
    <link href="../Content/Homory/css/common.css" rel="stylesheet" />
    <link href="../Content/Core/css/common.css" rel="stylesheet" />
    <script src="../Content/Semantic/javascript/semantic.min.js"></script>
    <script src="../Content/Homory/js/common.js"></script>
    <script src="../Content/Homory/js/notify.min.js"></script>
    <!--[if lt IE 9]>
	    <script src="../Content/Homory/js/html5shiv.js"></script>
	    <script src="../Content/Homory/js/respond.min.js"></script>
    <![endif]-->
</head>
<body>
    <form id="formHome" runat="server">
        <div>
            <homory:SideBar runat="server" ID="SideBar" />
        </div>
        <telerik:RadAjaxPanel ID="panel" runat="server" CssClass="container-fluid" Style="margin: 0; padding: 0;" LoadingPanelID="loading">
            <div class="row">
                <div class="col-md-12">
                    <div><i class="ui red circle icon"></i>是否开启用户自主注册</div>
                    <p>
                        <i class="ui icon"></i>
                        <telerik:RadButton ID="p1" runat="server" ButtonType="ToggleButton" ToggleType="CustomToggle" AutoPostBack="True" CssClass="ui mini button">
                            <ToggleStates>
                                <telerik:RadButtonToggleState Text="√ 开启" Value="True" CssClass="ui red mini button" />
                                <telerik:RadButtonToggleState Text="× 关闭" Value="False" CssClass="ui black mini button" />
                            </ToggleStates>
                        </telerik:RadButton>
                    </p>
                    <div><i class="ui red circle icon"></i>电子邮件正则表达式（com/cn结尾的电子邮件地址）</div>
                    <p>
                        <i class="ui icon"></i>
                        <input id="p2" runat="server" cssclass="ui input" style="width: 550px; height: 22px; text-align: center;" />
                    </p>
                    <div><i class="ui red circle icon"></i>手机号码正则表达式（13/14/15/18号段开头的11位手机号码）</div>
                    <p>
                        <i class="ui icon"></i>
                        <input id="p3" runat="server" cssclass="ui input" style="width: 200px; height: 22px; text-align: center;" />
                    </p>
                    <div><i class="ui red circle icon"></i>安全选项</div>
                    <p>
                        <i class="ui icon"></i>
                        <i class="ui purple circle icon"></i>
                        <label>用户注册密码最小长度：&nbsp;&nbsp;</label><input id="p4" runat="server" cssclass="ui input" style="width: 200px; height: 22px; text-align: center;" />
                    </p>
                    <p>
                        <i class="ui icon"></i>
                        <i class="ui purple circle icon"></i>
                        <label>用户登录凭证失效分钟：&nbsp;&nbsp;</label><input id="p5" runat="server" cssclass="ui input" style="width: 200px; height: 22px; text-align: center;" />
                    </p>
                    <div><i class="ui red circle icon"></i>系统SMTP邮件服务器</div>
                    <p>
                        <i class="ui icon"></i>
                        <i class="ui purple circle icon"></i>
                        <label>名称：&nbsp;&nbsp;</label><input id="p10" runat="server" cssclass="ui input" style="width: 200px; height: 22px; text-align: center;" />
                    </p>
                    <p>
                        <i class="ui icon"></i>
                        <i class="ui purple circle icon"></i>
                        <label>地址：&nbsp;&nbsp;</label><input id="p6" runat="server" cssclass="ui input" style="width: 200px; height: 22px; text-align: center;" />
                    </p>
                    <p>
                        <i class="ui icon"></i>
                        <i class="ui purple circle icon"></i>
                        <label>账号：&nbsp;&nbsp;</label><input id="p7" runat="server" cssclass="ui input" style="width: 200px; height: 22px; text-align: center;" />
                    </p>
                    <p>
                        <i class="ui icon"></i>
                        <i class="ui purple circle icon"></i>
                        <label>密码：&nbsp;&nbsp;</label><input id="p8" runat="server" cssclass="ui input" style="width: 200px; height: 22px; text-align: center;" />
                    </p>
                    <p>
                        <i class="ui icon"></i>
                        <i class="ui purple circle icon"></i>
                        <label>端口：&nbsp;&nbsp;</label><input id="p9" runat="server" cssclass="ui input" style="width: 200px; height: 22px; text-align: center;" />
                    </p>
                    <div><i class="ui red circle icon"></i>其他选项</div>
                    <p>
                        <i class="ui icon"></i>
                        <i class="ui purple circle icon"></i>
                        <label>教师调动保留原有兼职：&nbsp;&nbsp;</label>
                        <telerik:RadButton ID="p12" runat="server" ButtonType="ToggleButton" ToggleType="CustomToggle" AutoPostBack="True" CssClass="ui mini button">
                            <ToggleStates>
                                <telerik:RadButtonToggleState Text="√ 保留" Value="True" CssClass="ui red mini button" />
                                <telerik:RadButtonToggleState Text="× 删除" Value="False" CssClass="ui black mini button" />
                            </ToggleStates>
                        </telerik:RadButton>
                    </p>
                    <div class="ui divider"></div>
                    <p style="text-align: center; width: 100%;">
                        <asp:Button ID="save" runat="server" CssClass="ui teal small button" Text="保存更改" OnClick="save_OnClick"></asp:Button>
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
					<asp:Button ID="restore" runat="server" CssClass="ui purple small button" Text="还原默认" OnClick="restore_OnClick"></asp:Button>
                    </p>
                </div>
            </div>
        </telerik:RadAjaxPanel>
    </form>
</body>
</html>
