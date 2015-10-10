<%@ Control Language="C#" AutoEventWireup="true" CodeFile="HomeVideo.ascx.cs" Inherits="Control.ControlHomeGroupVideo" %>
<%@ Import Namespace="Homory.Model" %>

<div class="c-pc-left fl">

    <div class="c-p-title">

        <div class="box-hd">智能推荐</div>

        <a href="../Go/Catalog">更多视频</a>
    </div>

    <ul class="c-p-list clearfix" style="margin-left: 8px">

        <asp:Repeater runat="server" ID="video_random">
            <ItemTemplate>
                <li class="c-p-box1">
                    <div>
                        <a href='<%# string.Format("../Go/ViewVideo?Id={0}", Eval("Id")) %>'>
                            <img src='<%# Eval("Image") %>' width="140" height="140" alt=""></a>
                    </div>

                    <div><a href='<%# string.Format("../Go/ViewVideo?Id={0}", Eval("Id")) %>' class="dbt"><%# Eval("Title").ToString().CutString(9, "...") %></a></div>

                    <div class="c-color-a"></div>

                    <div>

                        <a href='<%# string.Format("../Go/Personal?Id={0}", Eval("UserId")) %>' class="fbr"><%# UC(Eval("UserId")) + "&nbsp;" + U(Eval("UserId")).RealName %></a>


                    </div>
                    <div class="c-color-a"></div>

                    <div>

                        <a class="f12"><a href='<%# string.Format("../Go/Catalog.aspx?Id={0}", Eval("Id")) %>'><%# GetCatalogName((Resource)Container.DataItem) %></a>
                    </div>

                    <div class="c-pb-ul-box">
                        <ul class="clearfix c-pb-ul mt10">
                            <li class="fl">
                                <img src="../image/pic5.jpg" width="14" height="12"></li>
                            <li><a href='<%# string.Format("../Go/ViewVideo?Id={0}", Eval("Id")) %>' class="c-p-count"><em class="f14"><%# Eval("View") %></em></a><li>
                            <li class="fl">
                                <img src="../image/pic6.jpg" width="14" height="12"></li>
                            <li>
                                <a href='<%# string.Format("../Go/ViewVideo?Id={0}", Eval("Id")) %>' class="c-p-count"><em class="f14"><%# Eval("Comment") %></em></a>
                            </li>

                        </ul>
                    </div>
                </li>

            </ItemTemplate>
        </asp:Repeater>
    </ul>
    <ul class="diary-list">
        <asp:Repeater runat="server" ID="video_randomX">
            <ItemTemplate>
                <li class="clearfix">
                    <span class="fl  mr5">
                        <img src="../image/spbf.png" width="20" height="20"></span>
                    <a class="fl" href='<%# string.Format("../Go/Catalog.aspx?Id={0}", Eval("Id")) %>'>[<%# GetCatalogName((Resource)Container.DataItem) %>]</a>
                    <a class="fl" href='<%# string.Format("../Go/ViewVideo?Id={0}", Eval("Id")) %>' style="color: #227dc5; margin-left: 4px;"><%# Eval("Title") %></a>
                </li>
            </ItemTemplate>
        </asp:Repeater>
    </ul>

        <div class="c-p-title">
        <div class="box-hd">切片视频</div>
        <a href="../Go/Catalog">更多视频</a>
    </div>

    <ul class="c-p-list clearfix" style="margin-left: 8px">
        <asp:Repeater runat="server" ID="video_cut">
            <ItemTemplate>
                <li class="c-p-box1">
                    <div>
                        <a href='<%# string.Format("../Go/ViewVideo?Id={0}", Eval("Id")) %>'>
                            <img src='<%# Eval("Image") %>' width="140" height="140" alt=""></a>
                    </div>

                    <div><a href='<%# string.Format("../Go/ViewVideo?Id={0}", Eval("Id")) %>' class="dbt"><%# Eval("Title").ToString().CutString(9, "...") %></a></div>

                    <div class="c-color-a"></div>

                    <div>

                        <a href='<%# string.Format("../Go/Personal?Id={0}", Eval("UserId")) %>' class="fbr"><%# UC(Eval("UserId")) + "&nbsp;" + U(Eval("UserId")).RealName %></a>


                    </div>
                    <div class="c-color-a"></div>

                    <div>

                        <a class="f12"><a href='<%# string.Format("../Go/Catalog.aspx?Id={0}", Eval("Id")) %>'><%# GetCatalogName((Resource)Container.DataItem) %></a>
                    </div>

                    <div class="c-pb-ul-box" style="text-align: center; cursor:pointer;">
                        <%# FormatPeriod((Resource)Container.DataItem) %>
                    </div>
                </li>

            </ItemTemplate>
        </asp:Repeater>
    </ul>


    <div class="c-p-title">
        <div class="box-hd">最新视频</div>
        <a href="../Go/Catalog">更多视频</a>
    </div>

    <ul class="c-p-list clearfix" style="margin-left: 8px">

        <asp:Repeater runat="server" ID="video_latest">
            <ItemTemplate>
                <li class="c-p-box1">
                    <div>
                        <a href='<%# string.Format("../Go/ViewVideo?Id={0}", Eval("Id")) %>'>
                            <img src='<%# Eval("Image") %>' width="140" height="140" alt=""></a>
                    </div>

                    <div><a href='<%# string.Format("../Go/ViewVideo?Id={0}", Eval("Id")) %>' class="dbt"><%# Eval("Title").ToString().CutString(9, "...") %></a></div>

                    <div class="c-color-a"></div>

                    <div>

                        <a href='<%# string.Format("../Go/Personal?Id={0}", Eval("UserId")) %>' class="fbr"><%# UC(Eval("UserId")) + "&nbsp;" + U(Eval("UserId")).RealName %></a>


                    </div>
                    <div class="c-color-a"></div>

                    <div>

                        <a class="f12"><a href='<%# string.Format("../Go/Catalog.aspx?Id={0}", Eval("Id")) %>'><%# GetCatalogName((Resource)Container.DataItem) %></a>
                    </div>

                    <div class="c-pb-ul-box">
                        <ul class="clearfix c-pb-ul mt10">
                            <li class="fl">
                                <img src="../image/pic5.jpg" width="14" height="12"></li>
                            <li><a href='<%# string.Format("../Go/ViewVideo?Id={0}", Eval("Id")) %>' class="c-p-count"><em class="f14"><%# Eval("View") %></em></a><li>
                            <li class="fl">
                                <img src="../image/pic6.jpg" width="14" height="12"></li>
                            <li>
                                <a href='<%# string.Format("../Go/ViewVideo?Id={0}", Eval("Id")) %>' class="c-p-count"><em class="f14"><%# Eval("Comment") %></em></a>
                            </li>

                        </ul>
                    </div>
                </li>

            </ItemTemplate>
        </asp:Repeater>
    </ul>
    <div class="c-p-title">
        <div class="box-hd">最热视频</div>
        <a href="../Go/Catalog">更多视频</a>
    </div>

    <ul class="c-p-list clearfix" style="margin-left: 8px">
        <asp:Repeater runat="server" ID="video_popular">
            <ItemTemplate>
                <li class="c-p-box1">
                    <div>
                        <a href='<%# string.Format("../Go/ViewVideo?Id={0}", Eval("Id")) %>'>
                            <img src='<%# Eval("Image") %>' width="140" height="140" alt=""></a>
                    </div>

                    <div><a href='<%# string.Format("../Go/ViewVideo?Id={0}", Eval("Id")) %>' class="dbt"><%# Eval("Title").ToString().CutString(9, "...") %></a></div>

                    <div class="c-color-a"></div>

                    <div>

                        <a href='<%# string.Format("../Go/Personal?Id={0}", Eval("UserId")) %>' class="fbr"><%# UC(Eval("UserId")) + "&nbsp;" + U(Eval("UserId")).RealName %></a>


                    </div>
                    <div class="c-color-a"></div>

                    <div>

                        <a class="f12"><a href='<%# string.Format("../Go/Catalog.aspx?Id={0}", Eval("Id")) %>'><%# GetCatalogName((Resource)Container.DataItem) %></a>
                    </div>

                    <div class="c-pb-ul-box">
                        <ul class="clearfix c-pb-ul mt10">
                            <li class="fl">
                                <img src="../image/pic5.jpg" width="14" height="12"></li>
                            <li><a href='<%# string.Format("../Go/ViewVideo?Id={0}", Eval("Id")) %>' class="c-p-count"><em class="f14"><%# Eval("View") %></em></a><li>
                            <li class="fl">
                                <img src="../image/pic6.jpg" width="14" height="12"></li>
                            <li>
                                <a href='<%# string.Format("../Go/ViewVideo?Id={0}", Eval("Id")) %>' class="c-p-count"><em class="f14"><%# Eval("Comment") %></em></a>
                            </li>

                        </ul>
                    </div>
                </li>

            </ItemTemplate>
        </asp:Repeater>
    </ul>
</div>

