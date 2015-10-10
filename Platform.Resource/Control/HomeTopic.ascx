<%@ Control Language="C#" AutoEventWireup="true" CodeFile="HomeTopic.ascx.cs" Inherits="Control.ControlHomeTopic" %>

<div class="u-rbox r-weekly">
    <div class="clearfix">
        <h3 class="fl mb5">推荐</h3>
    </div>
    <asp:Repeater ID="homory_topic" runat="server">
        <ItemTemplate>
            <div class="r-weekly-panel" style="display: block;width:250px;margin:8px 0px;">
                <a href='<%# ((XElement) Container.DataItem).Attribute("Url").Value %>'>
                    <img src='<%# ((XElement) Container.DataItem).Attribute("Image").Value %>' alt="" width="250" height="42">
                </a>
            </div>
        </ItemTemplate>
    </asp:Repeater>
</div>
