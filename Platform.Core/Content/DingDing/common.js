function getUrlParms() {
    var args = new Object();
    var query = location.search.substring(1);
    var pairs = query.split("&");
    for (var i = 0; i < pairs.length; i++) {
        var pos = pairs[i].indexOf('=');
        if (pos == -1) continue;
        var argname = pairs[i].substring(0, pos);
        var value = pairs[i].substring(pos + 1);
        args[argname] = decodeURIComponent(value);
    }
    return args;
}

var corp_id;
var corp_secret;

var url_get_access_token = "https://oapi.dingtalk.com/gettoken";

var url_department_add = "https://oapi.dingtalk.com/department/create?access_token=";
var url_department_remove = "https://oapi.dingtalk.com/department/delete?access_token=";
var url_department_edit = "https://oapi.dingtalk.com/department/update?access_token=";
var url_department_list = "https://oapi.dingtalk.com/department/list";

var access_token = "";

var agent_id = "10612057";
var jsapi_ticket = "";
var noncestr = "Homory";
var time_stamp = "";
var current_url = "http://localhost:65132/sso.html";

var url_get_jsapi_ticket = "https://oapi.dingtalk.com/get_jsapi_ticket";

