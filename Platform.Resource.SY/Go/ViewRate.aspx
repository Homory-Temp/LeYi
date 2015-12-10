<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ViewRate.aspx.cs" Inherits="Go_ViewRate" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <base target="_top" />
</head>
<body style="width: 400px; height: 300px; overflow: hidden;">
    <form id="form1" runat="server">
    <div style="width: 400px; height: 300px; overflow: auto;">
		<asp:GridView ID="grid" runat="server" Width="400px" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" GridLines="None">
			<AlternatingRowStyle BackColor="White" HorizontalAlign="Center" />
			<RowStyle HorizontalAlign="Center"></RowStyle>
			<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
			<Columns>
				<asp:TemplateField HeaderText="教师">
					<ItemTemplate>
						<%# U(Eval("Id1")).RealName %>
					</ItemTemplate>
				</asp:TemplateField>
				<asp:TemplateField HeaderText="评分">
					<ItemTemplate>
						<%# Eval("Content1") %>
					</ItemTemplate>
				</asp:TemplateField>
				<asp:TemplateField HeaderText="日期">
					<ItemTemplate>
						<%# ((System.DateTime)Eval("Time")).ToString("yyyy-MM-dd") %>
					</ItemTemplate>
				</asp:TemplateField>
			</Columns>
			<EditRowStyle BackColor="#2461BF" />
			<FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
			<HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
			<PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
			<RowStyle BackColor="#EFF3FB" />
			<SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
			<SortedAscendingCellStyle BackColor="#F5F7FB" />
			<SortedAscendingHeaderStyle BackColor="#6D95E1" />
			<SortedDescendingCellStyle BackColor="#E9EBEF" />
			<SortedDescendingHeaderStyle BackColor="#4870BE" />
		</asp:GridView>
    </div>
    </form>
</body>
</html>
