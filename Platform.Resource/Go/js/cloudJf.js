"use strict"
$.fn.extend({
	
	//鼠标悬停
	hoverList:function(){
		var _method=$(this);
		_method.each(function() {
			var hoverL=$(this).find("*[name='hoverList']");
			$(this).mouseover(function(){
				$(this).addClass('on').siblings().removeClass('on');
				parseInt(hoverL.width())>150?hoverL.css('width',150):'';
				hoverL.stop(true,true).slideDown(300);
			})
			$(this).mouseleave(function(){
				$(this).removeClass('on');
				hoverL.stop(true,true).slideUp(300);
			})
            
        });
	},
	
	
	//选项卡
	tabControl:function(tab,con,mor){
		$(this).each(function(){
			var _method=this;
			$(this).find(tab).click(function(){
				if(this.className=='more'){return false;}
				$(this).addClass('on').siblings().removeClass('on');
				$(_method).find(con).addClass('dis_none');
				$(_method).find(con).eq($(this).index()).removeClass('dis_none');
				//$(_method).find(tab+'.more').removeClass('on');
				return false;
			});
			$(this).find(mor).click(function(){
				window.location.href=$(this).attr('href');
			});
		});
	},
	
	//ZZ-星级评分
	grade:function(valObj,scoreObj,resObj){
		var resObj=resObj||false;
		$(this).click(function(event){
			var event=event||window.event;
			event.stopPropagation();
			var l=$(this).width();
			var result = "一般";
			var score=Math.ceil((event.pageX-$(this).offset().left)/(2*l)*10)*2;
			$(this).find('em').width(score*10+'%');
			valObj=$(this).find('input');
			valObj?valObj.val(score):false;			
			//alert("score: "+score+" valObj="+valObj.val());	
			//scoreObj.html(score*10);
			scoreObj?scoreObj.html(score+".0"):false;
			switch (score) {				
　　   			case 2:
 　　    			 result="很差";
 					 break;
 				case 4: 
				case 6:
 　　    			 result="一般";
 					 break;	
　　   			case 8:
　　     			 result="很好";
					 break;
　　   			default:
　　     			result="推荐";
				};
			resObj?resObj.html(result):false;	
		})
	},
	//ZZ
	
	//弹出框
	jumpBox:function(style){
		var _method=$(this);
		$(this).css({'position':'absolute'});
		$("body select").css('visibility','hidden');
		center($(this),style);
		$("#screen").css({'display':'block','width':'100%','height':$(document).height()});
		$(this).css({'display':'block'});
		$(this).find("*[name='close']").click(function(){
			$("body select").css('visibility','visible');	
			$("#screen").hide();
			_method.hide();
			return false;
		})
	}
	//end 弹出框
	
	
});



//取中间值JS
	function center(obj,style) {		//取中间值
	var screenWidth = $(window).width(), screenHeight = $(window).height(); 
	var scrolltop = $(document).scrollTop();
	var objLeft = (screenWidth - obj.width())/2 ;
	var objTop = (screenHeight - obj.height())/2 + scrolltop;
	
	obj.css({position:'absolute',left: objLeft + 'px', top: objTop + 'px'/*,'display': 'block'*/});
	
	$(window).bind('resize', function() { 
		center(obj,!loaded);
	});
	
	if(true==style){
		$(window).bind('scroll', function() { 
			objTop = (screenHeight - obj.height())/2 + $(document).scrollTop();
			obj.css({top:objTop+'px'});
		});	
	}else{
		$(window).unbind('scroll');
	}
}





//clear placeholder
;(function(f,h,$){var a='placeholder' in h.createElement('input'),d='placeholder' in h.createElement('textarea'),i=$.fn,c=$.valHooks,k,j;if(a&&d){j=i.placeholder=function(){return this};j.input=j.textarea=true}else{j=i.placeholder=function(){var l=this;l.filter((a?'textarea':':input')+'[placeholder]').not('.placeholder').bind({'focus.placeholder':b,'blur.placeholder':e}).data('placeholder-enabled',true).trigger('blur.placeholder');return l};j.input=a;j.textarea=d;k={get:function(m){var l=$(m);return l.data('placeholder-enabled')&&l.hasClass('placeholder')?'':m.value},set:function(m,n){var l=$(m);if(!l.data('placeholder-enabled')){return m.value=n}if(n==''){m.value=n;if(m!=h.activeElement){e.call(m)}}else{if(l.hasClass('placeholder')){b.call(m,true,n)||(m.value=n)}else{m.value=n}}return l}};a||(c.input=k);d||(c.textarea=k);$(function(){$(h).delegate('form','submit.placeholder',function(){var l=$('.placeholder',this).each(b);setTimeout(function(){l.each(e)},10)})});$(f).bind('beforeunload.placeholder',function(){$('.placeholder').each(function(){this.value=''})})}function g(m){var l={},n=/^jQuery\d+$/;$.each(m.attributes,function(p,o){if(o.specified&&!n.test(o.name)){l[o.name]=o.value}});return l}function b(m,n){var l=this,o=$(l);if(l.value==o.attr('placeholder')&&o.hasClass('placeholder')){if(o.data('placeholder-password')){o=o.hide().next().show().attr('id',o.removeAttr('id').data('placeholder-id'));if(m===true){return o[0].value=n}o.focus()}else{l.value='';o.removeClass('placeholder');l==h.activeElement&&l.select()}}}function e(){var q,l=this,p=$(l),m=p,o=this.id;if(l.value==''){if(l.type=='password'){if(!p.data('placeholder-textinput')){try{q=p.clone().attr({type:'text'})}catch(n){q=$('<input>').attr($.extend(g(this),{type:'text'}))}q.removeAttr('name').data({'placeholder-password':true,'placeholder-id':o}).bind('focus.placeholder',b);p.data({'placeholder-textinput':q,'placeholder-id':o}).before(q)}p=p.removeAttr('id').hide().prev().attr('id',o).show()}p.addClass('placeholder');p[0].value=p.attr('placeholder')}else{p.removeClass('placeholder')}}}(this,document,jQuery));

//operation on
$('input, textarea').placeholder();

