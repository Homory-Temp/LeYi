<%@ Control Language="C#" AutoEventWireup="true" CodeFile="HomeSplash.ascx.cs" Inherits="Control.ControlHomeSplash" %>

<div id="focus1">
    <ul>
        <asp:Repeater runat="server" ID="r_img">
            <ItemTemplate>
                <li>
                    <div style="left: 0; top: 0; width: 690px; height: 300px;">
                        <a href='<%# GetLink(Container.ItemIndex) %>' target="_blank">
                            <img src='<%# Container.DataItem %>' alt="" />
                        </a>
                    </div>
                </li>
            </ItemTemplate>
        </asp:Repeater>
    </ul>
</div>
