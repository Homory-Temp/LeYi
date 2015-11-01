<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ObjectIn.ascx.cs" Inherits="Control_ObjectIn" %>

<tr>
    <td>
        <telerik:RadDropDownTree ID="catalog" runat="server" AutoPostBack="true" DataTextField="Name" DataValueField="Id" DataFieldID="Id" DataFieldParentID="ParentId" DropDownSettings-CloseDropDownOnSelection="true" OnEntryAdded="catalog_EntryAdded"></telerik:RadDropDownTree>
    </td>
    <td>
        <telerik:RadComboBox ID="obj" runat="server" LocalizationPath="~/Language" DataTextField="Name" DataValueField="Id" Filter="Contains" MarkFirstMatch="true" AppendDataBoundItems="false" ShowToggleImage="false" AllowCustomText="true" AutoPostBack="true" OnSelectedIndexChanged="obj_SelectedIndexChanged">
            <ItemTemplate>
                <%# Eval("Name") %><span style="display: none;"><%# Eval("PinYin") %></span>
            </ItemTemplate>
        </telerik:RadComboBox>
    </td>
    <td>
        <asp:Label ID="unit" runat="server"></asp:Label>
    </td>
    <td>
        <asp:Label ID="specification" runat="server"></asp:Label>
    </td>
    <td>
        <asp:Label ID="stored" runat="server"></asp:Label>
    </td>
    <td>
        <telerik:RadNumericTextBox ID="amount" runat="server" Width="80" NumberFormat-DecimalDigits="2" DataType="System.Decimal" AllowOutOfRangeAutoCorrect="true" ClientEvents-OnValueChanged="calc"></telerik:RadNumericTextBox>
    </td>
    <td>
        <telerik:RadNumericTextBox ID="priceSet" runat="server" Width="80" NumberFormat-DecimalDigits="2" DataType="System.Decimal" AllowOutOfRangeAutoCorrect="true" ClientEvents-OnValueChanged="calc"></telerik:RadNumericTextBox>
    </td>
    <td>
        <telerik:RadNumericTextBox ID="money" runat="server" Width="80" NumberFormat-DecimalDigits="2" DataType="System.Decimal" AllowOutOfRangeAutoCorrect="true" NumberFormat-AllowRounding="false" ToCalc="calc" ClientEvents-OnValueChanged="calcTotal"></telerik:RadNumericTextBox>
    </td>
    <td style='<%# (Depot.Featured(Models.DepotType.幼儿园) ? "display: ;": "display: none;") %>'>
        <telerik:RadTextBox ID="age" runat="server" Width="100"></telerik:RadTextBox>
    </td>
    <td>
        <telerik:RadTextBox ID="place" runat="server" Width="100"></telerik:RadTextBox>
    </td>
    <td>
        <telerik:RadTextBox ID="note" runat="server" Width="100"></telerik:RadTextBox>
    </td>
</tr>
