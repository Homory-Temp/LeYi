/// <reference path="notify.js" />

function notify(id, message, type) {
    if (id == null || id == "") {
        $.notify(message, type);
    } else {
        $(id).notify(message, type);
        $(id).focus();
    }
}

function isIE() {
    if (window.ActiveXObject || "ActiveXObject" in window)
        return true;
    else
        return false;
}
function isIE6() {
    return isIE() && !window.XMLHttpRequest;
}
function isIE7() {
    return isIE() && window.XMLHttpRequest && !document.documentMode;
}
function isIE8() {
    return isIE() && !-[1, ] && document.documentMode;
}
function fixIE(url) {
    if (isIE() && (isIE6() || isIE7() || isIE8()))
        top.location.href = url;
}
function fixedIE(url) {
    if (!(isIE() && (isIE6() || isIE7() || isIE8())))
        top.location.href = url;
}
