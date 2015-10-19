<%@ Control Language="C#" AutoEventWireup="true" CodeFile="InBody.ascx.cs" Inherits="Control_InBody" %>

<td><%# Eval("Number") %></td>
<td><%# Eval("TimeNode").FromTimeNode() %></td>
<td><%# Eval("Name") %></td>
<td><%# Eval("Unit") %></td>
<td><%# Eval("SourceAmount").ToAmount() %></td>
<td><%# decimal.Divide((decimal)Eval("SourceMoney"), (decimal)Eval("SourceAmount")).ToMoney() %></td>
<td><%# Eval("SourceMoney").ToMoney() %></td>
<td><%# Eval("Age") %></td>
<td><%# Eval("Place") %></td>
<td><%# Eval("Responsible") %></td>
<td><%# Eval("Operator") %></td>
<td><%# Eval("Note") %></td>
