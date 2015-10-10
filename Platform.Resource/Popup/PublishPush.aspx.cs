using System;
using System.Data.Entity.Migrations;
using System.IO;
using System.Linq;
using System.Web.UI.HtmlControls;
using Homory.Model;
using Telerik.Web.UI;
using Resource = Homory.Model.Resource;
using ResourceType = Homory.Model.ResourceType;

namespace Popup
{
	public partial class PopupPublishPush : HomoryResourcePage
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			var content = filter.Value.Trim();
			result.DataSource = HomoryContext.Value.Resource.Where(o => o.UserId == CurrentUser.Id && o.State == State.启用).ToList().Where(o => o.Title.Contains(content)).ToList();
			result.DataBind();
		}

		protected Guid CatalogId
		{
			get { return Guid.Parse(Request.QueryString[0]); }
		}

		protected void filterGo_OnServerClick(object sender, EventArgs e)
		{
			var content = filter.Value.Trim();
			result.DataSource = HomoryContext.Value.Resource.Where(o => o.UserId == CurrentUser.Id && o.State == State.启用).ToList().Where(o => o.Title.Contains(content)).ToList();
			result.DataBind();
		}

		protected override bool ShouldOnline
		{
			get { return true; }
		}

		protected void btnDo_OnServerClick(object sender, EventArgs e)
		{
			var id = Guid.Parse(((HtmlAnchor)sender).Attributes["itemid"]);
			var rc = new ResourceCatalog
			{
				ResourceId = id,
				CatalogId = CatalogId,
				State =
					HomoryContext.Value.ResourceCatalog.Count(
						o => o.CatalogId == CatalogId && o.State == State.启用 && o.ResourceId == id) ==
					0
						? State.启用
						: State.删除
			};
			HomoryContext.Value.ResourceCatalog.AddOrUpdate(rc);
			HomoryContext.Value.SaveChanges();
			var content = filter.Value.Trim();
			result.DataSource = HomoryContext.Value.Resource.Where(o => o.UserId == CurrentUser.Id && o.State == State.启用).ToList().Where(o => o.Title.Contains(content)).ToList();
			result.DataBind();
		}
	}
}
