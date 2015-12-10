<%@ Control Language="C#" AutoEventWireup="true" CodeFile="C6Action.ascx.cs" Inherits="Control.ControlC6Action" %>
<%@ Import Namespace="System.Web.Configuration" %>
<%@ Import Namespace="Homory.Model" %>

<telerik:RadAjaxPanel runat="server" ID="PersonalActionPanel">
    <div class="box class-feed" style="width: 400px; height: 520px;">
     
        <div class="box-bd" style="height: 500px;">
            <ul class="hot-list">
                <asp:Repeater runat="server" ID="actions">
                    <ItemTemplate>
                        <asp:Panel runat="server" Visible='<%# ((Homory.Model.ActionType)Eval("Type")) == ActionType.用户评分资源 %>'>
                            <dl style="display: block;">
                                <dt>
                                    <a target="_blank" href='<%# string.Format("../Go/Personal?Id={0}", Eval("id3")) %>'>
                                        <asp:Image Width="49" Height="49" ImageUrl='<%# U(Eval("Id3")).Icon %>' CssClass="face face_40" runat="server"></asp:Image></a>
                                </dt>
                                <dd>
                                    <div>
                                        <p class="clearfix pb5">
                                            <a target="_blank" class="fl" href='<%# string.Format("../Go/Personal?Id={0}", Eval("id3")) %>'><%# UC(Eval("Id3")) + "&nbsp;" + U(Eval("Id3")).DisplayName %></a>
                                            <span class="fr tim"><%# ((DateTime)Eval("Time")).FormatTime() %></span>
                                        </p>
                                        <p class="tle">
                                            评分： <%# Eval("Content1") %>
                                        </p>
                                        <p>
                                            <a target="_blank" href='<%# string.Format("../Go/{1}?Id={0}", R(Eval("Id2")).Id,  R(Eval("Id2")).Type== Homory.Model.ResourceType.视频 ? "ViewVideo" : "ViewPlain") %>'>
                                                <%# R(Eval("Id2")).Title.CutString(12, "...") %>
                                            </a>
                                        </p>
                                    </div>
                                </dd>
                            </dl>
                        </asp:Panel>
                        <asp:Panel runat="server" Visible='<%# ((Homory.Model.ActionType)Eval("Type")) == ActionType.用户评论资源 %>'>
                            <dl style="display: block;">
                                <dt>
                                    <a target="_blank" href='<%# string.Format("../Go/Personal?Id={0}", Eval("id3")) %>'>
                                        <asp:Image Width="49" Height="49" ImageUrl='<%# U(Eval("Id3")).Icon %>' CssClass="face face_40" runat="server"></asp:Image></a>
                                </dt>
                                <dd>
                                    <div>
                                        <p class="clearfix pb5">
                                            <a target="_blank" class="fl" href="<%# string.Format("../Go/Personal?Id={0}", Eval("id3")) %>" ><%# UC(Eval("Id3")) + "&nbsp;" + U(Eval("Id3")).DisplayName %></a>
                                            <span class="fr tim"><%# ((DateTime)Eval("Time")).FormatTime() %></span>
                                        </p>
                                        <p class="tle">
                                            评论： <%# Eval("Content1").ToString().CutString(12, "...") %>
                                        </p>
                                        <p>
                                            <a target="_blank" href='<%# string.Format("../Go/{1}?Id={0}", R(Eval("Id2")).Id,R(Eval("Id2")).Type== Homory.Model.ResourceType.视频 ? "ViewVideo" : "ViewPlain") %>'>
                                                <%# R(Eval("Id2")).Title %>
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
