<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CenterResource.aspx.cs" Inherits="Go.GoCenterResource" EnableEventValidation="false" %>

<%@ Import Namespace="System.Web.Configuration" %>

<%@ Register Src="~/Control/CommonTop.ascx" TagPrefix="homory" TagName="CommonTop" %>
<%@ Register Src="~/Control/CommonBottom.ascx" TagPrefix="homory" TagName="CommonBottom" %>
<%@ Register Src="~/Control/PersonalActionvideo.ascx" TagPrefix="homory" TagName="PersonalActionvideo" %>
<%@ Register Src="~/Control/CenterLeft.ascx" TagPrefix="homory" TagName="CenterLeft" %>

<%@ Import Namespace="Homory.Model" %>
<!DOCTYPE html>


<html>
<head runat="server">
    <title>资源平台 - 个人中心</title>
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
    <script src="js/menu_min.js"></script>
    <telerik:RadCodeBlock runat="server">
    <script type="text/javascript">
        function menuclick() {

            $(".menu ul li").menu();

        }
        function checkChange(e) {

            var chk = $(e);

            var chk_children = chk.parent().children().find("input[type=checkbox]");

            chk_children.each(function () {

                var ev = $(this);

                ev.prop("checked", chk_children.first().is(":checked"));

            });

            var source_id_str = "";

            $("input[type='checkbox']").each(function () {
            
                if ($(this).is(":checked")) {
               
                    source_id_str += $(this).attr("source-id") + "|";

                }
            });
  
            $("#hidden_value").val(source_id_str);
        
            $find("<%= sa.ClientID %>").ajaxRequest(source_id_str);
        }
    </script>
    </telerik:RadCodeBlock>
    <link rel="stylesheet" type="text/css" href="css/menu-css.css">
    <link rel="stylesheet" type="text/css" href="css/style.css">
</head>
<body class="srx-phome">
    <form runat="server">
        <telerik:RadScriptManager ID="Rsm" runat="server">
        </telerik:RadScriptManager>
        <homory:CommonTop runat="server" ID="CommonTop" />

        <div class="srx-bg">
            <div class="srx-wrap">
                <%--左上方个人信息区--%>
                <div class="srx-main srx-main-bg">
                    <telerik:RadAjaxPanel ID="LEFT" runat="server" OnAjaxRequest="LEFT_AjaxRequest">
                        <div style="clear: both;"></div>
                        <homory:CenterLeft runat="server" ID="CenterLeftControl" />
                    </telerik:RadAjaxPanel>
                    <telerik:RadAjaxPanel runat="server">
                        <div class="xy_sort" style="margin: 15px;">
                            <table style="width: 750px; margin: 15px; float: right; line-height: 40px;">
                                <tr style="border-bottom: 1px dashed #eee;">
                                    <td style="width: 100px;">栏目选择：</td>
                                    <td colspan="6">
                                        <telerik:RadButton Width="80" runat="server" ID="VideoResource" OnClick="VideoResource_Click" Text="视频资源" Value='1' ToggleType="CheckBox" Checked="true" Style="margin-left: 10px; margin-right: 4px;"></telerik:RadButton>
                                        <telerik:RadButton Width="80" runat="server" ID="ArticleResource" OnClick="ArticleResource_Click" Text="文章资源" Value='2' ToggleType="CheckBox" Checked="true" Style="margin-left: 10px; margin-right: 4px;"></telerik:RadButton>
                                        <telerik:RadButton Width="80" runat="server" ID="CoursewareResource" OnClick="CoursewareResource_Click" Text="课件资源" Value='3' ToggleType="CheckBox" Checked="true" Style="margin-left: 10px; margin-right: 4px;"></telerik:RadButton>
                                    </td>
                                </tr>
                                <tr style="border-bottom: 1px dashed #eee;">
                                    <td>适用年龄段：</td>
                                    <td colspan="6">
                                        <telerik:RadButton ID="a1" runat="server" Text="通用" OnCheckedChanged="a_CheckedChanged" Value="A3757840-9DF7-4370-8151-FAD39B44EF6A" ToggleType="CheckBox" Width="60" Checked="true" Style="margin-right: 4px;"></telerik:RadButton>
                                        <telerik:RadButton ID="a2" runat="server" Text="大班" OnCheckedChanged="a_CheckedChanged" Value="625AE587-8C5A-454B-893C-08D2F6D187D5" ToggleType="CheckBox" Width="60" Checked="true" Style="margin-right: 4px;"></telerik:RadButton>
                                        <telerik:RadButton ID="a3" runat="server" Text="中班" OnCheckedChanged="a_CheckedChanged" Value="CF3AE587-8CB9-4D0A-B29A-08D2F6D187D9" ToggleType="CheckBox" Width="60" Checked="true" Style="margin-right: 4px;"></telerik:RadButton>
                                        <telerik:RadButton ID="a4" runat="server" Text="小班" OnCheckedChanged="a_CheckedChanged" Value="9FD9E587-8C09-4A55-9DB0-08D2F6D187DD" ToggleType="CheckBox" Width="60" Checked="true" Style="margin-right: 4px;"></telerik:RadButton>
                                        <telerik:RadButton ID="a5" runat="server" Text="托班" OnCheckedChanged="a_CheckedChanged" Value="850557E1-9EBD-4E0D-93DC-FE090A77D393" ToggleType="CheckBox" Width="60" Checked="true"></telerik:RadButton>
                                    </td>
                                </tr>
                                <tr style="border-bottom: 1px dashed #eee;">
                                    <td>日期：</td>
                                    <td style="width: 100px;">
                                        <telerik:RadMonthYearPicker ID="from" runat="server" LocalizationPath="~/Language" ShowPopupOnFocus="true" AutoPostBack="true" Width="90" DateInput-DisplayDateFormat="yyyy年MM月" DateInput-DateFormat="yyyy年MM月">
                                            <DatePopupButton runat="server" Visible="false" />
                                            <MonthYearNavigationSettings OkButtonCaption="确认" CancelButtonCaption="取消" TodayButtonCaption="今日" />
                                        </telerik:RadMonthYearPicker>
                                    </td>
                                    <td style="width: 15px;">-</td>
                                    <td align="left" style="width: 100px;">
                                        <telerik:RadMonthYearPicker ID="to" runat="server" LocalizationPath="~/Language" ShowPopupOnFocus="true" AutoPostBack="true" Width="90" DateInput-DisplayDateFormat="yyyy年MM月" DateInput-DateFormat="yyyy年MM月">
                                            <DatePopupButton runat="server" Visible="false" />
                                            <MonthYearNavigationSettings OkButtonCaption="确认" CancelButtonCaption="取消" TodayButtonCaption="今日" />
                                        </telerik:RadMonthYearPicker>
                                    </td>

                                    <td align="right" style="width: 50px;">内容：</td>
                                    <td align="left" style="width: 100px;">
                                        <telerik:RadTextBox ID="publisher" runat="server" Width="100"></telerik:RadTextBox>

                                    </td>
                                    <td>
                                        <asp:ImageButton runat="server" ID="Search" ImageUrl="~/Go/images/serch.png" Height="20" Style="margin-top: 15px; border: 1px solid #eee;" OnClick="Search_Click" />
                                    </td>
                                </tr>
                            </table>
                        </div>
                        </telerik:RadAjaxPanel>
                        <div class="srx-right">
                            <telerik:RadAjaxPanel runat="server" ID="st" OnAjaxRequest="st_AjaxRequest">
                            <div id="SourceLi" runat="server" class="fl mgt15" style="margin-left: 15px;">
                                <div>
                                    <div id="content" style="width: 220px;">
                                        <div class="menu">
                                            <h3>资源目录</h3>
                                            <telerik:RadListView runat="server" ID="secondList" OnNeedDataSource="secondList_NeedDataSource" Style="width: 300px;" ItemPlaceholderID="holder">
                                                <LayoutTemplate>
                                                    <ul>
                                                        <asp:PlaceHolder ID="holder" runat="server"></asp:PlaceHolder>
                                                    </ul>
                                                </LayoutTemplate>
                                                <ItemTemplate>
                                                    <li style="border-bottom: 1px dotted #d2d2d2;">
                                                        <div onclick="checkChange(this)" style="display: inline-block;">
                                                            <input type="checkbox" id="ischecked"  checked="checked" source-id='<%# Eval("Id") %>' />
                                                            <%--<asp:CheckBox  ID="cb" runat="server" Checked="true" class="chk_class"  AutoPostBack="true" OnCheckedChanged="cb_CheckedChanged"  Value='<%# Eval("Id")%>'/>--%>
                                                        </div>
                                                        <div class="" name="pic_name" style="display: inline-block;">
                                                            <a style="width: 120px;"><%# Eval("Name") %></a>
                                                        </div>
                                                        <ul style="display: block;" runat="server" visible='<%# IsDisplay((Guid)Eval("Id")) %>'>
                                                            <telerik:RadListView runat="server" ID="secondList2" Visible='<%# IsDisplay((Guid)Eval("Id"))%>' DataSource='<%# LoadCatalog((Guid)Eval("Id")) %>'>
                                                                <ItemTemplate>
                                                                    <li>
                                                                        <div onclick="checkChange(this)" style="display: inline-block;">
                                                                            <input type="checkbox" id="ischecked"  checked="checked" source-id='<%# Eval("Id") %>' />
                                                                            <%--<asp:CheckBox  ID="cb" runat="server" Checked="true" class="chk_class" AutoPostBack="true"  OnCheckedChanged="cb_CheckedChanged" Value='<%# Eval("Id")%>' />--%>
                                                                        </div>
                                                                        <div class="" name="pic_name" style="display: inline-block;">
                                                                            <a style="width: 120px;"><%# Eval("Name")%></a>
                                                                        </div>
                                                                        <ul style="display: block;" runat="server" visible='<%# IsDisplay((Guid)Eval("Id")) %>'>
                                                                            <telerik:RadListView runat="server" ID="secondList3" Visible='<%# IsDisplay((Guid)Eval("Id"))%>' DataSource='<%# LoadCatalog((Guid)Eval("Id")) %>'>
                                                                                <ItemTemplate>
                                                                                    <li>

                                                                                        <div onclick="checkChange(this)" style="display: inline-block;">
                                                                                            <input type="checkbox" id="ischecked" checked="checked" source-id='<%# Eval("Id") %>' />
                                                                                            <%--<asp:CheckBox  ID="cb" runat="server" Checked="true" AutoPostBack="true" class="chk_class"  OnCheckedChanged="cb_CheckedChanged" Value='<%# Eval("Id") %>'/>--%>
                                                                                        </div>
                                                                                        <div class="" name="pic_name" style="display: inline-block;">
                                                                                            <a style="width: 120px;"><%# Eval("Name") %></a>
                                                                                        </div>
                                                                                        <ul style="display: block;" runat="server" visible='<%# IsDisplay((Guid)Eval("Id")) %>'>
                                                                                            <telerik:RadListView runat="server" ID="secondList4" Visible='<%# IsDisplay((Guid)Eval("Id"))%>' DataSource='<%# LoadCatalog((Guid)Eval("Id")) %>'>
                                                                                                <ItemTemplate>
                                                                                                    <li>
                                                                                                        <div onclick="checkChange(this)" style="display: inline-block;">
                                                                                                            <input type="checkbox" id="ischecked"  checked="checked" source-id='<%# Eval("Id") %>' />
                                                                                                            <%--<asp:CheckBox ID="cb"  runat="server" Checked="true" AutoPostBack="true" class="chk_class" OnCheckedChanged="cb_CheckedChanged"  Value='<%# Eval("Id")%>'/>--%>
                                                                                                        </div>
                                                                                                        <div class="" name="pic_name" style="display: inline-block;">
                                                                                                            <a style="width: 120px;" class='<%# IsDisplay((Guid)Eval("Id"))%>'><%# Eval("Name") %></a>
                                                                                                        </div>
                                                                                                    </li>
                                                                                                </ItemTemplate>
                                                                                            </telerik:RadListView>
                                                                                        </ul>
                                                                                    </li>
                                                                                </ItemTemplate>
                                                                            </telerik:RadListView>
                                                                        </ul>
                                                                    </li>
                                                                </ItemTemplate>
                                                            </telerik:RadListView>
                                                        </ul>
                                                    </li>
                                                </ItemTemplate>
                                            </telerik:RadListView>

                                        </div>
                                    </div>
                                </div>
                            </div>
                             </telerik:RadAjaxPanel>
                            <telerik:RadAjaxPanel runat="server" ID="sa" OnAjaxRequest="sa_AjaxRequest">
                            <div class="fr" style="width: 500px; margin: 15px; border: 1px solid #eee;">
                                <div class="xy_allzylist">
                                    <telerik:RadListView runat="server" ID="result" OnItemDataBound="result_ItemDataBound" ItemPlaceholderID="holder">
                                        <LayoutTemplate>
                                            <table width="500" style="line-height: 40px;">
                                                <tr style="background: #F1F4F9; font-size: 14px;">
                                                    <td align="center" width="250" height="30">标题</td>
                                                    <td align="center" width="120">审核状态</td>
                                                    <td align="center" width="120">操作</td>
                                                </tr>
                                                <asp:PlaceHolder ID="holder" runat="server"></asp:PlaceHolder>
                                            </table>
                                        </LayoutTemplate>
                                        <ItemTemplate>
                                            <tr style='<%#Container.DataItemIndex%2 != 0?"background-color:#F9F9F9;": "" %>'>
                                                <td height="40" style="padding-left: 15px; line-height: 40px;">
                                                    <img src='<%# string.Format("../Image/img/{0}.jpg", Eval("Thumbnail")) %>' width="16" height="16" />&nbsp;&nbsp;&nbsp;&nbsp;<a limit="30" href='<%# string.Format("../Go/{0}?Id={1}", ((Homory.Model.ResourceType)Eval("Type"))== Homory.Model.ResourceType.视频 ? "ViewVideo" : "ViewPlain", Eval("Id")) %>' style="color: black;" class="xy_zkyl"><%# Eval("Title").ToString().Length>16?Eval("Title").ToString().Substring(0,16)+"...": Eval("Title").ToString() %> </a>
                                                </td>
                                                <td align="center">
                                                    <a id="ab" runat="server" style="cursor: pointer;"><%# ((OpenType)Eval("OpenType")) == OpenType.不公开 ? "不公开" : (((State)Eval("State")) == State.停用 ? "未通过" : (((State)Eval("State")) == State.默认 ? "未审核" : "已通过")) %></a>
                                                    <telerik:RadToolTip ID="tip" runat="server" IsClientID="true" Skin="MetroTouch" AutoCloseDelay="0" Visible='<%#(OpenType)Eval("OpenType")!= OpenType.不公开&&(State)Eval("State")!=State.默认?true:false %>'>
                                                        <telerik:RadListView runat="server" ID="results" DataSource='<%# ExamineList( (Guid)Eval("Id")) %>' ItemPlaceholderID="holderlist">
                                                            <LayoutTemplate>
                                                                <table width="310" style="line-height: 35px;">
                                                                    <tr style="background: #F1F4F9;">
                                                                        <td align="center" width="150" height="30">审核说明</td>
                                                                        <td align="center" width="80">审核人员</td>
                                                                        <td align="center" width="80">&nbsp;&nbsp;&nbsp;&nbsp;审核时间</td>
                                                                    </tr>
                                                                    <asp:PlaceHolder ID="holderlist" runat="server"></asp:PlaceHolder>
                                                                </table>
                                                            </LayoutTemplate>
                                                            <ItemTemplate>
                                                                <tr>
                                                                    <td align="center"><%# string.Empty.Equals(Eval("Content").ToString().Trim()) ? "未备注" : Eval("Content").ToString() %></td>
                                                                    <td align="center"><%# U(Eval("AuditUser")).DisplayName.ToString() %></td>
                                                                    <td align="center"><%# ((DateTime)Eval("Time")).ToShortDateString().ToString() %></td>
                                                                </tr>
                                                            </ItemTemplate>
                                                        </telerik:RadListView>
                                                    </telerik:RadToolTip>
                                                </td>
                                                <td align="center"><span runat="server" visible='<%#IsEdit((bool)Eval("Audit"),(bool)Eval("AuditEditable"),(State)Eval("State"))%>'><a target="_blank" href='<%# string.Format("../Go/Editing?Id={0}", Eval("Id")) %>'>编辑</a></span>
                                                    &nbsp;&nbsp;&nbsp;&nbsp;<span runat="server" visible='<%#IsEdit((bool)Eval("Audit"),(bool)Eval("AuditEditable"),(State)Eval("State"))%>'><a target="_blank" href="javascript:void(0);" data-id='<%# Eval("Id") %>' onserverclick="del2_ServerClick">删除</a></span>
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                    </telerik:RadListView>
                                    <br />

                                    <telerik:RadDataPager runat="server" ID="pager" Style="width: 98%; margin: auto;" PageSize="20" PagedControlID="result">
                                        <Fields>
                                            <telerik:RadDataPagerSliderField HorizontalPosition="NoFloat" LabelTextFormat="第{0}页 共{1}页 每页20项" SliderDecreaseText="前翻" SliderIncreaseText="后翻" SliderDragText="拖动" SliderOrientation="Horizontal" />
                                        </Fields>
                                    </telerik:RadDataPager>
                                </div>
                            </div>
                                </telerik:RadAjaxPanel>
                            <script src="../Script/index_click.js"></script>
                        </div>
                       <div id="hidden" style="display:none;">
                            <input type="text" runat="server" id="hidden_value"/>
                        </div>
                   
                </div>
                 
                <homory:CommonBottom runat="server" ID="CommonBottom" />
            </div>
        </div>

    </form>
</body>
</html>













