using Homory.Model;
using System;
using System.Linq;
using Telerik.Web.UI;
using Resource = Homory.Model.Resource;
using ResourceType = Homory.Model.ResourceType;

namespace Popup
{
    public partial class PopupPublishAttachment : HomoryResourcePage
	{
		protected void Page_Load(object sender, EventArgs e)
		{

		}

		protected ResourceType ResourceType
		{
			get
			{
				switch (Request.QueryString["Type"])
				{
					case "Courseware":
						return ResourceType.课件;
					case "Paper":
						return ResourceType.试卷;
					case "Media":
						return ResourceType.视频;
					case "Article":
						return ResourceType.文章;
					default:
						return ResourceType.文章;
				}
			}
		}

		protected Resource CurrentResource
		{
			get
			{
				return CurrentUser.Resource.First(o => o.State == State.审核 && o.Type == ResourceType && o.UserId == CurrentUser.Id);
			}
		}

		protected void publish_attachment_upload_OnFileUploaded(object sender, FileUploadedEventArgs e)
		{
			var id = CurrentResource.Id;
			var file = e.File;
			ResourceFileType type;
			switch (file.GetExtension().Replace(".", ""))
			{
				case "jpg":
				case "jpeg":
				case "png":
				case "gif":
				case "bmp":
					type = ResourceFileType.Image;
					break;
				case "rar":
				case "zip":
				case "7z":
					type = ResourceFileType.Zip;
					break;
				case "doc":
				case "docx":
				case "txt":
				case "rtf":
					type = ResourceFileType.Word;
					break;
				case "ppt":
				case "pptx":
					type = ResourceFileType.Powerpoint;
					break;
				case "xls":
				case "xlsx":
					type = ResourceFileType.Excel;
					break;
				case "pdf":
					type = ResourceFileType.Pdf;
					break;
				default:
					type = ResourceFileType.Media;
					break;
			}
			var name = string.Format("../Common/资源/{2}/附件/{1}_{0}", file.FileName, HomoryContext.Value.GetId(), CurrentUser.Id.ToString().ToUpper());
			file.SaveAs(Server.MapPath(name), true);
			var ra = new ResourceAttachment
			{
				Id = HomoryContext.Value.GetId(),
				ResourceId = id,
				FileType = type,
				Title = file.GetName(),
				Source = name,
				State = State.启用
			};
			HomoryContext.Value.ResourceAttachment.Add(ra);
			HomoryContext.Value.SaveChanges();
		}

		protected void publish_attachment_commit_OnServerClick(object sender, EventArgs e)
		{
			popup_publish_attachment_panel.ResponseScripts.Add("RadCloseRebind();");
		}

		protected override bool ShouldOnline
		{
			get { return true; }
		}
	}
}
