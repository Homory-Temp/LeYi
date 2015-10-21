<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ObjectInBody.ascx.cs" Inherits="Control_ObjectInBody" %>

<tr>
    <td>
        <input type="hidden" id="tid" runat="server" />
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
        <telerik:RadDatePicker ID="time" runat="server" LocalizationPath="~/Language" ShowPopupOnFocus="true" Width="100" AutoPostBack="true">
            <Calendar runat="server">
                <FastNavigationSettings TodayButtonCaption="今日" OkButtonCaption="确定" CancelButtonCaption="取消"></FastNavigationSettings>
            </Calendar>
            <DatePopupButton runat="server" Visible="false" />
        </telerik:RadDatePicker>
    </td>
    <td>
        <telerik:RadNumericTextBox ID="amount" runat="server" Width="80" NumberFormat-DecimalDigits="2" DataType="System.Decimal" AllowOutOfRangeAutoCorrect="true" ClientEvents-OnValueChanged="calc"></telerik:RadNumericTextBox>
    </td>
    <td>
        <telerik:RadNumericTextBox ID="perPrice" runat="server" Width="80" NumberFormat-DecimalDigits="2" DataType="System.Decimal" AllowOutOfRangeAutoCorrect="true" ClientEvents-OnValueChanged="calc"></telerik:RadNumericTextBox>
    </td>
    <td style="display: none;">
        <telerik:RadNumericTextBox ID="fee" runat="server" Width="80" NumberFormat-DecimalDigits="2" DataType="System.Decimal" AllowOutOfRangeAutoCorrect="true"></telerik:RadNumericTextBox>
    </td>
    <td>
        <telerik:RadNumericTextBox ID="money" runat="server" Width="80" NumberFormat-DecimalDigits="2" DataType="System.Decimal" AllowOutOfRangeAutoCorrect="true" NumberFormat-AllowRounding="false" ToCalc="calc" ClientEvents-OnValueChanged="calcTotal"></telerik:RadNumericTextBox>
    </td>
    <td>
        <telerik:RadTextBox ID="place" runat="server" Width="100"></telerik:RadTextBox>
    </td>
    <td>
        <telerik:RadTextBox ID="note" runat="server" Width="100"></telerik:RadTextBox>
    </td>
</tr>
