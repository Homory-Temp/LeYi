<%@ Page Title="梁溪教育入学辅助查询系统 - 数据导入" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeFile="Import.aspx.cs" Inherits="Import" %>

<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<asp:Content ID="content" ContentPlaceHolderID="body" runat="Server">
    <telerik:RadAjaxPanel ID="panel" runat="server" CssClass="container" LoadingPanelID="loading">
        <div class="row">
            <div class="col-md-12">
                <div>
                    1、上传文档&nbsp;&nbsp;&nbsp;&nbsp;<a href="导入示例.xlsx">导入模板下载</a>&nbsp;&nbsp;&nbsp;&nbsp;<span style="color: red;">（姓名、入学年份、住址必填）</span>
                </div>
                <div style="margin-left: 50px; margin-top: 10px;">
                    <telerik:RadAsyncUpload RegisterWithScriptManager="True" runat="server" ID="im_up" Skin="Bootstrap" OnFileUploaded="im_up_FileUploaded" HideFileInput="False" LocalizationPath="~/Language" TemporaryFolder="~/Temp" TargetFolder="~/Temp" PostbackTriggers="im_do" ChunkSize="1048576" AutoAddFileInputs="False" MaxFileInputsCount="1" InitialFileInputsCount="1" />
                    <input type="hidden" id="file" runat="server" />
                </div>
                <div>&nbsp;</div>
                <div>&nbsp;</div>
                <div>
                    2、<telerik:RadButton ID="im_do" runat="server" Text="预览文档" OnClick="im_do_Click"></telerik:RadButton>
                </div>
                <div style="margin-left: 50px; margin-top: 10px;">
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
                    3、<telerik:RadButton ID="im_ok" runat="server" Text="开始导入" OnClick="im_ok_Click"></telerik:RadButton>
                </div>
            </div>
        </div>
    </telerik:RadAjaxPanel>
    <br />
    <br />
    <br />
    <br />
</asp:Content>
