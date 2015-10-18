<%@ Control Language="C#" AutoEventWireup="true" CodeFile="PrintUseBody.ascx.cs" Inherits="Control_PrintUseBody" %>

<td><%# Obj.Name %></td>
<td><%# db.Value.GetCatalogPath(Obj.CatalogId).Single() %></td>
<td><%# Obj.Unit %></td>
<td><%# Eval("Type") %></td>
<td><%# Eval("Amount").ToAmount() %></td>
<td><%# Eval("Money").ToMoney() %></td>
<td><%# Obj.Specification %></td>
<td><%# Eval("Note") %></td>
