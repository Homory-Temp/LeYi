<%@ Control Language="C#" AutoEventWireup="true" CodeFile="TargetBody.ascx.cs" Inherits="Control_TargetBody" %>

<td><%# Eval("购置单号") %></td>
<td><%# Eval("发票编号") %></td>
<td><%# Eval("TimeNode").FromTimeNode() %></td>
<td><%# Eval("采购来源") %></td>
<td><%# Eval("使用对象") %></td>
<td><%# Eval("应付金额").ToMoney() %></td>
<td><%# Eval("实付金额").ToMoney() %></td>
<td><%# Eval("保管人") %></td>
<td><%# Eval("经手人") %></td>
<td style="cursor: pointer;">
    <span id="target_note" runat="server">清单简述</span>
    <telerik:RadToolTip ID="tooltip" runat="server" TargetControlID="target_note" Skin="Metro">
        <%# Eval("清单简述") %>
    </telerik:RadToolTip>
</td>
<td><%# Eval("操作人") %></td>
