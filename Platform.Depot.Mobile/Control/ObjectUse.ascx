<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ObjectUse.ascx.cs" Inherits="Control_ObjectUse" %>

<tr>
    <td style="display: none;">
        <telerik:RadDropDownTree ID="catalog" runat="server" AutoPostBack="true" DataTextField="Name" DataValueField="Id" DataFieldID="Id" DataFieldParentID="ParentId" DropDownSettings-CloseDropDownOnSelection="true" OnEntryAdded="catalog_EntryAdded"></telerik:RadDropDownTree>
    </td>
    <td style="display: none;">
        <asp:Label ID="unit" runat="server"></asp:Label>
    </td>
    <td style="display: none;">
        <asp:Label ID="specification" runat="server"></asp:Label>
    </td>
    <td style="text-align: left;">
        &nbsp;&nbsp;&nbsp;&nbsp;名称：<telerik:RadComboBox ID="obj" runat="server" MaxHeight="203" Width="120" LocalizationPath="~/Language" DataTextField="Name" DataValueField="Id" Filter="Contains" MarkFirstMatch="true" AppendDataBoundItems="true" ShowToggleImage="false" AllowCustomText="true" AutoPostBack="true" OnSelectedIndexChanged="obj_SelectedIndexChanged">
            <ItemTemplate>
                <%# Eval("Name") %><span style="display: none;"><%# Eval("PinYin") %></span>
            </ItemTemplate>
        </telerik:RadComboBox><br />
        &nbsp;&nbsp;&nbsp;&nbsp;类型：<telerik:RadComboBox ID="act" runat="server" LocalizationPath="~/Language" AutoPostBack="true" Width="120">
        </telerik:RadComboBox><br />
        &nbsp;&nbsp;&nbsp;&nbsp;库存：<asp:Label ID="stored" runat="server"></asp:Label><br />
        &nbsp;&nbsp;&nbsp;&nbsp;出库：<telerik:RadNumericTextBox ID="amount" runat="server" Width="120" NumberFormat-DecimalDigits="2" DataType="System.Decimal" AllowOutOfRangeAutoCorrect="true"></telerik:RadNumericTextBox>
        <telerik:RadComboBox ID="ordinalList" runat="server" MaxHeight="203" Width="120" Visible="false" AllowCustomText="false" CheckBoxes="true" LocalizationPath="~/Language"></telerik:RadComboBox>
    </td>
    <td style="display: none;">
        <telerik:RadTextBox ID="age" runat="server" Width="100"></telerik:RadTextBox>
    </td>
    <td style="display: none;">
        <telerik:RadTextBox ID="place" runat="server" Width="100"></telerik:RadTextBox>
    </td>
    <td style="display: none;">
        <telerik:RadTextBox ID="note" runat="server" Width="100"></telerik:RadTextBox>
    </td>
</tr>
