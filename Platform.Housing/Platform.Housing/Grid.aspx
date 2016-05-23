<%@ Page Title="梁溪教育学生住房信息检索平台" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeFile="Grid.aspx.cs" Inherits="Grid" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<asp:Content ID="content" ContentPlaceHolderID="body" runat="Server">
    <telerik:RadAjaxPanel ID="ap" runat="server">
        <telerik:RadComboBox ID="combo" runat="server" Skin="Bootstrap" DataTextField="Name" DataValueField="Id" Width="450" Filter="Contains" MarkFirstMatch="true" AutoPostBack="true" OnSelectedIndexChanged="combo_SelectedIndexChanged" LocalizationPath="~/Language"></telerik:RadComboBox>
        <telerik:RadButton ID="import" runat="server" Skin="Bootstrap" Text="导入" OnClick="import_Click"></telerik:RadButton>
        <telerik:RadButton ID="query" runat="server" Skin="Bootstrap" Text="检索" OnClick="query_Click"></telerik:RadButton>
        <br /><br />
        <telerik:RadGrid ID="grid" runat="server" Width="100%" OnNeedDataSource="grid_NeedDataSource" AllowPaging="True" PageSize="20" AllowSorting="True" ShowGroupPanel="False" RenderMode="Classic" LocalizationPath="~/Language" OnBatchEditCommand="grid_BatchEditCommand">
            <MasterTableView AutoGenerateColumns="False" EditMode="Batch" AllowFilteringByColumn="False" DataKeyNames="学校,姓名,身份证号,入学年份,户籍,住址" CommandItemDisplay="Top" InsertItemPageIndexAction="ShowItemOnFirstPage">
                <Columns>
                    <telerik:GridTemplateColumn HeaderStyle-Width="80" ItemStyle-Width="80" ItemStyle-HorizontalAlign="Center" DataField="姓名" HeaderText="姓名 *" SortExpression="姓名" UniqueName="姓名">
                        <ItemTemplate>
                            <%# Eval("姓名") %>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <telerik:RadTextBox ID="name" runat="server" Text='<%# Bind("姓名") %>' Width="60" EnabledStyle-HorizontalAlign="Center"></telerik:RadTextBox>
                        </EditItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn HeaderStyle-Width="180" ItemStyle-Width="180" ItemStyle-HorizontalAlign="Center" DataField="身份证号" HeaderText="身份证号 *" SortExpression="身份证号" UniqueName="身份证号">
                        <ItemTemplate>
                            <%# Eval("身份证号") %>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <telerik:RadTextBox ID="idcard" runat="server" Text='<%# Bind("身份证号") %>' Width="160" EnabledStyle-HorizontalAlign="Center"></telerik:RadTextBox>
                        </EditItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn HeaderStyle-Width="80" ItemStyle-Width="80" ItemStyle-HorizontalAlign="Center" DataField="入学年份" HeaderText="入学年份 *" SortExpression="入学年份" UniqueName="入学年份">
                        <ItemTemplate>
                            <%# Eval("入学年份") %>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <telerik:RadTextBox ID="year" runat="server" Text='<%# Bind("入学年份") %>' Width="60" EnabledStyle-HorizontalAlign="Center"></telerik:RadTextBox>
                        </EditItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn HeaderStyle-Width="100" ItemStyle-Width="100" ItemStyle-HorizontalAlign="Center" DataField="户籍" HeaderText="户籍 *" SortExpression="户籍" UniqueName="户籍">
                        <ItemTemplate>
                            <%# Eval("户籍") %>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <telerik:RadTextBox ID="birthplace" runat="server" Text='<%# Bind("户籍") %>' Width="80" EnabledStyle-HorizontalAlign="Center"></telerik:RadTextBox>
                        </EditItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn DataField="住址" HeaderText="住址 *" SortExpression="住址" UniqueName="住址">
                        <ItemTemplate>
                            <%# Eval("住址") %>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <telerik:RadTextBox ID="address" runat="server" Text='<%# Bind("住址") %>' Width="200"></telerik:RadTextBox>
                        </EditItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn HeaderStyle-Width="80" ItemStyle-Width="80" ItemStyle-HorizontalAlign="Center" DataField="班号" HeaderText="班号" SortExpression="班号" UniqueName="班号">
                        <ItemTemplate>
                            <%# Eval("班号") %>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <telerik:RadTextBox ID="classno" runat="server" Text='<%# Bind("班号") %>' Width="60" EnabledStyle-HorizontalAlign="Center"></telerik:RadTextBox>
                        </EditItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn DataField="备注" HeaderText="备注" SortExpression="备注" UniqueName="备注">
                        <ItemTemplate>
                            <%# Eval("备注") %>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <telerik:RadTextBox ID="note" runat="server" Text='<%# Bind("备注") %>' Width="200"></telerik:RadTextBox>
                        </EditItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn ReadOnly="true" HeaderStyle-Width="140" ItemStyle-Width="140" ItemStyle-HorizontalAlign="Center" DataField="时间" HeaderText="记录日期" SortExpression="时间" UniqueName="时间">
                        <ItemTemplate>
                            <%# Eval("时间") %>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridButtonColumn HeaderStyle-Width="60" ItemStyle-Width="60" ItemStyle-HorizontalAlign="Center" HeaderText="删除" UniqueName="删除" CommandName="Delete" ButtonType="ImageButton" Text="删除"></telerik:GridButtonColumn>
                </Columns>
                <HeaderStyle HorizontalAlign="Center" />
                <ItemStyle Height="30" />
                <AlternatingItemStyle Height="30" />
                <NoRecordsTemplate><div style="width: 100%; text-align: center;">无记录</div></NoRecordsTemplate>
                <PagerStyle Mode="NextPrevAndNumeric" PageSizes="10,20,60,100" Position="Bottom" PageSizeControlType="RadComboBox" AlwaysVisible="true" PagerTextFormat="{4} 第{0}页，共{1}页；第{2}-{3}项，共{5}项" />
            </MasterTableView>
        </telerik:RadGrid>
    </telerik:RadAjaxPanel>
</asp:Content>
