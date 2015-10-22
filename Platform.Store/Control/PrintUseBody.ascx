<%@ Control Language="C#" AutoEventWireup="true" CodeFile="PrintUseBody.ascx.cs" Inherits="Control_PrintUseBody" %>

<td><%# Eval("Name") %></td>
<td><%# Eval("Catalog") %></td>
<td><%# Eval("Unit") %></td>
<td><%# Eval("Type") %></td>
<td><%# Eval("Amount").ToAmount() %></td>
<td><%# Eval("PerPrice").ToAmount() %></td>
<td><%# Eval("Money").ToMoney() %></td>
<td><%# Eval("Specification") %></td>
<td><%# Eval("Note") %></td>
