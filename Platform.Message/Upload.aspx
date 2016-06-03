<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Upload.aspx.cs" Inherits="Upload" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>梁溪教育网络寻呼 - 附件上传</title>
    <style>
        .demo-container {
            width: 430px;
        }

            .demo-container .RadUpload .ruUploadProgress {
                width: 210px;
                display: inline-block;
                overflow: hidden;
                text-overflow: ellipsis;
            }

            .demo-container .ruFakeInput {
                width: 220px;
            }

            .demo-container .ruHeader {
                font-size: 0.8em;
            }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <telerik:RadScriptManager runat="server" ID="sm" />
            <telerik:RadAjaxPanel ID="ap" runat="server">
                <div class="demo-container size-narrow">
                    <telerik:RadAsyncUpload runat="server" ID="au" MultipleFileSelection="Automatic" InitialFileInputsCount="1" AutoAddFileInputs="true" Width="200px" TargetFolder="~/Resource/Slaves/Homory" TemporaryFolder="~/Resource/Temp" ChunkSize="1048576" LocalizationPath="~/Language" PostbackTriggers="save" OnFileUploaded="uploaded" />
                    <div style="clear: both;">&nbsp;</div>
                    <telerik:RadProgressArea runat="server" ID="pa" Width="300px" HeaderText="上传进度">
                        <Localization Cancel="取消" CurrentFileName="正在上传：" ElapsedTime="耗时：" EstimatedTime="预计：" Total="总共：" TotalFiles="总数：" TransferSpeed="上传速率：" Uploaded="当前上传：" UploadedFiles="总共上传：" />
                    </telerik:RadProgressArea>
                    <div style="clear: both;">&nbsp;</div>
                    <telerik:RadButton ID="save" runat="server" Width="80px" Text="保存"></telerik:RadButton>
                    <div style="clear: both;">&nbsp;</div>
                    <telerik:RadGrid ID="grid" runat="server" AutoGenerateColumns="false" OnNeedDataSource="need" OnItemCommand="del">
                        <HeaderStyle HorizontalAlign="Center" />
                        <MasterTableView runat="server" DataKeyNames="K">
                            <Columns>
                                <telerik:GridTemplateColumn HeaderText="已上传文件" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="300">
                                    <ItemTemplate>
                                        <%# Eval("V") %>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridButtonColumn HeaderText="" ItemStyle-HorizontalAlign="Center" ButtonType="LinkButton" Text="删除" CommandName="Delete"></telerik:GridButtonColumn>
                            </Columns>
                            <NoRecordsTemplate>&nbsp;</NoRecordsTemplate>
                        </MasterTableView>
                    </telerik:RadGrid>
                </div>
            </telerik:RadAjaxPanel>
        </div>
    </form>
</body>
</html>
