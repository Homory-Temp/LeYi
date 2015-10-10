<%@ Control Language="C#" AutoEventWireup="true" CodeFile="CommonAssistant.ascx.cs" Inherits="Control.ControlCommonAssistant" %>
<%@ Import Namespace="Homory.Model" %>

<div class="tabbg" name="tab" style="margin-top: 20px;">

    <div class="xy_zycont" name="tabCon">
        <asp:Repeater runat="server" ID="L">
            <ItemTemplate>
                <dl>
                    <dt>
                        <a href='<%# string.Format("../Go/{1}?Id={0}", Eval("Id"), ((Homory.Model.Resource)Container.DataItem).Type == Homory.Model.ResourceType.视频 ? "ViewVideo" : "ViewPlain") %>'>
                            <asp:Image runat="server" ImageUrl='<%# Eval("Image") %>' Width="120" Height="90" />
                        </a>
                    </dt>
                    <dd>
                        <h3>
                            <a title="<%# Eval("Title") %>" href='<%# string.Format("../Go/{1}?Id={0}", Eval("Id"), ((Homory.Model.Resource)Container.DataItem).Type == Homory.Model.ResourceType.视频 ? "ViewVideo" : "ViewPlain") %>'><%# Eval("Title").ToString().CutString(12, "...") %>
                            </a></h3>
                        <h4><span>发布者：<a href='<%# string.Format("../Go/Personal?Id={0}", Eval("UserId")) %>'><%# U(Eval("UserId")).RealName %></a></span></h4>

                        <p><%# Eval("View") %>人访问</p>
                    </dd>
                </dl>
            </ItemTemplate>
        </asp:Repeater><div style="clear: both;"></div>
        <ul >
            <asp:Repeater runat="server" ID="S">
                <ItemTemplate>
                    <li class="clearfix" style="float: left;">
                        <table width="270" border="0" align="left" cellpadding="0" cellspacing="0" class="hot-list">
                            <tr>
                                <td width="20" valign="middle">
                                    <img src='<%# string.Format("../Image/img/{0}.jpg", Eval("Thumbnail")) %>' /></td>
                                <td width="170" valign="middle"><a class="fl" href='<%# string.Format("../Go/{1}?Id={0}", Eval("Id"), ((Homory.Model.Resource)Container.DataItem).Type == Homory.Model.ResourceType.视频 ? "ViewVideo" : "ViewPlain") %>'><%# Eval("Title").ToString().CutString(12, "...") %></a></td>
                                <td width="80"><%# ((DateTime)Eval("Time")).FormatTime() %></td>
                            </tr>
                        </table>
                    </li>
                </ItemTemplate>
            </asp:Repeater>

        </ul>

    </div>



</div>


