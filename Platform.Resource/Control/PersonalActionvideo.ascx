<%@ Control Language="C#" AutoEventWireup="true" CodeFile="PersonalActionvideo.ascx.cs" Inherits="Control.ControlPersonalActionvideo" %>
<%@ Import Namespace="System.Web.Configuration" %>
<%@ Import Namespace="Homory.Model" %>

<telerik:RadAjaxPanel runat="server" ID="PersonalActionPanel">

    <asp:Repeater runat="server" ID="actions">
        <ItemTemplate>

               <div style="width:160px;float:left;margin:10px;"><table align="center">
                    <tr><td  align="center" > <img src='<%# Eval("Image") %>' width="140" height="100" /></td></tr>
                      <tr><td  align="center" height="30"> <p  class="fd-title"> <a  href='<%# string.Format("../Go/ViewVideo?Id={0}", Eval("Id")) %>' ><%# Eval("Title") %> </a> </p></td></tr>
                      <tr><td  align="center"><%# ((DateTime)Eval("Time")).FormatTime() %></td></tr>
                     <tr><td  align="center">编辑    删除</td></tr>
                     
                </table>
                    </div>
               

        </ItemTemplate>
    </asp:Repeater>

</telerik:RadAjaxPanel>
