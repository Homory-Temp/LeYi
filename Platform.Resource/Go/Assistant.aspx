<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Assistant.aspx.cs" Inherits="Go.GoAssistant" %>

<%@ Import Namespace="Homory.Model" %>

<%@ Register Src="~/Control/CommonAssistant.ascx" TagPrefix="homory" TagName="CommonAssistant" %>
<%@ Register Src="~/Control/CommonTop.ascx" TagPrefix="homory" TagName="CommonTop" %>
<%@ Register Src="~/Control/CommonBottom.ascx" TagPrefix="homory" TagName="CommonBottom" %>



<!DOCTYPE html>
<html>
<head runat="server">
    <title>资源平台 - 学习助手</title>
    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8">
    <meta http-equiv="Pragma" content="no-cache">
    <script src="../Script/jquery.min.js"></script>
    <link href="../Style/common.css" rel="stylesheet" />
    <link href="../Style/public.css" rel="stylesheet" />
    <link href="../Style/mhzy.css" rel="stylesheet" />

    <link href="../Style/login1.css" rel="stylesheet" />
        <base target="_top" />


</head>
<body>
    <form runat="server">
        <telerik:RadScriptManager ID="Rsm" runat="server">
            <Scripts>
                <asp:ScriptReference Assembly="Telerik.Web.UI" Name="Telerik.Web.UI.Common.Core.js" />
                <asp:ScriptReference Assembly="Telerik.Web.UI" Name="Telerik.Web.UI.Common.jQuery.js" />
                <asp:ScriptReference Assembly="Telerik.Web.UI" Name="Telerik.Web.UI.Common.jQueryInclude.js" />
            </Scripts>
        </telerik:RadScriptManager>
        <homory:CommonTop runat="server" ID="CommonTop" />


        <div class="srx-bg22">
            <div class="srx-wrap">
                <div class="portalMain w1000 clearfix">
                    <div class="Main w1000 clearfix eduSource">
                        <div class="cl mt10">
                        </div>

                        <div class="Main w960 clearfix">
                            <div class="w160 fl">
                                <div class="xy_eduCategory  mgb10">
                                    <p class="tit">资源助手分类</p>
                                    <ul>
                                        <asp:Repeater runat="server" ID="course">
                                            <ItemTemplate>
                                                <li style="text-align: center;">
                                                    <h3><a href='<%# string.Format("../Go/Search?Course={0}", Eval("Id")) %>' style="text-align: center; margin: auto; background-image: none;"><%# Eval("Name") %></a>
                                                    </h3>

                                                </li>
                                            </ItemTemplate>
                                        </asp:Repeater>

                                    </ul>
                                </div>
                            </div>
                            <div class="xy_w570 fl mgl10">

                                <ul class="xy_search">
                                    <li class="s-con clearfix">
                                        <input type="text" class="inps" id="search_content" runat="server">
                                        <input type="button" value="检 索" class="btn" id="search_go" runat="server" onserverclick="search_go_ServerClick">
                                    </li>
                                </ul>
                                <div style="background-color: #FFF; margin-top: 8px;">


                                    <div id="tabH" class="tabControl" style="width: 575px; height: 280px; float: left; background-color: #FFF">

                                        <div class="box doing">
                                            <div style="width: 575px; margin: auto;">
                                                <div class="tabs">
                                                    <div class="box-hd" style="float: left; width: 140px; font-size: 20px; color: #555; font-weight: normal;">推荐学科资源</div>
                                                    <span name="tabTit">
                                                        <div class="tab">视频</div>
                                                        <div class="tab">文章</div>
                                                        <div class="tab">课件</div>
                                                        <div class="tab">试卷</div>
                                                        

                                                        <div style="margin-right: 10px; float: right;"><a href="../Go/Search">更多</a></div>
                                                    </span>
                                                </div>


                                                <div class="tabClear"></div>
                                                <div class="tabContents" style="border-top: 2px solid #EFEFEF;">
                                                     <div class="tabContent">
                                                        <homory:CommonAssistant runat="server" ID="CommonCatalog29" CatalogName="推荐" ResourceType="视频" />
                                                    </div>
                                                    <div class="tabContent">
                                                        <homory:CommonAssistant runat="server" ID="CommonCatalog30" CatalogName="推荐" ResourceType="文章" />
                                                    </div>
                                                    <div class="tabContent">
                                                        <homory:CommonAssistant runat="server" ID="CommonCatalog31" CatalogName="推荐" ResourceType="课件" />
                                                    </div>

                                                    <div class="tabContent">
                                                        <homory:CommonAssistant runat="server" ID="CommonCatalog32" CatalogName="推荐" ResourceType="试卷" />
                                                    </div>
                                                   

                                                </div>

                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div style="background-color: #FFF; margin-top: 8px;">


                                    <div id="tabA" class="tabControl" style="width: 575px; height: 280px; float: left; background-color: #FFF">

                                        <div class="box doing">
                                            <div style="width: 575px; margin: auto;">
                                                <div class="tabs">
                                                    <div name="jump_hm1" class="box-hd" style="float: left; width: 140px; font-size: 20px; color: #555; font-weight: normal;">语文学科资源</div>
                                                    <span name="tabTit">
                                                        <div class="tab">视频</div>
                                                        <div class="tab">文章</div>
                                                        <div class="tab">课件</div>
                                                        <div class="tab">试卷</div>
                                                        

                                                        <div style="margin-right: 10px; float: right;"><a href="../Go/Search">更多</a></div>
                                                    </span>
                                                </div>


                                                <div class="tabClear"></div>
                                                <div class="tabContents" style="border-top: 2px solid #EFEFEF;">
                                                     <div class="tabContent">
                                                        <homory:CommonAssistant runat="server" ID="CommonCatalog4" CatalogName="语文" ResourceType="视频" />
                                                    </div>
                                                    <div class="tabContent">
                                                        <homory:CommonAssistant runat="server" ID="CommonCatalog1" CatalogName="语文" ResourceType="文章" />
                                                    </div>
                                                    <div class="tabContent">
                                                        <homory:CommonAssistant runat="server" ID="CommonCatalog2" CatalogName="语文" ResourceType="课件" />
                                                    </div>

                                                    <div class="tabContent">
                                                        <homory:CommonAssistant runat="server" ID="CommonCatalog3" CatalogName="语文" ResourceType="试卷" />
                                                    </div>
                                                   

                                                </div>

                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div style="background-color: #FFF; margin-top: 8px;">


                                    <div id="tabB" class="tabControl" style="width: 575px; height: 280px; float: left; background-color: #FFF">

                                        <div class="box doing">
                                            <div style="width: 575px; margin: auto;">
                                                <div class="tabs">
                                                    <div name="jump_hm2" class="box-hd" style="float: left; width: 140px; font-size: 20px; color: #555; font-weight: normal;">数学学科资源</div>
                                                    <span name="tabTit">
                                                        <div class="tab">视频</div>
                                                        <div class="tab">文章</div>
                                                        <div class="tab">课件</div>
                                                        <div class="tab">试卷</div>
                                                        

                                                        <div style="margin-right: 10px; float: right;"><a href="../Go/Search">更多</a></div>
                                                    </span>
                                                </div>


                                                <div class="tabClear"></div>
                                                <div class="tabContents" style="border-top: 2px solid #EFEFEF;">
                                                    <div class="tabContent">
                                                        <homory:CommonAssistant runat="server" ID="CommonCatalog8" CatalogName="数学" ResourceType="视频" />
                                                    </div>

                                                    <div class="tabContent">
                                                        <homory:CommonAssistant runat="server" ID="CommonCatalog5" CatalogName="数学" ResourceType="文章" />
                                                    </div>
                                                    <div class="tabContent">
                                                        <homory:CommonAssistant runat="server" ID="CommonCatalog6" CatalogName="数学" ResourceType="课件" />
                                                    </div>

                                                    <div class="tabContent">
                                                        <homory:CommonAssistant runat="server" ID="CommonCatalog7" CatalogName="数学" ResourceType="试卷" />
                                                    </div>
                                                    
                                                </div>

                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div style="background-color: #FFF; margin-top: 8px;">


                                    <div id="tabC" class="tabControl" style="width: 575px; height: 280px; float: left; background-color: #FFF">

                                        <div class="box doing">
                                            <div style="width: 575px; margin: auto;">
                                                <div class="tabs">
                                                    <div name="jump_hm3" class="box-hd" style="float: left; width: 140px; font-size: 20px; color: #555; font-weight: normal;">英语学科资源</div>
                                                    <span name="tabTit">
                                                        <div class="tab">视频</div>
                                                        <div class="tab">文章</div>
                                                        <div class="tab">课件</div>
                                                        <div class="tab">试卷</div>
                                                        

                                                        <div style="margin-right: 10px; float: right;"><a href="../Go/Search">更多</a></div>
                                                    </span>
                                                </div>


                                                <div class="tabClear"></div>
                                                <div class="tabContents" style="border-top: 2px solid #EFEFEF;">
                                                    <div class="tabContent">
                                                        <homory:CommonAssistant runat="server" ID="CommonCatalog9" CatalogName="英语" ResourceType="视频" />
                                                    </div>

                                                    <div class="tabContent">
                                                        <homory:CommonAssistant runat="server" ID="CommonCatalog10" CatalogName="英语" ResourceType="文章" />
                                                    </div>
                                                    <div class="tabContent">
                                                        <homory:CommonAssistant runat="server" ID="CommonCatalog11" CatalogName="英语" ResourceType="课件" />
                                                    </div>

                                                    <div class="tabContent">
                                                        <homory:CommonAssistant runat="server" ID="CommonCatalog12" CatalogName="英语" ResourceType="试卷" />
                                                    </div>
                                                    
                                                </div>

                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div style="background-color: #FFF; margin-top: 8px;">


                                    <div id="tabD" class="tabControl" style="width: 575px; height: 280px; float: left; background-color: #FFF">

                                        <div class="box doing">
                                            <div style="width: 575px; margin: auto;">
                                                <div class="tabs">
                                                    <div name="jump_hm4" class="box-hd" style="float: left; width: 140px; font-size: 20px; color: #555; font-weight: normal;">音乐学科资源</div>
                                                    <span name="tabTit">
                                                        <div class="tab">视频</div>
                                                        <div class="tab">文章</div>
                                                        <div class="tab">课件</div>
                                                        <div class="tab">试卷</div>
                                                        

                                                        <div style="margin-right: 10px; float: right;"><a href="../Go/Search">更多</a></div>
                                                    </span>
                                                </div>


                                                <div class="tabClear"></div>
                                                <div class="tabContents" style="border-top: 2px solid #EFEFEF;">
                                                    <div class="tabContent">
                                                        <homory:CommonAssistant runat="server" ID="CommonCatalog13" CatalogName="音乐" ResourceType="视频" />
                                                    </div>

                                                    <div class="tabContent">
                                                        <homory:CommonAssistant runat="server" ID="CommonCatalog14" CatalogName="音乐" ResourceType="文章" />
                                                    </div>
                                                    <div class="tabContent">
                                                        <homory:CommonAssistant runat="server" ID="CommonCatalog15" CatalogName="音乐" ResourceType="课件" />
                                                    </div>

                                                    <div class="tabContent">
                                                        <homory:CommonAssistant runat="server" ID="CommonCatalog16" CatalogName="音乐" ResourceType="试卷" />
                                                    </div>
                                                    
                                                </div>

                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div style="background-color: #FFF; margin-top: 8px;">


                                    <div id="tabE" class="tabControl" style="width: 575px; height: 280px; float: left; background-color: #FFF">

                                        <div class="box doing">
                                            <div style="width: 575px; margin: auto;">
                                                <div class="tabs">
                                                    <div name="jump_hm5" class="box-hd" style="float: left; width: 140px; font-size: 20px; color: #555; font-weight: normal;">体育学科资源</div>
                                                    <span name="tabTit">
                                                        <div class="tab">视频</div>
                                                        <div class="tab">文章</div>
                                                        <div class="tab">课件</div>
                                                        <div class="tab">试卷</div>
                                                        

                                                        <div style="margin-right: 10px; float: right;"><a href="../Go/Search">更多</a></div>
                                                    </span>
                                                </div>


                                                <div class="tabClear"></div>
                                                <div class="tabContents" style="border-top: 2px solid #EFEFEF;">
                                                    <div class="tabContent">
                                                        <homory:CommonAssistant runat="server" ID="CommonCatalog17" CatalogName="体育" ResourceType="视频" />
                                                    </div>

                                                    <div class="tabContent">
                                                        <homory:CommonAssistant runat="server" ID="CommonCatalog18" CatalogName="体育" ResourceType="文章" />
                                                    </div>
                                                    <div class="tabContent">
                                                        <homory:CommonAssistant runat="server" ID="CommonCatalog19" CatalogName="体育" ResourceType="课件" />
                                                    </div>

                                                    <div class="tabContent">
                                                        <homory:CommonAssistant runat="server" ID="CommonCatalog20" CatalogName="体育" ResourceType="试卷" />
                                                    </div>
                                                    
                                                </div>

                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div style="background-color: #FFF; margin-top: 8px;">


                                    <div id="tabF" class="tabControl" style="width: 575px; height: 280px; float: left; background-color: #FFF">

                                        <div class="box doing">
                                            <div style="width: 575px; margin: auto;">
                                                <div class="tabs">
                                                    <div name="jump_hm6" class="box-hd" style="float: left; width: 140px; font-size: 20px; color: #555; font-weight: normal;">美术学科资源</div>
                                                    <span name="tabTit">
                                                        <div class="tab">视频</div>
                                                        <div class="tab">文章</div>
                                                        <div class="tab">课件</div>
                                                        <div class="tab">试卷</div>
                                                        

                                                        <div style="margin-right: 10px; float: right;"><a href="../Go/Search">更多</a></div>
                                                    </span>
                                                </div>


                                                <div class="tabClear"></div>
                                                <div class="tabContents" style="border-top: 2px solid #EFEFEF;">
                                                    <div class="tabContent">
                                                        <homory:CommonAssistant runat="server" ID="CommonCatalog21" CatalogName="美术" ResourceType="视频" />
                                                    </div>

                                                    <div class="tabContent">
                                                        <homory:CommonAssistant runat="server" ID="CommonCatalog22" CatalogName="美术" ResourceType="文章" />
                                                    </div>
                                                    <div class="tabContent">
                                                        <homory:CommonAssistant runat="server" ID="CommonCatalog23" CatalogName="美术" ResourceType="课件" />
                                                    </div>

                                                    <div class="tabContent">
                                                        <homory:CommonAssistant runat="server" ID="CommonCatalog24" CatalogName="美术" ResourceType="试卷" />
                                                    </div>
                                                    
                                                </div>

                                            </div>
                                        </div>
                                    </div>
                                </div>
                                  <div style="background-color: #FFF; margin-top: 8px;">


                                    <div id="tabG" class="tabControl" style="width: 575px; height: 280px; float: left; background-color: #FFF">

                                        <div class="box doing">
                                            <div style="width: 575px; margin: auto;">
                                                <div class="tabs">
                                                    <div name="jump_hm7" class="box-hd" style="float: left; width: 140px; font-size: 20px; color: #555; font-weight: normal;">综合学科资源</div>
                                                    <span name="tabTit">
                                                        <div class="tab">视频</div>
                                                        <div class="tab">文章</div>
                                                        <div class="tab">课件</div>
                                                        <div class="tab">试卷</div>
                                                        

                                                        <div style="margin-right: 10px; float: right;"><a href="../Go/Search">更多</a></div>
                                                    </span>
                                                </div>


                                                <div class="tabClear"></div>
                                                <div class="tabContents" style="border-top: 2px solid #EFEFEF;">
                                                    <div class="tabContent">
                                                        <homory:CommonAssistant runat="server" ID="CommonCatalog25" CatalogName="综合" ResourceType="视频" />
                                                    </div>

                                                    <div class="tabContent">
                                                        <homory:CommonAssistant runat="server" ID="CommonCatalog26" CatalogName="综合" ResourceType="文章" />
                                                    </div>
                                                    <div class="tabContent">
                                                        <homory:CommonAssistant runat="server" ID="CommonCatalog27" CatalogName="综合" ResourceType="课件" />
                                                    </div>

                                                    <div class="tabContent">
                                                        <homory:CommonAssistant runat="server" ID="CommonCatalog28" CatalogName="综合" ResourceType="试卷" />
                                                    </div>
                                                    
                                                </div>

                                            </div>
                                        </div>
                                    </div>
                                </div>





                                <script src="../Script/index.js"></script>
                            </div>





                            <div class="xy_w200 fr">
                                <div class="xy_zynum">
                                    <p class="line">
                                        <i class="zykicon"></i><span>已发布资源库<br>
                                            <em runat="server" id="count1"></em></em>份资源</span>
                                    </p>
                                    <p>
                                        <i class="gxicon png_bg"></i><span>本周更新<br>
                                            <em runat="server" id="count2"></em></em>份资源</span>
                                    </p>
                                    <p><a href="../Go/Center?DoPublish=Publish" class="scbtn pop_btn">上传我的资源</a></p>
                                </div>
                                <div class="xy_phlist mgt20">
                                    <h3>最新资源排行</h3>
                                    <ul>
                                        <asp:Repeater runat="server" ID="latest">
                                            <ItemTemplate>
                                                <li><em class='<%# string.Format("num{0}", Container.ItemIndex + 1) %>'><%# Container.ItemIndex + 1 %></em><p>
                                                    <a href='<%# string.Format("../Go/{1}?Id={0}", Eval("Id"), ((Homory.Model.ResourceType)Eval("Type")) == Homory.Model.ResourceType.视频 ? "ViewVideo" : "ViewPlain") %>'>
                                                        <%# Eval("Title").ToString().CutString(12, "...") %></a><br />
                                                    <a href='<%# string.Format("../Go/Personal?Id={0}", Eval("UserId")) %>'><%# U(Eval("UserId")).DisplayName %></a>&nbsp;@&nbsp;
                                                    <%# ((DateTime)Eval("Time")).FormatTime() %><i class="xy_kicon"></i>
                                            </ItemTemplate>
                                        </asp:Repeater>

                                    </ul>
                                </div>
                                <div class="xy_phlist mgt20">
                                    <h3>推荐资源排行</h3>
                                    <ul>
                                        <asp:Repeater runat="server" ID="popular">
                                            <ItemTemplate>
                                                <li><em class='<%# string.Format("num{0}", Container.ItemIndex + 1) %>'><%# Container.ItemIndex + 1 %></em><p>
                                                    <a href='<%# string.Format("../Go/{1}?Id={0}", Eval("Id"), ((Homory.Model.ResourceType)Eval("Type")) == Homory.Model.ResourceType.视频 ? "ViewVideo" : "ViewPlain") %>'>
                                                        <%# Eval("Title").ToString().CutString(12, "...") %></a><br />
                                                    <a href='<%# string.Format("../Go/Personal?Id={0}", Eval("UserId")) %>'><%# U(Eval("UserId")).DisplayName %></a>&nbsp;@&nbsp;
                                                    <%# ((DateTime)Eval("Time")).FormatTime() %><i class="xy_kicon"></i>
                                            </ItemTemplate>
                                        </asp:Repeater>

                                    </ul>
                                </div>
                            </div>
                        </div>


                        <div class="popWrap xy_supbox" style="width: 800px;">
                            <div class="popInner">
                                <a href="goResourceHome.html#" class="close" name="close" id="closeX">
                                    <img src="images/close-pop.png" width="19" height="19" class="png_bg"></a>
                                <h2>上传资源</h2>
                                <a href="goResourceHome.html#" class="close" name="close">
                                    <img src="images/close-pop.png" width="19" height="19" class="png_bg"></a>
                                <iframe id="uploadFrame" src="images/index.htm" width="800px" height="538px;" frameborder="0"></iframe>
                            </div>
                        </div>

                    </div>
                </div>
                <homory:CommonBottom runat="server" ID="CommonBottom" />

            </div>
        </div>
        <div>


        </div>
    </form>
</body>
</html>
