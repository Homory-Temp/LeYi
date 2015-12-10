using Aspose.Pdf.Devices;
using Homory.Model;
using System;
using System.IO;
using System.Linq;
using Telerik.Web.UI;
using Resource = Homory.Model.Resource;
using ResourceType = Homory.Model.ResourceType;

public partial class Popup_PublishImportClass : System.Web.UI.Page
{
    protected Lazy<Entities> HomoryContext = new Lazy<Entities>(() => new Entities());

    protected Guid UserId
    {
        get
        {
            return Guid.Parse(Request.QueryString["UserId"]);

        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            var part = ResourceType == ResourceType.视频
                ? "上传文件不得超过1GB，格式仅限视频文件"
                : "上传文件不得超过100MB，格式仅限Office文档、文本文档和PDF文档";
            popup_publish_import_label.InnerHtml = string.Format("1、上传文件（{0}，上传过程中请勿关闭窗口）", part);
            publish_import_upload.AllowedFileExtensions = ResourceType == ResourceType.视频
                ? "flv,mp4,mpg,mpeg,wmv,avi,rm,rmvb".Split(new[] { ',' })
                : "doc,docx,ppt,pptx,xls,xlsx,txt,rtf,pdf".Split(new[] { ',' });
            popup_publish_import_sf_label.InnerText = ResourceType == ResourceType.视频
                ? "( flv, mp4, mpg, mpeg, wmv, avi, rm, rmvb )"
                : string.Empty;
            publish_import_upload.MaxFileSize = ResourceType == ResourceType.视频 ? 1048576000 : 104857600;
        }
    }

    protected ResourceType ResourceType
    {
        get
        {
            return (ResourceType)int.Parse(Session["ClassResType"].ToString());
        }
    }
    

    protected User CurrentUser
    {
        get
        {

            return HomoryContext.Value.User.SingleOrDefault(o => o.Id == UserId);
        }

    }
    protected Resource CurrentResource
    {
        get
        {
            return CurrentUser.Resource.First(o => o.State == State.审核 && o.Type == ResourceType && o.UserId == CurrentUser.Id && o.AssistantType == 1);
        }
    }

    protected void publish_import_upload_OnFileUploaded(object sender, FileUploadedEventArgs e)
    {
        var id = CurrentResource.Id;
        var file = e.File;
        var tempId = HomoryContext.Value.GetId();
        var suffix = file.GetExtension().Replace(".", "").ToLower();
        var path = string.Format("../Common/资源/{0}/{1}/{2}.{3}", CurrentUser.Id.ToString().ToUpper(), ResourceType.ToString(),
            tempId.ToString().ToUpper(), ResourceType == ResourceType.视频 ? suffix == "flv" ? suffix : "mp4" : "pdf");
        var pathX = Server.MapPath(path);
        var source = string.Format("../Common/资源/{0}/{1}/{2}.{3}", CurrentUser.Id.ToString().ToUpper(), ResourceType.ToString(),
            tempId.ToString().ToUpper(), suffix);
        var sourceX = Server.MapPath(source);
        var cpic = path.Replace(".pdf", ".jpg").Replace(".flv", ".jpg").Replace(".mp4", ".jpg");
        var cpicX = pathX.Replace(".pdf", ".jpg").Replace(".flv", ".jpg").Replace(".mp4", ".jpg");
        var res = HomoryContext.Value.Resource.Single(o => o.Id == id);
        file.SaveAs(sourceX, true);
        switch (suffix)
        {
            case "doc":
            case "docx":
            case "txt":
            case "rtf":
                var docW = new Aspose.Words.Document(sourceX);
                docW.Save(pathX, Aspose.Words.SaveFormat.Pdf);
                docW.Save(cpicX, Aspose.Words.SaveFormat.Jpeg);
                res.Image = cpic;
                res.FileType = ResourceFileType.Word;
                res.Thumbnail = ((int)ResourceFileType.Word).ToString();
                break;
            case "ppt":
            case "pptx":
                var docP = new Aspose.Slides.Presentation(sourceX);
                docP.Save(pathX, Aspose.Slides.Export.SaveFormat.Pdf);
                var tcdocp = new Aspose.Pdf.Document(pathX);
                using (var imageStream = new FileStream(cpicX, FileMode.Create))
                {
                    var resolution = new Resolution(300);
                    var jpegDevice = new JpegDevice(resolution, 100);
                    jpegDevice.Process(tcdocp.Pages[1], imageStream);
                    imageStream.Close();
                }
                res.Image = cpic;
                res.FileType = ResourceFileType.Powerpoint;
                res.Thumbnail = ((int)ResourceFileType.Powerpoint).ToString();
                break;
            case "xls":
            case "xlsx":
                var docE = new Aspose.Cells.Workbook(sourceX);
                docE.Save(pathX, Aspose.Cells.SaveFormat.Pdf);
                var tcdoce = new Aspose.Pdf.Document(pathX);
                using (var imageStream = new FileStream(cpicX, FileMode.Create))
                {
                    var resolution = new Resolution(300);
                    var jpegDevice = new JpegDevice(resolution, 100);
                    jpegDevice.Process(tcdoce.Pages[1], imageStream);
                    imageStream.Close();
                }
                res.Image = cpic;
                res.FileType = ResourceFileType.Excel;
                res.Thumbnail = ((int)ResourceFileType.Excel).ToString();
                break;
            case "pdf":
                var tcdoc = new Aspose.Pdf.Document(pathX);
                using (var imageStream = new FileStream(cpicX, FileMode.Create))
                {
                    var resolution = new Resolution(300);
                    var jpegDevice = new JpegDevice(resolution, 100);
                    jpegDevice.Process(tcdoc.Pages[1], imageStream);
                    imageStream.Close();
                }
                res.Image = cpic;
                res.FileType = ResourceFileType.Pdf;
                res.Thumbnail = ((int)ResourceFileType.Pdf).ToString();
                break;
            case "avi":
            case "mpg":
            case "mpeg":
            case "flv":
            case "mp4":
            case "rm":
            case "rmvb":
            case "wmv":
                NReco.VideoConverter.FFMpegConverter c = new NReco.VideoConverter.FFMpegConverter();
                c.GetVideoThumbnail(sourceX, cpicX, 2F);
                //if (!sourceX.EndsWith("flv"))
                //{
                //    c.ConvertMedia(sourceX, pathX, NReco.VideoConverter.Format.flv);
                //}
                res.Image = cpic;
                res.FileType = ResourceFileType.Media;
                res.Thumbnail = ((int)ResourceFileType.Media).ToString();
                break;
        }
        res.SourceName = file.GetName();
        res.Title = file.GetNameWithoutExtension();
        res.Source = source;
        res.Preview = path;
        res.Converted = true;
        HomoryContext.Value.SaveChanges();
    }

    protected void publish_import_commit_OnServerClick(object sender, EventArgs e)
    {
        popup_publish_import_panel.ResponseScripts.Add("top.location.reload();");
    }
}