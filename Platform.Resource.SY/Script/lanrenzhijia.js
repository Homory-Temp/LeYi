/* 代码整理：懒人之家 www.lanrenzhijia.com */
$(function() {
	var sWidth = $("#focus1").width(); 
	var len = $("#focus1 ul li").length; 
	var index = 0;
	var picTimer;
	
	var btn = "<div class='btnBg'></div><div class='btn'>";
	for(var i=0; i < len; i++) {
		btn += "<span>" + (i+1) + "</span>";
	}
	btn += "</div>"
	$("#focus1").append(btn);
	$("#focus1 .btnBg").css("opacity",0.5);
	

	$("#focus1 .btn span").mouseenter(function() {
		index = $("#focus1 .btn span").index(this);
		showPics(index);
	}).eq(0).trigger("mouseenter");

	$("#focus1 ul").css("width",sWidth * (len + 1));
	
	$("#focus1 ul li div").hover(function() {
		$(this).siblings().css("opacity",0.7);
	},function() {
		$("#focus1 ul li div").css("opacity",1);
	});
	
	$("#focus1").hover(function() {
		clearInterval(picTimer);
	},function() {
		picTimer = setInterval(function() {
			if(index == len) { 
				showFirPic();
				index = 0;
			} else {
				showPics(index);
			}
			index++;
		},3000); 
	}).trigger("mouseleave");
	
	function showPics(index) { 
		var nowLeft = -index*sWidth; 
		$("#focus1 ul").stop(true,false).animate({"left":nowLeft},500); 
		$("#focus1 .btn span").removeClass("on").eq(index).addClass("on")
	}
	
	function showFirPic() { 
		$("#focus1 ul").append($("#focus1 ul li:first").clone());
		var nowLeft = -len*sWidth;
		$("#focus1 ul").stop(true,false).animate({"left":nowLeft},500,function() {
			$("#focus1 ul").css("left","0");
			$("#focus1 ul li:last").remove();
		}); 
		$("#focus1 .btn span").removeClass("on").eq(0).addClass("on");
	}
});

/* 代码整理：懒人之家 www.lanrenzhijia.com */