<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ObjectReturn.ascx.cs" Inherits="Control_ObjectReturn" %>

<tr>
    <td style="display: none;">
        <asp:Label runat="server" ID="xid"></asp:Label>
        <asp:Label runat="server" ID="code"></asp:Label>
    </td>
    <td>
        <asp:Label runat="server" ID="time"></asp:Label>
    </td>
    <td>
        <asp:Label runat="server" ID="name"></asp:Label>
    </td>
    <td>
        <asp:Label runat="server" ID="price"></asp:Label>
    </td>
    <td>
        <telerik:RadNumericTextBox ID="amount" runat="server" Width="120" MaxValue="1" NumberFormat-DecimalDigits="0" DataType="System.Decimal" AllowOutOfRangeAutoCorrect="true"></telerik:RadNumericTextBox>
    </td>
    <td>
        <telerik:RadNumericTextBox ID="outAmount" runat="server" Width="120" MaxValue="1" NumberFormat-DecimalDigits="0" DataType="System.Decimal" AllowOutOfRangeAutoCorrect="true"></telerik:RadNumericTextBox>
    </td>
    <td>
        <telerik:RadTextBox ID="note" runat="server" Width="100"></telerik:RadTextBox>
    </td>
</tr>
