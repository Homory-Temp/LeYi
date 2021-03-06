﻿/// <reference path="jquery.min.js" />

var tabNames = new Array("tabA", "tabB", "tabC", "tabD", "tabE", "tabF", "tabG", "tabH");

function initTab(id) {
    $("#" + id + " .tab").each(function (i, tab) {
        if (i == 0) {
            $(tab).addClass("tabCurrent");
        }

        $(tab).click(function (a, c) {
            $("#" + id + " .tab").each(function (i, t) {
                $(t).removeClass("tabCurrent");
            });

            $(tab).addClass("tabCurrent");

            $("#" + id + " .tabMore").each(function (x, tabMore) {
                if (x == i) {
                    $(tabMore).show();
                } else {
                    $(tabMore).hide();
                }
            });

            $("#" + id + " .tabContent").each(function (x, tabContent) {
                if (x == i) {
                    $(tabContent).show();
                } else {
                    $(tabContent).hide();
                }
            });
        });
    });

    $("#" + id + " .tabMore").each(function (i, tabMore) {
        if (i > 0) {
            $(tabMore).hide();
        }
    });

    $("#" + id + " .tabContent").each(function (i, tabContent) {
        if (i > 0) {
            $(tabContent).hide();
        }
    });
}

function initTabs() {
    for (var i = 0; i < tabNames.length; i++)
        initTab(tabNames[i]);
}

function getQueryString(name) {
    var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)", "i");
    var r = window.location.search.substr(1).match(reg);
    if (r != null) return unescape(r[2]); return null;
}

initTabs();

function selectInnerTab(index) {
    $("#tabA .tabs .tab").each(function (i, t) {
        if (i == index) {
            $(t).addClass("tabCurrent");
        } else
            $(t).removeClass("tabCurrent");
    });

    $("#tabA .tabContents .tabContent").each(function (x, tabContent) {
        if (x == index) {
            $(tabContent).show();
        } else {
            $(tabContent).hide();
        }
    });
}

var q = getQueryString("initTab");

if (q) {
    selectInnerTab(q);
}
