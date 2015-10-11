<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Role.aspx.cs" Inherits="Go.GoRole" %>

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
        <telerik:RadAjaxPanel ID="panel" runat="server" CssClass="ui center aligned stackable page grid" Style="margin: 0; padding: 0;" LoadingPanelID="loading">
            <div class="six wide column">
                <telerik:RadGrid ID="grid" runat="server" AutoGenerateColumns="false" AllowMultiRowSelection="False" LocalizationPath="../Language" AllowSorting="True" PageSize="20" GridLines="None" OnNeedDataSource="grid_NeedDataSource" OnBatchEditCommand="grid_BatchEditCommand" OnItemCreated="grid_ItemCreated" OnItemCommand="grid_OnItemCommand">
                    <MasterTableView EditMode="Batch" DataKeyNames="Id" CommandItemDisplay="Top" HorizontalAlign="NotSet" ShowHeader="true" ShowHeadersWhenNoRecords="true" NoMasterRecordsText="">
                        <BatchEditingSettings EditType="Row" OpenEditingEvent="DblClick" />
                        <HeaderStyle HorizontalAlign="Center" />
                        <Columns>
                            <telerik:GridTemplateColumn HeaderText="序号" DataField="Ordinal" SortExpression="Ordinal" UniqueName="Ordinal">
                                <ItemTemplate>
                                    <asp:Label runat="server" Text='<%# Eval("Ordinal") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <telerik:RadNumericTextBox ID="Ordinal" runat="server" EnabledStyle-HorizontalAlign="Center" Width="64" MinValue="1" MaxValue="999999" AllowOutOfRangeAutoCorrect="true" Value='<%# Bind("Ordinal") %>'>
                                        <NumberFormat DecimalDigits="0" AllowRounding="true" />
                                    </telerik:RadNumericTextBox>
                                </EditItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridTemplateColumn HeaderText="名称 *" DataField="Name" SortExpression="Name" UniqueName="Name">
                                <ItemTemplate>
                                    <asp:Label runat="server" Text='<%# Eval("Name") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <telerik:RadTextBox ID="Name" runat="server" EnabledStyle-HorizontalAlign="Center" Width="128" MaxLength="16" Text='<%# Bind("Name") %>'>
                                    </telerik:RadTextBox>
                                </EditItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridTemplateColumn HeaderText="状态" DataField="State" SortExpression="State" UniqueName="State">
                                <ItemTemplate>
                                    <asp:Label ID="stateLabel" runat="server" Text='<%# Eval("State") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <telerik:RadComboBox ID="State" runat="server" Enabled='<%# Eval("State") == null || Eval("State").ToString() != "内置" %>' Width="64" EnableTextSelection="true" Text='<%# Eval("State") %>'>
                                        <Items>
                                            <telerik:RadComboBoxItem Text="" Value="-1" />
                                            <telerik:RadComboBoxItem Text="启用" Value="1" />
                                            <telerik:RadComboBoxItem Text="停用" Value="4" />
                                            <telerik:RadComboBoxItem Text="删除" Value="5" />
                                        </Items>
                                    </telerik:RadComboBox>
                                </EditItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridButtonColumn Text="权限列表" CommandName="Select">
                            </telerik:GridButtonColumn>
                        </Columns>
                    </MasterTableView>
                    <SelectedItemStyle Font-Bold="true" />
                </telerik:RadGrid>
            </div>
            <div class="ten wide column">
                <asp:Panel ID="right" runat="server" CssClass="left aligned column">
                    <div>
                        权限范围：
						<telerik:RadButton ID="Global" runat="server" ButtonType="ToggleButton" ToggleType="CustomToggle" AutoPostBack="True" CssClass="ui mini button" Style="margin: 10px;" OnClick="Right_OnClick">
                            <ToggleStates>
                                <telerik:RadButtonToggleState Text="√ 全部学校" Value="True" CssClass="ui teal mini button" />
                                <telerik:RadButtonToggleState Text="× 所在学校" Value="False" CssClass="ui black mini button" />
                            </ToggleStates>
                        </telerik:RadButton>
                    </div>
                    <div>
                        组织管理：
						<telerik:RadButton ID="Department" runat="server" ButtonType="ToggleButton" ToggleType="CustomToggle" AutoPostBack="True" CssClass="ui mini button" Style="margin: 10px;" OnClick="Right_OnClick">
                            <ToggleStates>
                                <telerik:RadButtonToggleState Text="√ 部门管理" Value="True" CssClass="ui teal mini button" />
                                <telerik:RadButtonToggleState Text="× 部门管理" Value="False" CssClass="ui black mini button" />
                            </ToggleStates>
                        </telerik:RadButton>
                        <telerik:RadButton ID="Grade" runat="server" ButtonType="ToggleButton" ToggleType="CustomToggle" AutoPostBack="True" CssClass="ui mini button" Style="margin: 10px;" OnClick="Right_OnClick">
                            <ToggleStates>
                                <telerik:RadButtonToggleState Text="√ 年级管理" Value="True" CssClass="ui teal mini button" />
                                <telerik:RadButtonToggleState Text="× 年级管理" Value="False" CssClass="ui black mini button" />
                            </ToggleStates>
                        </telerik:RadButton>
                        <telerik:RadButton ID="Class" runat="server" ButtonType="ToggleButton" ToggleType="CustomToggle" AutoPostBack="True" CssClass="ui mini button" Style="margin: 10px;" OnClick="Right_OnClick">
                            <ToggleStates>
                                <telerik:RadButtonToggleState Text="√ 班级管理" Value="True" CssClass="ui teal mini button" />
                                <telerik:RadButtonToggleState Text="× 班级管理" Value="False" CssClass="ui black mini button" />
                            </ToggleStates>
                        </telerik:RadButton>
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
						<telerik:RadButton ID="MoveDepartment" runat="server" ButtonType="ToggleButton" ToggleType="CustomToggle" AutoPostBack="True" CssClass="ui mini button" Style="margin: 10px;" OnClick="Right_OnClick">
                            <ToggleStates>
                                <telerik:RadButtonToggleState Text="√ 组织调动" Value="True" CssClass="ui teal mini button" />
                                <telerik:RadButtonToggleState Text="× 组织调动" Value="False" CssClass="ui black mini button" />
                            </ToggleStates>
                        </telerik:RadButton>
                    </div>
                    <div>
                        用户管理：
						<telerik:RadButton ID="Teacher" runat="server" ButtonType="ToggleButton" ToggleType="CustomToggle" AutoPostBack="True" CssClass="ui mini button" Style="margin: 10px;" OnClick="Right_OnClick">
                            <ToggleStates>
                                <telerik:RadButtonToggleState Text="√ 教师管理" Value="True" CssClass="ui teal mini button" />
                                <telerik:RadButtonToggleState Text="× 教师管理" Value="False" CssClass="ui black mini button" />
                            </ToggleStates>
                        </telerik:RadButton>
                        <telerik:RadButton ID="Student" runat="server" ButtonType="ToggleButton" ToggleType="CustomToggle" AutoPostBack="True" CssClass="ui mini button" Style="margin: 10px;" OnClick="Right_OnClick">
                            <ToggleStates>
                                <telerik:RadButtonToggleState Text="√ 学生管理" Value="True" CssClass="ui teal mini button" />
                                <telerik:RadButtonToggleState Text="× 学生管理" Value="False" CssClass="ui black mini button" />
                            </ToggleStates>
                        </telerik:RadButton>
                        <telerik:RadButton ID="Registrator" runat="server" ButtonType="ToggleButton" ToggleType="CustomToggle" AutoPostBack="True" CssClass="ui mini button" Style="margin: 10px;" OnClick="Right_OnClick">
                            <ToggleStates>
                                <telerik:RadButtonToggleState Text="√ 注册用户" Value="True" CssClass="ui teal mini button" />
                                <telerik:RadButtonToggleState Text="× 注册用户" Value="False" CssClass="ui black mini button" />
                            </ToggleStates>
                        </telerik:RadButton>
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
						<telerik:RadButton ID="MoveUser" runat="server" ButtonType="ToggleButton" ToggleType="CustomToggle" AutoPostBack="True" CssClass="ui mini button" Style="margin: 10px;" OnClick="Right_OnClick">
                            <ToggleStates>
                                <telerik:RadButtonToggleState Text="√ 用户调动" Value="True" CssClass="ui teal mini button" />
                                <telerik:RadButtonToggleState Text="× 用户调动" Value="False" CssClass="ui black mini button" />
                            </ToggleStates>
                        </telerik:RadButton>
                    </div>
                    <div>
                        系统管理：
						<telerik:RadButton ID="Role" runat="server" ButtonType="ToggleButton" ToggleType="CustomToggle" AutoPostBack="True" CssClass="ui mini button" Style="margin: 10px;" OnClick="Right_OnClick">
                            <ToggleStates>
                                <telerik:RadButtonToggleState Text="√ 角色管理" Value="True" CssClass="ui teal mini button" />
                                <telerik:RadButtonToggleState Text="× 角色管理" Value="False" CssClass="ui black mini button" />
                            </ToggleStates>
                        </telerik:RadButton>
                        <telerik:RadButton ID="Authorize" runat="server" ButtonType="ToggleButton" ToggleType="CustomToggle" AutoPostBack="True" CssClass="ui mini button" Style="margin: 10px;" OnClick="Right_OnClick">
                            <ToggleStates>
                                <telerik:RadButtonToggleState Text="√ 角色分配" Value="True" CssClass="ui teal mini button" />
                                <telerik:RadButtonToggleState Text="× 角色分配" Value="False" CssClass="ui black mini button" />
                            </ToggleStates>
                        </telerik:RadButton>
                        <telerik:RadButton ID="Policy" runat="server" ButtonType="ToggleButton" ToggleType="CustomToggle" AutoPostBack="True" CssClass="ui mini button" Style="margin: 10px;" OnClick="Right_OnClick">
                            <ToggleStates>
                                <telerik:RadButtonToggleState Text="√ 策略配置" Value="True" CssClass="ui teal mini button" />
                                <telerik:RadButtonToggleState Text="× 策略配置" Value="False" CssClass="ui black mini button" />
                            </ToggleStates>
                        </telerik:RadButton>
                        <telerik:RadButton ID="Application" runat="server" ButtonType="ToggleButton" ToggleType="CustomToggle" AutoPostBack="True" CssClass="ui mini button" Style="margin: 10px;" OnClick="Right_OnClick">
                            <ToggleStates>
                                <telerik:RadButtonToggleState Text="√ 应用管理" Value="True" CssClass="ui teal mini button" />
                                <telerik:RadButtonToggleState Text="× 应用管理" Value="False" CssClass="ui black mini button" />
                            </ToggleStates>
                        </telerik:RadButton>
                        <telerik:RadButton ID="Api" runat="server" ButtonType="ToggleButton" ToggleType="CustomToggle" AutoPostBack="True" CssClass="ui mini button" Style="margin: 10px;" OnClick="Right_OnClick">
                            <ToggleStates>
                                <telerik:RadButtonToggleState Text="√ 接口授权" Value="True" CssClass="ui teal mini button" />
                                <telerik:RadButtonToggleState Text="× 接口授权" Value="False" CssClass="ui black mini button" />
                            </ToggleStates>
                        </telerik:RadButton>
                    </div>
                    <div>
                        课程管理：
						<telerik:RadButton ID="Course" runat="server" ButtonType="ToggleButton" ToggleType="CustomToggle" AutoPostBack="True" CssClass="ui mini button" Style="margin: 10px;" OnClick="Right_OnClick">
                            <ToggleStates>
                                <telerik:RadButtonToggleState Text="√ 课程设定" Value="True" CssClass="ui teal mini button" />
                                <telerik:RadButtonToggleState Text="× 课程设定" Value="False" CssClass="ui black mini button" />
                            </ToggleStates>
                        </telerik:RadButton>
                        <telerik:RadButton ID="CourseLearned" runat="server" ButtonType="ToggleButton" ToggleType="CustomToggle" AutoPostBack="True" CssClass="ui mini button" Style="margin: 10px;" OnClick="Right_OnClick">
                            <ToggleStates>
                                <telerik:RadButtonToggleState Text="√ 课程选择" Value="True" CssClass="ui teal mini button" />
                                <telerik:RadButtonToggleState Text="× 课程选择" Value="False" CssClass="ui black mini button" />
                            </ToggleStates>
                        </telerik:RadButton>
                        <telerik:RadButton ID="CourseTaught" runat="server" ButtonType="ToggleButton" ToggleType="CustomToggle" AutoPostBack="True" CssClass="ui mini button" Style="margin: 10px;" OnClick="Right_OnClick">
                            <ToggleStates>
                                <telerik:RadButtonToggleState Text="√ 教师授课" Value="True" CssClass="ui teal mini button" />
                                <telerik:RadButtonToggleState Text="× 教师授课" Value="False" CssClass="ui black mini button" />
                            </ToggleStates>
                        </telerik:RadButton>
                    </div>
                    <div>
                        团队管理：
						<telerik:RadButton ID="Group" runat="server" ButtonType="ToggleButton" ToggleType="CustomToggle" AutoPostBack="True" CssClass="ui mini button" Style="margin: 10px;" OnClick="Right_OnClick">
                            <ToggleStates>
                                <telerik:RadButtonToggleState Text="√ 名师团队" Value="True" CssClass="ui teal mini button" />
                                <telerik:RadButtonToggleState Text="× 名师团队" Value="False" CssClass="ui black mini button" />
                            </ToggleStates>
                        </telerik:RadButton>
                        <telerik:RadButton ID="Studio" runat="server" ButtonType="ToggleButton" ToggleType="CustomToggle" AutoPostBack="True" CssClass="ui mini button" Style="margin: 10px;" OnClick="Right_OnClick">
                            <ToggleStates>
                                <telerik:RadButtonToggleState Text="√ 教研团队" Value="True" CssClass="ui teal mini button" />
                                <telerik:RadButtonToggleState Text="× 教研团队" Value="False" CssClass="ui black mini button" />
                            </ToggleStates>
                        </telerik:RadButton>
                    </div>
                    <div>
                        栏目管理：
						<telerik:RadButton ID="Video" runat="server" ButtonType="ToggleButton" ToggleType="CustomToggle" AutoPostBack="True" CssClass="ui mini button" Style="margin: 10px;" OnClick="Right_OnClick">
                            <ToggleStates>
                                <telerik:RadButtonToggleState Text="√ 视频栏目" Value="True" CssClass="ui teal mini button" />
                                <telerik:RadButtonToggleState Text="× 视频栏目" Value="False" CssClass="ui black mini button" />
                            </ToggleStates>
                        </telerik:RadButton>
                        <telerik:RadButton ID="Article" runat="server" ButtonType="ToggleButton" ToggleType="CustomToggle" AutoPostBack="True" CssClass="ui mini button" Style="margin: 10px;" OnClick="Right_OnClick">
                            <ToggleStates>
                                <telerik:RadButtonToggleState Text="√ 文章栏目" Value="True" CssClass="ui teal mini button" />
                                <telerik:RadButtonToggleState Text="× 文章栏目" Value="False" CssClass="ui black mini button" />
                            </ToggleStates>
                        </telerik:RadButton>
                        <telerik:RadButton ID="Note" runat="server" ButtonType="ToggleButton" ToggleType="CustomToggle" AutoPostBack="True" CssClass="ui mini button" Style="margin: 10px;" OnClick="Right_OnClick">
                            <ToggleStates>
                                <telerik:RadButtonToggleState Text="√ 通知公告" Value="True" CssClass="ui teal mini button" />
                                <telerik:RadButtonToggleState Text="× 通知公告" Value="False" CssClass="ui black mini button" />
                            </ToggleStates>
                        </telerik:RadButton>
                    </div>
                    <div>
                        物资管理：
						<telerik:RadButton ID="StorageCreate" runat="server" ButtonType="ToggleButton" ToggleType="CustomToggle" AutoPostBack="True" CssClass="ui mini button" Style="margin: 10px;" OnClick="Right_OnClick">
                            <ToggleStates>
                                <telerik:RadButtonToggleState Text="√ 创建仓库" Value="True" CssClass="ui teal mini button" />
                                <telerik:RadButtonToggleState Text="× 创建仓库" Value="False" CssClass="ui black mini button" />
                            </ToggleStates>
                        </telerik:RadButton>
                    </div>
                    <div>
                        资源设定：
						<telerik:RadButton ID="Honor" runat="server" ButtonType="ToggleButton" ToggleType="CustomToggle" AutoPostBack="True" CssClass="ui mini button" Style="margin: 10px;" OnClick="Right_OnClick">
                            <ToggleStates>
                                <telerik:RadButtonToggleState Text="√ 荣誉分值" Value="True" CssClass="ui teal mini button" />
                                <telerik:RadButtonToggleState Text="× 荣誉分值" Value="False" CssClass="ui black mini button" />
                            </ToggleStates>
                        </telerik:RadButton>
                        <telerik:RadButton ID="Assess" runat="server" ButtonType="ToggleButton" ToggleType="CustomToggle" AutoPostBack="True" CssClass="ui mini button" Style="margin: 10px;" OnClick="Right_OnClick">
                            <ToggleStates>
                                <telerik:RadButtonToggleState Text="√ 课堂评估" Value="True" CssClass="ui teal mini button" />
                                <telerik:RadButtonToggleState Text="× 课堂评估" Value="False" CssClass="ui black mini button" />
                            </ToggleStates>
                        </telerik:RadButton>
                        <telerik:RadButton ID="Rooms" runat="server" ButtonType="ToggleButton" ToggleType="CustomToggle" AutoPostBack="True" CssClass="ui mini button" Style="margin: 10px;" OnClick="Right_OnClick">
                            <ToggleStates>
                                <telerik:RadButtonToggleState Text="√ 直播课堂" Value="True" CssClass="ui teal mini button" />
                                <telerik:RadButtonToggleState Text="× 直播课堂" Value="False" CssClass="ui black mini button" />
                            </ToggleStates>
                        </telerik:RadButton>
                        <telerik:RadButton ID="OtherPublish" runat="server" ButtonType="ToggleButton" ToggleType="CustomToggle" AutoPostBack="True" CssClass="ui mini button" Style="margin: 10px;" OnClick="Right_OnClick">
                            <ToggleStates>
                                <telerik:RadButtonToggleState Text="√ 资源代发" Value="True" CssClass="ui teal mini button" />
                                <telerik:RadButtonToggleState Text="× 资源代发" Value="False" CssClass="ui black mini button" />
                            </ToggleStates>
                        </telerik:RadButton>
                    </div>
                    <div>
                        查询审计：
						<telerik:RadButton ID="QueryTeacher" runat="server" ButtonType="ToggleButton" ToggleType="CustomToggle" AutoPostBack="True" CssClass="ui mini button" Style="margin: 10px;" OnClick="Right_OnClick">
                            <ToggleStates>
                                <telerik:RadButtonToggleState Text="√ 教师查询" Value="True" CssClass="ui teal mini button" />
                                <telerik:RadButtonToggleState Text="× 教师查询" Value="False" CssClass="ui black mini button" />
                            </ToggleStates>
                        </telerik:RadButton>
                        <telerik:RadButton ID="QueryStudent" runat="server" ButtonType="ToggleButton" ToggleType="CustomToggle" AutoPostBack="True" CssClass="ui mini button" Style="margin: 10px;" OnClick="Right_OnClick">
                            <ToggleStates>
                                <telerik:RadButtonToggleState Text="√ 学生查询" Value="True" CssClass="ui teal mini button" />
                                <telerik:RadButtonToggleState Text="× 学生查询" Value="False" CssClass="ui black mini button" />
                            </ToggleStates>
                        </telerik:RadButton>
                        <telerik:RadButton ID="QueryTaught" runat="server" ButtonType="ToggleButton" ToggleType="CustomToggle" AutoPostBack="True" CssClass="ui mini button" Style="margin: 10px;" OnClick="Right_OnClick">
                            <ToggleStates>
                                <telerik:RadButtonToggleState Text="√ 授课查询" Value="True" CssClass="ui teal mini button" />
                                <telerik:RadButtonToggleState Text="× 授课查询" Value="False" CssClass="ui black mini button" />
                            </ToggleStates>
                        </telerik:RadButton>
                        <telerik:RadButton ID="StatisticsOperation" runat="server" ButtonType="ToggleButton" ToggleType="CustomToggle" AutoPostBack="True" CssClass="ui mini button" Style="margin: 10px;" OnClick="Right_OnClick">
                            <ToggleStates>
                                <telerik:RadButtonToggleState Text="√ 操作审计" Value="True" CssClass="ui teal mini button" />
                                <telerik:RadButtonToggleState Text="× 操作审计" Value="False" CssClass="ui black mini button" />
                            </ToggleStates>
                        </telerik:RadButton>
                        <telerik:RadButton ID="StatisticsResource" runat="server" ButtonType="ToggleButton" ToggleType="CustomToggle" AutoPostBack="True" CssClass="ui mini button" Style="margin: 10px;" OnClick="Right_OnClick">
                            <ToggleStates>
                                <telerik:RadButtonToggleState Text="√ 资源审计" Value="True" CssClass="ui teal mini button" />
                                <telerik:RadButtonToggleState Text="× 资源审计" Value="False" CssClass="ui black mini button" />
                            </ToggleStates>
                        </telerik:RadButton>
                        <telerik:RadButton ID="StatisticsLogin" runat="server" ButtonType="ToggleButton" ToggleType="CustomToggle" AutoPostBack="True" CssClass="ui mini button" Style="margin: 10px;" OnClick="Right_OnClick">
                            <ToggleStates>
                                <telerik:RadButtonToggleState Text="√ 登录审计" Value="True" CssClass="ui teal mini button" />
                                <telerik:RadButtonToggleState Text="× 登录审计" Value="False" CssClass="ui black mini button" />
                            </ToggleStates>
                        </telerik:RadButton>
                    </div>
                    <input id="gid" runat="server" type="hidden" />
                </asp:Panel>
            </div>
        </telerik:RadAjaxPanel>
    </form>
</body>
</html>
