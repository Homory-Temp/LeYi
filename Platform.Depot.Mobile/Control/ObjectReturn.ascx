<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ObjectReturn.ascx.cs" Inherits="Control_ObjectReturn" %>

<tr>
    <td style="display: none;">
        <asp:Label runat="server" ID="xid"></asp:Label>
        <asp:Label runat="server" ID="code"></asp:Label>
    </td>
    <td style="display: none;">
        <asp:Label runat="server" ID="price"></asp:Label>
    </td>
    <td style="text-align: left;">
        &nbsp;&nbsp;&nbsp;&nbsp;借用：<asp:Label runat="server" ID="time"></asp:Label><br />
        &nbsp;&nbsp;&nbsp;&nbsp;名称：<asp:Label runat="server" ID="name"></asp:Label><br />
        &nbsp;&nbsp;&nbsp;&nbsp;归还：<telerik:RadNumericTextBox ID="amount" runat="server" Width="120" MaxValue="1" NumberFormat-DecimalDigits="0" DataType="System.Decimal" AllowOutOfRangeAutoCorrect="true"></telerik:RadNumericTextBox><br />
        &nbsp;&nbsp;&nbsp;&nbsp;报废：<telerik:RadNumericTextBox ID="outAmount" runat="server" Width="120" MaxValue="1" NumberFormat-DecimalDigits="0" DataType="System.Decimal" AllowOutOfRangeAutoCorrect="true"></telerik:RadNumericTextBox>
    </td>
    <td style="display: none;">
        <telerik:RadTextBox ID="note" runat="server" Width="100"></telerik:RadTextBox>
    </td>
</tr>
