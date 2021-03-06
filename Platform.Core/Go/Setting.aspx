﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Setting.aspx.cs" Inherits="Go.GoSetting" %>

<%@ Register Src="~/Control/SideBar.ascx" TagPrefix="homory" TagName="SideBar" %>

<!DOCTYPE html>

<html>
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
    <!--[if lt IE 9]>
	    <script src="../Content/Homory/js/html5shiv.js"></script>
	    <script src="../Content/Homory/js/respond.min.js"></script>
    <![endif]-->
</head>
<body>
    <form id="formHome" runat="server">
        <div>
            <homory:SideBar runat="server" ID="SideBar" />
        </div>
        <telerik:RadAjaxManager ID="ajaxManager" runat="server" EnablePageHeadUpdate="false">
            <AjaxSettings>
                <telerik:AjaxSetting AjaxControlID="ajaxManager">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="viewer" />
                        <telerik:AjaxUpdatedControl ControlID="uploadControl" />
                    </UpdatedControls>
                </telerik:AjaxSetting>
            </AjaxSettings>
        </telerik:RadAjaxManager>
        <telerik:RadAjaxPanel ID="panel" runat="server" CssClass="container-fluid" LoadingPanelID="loading">
            <div class="row">
                <div class="col-md-6" style="text-align: center;">
                    <p>
                        <telerik:RadScriptBlock runat="server">
                            <%-- ReSharper disable UnusedParameter --%>
                            <%-- ReSharper disable UseOfImplicitGlobalInFunctionScope --%>
                            <script type="text/javascript">
                                function OnClientFilesUploaded(sender, args) {
                                    $find('<%=ajaxManager.ClientID %>').ajaxRequest();
							}
                            </script>
                            <%-- ReSharper restore UseOfImplicitGlobalInFunctionScope --%>
                            <%-- ReSharper restore UnusedParameter --%>
                        </telerik:RadScriptBlock>
                        <asp:Image ID="viewer" runat="server" Width="100" Height="100" />
                        <div style="margin: auto; width: 80px">
                            <telerik:RadAsyncUpload ID="upload" runat="server" MultipleFileSelection="Disabled" Skin="BlackMetroTouch" HideFileInput="true" AutoAddFileInputs="false" TemporaryFolder="~/Common/头像/用户" TargetFolder="~/Common/头像/用户" Localization-Cancel="取消" Localization-Remove="移除" Localization-Select="选择" OnClientFilesUploaded="OnClientFilesUploaded" OnFileUploaded="upload_FileUploaded">
                            </telerik:RadAsyncUpload>
                        </div>
                    </p>
                </div>
                <div class="col-md-6" style="text-align: left;">
                    <div class="btn btn-primary">密码</div>
                    <div class="coreTop"><br />
                        <input id="userPassword" type="password" value="" maxlength="32" style="width: 200px; height: 22px;" runat="server" />
                    </div>
                    <br />
                    <div class="btn btn-primary">密码确认</div>
                    <div class="coreTop"><br />
                        <input id="userPassword2" type="password" value="" maxlength="32" style="width: 200px; height: 22px;" runat="server" />
                    </div>
                    <div class="coreTop"><br />
                        <asp:Button ID="buttonSave" runat="server" CssClass="btn btn-warning" Style="margin-left: 50px;" Text="更改密码" OnClick="buttonSave_OnClick"></asp:Button>
                    </div>
                </div>
            </div>
        </telerik:RadAjaxPanel>
    </form>
</body>
</html>
