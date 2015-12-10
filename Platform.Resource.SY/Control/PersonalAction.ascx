<%@ Control Language="C#" AutoEventWireup="true" CodeFile="PersonalAction.ascx.cs" Inherits="Control.ControlPersonalAction" %>
<%@ Import Namespace="System.Web.Configuration" %>
<%@ Import Namespace="Homory.Model" %>

<telerik:RadAjaxPanel runat="server" ID="PersonalActionPanel">
    <div class="box class-feed" style="width: 262px; height: 520px;">
        <div class="box-hd">资源互动</div>
        <div class="box-bd" style="height: 500px;">
            <ul class="hot-list">
                <asp:Repeater runat="server" ID="actions">
                    <ItemTemplate>
                        <asp:Panel runat="server">
                            <dl style="display: block;">
                                <dt>
                                    <a href='<%# string.Format("../Go/Personal?Id={0}", Eval("id3")) %>'>
                                        <asp:Image Width="49" Height="49" ImageUrl='<%# P(U(Eval("Id3")).Icon) %>' CssClass="face face_40" runat="server"></asp:Image></a>
                                </dt>
                                <dd>
                                    <div>
                                        <p class="clearfix pb5">
                                            <a class="fl" style="color:#515151;" href='<%# string.Format("../Go/Personal?Id={0}", Eval("id3")) %>'><%# UC(Eval("Id3")) + "&nbsp;" + U(Eval("Id3")).DisplayName %></a>
                                            <span class="fr tim"><%# ((DateTime)Eval("Time")).FormatTime() %></span>
                                        </p>
                                        <p class="tle">
                                            <font color="#227dc5"><%# ((Homory.Model.ActionType)Eval("Type")) == ActionType.用户评分资源 ? "评分：" : (((Homory.Model.ActionType)Eval("Type")) == ActionType.用户评论资源 ? "评论：" : "回复：") %><%# Eval("Content1") == null ? "" : Eval("Content1").ToString().CutString(12,"...") %></font>
                                        </p>
                                        <p>
                                            <img src='<%# string.Format("../Image/img/{0}.jpg", R(Eval("Id2")).Thumbnail) %>' width="13" height="13" />
                                            <a href='<%# string.Format("../Go/{1}?Id={0}", R(Eval("Id2")).Id, R(Eval("Id2")).Type == Homory.Model.ResourceType.视频 ? "ViewVideo" : "ViewPlain") %>'>
                                                <%# R(Eval("Id2")).Title.CutString(12, "...") %>
                                            </a>
                                        </p>
                                    </div>
                                </dd>
                            </dl>
                        </asp:Panel>
                    </ItemTemplate>
                </asp:Repeater>
            </ul>
        </div>
    </div>
</telerik:RadAjaxPanel>
