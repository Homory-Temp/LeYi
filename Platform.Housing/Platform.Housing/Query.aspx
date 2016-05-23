<%@ Page Title="梁溪教育学生住房信息检索平台" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeFile="Query.aspx.cs" Inherits="Query" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<asp:Content ID="content" ContentPlaceHolderID="body" runat="Server">
    <telerik:RadAjaxPanel ID="ap" runat="server">
        <telerik:RadComboBox ID="combo" runat="server" Skin="Bootstrap" DataTextField="Name" DataValueField="Id" Width="450" Filter="Contains" MarkFirstMatch="true" AutoPostBack="true" OnSelectedIndexChanged="combo_SelectedIndexChanged" LocalizationPath="~/Language"></telerik:RadComboBox>
        <br /><br />
        <telerik:RadTextBox ID="basic" runat="server" Label="学生信息：" LabelWidth="80" Width="300" Skin="Bootstrap"></telerik:RadTextBox>&nbsp;&nbsp;&nbsp;&nbsp;
        <telerik:RadTextBox ID="extended" runat="server" Label="地址信息：" LabelWidth="80" Width="500" Skin="Bootstrap"></telerik:RadTextBox>&nbsp;&nbsp;&nbsp;&nbsp;
        <telerik:RadButton ID="query" runat="server" Skin="Bootstrap" Text="检索" OnClick="query_Click"></telerik:RadButton>&nbsp;&nbsp;&nbsp;&nbsp;
        <telerik:RadButton ID="back" runat="server" Skin="Bootstrap" Text="数据维护" OnClick="back_Click" Style="float: right;"></telerik:RadButton>
        <br /><br />
        <telerik:RadGrid ID="grid" runat="server" Width="100%" OnNeedDataSource="grid_NeedDataSource" AllowPaging="True" PageSize="20" AllowSorting="True" ShowGroupPanel="False" RenderMode="Classic" LocalizationPath="~/Language">
            <MasterTableView AutoGenerateColumns="False" AllowFilteringByColumn="False" DataKeyNames="学校,姓名,身份证号,入学年份,户籍,住址" CommandItemDisplay="None">
                <Columns>
                    <telerik:GridBoundColumn HeaderStyle-Width="80" ItemStyle-Width="80" ItemStyle-HorizontalAlign="Center" DataField="姓名" HeaderText="姓名 *" SortExpression="姓名" UniqueName="姓名">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn HeaderStyle-Width="180" ItemStyle-Width="180" ItemStyle-HorizontalAlign="Center" DataField="身份证号" HeaderText="身份证号 *" SortExpression="身份证号" UniqueName="身份证号">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn HeaderStyle-Width="80" ItemStyle-Width="80" ItemStyle-HorizontalAlign="Center" DataField="入学年份" HeaderText="入学年份 *" SortExpression="入学年份" UniqueName="入学年份">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn HeaderStyle-Width="130" ItemStyle-Width="130" ItemStyle-HorizontalAlign="Center" DataField="户籍" HeaderText="户籍 *" SortExpression="户籍" UniqueName="户籍">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="住址" HeaderText="住址 *" SortExpression="住址" UniqueName="住址">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn HeaderStyle-Width="80" ItemStyle-Width="80" ItemStyle-HorizontalAlign="Center" DataField="班号" HeaderText="班号" SortExpression="班号" UniqueName="班号">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="备注" HeaderText="备注" SortExpression="备注" UniqueName="备注">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn HeaderStyle-Width="140" ItemStyle-Width="140" ItemStyle-HorizontalAlign="Center" DataField="时间" HeaderText="记录日期" SortExpression="时间" UniqueName="时间">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn HeaderStyle-Width="80" ItemStyle-Width="80" ItemStyle-HorizontalAlign="Center" DataField="匹配度" HeaderText="匹配度" SortExpression="匹配度" UniqueName="匹配度">
                    </telerik:GridBoundColumn>
                </Columns>
                <HeaderStyle HorizontalAlign="Center" />
                <NoRecordsTemplate><div style="width: 100%; text-align: center;">无记录</div></NoRecordsTemplate>
                <PagerStyle Mode="NextPrevAndNumeric" PageSizes="10,20,60,100" Position="Bottom" PageSizeControlType="RadComboBox" AlwaysVisible="true" PagerTextFormat="{4} 第{0}页，共{1}页；第{2}-{3}项，共{5}项" />
            </MasterTableView>
        </telerik:RadGrid>
    </telerik:RadAjaxPanel>
</asp:Content>
