<%@ Control Language="C#" AutoEventWireup="true" CodeFile="PrintInBody.ascx.cs" Inherits="Control_PrintInBody" %>

<td><%# Eval("物资名称") %></td>
<td><%# Eval("类别") %></td>
<td><%# Eval("单位") %></td>
<td><%# Eval("数量").ToAmount() %></td>
<td><%# Eval("单价").ToMoney() %></td>
<td><%# Eval("合计").ToMoney() %></td>
<td><%= OrderSource %></td>
<td><%# Eval("规格") %></td>
<td><%# Eval("备注") %></td>
