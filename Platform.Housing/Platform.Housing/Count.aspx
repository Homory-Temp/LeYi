﻿<%@ Page Title="梁溪教育入学辅助查询系统" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeFile="Count.aspx.cs" Inherits="Count" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<asp:Content ID="content" ContentPlaceHolderID="body" runat="Server">
    <telerik:RadAjaxPanel ID="ap" runat="server">
        <telerik:RadHtmlChart ID="chart" runat="server" Width="100%" Height="2000" Legend-Appearance-Position="Top"></telerik:RadHtmlChart>
    </telerik:RadAjaxPanel>
</asp:Content>
