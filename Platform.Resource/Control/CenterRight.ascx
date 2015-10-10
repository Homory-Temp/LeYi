<%@ Control Language="C#" AutoEventWireup="true" CodeFile="CenterRight.ascx.cs" Inherits="Control_CenterRight" %>
<%@ Import Namespace="Homory.Model" %>

<div class="srx-r2">
                            <div id="rCalendar" class="u-rbox">
                                <div class="rb-calendar clearfix">
                                    <div class="rb-c-date pt5 pb5 f14">
                                        <%--时间--%>
                                        <asp:Label runat="server" ID="time"></asp:Label>

                                    </div>

                                </div>
                            </div>


                            <div class="r_topic mt20">
                                <h3 class="mb5">教研话题</h3>
                                <ul class="hot_topic">
	                                <asp:Repeater runat="server" ID="groupRes">
		                                <ItemTemplate>
											<li><span class="icon_order light_order"><%# Container.ItemIndex + 1 %></span> <span class="topic_title"><a class="fl" href='<%# string.Format("../Go/{0}?Id={1}", ((Homory.Model.ResourceType)Eval("Type"))== Homory.Model.ResourceType.视频 ? "ViewVideo" : "ViewPlain", Eval("Id")) %>'><%# ((Homory.Model.Resource)Container.DataItem).Title.CutString(12) %></a></span><span class="topic_author">发布人：<a href='<%# string.Format("../Go/Personal?Id={0}", Eval("UserId")) %>'><%# ((Homory.Model.Resource)Container.DataItem).User.DisplayName %></a></span> </li>
		                                </ItemTemplate>
	                                </asp:Repeater>
                                </ul>
                            </div>



                            <%--荣誉排行--%>
                            <div class="r_topic mt20">
                                <h3 class="mb5">评论排行</h3>
                                <ul class="hot_topic">


                                    <asp:Repeater runat="server" ID="board">
                                        <ItemTemplate>
                                            <li><span class="icon_order1">
                                                <img src='<%# string.Format("../Image/honor/{0}.jpg", Container.ItemIndex) %>' width="20" height="34"></span> <span class="topic_title"><a class="fl" href="#"><%# Eval("Title") %></a> </span><span class="topic_author">评论数：<%# Eval("Rate") %></span>
                                            </li>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </ul>
                            </div>





                            <%--关注的人--%>
                            <telerik:RadAjaxPanel runat="server" ID="favUp">
                                <div class="u-rbox" data-last-index="5">
                                    <div class="clearfix">
                                        <a href="../Go/CenterAttend"><h3 class="fl">关注的人</h3></a>
                                        <a href="../Go/CenterAttend" class="fr" id="refreshFavourite" target="_self">更多</a>
                                    </div>
                                    <ul class="clearfix">
                                        <telerik:RadListView runat="server" ID="favourites" OnNeedDataSource="favourites_NeedDataSource">
                                        <%--<asp:Repeater runat="server" ID="favourites">--%>
                                            <ItemTemplate>
                                                <li class="pt10 clearfix"><a href="<%# string.Format("../Go/Personal?Id={0}", Eval("Id")) %>" class="fl">
                                                    <asp:Image runat="server" ImageUrl='<%# P(Eval("Icon")) %>' Width="40" Height="40" />
                                                </a>
                                                    <div class="fl ml5">
                                                        <p><a href="<%# string.Format("../Go/Personal.aspx?Id={0}", Eval("Id")) %>" data-card="20530" data-card-type="person"><%# Eval("DisplayName") %></a>&nbsp; </p>
                                                        <asp:ImageButton ID="removeFavourite" ImageUrl="../image/img/noguanzhu.jpg" runat="server" OnClick="removeFavourite_OnClick" CommandArgument='<%# Eval("Id") %>' />
                                                    </div>
                                                </li>
                                            </ItemTemplate>
                                        <%--</asp:Repeater>--%>
                                        </telerik:RadListView>
                                    </ul>
                                </div>
                                <%--可能感兴趣的人--%>
                                <div class="u-rbox" data-last-index="5">
                                    <div class="clearfix">
                                        <h3 class="fl">你可能感兴趣的人</h3>
                                        <a class="fr" href="javascript:;" data-action="changeUsers"></a>
                                    </div>
                                    <ul class="clearfix">
                                        <telerik:RadListView runat="server" ID="relatives" OnNeedDataSource="relatives_NeedDataSource">
                                        <%--<asp:Repeater runat="server" ID="relatives">--%>
                                            <ItemTemplate>
                                                <li class="pt10 clearfix"><a href="<%# string.Format("../Go/Personal.aspx?Id={0}", Eval("Id")) %>" class="fl">
                                                    <asp:Image runat="server" ImageUrl='<%# P(Eval("Icon")) %>' Width="40" Height="40" />
                                                </a>
                                                    <div class="fl ml5">
                                                        <p><a href="<%# string.Format("../Go/Personal.aspx?Id={0}", Eval("Id")) %>" data-card="20530" data-card-type="person"><%# Eval("DisplayName") %></a>&nbsp; </p>
                                                        <asp:ImageButton ID="addFavourite" ImageUrl="../image/img/guanzhu.jpg" runat="server" OnClick="addFavourite_OnClick" CommandArgument='<%# Eval("Id") %>' />
                                                    </div>
                                                </li>
                                            </ItemTemplate>
                                        <%--</asp:Repeater>--%>
                                        </telerik:RadListView>
                                    </ul>
                                </div>






                            </telerik:RadAjaxPanel>

                            <%--<div class="u-rbox u-rbox-visitor">
                                <div class="u-rbox-title">
                                    <h3 class="">最近访客</h3>
                                    <p class="u-rbox-rtop skin-grey-a">访问总数:1</p>
                                </div>
                                <ul class="clearfix mt10">
                                    <li><a class="fl" href="#496251">
                                        <asp:Image runat="server" ID="visit" Width="40" Height="40"></asp:Image>
                                    </a>
                                        <p id="visitTime" runat="server" class="time"></p>
                                    </li>
                                </ul>
                            </div>--%>
                        </div>