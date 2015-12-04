<%@ Control Language="C#" AutoEventWireup="true" CodeFile="InBody.ascx.cs" Inherits="Control_InBody" %>

<td><asp:HyperLink runat="server" ForeColor="#3E5A70" Target="_blank" Text='<%# Eval("Number") %>' NavigateUrl='<%# "../StoreQuery/Target?StoreId={0}&TargetId={1}".Formatted(StoreId, Eval("TargetId")) %>'></asp:HyperLink></td>
<td><%# Eval("TimeNode").FromTimeNode() %></td>
<td><%# Eval("Name") %></td>
<td><%# Eval("OrderSource") %></td>
<td><%# Eval("Unit") %></td>
<td><%# Eval("SourceAmount").ToAmount() %></td>
<td><%# decimal.Divide((decimal)Eval("SourceMoney"), (decimal)Eval("SourceAmount")).ToMoney() %></td>
<td><%# Eval("SourceMoney").ToMoney() %></td>
<td><%# Eval("Age") %></td>
<td><%# Eval("Place") %></td>
<td style="display: none;"><%# Eval("Responsible") %></td>
<td><%# Eval("Operator") %></td>
