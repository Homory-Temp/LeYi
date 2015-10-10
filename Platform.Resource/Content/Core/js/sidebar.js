/// <reference path="../../jQuery/jquery.min.js" />
/// <reference path="common.js" />
/// <reference path="../../Semantic/javascript/semantic.min.js" />

var clientCookieId = "541A5F9E-475D-33A2-CAD2-B4E9D1C49704";
var sideBarOpen = "5E99C6AB8C64F342";
var cookieDays = 30;

var current;

$(document).ready(function () {
	if (getCookie(sideBarOpen) == null)
		setCookie(sideBarOpen, "1");

	document.onkeydown = function(e) {
		var event = document.all ? window.event : e;
		if (event.keyCode == 38) {
			$("#sidebar").sidebar("hide");
			setCookie(sideBarOpen, "0");
			return false;
		}
		if (event.keyCode == 40) {
			$("#sidebar").sidebar("show");
			setCookie(sideBarOpen, "1");
			return false;
		}
		return true;
	};

	if (getCookie(sideBarOpen) == "1") {
		$("#sidebar").sidebar("show");
	} else {
		$("#sidebar").sidebar("hide");
	}

	$("#sidebarSwitcher").click(function () {
		$("#sidebar").sidebar("toggle");
		setCookie(sideBarOpen, getCookie(sideBarOpen) == "1" ? "0" : "1");
	});

	$("#sidebar .sub.menu .item").each(function (i, obj) {
	    $(obj).css("margin-left", "1.72em");
	    var l = top.location.href.substr(top.location.href.lastIndexOf('/') + 1);
	    if (l.indexOf("?") > 0)
	        l = l.substring(0, l.indexOf('?'));
	    if ($(this).attr("href") == l) {
			$(this).find("i").addClass("play");
			$("#headInfo").text($(this).parent().parent().find(".circular.button").text() + " - " + $(this).text());
			current = $(this).parent(".sub.menu").attr("data-id");
        }
	});

	$(selectMenu());

	$("#sidebar .circular.button").each(function (i, obj) {
	    $(obj).click(function () {
	        current = $(this).parent().find(".sub.menu").attr("data-id");
	        selectMenu();
		});
	});
});

function setCookie(name, value) {
	var exp = new Date();
	exp.setTime(exp.getTime() + cookieDays * 24 * 60 * 60 * 1000);
	document.cookie = clientCookieId + name + "=" + escape(value) + ";expires=" + exp.toGMTString();
}

function getCookie(name) {
	var arr, reg = new RegExp("(^| )" + clientCookieId + name + "=([^;]*)(;|$)");
	if (arr = document.cookie.match(reg))
		return unescape(arr[2]);
	else
		return null;
}

function selectMenu() {
    $("#sidebar .sub.menu .item").each(function (i, obj) {
      if ($(this).parent(".sub.menu").attr("data-id") == current) {
      $(this).show();
    } else {
      $(this).hide();
    }
  });
}

function nameClick() {
	$("#sidebar").sidebar("toggle");
	setCookie(sideBarOpen, getCookie(sideBarOpen) == "1" ? "0" : "1");
}