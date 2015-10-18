<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ObjectUseBody.ascx.cs" Inherits="Control_ObjectUseBody" %>

<tr>
    <td>
        <input type="hidden" id="tid" runat="server" />
        <telerik:RadDropDownTree ID="catalog" runat="server" AutoPostBack="true" DataTextField="Name" DataValueField="Id" DataFieldID="Id" DataFieldParentID="ParentId" DropDownSettings-CloseDropDownOnSelection="true" OnEntryAdded="catalog_EntryAdded"></telerik:RadDropDownTree>
    </td>
    <td>
        <telerik:RadComboBox ID="obj" runat="server" LocalizationPath="~/Language" DataTextField="Name" DataValueField="Id" Filter="Contains" MarkFirstMatch="true" AppendDataBoundItems="true" ShowToggleImage="false" AllowCustomText="true" AutoPostBack="true" OnSelectedIndexChanged="obj_SelectedIndexChanged">
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
        <asp:Label ID="specification" runat="server"></asp:Label>
    </td>
    <td>
        <asp:Label ID="stored" runat="server"></asp:Label>
    </td>
    <td>
        <telerik:RadNumericTextBox ID="amount" runat="server" Width="80" NumberFormat-DecimalDigits="2" DataType="System.Decimal" AllowOutOfRangeAutoCorrect="true"></telerik:RadNumericTextBox>
    </td>
    <td>
        <telerik:RadTextBox ID="note" runat="server" Width="100"></telerik:RadTextBox>
    </td>
</tr>
