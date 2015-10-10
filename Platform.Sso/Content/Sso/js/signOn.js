var cookieDays = 30;
var clientCookieId = "541A5F9E-475D-33A2-CAD2-B4E9D1C49704";
var account = "543007AD06E4FCE3";
var password = "5E6711E155375218";

$(document).ready(function () {
	doInit();
});

function galleryLoaded(sender, args) {
    sender.playSlideshow();
}

function doInit() {
    try {
        if (getCookie(account) != null && getCookie(password) != null) {
            $("input[name='userName']").val(getCookie(account));
            $("input[name='userPassword']").val(getCookie(password));
            $("#autoPasswordCK").attr("checked", "checked");
            $("#autoPasswordLabel").html("记住密码&nbsp;&nbsp;");
        } else {
            $("#autoPasswordCK").removeAttr("checked");
            $("#autoPasswordLabel").html("不记住密码&nbsp;&nbsp;");
        }

        document.onkeydown = function (e) {
            var event = document.all ? window.event : e;
            if (event.keyCode == 13) {
                $("#buttonSign").click();
                return false;
            }
            return true;
        };

        $("input[name='userName']").focus();
    }
    catch (error) {
    }
}

function goFavFX() {
    $("#signThumbPost").click();
}

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

function delCookie(name) {
	var exp = new Date();
	exp.setTime(exp.getTime() - 1);
	var cval = getCookie(name);
	if (cval != null)
		document.cookie = clientCookieId + name + "=" + cval + ";expires=" + exp.toGMTString();
}

function signYN(obj) {
    if ($("#autoPasswordCK").attr("checked")) {
        $("#autoPasswordLabel").html("不记住密码&nbsp;&nbsp;");
        $("#autoPasswordCK").removeAttr("checked");
    } else {
        $("#autoPasswordLabel").html("记住密码&nbsp;&nbsp;");
        $("#autoPasswordCK").attr("checked", "checked");
    }
}


function doRemember() {
    if ($("#autoPasswordCK").attr("checked")) {
		setCookie(account, $("input[name='userName']").val().trim());
		setCookie(password, $("input[name='userPassword']").val().trim());
	} else {
		delCookie(account);
		delCookie(password);
	}
}
