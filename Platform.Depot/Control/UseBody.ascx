﻿<%@ Control Language="C#" AutoEventWireup="true" CodeFile="UseBody.ascx.cs" Inherits="Control_UseBody" %>

<td><%# Eval("TimeNode").FromTimeNode() %></td>
<td><%# Eval("Type") %></td>
<td><%# Eval("Name") %></td>
<td><%# Eval("Unit") %></td>
<td><%# Eval("Amount").ToAmount() %></td>
<td><%# decimal.Divide((decimal)Eval("Money"), (decimal)Eval("Amount")).ToMoney() %></td>
<td><%# Eval("Money").ToMoney() %></td>
<td><%# Eval("Age") %></td>
<td><%# Eval("User") %></td>
<td><%# Eval("Operator") %></td>
<td><%# Eval("Note") %></td>
