using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web.UI.HtmlControls;
using Aspose.Pdf;
using Aspose.Words.Lists;
using Homory.Model;
using Telerik.Web.UI;

namespace Control
{
	public partial class ControlCommonPushX : HomoryResourceControl
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{
				var gid = Guid.Parse(Request.QueryString[0]);
				var list = new List<dynamic>();
					list.AddRange(
						HomoryContext.Value.Catalog.Where(o => o.State < State.审核 && o.Type == CatalogType.团队_教研 && o.TopId == gid).OrderBy(o => o.Ordinal)
							.ToList()
							.Select(o => new {o.Id, o.ParentId, o.Name}));
				groupTree.DataSource = list;
				groupTree.DataBind();
				groupTree.ExpandAllNodes();
			}
		}

		protected override bool ShouldOnline
		{
			get { return false; }
		}

		protected void groupTree_OnNodeClick(object sender, RadTreeNodeEventArgs e)
		{
			e.Node.Selected = true;
			pop.Visible = e.Node.Nodes.Count == 0;
			pop.Attributes["onclick"] = string.Format("popupPush('../Popup/PublishPush?{0}');",e.Node.Value);
			result.DataSource =
				HomoryContext.Value.Resource.Where(o => o.UserId == CurrentUser.Id && o.State == State.启用)
					.ToList()
					.Where(o => o.ResourceCatalog.Count(p => p.CatalogId == CatalogId && p.State == State.启用) > 0)
					.ToList();
			result.DataBind();
		}

		protected Guid CatalogId
		{
			get
			{
				if (groupTree.SelectedNode == null)
				{
					return Guid.Empty;
				}
				return Guid.Parse(groupTree.SelectedNode.Value);
			}
		}

		public void ReBind()
		{
			result.DataSource =
				HomoryContext.Value.Resource.Where(o => o.UserId == CurrentUser.Id && o.State == State.启用)
					.ToList()
					.Where(o => o.ResourceCatalog.Count(p => p.CatalogId == CatalogId && p.State == State.启用) > 0)
					.ToList();
			result.DataBind();
		}

		protected void btnDo_OnServerClick(object sender, EventArgs e)
		{
			var id = Guid.Parse(((HtmlAnchor)sender).Attributes["itemid"]);
			var rc = new ResourceCatalog
			{
				ResourceId = id,
				CatalogId = CatalogId,
				State = State.删除
			};
			HomoryContext.Value.ResourceCatalog.AddOrUpdate(rc);
			HomoryContext.Value.SaveChanges();
			result.DataSource =
				HomoryContext.Value.Resource.Where(o => o.UserId == CurrentUser.Id && o.State == State.启用)
					.ToList()
					.Where(o => o.ResourceCatalog.Count(p => p.CatalogId == CatalogId && p.State == State.启用) > 0)
					.ToList();
			result.DataBind();
		}
	}
}
