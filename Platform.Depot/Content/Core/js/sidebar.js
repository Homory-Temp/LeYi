/// <reference path="../../jQuery/jquery.min.js" />

var url = top.location.href;
var link = url.substring(url.indexOf('/Go/'), url.indexOf('?') > 0 ? url.indexOf('?') : url.length);
link = link.substring(4);

$(".nav .coreSB").each(function (i, obj) {
    if ($(this).attr("href") == link) {
        $("#title").text($(this).attr("alt"));
    }
});

$("#title").show();
