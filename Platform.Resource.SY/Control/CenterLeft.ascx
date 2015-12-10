<%@ Control Language="C#" AutoEventWireup="true" CodeFile="CenterLeft.ascx.cs" Inherits="Control_CenterLeft" %>

<div class="srx-left" style="height: 1500px;">

                        <div class="u-l-info" id="userInfoBox">
                            <div class="u-li-pro clearfix" id="faceBox">
                                <a href="" class="srx-face">
                                    <asp:Image runat="server" ID="icon" Width="50" Height="50" />
                                </a>
                                <div class="u-lip-name ft14">
                                    <asp:Label runat="server" ID="name"></asp:Label>
                                </div>
                            </div>

                            <ul class="u-li-data clearfix mt10">
                                <li class="u-lid-flw"><em class="f16">
                                    <a href="../Go/CenterAttend"><asp:Label runat="server" ID="favourite"></asp:Label></a>
                                        </em><span class="f12"><label>关注</label></span>
                                </li>
                                <li class="u-lid-fns">

                                    <em class="f16">
                                        <a href="../Go/CenterFavor"><asp:Label runat="server" ID="fav"></asp:Label></a>
                                    </em><span class="f12">
                                        <label>收藏</label></span>
                                </li>
                                <li class="u-lid-cn">
                                    <em class="f16">
                                        <asp:Label runat="server" ID="honor"></asp:Label>
                                    </em><span class="f12">
                                        <label>荣誉</label></span>
                                </li>
                            </ul>
                            <table style="margin-top: 10px;">
                                <tr>
                                    <td>视频：<a href="../Go/CenterResource?initTab=0">
                                        <span id="cc4" runat="server"></span>

                                        </a></td>
                                    <td width="10"></td>
                                    <td>文章：<a href="../Go/CenterResource?initTab=1">
                                        <span id="cc1" runat="server"></span>
                                    </a></td>

                                </tr>
                                <tr>
                                    <td>课件：<a href="../Go/CenterResource?initTab=2">

                                        <span id="cc2" runat="server"></span>
                                        </a></td>
                                    <td width="10"></td>
                                    <td>试卷：<a href="../Go/CenterResource?initTab=3">
                                        <span id="cc3" runat="server"></span>

                                       </a></td>

                                </tr>
                            </table>
                        </div>


                        <%--左侧导航菜单--%>

                        <ul class="u-l-nav u-ln-cur-1 mt20" style="font-size: 14px">
                            <li class="u-ln">
                                <a href="../Go/Center" target="_top" class="u-ln-a u-ln-5"><i class="icon20 icon20-app-class"></i>资源互动</a>
                            </li>
                            <li class="u-ln">
                                <a id="toPublish" style="cursor:pointer;" onclick="popupPublish();" class="u-ln-a u-ln-5"><i class="icon20 icon20-app-new"></i>资源发布</a>
                            </li>
                            <li class="u-ln">
                                <a href="../Go/CenterResource" target="_top" class="u-ln-a u-ln-5"><i class="icon20 icon20-app-class"></i>资源管理</a>
                            </li>
                            <li class="u-ln">
                                <a href="../Go/CenterStudio.aspx" class="u-ln-a u-ln-6"><i class="icon20 icon20-app-school"></i>名师工作室</a>
                            </li>
                            <li class="u-ln">
                                <a href="../Go/CenterGroup.aspx" class="u-ln-a u-ln-6"><i class="icon20 icon20-app-school"></i>教研团队</a>
                            </li>
                            <li class="u-ln">
                                <a href="../Go/Centernote.aspx" class="u-ln-a u-ln-7"><i class="icon20 icon20-app-quwei"></i>我的听课笔记</a>
                            </li>
                             <li class="u-ln">
                                <a href="../Go/CenterFavor.aspx" class="u-ln-a u-ln-7"><i class="icon20 icon20-app-quwei"></i>我的收藏</a>
                            </li>
                             <li class="u-ln">
                                <a href="../Go/CenterAttend.aspx" class="u-ln-a u-ln-7"><i class="icon20 icon20-app-quwei"></i>我的关注</a>
                            </li>
                        </ul>
                    </div>
