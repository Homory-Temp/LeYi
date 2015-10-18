<%@ Control Language="C#" AutoEventWireup="true" CodeFile="RecordInBody.ascx.cs" Inherits="Control_TargetBody" %>

<td><%# Eval("类别") %></td>
<td><%# Eval("物资名称") %></td>
<td><%# Eval("单位") %></td>
<td><%# Eval("规格") %></td>
<td><%# Eval("入库日期").FromTimeNode() %></td>
<td><%# Eval("数量").ToAmount() %></td>
<td><%# Eval("单价").ToMoney() %></td>
<td style="display: none;"><%# Eval("优惠价").ToMoney() %></td>
<td><%# Eval("合计").ToMoney() %></td>
<td><%# Eval("存放地") %></td>
<td><%# Eval("备注") %></td>
