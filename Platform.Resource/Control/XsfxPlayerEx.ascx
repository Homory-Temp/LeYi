﻿<%@ Control Language="C#" AutoEventWireup="true" CodeFile="XsfxPlayerEx.ascx.cs" Inherits="HomoryPlayerEx" %>

<script src="../Content/jQuery/jquery.min.js"></script>
    <script>
        function GetUrlParms() {
            var args = new Object();
            var query = location.search.substring(1);//获取查询串   
            var pairs = query.split("&");//在逗号处断开   
            for (var i = 0; i < pairs.length; i++) {
                var pos = pairs[i].indexOf('=');//查找name=value   
                if (pos == -1) continue;//如果没有找到就跳过   
                var argname = pairs[i].substring(0, pos);//提取name   
                var value = pairs[i].substring(pos + 1);//提取value   
                args[argname] = unescape(value);//存为属性   
            }
            return args;
        }

        var args = new Object();
        args = GetUrlParms();
        var w = 738;
        var h = 500;
        if (args["W"]) {
            w = args["W"];
        }
        if (args["H"]) {
            h = args["H"];
        }
        
    </script>


<div id="a1" class="homory_player" style="width: 738px; height: 500px;"></div>
<script>
    $("#a1").css("width", w + "px");
    $("#a1").css("height", h + "px");
</script>
<input type="hidden" id="timer" value="Time" onclick="showtime();" />

<script type="text/javascript" src="../XsfxPlayer/XsfxPlayer/XsfxPlayer.js" charset="utf-8"></script>
<script type="text/javascript">
    //如果你不需要某项设置，可以直接删除，注意var flashvars的最后一个值后面不能有逗号
    function loadedHandler() {
        if (CKobject.getObjectById('ckplayer_a1').getType()) {//说明使用html5播放器
            //alert('播放器已加载，调用的是HTML5播放模块');
        }
        else {
            //alert('播放器已加载，调用的是Flash播放模块');
        }
    }
    function showtime() {
        var a = CKobject.getObjectById('ckplayer_a1');
        return a.getStatus()['time'];//当前播放长度
    }

    var flashvars = {
        f: '<%= Video %>',//视频地址
        a: '',//调用时的参数，只有当s>0的时候有效
        s: '0',//调用方式，0=普通方法（f=视频地址），1=网址形式,2=xml形式，3=swf形式(s>0时f=网址，配合a来完成对地址的组装)
        c: '0',//是否读取文本配置,0不是，1是
        x: '',//调用配置文件路径，只有在c=1时使用。默认为空调用的是ckplayer.xml
        i: '',//初始图片地址
        d: '',//暂停时播放的广告，swf/图片,多个用竖线隔开，图片要加链接地址，没有的时候留空就行
        u: '',//暂停时如果是图片的话，加个链接地址
        l: '',//前置广告，swf/图片/视频，多个用竖线隔开，图片和视频要加链接地址
        r: '',//前置广告的链接地址，多个用竖线隔开，没有的留空
        t: '',//视频开始前播放swf/图片时的时间，多个用竖线隔开
        y: '',//这里是使用网址形式调用广告地址时使用，前提是要设置l的值为空
        z: '',//缓冲广告，只能放一个，swf格式
        e: '1',//视频结束后的动作，0是调用js函数，1是循环播放，2是暂停播放并且不调用广告，3是调用视频推荐列表的插件，4是清除视频流并调用js功能和1差不多，5是暂停播放并且调用暂停广告
        v: '80',//默认音量，0-100之间
        p: '1',//视频默认0是暂停，1是播放，2是不加载视频
        h: '3',//播放http视频流时采用何种拖动方法，=0不使用任意拖动，=1是使用按关键帧，=2是按时间点，=3是自动判断按什么(如果视频格式是.mp4就按关键帧，.flv就按关键时间)，=4也是自动判断(只要包含字符mp4就按mp4来，只要包含字符flv就按flv来)
        q: '',//视频流拖动时参考函数，默认是start
        m: '',//让该参数为一个链接地址时，单击播放器将跳转到该地址
        o: '',//当p=2时，可以设置视频的时间，单位，秒
        w: '',//当p=2时，可以设置视频的总字节数
        g: '<%= PlayerStarts() %>',//视频直接g秒开始播放
        j: '',//跳过片尾功能，j>0则从播放多少时间后跳到结束，<0则总总时间-该值的绝对值时跳到结束
        k: '<%= PlayerTime() %>',//提示点时间，如 30|60鼠标经过进度栏30秒，60秒会提示n指定的相应的文字
        n: '<%= PlayerComment() %>',//提示点文字，跟k配合使用，如 提示点1|提示点2
        wh: '',//宽高比，可以自己定义视频的宽高或宽高比如：wh:'4:3',或wh:'1080:720'
        lv: '0',//是否是直播流，=1则锁定进度栏
        loaded: 'loadedHandler',//当播放器加载完成后发送该js函数loaded
        //调用播放器的所有参数列表结束
        //以下为自定义的播放器参数用来在插件里引用的
        my_url: encodeURIComponent(window.location.href)//本页面地址
        //调用自定义播放器参数结束
    };
    var params = { bgcolor: '#FFF', allowFullScreen: true, allowScriptAccess: 'always' };//这里定义播放器的其它参数如背景色（跟flashvars中的b不同），是否支持全屏，是否支持交互
    var video = ['<%= Video %>'];
    CKobject.embed('../XsfxPlayer/XsfxPlayer/XsfxPlayer.swf', 'a1', 'ckplayer_a1', '100%', '100%', false, flashvars, video, params);
    /*
      以上代码演示的兼容flash和html5环境的。如果只调用flash播放器或只调用html5请看其它示例
    */

    function ckmarqueeadv() { return '<%= PlayerComment() %>' }

    function videoLoadJs(s) {
        alert("执行了播放");
    }
    function playerstop() {
        //只有当调用视频播放器时设置e=0或4时会有效果
        alert('播放完成');
    }
    var _nn = 0;//用来计算实时监听的条数的，超过100条记录就要删除，不然会消耗内存

    function getstart() {
        var a = CKobject.getObjectById('ckplayer_a1').getStatus();
        var ss = '';
        for (var k in a) {
            ss += k + ":" + a[k] + '\n';
        }
        alert(ss);
    }
    function ckadjump() {
        alert('这里演示了点击跳过广告按钮后的执行的动作，如果注册会员可以做成直接跳过的效果。');
    }
    //开关灯
    var box = new LightBox();
    function closelights() {//关灯
        box.Show();
        CKobject._K_('a1').style.width = w + "px";
        CKobject._K_('a1').style.height = h + "px";
        CKobject.getObjectById('ckplayer_a1').width = 940;
        CKobject.getObjectById('ckplayer_a1').height = 550;
    }
    function openlights() {//开灯
        box.Close();
        CKobject._K_('a1').style.width = '600px';
        CKobject._K_('a1').style.height = '400px';
        CKobject.getObjectById('ckplayer_a1').width = 600;
        CKobject.getObjectById('ckplayer_a1').height = 400;
    }
    function changePrompt() {
        CKobject.getObjectById('ckplayer_a1').promptUnload();//卸载掉目前的
        CKobject.getObjectById('ckplayer_a1').changeFlashvars('{k->10|20|30}{n->重设提示点一|重设提示点二|重设提示点三}');
        CKobject.getObjectById('ckplayer_a1').promptLoad();//重新加载
    }
    function addflash() {
        if (CKobject.Flash()['f']) {
            CKobject._K_('a1').innerHTML = '';
            CKobject.embedSWF('../XsfxPlayer/XsfxPlayer/XsfxPlayer.swf', 'a1', 'ckplayer_a1', w, h, flashvars, params);
        }
        else {
            alert('该环境中没有安装flash插件，无法切换');
        }
    }
    function addhtml5() {
        if (CKobject.isHTML5()) {
            support = ['all'];
            CKobject._K_('a1').innerHTML = '';
            CKobject.embedHTML5('a1', 'ckplayer_a1', w, h, video, flashvars, support);
        }
        else {
            alert('该环境不支持html5，无法切换');
        }
    }
    function addListener() {
        if (CKobject.getObjectById('ckplayer_a1').getType()) {//说明使用html5播放器
            CKobject.getObjectById('ckplayer_a1').addListener('play', playHandler);
        }
        else {
            CKobject.getObjectById('ckplayer_a1').addListener('play', 'playHandler');
        }
    }
    function playHandler() {
        alert('因为注册了监听播放，所以弹出此内容，删除监听将不再弹出');
    }
    function removeListener() {//删除监听事件
        if (CKobject.getObjectById('ckplayer_a1').getType()) {//说明使用html5播放器
            CKobject.getObjectById('ckplayer_a1').removeListener('play', playHandler);
        }
        else {
            CKobject.getObjectById('ckplayer_a1').removeListener('play', 'playHandler');
        }
    }
</script>
