using System;
using System.Linq;
using EntityFramework.Extensions;
using Homory.Model;
using Telerik.Web.UI;

namespace Extended
{
	public partial class ExtendedStudioIcon : HomoryCorePageWithNotify
	{
		private const string Right = "Studio";

		protected void Page_Load(object sender, EventArgs e)
		{
			if (Request.QueryString.Count == 0)
			{
// ReSharper disable Html.PathError
				Response.Redirect("~/Go/Studio", false);
// ReSharper restore Html.PathError
			}
		}

		protected void upload_FileUploaded(object sender, FileUploadedEventArgs e)
		{
			using (var upload = sender as RadAsyncUpload)
			{
// ReSharper disable PossibleNullReferenceException
				if (upload.UploadedFiles.Count > 0)
// ReSharper restore PossibleNullReferenceException
				{
					var id = Guid.Parse(Request.QueryString[0]);
					var path = string.Format("~/Common/头像/群组/{0}.png", id);
					upload.UploadedFiles[0].SaveAs(Server.MapPath(path), true);
					HomoryContext.Value.Group.Where(o => o.Id == id).Update(o => new Group
					{
						Icon = path
					});
					HomoryContext.Value.SaveChanges();
				}
			}
			panelInner.ResponseScripts.Add("RadClose();");
		}

		protected override string PageRight
		{
			get { return Right; }
		}
	}
}
