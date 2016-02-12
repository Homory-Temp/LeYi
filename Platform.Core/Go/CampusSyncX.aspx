<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CampusSyncX.aspx.cs" Inherits="Extended_CampusSyncX" %>

<%@ Register Src="~/Control/SideBar.ascx" TagPrefix="homory" TagName="SideBar" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=Edge,Chrome=1" />
    <meta name="viewport" content="width=device-width,initial-scale=1,minimum-scale=1,maximum-scale=1" />
    <title>基础平台</title>
    <script src="../Content/jQuery/jquery.min.js"></script>
    <link href="../assets/css/bootstrap.min.css" rel="stylesheet" />
    <link href="../assets/css/style-responsive.css" rel="stylesheet" />
    <link href="../assets/css/style.css" rel="stylesheet" />
    <script src="../assets/js/bootstrap.min.js"></script>
    <link href="../Content/Homory/css/common.css" rel="stylesheet" />
    <link href="../Content/Core/css/common.css" rel="stylesheet" />
    <script src="../Content/Homory/js/common.js"></script>
    <script src="../Content/Homory/js/notify.min.js"></script>
    <script src="../Content/DingDing/dingtalk.js"></script>
    <script src="../Content/DingDing/common.js"></script>
    <!--[if lt IE 9]>
	    <script src="../Content/Homory/js/html5shiv.js"></script>
	    <script src="../Content/Homory/js/respond.min.js"></script>
    <![endif]-->
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <homory:SideBar runat="server" ID="SideBar" />
            <script>
                $("#title").text("钉钉用户同步");
            </script>
            <script>
                // 检测钉钉参数
                function check_corp() {
                    //alert(corp_id);
                    //alert(corp_secret);
                }

                // 加载钉钉部门
                function load_depts() {
                    // 获取接口参数
                    $.ajax({
                        url: url_get_access_token,
                        type: "GET",
                        data: { "corpid": corp_id, "corpsecret": corp_secret },
                        dataType: "json",
                        success: function (data) {
                            access_token = data.access_token;
                            // 获取所有部门
                            $.ajax({
                                url: url_department_list,
                                type: "GET",
                                data: { "access_token": access_token },
                                dataType: "json",
                                success: function (data) {
                                    for (var i = 0; i < data.department.length; i++) {
                                        data.department[i].core = false;
                                    }
                                    $("#dept_list_ding").text(JSON.stringify(data.department));
                                    load_depts_ding($("#dept_list_ding").text());
                                },
                                error: function (err_o, err_e, err_t) {
                                }
                            });
                        },
                        error: function (err_o, err_e, err_t) {
                        }
                    });
                }

                var callback_count = 0;
                var callback_count_match = 0;
                var interval_id;

                // 匹配数据中心和钉钉的部门
                function match_ding_core() {
                    var core_obj = JSON.parse($("#dept_list_core").text());
                    callback_count_match = 0;
                    callback_count = 0;
                    $(core_obj).each(function (i) {
                        // 数据中心有钉ID的部门匹配钉钉部门
                        if (core_obj[i].dingid) {
                            do_match_core_ding(core_obj[i].dingid);
                        }
                        // 数据中心无钉ID的部门新增钉钉部门
                        else {
                            callback_count_match++;
                            add_ding(core_obj[i]);
                        }
                    });
                    // 每秒检测需要新增的钉钉部门是否全部新增完毕
                    interval_id = window.setInterval("check_reload_core();", 1000);
                }

                // 检测需要新增的钉钉部门是否全部新增完毕
                function check_reload_core() {
                    if (callback_count == callback_count_match) {
                        window.clearInterval(interval_id);
                        if (callback_count > 0) {
                            load_depts_core($("#dept_list_add").text());
                            $("#dept_list_add").text("");
                            top.location.reload();
                        }
                        remove_depts();
                    }
                }

                var kept_depts = ["1"];

                // 移除数据中心不存在的部门
                function remove_depts() {
                    var ding_obj = JSON.parse($("#dept_list_ding").text());
                    callback_count_match = 0;
                    callback_count = 0;
                    $(ding_obj).each(function (i) {
                        if (!ding_obj[i].core) {
                            if (jQuery.inArray(ding_obj[i].id, kept_depts) == -1) {
                                callback_count_match++;
                                del_dept(ding_obj[i].id);
                            }
                        }
                    });
                    interval_id = window.setInterval("check_del();", 1000);
                }

                function check_del() {
                    if (callback_count == callback_count_match) {
                        window.clearInterval(interval_id);
                        if (callback_count > 0) {
                            top.location.reload();
                        }
                        var p = getUrlParms();
                        if (!p["Done"])
                            update_depts();
                    }
                }

                // 更新部门
                function update_depts() {
                    var ding_obj = JSON.parse($("#dept_list_ding").text());
                    callback_count_match = 0;
                    callback_count = 0;
                    $(ding_obj).each(function (i) {
                        if (ding_obj[i].id != 1) {
                            var ding__id = ding_obj[i].id;
                            var obj = find_core_by_dingid(ding__id);
                            callback_count_match++;
                            edit_dept(ding__id, obj.name, obj.ordinal);
                        }
                    });
                    interval_id = window.setInterval("check_update();", 1000);
                }

                function edit_dept(dingid, name, ordinal) {
                    $.ajax({
                        url: url_department_edit + access_token,
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        data: JSON.stringify({ "name": name, "order": ordinal, "id": dingid }),
                        dataType: "json",
                        success: function (data) {
                            callback_count++;
                        },
                        error: function (err_o, err_e, err_t) {
                            callback_count++;
                        }
                    });
                }

                function check_update() {
                    window.clearInterval(interval_id);
                    if (callback_count == callback_count_match) {
                        if (callback_count > 0) {
                            top.location.href = top.location.href + "&Done=1";
                        }
                    }
                }

                function del_dept(id) {
                    $.ajax({
                        url: url_department_remove + access_token,
                        type: "GET",
                        data: { "id": id },
                        dataType: "json",
                        success: function (data) {
                            callback_count++;
                        },
                        error: function (err_o, err_e, err_t) {
                            callback_count++;
                        }
                    });
                }

                function find_core_by_id(pid) {
                    var r;
                    var core_obj = JSON.parse($("#dept_list_core").text());
                    $(core_obj).each(function (j) {
                        if (core_obj[j].id.toString() == pid.toString()) {
                            r = core_obj[j];
                        }
                    });
                    return r;
                }

                function find_core_by_dingid(did) {
                    var r;
                    var core_obj = JSON.parse($("#dept_list_core").text());
                    $(core_obj).each(function (j) {
                        if (core_obj[j].dingid.toString() == did.toString()) {
                            r = core_obj[j];
                        }
                    });
                    return r;
                }

                function add_ding(obj) {
                    var core_parent = find_core_by_id(obj.parentid);
                    if (!core_parent.ding || !core_parent.dingid) {
                        add_ding(core_parent);
                        return;
                    }
                    var pid = core_parent.dingid;
                    $.ajax({
                        url: url_department_add + access_token,
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        data: JSON.stringify({ "name": obj.name, "parentid": pid, "order": obj.ordinal, "createDeptGroup": true }),
                        dataType: "json",
                        success: function (data) {
                            if (data.errcode == 0) {
                                var text = obj.id + "*" + data.id;
                                $("#dept_list_add").text($("#dept_list_add").text() + "|" + text);
                            }
                            callback_count++;
                        },
                        error: function (err_o, err_e, err_t) {
                            callback_count++;
                        }
                    });
                }

                function do_match_core_ding(dingid) {
                    var ding_obj = JSON.parse($("#dept_list_ding").text());
                    $(ding_obj).each(function (i) {
                        if (ding_obj[i].id == dingid)
                            ding_obj[i].core = true;
                    });
                    $("#dept_list_ding").text(JSON.stringify(ding_obj));
                    load_depts_ding_x($("#dept_list_ding").text());
                }
            </script>
            <telerik:RadCodeBlock runat="server">
                <script>
                    // 显示钉钉部门
                    function load_depts_ding(text) {
                        $find("<%= panel.ClientID %>").ajaxRequest(text);
                        match_ding_core();
                    }

                    function load_depts_ding_x(text) {
                        $find("<%= panel.ClientID %>").ajaxRequest(text);
                    }

                    function load_depts_core(text) {
                        $find("<%= panel_depts_refresh.ClientID %>").ajaxRequest(text);
                    }
                </script>
            </telerik:RadCodeBlock>
        </div>
        <div id="dept_list_ding" runat="server" style="display: none;"></div>
        <telerik:RadAjaxPanel ID="panel" runat="server" CssClass="container-fluid" OnAjaxRequest="panel_AjaxRequest">
            <div class="row">
                <div class="col-md-12">
                    <div id="dept_list_ding_copy" runat="server" style="display: none;"></div>
                    <telerik:RadGrid ID="dept_grid_ding" runat="server" AutoGenerateColumns="false" OnNeedDataSource="dept_grid_ding_NeedDataSource">
                        <MasterTableView runat="server">
                            <HeaderStyle HorizontalAlign="Center" Width="20%" />
                            <ItemStyle HorizontalAlign="Center" Width="20%" />
                            <AlternatingItemStyle HorizontalAlign="Center" Width="20%" />
                            <Columns>
                                <telerik:GridBoundColumn DataField="id" HeaderText="钉钉编号"></telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="name" HeaderText="钉钉名称"></telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="parentid" HeaderText="钉钉父级编号"></telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="createDeptGroup" HeaderText="自动创建群组"></telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="autoAddUser" HeaderText="自动添加用户"></telerik:GridBoundColumn>
                            </Columns>
                        </MasterTableView>
                    </telerik:RadGrid>
                </div>
            </div>
        </telerik:RadAjaxPanel>
        <p>&nbsp;</p>
        <telerik:RadAjaxPanel ID="panel_depts" runat="server" CssClass="container-fluid">
            <div class="row">
                <div class="col-md-12">
                    <div id="dept_list_core" runat="server" style="display: none;"></div>
                    <telerik:RadGrid ID="dept_grid_core" runat="server" AutoGenerateColumns="false" OnNeedDataSource="dept_grid_core_NeedDataSource">
                        <MasterTableView runat="server">
                            <HeaderStyle HorizontalAlign="Center" Width="20%" />
                            <ItemStyle HorizontalAlign="Center" Width="20%" />
                            <AlternatingItemStyle HorizontalAlign="Center" Width="20%" />
                            <Columns>
                                <telerik:GridBoundColumn DataField="id" HeaderText="数据中心编号"></telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="name" HeaderText="数据中心名称"></telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="parentid" HeaderText="数据中心父级编号"></telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="ordinal" HeaderText="顺序号"></telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="dingid" HeaderText="钉钉编号"></telerik:GridBoundColumn>
                            </Columns>
                        </MasterTableView>
                    </telerik:RadGrid>
                </div>
            </div>
        </telerik:RadAjaxPanel>
        <telerik:RadAjaxPanel ID="panel_depts_refresh" runat="server" CssClass="container-fluid" OnAjaxRequest="panel_depts_refresh_AjaxRequest">
            <div class="row">
                <div class="col-md-12">
                    <div id="dept_list_add" runat="server" style="display: none;"></div>
                </div>
            </div>
        </telerik:RadAjaxPanel>
        <p>&nbsp;</p>
    </form>
</body>
</html>
