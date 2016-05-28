<%@ Page Title="梁溪教育入学辅助查询系统" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeFile="Log.aspx.cs" Inherits="Log" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<asp:Content ID="content" ContentPlaceHolderID="body" runat="Server">
    <telerik:RadAjaxPanel ID="ap" runat="server">
        <telerik:RadComboBox ID="combo" runat="server" Skin="Bootstrap" DataTextField="Name" DataValueField="Id" Width="450" Filter="Contains" MarkFirstMatch="true" AutoPostBack="true" OnSelectedIndexChanged="combo_SelectedIndexChanged" LocalizationPath="~/Language"></telerik:RadComboBox>
        <br /><br />
        <telerik:RadDatePicker ID="from" runat="server" Skin="Bootstrap"></telerik:RadDatePicker>&nbsp;&nbsp;&nbsp;&nbsp;
        <telerik:RadDatePicker ID="to" runat="server" Skin="Bootstrap"></telerik:RadDatePicker>&nbsp;&nbsp;&nbsp;&nbsp;
        <telerik:RadButton ID="query" runat="server" Skin="Bootstrap" Text="检索" OnClick="query_Click"></telerik:RadButton>
        <telerik:RadButton ID="back" runat="server" Skin="Bootstrap" Text="返回" OnClick="back_Click" Style="float: right;"></telerik:RadButton>
        <br /><br />
        <telerik:RadGrid ID="grid" runat="server" Width="100%" OnNeedDataSource="grid_NeedDataSource" AllowPaging="True" PageSize="20" AllowSorting="True" ShowGroupPanel="False" RenderMode="Classic" LocalizationPath="~/Language">
            <MasterTableView AutoGenerateColumns="False" AllowFilteringByColumn="False" DataKeyNames="时间,学生信息查询内容,地址信息查询内容,用户姓名" CommandItemDisplay="None">
                <Columns>
                    <telerik:GridBoundColumn HeaderStyle-Width="140" ItemStyle-Width="140" ItemStyle-HorizontalAlign="Center" DataField="时间" HeaderText="时间" SortExpression="时间" UniqueName="时间">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="学生信息查询内容" HeaderText="学生信息查询内容" SortExpression="学生信息查询内容" UniqueName="学生信息查询内容">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="地址信息查询内容" HeaderText="地址信息查询内容" SortExpression="地址信息查询内容" UniqueName="地址信息查询内容">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn HeaderStyle-Width="100" ItemStyle-Width="100" ItemStyle-HorizontalAlign="Center" DataField="用户姓名" HeaderText="查询用户" SortExpression="用户姓名" UniqueName="用户姓名">
                    </telerik:GridBoundColumn>
                </Columns>
                <HeaderStyle HorizontalAlign="Center" />
                <NoRecordsTemplate><div style="width: 100%; text-align: center;">无记录</div></NoRecordsTemplate>
                <PagerStyle Mode="NextPrevAndNumeric" PageSizes="10,20,60,100" Position="Bottom" PageSizeControlType="RadComboBox" AlwaysVisible="true" PagerTextFormat="{4} 第{0}页，共{1}页；第{2}-{3}项，共{5}项" />
            </MasterTableView>
        </telerik:RadGrid>
    </telerik:RadAjaxPanel>
</asp:Content>
