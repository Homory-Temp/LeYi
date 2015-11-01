<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ObjectUse.ascx.cs" Inherits="Control_ObjectUse" %>

<tr>
    <td>
        <telerik:RadDropDownTree ID="catalog" runat="server" AutoPostBack="true" DataTextField="Name" DataValueField="Id" DataFieldID="Id" DataFieldParentID="ParentId" DropDownSettings-CloseDropDownOnSelection="true" OnEntryAdded="catalog_EntryAdded"></telerik:RadDropDownTree>
    </td>
    <td>
        <telerik:RadComboBox ID="obj" runat="server" MaxHeight="203" LocalizationPath="~/Language" DataTextField="Name" DataValueField="Id" Filter="Contains" MarkFirstMatch="true" AppendDataBoundItems="true" ShowToggleImage="false" AllowCustomText="true" AutoPostBack="true" OnSelectedIndexChanged="obj_SelectedIndexChanged">
            <ItemTemplate>
                <%# Eval("Name") %><span style="display: none;"><%# Eval("PinYin") %></span>
            </ItemTemplate>
        </telerik:RadComboBox>
    </td>
    <td>
        <telerik:RadComboBox ID="act" runat="server" LocalizationPath="~/Language" AutoPostBack="true">
        </telerik:RadComboBox>
    </td>
    <td>
        <asp:Label ID="unit" runat="server"></asp:Label>
    </td>
    <td>
        <asp:Label ID="brand" runat="server"></asp:Label>
    </td>
    <td>
        <asp:Label ID="specification" runat="server"></asp:Label>
    </td>
    <td>
        <asp:Label ID="stored" runat="server"></asp:Label>
    </td>
    <td>
        <telerik:RadNumericTextBox ID="amount" runat="server" Width="120" NumberFormat-DecimalDigits="2" DataType="System.Decimal" AllowOutOfRangeAutoCorrect="true"></telerik:RadNumericTextBox>
        <telerik:RadComboBox ID="ordinalList" runat="server" MaxHeight="203" Width="120" Visible="false" AllowCustomText="false" CheckBoxes="true" LocalizationPath="~/Language"></telerik:RadComboBox>
    </td>
    <td style="display: none;">
        <telerik:RadTextBox ID="age" runat="server" Width="100"></telerik:RadTextBox>
    </td>
    <td>
        <telerik:RadTextBox ID="place" runat="server" Width="100"></telerik:RadTextBox>
    </td>
    <td>
        <telerik:RadTextBox ID="note" runat="server" Width="100"></telerik:RadTextBox>
    </td>
</tr>
