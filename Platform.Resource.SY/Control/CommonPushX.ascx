<%@ Control Language="C#" AutoEventWireup="true" CodeFile="CommonPushX.ascx.cs" Inherits="Control.ControlCommonPushX" %>

<div class="srx-r1">
    <div class="msgFeed user_feed mt15">

        <div style="width: 575px;">
            <div style="width:120px; float: left; border-right: 1px solid #EEEEEE; text-align: left;">
                <telerik:RadTreeView runat="server" ID="groupTree" OnNodeClick="groupTree_OnNodeClick" DataFieldID="Id" DataFieldParentID="ParentId" DataTextField="Name" DataValueField="Id">
                </telerik:RadTreeView>
            </div>
            <div style="width: 440px; float: left;padding-left:10px;line-height:16px;height:25px;">
                <label id="pop" runat="server" visible="true" style="margin:0px 0px 20px 20px; padding: 4px 20px; color: white; cursor:pointer;background-color:#227DC5;"><B>选择资源</B></label>
                <asp:Repeater runat="server" ID="result">
                    <HeaderTemplate>
                        <table style="margin-top:10px;">
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr>
                            <td height="25">
                                <img src='<%# string.Format("../Image/img/{0}.jpg", (int)Eval("FileType")) %>' width="16" height="16" />
                            </td>
                            <td>&nbsp;<%# Eval("Title") %> 
                            </td>
                            <td>时间： <em><%# ((DateTime)Eval("Time")).ToString("yyyy-MM-dd") %></em>
                            </td>
                            <td align="right" width="70">
                                <a id="btnDo" itemid='<%# Eval("Id") %>' runat="server" onserverclick="btnDo_OnServerClick">取消呈送</a>
                            </td>
                        </tr>
                    </ItemTemplate>


                    <FooterTemplate>
                        </table>
                    </FooterTemplate>

                </asp:Repeater>
            </div>
        </div>
    </div>
</div>
