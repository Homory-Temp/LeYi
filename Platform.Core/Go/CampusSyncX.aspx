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

                function do_dingding() {
                    load_users();
                }

                function start_dingding() {
                    $.ajax({
                        url: url_get_access_token,
                        type: "GET",
                        data: { "corpid": corp_id, "corpsecret": corp_secret },
                        dataType: "json",
                        success: function (data) {
                            access_token = data.access_token;
                            var obj = JSON.parse($("#user_list").text());
                            $(obj).each(function (i) {
                                    $.ajax({
                                        url: url_user_get + access_token,
                                        type: "GET",
                                        data: { "userid": obj[i].Account },
                                        dataType: "json",
                                        success: function (data) {
                                            if (data.errcode == 0) {

                                            } else {
                                                $.ajax({
                                                    url: url_user_add + access_token,
                                                    type: "POST",
                                                    contentType: "application/json; charset=utf-8",
                                                    data: JSON.stringify({ "userid": obj[i].userid, "name": obj[i].name, "department": obj[i].department, "orderInDepts": obj[i].orderInDepts, "mobile": obj[i].mobile, "position": obj[i].position }),
                                                    dataType: "json",
                                                    success: function (data) {
                                                    },
                                                    error: function (err_o, err_e, err_t) {
                                                    }
                                                });
                                            }
                                        },
                                        error: function (err_o, err_e, err_t) {
                                        }
                                    });
                            });
                        },
                        error: function (err_o, err_e, err_t) {
                        }
                    });
                }
            </script>
            <telerik:RadCodeBlock runat="server">
                <script>
                    function load_users() {
                        $find("<%= panel_depts_refresh.ClientID %>").ajaxRequest();
                    }
                </script>
            </telerik:RadCodeBlock>
        </div>
        <div id="user_list" runat="server" style="display: ;"></div>
        <telerik:RadAjaxPanel ID="panel" runat="server" CssClass="container-fluid" OnAjaxRequest="panel_AjaxRequest">
            <div class="row">
                <div class="col-md-12">
                </div>
            </div>
        </telerik:RadAjaxPanel>
        <p>&nbsp;</p>
        <telerik:RadAjaxPanel ID="panel_depts" runat="server" CssClass="container-fluid">
            <div class="row">
                <div class="col-md-12">
                    <telerik:RadGrid ID="grid" runat="server" AutoGenerateColumns="false" OnNeedDataSource="grid_NeedDataSource">
                        <MasterTableView runat="server">
                            <HeaderStyle HorizontalAlign="Center" Width="16%" />
                            <ItemStyle HorizontalAlign="Center" Width="16%" />
                            <AlternatingItemStyle HorizontalAlign="Center" Width="16%" />
                            <Columns>
                                <telerik:GridBoundColumn DataField="Account" HeaderText="账号"></telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="RealName" HeaderText="姓名"></telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="Phone" HeaderText="手机号码"></telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="Ordinal" HeaderText="顺序号"></telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="PerStaff" HeaderText="在编状态"></telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="DingKey" HeaderText="钉钉部门编号"></telerik:GridBoundColumn>
                            </Columns>
                        </MasterTableView>
                    </telerik:RadGrid>
                </div>
            </div>
        </telerik:RadAjaxPanel>
        <telerik:RadAjaxPanel ID="panel_depts_refresh" runat="server" CssClass="container-fluid" OnAjaxRequest="panel_depts_refresh_AjaxRequest">
            <div class="row">
                <div class="col-md-12">
                </div>
            </div>
        </telerik:RadAjaxPanel>
        <p>&nbsp;</p>
    </form>
</body>
</html>
