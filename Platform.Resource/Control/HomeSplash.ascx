<%@ Control Language="C#" AutoEventWireup="true" CodeFile="HomeSplash.ascx.cs" Inherits="Control.ControlHomeSplash" %>


<div id="focus1">
    <ul>
        <asp:Repeater runat="server" ID="r_img">
            <ItemTemplate>

                <li>
                    <div style="left: 0; top: 0; width: 690px; height: 300px;">
                        <a href='<%# ((XElement) Container.DataItem).Attribute
("Url").Value %>'
                            target="_blank">
                            <img src='<%# ((XElement) Container.DataItem).Attribute("Image").Value %>' alt="" /></a>
                    </div>
                </li>


            </ItemTemplate>
        </asp:Repeater>
    </ul>
</div>

<asp:Repeater runat="server" ID="r_div" Visible="false">
    <ItemTemplate>
        <li class='<%# Container.ItemIndex==0?"dot-hot": "" %>'></li>
    </ItemTemplate>
</asp:Repeater>
