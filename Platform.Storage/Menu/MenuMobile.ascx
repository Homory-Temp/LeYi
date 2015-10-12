<%@ Control Language="C#" AutoEventWireup="true" CodeFile="MenuMobile.ascx.cs" Inherits="Menu" %>


<!--[if lt IE 9]>
        <script src="../Assets/javascripts/html5.js"></script>
    <![endif]-->
<!--[if (gt IE 8) | (IEMobile)]><!-->
<meta name="viewport" content="width=device-width, initial-scale=1.0, maximum-scale=1.0, user-scalable=0">
<meta name="apple-mobile-web-app-capable" content="yes">
<meta name="layoutmode" content="standard">
<meta name="apple-mobile-web-app-status-bar-style" content="black">
<meta name="renderer" content="webkit">
<script type="text/javascript">

    //html root的字体计算应该放在最前面，这样计算就不会有误差了/
    var _htmlFontSize = (function () {
        var clientWidth = document.documentElement ? document.documentElement.clientWidth : document.body.clientWidth;
        if (clientWidth > 640) clientWidth = 640;
        document.documentElement.style.fontSize = clientWidth * 1 / 16 + "px";
        return clientWidth * 1 / 16;
    })();



</script>


<link href="../images/common.css" rel="stylesheet" type="text/css">
<link href="../images/home.css" rel="stylesheet" type="text/css">
<link href="../images/header.css$1414753326.css" rel="stylesheet" type="text/css" media="all">
<link href="../images/add2home.css" rel="stylesheet" type="text/css" media="screen">
<script src="../images/add2home.js" type="text/javascript"></script>

<script src="../images/jquery-1.7.2.min.js" type="text/javascript"></script>
<!--导航菜单js-->
<script src="../images/menu.js" type="text/javascript"></script>
<!--导航菜单js结束-->
<!--[if lt IE 9]>
        <script src="../Assets/javascripts/html5.js"></script>
    <![endif]-->
<!--[if (gt IE 8) | (IEMobile)]><!-->
<link rel="stylesheet" href="../Assets/stylesheets/unsemantic-grid-responsive.css" />
<!--<![endif]-->
<!--[if (lt IE 9) & (!IEMobile)]>
        <link rel="stylesheet" href="../Assets/stylesheets/ie.css" />
    <![endif]-->
<link href="../Assets/stylesheets/common.css" rel="stylesheet" />




<telerik:RadCodeBlock runat="server">



    <header id="header">
        <img width="24" class="navbtn" src="../images/navbtn2.png">
        <a href="../Storage/StorageMobile.aspx">
            <asp:Image ID="logo" runat="server" AlternateText="Logo" ImageUrl="~/images/logo.png" /></a>
    </header>
    <!--导航菜开始-->
    <div class="nav">
        <div class="incd ">
            <div class="clearfix">
                <a href="../Storage/StorageMobile.aspx">
                    <img class="back" src="../images/back.png"></a><img
                        class="close right" src="../images/close.png">
            </div>
            <div class="incdbtn">
                <ul>
                    <li><a href='<%= "../StorageHome/HomeMobile?StorageId={0}".Formatted(StorageId) %>'>仓库首页</a></li>
                    <li><a href='<%= "../StorageCheck/ToCheck?StorageId={0}".Formatted(StorageId) %>'>物资盘库</a></li>
                    <li><a href='<%= "../StorageObject/ObjectMobile?StorageId={0}".Formatted(StorageId) %>'>物资查询</a></li>
                    <li><a href='<%= "../StorageQuery/QueryPersonalwap?StorageId={0}".Formatted(StorageId) %>'>借用查询</a></li>
                    <li><a href='<%= "../StorageScan/ScanQuery?StorageId={0}".Formatted(StorageId) %>'>流通查询</a></li>
                </ul>
            </div>
        </div>
    </div>
    <!--导航菜end-->
</telerik:RadCodeBlock>
