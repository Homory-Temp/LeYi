<%@ Control Language="C#" AutoEventWireup="true" CodeFile="CenterLeft.ascx.cs" Inherits="Control_CenterLeft" %>

<div class="srx-left" style="height: 1500px;">

    <div class="u-l-info" id="userInfoBox">
        <div class="u-li-pro clearfix" id="faceBox">
            <a href="" class="srx-face">
                <asp:Image runat="server" ID="icon" Width="50" Height="50" />
            </a>
            <div class="u-lip-name ft14">
                <asp:Label runat="server" ID="name"></asp:Label>
            </div>
        </div>

        <ul class="u-li-data clearfix mt10">
            <li class="u-lid-flw"><em class="f16">
                <a href="../Go/CenterAttend">
                    <asp:Label runat="server" ID="favourite"></asp:Label></a>
            </em><span class="f12">
                <label>关注</label></span>
            </li>
            <li class="u-lid-fns">

                <em class="f16">
                    <a href="../Go/CenterFavor">
                        <asp:Label runat="server" ID="fav"></asp:Label></a>
                </em><span class="f12">
                    <label>收藏</label></span>
            </li>
            <li class="u-lid-cn">
                <em class="f16">
                    <asp:Label runat="server" ID="honor"></asp:Label>
                </em><span class="f12">
                    <label>荣誉</label></span>
            </li>
        </ul>
        <table style="margin-top: 10px;">
            <tr>
                <td>视频：<a href="../Go/CenterResource?initTab=0">
                    <span id="cc4" runat="server"></span>

                </a></td>
                <td width="10"></td>
                <td>文章：<a href="../Go/CenterResource?initTab=1">
                    <span id="cc1" runat="server"></span>
                </a></td>

            </tr>
            <tr>
                <td>课件：<a href="../Go/CenterResource?initTab=2">

                    <span id="cc2" runat="server"></span>
                </a></td>
                <td width="10"></td>
                <td>试卷：<a href="../Go/CenterResource?initTab=3">
                    <span id="cc3" runat="server"></span>

                </a></td>

            </tr>
        </table>
    </div>


    <%--左侧导航菜单--%>

    <ul class="u-l-nav u-ln-cur-1 mt20" style="font-size: 14px">
        <li class="u-ln">
            <a href="../Go/Center" target="_top" class="u-ln-a u-ln-5"><i class="icon20 icon20-app-class"></i>资源互动</a>
        </li>
        <li class="u-ln">
            <a id="toPublish" style="cursor: pointer;" onclick="popupPublish();" class="u-ln-a u-ln-5"><i class="icon20 icon20-app-new"></i>资源发布</a>
        </li>
        <li class="u-ln">
            <a href="../Go/CenterResource" target="_top" class="u-ln-a u-ln-5"><i class="icon20 icon20-app-class"></i>资源管理</a>
        </li>
        <li class="u-ln">
            <a href="../Go/CenterGroup.aspx" class="u-ln-a u-ln-6"><i class="icon20 icon20-app-school"></i>教研团队</a>
        </li>
        <li class="u-ln">
            <a href="../Go/Centernote.aspx" class="u-ln-a u-ln-7"><i class="icon20 icon20-app-quwei"></i>我的听课笔记</a>
        </li>
        <li class="u-ln">
            <a href="../Go/CenterFavor.aspx" class="u-ln-a u-ln-7"><i class="icon20 icon20-app-quwei"></i>我的收藏</a>
        </li>
        <li class="u-ln">
            <a href="../Go/CenterAttend.aspx" class="u-ln-a u-ln-7"><i class="icon20 icon20-app-quwei"></i>我的关注</a>
        </li>
    </ul>
</div>

<telerik:RadWindowManager runat="server" ID="Rwm" Skin="Metro">
    <Windows>
        <telerik:RadWindow ID="popup_publish" Title="资源发布" runat="server" AutoSize="False" Width="320" Height="260" ShowContentDuringLoad="false" ReloadOnShow="False" KeepInScreenBounds="true" VisibleStatusbar="false" Behaviors="Close" Modal="True" Localization-Close="关闭" EnableEmbeddedScripts="True" EnableEmbeddedBaseStylesheet="True" VisibleTitlebar="True">
            <ContentTemplate>
                <style>
                    .pub_v, .pub_v:hover {
                        display: block;
                        margin: 0 auto;
                        background: url("../image/up/pub_v.png") 0 0 no-repeat;
                        width: 173px;
                        height: 48px;
                        line-height: 43px;
                        color: #fff;
                        padding-left: 15px;
                        overflow: hidden;
                        text-decoration: none;
                        font-size: 16px;
                    }

                    .pub_a, .pub_a:hover {
                        display: block;
                        margin: 0 auto;
                        background: url("../image/up/pub_a.png") 0 0 no-repeat;
                        width: 173px;
                        height: 48px;
                        line-height: 43px;
                        color: #fff;
                        padding-left: 15px;
                        overflow: hidden;
                        text-decoration: none;
                        font-size: 16px;
                    }

                    .pub_c, .pub_c:hover {
                        display: block;
                        margin: 0 auto;
                        background: url("../image/up/pub_c.png") 0 0 no-repeat;
                        width: 173px;
                        height: 48px;
                        line-height: 43px;
                        color: #fff;
                        padding-left: 15px;
                        overflow: hidden;
                        text-decoration: none;
                        font-size: 16px;
                    }
                </style>
                <div style="width: 280px; text-align: center; margin: auto;">
                    <a class="pub_v" style="cursor: pointer; margin: 20px auto 10px 50px;" href="Publishing.aspx?Type=Media" title=" &#13;课堂实录、专题活动&#13;&#13; ">发布视频</a>
                    <a class="pub_a" style="cursor: pointer; margin: 10px auto 10px 50px;" href="Publishing.aspx?Type=Article" title=" &#13;案例论文、活动方案&#13;教材选登、佳作欣赏&#13; ">发布文章</a>
                    <a class="pub_c" style="cursor: pointer; margin: 10px auto 10px 50px;" href="Publishing.aspx?Type=Courseware" title=" &#13;语言、数学、科学&#13;美术、音乐、健康、综合&#13;经历学习活动、亲子时分&#13; ">发布课件</a>
                </div>
            </ContentTemplate>
        </telerik:RadWindow>
        <telerik:RadWindow ID="popup_pushX" runat="server" OnClientClose="pushPopped" Title="呈送" ReloadOnShow="True" Width="600" Height="400" Top="60" Left="200" ShowContentDuringLoad="false" VisibleStatusbar="false" Behaviors="Move,Close" Modal="True" CenterIfModal="False" Localization-Close="关闭">
        </telerik:RadWindow>
        <telerik:RadWindow ID="popup_create" runat="server" AutoSize="True" Width="840" Height="350" ShowContentDuringLoad="False" ReloadOnShow="True" KeepInScreenBounds="true" VisibleStatusbar="false" Behaviors="Close" Modal="True" Localization-Close="关闭" Title="创建团队" EnableEmbeddedScripts="True" EnableEmbeddedBaseStylesheet="True" VisibleTitlebar="True">
        </telerik:RadWindow>
        <telerik:RadWindow ID="popup_edit" runat="server" AutoSize="True" Width="840" Height="350" ShowContentDuringLoad="False" ReloadOnShow="True" KeepInScreenBounds="true" VisibleStatusbar="false" Behaviors="Close" Modal="True" Localization-Close="关闭" Title="编辑团队" EnableEmbeddedScripts="True" EnableEmbeddedBaseStylesheet="True" VisibleTitlebar="True">
        </telerik:RadWindow>
        <telerik:RadWindow ID="windowCatalog" runat="server" NavigateUrl="~/Popup/StudioCatalog.aspx" Width="800" Height="400" CssClass="coreCenter coreMiddle" ReloadOnShow="true" VisibleStatusbar="false" Behaviors="Close" Modal="true" Title="栏目管理" Localization-Close="关闭">
        </telerik:RadWindow>
        <telerik:RadWindow ID="windowMember" runat="server" NavigateUrl="~/Popup/StudioMember.aspx" Width="800" Height="400" CssClass="coreCenter coreMiddle" ReloadOnShow="true" VisibleStatusbar="false" Behaviors="Close" Modal="true" Title="成员选择" Localization-Close="关闭">
        </telerik:RadWindow>
        <telerik:RadWindow ID="windowRes" runat="server" NavigateUrl="~/Popup/CenterStudio.aspx" Width="800" Height="400" CssClass="coreCenter coreMiddle" ReloadOnShow="true" VisibleStatusbar="false" Behaviors="Close" Modal="true" Title="资源呈送" Localization-Close="关闭">
        </telerik:RadWindow>
    </Windows>
</telerik:RadWindowManager>
<script>
    var window_publish;

    function popupPublish() {
        window_publish = window.radopen(null, "popup_publish");
        return false;
    }
    function closePublish() {
        window_publish.close();
        return false;
    }
</script>
<script>
    function popupCreate() {
        window.radopen("../Popup/GroupManage", "popup_create");
        return false;
    }
    function popupEdit(id) {
        window.radopen("../Popup/GroupManageX.aspx?Id=" + id, "popup_edit");
        return false;
    }
    function PopCatalog(id) {
        window.radopen("../Popup/StudioCatalog.aspx?" + id, "windowCatalog");
        return false;
    }
    function PopMember(id) {
        window.radopen("../Popup/StudioMember.aspx?" + id, "windowMember");
        return false;
    }
    function PopRes(id) {
        window.radopen("../Popup/CenterStudio.aspx?" + id, "windowRes");
        return false;
    }
</script>
<script>
    var window_push;

    function popupPushX(url) {
        window_push = window.radopen(url, "popup_pushX");
        return false;
    }
    function pushPopped() {
        window_push.close();
    }
</script>
