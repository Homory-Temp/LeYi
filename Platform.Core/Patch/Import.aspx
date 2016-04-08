<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Import.aspx.cs" Inherits="Extended_Import" %>

<%@ Register Src="~/Control/SideBar.ascx" TagPrefix="homory" TagName="SideBar" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=Edge,Chrome=1" />
    <meta name="viewport" content="width=device-width,initial-scale=1,minimum-scale=1,maximum-scale=1" />
    <title>基础数据维护</title>
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
    <form id="form1" runat="server">
        <div>
        <telerik:RadScriptManager ID="sm" runat="server"></telerik:RadScriptManager>
        <telerik:RadAjaxLoadingPanel ID="loading" runat="server">
            <div>&nbsp;</div>
            <div class="btn btn-lg btn-warning" style="margin-top: 50px;">正在加载 请稍候....</div>
        </telerik:RadAjaxLoadingPanel>
            <telerik:RadAjaxPanel ID="panel" runat="server" CssClass="container" LoadingPanelID="loading">
                <div class="row">
                    <div class="col-md-12">
                        <div>
                            <div class="btn btn-primary">1</div>
                            上传教师Excel文档
                        </div>
                        <div style="margin-left: 50px;">
                            <telerik:RadAsyncUpload RegisterWithScriptManager="True" runat="server" ID="im_up" Skin="Metro" OnFileUploaded="im_up_FileUploaded" HideFileInput="False" LocalizationPath="~/Language" TemporaryFolder="~/Temp" TargetFolder="~/Temp" PostbackTriggers="im_do" ChunkSize="1048576" AutoAddFileInputs="False" MaxFileInputsCount="1" InitialFileInputsCount="1" />
                            <input type="hidden" id="file" runat="server" />
                        </div>
                        <div>&nbsp;</div>
                        <div>&nbsp;</div>
                        <div>
                            <div class="btn btn-primary">2</div>
                            <telerik:RadButton ID="im_do" runat="server" Text="生成并预览教师数据" OnClick="im_do_Click"></telerik:RadButton>
                        </div>
                        <div style="margin-left: 50px;">
                            <asp:GridView ID="grid" runat="server" AutoGenerateColumns="true" Font-Size="12px">
                            </asp:GridView>
                        </div>
                    </div>
                </div>
            </telerik:RadAjaxPanel>
            <div>&nbsp;</div>
            <div>&nbsp;</div>
            <telerik:RadAjaxPanel ID="panelX" runat="server" CssClass="container" LoadingPanelID="loading">
                <div class="row">
                    <div class="col-md-12">
                        <div>
                            <div class="btn btn-primary">3</div>
                            <telerik:RadButton ID="im_ok" runat="server" Text="完成教师数据导入" OnClick="im_ok_Click"></telerik:RadButton>
                        </div>
                    </div>
                </div>
            </telerik:RadAjaxPanel>
        </div>
    </form>
</body>
</html>
