/*页面加载动画*/
$(window).load(function(){
    $(".wrap").animate({ opacity: 1 }, 300);
    $("#loading").animate({ opacity: 0 }, 300);
    $("#loading").hide();
});
/*导航菜单显示隐藏*/
$(function () {
    $("#loading").height($(document).height());
    $(".navbtn").click(function () {
        $(".nav").fadeIn(300);
        $(".navbtn").fadeOut(250);
    });
    $(".close").click(function () {
        $(".nav").fadeOut(250);
        $(".navbtn").fadeIn(300);
    });
   /* myScroll = new iScroll( "wrapper",{
        hScrollbar:false,
        snap:true,
        momentum:false,
        onScrollEnd:function(){
            $ulId.find( "img.manHeadOn")
                .removeClass( "manHeadOn")
                .end().children( "li").eq( this.currPageX ).find( "img").addClass( "manHeadOn" );
        },
        onScrollStart:function(){
            if( this.currPageX == 5 ){
                myScroll.stop();
            }
        }
    } );*/



});
