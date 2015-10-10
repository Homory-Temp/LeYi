<%@ Control Language="C#" AutoEventWireup="true" CodeFile="HomeCourseware.ascx.cs" Inherits="Control.ControlHomeCourseware" %>
<%@ Import Namespace="Homory.Model" %>
<div id="tabA" class="tabControl" style="width: 690px; height: 220px; float: left; background-color: #FFF">

    <div class="box doing">
        <div style="width: 688px; margin: auto; height: 44px;">
            <div class="tabs">
                <div class="box-hd" style="float: left; width: 68px">最新课件</div>
                <div class="tab" style="margin-top: -10px; margin-left: -6px;" onclick="window.open('Catalog#jump_hm1');">语文</div>
                <div class="tab" style="margin-top: -10px; margin-left: -6px;" onclick="window.open('Catalog#jump_hm2');">数学</div>
                <div class="tab" style="margin-top: -10px; margin-left: -6px;" onclick="window.open('Catalog#jump_hm3');">英语</div>
                <div class="tab" style="margin-top: -10px; margin-left: -6px;" onclick="window.open('Catalog#jump_hm4');">音乐</div>
                <div class="tab" style="margin-top: -10px; margin-left: -6px;" onclick="window.open('Catalog#jump_hm5');">体育</div>
                <div class="tab" style="margin-top: -10px; margin-left: -6px;" onclick="window.open('Catalog#jump_hm6');">美术</div>
                <div class="tab" style="margin-top: -10px; margin-left: -6px;" onclick="window.open('Catalog#jump_hm7');">综合</div>
            </div>

            <div class="tabClear"></div>
            <div class="tabContents">

                <div class="tabContent">
                    <ul class="diary1-list1">
                        <asp:Repeater runat="server" ID="A0">
                            <ItemTemplate>
                                 <li class="clearfix">
                                <table width="335" border="0" align="left" cellpadding="0" cellspacing="0" class="hot-list">
                                  <tr>
                                    <td width="20"  valign="middle">
                                        <img src='<%# string.Format("../Image/img/{0}.jpg", Eval("Thumbnail")) %>' /></td>
                                    <td width="175" valign="middle"><a class="fl" href='<%# string.Format("../Go/ViewPlain?Id={0}", Eval("Id")) %>'><%# Eval("Title").ToString().CutString(12) %></a></td>
                                    <td width="50"><a href='<%# string.Format("../Go/Personal?Id={0}", Eval("UserId")) %>'><%# U(Eval("UserId")).RealName %></a></td>
                                    <td width="80"><%# ((DateTime)Eval("Time")).FormatTime() %></td>
                                  </tr>
                                </table>
                               </li>
                            </ItemTemplate>
                        </asp:Repeater>
                    </ul>
                </div>
                <div class="tabContent">
                    <ul class="diary1-list1">
                        <asp:Repeater runat="server" ID="A1">
                            <ItemTemplate>

                                   <li class="clearfix">
                                <table width="335" border="0" align="left" cellpadding="0" cellspacing="0" class="hot-list">
                                  <tr>
                                    <td width="20"  valign="middle">
                                        <img src='<%# string.Format("../Image/img/{0}.jpg", Eval("Thumbnail")) %>' /></td>
                                    <td width="175" valign="middle"><a class="fl" href="<%# string.Format("../Go/ViewPlain?Id={0}", Eval("Id")) %>"><%# Eval("Title").ToString().CutString(12) %></a></td>
                                    <td width="50"><a href='<%# string.Format("../Go/Personal?Id={0}", Eval("UserId")) %>'><%# U(Eval("UserId")).RealName %></a></td>
                                    <td width="80"><%# ((DateTime)Eval("Time")).FormatTime() %></td>
                                  </tr>
                                </table>
                               </li>
                            </ItemTemplate>
                        </asp:Repeater>
                    </ul>
                </div>
                <div class="tabContent">
                    <ul class="diary1-list1">
                        <asp:Repeater runat="server" ID="A2">
                            <ItemTemplate>

                                  <li class="clearfix">
                                <table width="335" border="0" align="left" cellpadding="0" cellspacing="0" class="hot-list">
                                  <tr>
                                    <td width="20"  valign="middle">
                                        <img src='<%# string.Format("../Image/img/{0}.jpg", Eval("Thumbnail")) %>' /></td>
                                    <td width="175" valign="middle"><a class="fl" href="<%# string.Format("../Go/ViewPlain?Id={0}", Eval("Id")) %>"><%# Eval("Title").ToString().CutString(12) %></a></td>
                                    <td width="50"><a href='<%# string.Format("../Go/Personal?Id={0}", Eval("UserId")) %>'><%# U(Eval("UserId")).RealName %></a></td>
                                    <td width="80"><%# ((DateTime)Eval("Time")).FormatTime() %></td>
                                  </tr>
                                </table>
                               </li>
                            </ItemTemplate>
                        </asp:Repeater>
                    </ul>
                </div>
                <div class="tabContent">
                    <ul class="diary1-list1">
                        <asp:Repeater runat="server" ID="A3">
                            <ItemTemplate>

                                  <li class="clearfix">
                                <table width="335" border="0" align="left" cellpadding="0" cellspacing="0" class="hot-list">
                                  <tr>
                                    <td width="20"  valign="middle">
                                        <img src='<%# string.Format("../Image/img/{0}.jpg", Eval("Thumbnail")) %>' /></td>
                                    <td width="175" valign="middle"><a class="fl" href="<%# string.Format("../Go/ViewPlain?Id={0}", Eval("Id")) %>"><%# Eval("Title").ToString().CutString(12) %></a></td>
                                    <td width="50"><a href='<%# string.Format("../Go/Personal?Id={0}", Eval("UserId")) %>'><%# U(Eval("UserId")).RealName %></a></td>
                                    <td width="80"><%# ((DateTime)Eval("Time")).FormatTime() %></td>
                                  </tr>
                                </table>
                               </li>
                            </ItemTemplate>
                        </asp:Repeater>
                    </ul>
                </div>
                <div class="tabContent">
                    <ul class="diary1-list1">
                        <asp:Repeater runat="server" ID="A4">
                            <ItemTemplate>

                                  <li class="clearfix">
                                <table width="335" border="0" align="left" cellpadding="0" cellspacing="0" class="hot-list">
                                  <tr>
                                    <td width="20"  valign="middle">
                                        <img src='<%# string.Format("../Image/img/{0}.jpg", Eval("Thumbnail")) %>' /></td>
                                    <td width="175" valign="middle"><a class="fl" href="<%# string.Format("../Go/ViewPlain?Id={0}", Eval("Id")) %>"><%# Eval("Title").ToString().CutString(12) %></a></td>
                                    <td width="50"><a href='<%# string.Format("../Go/Personal?Id={0}", Eval("UserId")) %>'><%# U(Eval("UserId")).RealName %></a></td>
                                    <td width="80"><%# ((DateTime)Eval("Time")).FormatTime() %></td>
                                  </tr>
                                </table>
                               </li>
                            </ItemTemplate>
                        </asp:Repeater>
                    </ul>
                </div>
                <div class="tabContent">
                    <ul class="diary1-list1">
                        <asp:Repeater runat="server" ID="A5">
                            <ItemTemplate>

                                   <li class="clearfix">
                                <table width="335" border="0" align="left" cellpadding="0" cellspacing="0" class="hot-list">
                                  <tr>
                                    <td width="20"  valign="middle">
                                        <img src='<%# string.Format("../Image/img/{0}.jpg", Eval("Thumbnail")) %>' /></td>
                                    <td width="175" valign="middle"><a class="fl" href="<%# string.Format("../Go/ViewPlain?Id={0}", Eval("Id")) %>"><%# Eval("Title").ToString().CutString(12) %></a></td>
                                    <td width="50"><a href='<%# string.Format("../Go/Personal?Id={0}", Eval("UserId")) %>'><%# U(Eval("UserId")).RealName %></a></td>
                                    <td width="80"><%# ((DateTime)Eval("Time")).FormatTime() %></td>
                                  </tr>
                                </table>
                               </li>
                            </ItemTemplate>
                        </asp:Repeater>
                    </ul>
                </div>
                <div class="tabContent">
                    <ul class="diary1-list1">
                        <asp:Repeater runat="server" ID="A6">
                            <ItemTemplate>

                                   <li class="clearfix">
                                <table width="335" border="0" align="left" cellpadding="0" cellspacing="0" class="hot-list">
                                  <tr>
                                    <td width="20"  valign="middle">
                                        <img src='<%# string.Format("../Image/img/{0}.jpg", Eval("Thumbnail")) %>' /></td>
                                    <td width="175" valign="middle"><a class="fl" href="<%# string.Format("../Go/ViewPlain?Id={0}", Eval("Id")) %>"><%# Eval("Title").ToString().CutString(12) %></a></td>
                                    <td width="50"><a href='<%# string.Format("../Go/Personal?Id={0}", Eval("UserId")) %>'><%# U(Eval("UserId")).RealName %></a></td>
                                    <td width="80"><%# ((DateTime)Eval("Time")).FormatTime() %></td>
                                  </tr>
                                </table>
                               </li>
                            </ItemTemplate>
                        </asp:Repeater>
                    </ul>
                </div>
            </div>

        </div>
    </div>
</div>


<div style="clear: both"></div>

<div id="tabB" class="tabControl" style="width: 690px; height: 220px; float: left; background-color: #FFF">

    <div class="box doing">
        <div style="width: 688px; margin: auto; height: 44px;">
            <div class="tabs">
                <div class="box-hd" style="float: left; width: 68px">最热课件</div>
                <div class="tab" style="margin-top: -10px; margin-left: -6px;" onclick="window.open('Catalog#jump_hm1');">语文</div>
                <div class="tab" style="margin-top: -10px; margin-left: -6px;" onclick="window.open('Catalog#jump_hm2');">数学</div>
                <div class="tab" style="margin-top: -10px; margin-left: -6px;" onclick="window.open('Catalog#jump_hm3');">英语</div>
                <div class="tab" style="margin-top: -10px; margin-left: -6px;" onclick="window.open('Catalog#jump_hm4');">音乐</div>
                <div class="tab" style="margin-top: -10px; margin-left: -6px;" onclick="window.open('Catalog#jump_hm5');">体育</div>
                <div class="tab" style="margin-top: -10px; margin-left: -6px;" onclick="window.open('Catalog#jump_hm6');">美术</div>
                <div class="tab" style="margin-top: -10px; margin-left: -6px;" onclick="window.open('Catalog#jump_hm7');">综合</div>
            </div>

            <div class="tabClear"></div>
            <div class="tabContents">
                <div class="tabContent">
                    <ul class="diary1-list1">
                        <asp:Repeater runat="server" ID="B0">
                            <ItemTemplate>

                                   <li class="clearfix">
                                <table width="335" border="0" align="left" cellpadding="0" cellspacing="0" class="hot-list">
                                  <tr>
                                    <td width="20"  valign="middle">
                                        <img src='<%# string.Format("../Image/img/{0}.jpg", Eval("Thumbnail")) %>' /></td>
                                    <td width="175" valign="middle"><a class="fl" href="<%# string.Format("../Go/ViewPlain?Id={0}", Eval("Id")) %>"><%# Eval("Title").ToString().CutString(12) %></a></td>
                                    <td width="50"><a href='<%# string.Format("../Go/Personal?Id={0}", Eval("UserId")) %>'><%# U(Eval("UserId")).RealName %></a></td>
                                    <td width="80"><%# ((DateTime)Eval("Time")).FormatTime() %></td>
                                  </tr>
                                </table>
                               </li>
                            </ItemTemplate>
                        </asp:Repeater>
                    </ul>
                </div>
                <div class="tabContent">
                    <ul class="diary1-list1">
                        <asp:Repeater runat="server" ID="B1">
                            <ItemTemplate>

                                   <li class="clearfix">
                                <table width="335" border="0" align="left" cellpadding="0" cellspacing="0" class="hot-list">
                                  <tr>
                                    <td width="20"  valign="middle">
                                        <img src='<%# string.Format("../Image/img/{0}.jpg", Eval("Thumbnail")) %>' /></td>
                                    <td width="175" valign="middle"><a class="fl" href="<%# string.Format("../Go/ViewPlain?Id={0}", Eval("Id")) %>"><%# Eval("Title").ToString().CutString(12) %></a></td>
                                    <td width="50"><a href='<%# string.Format("../Go/Personal?Id={0}", Eval("UserId")) %>'><%# U(Eval("UserId")).RealName %></a></td>
                                    <td width="80"><%# ((DateTime)Eval("Time")).FormatTime() %></td>
                                  </tr>
                                </table>
                               </li>
                            </ItemTemplate>
                        </asp:Repeater>
                    </ul>
                </div>
                <div class="tabContent">
                    <ul class="diary1-list1">
                        <asp:Repeater runat="server" ID="B2">
                            <ItemTemplate>

                                   <li class="clearfix">
                                <table width="335" border="0" align="left" cellpadding="0" cellspacing="0" class="hot-list">
                                  <tr>
                                    <td width="20"  valign="middle">
                                        <img src='<%# string.Format("../Image/img/{0}.jpg", Eval("Thumbnail")) %>' /></td>
                                    <td width="175" valign="middle"><a class="fl" href="<%# string.Format("../Go/ViewPlain?Id={0}", Eval("Id")) %>"><%# Eval("Title").ToString().CutString(12) %></a></td>
                                    <td width="50"><a href='<%# string.Format("../Go/Personal?Id={0}", Eval("UserId")) %>'><%# U(Eval("UserId")).RealName %></a></td>
                                    <td width="80"><%# ((DateTime)Eval("Time")).FormatTime() %></td>
                                  </tr>
                                </table>
                               </li>
                            </ItemTemplate>
                        </asp:Repeater>
                    </ul>
                </div>
                <div class="tabContent">
                    <ul class="diary1-list1">
                        <asp:Repeater runat="server" ID="B3">
                            <ItemTemplate>

                                   <li class="clearfix">
                                <table width="335" border="0" align="left" cellpadding="0" cellspacing="0" class="hot-list">
                                  <tr>
                                    <td width="20"  valign="middle">
                                        <img src='<%# string.Format("../Image/img/{0}.jpg", Eval("Thumbnail")) %>' /></td>
                                    <td width="175" valign="middle"><a class="fl" href="<%# string.Format("../Go/ViewPlain?Id={0}", Eval("Id")) %>"><%# Eval("Title").ToString().CutString(12) %></a></td>
                                    <td width="50"><a href='<%# string.Format("../Go/Personal?Id={0}", Eval("UserId")) %>'><%# U(Eval("UserId")).RealName %></a></td>
                                    <td width="80"><%# ((DateTime)Eval("Time")).FormatTime() %></td>
                                  </tr>
                                </table>
                               </li>
                            </ItemTemplate>
                        </asp:Repeater>
                    </ul>
                </div>
                <div class="tabContent">
                    <ul class="diary1-list1">
                        <asp:Repeater runat="server" ID="B4">
                            <ItemTemplate>

                                   <li class="clearfix">
                                <table width="335" border="0" align="left" cellpadding="0" cellspacing="0" class="hot-list">
                                  <tr>
                                    <td width="20"  valign="middle">
                                        <img src='<%# string.Format("../Image/img/{0}.jpg", Eval("Thumbnail")) %>' /></td>
                                    <td width="175" valign="middle"><a class="fl" href="<%# string.Format("../Go/ViewPlain?Id={0}", Eval("Id")) %>"><%# Eval("Title").ToString().CutString(12) %></a></td>
                                    <td width="50"><a href='<%# string.Format("../Go/Personal?Id={0}", Eval("UserId")) %>'><%# U(Eval("UserId")).RealName %></a></td>
                                    <td width="80"><%# ((DateTime)Eval("Time")).FormatTime() %></td>
                                  </tr>
                                </table>
                               </li>
                            </ItemTemplate>
                        </asp:Repeater>
                    </ul>
                </div>
                <div class="tabContent">
                    <ul class="diary1-list1">
                        <asp:Repeater runat="server" ID="B5">
                            <ItemTemplate>

                                  <li class="clearfix">
                                <table width="335" border="0" align="left" cellpadding="0" cellspacing="0" class="hot-list">
                                  <tr>
                                    <td width="20"  valign="middle">
                                        <img src='<%# string.Format("../Image/img/{0}.jpg", Eval("Thumbnail")) %>' /></td>
                                    <td width="175" valign="middle"><a class="fl" href="<%# string.Format("../Go/ViewPlain?Id={0}", Eval("Id")) %>"><%# Eval("Title").ToString().CutString(12) %></a></td>
                                    <td width="50"><a href='<%# string.Format("../Go/Personal?Id={0}", Eval("UserId")) %>'><%# U(Eval("UserId")).RealName %></a></td>
                                    <td width="80"><%# ((DateTime)Eval("Time")).FormatTime() %></td>
                                  </tr>
                                </table>
                               </li>
                            </ItemTemplate>
                        </asp:Repeater>
                    </ul>
                </div>
                <div class="tabContent">
                    <ul class="diary1-list1">
                        <asp:Repeater runat="server" ID="B6">
                            <ItemTemplate>

                                   <li class="clearfix">
                                <table width="335" border="0" align="left" cellpadding="0" cellspacing="0" class="hot-list">
                                  <tr>
                                    <td width="20"  valign="middle">
                                        <img src='<%# string.Format("../Image/img/{0}.jpg", Eval("Thumbnail")) %>' /></td>
                                    <td width="175" valign="middle"><a class="fl" href="<%# string.Format("../Go/ViewPlain?Id={0}", Eval("Id")) %>"><%# Eval("Title").ToString().CutString(12) %></a></td>
                                    <td width="50"><a href='<%# string.Format("../Go/Personal?Id={0}", Eval("UserId")) %>'><%# U(Eval("UserId")).RealName %></a></td>
                                    <td width="80"><%# ((DateTime)Eval("Time")).FormatTime() %></td>
                                  </tr>
                                </table>
                               </li>
                            </ItemTemplate>
                        </asp:Repeater>
                    </ul>
                </div>
                 </div>
            </div>
        </div>
    </div>
    <div style="clear: both"></div>

    <div id="tabC" class="tabControl" style="width: 690px; height: 220px; float: left; background-color: #FFF">

        <div class="box doing">
            <div style="width: 688px; margin: auto; height: 44px;">
                <div class="tabs">
                    <div class="box-hd" style="float: left; width: 68px">在线课堂</div>
                <div class="tab" style="margin-top: -10px; margin-left: -6px;" onclick="window.open('Catalog#jump_hm1');">语文</div>
                <div class="tab" style="margin-top: -10px; margin-left: -6px;" onclick="window.open('Catalog#jump_hm2');">数学</div>
                <div class="tab" style="margin-top: -10px; margin-left: -6px;" onclick="window.open('Catalog#jump_hm3');">英语</div>
                <div class="tab" style="margin-top: -10px; margin-left: -6px;" onclick="window.open('Catalog#jump_hm4');">音乐</div>
                <div class="tab" style="margin-top: -10px; margin-left: -6px;" onclick="window.open('Catalog#jump_hm5');">体育</div>
                <div class="tab" style="margin-top: -10px; margin-left: -6px;" onclick="window.open('Catalog#jump_hm6');">美术</div>
                <div class="tab" style="margin-top: -10px; margin-left: -6px;" onclick="window.open('Catalog#jump_hm7');">综合</div>
                </div>

                <div class="tabClear"></div>
                <div class="tabContents">
                    <div class="tabContent">
                        <ul class="diary1-list1">
                            <asp:Repeater runat="server" ID="C0">
                                <ItemTemplate>

                                       <li class="clearfix">
                                <table width="335" border="0" align="left" cellpadding="0" cellspacing="0" class="hot-list">
                                  <tr>
                                    <td width="20"  valign="middle">
                                        <img src='<%# string.Format("../Image/img/{0}.jpg", Eval("Thumbnail")) %>' /></td>
                                    <td width="175" valign="middle"><a class="fl" href="<%# string.Format("../Go/ViewPlain?Id={0}", Eval("Id")) %>"><%# Eval("Title").ToString().CutString(12) %></a></td>
                                    <td width="50"><a href='<%# string.Format("../Go/Personal?Id={0}", Eval("UserId")) %>'><%# U(Eval("UserId")).RealName %></a></td>
                                    <td width="80"><%# ((DateTime)Eval("Time")).FormatTime() %></td>
                                  </tr>
                                </table>
                               </li>
                                </ItemTemplate>
                            </asp:Repeater>
                        </ul>
                    </div>
                    <div class="tabContent">
                        <ul class="diary1-list1">
                            <asp:Repeater runat="server" ID="C1">
                                <ItemTemplate>

                                      <li class="clearfix">
                                <table width="335" border="0" align="left" cellpadding="0" cellspacing="0" class="hot-list">
                                  <tr>
                                    <td width="20"  valign="middle">
                                        <img src='<%# string.Format("../Image/img/{0}.jpg", Eval("Thumbnail")) %>' /></td>
                                    <td width="175" valign="middle"><a class="fl" href="<%# string.Format("../Go/ViewPlain?Id={0}", Eval("Id")) %>"><%# Eval("Title").ToString().CutString(12) %></a></td>
                                    <td width="50"><a href='<%# string.Format("../Go/Personal?Id={0}", Eval("UserId")) %>'><%# U(Eval("UserId")).RealName %></a></td>
                                    <td width="80"><%# ((DateTime)Eval("Time")).FormatTime() %></td>
                                  </tr>
                                </table>
                               </li>
                                </ItemTemplate>
                            </asp:Repeater>
                        </ul>
                    </div>
                    <div class="tabContent">
                        <ul class="diary1-list1">
                            <asp:Repeater runat="server" ID="C2">
                                <ItemTemplate>

                                       <li class="clearfix">
                                <table width="335" border="0" align="left" cellpadding="0" cellspacing="0" class="hot-list">
                                  <tr>
                                    <td width="20"  valign="middle">
                                        <img src='<%# string.Format("../Image/img/{0}.jpg", Eval("Thumbnail")) %>' /></td>
                                    <td width="175" valign="middle"><a class="fl" href="<%# string.Format("../Go/ViewPlain?Id={0}", Eval("Id")) %>"><%# Eval("Title").ToString().CutString(12) %></a></td>
                                    <td width="50"><a href='<%# string.Format("../Go/Personal?Id={0}", Eval("UserId")) %>'><%# U(Eval("UserId")).RealName %></a></td>
                                    <td width="80"><%# ((DateTime)Eval("Time")).FormatTime() %></td>
                                  </tr>
                                </table>
                               </li>
                                </ItemTemplate>
                            </asp:Repeater>
                        </ul>
                    </div>
                    <div class="tabContent">
                        <ul class="diary1-list1">
                            <asp:Repeater runat="server" ID="C3">
                                <ItemTemplate>

                                      <li class="clearfix">
                                <table width="335" border="0" align="left" cellpadding="0" cellspacing="0" class="hot-list">
                                  <tr>
                                    <td width="20"  valign="middle">
                                        <img src='<%# string.Format("../Image/img/{0}.jpg", Eval("Thumbnail")) %>' /></td>
                                    <td width="175" valign="middle"><a class="fl" href="<%# string.Format("../Go/ViewPlain?Id={0}", Eval("Id")) %>"><%# Eval("Title").ToString().CutString(12) %></a></td>
                                    <td width="50"><a href='<%# string.Format("../Go/Personal?Id={0}", Eval("UserId")) %>'><%# U(Eval("UserId")).RealName %></a></td>
                                    <td width="80"><%# ((DateTime)Eval("Time")).FormatTime() %></td>
                                  </tr>
                                </table>
                               </li>
                                </ItemTemplate>
                            </asp:Repeater>
                        </ul>
                    </div>
                    <div class="tabContent">
                        <ul class="diary1-list1">
                            <asp:Repeater runat="server" ID="C4">
                                <ItemTemplate>

                                      <li class="clearfix">
                                <table width="335" border="0" align="left" cellpadding="0" cellspacing="0" class="hot-list">
                                  <tr>
                                    <td width="20"  valign="middle">
                                        <img src='<%# string.Format("../Image/img/{0}.jpg", Eval("Thumbnail")) %>' /></td>
                                    <td width="175" valign="middle"><a class="fl" href="<%# string.Format("../Go/ViewPlain?Id={0}", Eval("Id")) %>"><%# Eval("Title").ToString().CutString(12) %></a></td>
                                    <td width="50"><a href='<%# string.Format("../Go/Personal?Id={0}", Eval("UserId")) %>'><%# U(Eval("UserId")).RealName %></a></td>
                                    <td width="80"><%# ((DateTime)Eval("Time")).FormatTime() %></td>
                                  </tr>
                                </table>
                               </li>
                                </ItemTemplate>
                            </asp:Repeater>
                        </ul>
                    </div>
                    <div class="tabContent">
                        <ul class="diary1-list1">
                            <asp:Repeater runat="server" ID="C5">
                                <ItemTemplate>

                                        <li class="clearfix">
                                <table width="335" border="0" align="left" cellpadding="0" cellspacing="0" class="hot-list">
                                  <tr>
                                    <td width="20"  valign="middle">
                                        <img src='<%# string.Format("../Image/img/{0}.jpg", Eval("Thumbnail")) %>' /></td>
                                    <td width="175" valign="middle"><a class="fl" href="<%# string.Format("../Go/ViewPlain?Id={0}", Eval("Id")) %>"><%# Eval("Title").ToString().CutString(12) %></a></td>
                                    <td width="50"><a href='<%# string.Format("../Go/Personal?Id={0}", Eval("UserId")) %>'><%# U(Eval("UserId")).RealName %></a></td>
                                    <td width="80"><%# ((DateTime)Eval("Time")).FormatTime() %></td>
                                  </tr>
                                </table>
                               </li>
                                </ItemTemplate>
                            </asp:Repeater>
                        </ul>
                    </div>
                    <div class="tabContent">
                        <ul class="diary1-list1">
                            <asp:Repeater runat="server" ID="C6">
                                <ItemTemplate>
    <li class="clearfix">
                                <table width="335" border="0" align="left" cellpadding="0" cellspacing="0" class="hot-list">
                                  <tr>
                                    <td width="20"  valign="middle">
                                        <img src='<%# string.Format("../Image/img/{0}.jpg", Eval("Thumbnail")) %>' /></td>
                                    <td width="175" valign="middle"><a class="fl" href="<%# string.Format("../Go/ViewPlain?Id={0}", Eval("Id")) %>"><%# Eval("Title").ToString().CutString(12) %></a></td>
                                    <td width="50"><a href='<%# string.Format("../Go/Personal?Id={0}", Eval("UserId")) %>'><%# U(Eval("UserId")).RealName %></a></td>
                                    <td width="80"><%# ((DateTime)Eval("Time")).FormatTime() %></td>
                                  </tr>
                                </table>
                               </li>
                                </ItemTemplate>
                            </asp:Repeater>
                        </ul>
                    </div>
                    </div>
                </div>
            </div>
        </div>
