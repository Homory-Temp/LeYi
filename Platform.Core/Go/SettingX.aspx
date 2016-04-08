<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SettingX.aspx.cs" Inherits="Go.GoSettingX" %>

<%@ Register Src="~/Control/SideBar.ascx" TagPrefix="homory" TagName="SideBar" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=Edge,Chrome=1" />
    <meta name="viewport" content="width=device-width,initial-scale=1,minimum-scale=1,maximum-scale=1" />
    <title>密码修改</title>
    <script src="../Content/jQuery/jquery.min.js"></script>
    <link href="../assets/css/bootstrap.min.css" rel="stylesheet" />
    <link href="../assets/css/style-responsive.css" rel="stylesheet" />
    <link href="../assets/css/style.css" rel="stylesheet" />
    <script src="../assets/js/bootstrap.min.js"></script>
    <link href="../Content/Homory/css/common.css" rel="stylesheet" />
    <link href="../Content/Core/css/common.css" rel="stylesheet" />
    <script src="../Content/Homory/js/common.js"></script>
    <script src="../Content/Homory/js/notify.min.js"></script>
    <!--[if lt IE 9]>
	    <script src="../Content/Homory/js/html5shiv.js"></script>
	    <script src="../Content/Homory/js/respond.min.js"></script>
    <![endif]-->
</head>
<body>
    <form id="formHome" runat="server">
        <div class="row">
            <div class="col-md-5">&nbsp;</div>
            <div class="col-md-6">
                <div class="btn btn-primary">密码</div>
                <div class="coreTop">
                    <br />
                    <input id="userPassword" type="password" value="" maxlength="32" style="width: 200px; height: 22px;" runat="server" />
                </div>
                <br />
                <div class="btn btn-primary">密码确认</div>
                <div class="coreTop">
                    <br />
                    <input id="userPassword2" type="password" value="" maxlength="32" style="width: 200px; height: 22px;" runat="server" />
                </div>
                <div class="coreTop">
                    <br />
                    <asp:Button ID="buttonSave" runat="server" CssClass="btn btn-warning" Style="margin-left: 50px;" Text="更改密码" OnClick="buttonSave_OnClick"></asp:Button>
                </div>
            </div>
            <div class="col-md-1">&nbsp;</div>
        </div>
    </form>
</body>
</html>
